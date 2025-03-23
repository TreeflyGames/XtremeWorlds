using Core;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Mirage.Sharp.Asfw;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    public class Shop : IDisposable
    {
        #region Fields and Properties

        private readonly IMemoryCache _cache;
        private readonly ILogger<Shop> _logger;
        private readonly SemaphoreSlim _syncLock = new(1, 1);
        private readonly CancellationTokenSource _cts = new();
        private static readonly ConcurrentDictionary<int, ShopMetrics> _shopMetrics = new();

        // Configuration
        private static readonly TimeSpan DefaultCacheExpiration = TimeSpan.FromMinutes(15);
        private const int DefaultBuyRate = 100;
        private const int ShopTimeoutSeconds = 300;

        // Events
        public event EventHandler<ShopUpdatedEventArgs> ShopUpdated;
        public event EventHandler<ShopMetricsUpdatedEventArgs> MetricsUpdated;

        #endregion

        #region Constructor and Disposal

        public Shop(IMemoryCache cache, ILogger<Shop> logger)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            InitializeDefaultShops();
        }

        public void Dispose()
        {
            _cts.Cancel();
            _cts.Dispose();
            _syncLock.Dispose();
        }

        #endregion

        #region Core Shop Operations

        public async Task CloseShopAsync()
        {
            try
            {
                await NetworkSend.SendCloseShopAsync();
                await Task.WhenAll(
                    Gui.HideWindowAsync(Gui.GetWindowIndex("winShop")),
                    Gui.HideWindowAsync(Gui.GetWindowIndex("winDescription"))
                );
                ResetShopSelection();
                GameState.InShop = -1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error closing shop");
                throw;
            }
        }

        private void ResetShopSelection()
        {
            GameState.shopSelectedSlot = 0L;
            GameState.shopSelectedItem = 0L;
            GameState.shopIsSelling = false;
            GameState.ShopAction = 0;
        }

        #endregion

        #region Database Operations

        public async Task ClearShopAsync(int index)
        {
            ValidateShopIndex(index);
            await _syncLock.WaitAsync();
            try
            {
                Core.Type.Shop[index] = CreateDefaultShopStruct();
                GameState.Shop_Loaded[index] = 0;
                _shopMetrics.TryRemove(index, out _);
                _cache.Remove(GetCacheKey(index));
            }
            finally
            {
                _syncLock.Release();
            }
        }

        public async Task ClearAllShopsAsync()
        {
            await _syncLock.WaitAsync();
            try
            {
                Core.Type.Shop = new Core.Type.ShopStruct[Constant.MAX_SHOPS];
                _shopMetrics.Clear();
                _cache.RemoveAll();
                await Task.WhenAll(Enumerable.Range(0, Constant.MAX_SHOPS)
                    .Select(i => ClearShopAsync(i)));
            }
            finally
            {
                _syncLock.Release();
            }
        }

        public async Task StreamShopAsync(int shopNum)
        {
            ValidateShopIndex(shopNum);
            if (!ShouldStreamShop(shopNum)) return;

            await _syncLock.WaitAsync();
            try
            {
                if (GameState.Shop_Loaded[shopNum] == 0)
                {
                    GameState.Shop_Loaded[shopNum] = 1;
                    await SendRequestShopAsync(shopNum);
                    UpdateShopMetrics(shopNum, DateTime.UtcNow);
                }
            }
            finally
            {
                _syncLock.Release();
            }
        }

        #endregion

        #region Packet Handling

        public async Task HandleOpenShopAsync(byte[] data)
        {
            using var buffer = new ByteStream(data);
            int shopNum = buffer.ReadInt32();
            ValidateShopIndex(shopNum);
            await GameLogic.OpenShopAsync(shopNum);
            UpdateShopMetrics(shopNum);
        }

        public void HandleResetShopAction(byte[] data)
        {
            GameState.ShopAction = 0;
        }

        public async Task HandleUpdateShopAsync(byte[] data)
        {
            using var buffer = new ByteStream(data);
            int shopNum = buffer.ReadInt32();
            ValidateShopIndex(shopNum);

            await _syncLock.WaitAsync();
            try
            {
                var shop = await UpdateShopFromBufferAsync(shopNum, buffer);
                _cache.Set(GetCacheKey(shopNum), shop, DefaultCacheExpiration);
                ShopUpdated?.Invoke(this, new ShopUpdatedEventArgs(shopNum));
            }
            finally
            {
                _syncLock.Release();
            }
        }

        #endregion

        #region Outgoing Requests

        public async Task BuyItemAsync(int shopSlot)
        {
            ValidateSlot(shopSlot);
            if (!await CanBuyItemAsync(shopSlot)) return;

            using var buffer = await CreateBuyItemBufferAsync(shopSlot);
            await SendDataAsync(buffer);
        }

        public async Task SellItemAsync(int invSlot)
        {
            ValidateSlot(invSlot);
            if (!await CanSellItemAsync(invSlot)) return;

            using var buffer = await CreateSellItemBufferAsync(invSlot);
            await SendDataAsync(buffer);
        }

        #endregion

        #region Enhanced Features

        #region Inventory and Currency

        public async Task<bool> CanBuyItemAsync(int shopSlot)
        {
            var shop = Core.Type.Shop[GameState.InShop];
            var item = shop.TradeItem[shopSlot];
            return await GameState.Player.HasCurrencyAsync(item.CurrencyType, item.CostValue);
        }

        public async Task<bool> CanSellItemAsync(int invSlot)
        {
            return await GameState.Player.HasItemInInventoryAsync(invSlot);
        }

        public async Task<decimal> ConvertCurrencyAsync(int fromType, int toType, decimal amount)
        {
            var rates = await GetCurrencyExchangeRatesAsync();
            if (!rates.TryGetValue((fromType, toType), out decimal rate))
                throw new InvalidOperationException("Currency conversion not supported.");
            return amount * rate;
        }

        #endregion

        #region Shop Management

        public async Task AddDynamicCategoryAsync(int shopNum, string categoryName, IEnumerable<int> items)
        {
            ValidateShopIndex(shopNum);
            await _syncLock.WaitAsync();
            try
            {
                var shop = Core.Type.Shop[shopNum];
                shop.Categories[categoryName] = items.ToList();
                await UpdateCacheAsync(shopNum, shop);
            }
            finally
            {
                _syncLock.Release();
            }
        }

        public async Task ApplyBulkDiscountAsync(int shopNum, decimal discountPercentage, TimeSpan duration)
        {
            ValidateShopIndex(shopNum);
            if (discountPercentage < 0 || discountPercentage > 100)
                throw new ArgumentException("Invalid discount percentage");

            await _syncLock.WaitAsync();
            try
            {
                var shop = Core.Type.Shop[shopNum];
                shop.BuyRate = (int)(shop.BuyRate * (1 - discountPercentage / 100));
                await UpdateCacheAsync(shopNum, shop);

                _ = Task.Run(async () =>
                {
                    await Task.Delay(duration, _cts.Token);
                    await ResetBuyRateAsync(shopNum);
                });
            }
            finally
            {
                _syncLock.Release();
            }
        }

        #endregion

        #region Analytics

        public async Task<ShopAnalytics> GetShopAnalyticsAsync(int shopNum)
        {
            ValidateShopIndex(shopNum);
            var metrics = _shopMetrics.GetOrAdd(shopNum, _ => new ShopMetrics());
            return new ShopAnalytics
            {
                ShopId = shopNum,
                TotalAccesses = metrics.AccessCount,
                LastAccessed = metrics.LastAccess,
                AverageTransactionValue = await CalculateAverageTransactionValueAsync(shopNum)
            };
        }

        #endregion

        #region Persistence

        public async Task SaveShopDataAsync(string filePath)
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                await using var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                await JsonSerializer.SerializeAsync(stream, Core.Type.Shop, options);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to save shop data");
                throw;
            }
        }

        public async Task LoadShopDataAsync(string filePath)
        {
            try
            {
                await using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                Core.Type.Shop = await JsonSerializer.DeserializeAsync<Core.Type.ShopStruct[]>(stream)
                    ?? throw new InvalidDataException("Failed to deserialize shop data");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load shop data");
                throw;
            }
        }

        #endregion

        #region Advanced Features

        public async Task RecommendItemsAsync(int shopNum, int playerId)
        {
            var playerHistory = await GameState.Player.GetPurchaseHistoryAsync(playerId);
            var shopItems = Core.Type.Shop[shopNum].TradeItem;
            var recommendations = shopItems
                .Where(ti => ti.Item >= 0 && !playerHistory.Contains(ti.Item))
                .OrderBy(ti => ti.ItemValue)
                .Take(3)
                .Select(ti => ti.Item)
                .ToList();

            return recommendations;
        }

        public async Task<bool> IsShopOverstockedAsync(int shopNum)
        {
            var shop = Core.Type.Shop[shopNum];
            var activeItems = shop.TradeItem.Count(ti => ti.Item >= 0);
            var avgValue = await CalculateAverageTransactionValueAsync(shopNum);
            return activeItems > Constant.MAX_TRADES * 0.8 && avgValue < 100;
        }

        #endregion

        #endregion

        #region Helper Methods

        private Core.Type.ShopStruct CreateDefaultShopStruct() => new()
        {
            Name = string.Empty,
            TradeItem = Enumerable.Range(0, Constant.MAX_TRADES)
                .Select(_ => new Core.Type.TradeItemStruct { Item = -1, CostItem = -1 })
                .ToArray(),
            BuyRate = DefaultBuyRate,
            Categories = new Dictionary<string, List<int>>()
        };

        private async Task UpdateCacheAsync(int shopNum, Core.Type.ShopStruct shop)
        {
            _cache.Set(GetCacheKey(shopNum), shop, DefaultCacheExpiration);
            await Task.CompletedTask;
        }

        private string GetCacheKey(int shopNum) => $"Shop_{shopNum}";

        private void UpdateShopMetrics(int shopNum, DateTime? time = null)
        {
            var metrics = _shopMetrics.GetOrAdd(shopNum, _ => new ShopMetrics());
            metrics.AccessCount++;
            metrics.LastAccess = time ?? DateTime.UtcNow;
            MetricsUpdated?.Invoke(this, new ShopMetricsUpdatedEventArgs(shopNum, metrics));
        }

        private void ValidateShopIndex(int index)
        {
            if (index < 0 || index >= Constant.MAX_SHOPS)
                throw new ArgumentOutOfRangeException(nameof(index));
        }

        private void ValidateSlot(int slot)
        {
            if (slot < 0 || slot >= Constant.MAX_TRADES)
                throw new ArgumentOutOfRangeException(nameof(slot));
        }

        #endregion

        #region Nested Classes

        private class ShopMetrics
        {
            public int AccessCount { get; set; }
            public DateTime LastAccess { get; set; }
        }

        public class ShopAnalytics
        {
            public int ShopId { get; set; }
            public int TotalAccesses { get; set; }
            public DateTime LastAccessed { get; set; }
            public decimal AverageTransactionValue { get; set; }
        }

        public class ShopMetricsUpdatedEventArgs : EventArgs
        {
            public int ShopNumber { get; }
            public ShopMetrics Metrics { get; }

            public ShopMetricsUpdatedEventArgs(int shopNumber, ShopMetrics metrics)
            {
                ShopNumber = shopNumber;
                Metrics = metrics;
            }
        }

        #endregion
    }
}
