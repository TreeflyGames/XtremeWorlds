#region Usings
using Core; // Assuming this namespace contains base types like IEntity
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options; // For configuration pattern
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO; // For backup/restore
using System.Linq;
using System.Security.Cryptography;
using System.Text; // For hashing
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
#endregion

namespace Client.EnhancedShop
{
    #region Interfaces (for DI and Testability)

    public interface IShopEventBus
    {
        Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default);
        // Assuming Subscribe logic is handled elsewhere or via DI framework
    }

    public interface IPaymentGatewayService
    {
        Task<PaymentResult> ProcessPaymentAsync(int playerId, decimal amount, string currency, string paymentMethodToken, CancellationToken cancellationToken = default);
        Task<RefundResult> ProcessRefundAsync(string transactionId, decimal amount, string reason, CancellationToken cancellationToken = default);
    }

    public interface IInventoryNotifier
    {
        Task NotifyLowStockAsync(int shopId, int itemId, string itemName, int currentQuantity, CancellationToken cancellationToken = default);
        Task NotifyItemRestockedAsync(int shopId, int itemId, string itemName, int newQuantity, CancellationToken cancellationToken = default);
    }

    public interface IRecommendationEngine
    {
        Task<List<int>> GetRecommendedItemIdsAsync(int playerId, int count, CancellationToken cancellationToken = default);
        Task TrainModelAsync(IEnumerable<Order> recentOrders, CancellationToken cancellationToken = default); // AI Training hook
    }

    public interface IShopPersistenceService // Interface for database operations
    {
        Task<ShopData> LoadShopDataAsync(int shopId, CancellationToken cancellationToken = default);
        Task SaveShopDataAsync(int shopId, ShopData data, CancellationToken cancellationToken = default);
        Task SaveOrderAsync(Order order, CancellationToken cancellationToken = default);
        Task<IEnumerable<Order>> GetPlayerOrderHistoryAsync(int playerId, int limit, int offset, CancellationToken cancellationToken = default);
        Task LogAuditEventAsync(AuditLogEntry entry, CancellationToken cancellationToken = default);
        // ... other persistence methods ...
    }

    #endregion

    public class Shop : IDisposable
    {
        #region Fields and Properties

        // --- Dependencies ---
        private readonly IMemoryCache _cache;
        private readonly ILogger<Shop> _logger;
        private readonly IShopEventBus _eventBus;
        private readonly IPaymentGatewayService _paymentGateway; // NEW: Payment processing
        private readonly IInventoryNotifier _inventoryNotifier; // NEW: Stock notifications
        private readonly IRecommendationEngine _recommendationEngine; // NEW: AI recommendations
        private readonly IShopPersistenceService _persistenceService; // NEW: Data persistence
        private ShopConfiguration _config; // Now populated via IOptions

        // --- Concurrency & State ---
        private readonly SemaphoreSlim _globalConfigLock = new(1, 1); // Lock for global config changes
        private readonly ConcurrentDictionary<int, SemaphoreSlim> _shopLocks = new(); // Per-shop locks for critical ops
        private readonly SemaphoreSlim _operationThrottle; // Throttle concurrent operations
        private readonly CancellationTokenSource _cts = new();
        private readonly ConcurrentDictionary<int, ShopState> _shopStates = new(); // Holds all data per shop
        private readonly ConcurrentDictionary<string, Coupon> _activeCoupons = new(); // NEW: Global coupons
        private readonly ConcurrentDictionary<int, UserSession> _userSessions = new(); // NEW: Basic session tracking

        // --- Background Task Timers ---
        private Timer _healthMonitorTimer;
        private Timer _pendingTransactionTimer;
        private Timer _autoRestockTimer;
        private Timer _auctionProcessingTimer; // NEW: For ending auctions
        private Timer _recommendationModelUpdateTimer; // NEW: For AI training

        // --- Public Accessors ---
        public IReadOnlyDictionary<int, ShopState> ShopStates => _shopStates; // Provides access to individual shop data
        public ShopGlobalStatus GlobalStatus { get; private set; } = ShopGlobalStatus.Initializing; // NEW: Overall system status

        // --- Events ---
        public event EventHandler<ShopStatusChangedEventArgs> ShopStatusChanged;
        public event EventHandler<OrderEventArgs> OrderPlaced; // Changed from TransactionCompleted
        public event EventHandler<OrderEventArgs> OrderStatusChanged; // NEW
        public event EventHandler<ShopReviewEventArgs> ReviewAdded;
        public event EventHandler<ShopReviewEventArgs> ReviewModerated; // NEW
        public event EventHandler<AuctionEventArgs> AuctionUpdated;
        public event EventHandler<AuctionEventArgs> AuctionEnded; // NEW
        public event EventHandler<LoyaltyEventArgs> LoyaltyPointsUpdated; // NEW
        public event EventHandler<WishlistEventArgs> WishlistUpdated; // NEW
        public event EventHandler<GiftEventArgs> ItemGifted; // NEW

        #endregion

        #region Constructor and Initialization

        public Shop(
            IMemoryCache cache,
            ILogger<Shop> logger,
            IShopEventBus eventBus,
            IOptionsMonitor<ShopConfiguration> configMonitor, // Use IOptionsMonitor for reloadable config
            IPaymentGatewayService paymentGateway,
            IInventoryNotifier inventoryNotifier,
            IRecommendationEngine recommendationEngine,
            IShopPersistenceService persistenceService)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _paymentGateway = paymentGateway ?? throw new ArgumentNullException(nameof(paymentGateway));
            _inventoryNotifier = inventoryNotifier ?? throw new ArgumentNullException(nameof(inventoryNotifier));
            _recommendationEngine = recommendationEngine ?? throw new ArgumentNullException(nameof(recommendationEngine));
            _persistenceService = persistenceService ?? throw new ArgumentNullException(nameof(persistenceService));

            // Subscribe to configuration changes
            configMonitor.OnChange(UpdateConfiguration);
            _config = configMonitor.CurrentValue ?? new ShopConfiguration(); // Initial config load

            _operationThrottle = new SemaphoreSlim(_config.MaxConcurrentOperations, _config.MaxConcurrentOperations);

            _logger.LogInformation("Shop system initializing...");
            // Initialization logic (e.g., load shops from persistence)
            _ = InitializeShopsAsync(_cts.Token);
        }

        private async Task InitializeShopsAsync(CancellationToken cancellationToken)
        {
            try
            {
                GlobalStatus = ShopGlobalStatus.LoadingData;
                // Simulate loading shop IDs from a master list or discovery service
                var shopIdsToLoad = Enumerable.Range(0, Constant.MAX_SHOPS); // Example

                foreach (var shopId in shopIdsToLoad)
                {
                    await LoadShopAsync(shopId, cancellationToken);
                    _shopLocks.TryAdd(shopId, new SemaphoreSlim(1, 1)); // Initialize lock per shop
                }

                StartBackgroundTasks();
                GlobalStatus = ShopGlobalStatus.Online;
                await _eventBus.PublishAsync(new ShopSystemStatusEvent(GlobalStatus), cancellationToken);
                _logger.LogInformation("Shop system initialized and online.");
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Failed to initialize Shop system.");
                GlobalStatus = ShopGlobalStatus.Error;
                // Consider shutting down or entering a degraded state
            }
        }

        // Handles configuration updates from IOptionsMonitor
        private void UpdateConfiguration(ShopConfiguration newConfig, string _)
        {
            _logger.LogInformation("Shop configuration reloading...");
            _globalConfigLock.Wait();
            try
            {
                _config = newConfig ?? new ShopConfiguration();
                // Adjust throttle if needed (handle potential semaphore disposal/recreation carefully)
                // For simplicity here, we assume MaxConcurrentOperations doesn't change often
                // or requires a restart. A more complex implementation could resize the semaphore.
                _logger.LogInformation("Shop configuration reloaded successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to reload shop configuration.");
            }
            finally
            {
                _globalConfigLock.Release();
            }
        }

        public void Dispose()
        {
            _logger.LogInformation("Shop system shutting down...");
            GlobalStatus = ShopGlobalStatus.ShuttingDown;

            // Signal cancellation
            _cts.Cancel();

            // Dispose timers safely
            _healthMonitorTimer?.Dispose();
            _pendingTransactionTimer?.Dispose();
            _autoRestockTimer?.Dispose();
            _auctionProcessingTimer?.Dispose();
            _recommendationModelUpdateTimer?.Dispose();

            // Dispose semaphores
            _globalConfigLock.Dispose();
            _operationThrottle.Dispose();
            foreach (var kvp in _shopLocks) kvp.Value.Dispose();
            _cts.Dispose();

            _logger.LogInformation("Shop system shut down complete.");
        }

        #endregion

        #region Enhanced State Management

        // NEW: Overall status of the entire shop system
        public enum ShopGlobalStatus
        {
            Initializing,
            LoadingData,
            Online,
            Degraded, // Some features might be unavailable
            Maintenance,
            ShuttingDown,
            Error
        }

        // Renamed from ShopStatus for clarity
        public enum ShopInstanceStatus
        {
            Open,
            Closed, // Manually closed by owner/admin
            ScheduledClosure, // Closed based on schedule
            Maintenance,
            Initializing, // NEW: While loading data
            Locked, // Security lockdown
            Archived // NEW: No longer active but data retained
        }

        // Holds all data and state for a single shop instance
        public class ShopState
        {
            public int ShopId { get; }
            public ShopInstanceStatus Status { get; set; } = ShopInstanceStatus.Initializing;
            public ShopConfiguration Configuration { get; set; } // Per-shop config overrides global
            public ShopReputation Reputation { get; set; } = new();
            public ConcurrentDictionary<int, ShopItem> Inventory { get; } = new(); // Enhanced Inventory
            public ConcurrentDictionary<string, List<int>> Categories { get; } = new(); // Item IDs per category
            public ConcurrentDictionary<string, HashSet<int>> Tags { get; } = new(); // Item IDs per tag
            public List<LimitedTimeOffer> LimitedOffers { get; } = new();
            public ConcurrentDictionary<int, (int PlayerId, DateTime Expiry)> ReservedItems { get; } = new();
            public List<ForumPost> ForumPosts { get; } = new();
            public ConcurrentDictionary<int, Auction> ActiveAuctions { get; } = new();
            public List<ShopSchedule> Schedules { get; } = new(); // NEW: Multiple schedules support
            public List<LoyaltyRecord> LoyaltyRecords { get; } = new();
            public List<string> BlockedPlayerIds { get; } = new(); // NEW: Per-shop blocking
            public DateTime LastRestockTime { get; set; } = DateTime.MinValue; // NEW: Track restocks
            public DateTime LastPriceUpdateTime { get; set; } = DateTime.MinValue; // NEW: Track price updates
            public string OwnerPlayerId { get; set; } // NEW: Identify the shop owner

            public ShopState(int shopId, ShopConfiguration initialConfig)
            {
                ShopId = shopId;
                Configuration = initialConfig; // Can be overridden later
            }
        }

        // Central configuration, potentially overridable per shop
        public class ShopConfiguration
        {
            public decimal DefaultTaxRate { get; set; } = 0.05m;
            public int DefaultMaxItemsPerOrder { get; set; } = 50;
            public bool EnableDynamicPricingGlobally { get; set; } = true;
            public string DefaultCurrency { get; set; } = "Gold";
            public bool AllowPlayerTradesGlobally { get; set; } = false; // Default to off for complexity
            public int MaxConcurrentOperations { get; set; } = 50; // Increased capacity
            public TimeSpan DefaultReservationTime { get; set; } = TimeSpan.FromMinutes(15);
            public TimeSpan CacheDurationShort { get; set; } = TimeSpan.FromMinutes(5);
            public TimeSpan CacheDurationMedium { get; set; } = TimeSpan.FromMinutes(30);
            public TimeSpan CacheDurationLong { get; set; } = TimeSpan.FromHours(4);
            public int MaxReviewsPerPlayerPerShop { get; set; } = 1; // NEW: Limit review spam
            public float ReputationWeightRecent { get; set; } = 0.7f; // NEW: Weight for recent ratings
            public TimeSpan AuctionEndTimeExtension { get; set; } = TimeSpan.FromMinutes(1); // NEW: Anti-sniping
            public int PointsPerCurrencyUnitSpent { get; set; } = 1; // NEW: Loyalty points calculation
            public List<LoyaltyTier> LoyaltyTiers { get; set; } = GetDefaultLoyaltyTiers(); // NEW: Configurable tiers
            public int MaxWishlistItems { get; set; } = 20; // NEW
            public bool Require2FAForHighValueTrades { get; set; } = true; // NEW
            public string AuditLogFilePath { get; set; } = "shop_audit.log"; // NEW
            public int MaxInventorySize { get; set; } = 1000; // NEW
            public TimeSpan AutoRestockCheckInterval { get; set; } = TimeSpan.FromHours(1);
            public TimeSpan HealthCheckInterval { get; set; } = TimeSpan.FromMinutes(5);
            public TimeSpan PendingTransactionInterval { get; set; } = TimeSpan.FromMilliseconds(100);
            public TimeSpan AuctionProcessingInterval { get; set; } = TimeSpan.FromSeconds(10);
            public TimeSpan RecommendationModelUpdateInterval { get; set; } = TimeSpan.FromHours(6);

            private static List<LoyaltyTier> GetDefaultLoyaltyTiers() => new()
            {
                new LoyaltyTier("Bronze", 0, 0, 0),
                new LoyaltyTier("Silver", 1000, 0.02m, 5), // Points needed, discount, bonus points %
                new LoyaltyTier("Gold", 5000, 0.05m, 10),
                new LoyaltyTier("Platinum", 20000, 0.10m, 15)
            };
        }

        // Represents a user's session, could hold roles, permissions etc.
        public class UserSession
        {
            public int PlayerId { get; }
            public HashSet<string> Roles { get; set; } = new(); // e.g., "Admin", "ShopOwner_1", "Moderator"
            public DateTime LastActivity { get; set; }
            public string SessionToken { get; } // Secure token

            public UserSession(int playerId)
            {
                PlayerId = playerId;
                LastActivity = DateTime.UtcNow;
                SessionToken = GenerateSecureToken();
            }
            // Add methods for checking permissions: HasRole(string role), HasPermission(string permission)
        }

        private async Task UpdateShopStatusAsync(int shopId, ShopInstanceStatus newStatus, string reason = null, CancellationToken cancellationToken = default)
        {
            if (_shopStates.TryGetValue(shopId, out var state))
            {
                var oldStatus = state.Status;
                if (oldStatus != newStatus)
                {
                    state.Status = newStatus;
                    // Raise local event
                    ShopStatusChanged?.Invoke(this, new ShopStatusChangedEventArgs(shopId, newStatus, oldStatus, reason));
                    // Publish to event bus
                    await _eventBus.PublishAsync(new ShopStatusChangedEvent(shopId, newStatus, oldStatus, reason), cancellationToken);
                    // Log
                    await LogAuditEventAsync(shopId, -1, "System", AuditAction.StatusChange, $"Status changed from {oldStatus} to {newStatus}. Reason: {reason ?? "N/A"}", cancellationToken);
                    _logger.LogInformation("Shop {ShopId} status updated from {OldStatus} to {NewStatus}. Reason: {Reason}", shopId, oldStatus, newStatus, reason ?? "N/A");
                }
            }
            else
            {
                _logger.LogWarning("Attempted to update status for non-existent shop {ShopId}", shopId);
            }
        }

        // Simplified methods - use UpdateShopStatusAsync internally
        public Task OpenShopAsync(int shopId, CancellationToken cancellationToken = default) => UpdateShopStatusAsync(shopId, ShopInstanceStatus.Open, cancellationToken: cancellationToken);
        public Task CloseShopAsync(int shopId, string reason = "Manual Closure", CancellationToken cancellationToken = default) => UpdateShopStatusAsync(shopId, ShopInstanceStatus.Closed, reason, cancellationToken);
        public Task SetMaintenanceModeAsync(int shopId, bool isMaintenance, string reason = null, CancellationToken cancellationToken = default) => UpdateShopStatusAsync(shopId, isMaintenance ? ShopInstanceStatus.Maintenance : ShopInstanceStatus.Open, reason ?? (isMaintenance ? "Entering Maintenance" : "Exiting Maintenance"), cancellationToken);
        public Task LockShopAsync(int shopId, string reason, CancellationToken cancellationToken = default) => UpdateShopStatusAsync(shopId, ShopInstanceStatus.Locked, reason, cancellationToken);
        public Task ArchiveShopAsync(int shopId, string reason = "Archived by Admin", CancellationToken cancellationToken = default) => UpdateShopStatusAsync(shopId, ShopInstanceStatus.Archived, reason, cancellationToken);


        // Update per-shop configuration overrides
        public async Task<ShopOperationResult> UpdateShopConfigurationAsync(int shopId, Action<ShopConfiguration> configure, int performingPlayerId, CancellationToken cancellationToken = default)
        {
            if (!_shopStates.TryGetValue(shopId, out var state))
                return ShopOperationResult.Fail($"Shop {shopId} not found.");
            if (!await CheckPermissionAsync(performingPlayerId, $"ShopConfig_Update_{shopId}", cancellationToken)) // Example permission check
                return ShopOperationResult.Fail("Permission denied.");

            await GetShopLock(shopId).WaitAsync(cancellationToken);
            try
            {
                // Create a copy based on the global config or existing shop config to modify safely
                var shopConfig = state.Configuration ?? new ShopConfiguration(); // Start with current or create if null
                // Clone it to avoid modifying the shared instance if it came from global
                var newShopConfig = JsonSerializer.Deserialize<ShopConfiguration>(JsonSerializer.Serialize(shopConfig));

                configure(newShopConfig);
                state.Configuration = newShopConfig; // Assign the modified config

                await _persistenceService.SaveShopDataAsync(shopId, ConvertToShopData(state), cancellationToken); // Persist changes
                await LogAuditEventAsync(shopId, performingPlayerId, "System", AuditAction.ConfigUpdate, "Shop configuration updated.", cancellationToken);
                _logger.LogInformation("Shop {ShopId} configuration updated by Player {PlayerId}.", shopId, performingPlayerId);
                return ShopOperationResult.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating configuration for Shop {ShopId}", shopId);
                return ShopOperationResult.Fail($"Internal error updating configuration: {ex.Message}");
            }
            finally
            {
                GetShopLock(shopId).Release();
            }
        }

        #endregion

        #region Detailed Inventory Management

        public class ShopItem : IEntity // Assuming IEntity provides an Id property
        {
            public int Id { get; set; } // Unique Item ID (globally or per shop depending on design)
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal BasePrice { get; set; } // Price before dynamic adjustments/discounts
            public decimal CurrentPrice { get; set; } // Price after all adjustments
            public int Quantity { get; set; } // Available stock
            public string Category { get; set; }
            public List<string> Tags { get; set; } = new();
            public List<ItemVariant> Variants { get; set; } = new(); // Color, Size, etc.
            public Dictionary<string, string> Attributes { get; set; } = new(); // Custom fields (e.g., "Damage", "Material")
            public bool IsAvailable { get; set; } = true; // Can be purchased
            public bool IsTradeable { get; set; } = true; // Can be traded between players (if enabled)
            public DateTime DateAdded { get; set; } = DateTime.UtcNow;
            public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
            public int? SupplierId { get; set; } // NEW: Track where item came from
            public int LowStockThreshold { get; set; } = 5; // NEW: Threshold for notification

            // Method to update quantity safely (example)
            public bool TryUpdateQuantity(int change)
            {
                // WARNING: This needs proper locking if called concurrently on the same item instance
                // Better handled within methods that acquire item-level or shop-level locks.
                if (Quantity + change < 0) return false;
                Quantity += change;
                LastUpdated = DateTime.UtcNow;
                return true;
            }
        }

        public class ItemVariant
        {
            public string Sku { get; set; } // Unique identifier for the variant
            public string Name { get; set; } // e.g., "Large", "Red"
            public decimal PriceModifier { get; set; } // Additive or multiplicative
            public int Quantity { get; set; }
            // Specific variant attributes (e.g., Color: "Red", Size: "XL")
            public Dictionary<string, string> Attributes { get; set; } = new();
        }

        public async Task<ShopOperationResult<ShopItem>> AddItemToShopAsync(int shopId, ShopItem item, int performingPlayerId, CancellationToken cancellationToken = default)
        {
            if (!_shopStates.TryGetValue(shopId, out var state))
                return ShopOperationResult<ShopItem>.Fail($"Shop {shopId} not found.");
            if (!await CheckPermissionAsync(performingPlayerId, $"Inventory_Manage_{shopId}", cancellationToken))
                return ShopOperationResult<ShopItem>.Fail("Permission denied.");
            if (state.Inventory.Count >= (state.Configuration?.MaxInventorySize ?? _config.MaxInventorySize))
                return ShopOperationResult<ShopItem>.Fail("Shop inventory is full.");


            await GetShopLock(shopId).WaitAsync(cancellationToken);
            try
            {
                // Ensure unique ID if adding globally, or handle shop-local IDs
                // item.Id = GenerateNewItemId(); // Assuming a method exists
                item.DateAdded = DateTime.UtcNow;
                item.LastUpdated = DateTime.UtcNow;
                item.CurrentPrice = item.BasePrice; // Initial price

                if (state.Inventory.TryAdd(item.Id, item))
                {
                    // Update categories and tags
                    if (!string.IsNullOrEmpty(item.Category))
                    {
                        state.Categories.AddOrUpdate(item.Category,
                            _ => new List<int> { item.Id },
                            (_, list) => { list.Add(item.Id); return list; });
                    }
                    foreach (var tag in item.Tags)
                    {
                        state.Tags.AddOrUpdate(tag,
                            _ => new HashSet<int> { item.Id },
                            (_, set) => { set.Add(item.Id); return set; });
                    }

                    await _persistenceService.SaveShopDataAsync(shopId, ConvertToShopData(state), cancellationToken);
                    await LogAuditEventAsync(shopId, performingPlayerId, "Inventory", AuditAction.ItemAdd, $"Item '{item.Name}' (ID: {item.Id}) added.", cancellationToken);
                    _logger.LogInformation("Item {ItemId} ('{ItemName}') added to Shop {ShopId} by Player {PlayerId}", item.Id, item.Name, shopId, performingPlayerId);
                    return ShopOperationResult<ShopItem>.Success(item);
                }
                else
                {
                    return ShopOperationResult<ShopItem>.Fail($"Item with ID {item.Id} already exists in Shop {shopId}.");
                }
            }
            catch (Exception ex)
            {
                 _logger.LogError(ex, "Error adding item {ItemId} to Shop {ShopId}", item.Id, shopId);
                 return ShopOperationResult<ShopItem>.Fail($"Internal error adding item: {ex.Message}");
            }
            finally
            {
                GetShopLock(shopId).Release();
            }
        }

        // Add methods for UpdateItemInShopAsync, RemoveItemFromShopAsync, RestockItemAsync etc.

        public async Task<ShopOperationResult> RestockShopAsync(int shopId, int performingPlayerId = -1, CancellationToken cancellationToken = default) // -1 for system restock
        {
            if (!_shopStates.TryGetValue(shopId, out var state))
                 return ShopOperationResult.Fail($"Shop {shopId} not found.");
            // Permission check only if initiated by a player
            if (performingPlayerId != -1 && !await CheckPermissionAsync(performingPlayerId, $"Inventory_Manage_{shopId}", cancellationToken))
                 return ShopOperationResult.Fail("Permission denied.");


            await GetShopLock(shopId).WaitAsync(cancellationToken);
            try
            {
                 _logger.LogInformation("Starting restock for Shop {ShopId}...", shopId);
                 var itemsToRestock = GetItemsEligibleForRestock(state); // Placeholder logic
                 int restockedCount = 0;

                 foreach (var item in itemsToRestock)
                 {
                     int restockAmount = CalculateRestockAmount(item); // Placeholder logic
                     if (item.TryUpdateQuantity(restockAmount)) // Needs locking if ShopItem shared
                     {
                         restockedCount++;
                         await _inventoryNotifier.NotifyItemRestockedAsync(shopId, item.Id, item.Name, item.Quantity, cancellationToken);
                     }
                 }
                 state.LastRestockTime = DateTime.UtcNow;

                 await _persistenceService.SaveShopDataAsync(shopId, ConvertToShopData(state), cancellationToken);
                 await LogAuditEventAsync(shopId, performingPlayerId, "Inventory", AuditAction.Restock, $"Restocked {restockedCount} item types.", cancellationToken);
                 _logger.LogInformation("Shop {ShopId} restocked {Count} item types.", shopId, restockedCount);
                 return ShopOperationResult.Success($"Restocked {restockedCount} items.");
            }
             catch (Exception ex)
             {
                 _logger.LogError(ex, "Error restocking Shop {ShopId}", shopId);
                 return ShopOperationResult.Fail($"Internal error during restock: {ex.Message}");
             }
             finally
             {
                 GetShopLock(shopId).Release();
             }
        }

        // Placeholder implementations for restock logic
        private IEnumerable<ShopItem> GetItemsEligibleForRestock(ShopState state) => state.Inventory.Values.Where(i => i.Quantity < i.LowStockThreshold * 2 && i.IsAvailable).Take(10); // Example: Restock low items
        private int CalculateRestockAmount(ShopItem item) => Math.Max(10, item.LowStockThreshold * 3 - item.Quantity); // Example: Restock to 3x threshold


        public async Task<ShopOperationResult<IEnumerable<ShopItem>>> GetShopInventoryAsync(int shopId, string categoryFilter = null, string tagFilter = null, CancellationToken cancellationToken = default)
        {
             if (!_shopStates.TryGetValue(shopId, out var state))
                 return ShopOperationResult<IEnumerable<ShopItem>>.Fail($"Shop {shopId} not found.");

            // Use caching for inventory queries
            string cacheKey = $"Inventory_{shopId}_{categoryFilter ?? "all"}_{tagFilter ?? "all"}";
            if (_cache.TryGetValue(cacheKey, out IEnumerable<ShopItem> cachedInventory))
            {
                _logger.LogDebug("Inventory cache hit for key: {CacheKey}", cacheKey);
                return ShopOperationResult<IEnumerable<ShopItem>>.Success(cachedInventory);
            }

            _logger.LogDebug("Inventory cache miss for key: {CacheKey}", cacheKey);

            IEnumerable<ShopItem> inventory = state.Inventory.Values.Where(i => i.IsAvailable);

            if (!string.IsNullOrEmpty(categoryFilter) && state.Categories.TryGetValue(categoryFilter, out var itemIdsInCategory))
            {
                 var idSet = itemIdsInCategory.ToHashSet();
                 inventory = inventory.Where(i => idSet.Contains(i.Id));
            }

            if (!string.IsNullOrEmpty(tagFilter) && state.Tags.TryGetValue(tagFilter, out var itemIdsWithTag))
            {
                 inventory = inventory.Where(i => itemIdsWithTag.Contains(i.Id));
            }

            // Ensure results are materialized before caching if needed (ToList())
            var resultList = inventory.ToList(); // Materialize

            _cache.Set(cacheKey, resultList, _config.CacheDurationShort); // Cache the result list

            return ShopOperationResult<IEnumerable<ShopItem>>.Success(resultList);
        }


        #endregion

        #region Pricing, Promotions, Coupons

        public class Coupon
        {
            public string Code { get; set; }
            public CouponType Type { get; set; }
            public decimal Value { get; set; } // Amount or Percentage
            public DateTime ExpiryDate { get; set; }
            public int? MaxUses { get; set; }
            public int CurrentUses { get; set; }
            public decimal? MinimumSpend { get; set; }
            public List<int>? ApplicableItemIds { get; set; } // Null means applies to all
            public List<string>? ApplicableCategories { get; set; }
            public bool IsActive { get; set; } = true;
        }

        public enum CouponType { PercentageDiscount, FixedAmountDiscount, FreeShipping } // Example types

        public async Task<ShopOperationResult<Coupon>> CreateCouponAsync(Coupon coupon, int performingPlayerId, CancellationToken cancellationToken = default)
        {
            if (!await CheckPermissionAsync(performingPlayerId, "Manage_Coupons", cancellationToken)) // Global permission
                return ShopOperationResult<Coupon>.Fail("Permission denied.");
            if (string.IsNullOrWhiteSpace(coupon.Code))
                coupon.Code = GenerateCouponCode(); // Generate a random code
            if (_activeCoupons.ContainsKey(coupon.Code))
                return ShopOperationResult<Coupon>.Fail($"Coupon code '{coupon.Code}' already exists.");

            coupon.IsActive = true;
            coupon.CurrentUses = 0;
            if (_activeCoupons.TryAdd(coupon.Code, coupon))
            {
                // Persist coupon (e.g., to a separate coupon table)
                // await _persistenceService.SaveCouponAsync(coupon, cancellationToken);
                await LogAuditEventAsync(-1, performingPlayerId, "Promotion", AuditAction.CouponCreate, $"Coupon '{coupon.Code}' created.", cancellationToken);
                _logger.LogInformation("Coupon '{CouponCode}' created by Player {PlayerId}", coupon.Code, performingPlayerId);
                return ShopOperationResult<Coupon>.Success(coupon);
            }
            return ShopOperationResult<Coupon>.Fail("Failed to add coupon due to a concurrent issue.");
        }

        // Add methods for DeactivateCouponAsync, GetCouponAsync etc.

        // Central method to calculate final price considering all factors
        private decimal CalculateFinalItemPrice(ShopItem item, ShopState shopState, int playerId, Coupon appliedCoupon = null)
        {
            decimal currentPrice = item.BasePrice; // Start with base

            // 1. Dynamic Pricing (if enabled)
            if ((shopState.Configuration?.EnableDynamicPricingGlobally ?? _config.EnableDynamicPricingGlobally))
            {
                 currentPrice = ApplyDynamicPricing(item, shopState); // Placeholder
            }

            // 2. Limited Time Offers (apply best offer if multiple apply)
            var applicableOffers = shopState.LimitedOffers
                .Where(o => o.ItemId == item.Id && o.Expiry > DateTime.UtcNow)
                .ToList();
            if (applicableOffers.Any())
            {
                // Assuming offers are discounts, find the best one (e.g., largest percentage)
                var bestOffer = applicableOffers.OrderByDescending(o => o.DiscountPercentage).First(); // Example logic
                currentPrice *= (1 - bestOffer.DiscountPercentage);
            }

            // 3. Loyalty Discount
            var loyaltyTier = GetPlayerLoyaltyTier(shopState, playerId);
            if (loyaltyTier != null)
            {
                 currentPrice *= (1 - loyaltyTier.DiscountPercentage);
            }

            // 4. Coupon Application (apply AFTER other discounts typically)
            if (appliedCoupon != null && IsCouponApplicable(appliedCoupon, item, shopState))
            {
                currentPrice = ApplyCouponValue(currentPrice, appliedCoupon);
            }


            // Ensure price doesn't go below zero (or a configured minimum)
            currentPrice = Math.Max(0, currentPrice);
            item.CurrentPrice = Math.Round(currentPrice, 2); // Update item's current price snapshot

            return item.CurrentPrice;
        }

        // Placeholder methods for pricing logic
        private decimal ApplyDynamicPricing(ShopItem item, ShopState shopState)
        {
            // Complex logic based on demand (sales history?), supply (quantity), reputation, time etc.
            // Example: slightly increase price if quantity is low
            double factor = 1.0;
            if (item.Quantity < item.LowStockThreshold) factor += 0.1;
            if (item.Quantity > item.LowStockThreshold * 10) factor -= 0.05;
            // Could query _recommendationEngine or another AI service for price suggestions
            return item.BasePrice * (decimal)factor;
        }
        private bool IsCouponApplicable(Coupon coupon, ShopItem item, ShopState shopState)
        {
             if (!coupon.IsActive || coupon.ExpiryDate < DateTime.UtcNow) return false;
             if (coupon.MaxUses.HasValue && coupon.CurrentUses >= coupon.MaxUses.Value) return false;
             if (coupon.ApplicableItemIds != null && !coupon.ApplicableItemIds.Contains(item.Id)) return false;
             if (coupon.ApplicableCategories != null && !coupon.ApplicableCategories.Contains(item.Category)) return false;
             // Check MinimumSpend at order level, not item level usually
             return true;
        }
        private decimal ApplyCouponValue(decimal price, Coupon coupon)
        {
             switch (coupon.Type)
             {
                 case CouponType.PercentageDiscount:
                     return price * (1 - coupon.Value);
                 case CouponType.FixedAmountDiscount:
                     return Math.Max(0, price - coupon.Value);
                 case CouponType.FreeShipping: // Handled at order level
                 default:
                     return price;
             }
        }

        public async Task AdjustAllShopPricesAsync(int shopId, int performingPlayerId = -1, CancellationToken cancellationToken = default)
        {
            if (!_shopStates.TryGetValue(shopId, out var state)) return; // Log error?
            // Permission check needed if player initiated
             if (performingPlayerId != -1 && !await CheckPermissionAsync(performingPlayerId, $"Inventory_Manage_{shopId}", cancellationToken)) return;


            await GetShopLock(shopId).WaitAsync(cancellationToken);
            try
            {
                if (!(state.Configuration?.EnableDynamicPricingGlobally ?? _config.EnableDynamicPricingGlobally))
                {
                    _logger.LogInformation("Dynamic pricing is disabled for Shop {ShopId}. Skipping price adjustment.", shopId);
                    return;
                }

                 _logger.LogInformation("Adjusting dynamic prices for Shop {ShopId}...", shopId);
                 int adjustedCount = 0;
                 foreach (var item in state.Inventory.Values.Where(i => i.IsAvailable))
                 {
                     // Calculate price without considering player/coupons for the base dynamic adjustment
                     decimal newPrice = ApplyDynamicPricing(item, state);
                     if (item.CurrentPrice != newPrice)
                     {
                         item.CurrentPrice = Math.Round(newPrice, 2);
                         item.LastUpdated = DateTime.UtcNow;
                         adjustedCount++;
                     }
                 }
                 state.LastPriceUpdateTime = DateTime.UtcNow;

                 await _persistenceService.SaveShopDataAsync(shopId, ConvertToShopData(state), cancellationToken);
                 await LogAuditEventAsync(shopId, performingPlayerId, "Pricing", AuditAction.PriceUpdate, $"Dynamically adjusted prices for {adjustedCount} items.", cancellationToken);
                 _logger.LogInformation("Dynamically adjusted {Count} prices for Shop {ShopId}.", adjustedCount, shopId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adjusting prices for Shop {ShopId}", shopId);
            }
            finally
            {
                GetShopLock(shopId).Release();
            }
        }


        #endregion

        #region Order Management and Transactions

        public class Order
        {
            public Guid OrderId { get; } = Guid.NewGuid();
            public int ShopId { get; set; }
            public int PlayerId { get; set; }
            public List<OrderItem> Items { get; set; } = new();
            public decimal Subtotal { get; set; }
            public decimal TaxAmount { get; set; }
            public decimal ShippingCost { get; set; } // NEW
            public decimal DiscountAmount { get; set; } // From coupons/promos
            public decimal TotalAmount { get; set; }
            public string Currency { get; set; }
            public OrderStatus Status { get; set; } = OrderStatus.Pending;
            public PaymentDetails PaymentInfo { get; set; }
            public ShippingDetails ShippingInfo { get; set; } // NEW
            public DateTime OrderDate { get; set; } = DateTime.UtcNow;
            public DateTime? LastUpdateDate { get; set; }
            public string Notes { get; set; } // Customer notes
        }

        public class OrderItem
        {
            public int ItemId { get; set; }
            public string ItemNameSnapshot { get; set; } // Name at time of order
            public int Quantity { get; set; }
            public decimal PricePerUnitSnapshot { get; set; } // Price paid per unit
            public string VariantSku { get; set; } // If applicable
        }

        public enum OrderStatus { Pending, PaymentProcessing, PaymentFailed, AwaitingShipment, Shipped, Delivered, Cancelled, Refunded }

        public class PaymentDetails
        {
            public string PaymentMethodToken { get; set; } // e.g., "visa_****1234" or "paypal_token"
            public string TransactionId { get; set; } // From payment gateway
            public PaymentStatus Status { get; set; }
            public DateTime? PaymentDate { get; set; }
        }
        public enum PaymentStatus { Pending, Success, Failed, Refunded }

        public class ShippingDetails { /* Address, TrackingNumber, Carrier etc. */ }
        public class PaymentResult { public bool Success { get; set; } public string TransactionId { get; set; } public string ErrorMessage { get; set; } } // From Payment Gateway
        public class RefundResult { public bool Success { get; set; } public string RefundTransactionId { get; set; } public string ErrorMessage { get; set; } } // From Payment Gateway


        // Represents the input for creating an order
        public class CreateOrderRequest
        {
             public int ShopId { get; set; }
             public int PlayerId { get; set; }
             public Dictionary<int, int> Items { get; set; } // ItemId -> Quantity
             public string CouponCode { get; set; }
             public string PaymentMethodToken { get; set; }
             public ShippingDetails ShippingInfo { get; set; }
             public string Notes { get; set; }
        }

        public async Task<ShopOperationResult<Order>> CreateOrderAsync(CreateOrderRequest request, CancellationToken cancellationToken = default)
        {
            await _operationThrottle.WaitAsync(cancellationToken); // Throttle overall operations
            try
            {
                if (!_shopStates.TryGetValue(request.ShopId, out var state))
                    return ShopOperationResult<Order>.Fail($"Shop {request.ShopId} not found.");
                if (state.Status != ShopInstanceStatus.Open)
                    return ShopOperationResult<Order>.Fail($"Shop {request.ShopId} is not open ({state.Status}).");
                if (request.Items == null || !request.Items.Any())
                    return ShopOperationResult<Order>.Fail("Order must contain items.");

                // --- Validation & Locking ---
                var shopConfig = state.Configuration ?? _config;
                if (request.Items.Count > shopConfig.DefaultMaxItemsPerOrder)
                     return ShopOperationResult<Order>.Fail($"Cannot order more than {shopConfig.DefaultMaxItemsPerOrder} distinct items at once.");
                if (state.BlockedPlayerIds.Contains(request.PlayerId.ToString())) // Assuming string IDs for block list
                     return ShopOperationResult<Order>.Fail("You are blocked from purchasing at this shop.");

                // Verify 2FA if needed (example)
                if (shopConfig.Require2FAForHighValueTrades && CalculatePotentialOrderValue(request.Items, state) > 1000) // Example threshold
                {
                    // string twoFactorCode = Get2FACodeFromRequest(request); // Need a way to pass this
                    // if (!await VerifyTwoFactorAuthAsync(request.PlayerId, twoFactorCode, cancellationToken))
                    //     return ShopOperationResult<Order>.Fail("Two-factor authentication failed.");
                }

                // Validate Coupon
                Coupon coupon = null;
                if (!string.IsNullOrEmpty(request.CouponCode))
                {
                    if (!_activeCoupons.TryGetValue(request.CouponCode, out coupon) || !coupon.IsActive || coupon.ExpiryDate < DateTime.UtcNow)
                         return ShopOperationResult<Order>.Fail($"Invalid or expired coupon code: {request.CouponCode}");
                    // Further coupon validation (uses left, applicable items check later)
                }


                // --- Acquire Lock & Check Inventory ---
                // Lock the specific shop for the critical inventory check and update phase
                await GetShopLock(request.ShopId).WaitAsync(cancellationToken);
                try
                {
                    var order = new Order
                    {
                        ShopId = request.ShopId,
                        PlayerId = request.PlayerId,
                        Currency = shopConfig.DefaultCurrency,
                        ShippingInfo = request.ShippingInfo,
                        Notes = request.Notes,
                        PaymentInfo = new PaymentDetails { PaymentMethodToken = request.PaymentMethodToken, Status = PaymentStatus.Pending }
                    };

                    decimal subtotal = 0;
                    var itemsToUpdate = new List<(ShopItem Item, int QuantityChange)>();

                    foreach (var kvp in request.Items)
                    {
                        int itemId = kvp.Key;
                        int quantity = kvp.Value;

                        if (quantity <= 0) continue; // Ignore invalid quantities

                        if (!state.Inventory.TryGetValue(itemId, out var item) || !item.IsAvailable)
                             return ShopOperationResult<Order>.Fail($"Item ID {itemId} not found or unavailable.", order); // Return partial order for context

                        if (item.Quantity < quantity)
                             return ShopOperationResult<Order>.Fail($"Insufficient stock for Item ID {itemId} ('{item.Name}'). Available: {item.Quantity}", order);

                        // Recalculate price at time of order creation, considering player/coupon
                        decimal pricePerUnit = CalculateFinalItemPrice(item, state, request.PlayerId, coupon); // Pass coupon here too

                        order.Items.Add(new OrderItem
                        {
                            ItemId = itemId,
                            ItemNameSnapshot = item.Name,
                            Quantity = quantity,
                            PricePerUnitSnapshot = pricePerUnit
                            // VariantSku = ... // Handle variants if necessary
                        });
                        subtotal += pricePerUnit * quantity;
                        itemsToUpdate.Add((item, -quantity)); // Prepare inventory update
                    }

                    order.Subtotal = Math.Round(subtotal, 2);

                    // Apply Order-Level Coupon Effects (Minimum Spend, Fixed Amount Off Subtotal)
                    decimal discountAmount = 0;
                    if (coupon != null)
                    {
                        if (coupon.MinimumSpend.HasValue && order.Subtotal < coupon.MinimumSpend.Value)
                        {
                             return ShopOperationResult<Order>.Fail($"Coupon '{coupon.Code}' requires a minimum spend of {coupon.MinimumSpend.Value:C}.", order);
                        }
                        if (coupon.Type == CouponType.FixedAmountDiscount) // Apply fixed amount off subtotal
                        {
                            discountAmount = Math.Min(order.Subtotal, coupon.Value); // Don't discount more than the subtotal
                        }
                        // Percentage was already applied at item level in CalculateFinalItemPrice
                        // Free Shipping handled later
                    }

                    order.DiscountAmount = Math.Round(discountAmount, 2);
                    decimal taxableAmount = order.Subtotal - order.DiscountAmount;
                    order.TaxAmount = Math.Round(taxableAmount * shopConfig.DefaultTaxRate, 2);
                    order.ShippingCost = CalculateShippingCost(request.ShippingInfo, order); // Placeholder
                     if (coupon?.Type == CouponType.FreeShipping) order.ShippingCost = 0; // Apply free shipping coupon
                    order.TotalAmount = Math.Round(taxableAmount + order.TaxAmount + order.ShippingCost, 2);


                    // --- Payment Processing ---
                    order.Status = OrderStatus.PaymentProcessing;
                    _logger.LogInformation("Processing payment for Order {OrderId}, Amount: {Amount} {Currency}", order.OrderId, order.TotalAmount, order.Currency);
                    PaymentResult paymentResult = await _paymentGateway.ProcessPaymentAsync(request.PlayerId, order.TotalAmount, order.Currency, request.PaymentMethodToken, cancellationToken);

                    if (!paymentResult.Success)
                    {
                        order.Status = OrderStatus.PaymentFailed;
                        order.PaymentInfo.Status = PaymentStatus.Failed;
                        await LogAuditEventAsync(request.ShopId, request.PlayerId, "Order", AuditAction.PaymentFail, $"Payment failed for Order {order.OrderId}. Gateway Msg: {paymentResult.ErrorMessage}", cancellationToken);
                        _logger.LogWarning("Payment failed for Order {OrderId}. Gateway Msg: {ErrorMessage}", order.OrderId, paymentResult.ErrorMessage);
                        // No inventory change needed if payment fails BEFORE inventory update
                        return ShopOperationResult<Order>.Fail($"Payment failed: {paymentResult.ErrorMessage}", order);
                    }

                    // --- Payment Success - Update Inventory & State ---
                    order.Status = OrderStatus.AwaitingShipment; // Or directly to Delivered/Complete if digital goods
                    order.PaymentInfo.Status = PaymentStatus.Success;
                    order.PaymentInfo.TransactionId = paymentResult.TransactionId;
                    order.PaymentInfo.PaymentDate = DateTime.UtcNow;
                    order.LastUpdateDate = DateTime.UtcNow;

                    // Atomically update inventory (within the lock)
                    foreach (var (item, quantityChange) in itemsToUpdate)
                    {
                        if (!item.TryUpdateQuantity(quantityChange)) // This update should succeed as we checked before
                        {
                            // THIS IS A CRITICAL FAILURE - Payment succeeded but inventory update failed. Requires compensation logic (refund, admin alert)
                            _logger.LogCritical("CRITICAL: Inventory update failed for Item {ItemId} on Order {OrderId} AFTER successful payment. Manual intervention required.", item.Id, order.OrderId);
                            order.Status = OrderStatus.PaymentProcessing; // Revert status? Requires careful state management.
                            // Immediately attempt refund?
                            // await ProcessRefundAsync(order.OrderId, order.TotalAmount, "Inventory update failure", -1, cancellationToken); // System initiated refund
                            return ShopOperationResult<Order>.Fail("CRITICAL ERROR: Inventory update failed after payment. Please contact support.", order);
                        }
                         // Check for low stock notification
                         if (item.Quantity <= item.LowStockThreshold)
                         {
                             await _inventoryNotifier.NotifyLowStockAsync(request.ShopId, item.Id, item.Name, item.Quantity, cancellationToken);
                         }
                    }

                    // Increment coupon usage count if applicable
                    if (coupon != null)
                    {
                        Interlocked.Increment(ref coupon.CurrentUses);
                        // Persist coupon update
                        // await _persistenceService.UpdateCouponUsageAsync(coupon.Code, coupon.CurrentUses, cancellationToken);
                    }

                    // Add loyalty points
                    await AddLoyaltyPointsAsync(request.ShopId, request.PlayerId, order.TotalAmount, cancellationToken); // Use TotalAmount or Subtotal based on rules


                    // --- Persistence and Events ---
                    await _persistenceService.SaveOrderAsync(order, cancellationToken);
                    await _persistenceService.SaveShopDataAsync(request.ShopId, ConvertToShopData(state), cancellationToken); // Save updated inventory/state
                    await LogAuditEventAsync(request.ShopId, request.PlayerId, "Order", AuditAction.OrderCreate, $"Order {order.OrderId} created successfully. Total: {order.TotalAmount} {order.Currency}", cancellationToken);

                    // Raise events
                    OrderPlaced?.Invoke(this, new OrderEventArgs(order));
                    await _eventBus.PublishAsync(new OrderPlacedEvent(order), cancellationToken);

                    _logger.LogInformation("Order {OrderId} created successfully for Player {PlayerId} at Shop {ShopId}. Total: {TotalAmount} {Currency}", order.OrderId, request.PlayerId, request.ShopId, order.TotalAmount, order.Currency);
                    return ShopOperationResult<Order>.Success(order);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Critical error during order creation/inventory update for Shop {ShopId}, Player {PlayerId}", request.ShopId, request.PlayerId);
                    // Consider attempting a refund if payment might have succeeded
                    return ShopOperationResult<Order>.Fail($"Internal server error during order processing: {ex.Message}");
                }
                finally
                {
                    GetShopLock(request.ShopId).Release(); // Release the shop-specific lock
                }
            }
            finally
            {
                _operationThrottle.Release(); // Release the global operation throttle
            }
        }

        public async Task<ShopOperationResult> UpdateOrderStatusAsync(Guid orderId, OrderStatus newStatus, int performingPlayerId, string trackingNumber = null, CancellationToken cancellationToken = default)
        {
             // TODO: Load order from persistence
             // Order order = await _persistenceService.LoadOrderAsync(orderId, cancellationToken);
             Order order = null; // Placeholder
             if (order == null) return ShopOperationResult.Fail($"Order {orderId} not found.");

             if (!await CheckPermissionAsync(performingPlayerId, $"Order_Manage_{order.ShopId}", cancellationToken)) // Check permission
                 return ShopOperationResult.Fail("Permission denied.");

             var oldStatus = order.Status;
             if (oldStatus == newStatus) return ShopOperationResult.Success("Status already set."); // No change

             // Add logic for valid status transitions
             // e.g., cannot go from Delivered back to AwaitingShipment

             order.Status = newStatus;
             order.LastUpdateDate = DateTime.UtcNow;
             if (newStatus == OrderStatus.Shipped && trackingNumber != null)
             {
                 order.ShippingInfo ??= new ShippingDetails();
                 // order.ShippingInfo.TrackingNumber = trackingNumber;
             }

             // await _persistenceService.SaveOrderAsync(order, cancellationToken); // Save changes

             OrderStatusChanged?.Invoke(this, new OrderEventArgs(order));
             await _eventBus.PublishAsync(new OrderStatusChangedEvent(order), cancellationToken);
             await LogAuditEventAsync(order.ShopId, performingPlayerId, "Order", AuditAction.StatusChange, $"Order {orderId} status changed from {oldStatus} to {newStatus}.", cancellationToken);
             _logger.LogInformation("Order {OrderId} status updated to {NewStatus} by Player {PlayerId}", orderId, newStatus, performingPlayerId);

             return ShopOperationResult.Success();
        }

        public async Task<ShopOperationResult<RefundResult>> ProcessRefundAsync(Guid orderId, decimal amountToRefund, string reason, int performingPlayerId, CancellationToken cancellationToken = default)
        {
            // TODO: Load order from persistence
            Order order = null; // Placeholder
            if (order == null) return ShopOperationResult<RefundResult>.Fail($"Order {orderId} not found.");

            if (order.Status == OrderStatus.Refunded || order.PaymentInfo?.Status == PaymentStatus.Refunded)
                 return ShopOperationResult<RefundResult>.Fail("Order is already fully refunded.");
             if (amountToRefund <= 0 || amountToRefund > order.TotalAmount /* Add check for partial refunds already processed */)
                 return ShopOperationResult<RefundResult>.Fail($"Invalid refund amount: {amountToRefund}");
             if (!await CheckPermissionAsync(performingPlayerId, $"Order_Refund_{order.ShopId}", cancellationToken)) // Check permission
                 return ShopOperationResult<RefundResult>.Fail("Permission denied to process refunds.");


            _logger.LogInformation("Attempting refund for Order {OrderId}, Amount: {Amount}, Reason: {Reason}", orderId, amountToRefund, reason);
            RefundResult refundResult = await _paymentGateway.ProcessRefundAsync(order.PaymentInfo?.TransactionId, amountToRefund, reason, cancellationToken);

            if (!refundResult.Success)
            {
                _logger.LogWarning("Refund failed for Order {OrderId}. Gateway Msg: {ErrorMessage}", orderId, refundResult.ErrorMessage);
                 await LogAuditEventAsync(order.ShopId, performingPlayerId, "Order", AuditAction.RefundFail, $"Refund attempt failed for Order {orderId}. Gateway Msg: {refundResult.ErrorMessage}", cancellationToken);
                 return ShopOperationResult<RefundResult>.Fail($"Refund failed: {refundResult.ErrorMessage}", refundResult);
            }

             _logger.LogInformation("Refund successful for Order {OrderId}. Refund Tx ID: {RefundTxId}", orderId, refundResult.RefundTransactionId);

             // --- Update Order Status & State ---
             order.Status = OrderStatus.Refunded; // Or PartialRefund if implementing that
             order.PaymentInfo.Status = PaymentStatus.Refunded; // Or PartialRefund
             order.LastUpdateDate = DateTime.UtcNow;
             // Store refund transaction ID? Maybe in a separate refund record list on the order.

             // --- Reverse Loyalty Points ---
             // await RemoveLoyaltyPointsAsync(order.ShopId, order.PlayerId, amountToRefund, cancellationToken); // Need logic for point removal

             // --- Potentially Restock Items (Optional based on policy) ---
             // if (ShouldRestockOnRefund(reason)) { await RestockOrderItemsAsync(order, cancellationToken); }

             // --- Persistence and Events ---
             // await _persistenceService.SaveOrderAsync(order, cancellationToken);
             await LogAuditEventAsync(order.ShopId, performingPlayerId, "Order", AuditAction.RefundSuccess, $"Refund processed for Order {orderId}. Amount: {amountToRefund}. Refund Tx ID: {refundResult.RefundTransactionId}", cancellationToken);

             OrderStatusChanged?.Invoke(this, new OrderEventArgs(order));
             await _eventBus.PublishAsync(new OrderRefundedEvent(order, amountToRefund, reason), cancellationToken);


             return ShopOperationResult<RefundResult>.Success(refundResult);
        }

        // Placeholder calculation methods
        private decimal CalculatePotentialOrderValue(Dictionary<int, int> items, ShopState state) => 5000; // Simulate high value
        private decimal CalculateShippingCost(ShippingDetails shippingInfo, Order order) => 10.0m; // Basic flat rate

        #endregion

        #region Reputation System Enhancements

        public class ShopReputation
        {
            // Store individual ratings with timestamps for decay/weighting
            private readonly ConcurrentBag<PlayerRating> _ratings = new();
            private readonly ConcurrentDictionary<int, string> _playerReviews = new(); // PlayerId -> Review Text
            public List<SellerResponse> SellerResponses { get; } = new(); // NEW: Responses to reviews
            public List<ModerationAction> ModerationHistory { get; } = new(); // NEW: Track moderated reviews

            public float CalculateAverageRating(float recencyWeightFactor) // Example: factor = 0.7 means 70% weight to recent
            {
                var now = DateTime.UtcNow;
                double weightedSum = 0;
                double totalWeight = 0;
                // Give more weight to recent ratings (e.g., within last 3 months)
                var recentCutoff = now.AddMonths(-3);

                foreach (var r in _ratings)
                {
                    double weight = (r.Timestamp > recentCutoff) ? recencyWeightFactor : (1.0 - recencyWeightFactor);
                    weightedSum += r.Rating * weight;
                    totalWeight += weight;
                }

                return totalWeight > 0 ? (float)(weightedSum / totalWeight) : 0;
            }

            public int RatingCount => _ratings.Count;
            public string CalculateReputationTier(ShopConfiguration config) // Pass config for tiers
            {
                float score = CalculateAverageRating(config.ReputationWeightRecent); // Use configured weight
                // Use configured tiers or default logic
                 return score switch
                 {
                     >= 4.7f => "Elite",
                     >= 4.2f => "Excellent",
                     >= 3.5f => "Trusted",
                     >= 2.5f => "Standard",
                     _ => "Needs Improvement" // Changed from Unrated
                 };
            }

            public bool AddRating(int playerId, float rating, DateTime timestamp, string review = null, bool requiresModeration = false)
            {
                // Check if player already rated/reviewed based on config
                // if (_ratings.Count(r => r.PlayerId == playerId) >= config.MaxReviewsPerPlayerPerShop) return false;

                var clampedRating = Math.Clamp(rating, 0, 5); // Clamp rating 0-5
                _ratings.Add(new PlayerRating(playerId, clampedRating, timestamp, requiresModeration));

                if (!string.IsNullOrWhiteSpace(review))
                {
                    _playerReviews[playerId] = review; // Overwrite previous review if allowed, or handle differently
                }
                return true;
            }

            public IEnumerable<PlayerRating> GetRatings(bool includePendingModeration = false) =>
                _ratings.Where(r => includePendingModeration || !r.IsPendingModeration);

            public IEnumerable<(int PlayerId, string Review)> GetReviews() =>
                 _playerReviews.Select(kvp => (kvp.Key, kvp.Value));

             public PlayerRating GetRatingByPlayer(int playerId) => _ratings.FirstOrDefault(r => r.PlayerId == playerId);
        }

        public record PlayerRating(int PlayerId, float Rating, DateTime Timestamp, bool IsPendingModeration = false);
        public record SellerResponse(int ReviewerPlayerId, string ResponseText, DateTime Timestamp);
        public record ModerationAction(int ReviewerPlayerId, ModerationOutcome Outcome, string Reason, int ModeratorId, DateTime Timestamp);
        public enum ModerationOutcome { Approved, Rejected, Edited }


        public async Task<ShopOperationResult> RateAndReviewShopAsync(int shopId, int playerId, float rating, string review = null, CancellationToken cancellationToken = default)
        {
            if (!_shopStates.TryGetValue(shopId, out var state))
                return ShopOperationResult.Fail($"Shop {shopId} not found.");

            var shopConfig = state.Configuration ?? _config;
            var reputation = state.Reputation;

            // Check if player is allowed to review (e.g., must have purchased recently - needs order history check)
            // bool canReview = await CheckPlayerEligibilityToReviewAsync(shopId, playerId, cancellationToken);
            // if (!canReview) return ShopOperationResult.Fail("You must make a purchase before reviewing.");

             if (reputation.GetRatings().Count(r => r.PlayerId == playerId) >= shopConfig.MaxReviewsPerPlayerPerShop)
                 return ShopOperationResult.Fail($"You have reached the maximum number of reviews ({shopConfig.MaxReviewsPerPlayerPerShop}) for this shop.");


            bool requiresModeration = DoesReviewRequireModeration(review); // Placeholder for content filtering AI/logic
            if (reputation.AddRating(playerId, rating, DateTime.UtcNow, review, requiresModeration))
            {
                // Persist changes
                // await _persistenceService.SaveShopReputationAsync(shopId, reputation, cancellationToken);

                // Raise local event
                ReviewAdded?.Invoke(this, new ShopReviewEventArgs(shopId, playerId, rating, review, requiresModeration));
                // Publish to event bus
                await _eventBus.PublishAsync(new ShopRatingEvent(shopId, playerId, rating, review, requiresModeration), cancellationToken);

                await LogAuditEventAsync(shopId, playerId, "Reputation", AuditAction.ReviewAdd, $"Review submitted. Rating: {rating}. Moderation: {requiresModeration}", cancellationToken);
                _logger.LogInformation("Shop {ShopId} reviewed by Player {PlayerId}. Rating: {Rating}. Moderation pending: {Moderation}", shopId, playerId, rating, requiresModeration);

                // If not pending moderation, immediately update cached reputation score?
                if (!requiresModeration)
                     await UpdateCachedReputationScoreAsync(shopId, state);


                return ShopOperationResult.Success(requiresModeration ? "Review submitted and pending moderation." : "Review submitted successfully.");
            }
            else
            {
                // This case might happen due to concurrency if AddRating had internal checks/locks failed
                return ShopOperationResult.Fail("Failed to add review due to an internal conflict.");
            }
        }

        public async Task<ShopOperationResult> ModerateReviewAsync(int shopId, int reviewerPlayerId, ModerationOutcome outcome, string reason, int moderatorId, CancellationToken cancellationToken = default)
        {
             if (!_shopStates.TryGetValue(shopId, out var state))
                 return ShopOperationResult.Fail($"Shop {shopId} not found.");
             if (!await CheckPermissionAsync(moderatorId, "Moderate_Reviews", cancellationToken)) // Global moderation permission
                 return ShopOperationResult.Fail("Permission denied.");

             var reputation = state.Reputation;
             var rating = reputation.GetRatingByPlayer(reviewerPlayerId);

             if (rating == null || !rating.IsPendingModeration)
                 return ShopOperationResult.Fail($"No review pending moderation found for Player {reviewerPlayerId} in Shop {shopId}.");

             // Update the PlayerRating record - REMOVING the IsPendingModeration flag
             // This requires finding and updating the specific record in the ConcurrentBag, which is tricky.
             // A better approach might be to store ratings in a ConcurrentDictionary<int, PlayerRating> by PlayerId
             // Or filter out pending reviews during calculation and add approved ones to a separate 'published' list.
             // For simplicity here, assume we can mark it as not pending:
             // rating.IsPendingModeration = false; // This won't work directly on the record from the Bag

             // Record the moderation action
             var modAction = new ModerationAction(reviewerPlayerId, outcome, reason, moderatorId, DateTime.UtcNow);
             reputation.ModerationHistory.Add(modAction);

             if (outcome == ModerationOutcome.Rejected)
             {
                 // Optionally remove the rating/review entirely
                 // reputation.RemoveRating(reviewerPlayerId); // Requires ability to remove from ConcurrentBag or change data structure
                 _playerReviews.TryRemove(reviewerPlayerId, out _);
             }

             // Persist changes
             // await _persistenceService.SaveShopReputationAsync(shopId, reputation, cancellationToken);

             ReviewModerated?.Invoke(this, new ShopReviewEventArgs(shopId, reviewerPlayerId, rating.Rating, _playerReviews.GetValueOrDefault(reviewerPlayerId), false)); // Indicate moderation complete
             await _eventBus.PublishAsync(new ShopReviewModeratedEvent(shopId, reviewerPlayerId, outcome, reason, moderatorId), cancellationToken);
             await LogAuditEventAsync(shopId, moderatorId, "Reputation", AuditAction.ReviewModerate, $"Review from Player {reviewerPlayerId} moderated. Outcome: {outcome}. Reason: {reason}", cancellationToken);
             _logger.LogInformation("Review for Shop {ShopId} from Player {ReviewerId} moderated by {ModeratorId}. Outcome: {Outcome}", shopId, reviewerPlayerId, moderatorId, outcome);

             // Update cached score now that moderation is done
             await UpdateCachedReputationScoreAsync(shopId, state);

             return ShopOperationResult.Success($"Review moderation completed with outcome: {outcome}");
        }

        // Add AddSellerResponseAsync

        // Helper method to update cached score (call after review add/mod)
        private async Task UpdateCachedReputationScoreAsync(int shopId, ShopState state)
        {
             var shopConfig = state.Configuration ?? _config;
             float avgRating = state.Reputation.CalculateAverageRating(shopConfig.ReputationWeightRecent);
             string tier = state.Reputation.CalculateReputationTier(shopConfig);
             _cache.Set($"ReputationScore_{shopId}", avgRating, _config.CacheDurationMedium);
             _cache.Set($"ReputationTier_{shopId}", tier, _config.CacheDurationMedium);
             await Task.CompletedTask; // Placeholder if async operations needed
        }

        // Placeholders
        private bool DoesReviewRequireModeration(string review) => review?.Contains("badword", StringComparison.OrdinalIgnoreCase) ?? false; // Basic check
        private Task<bool> CheckPlayerEligibilityToReviewAsync(int shopId, int playerId, CancellationToken cancellationToken) => Task.FromResult(true); // Assume true for now


        // Public accessors for cached reputation data
        public float GetShopAverageRating(int shopId) => _cache.Get<float>($"ReputationScore_{shopId}");
        public string GetShopReputationTier(int shopId) => _cache.Get<string>($"ReputationTier_{shopId}") ?? "N/A";

        #endregion

        #region Auction System Improvements

        public class Auction
        {
            public Guid AuctionId { get; } = Guid.NewGuid(); // Unique ID for the auction
            public int ShopId { get; }
            public int ItemId { get; }
            public ShopItem ItemSnapshot { get; set; } // Snapshot of item details at auction start
            public int Quantity { get; } = 1; // Auctions usually for single items or specific lots
            public decimal StartingBid { get; }
            public decimal? ReservePrice { get; set; } // NEW: Minimum price to sell
            public decimal CurrentBid { get; set; }
            public int? HighestBidderId { get; set; }
            public decimal? BuyItNowPrice { get; set; } // NEW: Option to buy immediately
            public DateTime StartTime { get; }
            public DateTime EndTime { get; set; } // Can be extended
            public AuctionStatus Status { get; set; } = AuctionStatus.Scheduled;
            public List<BidRecord> BidHistory { get; } = new(); // NEW: Track all bids
            public ConcurrentDictionary<int, decimal> MaxProxyBids { get; } = new(); // NEW: PlayerId -> Max Bid
        }

        public enum AuctionStatus { Scheduled, Active, EndingSoon, Sold, FailedReserve, Cancelled, EndedNoBids }
        public record BidRecord(int PlayerId, decimal BidAmount, DateTime Timestamp);


        public async Task<ShopOperationResult<Auction>> StartAuctionAsync(int shopId, int itemId, int quantity, decimal startingBid, TimeSpan duration, decimal? reservePrice = null, decimal? buyItNowPrice = null, int performingPlayerId = -1, CancellationToken cancellationToken = default)
        {
            if (!_shopStates.TryGetValue(shopId, out var state))
                return ShopOperationResult<Auction>.Fail($"Shop {shopId} not found.");
            // Permission check
            if (performingPlayerId != -1 && !await CheckPermissionAsync(performingPlayerId, $"Auction_Manage_{shopId}", cancellationToken))
                return ShopOperationResult<Auction>.Fail("Permission denied.");


            // --- Lock Shop & Validate Item ---
            await GetShopLock(shopId).WaitAsync(cancellationToken);
            try
            {
                 if (!state.Inventory.TryGetValue(itemId, out var item) || !item.IsAvailable || item.Quantity < quantity)
                    return ShopOperationResult<Auction>.Fail($"Item ID {itemId} not found, unavailable, or insufficient quantity ({item.Quantity}/{quantity}) for auction.");


                // --- Remove Item from Regular Inventory ---
                if (!item.TryUpdateQuantity(-quantity))
                    return ShopOperationResult<Auction>.Fail($"Failed to reserve item {itemId} quantity for auction.");


                // --- Create Auction Object ---
                var auction = new Auction(shopId, itemId, quantity, startingBid, DateTime.UtcNow + duration)
                {
                    ItemSnapshot = JsonSerializer.Deserialize<ShopItem>(JsonSerializer.Serialize(item)), // Deep clone snapshot
                    ReservePrice = reservePrice,
                    BuyItNowPrice = buyItNowPrice,
                    Status = AuctionStatus.Active // Start immediately
                };
                auction.CurrentBid = startingBid; // Initial current bid is the starting bid

                if (!state.ActiveAuctions.TryAdd(auction.AuctionId, auction))
                {
                    // Failed to add, attempt to rollback inventory change
                    item.TryUpdateQuantity(quantity); // Best effort rollback
                    return ShopOperationResult<Auction>.Fail("Failed to add auction due to a concurrent issue.");
                }

                // --- Persistence and Events ---
                await _persistenceService.SaveShopDataAsync(shopId, ConvertToShopData(state), cancellationToken); // Save updated inventory & auctions
                await LogAuditEventAsync(shopId, performingPlayerId, "Auction", AuditAction.AuctionStart, $"Auction {auction.AuctionId} started for Item {itemId} (Qty: {quantity}).", cancellationToken);

                AuctionUpdated?.Invoke(this, new AuctionEventArgs(auction));
                await _eventBus.PublishAsync(new AuctionStartedEvent(auction), cancellationToken);

                _logger.LogInformation("Auction {AuctionId} started for Item {ItemId} (Qty: {Quantity}) in Shop {ShopId}. Ends at: {EndTime}", auction.AuctionId, itemId, quantity, shopId, auction.EndTime);
                return ShopOperationResult<Auction>.Success(auction);
            }
            catch (Exception ex)
            {
                 _logger.LogError(ex, "Error starting auction for Item {ItemId} in Shop {ShopId}", itemId, shopId);
                 // Consider rollback logic if item was already removed from inventory
                 return ShopOperationResult<Auction>.Fail($"Internal error starting auction: {ex.Message}");
            }
            finally
            {
                GetShopLock(shopId).Release();
            }
        }


        public async Task<ShopOperationResult> PlaceBidAsync(Guid auctionId, int shopId, int playerId, decimal bidAmount, bool isProxyBid = false, decimal maxProxyBid = 0, CancellationToken cancellationToken = default)
        {
            if (!_shopStates.TryGetValue(shopId, out var state) || !state.ActiveAuctions.TryGetValue(auctionId, out var auction))
                return ShopOperationResult.Fail($"Auction {auctionId} not found or invalid shop.");
            if (auction.Status != AuctionStatus.Active && auction.Status != AuctionStatus.EndingSoon)
                return ShopOperationResult.Fail($"Auction {auctionId} is not active ({auction.Status}).");
            if (auction.EndTime < DateTime.UtcNow)
                 return ShopOperationResult.Fail($"Auction {auctionId} has already ended.");
             if (playerId == auction.HighestBidderId && bidAmount <= auction.CurrentBid) // Allow raising own bid
                 return ShopOperationResult.Fail("Your bid must be higher than the current bid.");
             if (bidAmount <= auction.CurrentBid)
                 return ShopOperationResult.Fail($"Bid amount {bidAmount:C} must be higher than the current bid {auction.CurrentBid:C}.");


            // --- Lock Auction (or Shop) ---
            // For simplicity, using shop lock, but ideally auction could have its own lock
            await GetShopLock(shopId).WaitAsync(cancellationToken);
            try
            {
                // Re-check status after acquiring lock
                if (auction.Status != AuctionStatus.Active && auction.Status != AuctionStatus.EndingSoon || auction.EndTime < DateTime.UtcNow)
                    return ShopOperationResult.Fail("Auction status changed or ended while waiting to place bid.");

                decimal actualBid = bidAmount; // The bid amount for this specific action
                int? previousHighestBidder = auction.HighestBidderId;

                // --- Handle Proxy Bidding ---
                if (isProxyBid)
                {
                    if (maxProxyBid <= auction.CurrentBid) return ShopOperationResult.Fail("Maximum proxy bid must be higher than the current bid.");
                    auction.MaxProxyBids[playerId] = maxProxyBid; // Store/Update max proxy bid

                    // Determine who should be the highest bidder now
                    var (newHighestBidder, newCurrentBid) = DetermineHighestBidderWithProxy(auction);

                    if (newHighestBidder != auction.HighestBidderId || newCurrentBid != auction.CurrentBid)
                    {
                        auction.HighestBidderId = newHighestBidder;
                        auction.CurrentBid = newCurrentBid;
                        actualBid = newCurrentBid; // Log the actual resulting bid
                    }
                    else
                    {
                        // Proxy bid placed but didn't change the outcome immediately
                        // Still log the proxy placement maybe? Or just return success.
                         _logger.LogInformation("Player {PlayerId} updated max proxy bid to {MaxBid} for Auction {AuctionId}. No change in current bid.", playerId, maxProxyBid, auctionId);
                         // No event needed if current bid/bidder didn't change
                         return ShopOperationResult.Success($"Proxy bid updated to {maxProxyBid:C}. You are still the highest bidder."); // Or adjust message
                    }
                }
                else // Regular Bid
                {
                    // Check against existing proxy bids
                    auction.MaxProxyBids.TryRemove(playerId, out _); // Placing a manual bid removes previous proxy for that player
                    var (newHighestBidder, newCurrentBid) = DetermineHighestBidderWithProxy(auction, (playerId, bidAmount)); // Check if this bid triggers others

                    auction.HighestBidderId = newHighestBidder;
                    auction.CurrentBid = newCurrentBid;
                    actualBid = newCurrentBid; // The resulting bid might be higher due to proxy
                }


                // --- Record Bid and Update State ---
                var bidRecord = new BidRecord(playerId, actualBid, DateTime.UtcNow);
                auction.BidHistory.Add(bidRecord);

                // Extend auction end time if bid placed near the end (Anti-Sniping)
                var config = state.Configuration ?? _config;
                if (auction.EndTime - DateTime.UtcNow < config.AuctionEndTimeExtension)
                {
                    auction.EndTime = DateTime.UtcNow + config.AuctionEndTimeExtension;
                    _logger.LogInformation("Auction {AuctionId} end time extended to {NewEndTime} due to late bid.", auctionId, auction.EndTime);
                }


                // --- Persistence and Events ---
                // await _persistenceService.SaveAuctionUpdateAsync(auction, cancellationToken); // Persist bid history, current bid, bidder, end time
                await LogAuditEventAsync(shopId, playerId, "Auction", AuditAction.AuctionBid, $"Placed bid on Auction {auctionId}. Resulting Bid: {auction.CurrentBid}. New High Bidder: {auction.HighestBidderId}", cancellationToken);

                AuctionUpdated?.Invoke(this, new AuctionEventArgs(auction));
                await _eventBus.PublishAsync(new AuctionBidEvent(auction, playerId, actualBid), cancellationToken);

                // Notify previous high bidder they were outbid (if different)
                if (previousHighestBidder.HasValue && previousHighestBidder != auction.HighestBidderId)
                {
                    await _eventBus.PublishAsync(new AuctionOutbidEvent(auction, previousHighestBidder.Value), cancellationToken);
                }

                _logger.LogInformation("Player {PlayerId} bid on Auction {AuctionId}. Current Bid: {CurrentBid}, High Bidder: {HighBidderId}", playerId, auctionId, auction.CurrentBid, auction.HighestBidderId);
                return ShopOperationResult.Success($"Bid placed successfully. Current bid is now {auction.CurrentBid:C}.");

            }
            catch (Exception ex)
            {
                 _logger.LogError(ex, "Error placing bid for Auction {AuctionId} by Player {PlayerId}", auctionId, playerId);
                 return ShopOperationResult.Fail($"Internal error placing bid: {ex.Message}");
            }
            finally
            {
                GetShopLock(shopId).Release();
            }
        }

        // Helper for proxy bid resolution
        private (int? HighestBidder, decimal HighestBid) DetermineHighestBidderWithProxy(Auction auction, (int PlayerId, decimal BidAmount)? currentManualBid = null)
        {
            // 1. Combine existing proxy bids and the current manual bid (if any)
            var potentialBids = auction.MaxProxyBids.Select(kvp => (PlayerId: kvp.Key, MaxBid: kvp.Value)).ToList();
            if (currentManualBid.HasValue)
            {
                // Treat a manual bid like a proxy bid of that exact amount for resolution purposes
                potentialBids.Add((currentManualBid.Value.PlayerId, currentManualBid.Value.BidAmount));
            }

            if (!potentialBids.Any()) // No bids at all
            {
                 return (null, auction.StartingBid); // Or CurrentBid if it could be > starting
            }

            // 2. Filter out bids lower than the absolute minimum needed (current bid + increment)
            //    Define a minimum bid increment (e.g., 1% or a fixed amount)
            decimal minIncrement = Math.Max(1.0m, auction.CurrentBid * 0.01m); // Example: $1 or 1%
            decimal minNextBid = auction.CurrentBid + minIncrement;

            var eligibleBidders = potentialBids
                .Where(b => b.MaxBid >= minNextBid)
                .OrderByDescending(b => b.MaxBid) // Highest max bid first
                // Add tie-breaking rule if needed (e.g., earliest proxy bid placed wins tie - needs timestamp tracking for proxy bids)
                .ToList();

             if (!eligibleBidders.Any())
             {
                 // No one bid high enough, current bidder/bid remains
                 return (auction.HighestBidderId, auction.CurrentBid);
             }

             // 3. Determine Winner and Winning Bid
             var winner = eligibleBidders[0];

             if (eligibleBidders.Count == 1)
             {
                 // Only one person bid high enough. They win at the minimum required bid.
                 return (winner.PlayerId, minNextBid);
             }
             else
             {
                 // Multiple bidders. Winner pays slightly more than the second highest bidder's max bid.
                 var secondHighestMaxBid = eligibleBidders[1].MaxBid;
                 decimal winningBid = Math.Min(winner.MaxBid, secondHighestMaxBid + minIncrement); // Winner pays increment over 2nd place, up to their max
                 return (winner.PlayerId, winningBid);
             }
        }

        // Process ended auctions (called by background timer)
        private async Task ProcessEndedAuctionsAsync(object state) // state is CancellationToken
        {
            var cancellationToken = (CancellationToken)state;
            _logger.LogDebug("Running auction processing task...");

            List<Auction> auctionsToEnd = new List<Auction>();
            foreach (var shopState in _shopStates.Values)
            {
                auctionsToEnd.AddRange(shopState.ActiveAuctions.Values
                    .Where(a => (a.Status == AuctionStatus.Active || a.Status == AuctionStatus.EndingSoon) && a.EndTime < DateTime.UtcNow));
            }

            if (!auctionsToEnd.Any()) return;

            _logger.LogInformation("Processing {Count} ended auctions...", auctionsToEnd.Count);

            foreach (var auction in auctionsToEnd)
            {
                 if (!_shopStates.TryGetValue(auction.ShopId, out var shopState)) continue; // Shop gone?

                await GetShopLock(auction.ShopId).WaitAsync(cancellationToken);
                try
                {
                    // Double check status and end time inside lock
                    if (auction.Status != AuctionStatus.Active && auction.Status != AuctionStatus.EndingSoon || auction.EndTime >= DateTime.UtcNow) continue;


                    bool sold = false;
                    bool reserveMet = !auction.ReservePrice.HasValue || auction.CurrentBid >= auction.ReservePrice.Value;

                    if (auction.HighestBidderId.HasValue && reserveMet)
                    {
                        // --- Auction Sold ---
                        auction.Status = AuctionStatus.Sold;
                        sold = true;

                        // Create an Order automatically for the winner
                        var orderRequest = new CreateOrderRequest
                        {
                            ShopId = auction.ShopId,
                            PlayerId = auction.HighestBidderId.Value,
                            Items = new Dictionary<int, int> { { auction.ItemId, auction.Quantity } },
                             // Payment token needs to be handled - maybe player pre-authorized? Or mark order as AwaitingPayment?
                             // PaymentMethodToken = GetPreAuthorizedToken(auction.HighestBidderId.Value),
                             Notes = $"Winning bid for Auction {auction.AuctionId}",
                             // Shipping info might need collection post-auction
                        };
                        // Price is auction.CurrentBid. Need to adapt CreateOrderAsync or have a specific method.
                        // For now, log and notify, assuming manual order creation or separate flow.
                        _logger.LogInformation("Auction {AuctionId} SOLD to Player {PlayerId} for {Amount} {Currency}.", auction.AuctionId, auction.HighestBidderId.Value, auction.CurrentBid, shopState.Configuration?.DefaultCurrency ?? _config.DefaultCurrency);
                        await LogAuditEventAsync(auction.ShopId, -1, "Auction", AuditAction.AuctionEnd, $"Auction {auction.AuctionId} ended. Sold to {auction.HighestBidderId.Value} for {auction.CurrentBid}.", cancellationToken);
                        await _eventBus.PublishAsync(new AuctionSoldEvent(auction), cancellationToken); // Notify winner & seller

                    }
                    else if (auction.HighestBidderId.HasValue && !reserveMet)
                    {
                        // --- Reserve Not Met ---
                        auction.Status = AuctionStatus.FailedReserve;
                        _logger.LogInformation("Auction {AuctionId} ended. Reserve price not met (Current: {CurrentBid}, Reserve: {ReservePrice}).", auction.AuctionId, auction.CurrentBid, auction.ReservePrice);
                         await LogAuditEventAsync(auction.ShopId, -1, "Auction", AuditAction.AuctionEnd, $"Auction {auction.AuctionId} ended. Reserve not met.", cancellationToken);
                         await _eventBus.PublishAsync(new AuctionEndedEvent(auction), cancellationToken); // Generic end event

                        // Return item to inventory?
                        // await ReturnAuctionItemToInventoryAsync(auction, shopState, cancellationToken);
                    }
                    else
                    {
                        // --- No Bids ---
                        auction.Status = AuctionStatus.EndedNoBids;
                        _logger.LogInformation("Auction {AuctionId} ended with no bids.", auction.AuctionId);
                         await LogAuditEventAsync(auction.ShopId, -1, "Auction", AuditAction.AuctionEnd, $"Auction {auction.AuctionId} ended. No bids.", cancellationToken);
                         await _eventBus.PublishAsync(new AuctionEndedEvent(auction), cancellationToken); // Generic end event

                        // Return item to inventory?
                         // await ReturnAuctionItemToInventoryAsync(auction, shopState, cancellationToken);
                    }

                    // Remove from active auctions (or move to historical auctions)
                    shopState.ActiveAuctions.TryRemove(auction.AuctionId, out _);
                    // Persist changes
                    // await _persistenceService.SaveShopDataAsync(shopState.ShopId, ConvertToShopData(shopState), cancellationToken);
                    // await _persistenceService.SaveAuctionResultAsync(auction, cancellationToken);


                    AuctionEnded?.Invoke(this, new AuctionEventArgs(auction)); // Local event

                }
                 catch (Exception ex)
                 {
                     _logger.LogError(ex, "Error processing ended auction {AuctionId} for Shop {ShopId}", auction.AuctionId, auction.ShopId);
                 }
                 finally
                 {
                     GetShopLock(auction.ShopId).Release();
                 }
            }
        }


        // Add BuyItNowAsync method

        #endregion

        #region Loyalty Program Enhancements

        public class LoyaltyTier
        {
            public string Name { get; }
            public int PointsRequired { get; }
            public decimal DiscountPercentage { get; } // Applied automatically
            public int BonusPointsPercentage { get; } // Extra points earned per purchase
            // Add potential other benefits: exclusive offers flag, early access flag, etc.

            public LoyaltyTier(string name, int pointsRequired, decimal discountPercentage, int bonusPointsPercentage)
            {
                Name = name;
                PointsRequired = pointsRequired;
                DiscountPercentage = discountPercentage;
                BonusPointsPercentage = bonusPointsPercentage;
            }
        }

        public class LoyaltyRecord
        {
            public int PlayerId { get; }
            public int CurrentPoints { get; set; } = 0;
            public int LifetimePoints { get; set; } = 0;
            public string CurrentTierName { get; set; } = "Bronze"; // Default starting tier
            public DateTime LastActivityDate { get; set; } = DateTime.UtcNow;

            public LoyaltyRecord(int playerId) => PlayerId = playerId;

            public void AddPoints(int pointsToAdd, List<LoyaltyTier> tiers)
            {
                if (pointsToAdd <= 0) return;
                CurrentPoints += pointsToAdd;
                LifetimePoints += pointsToAdd;
                LastActivityDate = DateTime.UtcNow;
                UpdateTier(tiers); // Check if tier changes
            }

            public bool RedeemPoints(int pointsToRedeem)
            {
                if (pointsToRedeem <= 0 || pointsToRedeem > CurrentPoints) return false;
                CurrentPoints -= pointsToRedeem;
                LastActivityDate = DateTime.UtcNow;
                // Tier update typically depends on lifetime points or recent activity, not just current points after redemption
                // UpdateTier(tiers); // Re-evaluate tier if rules depend on current points balance
                return true;
            }

            public void UpdateTier(List<LoyaltyTier> tiers)
            {
                // Determine tier based on LifetimePoints (or other rules)
                var newTier = tiers.Where(t => LifetimePoints >= t.PointsRequired)
                                  .OrderByDescending(t => t.PointsRequired)
                                  .FirstOrDefault();
                CurrentTierName = newTier?.Name ?? "Bronze";
            }
        }

        public async Task AddLoyaltyPointsAsync(int shopId, int playerId, decimal amountSpent, CancellationToken cancellationToken = default)
        {
             if (!_shopStates.TryGetValue(shopId, out var state)) return; // Log error?
             if (amountSpent <= 0) return;

             // Find or create loyalty record (using shop lock for safety on add)
             LoyaltyRecord record = null;
             bool recordExists = state.LoyaltyRecords.Any(r => r.PlayerId == playerId);

             if (recordExists)
             {
                 record = state.LoyaltyRecords.First(r => r.PlayerId == playerId);
             }
             else
             {
                 // Need lock to safely add new record if multiple purchases happen concurrently for a new player
                 await GetShopLock(shopId).WaitAsync(cancellationToken);
                 try
                 {
                     // Double check inside lock
                     record = state.LoyaltyRecords.FirstOrDefault(r => r.PlayerId == playerId);
                     if (record == null)
                     {
                         record = new LoyaltyRecord(playerId);
                         state.LoyaltyRecords.Add(record);
                     }
                 }
                 finally
                 {
                     GetShopLock(shopId).Release();
                 }
             }


             var shopConfig = state.Configuration ?? _config;
             var currentTier = shopConfig.LoyaltyTiers.FirstOrDefault(t => t.Name == record.CurrentTierName) ?? shopConfig.LoyaltyTiers.First(); // Default to base tier

             int pointsEarned = (int)Math.Floor(amountSpent * shopConfig.PointsPerCurrencyUnitSpent);
             int bonusPoints = (int)Math.Floor(pointsEarned * (currentTier.BonusPointsPercentage / 100.0m));
             int totalPointsToAdd = pointsEarned + bonusPoints;

             record.AddPoints(totalPointsToAdd, shopConfig.LoyaltyTiers); // Add points and update tier

             // Persist loyalty data update
             // await _persistenceService.SaveLoyaltyRecordAsync(shopId, record, cancellationToken);

             LoyaltyPointsUpdated?.Invoke(this, new LoyaltyEventArgs(shopId, playerId, record.CurrentPoints, record.CurrentTierName, totalPointsToAdd));
             await _eventBus.PublishAsync(new LoyaltyUpdateEvent(shopId, playerId, record.CurrentPoints, record.CurrentTierName, totalPointsToAdd), cancellationToken);
             await LogAuditEventAsync(shopId, playerId, "Loyalty", AuditAction.PointsAdd, $"Earned {totalPointsToAdd} points. New Balance: {record.CurrentPoints}. Tier: {record.CurrentTierName}", cancellationToken);
             _logger.LogDebug("Player {PlayerId} earned {Points} loyalty points in Shop {ShopId}. New balance: {Balance}. Tier: {Tier}", playerId, totalPointsToAdd, shopId, record.CurrentPoints, record.CurrentTierName);
        }

        public async Task<ShopOperationResult> RedeemLoyaltyPointsAsync(int shopId, int playerId, int pointsToRedeem, string rewardDescription, CancellationToken cancellationToken = default)
        {
             if (!_shopStates.TryGetValue(shopId, out var state))
                 return ShopOperationResult.Fail($"Shop {shopId} not found.");

             var record = state.LoyaltyRecords.FirstOrDefault(r => r.PlayerId == playerId);
             if (record == null || record.CurrentPoints < pointsToRedeem)
                 return ShopOperationResult.Fail($"Insufficient loyalty points. Available: {record?.CurrentPoints ?? 0}, Required: {pointsToRedeem}");
             if (pointsToRedeem <= 0)
                 return ShopOperationResult.Fail("Points to redeem must be positive.");

             // Potentially need lock if multiple redemptions can happen concurrently
             // await GetShopLock(shopId).WaitAsync(cancellationToken);
             // try { ... } finally { GetShopLock(shopId).Release(); }

             if (record.RedeemPoints(pointsToRedeem))
             {
                 // Persist changes
                 // await _persistenceService.SaveLoyaltyRecordAsync(shopId, record, cancellationToken);

                 LoyaltyPointsUpdated?.Invoke(this, new LoyaltyEventArgs(shopId, playerId, record.CurrentPoints, record.CurrentTierName, -pointsToRedeem)); // Negative points indicates redemption
                 await _eventBus.PublishAsync(new LoyaltyPointsRedeemedEvent(shopId, playerId, pointsToRedeem, rewardDescription), cancellationToken);
                 await LogAuditEventAsync(shopId, playerId, "Loyalty", AuditAction.PointsRedeem, $"Redeemed {pointsToRedeem} points for '{rewardDescription}'. New Balance: {record.CurrentPoints}", cancellationToken);
                 _logger.LogInformation("Player {PlayerId} redeemed {Points} loyalty points in Shop {ShopId} for {Reward}. New balance: {Balance}", playerId, pointsToRedeem, shopId, rewardDescription, record.CurrentPoints);

                 return ShopOperationResult.Success($"Successfully redeemed {pointsToRedeem} points.");
             }
             else
             {
                 // Should not happen if check above passed, unless concurrency issue without lock
                 return ShopOperationResult.Fail("Failed to redeem points due to concurrent update.");
             }
        }

        public LoyaltyTier GetPlayerLoyaltyTier(ShopState state, int playerId)
        {
            var record = state.LoyaltyRecords.FirstOrDefault(r => r.PlayerId == playerId);
            if (record == null) return (state.Configuration ?? _config).LoyaltyTiers.First(); // Return base tier

            return (state.Configuration ?? _config).LoyaltyTiers.FirstOrDefault(t => t.Name == record.CurrentTierName) ?? (state.Configuration ?? _config).LoyaltyTiers.First();
        }


        #endregion

        #region Social Features (Wishlist, Gifting)

        public class WishlistItem
        {
            public int ItemId { get; set; }
            public DateTime DateAdded { get; set; } = DateTime.UtcNow;
            public string Notes { get; set; }
        }

        // Store wishlists per player (globally or maybe per-shop context if desired)
        private readonly ConcurrentDictionary<int, List<WishlistItem>> _playerWishlists = new();

        public async Task<ShopOperationResult> AddToWishlistAsync(int playerId, int itemId, int shopIdHint, string notes = null, CancellationToken cancellationToken = default)
        {
             // Check if item exists (using shopIdHint or global item lookup)
             // if (!await DoesItemExistAsync(itemId, shopIdHint, cancellationToken))
             //     return ShopOperationResult.Fail($"Item {itemId} does not exist.");

             var wishlist = _playerWishlists.GetOrAdd(playerId, _ => new List<WishlistItem>());

             if (wishlist.Count >= _config.MaxWishlistItems)
                 return ShopOperationResult.Fail($"Wishlist is full (Max: {_config.MaxWishlistItems}).");
             if (wishlist.Any(w => w.ItemId == itemId))
                 return ShopOperationResult.Fail($"Item {itemId} is already in your wishlist.");

             wishlist.Add(new WishlistItem { ItemId = itemId, Notes = notes });

             // Persist wishlist change
             // await _persistenceService.SaveWishlistAsync(playerId, wishlist, cancellationToken);

             WishlistUpdated?.Invoke(this, new WishlistEventArgs(playerId, itemId, WishlistAction.Added));
             await _eventBus.PublishAsync(new WishlistUpdatedEvent(playerId, itemId, WishlistAction.Added), cancellationToken);
             // No audit log needed for personal wishlist typically

             _logger.LogDebug("Player {PlayerId} added Item {ItemId} to wishlist.", playerId, itemId);
             return ShopOperationResult.Success("Item added to wishlist.");
        }

        // Add RemoveFromWishlistAsync, GetWishlistAsync

        public async Task<ShopOperationResult<Order>> GiftItemAsync(int shopId, int givingPlayerId, int receivingPlayerId, int itemId, int quantity, string message, string paymentMethodToken, CancellationToken cancellationToken = default)
        {
             if (givingPlayerId == receivingPlayerId)
                 return ShopOperationResult<Order>.Fail("Cannot gift an item to yourself.");

             _logger.LogInformation("Attempting to gift Item {ItemId} (Qty: {Quantity}) from Player {GiverId} to Player {ReceiverId} via Shop {ShopId}", itemId, quantity, givingPlayerId, receivingPlayerId, shopId);

             // Use CreateOrder logic, but modify recipient and potentially skip shipping step if digital
             var giftRequest = new CreateOrderRequest
             {
                 ShopId = shopId,
                 PlayerId = givingPlayerId, // The buyer is the giver
                 Items = new Dictionary<int, int> { { itemId, quantity } },
                 PaymentMethodToken = paymentMethodToken,
                 Notes = $"Gift for Player {receivingPlayerId}. Message: {message}",
                 // ShippingInfo might still be needed if physical item
             };

             var result = await CreateOrderAsync(giftRequest, cancellationToken);

             if (result.IsSuccess)
             {
                 var order = result.Value;
                 // Mark the order as a gift, potentially update status immediately if digital
                 // order.IsGift = true;
                 // order.ReceivingPlayerId = receivingPlayerId;
                 // if (IsDigitalItem(itemId)) order.Status = OrderStatus.Delivered;

                 // await _persistenceService.SaveOrderAsync(order, cancellationToken); // Save gift details

                 ItemGifted?.Invoke(this, new GiftEventArgs(order, receivingPlayerId, message));
                 await _eventBus.PublishAsync(new ItemGiftedEvent(order, receivingPlayerId, message), cancellationToken);
                 await LogAuditEventAsync(shopId, givingPlayerId, "Gifting", AuditAction.GiftSend, $"Gifted Item {itemId} (Qty: {quantity}) to Player {receivingPlayerId}. Order: {order.OrderId}", cancellationToken);
                 _logger.LogInformation("Player {GiverId} successfully gifted Item {ItemId} to Player {ReceiverId}. Order {OrderId}", givingPlayerId, itemId, receivingPlayerId, order.OrderId);
             }
             else
             {
                 _logger.LogWarning("Gifting Item {ItemId} from Player {GiverId} to Player {ReceiverId} failed: {Error}", itemId, givingPlayerId, receivingPlayerId, result.ErrorMessage);
             }

             return result; // Return the result of the underlying order creation attempt
        }

        #endregion

        #region AI Features Enhancements

        public async Task<ShopOperationResult<List<int>>> GetPersonalizedRecommendationsAsync(int playerId, int count, CancellationToken cancellationToken = default)
        {
            string cacheKey = $"Recommendations_{playerId}_{count}";
            if (_cache.TryGetValue(cacheKey, out List<int> cachedRecs))
            {
                return ShopOperationResult<List<int>>.Success(cachedRecs);
            }

            try
            {
                 var recommendations = await _recommendationEngine.GetRecommendedItemIdsAsync(playerId, count, cancellationToken);
                 _cache.Set(cacheKey, recommendations, _config.CacheDurationMedium);
                 return ShopOperationResult<List<int>>.Success(recommendations);
            }
            catch (Exception ex)
            {
                 _logger.LogError(ex, "Failed to get recommendations for Player {PlayerId}", playerId);
                 return ShopOperationResult<List<int>>.Fail($"Error fetching recommendations: {ex.Message}");
            }
        }

        // Background task to periodically update the recommendation model
        private async Task UpdateRecommendationModelAsync(object state) // state is CancellationToken
        {
            var cancellationToken = (CancellationToken)state;
            _logger.LogInformation("Starting recommendation model training update...");
            try
            {
                // Fetch recent orders or relevant interaction data from persistence
                var recentOrders = await _persistenceService.GetRecentOrdersForTrainingAsync(TimeSpan.FromDays(7), cancellationToken); // Example method
                await _recommendationEngine.TrainModelAsync(recentOrders, cancellationToken);
                _logger.LogInformation("Recommendation model training update completed.");
            }
            catch (Exception ex)
            {
                 _logger.LogError(ex, "Failed to update recommendation model.");
            }
        }

        // Add hooks for AI-based fraud detection, chatbot interaction etc.
        public async Task<FraudCheckResult> CheckTransactionFraudAsync(Order order, CancellationToken cancellationToken = default)
        {
             // Placeholder: Send order details to an AI fraud detection service
             // FraudCheckResult result = await _fraudDetectionService.AnalyzeAsync(order, cancellationToken);
             await Task.Delay(50, cancellationToken); // Simulate check
             var result = new FraudCheckResult { IsFraudulent = false, Score = 0.1f, Reason = "Low risk" };
             if (result.IsFraudulent)
             {
                  _logger.LogWarning("Potential fraud detected for Order {OrderId}. Score: {Score}, Reason: {Reason}", order.OrderId, result.Score, result.Reason);
                  await LogAuditEventAsync(order.ShopId, order.PlayerId, "Fraud", AuditAction.FraudCheck, $"Potential fraud detected. Score: {result.Score}", cancellationToken);
             }
             return result;
        }
        public record FraudCheckResult(bool IsFraudulent, float Score, string Reason);

        #endregion

        #region Security, Admin, Auditing

        // Basic Role-Based Access Control (RBAC) simulation
        public async Task<bool> AssignRoleAsync(int targetPlayerId, string role, int performingAdminId, CancellationToken cancellationToken = default)
        {
            if (!await CheckPermissionAsync(performingAdminId, "Manage_Roles", cancellationToken)) return false;

            var session = _userSessions.GetOrAdd(targetPlayerId, pid => new UserSession(pid));
            bool added = session.Roles.Add(role);

            if (added)
            {
                 // Persist role change
                 // await _persistenceService.SaveUserRolesAsync(targetPlayerId, session.Roles, cancellationToken);
                 await LogAuditEventAsync(-1, performingAdminId, "Admin", AuditAction.RoleAssign, $"Assigned role '{role}' to Player {targetPlayerId}.", cancellationToken);
                 _logger.LogInformation("Player {AdminId} assigned role '{Role}' to Player {TargetId}", performingAdminId, role, targetPlayerId);
            }
            return added;
        }

        // Add RemoveRoleAsync

        // Central permission checking logic
        public async Task<bool> CheckPermissionAsync(int playerId, string permission, CancellationToken cancellationToken = default)
        {
            // Super Admin Check
             if (playerId == Constant.SUPER_ADMIN_PLAYER_ID) return true; // Example super admin

            if (!_userSessions.TryGetValue(playerId, out var session))
            {
                // Load roles if session doesn't exist? For simplicity, assume session exists if roles assigned.
                 return false; // No session, no permissions
            }

            // Check for direct permission or role-based permission
            // This requires a mapping of roles to permissions (e.g., "Admin" has "Manage_Roles", "ShopOwner_1" has "Inventory_Manage_1")
            // Example simple check:
            if (permission.StartsWith("Inventory_Manage_"))
            {
                 int shopId = int.Parse(permission.Split('_').Last());
                 if (session.Roles.Contains($"ShopOwner_{shopId}") || session.Roles.Contains("Admin")) return true;
            }
            if (permission == "Manage_Coupons" && session.Roles.Contains("Admin")) return true;
            if (permission == "Moderate_Reviews" && (session.Roles.Contains("Admin") || session.Roles.Contains("Moderator"))) return true;
            // ... other permission checks ...

            await Task.CompletedTask; // Placeholder for potential async permission lookups
            return false; // Default deny
        }


        // Audit Logging
        public enum AuditAction { Login, ConfigUpdate, StatusChange, ItemAdd, ItemUpdate, ItemRemove, Restock, PriceUpdate, OrderCreate, PaymentFail, RefundSuccess, RefundFail, ReviewAdd, ReviewModerate, AuctionStart, AuctionBid, AuctionEnd, LoyaltyUpdate, PointsAdd, PointsRedeem, GiftSend, RoleAssign, RoleRemove, BanUser, FraudCheck, DataBackup, DataRestore, CouponCreate, CouponDeactivate /* etc */ }

        public record AuditLogEntry(Guid EntryId, DateTime Timestamp, int? ShopId, int PerformingPlayerId, string ActorType, string Category, AuditAction Action, string Details, string IpAddress); // ActorType: Player/System

        private async Task LogAuditEventAsync(int? shopId, int performingPlayerId, string category, AuditAction action, string details, CancellationToken cancellationToken = default, string ipAddress = "N/A") // IP would come from request context
        {
            var entry = new AuditLogEntry(
                Guid.NewGuid(),
                DateTime.UtcNow,
                shopId,
                performingPlayerId,
                performingPlayerId == -1 ? "System" : "Player",
                category,
                action,
                details,
                ipAddress
            );

            // Log locally
            _logger.LogInformation("[AUDIT] Actor:{ActorId} Shop:{ShopId} Cat:{Category} Action:{Action} Details:{Details}",
                performingPlayerId == -1 ? "System" : performingPlayerId.ToString(),
                shopId?.ToString() ?? "Global",
                category,
                action,
                details);

            // Persist audit entry asynchronously
            await _persistenceService.LogAuditEventAsync(entry, cancellationToken);

            // Optionally publish to an event bus for real-time monitoring
             // await _eventBus.PublishAsync(new AuditEventOccurred(entry), cancellationToken);
        }

        // Add BanPlayerAsync, UnbanPlayerAsync methods (would update block lists)

        // Basic Auth Token Management (from original code, slightly enhanced)
        public string GenerateUserSessionToken(int playerId)
        {
             var session = _userSessions.GetOrAdd(playerId, pid => new UserSession(pid));
             // Invalidate old tokens if necessary
             // string token = GenerateSecureToken(); // Use the method below
             // _cache.Set($"SessionToken_{playerId}", token, TimeSpan.FromHours(8)); // Cache token linked to player
             return session.SessionToken; // Return the existing or newly generated token from session
        }

        public bool ValidateUserSessionToken(int playerId, string token)
        {
             if (_userSessions.TryGetValue(playerId, out var session) && session.SessionToken == token)
             {
                 session.LastActivity = DateTime.UtcNow; // Update activity timestamp
                 return true;
             }
             // Could also check cache if tokens are stored there separately
             // return _cache.TryGetValue($"SessionToken_{playerId}", out string cachedToken) && cachedToken == token;
             return false;
        }

        private static string GenerateSecureToken(int byteLength = 32)
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(byteLength));
        }

        // Add Rate Limiting simulation hooks

        #endregion

        #region Management, Backup, Restore

        public class ShopSchedule
        {
            public DayOfWeek Day { get; set; }
            public TimeSpan OpenTime { get; set; }
            public TimeSpan CloseTime { get; set; }
            public bool IsSpecialEvent { get; set; } // For holidays etc.
        }

        public async Task ConfigureShopScheduleAsync(int shopId, List<ShopSchedule> schedules, int performingPlayerId, CancellationToken cancellationToken = default)
        {
             if (!_shopStates.TryGetValue(shopId, out var state)) return; // Log error?
             if (!await CheckPermissionAsync(performingPlayerId, $"ShopConfig_Update_{shopId}", cancellationToken)) return;


            await GetShopLock(shopId).WaitAsync(cancellationToken);
            try
            {
                state.Schedules.Clear();
                state.Schedules.AddRange(schedules);

                // Persist changes
                 // await _persistenceService.SaveShopScheduleAsync(shopId, schedules, cancellationToken);
                 await LogAuditEventAsync(shopId, performingPlayerId, "Admin", AuditAction.ConfigUpdate, "Shop schedule updated.", cancellationToken);
                 _logger.LogInformation("Shop {ShopId} schedule updated by Player {PlayerId}", shopId, performingPlayerId);
            }
            finally
            {
                 GetShopLock(shopId).Release();
            }
        }

        // Method to check if a shop should be open based on schedule
        private bool IsShopScheduledOpen(ShopState state)
        {
            if (!state.Schedules.Any()) return true; // No schedule means always open (or follow manual status)

            var now = DateTime.UtcNow;
            var currentDay = now.DayOfWeek;
            var currentTime = now.TimeOfDay;

            var relevantSchedules = state.Schedules.Where(s => s.Day == currentDay).ToList();
            // TODO: Add handling for special event overrides

            foreach (var schedule in relevantSchedules)
            {
                 if (currentTime >= schedule.OpenTime && currentTime < schedule.CloseTime)
                 {
                     return true; // Currently within an open period
                 }
            }
            return false; // Not within any scheduled open period for today
        }

        // Update shop status based on schedule (run periodically)
        private async Task UpdateShopStatusFromScheduleAsync(CancellationToken cancellationToken)
        {
             foreach (var (shopId, state) in _shopStates)
             {
                 // Only affect shops that are currently Closed or Open (don't override Maintenance/Locked etc.)
                 if (state.Status == ShopInstanceStatus.Open || state.Status == ShopInstanceStatus.Closed || state.Status == ShopInstanceStatus.ScheduledClosure)
                 {
                     bool shouldBeOpen = IsShopScheduledOpen(state);
                     var desiredStatus = shouldBeOpen ? ShopInstanceStatus.Open : ShopInstanceStatus.ScheduledClosure;

                     if (state.Status != desiredStatus)
                     {
                         await UpdateShopStatusAsync(shopId, desiredStatus, "Scheduled Status Change", cancellationToken);
                     }
                 }
             }
        }


        public async Task<ShopOperationResult> BackupAllShopDataAsync(string directoryPath, int performingPlayerId, CancellationToken cancellationToken = default)
        {
            if (!await CheckPermissionAsync(performingPlayerId, "System_Backup", cancellationToken))
                return ShopOperationResult.Fail("Permission denied.");

            _logger.LogInformation("Starting full shop data backup...");
            GlobalStatus = ShopGlobalStatus.Maintenance; // Enter maintenance for safety? Or just log?
            await _eventBus.PublishAsync(new ShopSystemStatusEvent(GlobalStatus), cancellationToken);

            try
            {
                Directory.CreateDirectory(directoryPath);
                string timestamp = DateTime.UtcNow.ToString("yyyyMMdd_HHmmss");

                // Backup Global Config
                var globalConfigPath = Path.Combine(directoryPath, $"global_config_{timestamp}.json");
                await File.WriteAllTextAsync(globalConfigPath, JsonSerializer.Serialize(_config, GetJsonOptions()), cancellationToken);

                // Backup Coupons
                var couponsPath = Path.Combine(directoryPath, $"coupons_{timestamp}.json");
                await File.WriteAllTextAsync(couponsPath, JsonSerializer.Serialize(_activeCoupons.Values, GetJsonOptions()), cancellationToken);

                // Backup each shop state
                foreach (var (shopId, state) in _shopStates)
                {
                    // WARNING: Accessing state directly might have concurrency issues if not locked.
                    // Better: Load fresh data from persistence for backup, or ensure proper locking.
                    // For simplicity here, we serialize the in-memory state.
                    var shopData = ConvertToShopData(state); // Use the DTO for serialization
                    var shopPath = Path.Combine(directoryPath, $"shop_{shopId}_data_{timestamp}.json");
                    await File.WriteAllTextAsync(shopPath, JsonSerializer.Serialize(shopData, GetJsonOptions()), cancellationToken);
                }

                // Backup Orders, Audit Logs etc. would likely be handled by DB backup procedures.

                await LogAuditEventAsync(null, performingPlayerId, "Admin", AuditAction.DataBackup, $"Full system data backed up to {directoryPath}", cancellationToken);
                _logger.LogInformation("Shop data backup completed successfully to {Directory}", directoryPath);
                return ShopOperationResult.Success($"Backup completed to {directoryPath}");
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Failed to perform shop data backup.");
                await LogAuditEventAsync(null, performingPlayerId, "Admin", AuditAction.DataBackup, $"Backup FAILED: {ex.Message}", cancellationToken);
                return ShopOperationResult.Fail($"Backup failed: {ex.Message}");
            }
            finally
            {
                GlobalStatus = ShopGlobalStatus.Online; // Restore status
                await _eventBus.PublishAsync(new ShopSystemStatusEvent(GlobalStatus), cancellationToken);
            }
        }

        // Add RestoreShopDataAsync (much more complex, involves stopping services, clearing state, loading from files)

        // Add methods for analytics: GetSalesReportAsync, GetTopSellingItemsAsync, GetCustomerLifetimeValueAsync etc.

        #endregion

        #region Background Operations Setup

        private void StartBackgroundTasks()
        {
            _logger.LogInformation("Starting background tasks...");

            // Use System.Threading.Timer for recurring tasks
            _healthMonitorTimer = new Timer(
                async (_) => await MonitorHealthAsync(_cts.Token),
                _cts.Token, // Pass token as state
                TimeSpan.FromSeconds(30), // Initial delay
                _config.HealthCheckInterval);

            _pendingTransactionTimer = new Timer(
                async (_) => await ProcessPendingTransactionsAsync(_cts.Token), // Placeholder if needed
                _cts.Token,
                TimeSpan.FromSeconds(5),
                _config.PendingTransactionInterval);

            _autoRestockTimer = new Timer(
                async (_) => await AutoRestockShopsTaskAsync(_cts.Token),
                _cts.Token,
                TimeSpan.FromMinutes(1),
                _config.AutoRestockCheckInterval);

            _auctionProcessingTimer = new Timer(
                async (_) => await ProcessEndedAuctionsAsync(_cts.Token),
                _cts.Token,
                TimeSpan.FromSeconds(15),
                _config.AuctionProcessingInterval);

            _recommendationModelUpdateTimer = new Timer(
                 async (_) => await UpdateRecommendationModelAsync(_cts.Token),
                 _cts.Token,
                 TimeSpan.FromMinutes(5), // Delay initial run
                 _config.RecommendationModelUpdateInterval);

             // Add Timer for checking shop schedules and updating status
             var scheduleCheckTimer = new Timer(
                 async (_) => await UpdateShopStatusFromScheduleAsync(_cts.Token),
                 _cts.Token,
                 TimeSpan.FromMinutes(1),
                 TimeSpan.FromMinutes(5)); // Check schedule every 5 mins


            _logger.LogInformation("Background tasks started.");
        }

        private async Task MonitorHealthAsync(CancellationToken token)
        {
            if (token.IsCancellationRequested) return;
            _logger.LogDebug("Running health check...");
            // Perform checks: Dependency connectivity, DB ping, cache status, queue lengths
            bool isHealthy = true; // Assume healthy initially
            // if (!await _persistenceService.PingAsync(token)) isHealthy = false;
            // ... other checks ...

            var previousStatus = GlobalStatus;
            if (isHealthy && (GlobalStatus == ShopGlobalStatus.Degraded || GlobalStatus == ShopGlobalStatus.Error))
            {
                GlobalStatus = ShopGlobalStatus.Online;
                _logger.LogWarning("Shop system health restored. Status changed back to Online.");
                 await LogAuditEventAsync(null, -1, "System", AuditAction.StatusChange, "System health restored.", token);
                 await _eventBus.PublishAsync(new ShopSystemStatusEvent(GlobalStatus), token);
            }
            else if (!isHealthy && GlobalStatus == ShopGlobalStatus.Online)
            {
                 GlobalStatus = ShopGlobalStatus.Degraded; // Or Error if critical
                 _logger.LogError("Shop system health check failed! Status changed to Degraded.");
                 await LogAuditEventAsync(null, -1, "System", AuditAction.StatusChange, "System health check failed. Status: Degraded.", token);
                 await _eventBus.PublishAsync(new ShopSystemStatusEvent(GlobalStatus), token);
            }
            _logger.LogDebug("Health check complete. Status: {Status}", GlobalStatus);
        }

        private async Task ProcessPendingTransactionsAsync(CancellationToken token)
        {
            // Original code had this, but orders are now processed synchronously in CreateOrderAsync.
            // This could be repurposed for background tasks like order fulfillment simulation,
            // processing asynchronous payment callbacks, etc.
            if (token.IsCancellationRequested) return;
            _logger.LogTrace("Checking for pending background tasks...");
            // Example: Process orders marked as 'AwaitingShipment'
            // var ordersToShip = await _persistenceService.GetOrdersByStatusAsync(OrderStatus.AwaitingShipment, 10, token);
            // foreach(var order in ordersToShip) { await SimulateShipmentAsync(order, token); }
            await Task.Delay(10, token); // Prevent tight loop if nothing to do
        }

        private async Task AutoRestockShopsTaskAsync(CancellationToken token)
        {
             if (token.IsCancellationRequested) return;
             _logger.LogDebug("Running auto-restock task...");
             foreach (var (shopId, shopState) in _shopStates)
             {
                 if (shopState.Status == ShopInstanceStatus.Open || shopState.Status == ShopInstanceStatus.Closed) // Restock even if closed? Configurable.
                 {
                     // Check if restock needed based on time or inventory levels
                     if (DateTime.UtcNow - shopState.LastRestockTime > _config.AutoRestockCheckInterval * 0.9) // Example time check
                     {
                          await RestockShopAsync(shopId, -1, token); // System initiated restock
                     }
                 }
             }
             _logger.LogDebug("Auto-restock task finished.");
        }

        #endregion

        #region Helper Methods

        private SemaphoreSlim GetShopLock(int shopId)
        {
            // Gets or adds a semaphore for the given shop ID. Should have been added during init.
            return _shopLocks.GetOrAdd(shopId, _ => new SemaphoreSlim(1, 1));
        }

        private async Task<bool> LoadShopAsync(int shopId, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Loading data for Shop {ShopId}...", shopId);
            try
            {
                 // Load from persistence service
                 ShopData data = await _persistenceService.LoadShopDataAsync(shopId, cancellationToken);

                 ShopState state;
                 if (data != null)
                 {
                     state = ConvertFromShopData(shopId, data);
                 }
                 else
                 {
                     // Shop doesn't exist in DB, create new state
                     _logger.LogInformation("No existing data found for Shop {ShopId}. Creating new state.", shopId);
                     state = new ShopState(shopId, _config); // Use global config as base
                     state.Status = ShopInstanceStatus.Closed; // Default to closed initially
                 }

                 _shopStates[shopId] = state; // Replace or add
                 await UpdateCachedReputationScoreAsync(shopId, state); // Initialize cache
                 _logger.LogDebug("Shop {ShopId} data loaded. Status: {Status}", shopId, state.Status);
                 return true;
            }
            catch (Exception ex)
            {
                 _logger.LogError(ex, "Failed to load data for Shop {ShopId}", shopId);
                 // Add a placeholder state indicating error?
                 _shopStates[shopId] = new ShopState(shopId, _config) { Status = ShopInstanceStatus.Locked }; // Error state
                 return false;
            }
        }

        // --- Data Transfer Object (DTO) for Persistence ---
        // Separates the in-memory ShopState (with concurrent collections, etc.)
        // from the data structure used for saving/loading (e.g., to JSON or DB).
        public class ShopData
        {
            public ShopInstanceStatus Status { get; set; }
            public ShopConfiguration Configuration { get; set; } // Persist overrides
            public ShopReputationData Reputation { get; set; }
            public List<ShopItem> Inventory { get; set; } // Simple list for persistence
            public Dictionary<string, List<int>> Categories { get; set; } // Dict is fine for JSON
            public Dictionary<string, List<int>> Tags { get; set; } // Dict is fine for JSON
            public List<LimitedTimeOffer> LimitedOffers { get; set; }
            public List<ReservedItemData> ReservedItems { get; set; }
            public List<ForumPost> ForumPosts { get; set; }
            public List<AuctionData> ActiveAuctions { get; set; }
            public List<ShopSchedule> Schedules { get; set; }
            public List<LoyaltyRecord> LoyaltyRecords { get; set; }
            public List<string> BlockedPlayerIds { get; set; }
            public DateTime LastRestockTime { get; set; }
            public DateTime LastPriceUpdateTime { get; set; }
            public string OwnerPlayerId { get; set; }
        }
        // Add supporting DTOs like ShopReputationData, ReservedItemData, AuctionData if needed

        private ShopData ConvertToShopData(ShopState state)
        {
             // Convert concurrent collections to simple lists/dicts for serialization
             return new ShopData
             {
                 Status = state.Status,
                 Configuration = state.Configuration, // Assumes Configuration is serializable
                 Reputation = ConvertToReputationData(state.Reputation), // Needs converter method
                 Inventory = state.Inventory.Values.ToList(),
                 Categories = state.Categories.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToList()),
                 Tags = state.Tags.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToList()),
                 LimitedOffers = state.LimitedOffers.ToList(),
                 ReservedItems = state.ReservedItems.Select(kvp => new ReservedItemData { ItemId = kvp.Key, PlayerId = kvp.Value.PlayerId, Expiry = kvp.Value.Expiry }).ToList(),
                 ForumPosts = state.ForumPosts.ToList(),
                 ActiveAuctions = state.ActiveAuctions.Values.Select(ConvertToAuctionData).ToList(), // Needs converter
                 Schedules = state.Schedules.ToList(),
                 LoyaltyRecords = state.LoyaltyRecords.ToList(),
                 BlockedPlayerIds = state.BlockedPlayerIds.ToList(),
                 LastRestockTime = state.LastRestockTime,
                 LastPriceUpdateTime = state.LastPriceUpdateTime,
                 OwnerPlayerId = state.OwnerPlayerId
             };
        }

        private ShopState ConvertFromShopData(int shopId, ShopData data)
        {
             var state = new ShopState(shopId, data.Configuration ?? _config) // Use loaded config or global
             {
                 Status = data.Status,
                 Reputation = ConvertFromReputationData(data.Reputation), // Needs converter
                 LimitedOffers = data.LimitedOffers ?? new List<LimitedTimeOffer>(),
                 ForumPosts = data.ForumPosts ?? new List<ForumPost>(),
                 Schedules = data.Schedules ?? new List<ShopSchedule>(),
                 LoyaltyRecords = data.LoyaltyRecords ?? new List<LoyaltyRecord>(),
                 BlockedPlayerIds = data.BlockedPlayerIds ?? new List<string>(),
                 LastRestockTime = data.LastRestockTime,
                 LastPriceUpdateTime = data.LastPriceUpdateTime,
                 OwnerPlayerId = data.OwnerPlayerId
             };

             // Populate concurrent collections
             foreach (var item in data.Inventory ?? Enumerable.Empty<ShopItem>()) state.Inventory.TryAdd(item.Id, item);
             foreach (var kvp in data.Categories ?? new Dictionary<string, List<int>>()) state.Categories.TryAdd(kvp.Key, kvp.Value);
             foreach (var kvp in data.Tags ?? new Dictionary<string, List<int>>()) state.Tags.TryAdd(kvp.Key, new HashSet<int>(kvp.Value));
             foreach (var res in data.ReservedItems ?? Enumerable.Empty<ReservedItemData>()) state.ReservedItems.TryAdd(res.ItemId, (res.PlayerId, res.Expiry));
             foreach (var aucData in data.ActiveAuctions ?? Enumerable.Empty<AuctionData>())
             {
                 var auction = ConvertFromAuctionData(aucData); // Needs converter
                 state.ActiveAuctions.TryAdd(auction.AuctionId, auction);
             }

             return state;
        }
        // Add placeholder DTOs and converters as needed (e.g., for Reputation, Auction)
        public class ReservedItemData { public int ItemId; public int PlayerId; public DateTime Expiry; }
        public class AuctionData { /* Fields matching Auction needed for persistence */ public Guid AuctionId; /*...*/ }
        public class ShopReputationData { /* Fields matching ShopReputation */ public List<PlayerRating> Ratings; /* ... */ }
        private ShopReputationData ConvertToReputationData(ShopReputation rep) => new ShopReputationData { Ratings = rep.GetRatings(true).ToList() /* ... */ };
        private ShopReputation ConvertFromReputationData(ShopReputationData data) { var rep = new ShopReputation(); /* populate rep._ratings etc */ return rep; }
        private AuctionData ConvertToAuctionData(Auction auc) => new AuctionData { AuctionId = auc.AuctionId /* ... */ };
        private Auction ConvertFromAuctionData(AuctionData data) { /* Create Auction from data */ return null; }


        // JSON Serialization Options
        private JsonSerializerOptions GetJsonOptions() => new()
        {
            WriteIndented = true, // For readability in backups
            // ReferenceHandler = ReferenceHandler.Preserve, // If object graph is complex
            // Converters = { ... } // Add custom converters if needed
        };

        // Placeholder for simple constant definition
        private static class Constant { public const int MAX_SHOPS = 100; public const int SUPER_ADMIN_PLAYER_ID = 0; }

        // Simple Result Pattern
        public class ShopOperationResult
        {
             public bool IsSuccess { get; protected set; }
             public string ErrorMessage { get; protected set; }

             public static ShopOperationResult Success(string message = null) => new ShopOperationResult { IsSuccess = true, ErrorMessage = message }; // Optional success message
             public static ShopOperationResult Fail(string errorMessage) => new ShopOperationResult { IsSuccess = false, ErrorMessage = errorMessage };
        }

        public class ShopOperationResult<T> : ShopOperationResult
        {
             public T Value { get; private set; }

             // Private constructor
             private ShopOperationResult() { }

             public static ShopOperationResult<T> Success(T value, string message = null) => new ShopOperationResult<T> { IsSuccess = true, Value = value, ErrorMessage = message };
             public static new ShopOperationResult<T> Fail(string errorMessage) => new ShopOperationResult<T> { IsSuccess = false, ErrorMessage = errorMessage, Value = default };
             public static ShopOperationResult<T> Fail(string errorMessage, T partialValue) => new ShopOperationResult<T> { IsSuccess = false, ErrorMessage = errorMessage, Value = partialValue }; // Include partial data on failure
        }

        // Placeholder for coupon code generation
        private string GenerateCouponCode(int length = 8)
        {
             const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
             return new string(Enumerable.Repeat(chars, length)
               .Select(s => s[RandomNumberGenerator.GetInt32(s.Length)]).ToArray());
        }

        #endregion
    }

    #region Supporting Classes and Enums (Many moved inside Shop or defined above)

    // Keep EventArgs external if used by subscribers outside the Shop class assembly
    public class ShopStatusChangedEventArgs : EventArgs
    {
        public int ShopId { get; }
        public Shop.ShopInstanceStatus NewStatus { get; }
        public Shop.ShopInstanceStatus OldStatus { get; }
        public string Reason { get; }
        public ShopStatusChangedEventArgs(int shopId, Shop.ShopInstanceStatus newStatus, Shop.ShopInstanceStatus oldStatus, string reason)
            => (ShopId, NewStatus, OldStatus, Reason) = (shopId, newStatus, oldStatus, reason);
    }

    public class OrderEventArgs : EventArgs // Replaces ShopTransactionEventArgs
    {
        public Shop.Order Order { get; }
        public OrderEventArgs(Shop.Order order) => Order = order;
    }

    public class ShopReviewEventArgs : EventArgs
    {
        public int ShopId { get; }
        public int PlayerId { get; }
        public float Rating { get; }
        public string Review { get; }
        public bool IsPendingModeration { get; } // Added
        public ShopReviewEventArgs(int shopId, int playerId, float rating, string review, bool isPendingModeration)
            => (ShopId, PlayerId, Rating, Review, IsPendingModeration) = (shopId, playerId, rating, review, isPendingModeration);
    }

    public class AuctionEventArgs : EventArgs
    {
        public Shop.Auction Auction { get; }
        public AuctionEventArgs(Shop.Auction auction) => Auction = auction;
    }

    public class LoyaltyEventArgs : EventArgs
    {
        public int ShopId { get; }
        public int PlayerId { get; }
        public int CurrentPoints { get; }
        public string CurrentTier { get; }
        public int PointsChanged { get; } // Positive for earned, negative for redeemed
        public LoyaltyEventArgs(int shopId, int playerId, int currentPoints, string tier, int pointsChanged)
            => (ShopId, PlayerId, CurrentPoints, CurrentTier, PointsChanged) = (shopId, playerId, currentPoints, tier, pointsChanged);
    }

    public enum WishlistAction { Added, Removed }
    public class WishlistEventArgs : EventArgs
    {
        public int PlayerId { get; }
        public int ItemId { get; }
        public WishlistAction Action { get; }
        public WishlistEventArgs(int playerId, int itemId, WishlistAction action) => (PlayerId, ItemId, Action) = (playerId, itemId, action);
    }

    public class GiftEventArgs : EventArgs
    {
        public Shop.Order Order { get; }
        public int ReceivingPlayerId { get; }
        public string Message { get; }
        public GiftEventArgs(Shop.Order order, int receivingPlayerId, string message) => (Order, ReceivingPlayerId, Message) = (order, receivingPlayerId, message);
    }


    #endregion

    #region Event Classes (for IShopEventBus - make more detailed)

    // --- System Events ---
    public record ShopSystemStatusEvent(Shop.ShopGlobalStatus NewStatus);
    public record AuditEventOccurred(Shop.AuditLogEntry Entry);

    // --- Shop Specific Events ---
    public record ShopStatusChangedEvent(int ShopId, Shop.ShopInstanceStatus NewStatus, Shop.ShopInstanceStatus OldStatus, string Reason);
    public record ShopConfigurationChangedEvent(int ShopId); // Maybe include changed fields?

    // --- Order Events ---
    public record OrderPlacedEvent(Shop.Order Order);
    public record OrderStatusChangedEvent(Shop.Order Order);
    public record OrderRefundedEvent(Shop.Order Order, decimal RefundAmount, string Reason);

    // --- Inventory Events ---
    public record ItemAddedEvent(int ShopId, Shop.ShopItem Item);
    public record ItemUpdatedEvent(int ShopId, Shop.ShopItem Item);
    public record ItemRemovedEvent(int ShopId, int ItemId);
    public record ItemStockChangedEvent(int ShopId, int ItemId, int NewQuantity, int Change);
    public record ShopRestockedEvent(int ShopId, int ItemsRestockedCount);

    // --- Reputation Events ---
    public record ShopRatingEvent(int ShopId, int PlayerId, float Rating, string Review, bool IsPendingModeration);
    public record ShopReviewModeratedEvent(int ShopId, int ReviewerPlayerId, Shop.ModerationOutcome Outcome, string Reason, int ModeratorId);
    public record SellerResponseAddedEvent(int ShopId, int ReviewerPlayerId, string ResponseText);

    // --- Auction Events ---
    public record AuctionStartedEvent(Shop.Auction Auction);
    public record AuctionBidEvent(Shop.Auction Auction, int BidderPlayerId, decimal BidAmount);
    public record AuctionOutbidEvent(Shop.Auction Auction, int OutbidPlayerId); // Specific event for outbid notification
    public record AuctionEndedEvent(Shop.Auction Auction); // Generic end
    public record AuctionSoldEvent(Shop.Auction Auction); // Specific end: Sold
    public record AuctionFailedReserveEvent(Shop.Auction Auction); // Specific end: Reserve not met

    // --- Loyalty Events ---
    public record LoyaltyUpdateEvent(int ShopId, int PlayerId, int CurrentPoints, string CurrentTier, int PointsChanged);
    public record LoyaltyPointsRedeemedEvent(int ShopId, int PlayerId, int PointsRedeemed, string RewardDescription);
    public record LoyaltyTierChangedEvent(int ShopId, int PlayerId, string NewTier, string OldTier);

    // --- Social Events ---
    public record WishlistUpdatedEvent(int PlayerId, int ItemId, WishlistAction Action);
    public record ItemGiftedEvent(Shop.Order Order, int ReceivingPlayerId, string Message);
    public record ForumPostEvent(int ShopId, int PlayerId, string Content); // Assuming from original

    // --- Promotion Events ---
    public record CouponCreatedEvent(Shop.Coupon Coupon);
    public record CouponRedeemedEvent(string CouponCode, Guid OrderId, int PlayerId);

    // --- Security Events ---
    public record UserBannedEvent(int PlayerId, string Reason, int AdminId);
    public record UserRoleChangedEvent(int PlayerId, string Role, bool WasAdded, int AdminId);


    #endregion
}
