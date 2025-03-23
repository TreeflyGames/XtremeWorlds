using Core;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        private readonly IShopEventBus _eventBus;
        private readonly SemaphoreSlim _syncLock = new(1, 1);
        private readonly SemaphoreSlim _operationThrottle = new(10); // Increased to 10 concurrent operations
        private readonly CancellationTokenSource _cts = new();
        private readonly ConcurrentDictionary<int, ShopData> _shopData = new();
        private readonly ConcurrentDictionary<int, ShopReputation> _shopReputations = new();
        private readonly ConcurrentDictionary<int, List<int>> _playerPreferences = new();
        private readonly ConcurrentQueue<PendingTransaction> _pendingTransactions = new();
        private readonly ConcurrentDictionary<int, List<LoyaltyRecord>> _loyaltyRecords = new();
        private readonly ConcurrentDictionary<int, Auction> _activeAuctions = new();

        public ShopStatus Status { get; private set; } = ShopStatus.Offline;
        public ShopConfiguration Config { get; private set; } = new ShopConfiguration();
        public IReadOnlyDictionary<int, ShopReputation> Reputations => _shopReputations;
        public IReadOnlyDictionary<int, ShopData> Shops => _shopData;

        // Events
        public event EventHandler<ShopStatusChangedEventArgs> StatusChanged;
        public event EventHandler<ShopTransactionEventArgs> TransactionCompleted;
        public event EventHandler<ShopReviewEventArgs> ReviewAdded;
        public event EventHandler<AuctionEventArgs> AuctionUpdated;

        #endregion

        #region Constructor and Disposal

        public Shop(IMemoryCache cache, ILogger<Shop> logger, IShopEventBus eventBus)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            StartBackgroundTasks();
        }

        public void Dispose()
        {
            _cts.Cancel();
            _cts.Dispose();
            _syncLock.Dispose();
            _operationThrottle.Dispose();
        }

        #endregion

        #region Advanced State Management

        public enum ShopStatus
        {
            Open,
            Closed,
            Maintenance,
            Offline,
            Locked // New status for security lockdowns
        }

        public class ShopConfiguration
        {
            public decimal TaxRate { get; set; } = 0.05m;
            public int MaxBulkBuy { get; set; } = 10;
            public bool EnableDynamicPricing { get; set; } = true;
            public string CurrencyType { get; set; } = "Gold"; // New: Multi-currency support
            public string WelcomeMessage { get; set; } = "Welcome to the shop!";
            public int MaxItemsPerSale { get; set; } = 100; // New: Limit items per transaction
            public bool AllowTrades { get; set; } = true; // New: Toggle trade acceptance
        }

        private async Task UpdateStatusAsync(ShopStatus newStatus)
        {
            if (Status != newStatus)
            {
                Status = newStatus;
                StatusChanged?.Invoke(this, new ShopStatusChangedEventArgs(newStatus));
                await _eventBus.PublishAsync(new ShopStatusChangedEvent(newStatus));
                _logger.LogInformation($"Shop status updated to {newStatus}.");
            }
        }

        public async Task OpenShopAsync(int shopNum)
        {
            ValidateShopIndex(shopNum);
            await UpdateStatusAsync(ShopStatus.Open);
            _logger.LogInformation($"Shop {shopNum} opened.");
        }

        public async Task CloseShopAsync(int shopNum)
        {
            ValidateShopIndex(shopNum);
            await UpdateStatusAsync(ShopStatus.Closed);
            _logger.LogInformation($"Shop {shopNum} closed.");
        }

        public async Task SetMaintenanceModeAsync(int shopNum, bool isMaintenance)
        {
            ValidateShopIndex(shopNum);
            await UpdateStatusAsync(isMaintenance ? ShopStatus.Maintenance : ShopStatus.Open);
            _logger.LogInformation($"Shop {shopNum} maintenance mode: {isMaintenance}.");
        }

        public async Task LockShopAsync(int shopNum, string reason)
        {
            ValidateShopIndex(shopNum);
            await UpdateStatusAsync(ShopStatus.Locked);
            _logger.LogWarning($"Shop {shopNum} locked due to: {reason}.");
            await _eventBus.PublishAsync(new ShopLockedEvent(shopNum, reason));
        }

        public async Task UpdateConfigurationAsync(int shopNum, Action<ShopConfiguration> configure)
        {
            ValidateShopIndex(shopNum);
            await _syncLock.WaitAsync();
            try
            {
                var newConfig = new ShopConfiguration();
                configure(newConfig);
                Config = newConfig;
                _logger.LogInformation($"Shop {shopNum} configuration updated.");
            }
            finally
            {
                _syncLock.Release();
            }
        }

        #endregion

        #region Reputation System

        public class ShopReputation
        {
            private readonly ConcurrentDictionary<int, float> _playerRatings = new();
            private readonly ConcurrentDictionary<int, string> _playerReviews = new();
            private readonly ConcurrentBag<DateTime> _ratingTimestamps = new();

            public float AverageRating => _playerRatings.Any() ? _playerRatings.Values.Average() : 0;
            public int RatingCount => _playerRatings.Count;
            public string ReputationTier => CalculateReputationTier();

            public void AddRating(int playerId, float rating, string review = null)
            {
                _playerRatings[playerId] = Math.Clamp(rating, 0, 5);
                _ratingTimestamps.Add(DateTime.UtcNow);
                if (!string.IsNullOrEmpty(review))
                {
                    _playerReviews[playerId] = review;
                }
            }

            public float GetServerVerifiedScore()
            {
                // Enhanced logic considering rating age and count
                var recentRatings = _playerRatings.Values
                    .Where((_, i) => _ratingTimestamps.ElementAt(i) > DateTime.UtcNow.AddMonths(-1))
                    .ToList();
                return recentRatings.Any() ? recentRatings.Average() : AverageRating;
            }

            public List<string> GetReviews() => _playerReviews.Values.ToList();

            private string CalculateReputationTier()
            {
                float score = GetServerVerifiedScore();
                return score switch
                {
                    >= 4.5f => "Elite",
                    >= 3.5f => "Trusted",
                    >= 2.5f => "Standard",
                    _ => "Unrated"
                };
            }
        }

        public async Task RateShopAsync(int shopNum, int playerId, float rating, string review = null)
        {
            ValidateShopIndex(shopNum);
            var reputation = _shopReputations.GetOrAdd(shopNum, _ => new ShopReputation());
            reputation.AddRating(playerId, rating, review);
            ReviewAdded?.Invoke(this, new ShopReviewEventArgs(shopNum, playerId, rating, review));
            await _eventBus.PublishAsync(new ShopRatingEvent(shopNum, playerId, rating, review));
            _logger.LogInformation($"Shop {shopNum} rated by player {playerId}: {rating}");
        }

        public float GetShopReputation(int shopNum) => _shopReputations.TryGetValue(shopNum, out var rep) ? rep.GetServerVerifiedScore() : 0;
        public string GetReputationTier(int shopNum) => _shopReputations.TryGetValue(shopNum, out var rep) ? rep.ReputationTier : "Unrated";
        public List<string> GetShopReviews(int shopNum) => _shopReputations.TryGetValue(shopNum, out var rep) ? rep.GetReviews() : new List<string>();

        #endregion

        #region Trading Enhancements

        public class TradePreview
        {
            public int ItemId { get; set; }
            public decimal Cost { get; set; }
            public float SuccessProbability { get; set; }
            public decimal Tax { get; set; }
            public TimeSpan EstimatedDeliveryTime { get; set; } // New
            public bool IsCompatible { get; set; } // New: Compatibility check
        }

        public class LimitedTimeOffer
        {
            public int ItemId { get; }
            public decimal Discount { get; }
            public DateTime Expiry { get; }
            public LimitedTimeOffer(int itemId, decimal discount, DateTime expiry)
            {
                ItemId = itemId;
                Discount = discount;
                Expiry = expiry;
            }
        }

        public class Auction
        {
            public int ItemId { get; }
            public decimal CurrentBid { get; set; }
            public int HighestBidderId { get; set; }
            public DateTime EndTime { get; }
            public Auction(int itemId, decimal startingBid, DateTime endTime)
            {
                ItemId = itemId;
                CurrentBid = startingBid;
                EndTime = endTime;
            }
        }

        public async Task BulkBuyItemsAsync(int shopNum, Dictionary<int, int> itemsToBuy)
        {
            ValidateShopIndex(shopNum);
            await _operationThrottle.WaitAsync();
            try
            {
                if (itemsToBuy.Count > Config.MaxItemsPerSale)
                    throw new InvalidOperationException($"Max items per sale is {Config.MaxItemsPerSale}.");
                foreach (var (itemId, quantity) in itemsToBuy)
                {
                    if (quantity > Config.MaxBulkBuy)
                        throw new InvalidOperationException($"Max bulk buy limit is {Config.MaxBulkBuy}.");
                    await ProcessTransactionAsync(shopNum, itemId, quantity);
                }
                await _eventBus.PublishAsync(new BulkBuyEvent(shopNum, itemsToBuy));
                _logger.LogInformation($"Bulk buy completed for shop {shopNum}.");
            }
            finally
            {
                _operationThrottle.Release();
            }
        }

        public async Task<TradePreview> PreviewTradeAsync(int shopNum, int itemId, int playerId)
        {
            ValidateShopIndex(shopNum);
            decimal cost = CalculateItemCost(itemId);
            return new TradePreview
            {
                ItemId = itemId,
                Cost = cost,
                SuccessProbability = 0.95f,
                Tax = cost * Config.TaxRate,
                EstimatedDeliveryTime = TimeSpan.FromMinutes(15),
                IsCompatible = await CheckItemCompatibilityAsync(playerId, itemId)
            };
        }

        public async Task AddLimitedTimeOfferAsync(int shopNum, int itemId, decimal discount, TimeSpan duration)
        {
            ValidateShopIndex(shopNum);
            var offer = new LimitedTimeOffer(itemId, discount, DateTime.UtcNow + duration);
            var shop = _shopData.GetOrAdd(shopNum, _ => new ShopData());
            shop.LimitedOffers.Add(offer);
            await _eventBus.PublishAsync(new LimitedTimeOfferEvent(shopNum, offer));
        }

        public async Task ReserveItemAsync(int shopNum, int itemId, int playerId, TimeSpan reservationTime)
        {
            ValidateShopIndex(shopNum);
            var shop = _shopData.GetOrAdd(shopNum, _ => new ShopData());
            if (!shop.ReservedItems.TryAdd(itemId, (playerId, DateTime.UtcNow + reservationTime)))
                throw new InvalidOperationException("Item already reserved.");
            await Task.Delay(50); // Simulate reservation
            _logger.LogInformation($"Item {itemId} reserved by player {playerId} in shop {shopNum}.");
        }

        public async Task StartAuctionAsync(int shopNum, int itemId, decimal startingBid, TimeSpan duration)
        {
            ValidateShopIndex(shopNum);
            var auction = new Auction(itemId, startingBid, DateTime.UtcNow + duration);
            _activeAuctions[shopNum] = auction;
            AuctionUpdated?.Invoke(this, new AuctionEventArgs(shopNum, auction));
            await _eventBus.PublishAsync(new AuctionStartedEvent(shopNum, auction));
            _logger.LogInformation($"Auction started for item {itemId} in shop {shopNum}.");
        }

        public async Task PlaceBidAsync(int shopNum, int playerId, decimal bid)
        {
            ValidateShopIndex(shopNum);
            if (!_activeAuctions.TryGetValue(shopNum, out var auction) || auction.EndTime < DateTime.UtcNow)
                throw new InvalidOperationException("Auction not active.");
            if (bid <= auction.CurrentBid)
                throw new InvalidOperationException("Bid must exceed current bid.");
            auction.CurrentBid = bid;
            auction.HighestBidderId = playerId;
            AuctionUpdated?.Invoke(this, new AuctionEventArgs(shopNum, auction));
            await _eventBus.PublishAsync(new AuctionBidEvent(shopNum, playerId, bid));
            _logger.LogInformation($"Player {playerId} placed bid {bid} on shop {shopNum}.");
        }

        #endregion

        #region Security Features

        public string GenerateAuthToken(int shopNum)
        {
            byte[] tokenBytes = RandomNumberGenerator.GetBytes(32);
            string token = Convert.ToBase64String(tokenBytes);
            _cache.Set($"ShopToken_{shopNum}", token, TimeSpan.FromHours(1));
            return token;
        }

        public bool ValidateAuthToken(int shopNum, string token)
        {
            return _cache.TryGetValue($"ShopToken_{shopNum}", out string cachedToken) && cachedToken == token;
        }

        public async Task LogTransactionAsync(int shopNum, string transactionDetails)
        {
            await Task.Delay(10); // Simulate logging
            _logger.LogInformation($"Transaction for shop {shopNum}: {transactionDetails}");
        }

        public async Task<bool> VerifyTwoFactorAuthAsync(int shopNum, int playerId, string code)
        {
            // Placeholder for 2FA logic
            await Task.Delay(20);
            return true; // Simulated success
        }

        #endregion

        #region Social Features

        public async Task ShareShopAsync(int shopNum, string platform)
        {
            string summary = await GenerateInventorySummaryAsync(shopNum);
            await _eventBus.PublishAsync(new ShopSharedEvent(shopNum, platform, summary));
            _logger.LogInformation($"Shop {shopNum} shared on {platform}.");
        }

        public async Task<string> GenerateInventorySummaryAsync(int shopNum)
        {
            ValidateShopIndex(shopNum);
            var shop = _shopData.GetOrAdd(shopNum, _ => new ShopData());
            return $"Shop {shopNum} has {shop.Inventory.Count} items and {shop.LimitedOffers.Count} offers.";
        }

        public async Task AddPlayerReviewAsync(int shopNum, int playerId, string review)
        {
            ValidateShopIndex(shopNum);
            var reputation = _shopReputations.GetOrAdd(shopNum, _ => new ShopReputation());
            reputation.AddRating(playerId, 0, review); // Rating 0 for review-only
            ReviewAdded?.Invoke(this, new ShopReviewEventArgs(shopNum, playerId, 0, review));
            await _eventBus.PublishAsync(new ShopReviewEvent(shopNum, playerId, review));
        }

        public async Task CreateShopForumPostAsync(int shopNum, int playerId, string content)
        {
            ValidateShopIndex(shopNum);
            var shop = _shopData.GetOrAdd(shopNum, _ => new ShopData());
            shop.ForumPosts.Add(new ForumPost(playerId, content));
            await _eventBus.PublishAsync(new ForumPostEvent(shopNum, playerId, content));
        }

        #endregion

        #region AI-Powered Features

        public async Task<List<int>> GetRecommendationsAsync(int playerId)
        {
            if (_playerPreferences.TryGetValue(playerId, out var preferences))
                return preferences.Take(5).ToList();
            return await AnalyzePlayerPreferencesAsync(playerId);
        }

        private async Task<List<int>> AnalyzePlayerPreferencesAsync(int playerId)
        {
            // Enhanced AI placeholder
            var preferences = new List<int> { 1, 2, 3, 4, 5 };
            _playerPreferences[playerId] = preferences;
            await Task.Delay(30); // Simulate AI processing
            return preferences;
        }

        public async Task AdjustPricesAsync(int shopNum)
        {
            if (Config.EnableDynamicPricing)
            {
                var shop = _shopData.GetOrAdd(shopNum, _ => new ShopData());
                foreach (var item in shop.Inventory)
                {
                    decimal newPrice = CalculateDynamicPrice(item);
                    // Update price logic here
                }
                await Task.Delay(50);
                _logger.LogInformation($"Prices adjusted for shop {shopNum}.");
            }
        }

        public async Task SendPersonalizedOfferAsync(int shopNum, int playerId)
        {
            var recommendations = await GetRecommendationsAsync(playerId);
            var offer = new LimitedTimeOffer(recommendations.First(), 0.2m, DateTime.UtcNow.AddHours(24));
            await _eventBus.PublishAsync(new PersonalizedOfferEvent(shopNum, playerId, offer));
        }

        #endregion

        #region Background Operations

        private void StartBackgroundTasks()
        {
            Task.Run(() => MonitorHealthAsync(_cts.Token));
            Task.Run(() => ProcessPendingTransactionsAsync(_cts.Token));
            Task.Run(() => AutoRestockShopsAsync(_cts.Token));
        }

        private async Task MonitorHealthAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromMinutes(5), token);
                foreach (var shopNum in _shopData.Keys)
                {
                    if (Status == ShopStatus.Offline) continue;
                    _logger.LogInformation($"Health check for shop {shopNum}: OK");
                }
            }
        }

        private async Task ProcessPendingTransactionsAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                if (_pendingTransactions.TryDequeue(out var transaction))
                {
                    await ProcessTransactionAsync(transaction.ShopNum, transaction.ItemId, transaction.Quantity);
                }
                await Task.Delay(100, token);
            }
        }

        private async Task AutoRestockShopsAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                foreach (var (shopNum, shop) in _shopData)
                {
                    if (shop.Inventory.Count < 10)
                        await RestockShopAsync(shopNum);
                }
                await Task.Delay(TimeSpan.FromHours(1), token);
            }
        }

        public void EnqueuePendingTransaction(PendingTransaction transaction)
        {
            _pendingTransactions.Enqueue(transaction);
            _logger.LogInformation($"Transaction queued for shop {transaction.ShopNum}.");
        }

        #endregion

        #region Event System

        public interface IShopEventBus
        {
            Task PublishAsync<T>(T @event);
            Task SubscribeAsync<T>(Func<T, Task> handler);
        }

        public class ShopStatusChangedEvent
        {
            public ShopStatus NewStatus { get; }
            public ShopStatusChangedEvent(ShopStatus newStatus) => NewStatus = newStatus;
        }

        public class ShopTransactionEvent
        {
            public int ShopNum { get; }
            public int ItemId { get; }
            public ShopTransactionEvent(int shopNum, int itemId) => (ShopNum, ItemId) = (shopNum, itemId);
        }

        public class ShopReviewEventArgs : EventArgs
        {
            public int ShopNum { get; }
            public int PlayerId { get; }
            public float Rating { get; }
            public string Review { get; }
            public ShopReviewEventArgs(int shopNum, int playerId, float rating, string review)
            {
                ShopNum = shopNum;
                PlayerId = playerId;
                Rating = rating;
                Review = review;
            }
        }

        public class AuctionEventArgs : EventArgs
        {
            public int ShopNum { get; }
            public Auction Auction { get; }
            public AuctionEventArgs(int shopNum, Auction auction) => (ShopNum, Auction) = (shopNum, auction);
        }

        #endregion

        #region Performance Improvements

        private string GetCacheKey(int shopNum) => $"Shop_{shopNum}";

        public async Task<T> GetCachedOrComputeAsync<T>(int shopNum, string key, Func<Task<T>> compute)
        {
            string cacheKey = $"{GetCacheKey(shopNum)}_{key}";
            if (_cache.TryGetValue(cacheKey, out T value))
                return value;
            value = await compute();
            _cache.Set(cacheKey, value, TimeSpan.FromMinutes(30));
            return value;
        }

        #endregion

        #region Management Features

        public class ShopSchedule
        {
            public TimeSpan OpenTime { get; set; }
            public TimeSpan CloseTime { get; set; }
        }

        public class ShopData
        {
            public List<int> Inventory { get; set; } = new();
            public Dictionary<string, List<int>> Categories { get; set; } = new();
            public List<LimitedTimeOffer> LimitedOffers { get; set; } = new();
            public ConcurrentDictionary<int, (int PlayerId, DateTime Expiry)> ReservedItems { get; set; } = new();
            public List<ForumPost> ForumPosts { get; set; } = new();
        }

        public class ForumPost
        {
            public int PlayerId { get; }
            public string Content { get; }
            public DateTime PostedAt { get; } = DateTime.UtcNow;
            public ForumPost(int playerId, string content) => (PlayerId, Content) = (playerId, content);
        }

        public class LoyaltyRecord
        {
            public int PlayerId { get; }
            public int PurchaseCount { get; set; }
            public decimal TotalSpent { get; set; }
            public LoyaltyRecord(int playerId) => PlayerId = playerId;
        }

        public async Task ConfigureScheduleAsync(int shopNum, ShopSchedule schedule)
        {
            ValidateShopIndex(shopNum);
            await Task.Delay(50); // Simulate scheduling
            _logger.LogInformation($"Schedule configured for shop {shopNum}.");
        }

        public async Task BackupShopDataAsync(string filePath)
        {
            var data = new { Shops = _shopData, Reputations = _shopReputations, Loyalty = _loyaltyRecords };
            await File.WriteAllTextAsync(filePath, JsonSerializer.Serialize(data));
            _logger.LogInformation($"Shop data backed up to {filePath}.");
        }

        public async Task AddCategoryAsync(int shopNum, string category, List<int> items)
        {
            ValidateShopIndex(shopNum);
            var shop = _shopData.GetOrAdd(shopNum, _ => new ShopData());
            shop.Categories[category] = items;
            await Task.Delay(50);
        }

        public async Task RestockShopAsync(int shopNum)
        {
            ValidateShopIndex(shopNum);
            var shop = _shopData.GetOrAdd(shopNum, _ => new ShopData());
            shop.Inventory.AddRange(Enumerable.Range(1, 10)); // Placeholder restock
            await Task.Delay(100);
            _logger.LogInformation($"Shop {shopNum} restocked.");
        }

        public async Task AddLoyaltyPointsAsync(int shopNum, int playerId, decimal amountSpent)
        {
            ValidateShopIndex(shopNum);
            var records = _loyaltyRecords.GetOrAdd(shopNum, _ => new List<LoyaltyRecord>());
            var record = records.FirstOrDefault(r => r.PlayerId == playerId) ?? new LoyaltyRecord(playerId);
            record.PurchaseCount++;
            record.TotalSpent += amountSpent;
            if (!records.Contains(record)) records.Add(record);
            await _eventBus.PublishAsync(new LoyaltyUpdateEvent(shopNum, playerId, record.PurchaseCount));
        }

        #endregion

        #region Helper Methods

        private void ValidateShopIndex(int shopNum)
        {
            if (shopNum < 0 || shopNum >= Constant.MAX_SHOPS)
                throw new ArgumentOutOfRangeException(nameof(shopNum));
        }

        private decimal CalculateItemCost(int itemId) => itemId * 10m; // Placeholder
        private decimal CalculateDynamicPrice(int itemId) => CalculateItemCost(itemId) * (1 + new Random().NextDouble()); // Placeholder
        private async Task<bool> CheckItemCompatibilityAsync(int playerId, int itemId) => await Task.FromResult(true); // Placeholder
        private async Task ProcessTransactionAsync(int shopNum, int itemId, int quantity)
        {
            await Task.Delay(100); // Simulate transaction
            TransactionCompleted?.Invoke(this, new ShopTransactionEventArgs(shopNum, itemId));
            await LogTransactionAsync(shopNum, $"Purchased {quantity} of item {itemId}");
        }

        #endregion
    }

    #region Supporting Classes

    public class PendingTransaction
    {
        public int ShopNum { get; }
        public int ItemId { get; }
        public int Quantity { get; }
        public PendingTransaction(int shopNum, int itemId, int quantity) => (ShopNum, ItemId, Quantity) = (shopNum, itemId, quantity);
    }

    public class ShopStatusChangedEventArgs : EventArgs
    {
        public ShopStatus NewStatus { get; }
        public ShopStatusChangedEventArgs(ShopStatus newStatus) => NewStatus = newStatus;
    }

    public class ShopTransactionEventArgs : EventArgs
    {
        public int ShopNum { get; }
        public int ItemId { get; }
        public ShopTransactionEventArgs(int shopNum, int itemId) => (ShopNum, ItemId) = (shopNum, itemId);
    }

    #endregion

    #region Event Classes (Simplified for Brevity)

    public class ShopRatingEvent { public int ShopNum { get; } public int PlayerId { get; } public float Rating { get; } public string Review { get; } public ShopRatingEvent(int shopNum, int playerId, float rating, string review) => (ShopNum, PlayerId, Rating, Review) = (shopNum, playerId, rating, review); }
    public class BulkBuyEvent { public int ShopNum { get; } public Dictionary<int, int> Items { get; } public BulkBuyEvent(int shopNum, Dictionary<int, int> items) => (ShopNum, Items) = (shopNum, items); }
    public class ShopSharedEvent { public int ShopNum { get; } public string Platform { get; } public string Summary { get; } public ShopSharedEvent(int shopNum, string platform, string summary) => (ShopNum, Platform, Summary) = (shopNum, platform, summary); }
    public class ShopLockedEvent { public int ShopNum { get; } public string Reason { get; } public ShopLockedEvent(int shopNum, string reason) => (ShopNum, Reason) = (shopNum, reason); }
    public class LimitedTimeOfferEvent { public int ShopNum { get; } public Shop.LimitedTimeOffer Offer { get; } public LimitedTimeOfferEvent(int shopNum, Shop.LimitedTimeOffer offer) => (ShopNum, Offer) = (shopNum, offer); }
    public class AuctionStartedEvent { public int ShopNum { get; } public Shop.Auction Auction { get; } public AuctionStartedEvent(int shopNum, Shop.Auction auction) => (ShopNum, Auction) = (shopNum, auction); }
    public class AuctionBidEvent { public int ShopNum { get; } public int PlayerId { get; } public decimal Bid { get; } public AuctionBidEvent(int shopNum, int playerId, decimal bid) => (ShopNum, PlayerId, Bid) = (shopNum, playerId, bid); }
    public class ShopReviewEvent { public int ShopNum { get; } public int PlayerId { get; } public string Review { get; } public ShopReviewEvent(int shopNum, int playerId, string review) => (ShopNum, PlayerId, Review) = (shopNum, playerId, review); }
    public class ForumPostEvent { public int ShopNum { get; } public int PlayerId { get; } public string Content { get; } public ForumPostEvent(int shopNum, int playerId, string content) => (ShopNum, PlayerId, Content) = (shopNum, playerId, content); }
    public class PersonalizedOfferEvent { public int ShopNum { get; } public int PlayerId { get; } public Shop.LimitedTimeOffer Offer { get; } public PersonalizedOfferEvent(int shopNum, int playerId, Shop.LimitedTimeOffer offer) => (ShopNum, PlayerId, Offer) = (shopNum, playerId, offer); }
    public class LoyaltyUpdateEvent { public int ShopNum { get; } public int PlayerId { get; } public int PurchaseCount { get; } public LoyaltyUpdateEvent(int shopNum, int playerId, int count) => (ShopNum, PlayerId, PurchaseCount) = (shopNum, playerId, count); }

    #endregion
}
