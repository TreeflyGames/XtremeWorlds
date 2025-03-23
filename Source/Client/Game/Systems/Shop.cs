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
using System.Security.Cryptography;

namespace Client
{
    public class Shop : IDisposable
    {
        #region Fields and Properties

        private readonly IMemoryCache _cache;
        private readonly ILogger<Shop> _logger;
        private readonly SemaphoreSlim _syncLock = new(1, 1);
        private readonly CancellationTokenSource _cts = new();
        private readonly ConcurrentDictionary<int, ShopMetrics> _shopMetrics = new();
        private readonly ConcurrentDictionary<int, ShopReputation> _shopReputations = new();
        private readonly IShopEventBus _eventBus;

        // Configuration
        private static readonly TimeSpan DefaultCacheExpiration = TimeSpan.FromMinutes(15);
        private const int DefaultBuyRate = 100;
        private const int ShopTimeoutSeconds = 300;
        private const int MaxConcurrentOperations = 5;

        // Enhanced Properties
        public ShopStatus Status { get; private set; } = ShopStatus.Offline;
        public IReadOnlyDictionary<int, ShopReputation> Reputations => _shopReputations;
        public ShopConfiguration Config { get; private set; }

        // Events
        public event EventHandler<ShopUpdatedEventArgs> ShopUpdated;
        public event EventHandler<ShopMetricsUpdatedEventArgs> MetricsUpdated;
        public event EventHandler<ShopStatusChangedEventArgs> StatusChanged;
        public event EventHandler<ShopTransactionEventArgs> TransactionCompleted;

        #endregion

        #region Constructor and Disposal

        public Shop(IMemoryCache cache, ILogger<Shop> logger, IShopEventBus eventBus)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            Config = new ShopConfiguration();
            InitializeDefaultShops();
            StartBackgroundTasks();
        }

        public void Dispose()
        {
            _cts.Cancel();
            _cts.Dispose();
            _syncLock.Dispose();
            CleanupBackgroundTasks();
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
                UpdateStatus(ShopStatus.Closed);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error closing shop");
                throw;
            }
        }

        public async Task OpenShopAsync(int shopNum)
        {
            ValidateShopIndex(shopNum);
            await StreamShopAsync(shopNum);
            await Gui.ShowWindowAsync(Gui.GetWindowIndex("winShop"));
            GameState.InShop = shopNum;
            UpdateStatus(ShopStatus.Open);
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
                _shopReputations.TryRemove(index, out _);
                _cache.Remove(GetCacheKey(index));
                await _eventBus.PublishAsync(new ShopClearedEvent(index));
            }
            finally
            {
                _syncLock.Release();
            }
        }

        public async Task BackupShopsAsync(string backupPath)
        {
            await _syncLock.WaitAsync();
            try
            {
                string backupFile = Path.Combine(backupPath, $"shops_backup_{DateTime.UtcNow:yyyyMMddHHmmss}.json");
                await SaveShopDataAsync(backupFile);
                _logger.LogInformation($"Shop backup created at {backupFile}");
            }
            finally
            {
                _syncLock.Release();
            }
        }

        #endregion

        #region Packet Handling

        public async Task HandleShopReputationUpdate(byte[] data)
        {
            using var buffer = new ByteStream(data);
            int shopNum = buffer.ReadInt32();
            float reputationScore = buffer.ReadSingle();
            
            var reputation = _shopReputations.GetOrAdd(shopNum, _ => new ShopReputation());
            reputation.UpdateScore(reputationScore);
            await _eventBus.PublishAsync(new ShopReputationUpdatedEvent(shopNum, reputationScore));
        }

        #endregion

        #region Outgoing Requests

        public async Task BulkBuyItemsAsync(Dictionary<int, int> shopSlotsWithQuantities)
        {
            using var throttle = new SemaphoreSlim(MaxConcurrentOperations);
            var tasks = shopSlotsWithQuantities.Select(async kvp =>
            {
                await throttle.WaitAsync(_cts.Token);
                try
                {
                    for (int i = 0; i < kvp.Value; i++)
                    {
                        await BuyItemAsync(kvp.Key);
                    }
                }
                finally
                {
                    throttle.Release();
                }
            });
            await Task.WhenAll(tasks);
        }

        #endregion

        #region Enhanced Features

        #region Inventory and Currency

        public async Task<TradePreview> PreviewTradeAsync(int shopSlot, TradeType tradeType)
        {
            ValidateSlot(shopSlot);
            var shop = Core.Type.Shop[GameState.InShop];
            var item = shop.TradeItem[shopSlot];
            
            return new TradePreview
            {
                ItemId = item.Item,
                Cost = item.CostValue,
                CurrencyType = item.CurrencyType,
                SuccessProbability = await CalculateTradeSuccessProbability(shopSlot, tradeType),
                TaxAmount = CalculateTax(item.CostValue)
            };
        }

        #endregion

        #region Shop Management

        public async Task ConfigureShopScheduleAsync(int shopNum, ShopSchedule schedule)
        {
            ValidateShopIndex(shopNum);
            await _syncLock.WaitAsync();
            try
            {
                var shop = Core.Type.Shop[shopNum];
                shop.Schedule = schedule;
                await UpdateCacheAsync(shopNum, shop);
                await ScheduleShopOperations(shopNum, schedule);
            }
            finally
            {
                _syncLock.Release();
            }
        }

        public async Task AddLimitedTimeOfferAsync(int shopNum, int itemId, decimal discount, TimeSpan duration)
        {
            ValidateShopIndex(shopNum);
            await _syncLock.WaitAsync();
            try
            {
                var shop = Core.Type.Shop[shopNum];
                var offer = new LimitedTimeOffer(itemId, discount, DateTime.UtcNow + duration);
                shop.LimitedOffers.Add(offer);
                await UpdateCacheAsync(shopNum, shop);
                
                _ = Task.Run(() => RemoveOfferAfterExpiration(shopNum, offer, duration));
            }
            finally
            {
                _syncLock.Release();
            }
        }

        #endregion

        #region Security

        public async Task<string> GenerateShopAuthTokenAsync(int shopNum)
        {
            var shop = Core.Type.Shop[shopNum];
            byte[] tokenBytes = RandomNumberGenerator.GetBytes(32);
            string token = Convert.ToBase64String(tokenBytes);
            await _cache.SetAsync(GetAuthTokenKey(shopNum), token, TimeSpan.FromHours(24));
            return token;
        }

        public async Task<bool> ValidateShopAuthTokenAsync(int shopNum, string token)
        {
            string cachedToken = await _cache.GetAsync<string>(GetAuthTokenKey(shopNum));
            return cachedToken == token;
        }

        #endregion

        #region Social Features

        public async Task ShareShopInventoryAsync(int shopNum, string platform)
        {
            var shop = Core.Type.Shop[shopNum];
            var inventorySummary = await GenerateInventorySummaryAsync(shopNum);
            await _eventBus.PublishAsync(new ShopSharedEvent(shopNum, platform, inventorySummary));
        }

        public async Task RateShopAsync(int shopNum, int playerId, float rating)
        {
            ValidateShopIndex(shopNum);
            var reputation = _shopReputations.GetOrAdd(shopNum, _ => new ShopReputation());
            reputation.AddRating(playerId, rating);
            await NetworkSend.SendShopRatingAsync(shopNum, rating);
        }

        #endregion

        #region AI-Powered Features

        public async Task<List<int>> GetPersonalizedRecommendationsAsync(int shopNum, int playerId)
        {
            var playerHistory = await GameState.Player.GetPurchaseHistoryAsync(playerId);
            var shopItems = Core.Type.Shop[shopNum].TradeItem;
            var preferences = await AnalyzePlayerPreferencesAsync(playerId);
            
            return shopItems
                .Where(ti => ti.Item >= 0 && !playerHistory.Contains(ti.Item))
                .OrderBy(ti => CalculatePreferenceScore(ti, preferences))
                .Take(5)
                .Select(ti => ti.Item)
                .ToList();
        }

        #endregion

        #endregion

        #region Helper Methods

        private void StartBackgroundTasks()
        {
            Task.Run(() => MonitorShopHealthAsync(_cts.Token));
            Task.Run(() => ProcessPendingTransactionsAsync(_cts.Token));
        }

        private async Task MonitorShopHealthAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromMinutes(5), token);
                foreach (var shopNum in Enumerable.Range(0, Constant.MAX_SHOPS))
                {
                    if (await IsShopOverstockedAsync(shopNum))
                    {
                        await TriggerOverstockClearanceAsync(shopNum);
                    }
                }
            }
        }

        private async Task UpdateStatus(ShopStatus newStatus)
        {
            if (Status != newStatus)
            {
                Status = newStatus;
                StatusChanged?.Invoke(this, new ShopStatusChangedEventArgs(newStatus));
                await _eventBus.PublishAsync(new ShopStatusChangedEvent(newStatus));
            }
        }

        #endregion

        #region Nested Classes and Enums

        public enum ShopStatus
        {
            Offline,
            Open,
            Closed,
            Maintenance
        }

        public enum TradeType
        {
            Buy,
            Sell
        }

        public class ShopConfiguration
        {
            public bool EnableDynamicPricing { get; set; } = true;
            public int MaxItemsPerCategory { get; set; } = 50;
            public TimeSpan InventoryRefreshInterval { get; set; } = TimeSpan.FromHours(1);
        }

        public class ShopReputation
        {
            private readonly ConcurrentDictionary<int, float> _playerRatings = new();
            public float AverageRating => _playerRatings.Any() ? _playerRatings.Values.Average() : 0;
            public int RatingCount => _playerRatings.Count;

            public void AddRating(int playerId, float rating) => _playerRatings[playerId] = Math.Clamp(rating, 0, 5);
            public void UpdateScore(float serverScore) => ServerScore = serverScore;
            public float ServerScore { get; private set; }
        }

        public class TradePreview
        {
            public int ItemId { get; set; }
            public decimal Cost { get; set; }
            public int CurrencyType { get; set; }
            public float SuccessProbability { get; set; }
            public decimal TaxAmount { get; set; }
        }

        public class LimitedTimeOffer
        {
            public int ItemId { get; }
            public decimal Discount { get; }
            public DateTime Expiration { get; }

            public LimitedTimeOffer(int itemId, decimal discount, DateTime expiration)
            {
                ItemId = itemId;
                Discount = discount;
                Expiration = expiration;
            }
        }

        #endregion
    }

    #region Event Classes

    public class ShopStatusChangedEventArgs : EventArgs
    {
        public ShopStatus NewStatus { get; }
        public ShopStatusChangedEventArgs(ShopStatus newStatus) => NewStatus = newStatus;
    }

    public class ShopTransactionEventArgs : EventArgs
    {
        public int ShopNumber { get; }
        public int ItemId { get; }
        public decimal Amount { get; }
        public Shop.TransactionType Type { get; }

        public ShopTransactionEventArgs(int shopNumber, int itemId, decimal amount, Shop.TransactionType type)
        {
            ShopNumber = shopNumber;
            ItemId = itemId;
            Amount = amount;
            Type = type;
        }
    }

    #endregion

    #region Interfaces

    public interface IShopEventBus
    {
        Task PublishAsync<T>(T @event);
        Task SubscribeAsync<T>(Func<T, Task> handler);
    }

    #endregion
}
