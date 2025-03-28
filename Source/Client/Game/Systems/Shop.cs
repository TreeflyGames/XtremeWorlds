#region Usings
// --- System ---
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // For validation attributes
using System.Diagnostics; // For DebuggerStepThrough
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization; // For ReferenceHandler, Converters
using System.Threading;
using System.Threading.Tasks;

// --- Third Party / Framework ---
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

// --- Project Specific ---
using Core; // Assuming this namespace contains base types like IEntity
#endregion

namespace Client.EnhancedShop
{
    #region Enhanced Interfaces (for DI and Testability)

    // (Existing interfaces like IShopEventBus, IPaymentGatewayService, etc., remain)

    public interface IShippingService // NEW: Shipping calculation and fulfillment
    {
        Task<ShippingQuoteResult> GetShippingQuotesAsync(Address destination, IEnumerable<CartItemInfo> items, CancellationToken cancellationToken = default);
        Task<LabelGenerationResult> GenerateShippingLabelAsync(Order order, ShippingMethod selectedMethod, CancellationToken cancellationToken = default);
        Task<TrackingUpdateResult> GetTrackingUpdateAsync(string trackingNumber, string carrier, CancellationToken cancellationToken = default);
    }

    public interface ITaxCalculationService // NEW: External tax calculation
    {
        Task<TaxCalculationResult> CalculateTaxesAsync(Address destination, Address origin, IEnumerable<CartItemInfo> items, string customerTaxId = null, CancellationToken cancellationToken = default);
    }

    public interface IFraudDetectionService // NEW: AI-based fraud check
    {
        Task<FraudCheckResult> AnalyzeTransactionAsync(Order order, UserContext userContext, CancellationToken cancellationToken = default);
    }

    public interface ISubscriptionService // NEW: Handle recurring orders
    {
        Task<Subscription> CreateSubscriptionAsync(CreateSubscriptionRequest request, CancellationToken cancellationToken = default);
        Task ProcessSubscriptionBillingAsync(Guid subscriptionId, CancellationToken cancellationToken = default);
        Task CancelSubscriptionAsync(Guid subscriptionId, int performingPlayerId, CancellationToken cancellationToken = default);
        // ... other subscription management methods ...
    }

    public interface IContentModerationService // NEW: AI for review/forum content check
    {
        Task<ModerationResult> CheckContentAsync(string text, ContentType type, CancellationToken cancellationToken = default);
    }

    public interface ICartPersistenceService // NEW: Persist shopping carts
    {
        Task<ShoppingCart> GetCartAsync(int playerId, CancellationToken cancellationToken = default);
        Task SaveCartAsync(ShoppingCart cart, CancellationToken cancellationToken = default);
        Task DeleteCartAsync(int playerId, CancellationToken cancellationToken = default);
    }

    public interface IUserNotificationService // NEW: Sending emails, push notifications etc.
    {
        Task SendNotificationAsync(int playerId, NotificationType type, Dictionary<string, string> parameters, CancellationToken cancellationToken = default);
    }


    #endregion

    // Main Shop Service Class
    public class Shop : IDisposable
    {
        #region Fields and Properties

        // --- Dependencies ---
        private readonly IMemoryCache _cache;
        private readonly ILogger<Shop> _logger;
        private readonly IShopEventBus _eventBus;
        private readonly IOptionsMonitor<ShopConfiguration> _configMonitor;
        private readonly IPaymentGatewayService _paymentGateway;
        private readonly IInventoryNotifier _inventoryNotifier;
        private readonly IRecommendationEngine _recommendationEngine;
        private readonly IShopPersistenceService _persistenceService;
        private readonly IShippingService _shippingService; // NEW
        private readonly ITaxCalculationService _taxService; // NEW
        private readonly IFraudDetectionService _fraudService; // NEW
        private readonly ISubscriptionService _subscriptionService; // NEW
        private readonly IContentModerationService _contentModerationService; // NEW
        private readonly ICartPersistenceService _cartPersistenceService; // NEW
        private readonly IUserNotificationService _userNotificationService; // NEW
        private ShopConfiguration _config; // Populated via IOptionsMonitor

        // --- Concurrency & State ---
        private readonly SemaphoreSlim _globalConfigLock = new(1, 1);
        private readonly ConcurrentDictionary<int, SemaphoreSlim> _shopLocks = new();
        private readonly ConcurrentDictionary<Guid, SemaphoreSlim> _orderLocks = new(); // NEW: Per-order locks for status updates/refunds
        private readonly ConcurrentDictionary<Guid, SemaphoreSlim> _auctionLocks = new(); // NEW: Per-auction locks
        private readonly SemaphoreSlim _operationThrottle;
        private readonly CancellationTokenSource _cts = new();
        private readonly ConcurrentDictionary<int, ShopState> _shopStates = new(); // Holds all data per shop
        private readonly ConcurrentDictionary<string, Coupon> _activeCoupons = new();
        private readonly ConcurrentDictionary<int, UserSession> _userSessions = new(); // Enhanced session tracking
        private readonly ConcurrentDictionary<int, ShoppingCart> _activeCarts = new(); // NEW: In-memory carts (backed by persistence)
        private readonly ConcurrentDictionary<Guid, Subscription> _activeSubscriptions = new(); // NEW

        // --- Background Task Timers ---
        private Timer _healthMonitorTimer;
        private Timer _abandonedCartTimer; // NEW
        private Timer _autoRestockTimer;
        private Timer _auctionProcessingTimer;
        private Timer _recommendationModelUpdateTimer;
        private Timer _scheduledSalesTimer; // NEW
        private Timer _subscriptionBillingTimer; // NEW
        private Timer _statusUpdateFromScheduleTimer; // NEW
        private Timer _dataArchivalTimer; // NEW

        // --- Public Accessors ---
        public IReadOnlyDictionary<int, ShopState> ShopStates => _shopStates;
        public ShopGlobalStatus GlobalStatus { get; private set; } = ShopGlobalStatus.Initializing;

        // --- Events ---
        // (Existing events remain: ShopStatusChanged, OrderPlaced, OrderStatusChanged, ReviewAdded, ReviewModerated, AuctionUpdated, AuctionEnded, LoyaltyPointsUpdated, WishlistUpdated, ItemGifted)
        public event EventHandler<CartEventArgs> CartUpdated; // NEW
        public event EventHandler<CartEventArgs> CartAbandonedWarning; // NEW
        public event EventHandler<SubscriptionEventArgs> SubscriptionStatusChanged; // NEW
        public event EventHandler<NotificationEventArgs> UserNotificationSent; // NEW
        public event EventHandler<SystemHealthEventArgs> SystemHealthChanged; // NEW
        public event EventHandler<FeatureFlagEventArgs> FeatureFlagToggled; // NEW

        #endregion

        #region Constructor and Initialization

        public Shop(
            IMemoryCache cache,
            ILogger<Shop> logger,
            IShopEventBus eventBus,
            IOptionsMonitor<ShopConfiguration> configMonitor,
            IPaymentGatewayService paymentGateway,
            IInventoryNotifier inventoryNotifier,
            IRecommendationEngine recommendationEngine,
            IShopPersistenceService persistenceService,
            // --- NEW Dependencies ---
            IShippingService shippingService,
            ITaxCalculationService taxService,
            IFraudDetectionService fraudService,
            ISubscriptionService subscriptionService,
            IContentModerationService contentModerationService,
            ICartPersistenceService cartPersistenceService,
            IUserNotificationService userNotificationService)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _configMonitor = configMonitor ?? throw new ArgumentNullException(nameof(configMonitor));
            _paymentGateway = paymentGateway ?? throw new ArgumentNullException(nameof(paymentGateway));
            _inventoryNotifier = inventoryNotifier ?? throw new ArgumentNullException(nameof(inventoryNotifier));
            _recommendationEngine = recommendationEngine ?? throw new ArgumentNullException(nameof(recommendationEngine));
            _persistenceService = persistenceService ?? throw new ArgumentNullException(nameof(persistenceService));
            _shippingService = shippingService ?? throw new ArgumentNullException(nameof(shippingService));
            _taxService = taxService ?? throw new ArgumentNullException(nameof(taxService));
            _fraudService = fraudService ?? throw new ArgumentNullException(nameof(fraudService));
            _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
            _contentModerationService = contentModerationService ?? throw new ArgumentNullException(nameof(contentModerationService));
            _cartPersistenceService = cartPersistenceService ?? throw new ArgumentNullException(nameof(cartPersistenceService));
            _userNotificationService = userNotificationService ?? throw new ArgumentNullException(nameof(userNotificationService));

            // Subscribe to configuration changes
            _configMonitor.OnChange(UpdateConfiguration);
            _config = _configMonitor.CurrentValue ?? new ShopConfiguration(); // Initial config load

            // Validate initial config
            if (!ValidateConfiguration(_config))
            {
                throw new InvalidOperationException("Initial shop configuration is invalid.");
            }

            _operationThrottle = new SemaphoreSlim(_config.MaxConcurrentOperations, _config.MaxConcurrentOperations);

            _logger.LogInformation("Enhanced Shop system initializing with extended features...");
            _ = InitializeSystemAsync(_cts.Token); // Fire-and-forget initialization
        }

        private async Task InitializeSystemAsync(CancellationToken cancellationToken)
        {
            try
            {
                SetGlobalStatus(ShopGlobalStatus.LoadingData);

                // --- Load Global Data ---
                await LoadGlobalCouponsAsync(cancellationToken);
                await LoadFeatureFlagStatesAsync(cancellationToken); // NEW

                // --- Load Shop Instances ---
                // Simulate loading shop IDs (replace with actual discovery/DB query)
                var shopIdsToLoad = await _persistenceService.GetAllShopIdsAsync(cancellationToken)
                                    ?? Enumerable.Range(0, Constant.MAX_SHOPS);

                var loadTasks = shopIdsToLoad.Select(shopId => LoadShopAsync(shopId, cancellationToken)).ToList();
                await Task.WhenAll(loadTasks);

                // Initialize locks for successfully loaded shops AFTER loading
                foreach(var shopId in _shopStates.Keys)
                {
                     _shopLocks.TryAdd(shopId, new SemaphoreSlim(1, 1));
                     // Pre-warm caches if needed
                     _ = GetShopInventoryAsync(shopId, cancellationToken: cancellationToken); // Example pre-warm
                }

                // --- Load Subscriptions ---
                await LoadActiveSubscriptionsAsync(cancellationToken); // NEW

                StartBackgroundTasks();
                SetGlobalStatus(ShopGlobalStatus.Online);
                _logger.LogInformation("Enhanced Shop system initialized and online.");
            }
            catch (OperationCanceledException)
            {
                 _logger.LogWarning("Shop system initialization cancelled.");
                 SetGlobalStatus(ShopGlobalStatus.ShuttingDown); // Or Error
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "CRITICAL FAILURE during Shop system initialization.");
                SetGlobalStatus(ShopGlobalStatus.Error);
                // Consider triggering alerts
                await NotifyAdminOfFailureAsync("Initialization Failure", ex.ToString(), CancellationToken.None);
            }
        }

        private void UpdateConfiguration(ShopConfiguration newConfig, string _)
        {
            _logger.LogInformation("Shop configuration reloading...");
            if (!ValidateConfiguration(newConfig))
            {
                 _logger.LogError("Reloaded shop configuration is invalid. Changes rejected.");
                 return;
            }

            _globalConfigLock.Wait();
            try
            {
                var oldConfig = _config;
                _config = newConfig ?? new ShopConfiguration();

                // --- Adjust dynamic components based on config changes ---
                // Resize throttle semaphore (handle carefully)
                // Update background timer intervals (requires disposing and recreating timers)
                RestartBackgroundTasksIfNeeded(oldConfig, _config); // NEW method

                _logger.LogInformation("Shop configuration reloaded successfully.");
                // Publish event about config change
                 _ = _eventBus.PublishAsync(new GlobalConfigChangedEvent(_config), CancellationToken.None);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to reload shop configuration.");
                 // Rollback or alert admin?
            }
            finally
            {
                _globalConfigLock.Release();
            }
        }

        // Validate configuration settings
        private bool ValidateConfiguration(ShopConfiguration config)
        {
            if (config == null) return false;
            var context = new ValidationContext(config, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(config, context, results, validateAllProperties: true);

            if (!isValid)
            {
                foreach (var validationResult in results)
                {
                    _logger.LogError("Configuration Error: {ErrorMessage} for members: {MemberNames}",
                        validationResult.ErrorMessage, string.Join(", ", validationResult.MemberNames));
                }
            }
            // Add custom cross-field validation if needed
            if(config.CacheDurationShort >= config.CacheDurationMedium)
            {
                _logger.LogError("Configuration Error: CacheDurationShort must be less than CacheDurationMedium.");
                isValid = false;
            }

            return isValid;
        }


        public void Dispose()
        {
            _logger.LogInformation("Enhanced Shop system shutting down...");
            SetGlobalStatus(ShopGlobalStatus.ShuttingDown);

            // Signal cancellation to background tasks and pending operations
            _cts.Cancel();

            // Dispose timers safely
            _healthMonitorTimer?.Dispose();
            _abandonedCartTimer?.Dispose();
            _autoRestockTimer?.Dispose();
            _auctionProcessingTimer?.Dispose();
            _recommendationModelUpdateTimer?.Dispose();
            _scheduledSalesTimer?.Dispose();
            _subscriptionBillingTimer?.Dispose();
            _statusUpdateFromScheduleTimer?.Dispose();
            _dataArchivalTimer?.Dispose();

            // Dispose semaphores (allow pending operations to finish slightly?)
            // Consider a brief wait or more sophisticated shutdown sequence
            _globalConfigLock.Dispose();
            _operationThrottle.Dispose();
            foreach (var kvp in _shopLocks) kvp.Value.Dispose();
            foreach (var kvp in _orderLocks) kvp.Value.Dispose();
            foreach (var kvp in _auctionLocks) kvp.Value.Dispose();
            _cts.Dispose();

            _logger.LogInformation("Enhanced Shop system shut down complete.");
        }

        #endregion

        #region Enhanced State Management & Status

        public enum ShopGlobalStatus
        {
            Initializing, LoadingData, Online, Degraded, Maintenance, ShuttingDown, Error
        }

        public enum ShopInstanceStatus
        {
            Open, Closed, ScheduledClosure, Maintenance, Initializing, Locked, Archived, Deleted // NEW: Deleted
        }

        // Holds all data and state for a single shop instance
        public class ShopState
        {
            public int ShopId { get; }
            public ShopInstanceStatus Status { get; set; } = ShopInstanceStatus.Initializing;
            public ShopConfiguration Configuration { get; set; } // Per-shop config overrides global
            public ShopReputation Reputation { get; set; } = new();
            public ConcurrentDictionary<int, ShopItem> Inventory { get; } = new();
            public ConcurrentDictionary<string, List<int>> Categories { get; } = new();
            public ConcurrentDictionary<string, HashSet<int>> Tags { get; } = new();
            public List<LimitedTimeOffer> LimitedOffers { get; } = new();
            public List<ScheduledSale> ScheduledSales { get; } = new(); // NEW
            public ConcurrentDictionary<int, (int PlayerId, DateTime Expiry)> ReservedItems { get; } = new();
            public List<ForumPost> ForumPosts { get; } = new(); // Consider moving to separate ForumService
            public ConcurrentDictionary<Guid, Auction> ActiveAuctions { get; } = new();
            public List<ShopSchedule> Schedules { get; } = new();
            public List<LoyaltyRecord> LoyaltyRecords { get; } = new();
            public List<string> BlockedPlayerIds { get; } = new();
            public DateTime LastRestockTime { get; set; } = DateTime.MinValue;
            public DateTime LastPriceUpdateTime { get; set; } = DateTime.MinValue;
            public string OwnerPlayerId { get; set; }
            public DateTime DateCreated { get; set; } = DateTime.UtcNow; // NEW
            public Dictionary<string, string> CustomFields { get; set; } = new(); // NEW: For extensibility

            public ShopState(int shopId, ShopConfiguration initialConfig)
            {
                ShopId = shopId;
                Configuration = initialConfig; // Can be overridden later
            }
        }

        // Central configuration with Validation attributes
        public class ShopConfiguration
        {
            [Range(0.0, 1.0)] public decimal DefaultTaxRate { get; set; } = 0.05m;
            [Range(1, 1000)] public int DefaultMaxItemsPerOrder { get; set; } = 50;
            public bool EnableDynamicPricingGlobally { get; set; } = true;
            [Required][MaxLength(10)] public string DefaultCurrency { get; set; } = "Gold";
            public bool AllowPlayerTradesGlobally { get; set; } = false;
            [Range(1, 10000)] public int MaxConcurrentOperations { get; set; } = 100; // Increased capacity
            [Range(typeof(TimeSpan), "00:00:10", "01:00:00")] public TimeSpan DefaultReservationTime { get; set; } = TimeSpan.FromMinutes(15);
            [Range(typeof(TimeSpan), "00:00:05", "00:30:00")] public TimeSpan CacheDurationShort { get; set; } = TimeSpan.FromMinutes(1); // Reduced default
            [Range(typeof(TimeSpan), "00:01:00", "02:00:00")] public TimeSpan CacheDurationMedium { get; set; } = TimeSpan.FromMinutes(15);
            [Range(typeof(TimeSpan), "00:10:00", "12:00:00")] public TimeSpan CacheDurationLong { get; set; } = TimeSpan.FromHours(2);
            [Range(1, 10)] public int MaxReviewsPerPlayerPerShop { get; set; } = 1;
            [Range(0.1f, 0.9f)] public float ReputationWeightRecent { get; set; } = 0.7f;
            [Range(typeof(TimeSpan), "00:00:10", "00:10:00")] public TimeSpan AuctionEndTimeExtension { get; set; } = TimeSpan.FromMinutes(1);
            [Range(0, 1000)] public int PointsPerCurrencyUnitSpent { get; set; } = 1;
            public List<LoyaltyTier> LoyaltyTiers { get; set; } = GetDefaultLoyaltyTiers();
            [Range(5, 100)] public int MaxWishlistItems { get; set; } = 20;
            public bool Require2FAForHighValueTrades { get; set; } = true;
            [Range(100, 100000)] public decimal HighValueTradeThreshold { get; set; } = 1000m; // NEW: Threshold for 2FA
            public string AuditLogStoragePath { get; set; } = "/var/log/shop/audit"; // NEW: More specific path/type
            public AuditLogStorageType AuditLogType { get; set; } = AuditLogStorageType.File; // NEW: File, Database, EventStream
            [Range(10, 10000)] public int MaxInventorySize { get; set; } = 1000;
            [Range(typeof(TimeSpan), "00:05:00", "24:00:00")] public TimeSpan AutoRestockCheckInterval { get; set; } = TimeSpan.FromHours(1);
            [Range(typeof(TimeSpan), "00:01:00", "01:00:00")] public TimeSpan HealthCheckInterval { get; set; } = TimeSpan.FromMinutes(5);
            [Range(typeof(TimeSpan), "00:00:01", "00:05:00")] public TimeSpan AuctionProcessingInterval { get; set; } = TimeSpan.FromSeconds(10);
            [Range(typeof(TimeSpan), "00:15:00", "48:00:00")] public TimeSpan RecommendationModelUpdateInterval { get; set; } = TimeSpan.FromHours(6);
            [Range(typeof(TimeSpan), "00:05:00", "24:00:00")] public TimeSpan AbandonedCartCheckInterval { get; set; } = TimeSpan.FromHours(2); // NEW
            [Range(typeof(TimeSpan), "00:00:30", "01:00:00")] public TimeSpan AbandonedCartThreshold { get; set; } = TimeSpan.FromMinutes(90); // NEW
            [Range(typeof(TimeSpan), "00:05:00", "24:00:00")] public TimeSpan SubscriptionBillingInterval { get; set; } = TimeSpan.FromHours(4); // NEW
            [Range(typeof(TimeSpan), "01:00:00", "7.00:00:00")] public TimeSpan DataArchivalInterval { get; set; } = TimeSpan.FromDays(1); // NEW
            [Range(typeof(TimeSpan), "30.00:00:00", "3650.00:00:00")] public TimeSpan OrderArchivalAge { get; set; } = TimeSpan.FromDays(180); // NEW
            public Dictionary<string, bool> FeatureFlags { get; set; } = new(); // NEW: Dynamic feature toggles

            private static List<LoyaltyTier> GetDefaultLoyaltyTiers() => new()
            {
                new LoyaltyTier("Bronze", 0, 0, 0, TimeSpan.Zero), // Added Expiry
                new LoyaltyTier("Silver", 1000, 0.02m, 5, TimeSpan.FromDays(365)),
                new LoyaltyTier("Gold", 5000, 0.05m, 10, TimeSpan.FromDays(365)),
                new LoyaltyTier("Platinum", 20000, 0.10m, 15, TimeSpan.Zero) // Platinum never expires?
            };
        }
        public enum AuditLogStorageType { File, Database, EventStream } // NEW

        // Represents a user's session, potentially loaded on demand
        public class UserSession
        {
            public int PlayerId { get; }
            public HashSet<string> Roles { get; set; } = new();
            public DateTime LastActivity { get; set; }
            public DateTime SessionStartTime { get; }
            public string SessionToken { get; private set; } // Can be refreshed
            public string IpAddress { get; set; } // NEW
            public string UserAgent { get; set; } // NEW
            // Add methods: HasRole, HasPermission, RefreshToken

            public UserSession(int playerId)
            {
                PlayerId = playerId;
                var now = DateTime.UtcNow;
                SessionStartTime = now;
                LastActivity = now;
                SessionToken = GenerateSecureToken();
            }

            public bool IsExpired(TimeSpan timeout) => DateTime.UtcNow - LastActivity > timeout;
            public void RefreshToken() => SessionToken = GenerateSecureToken();
        }

        // Method to update global status and publish event
        private void SetGlobalStatus(ShopGlobalStatus newStatus, string reason = null)
        {
            var oldStatus = GlobalStatus;
            if (oldStatus != newStatus)
            {
                GlobalStatus = newStatus;
                _logger.LogWarning("Shop System Global Status changed from {OldStatus} to {NewStatus}. Reason: {Reason}", oldStatus, newStatus, reason ?? "N/A");
                // Publish system event
                _ = _eventBus.PublishAsync(new ShopSystemStatusEvent(newStatus), CancellationToken.None);
                SystemHealthChanged?.Invoke(this, new SystemHealthEventArgs(newStatus, oldStatus, reason));
            }
        }

        private async Task UpdateShopStatusAsync(int shopId, ShopInstanceStatus newStatus, string reason = null, CancellationToken cancellationToken = default)
        {
            if (_shopStates.TryGetValue(shopId, out var state))
            {
                var oldStatus = state.Status;
                if (oldStatus != newStatus)
                {
                    // Acquire lock for status change consistency
                    await GetShopLock(shopId).WaitAsync(cancellationToken);
                    try
                    {
                         // Re-check status inside lock
                        if (state.Status != oldStatus) return;

                        state.Status = newStatus;
                        state.LastUpdated = DateTime.UtcNow; // Update last updated timestamp on ShopItem

                        // --- Persistence ---
                        // Opt: Only save the status field if persistence supports partial updates
                        // await _persistenceService.UpdateShopStatusAsync(shopId, newStatus, cancellationToken);
                        // Full save for now:
                        await _persistenceService.SaveShopDataAsync(shopId, ConvertToShopData(state), cancellationToken);

                        // --- Events & Logging ---
                        ShopStatusChanged?.Invoke(this, new ShopStatusChangedEventArgs(shopId, newStatus, oldStatus, reason));
                        await _eventBus.PublishAsync(new ShopStatusChangedEvent(shopId, newStatus, oldStatus, reason), cancellationToken);
                        await LogAuditEventAsync(shopId, -1, "System", AuditAction.StatusChange, $"Status changed from {oldStatus} to {newStatus}. Reason: {reason ?? "N/A"}", cancellationToken);
                        _logger.LogInformation("Shop {ShopId} status updated from {OldStatus} to {NewStatus}. Reason: {Reason}", shopId, oldStatus, newStatus, reason ?? "N/A");
                    }
                    catch(Exception ex)
                    {
                         _logger.LogError(ex, "Failed to update status for Shop {ShopId}", shopId);
                         // Should we attempt to revert the in-memory status?
                    }
                    finally
                    {
                        GetShopLock(shopId).Release();
                    }
                }
            }
            else
            {
                _logger.LogWarning("Attempted to update status for non-existent shop {ShopId}", shopId);
            }
        }

        // --- Simplified Status Update Methods ---
        public Task OpenShopAsync(int shopId, CancellationToken cancellationToken = default) => UpdateShopStatusAsync(shopId, ShopInstanceStatus.Open, cancellationToken: cancellationToken);
        public Task CloseShopAsync(int shopId, string reason = "Manual Closure", CancellationToken cancellationToken = default) => UpdateShopStatusAsync(shopId, ShopInstanceStatus.Closed, reason, cancellationToken);
        public Task SetMaintenanceModeAsync(int shopId, bool isMaintenance, string reason = null, CancellationToken cancellationToken = default) => UpdateShopStatusAsync(shopId, isMaintenance ? ShopInstanceStatus.Maintenance : ShopInstanceStatus.Open, reason ?? (isMaintenance ? "Entering Maintenance" : "Exiting Maintenance"), cancellationToken);
        public Task LockShopAsync(int shopId, string reason, CancellationToken cancellationToken = default) => UpdateShopStatusAsync(shopId, ShopInstanceStatus.Locked, reason, cancellationToken);
        public Task ArchiveShopAsync(int shopId, string reason = "Archived by Admin", CancellationToken cancellationToken = default) => UpdateShopStatusAsync(shopId, ShopInstanceStatus.Archived, reason, cancellationToken);
        // NEW: Method to permanently delete shop data (use with extreme caution!)
        public async Task DeleteShopAsync(int shopId, int performingPlayerId, CancellationToken cancellationToken = default)
        {
             if (!await CheckPermissionAsync(performingPlayerId, "System_DeleteShop", cancellationToken)) return; // Needs strict permission
             if (!_shopStates.TryGetValue(shopId, out _)) return; // Already gone?

             _logger.LogCritical("Initiating PERMANENT DELETION of Shop {ShopId} by Player {PlayerId}", shopId, performingPlayerId);
             // 1. Set status to Deleting/Archived first
             await UpdateShopStatusAsync(shopId, ShopInstanceStatus.Deleted, $"Deletion initiated by {performingPlayerId}", cancellationToken);
             // 2. Remove from in-memory state
             _shopStates.TryRemove(shopId, out _);
             _shopLocks.TryRemove(shopId, out var lockToDispose);
             lockToDispose?.Dispose();
             // 3. Trigger persistent deletion (async) - might involve deleting related orders, etc.
             await _persistenceService.DeleteShopAsync(shopId, cancellationToken); // Ensure this deletes related data!
             // 4. Audit
             await LogAuditEventAsync(shopId, performingPlayerId, "Admin", AuditAction.ShopDelete, "Shop permanently deleted.", cancellationToken);
             _logger.LogCritical("Shop {ShopId} PERMANENTLY DELETED.", shopId);
        }


        // Update per-shop configuration overrides
        public async Task<ShopOperationResult> UpdateShopConfigurationAsync(int shopId, Action<ShopConfiguration> configure, int performingPlayerId, CancellationToken cancellationToken = default)
        {
             // (Implementation similar to previous version, ensures permissions, locks, persists)
             // ... ensure validation runs on the newShopConfig ...
             if (!ValidateConfiguration(newShopConfig))
             {
                   return ShopOperationResult.Fail("Invalid configuration provided.");
             }
             // ... (rest of the logic: lock, apply, save, audit, log) ...
        }

        #endregion

        // --- Sections for other massively added features would follow a similar pattern ---
        // #region Enhanced Inventory Management (Bundles, Digital Goods, Bulk Import/Export)
        // #region Advanced Pricing & Promotions (Tiered Pricing, BOGO, Scheduled Sales)
        // #region Enhanced Order Management (Partial Shipments, Returns/RMAs, Subscriptions, Abandoned Carts)
        // #region Advanced Payment & Checkout (Multi-Gateway, Saved Methods, Tax Integration)
        // #region Shipping & Fulfillment Integration (Rate Calc, Labels, Tracking)
        // #region Enhanced User/Player Management (Profiles, Addresses, GDPR)
        // #region Expanded Social & Community Features (Q&A, Following, Sharing)
        // #region Advanced AI & Machine Learning Hooks (Demand Forecasting, Segmentation, Chatbots)
        // #region Enhanced Admin & Operations (Dashboard Data API, Reporting Hooks, CMS Hooks, Feature Flags API)
        // #region Advanced Security (Granular Perms, API Keys, Rate Limiting)
        // #region Data Management & Persistence Enhancements (Archival, CDC Hooks)

        // Placeholder implementations for brevity. A full implementation would be thousands of lines.

        #region Example Expansion: Abandoned Cart Recovery

        private async Task CheckAbandonedCartsAsync(object state) // Called by timer
        {
            var cancellationToken = (CancellationToken)state;
             _logger.LogDebug("Checking for abandoned carts...");
             var cutoffTime = DateTime.UtcNow - _config.AbandonedCartThreshold;
             var abandonedCartCandidates = _activeCarts.Values // Or load from persistence
                 .Where(c => c.LastUpdated < cutoffTime && c.Items.Any() && !c.RecoveryAttempted)
                 .ToList();

             if (!abandonedCartCandidates.Any()) return;

             _logger.LogInformation("Found {Count} potential abandoned carts to process.", abandonedCartCandidates.Count);

             foreach (var cart in abandonedCartCandidates)
             {
                 if (cancellationToken.IsCancellationRequested) return;
                 try
                 {
                     _logger.LogInformation("Sending abandoned cart reminder for Player {PlayerId}", cart.PlayerId);
                     // Mark attempt to prevent re-sending immediately
                     cart.RecoveryAttempted = true; // Needs persistence update

                     // Trigger notification
                     await _userNotificationService.SendNotificationAsync(
                         cart.PlayerId,
                         NotificationType.AbandonedCart,
                         new Dictionary<string, string> {
                             { "CartItemCount", cart.Items.Count.ToString() },
                             { "FirstItemName", cart.Items.First().ItemNameSnapshot },
                             // Add link back to cart etc.
                         },
                         cancellationToken);

                     CartAbandonedWarning?.Invoke(this, new CartEventArgs(cart));
                     await _eventBus.PublishAsync(new CartAbandonedEvent(cart), cancellationToken);
                     await LogAuditEventAsync(null, cart.PlayerId, "Cart", AuditAction.AbandonedCartReminder, $"Sent reminder for cart with {cart.Items.Count} items.", cancellationToken);

                     // Update cart in persistence
                     await _cartPersistenceService.SaveCartAsync(cart, cancellationToken);
                 }
                 catch (Exception ex)
                 {
                      _logger.LogError(ex, "Failed to process abandoned cart reminder for Player {PlayerId}", cart.PlayerId);
                 }
             }
        }

        #endregion

        #region Example Expansion: Feature Flags

        private ConcurrentDictionary<string, bool> _featureFlags = new(); // Loaded during init

        private async Task LoadFeatureFlagStatesAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug("Loading feature flags...");
            _featureFlags = new ConcurrentDictionary<string, bool>(
                await _persistenceService.LoadFeatureFlagsAsync(cancellationToken) ?? _config.FeatureFlags ?? new Dictionary<string, bool>()
            );
             _logger.LogInformation("Loaded {Count} feature flags.", _featureFlags.Count);
        }

        public bool IsFeatureEnabled(string featureKey)
        {
            return _featureFlags.TryGetValue(featureKey, out var isEnabled) && isEnabled;
        }

        public async Task<ShopOperationResult> SetFeatureFlagAsync(string featureKey, bool isEnabled, int performingPlayerId, CancellationToken cancellationToken = default)
        {
             if (!await CheckPermissionAsync(performingPlayerId, "Admin_ManageFeatureFlags", cancellationToken))
                 return ShopOperationResult.Fail("Permission denied.");

             var oldValue = IsFeatureEnabled(featureKey); // Get current value before update
             _featureFlags[featureKey] = isEnabled; // Add or update

             // Persist the change
             await _persistenceService.SaveFeatureFlagAsync(featureKey, isEnabled, cancellationToken);

             await LogAuditEventAsync(null, performingPlayerId, "Admin", AuditAction.FeatureToggle, $"Feature '{featureKey}' set to {isEnabled}.", cancellationToken);
             FeatureFlagToggled?.Invoke(this, new FeatureFlagEventArgs(featureKey, isEnabled, oldValue));
             await _eventBus.PublishAsync(new FeatureFlagUpdatedEvent(featureKey, isEnabled), cancellationToken);
             _logger.LogInformation("Feature flag '{FeatureKey}' set to {IsEnabled} by Player {PlayerId}", featureKey, isEnabled, performingPlayerId);

             return ShopOperationResult.Success($"Feature '{featureKey}' set to {isEnabled}.");
        }

        #endregion

        #region Background Operations Setup (Enhanced)

        private void StartBackgroundTasks()
        {
            _logger.LogInformation("Starting enhanced background tasks...");
            Stopwatch sw = Stopwatch.StartNew();

            // Dispose existing timers before creating new ones (needed for config reload)
            DisposeTimers();

            // Helper to create timers robustly
            Timer CreateTimer(Func<object, Task> callback, TimeSpan dueTime, TimeSpan period, string taskName)
            {
                if (period <= TimeSpan.Zero)
                {
                    _logger.LogWarning("Background task '{TaskName}' disabled due to zero or negative period in config.", taskName);
                    return null;
                }
                 // Add jitter to dueTime to spread load? TimeSpan.FromMilliseconds(RandomNumberGenerator.GetInt32(1000, 5000))
                _logger.LogDebug("Starting background task '{TaskName}'. Due: {DueTime}, Period: {Period}", taskName, dueTime, period);
                 // Use a robust wrapper around the callback to handle exceptions
                 return new Timer(async state => {
                     var token = (CancellationToken)state;
                     if (token.IsCancellationRequested) return;
                     try
                     {
                        _logger.LogTrace("Executing background task: {TaskName}", taskName);
                        await callback(token);
                        _logger.LogTrace("Finished background task: {TaskName}", taskName);
                     }
                     catch (OperationCanceledException) { /* Expected during shutdown */ }
                     catch (Exception ex)
                     {
                         _logger.LogError(ex, "Unhandled exception in background task: {TaskName}", taskName);
                     }
                 }, _cts.Token, dueTime, period);
            }

            _healthMonitorTimer = CreateTimer(MonitorHealthAsync, TimeSpan.FromSeconds(30), _config.HealthCheckInterval, "HealthMonitor");
            _abandonedCartTimer = CreateTimer(CheckAbandonedCartsAsync, TimeSpan.FromMinutes(2), _config.AbandonedCartCheckInterval, "AbandonedCart"); // NEW
            _autoRestockTimer = CreateTimer(AutoRestockShopsTaskAsync, TimeSpan.FromMinutes(1), _config.AutoRestockCheckInterval, "AutoRestock");
            _auctionProcessingTimer = CreateTimer(ProcessEndedAuctionsAsync, TimeSpan.FromSeconds(15), _config.AuctionProcessingInterval, "AuctionProcessing");
            _recommendationModelUpdateTimer = CreateTimer(UpdateRecommendationModelAsync, TimeSpan.FromMinutes(5), _config.RecommendationModelUpdateInterval, "RecommendationModelUpdate");
            _scheduledSalesTimer = CreateTimer(ProcessScheduledSalesAsync, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(15), "ScheduledSales"); // NEW
            _subscriptionBillingTimer = CreateTimer(ProcessSubscriptionBillingCycleAsync, TimeSpan.FromMinutes(10), _config.SubscriptionBillingInterval, "SubscriptionBilling"); // NEW
            _statusUpdateFromScheduleTimer = CreateTimer(UpdateShopStatusFromScheduleAsync, TimeSpan.FromSeconds(45), TimeSpan.FromMinutes(5), "ShopStatusScheduleUpdate");
            _dataArchivalTimer = CreateTimer(PerformDataArchivalAsync, TimeSpan.FromHours(1), _config.DataArchivalInterval, "DataArchival"); // NEW

            sw.Stop();
            _logger.LogInformation("Background tasks started in {ElapsedMilliseconds}ms.", sw.ElapsedMilliseconds);
        }

        private void RestartBackgroundTasksIfNeeded(ShopConfiguration oldConfig, ShopConfiguration newConfig)
        {
             // Compare relevant interval properties
             if (oldConfig.HealthCheckInterval != newConfig.HealthCheckInterval ||
                 oldConfig.AbandonedCartCheckInterval != newConfig.AbandonedCartCheckInterval || // Add all timers
                 /* ... other interval comparisons ... */ true)
             {
                  _logger.LogInformation("Configuration change detected requires restarting background tasks.");
                  StartBackgroundTasks(); // This will dispose old timers and create new ones
             }
        }

        private void DisposeTimers()
        {
            _healthMonitorTimer?.Dispose(); _healthMonitorTimer = null;
            _abandonedCartTimer?.Dispose(); _abandonedCartTimer = null;
            _autoRestockTimer?.Dispose(); _autoRestockTimer = null;
            _auctionProcessingTimer?.Dispose(); _auctionProcessingTimer = null;
            _recommendationModelUpdateTimer?.Dispose(); _recommendationModelUpdateTimer = null;
            _scheduledSalesTimer?.Dispose(); _scheduledSalesTimer = null;
            _subscriptionBillingTimer?.Dispose(); _subscriptionBillingTimer = null;
            _statusUpdateFromScheduleTimer?.Dispose(); _statusUpdateFromScheduleTimer = null;
            _dataArchivalTimer?.Dispose(); _dataArchivalTimer = null;
        }

        // Placeholder async methods for new background tasks
        private Task ProcessScheduledSalesAsync(object state) { _logger.LogTrace("Checking scheduled sales..."); return Task.CompletedTask; }
        private Task ProcessSubscriptionBillingCycleAsync(object state) { _logger.LogTrace("Processing subscription billing..."); return Task.CompletedTask; }
        private Task PerformDataArchivalAsync(object state) { _logger.LogTrace("Performing data archival..."); return Task.CompletedTask; }
        private Task NotifyAdminOfFailureAsync(string subject, string details, CancellationToken cancellationToken) { _logger.LogCritical("ADMIN ALERT NEEDED: {Subject} - {Details}", subject, details); /* Send email/alert */ return Task.CompletedTask;}


        #endregion

        #region Helper Methods (Enhanced)

        // Get shop lock (remains same)
        private SemaphoreSlim GetShopLock(int shopId) => _shopLocks.GetOrAdd(shopId, _ => new SemaphoreSlim(1, 1));
        // NEW: Get or create order lock
        private SemaphoreSlim GetOrderLock(Guid orderId) => _orderLocks.GetOrAdd(orderId, _ => new SemaphoreSlim(1, 1));
        // NEW: Get or create auction lock
        private SemaphoreSlim GetAuctionLock(Guid auctionId) => _auctionLocks.GetOrAdd(auctionId, _ => new SemaphoreSlim(1, 1));


        private async Task<bool> LoadShopAsync(int shopId, CancellationToken cancellationToken)
        {
             // (Implementation largely same, but use ConvertFromShopData)
             _logger.LogDebug("Loading data for Shop {ShopId}...", shopId);
             try
             {
                  ShopData data = await _persistenceService.LoadShopDataAsync(shopId, cancellationToken);
                  ShopState state = (data != null)
                       ? ConvertFromShopData(shopId, data)
                       : new ShopState(shopId, _config) { Status = ShopInstanceStatus.Closed }; // Ensure new shops start closed

                 // Apply global config defaults if shop config is missing fields (e.g., after adding new global settings)
                 // MergeConfigs(state.Configuration, _config);

                  _shopStates[shopId] = state;
                  await UpdateCachedReputationScoreAsync(shopId, state);
                  _logger.LogDebug("Shop {ShopId} data loaded. Status: {Status}", shopId, state.Status);
                  return true;
             }
             // ... (catch block remains same) ...
        }

        // --- DTOs & Conversion (Needs expansion for all new classes) ---
        public class ShopData { /* ... existing + new lists/properties for ScheduledSales, Subscriptions etc ... */ }
        private ShopData ConvertToShopData(ShopState state) { /* ... Convert all state inc. new fields to DTO ... */ }
        private ShopState ConvertFromShopData(int shopId, ShopData data) { /* ... Convert DTO back to state, populating ConcurrentCollections etc ... */ }
        // ... (Other DTOs and converters: ShopReputationData, AuctionData, etc.) ...


        // --- Central Permission Checking (Enhanced Example) ---
        public async Task<bool> CheckPermissionAsync(int playerId, string permission, CancellationToken cancellationToken = default)
        {
            // 1. Super Admin Check
            if (playerId == Constant.SUPER_ADMIN_PLAYER_ID) return true;

            // 2. Load User Roles/Permissions (from cache or session, fallback to DB)
            HashSet<string> userRoles;
            if (_userSessions.TryGetValue(playerId, out var session))
            {
                userRoles = session.Roles;
                session.LastActivity = DateTime.UtcNow; // Update activity
            }
            else
            {
                 // Attempt to load session/roles from persistence
                 userRoles = await _persistenceService.LoadUserRolesAsync(playerId, cancellationToken);
                 if (userRoles == null || !userRoles.Any()) return false; // No roles found
                 // Optionally create a session object here
            }

            // 3. Permission Mapping Logic (more sophisticated)
            // This could involve querying a dedicated permission service or checking a pre-loaded map.
            // Example: Check if any role grants the required permission
            foreach (var role in userRoles)
            {
                // bool roleGrantsPermission = await _permissionService.DoesRoleGrantPermissionAsync(role, permission, cancellationToken);
                // Mock check:
                if (role == "Admin") return true; // Admins have all permissions
                if (role == "Moderator" && permission.StartsWith("Moderate_")) return true;
                if (permission.StartsWith("ShopOwner_") && role == permission) return true; // Direct owner role match
                // Shop specific permissions: e.g., "Inventory_Manage_{ShopId}"
                if (permission.Contains('_') && permission.Contains('{') && permission.Contains('}')) // Placeholder for resource-specific perm
                {
                    // Extract resource ID (e.g., shopId) and check ownership role
                    // e.g. if (permission.StartsWith("Inventory_Manage_") && role == $"ShopOwner_{shopIdFromPermission}") return true;
                }
            }

            return false; // Default deny
        }

        // --- Audit Logging (Enhanced) ---
        public enum AuditAction { /* ... Existing ... */ ShopDelete, FeatureToggle, AbandonedCartReminder, SubscriptionCreate, SubscriptionCancel, SubscriptionBilled, TaxCalculation, ShippingQuote, FraudCheckPass, FraudCheckFail /* etc */ }
        public record AuditLogEntry(/* ... fields ... */);
        private async Task LogAuditEventAsync(int? shopId, int performingPlayerId, string category, AuditAction action, string details, CancellationToken cancellationToken = default, string ipAddress = "N/A")
        {
            var entry = new AuditLogEntry(/* ... create entry ... */);
            _logger.LogInformation("[AUDIT] Actor:{ActorId} Shop:{ShopId} Cat:{Category} Action:{Action} Details:{Details}", /* ... format ... */);

            // Send to configured storage
            try
            {
                 switch(_config.AuditLogType)
                 {
                     case AuditLogStorageType.File:
                         await AppendAuditToFileAsync(entry, cancellationToken);
                         break;
                     case AuditLogStorageType.Database:
                         await _persistenceService.LogAuditEventAsync(entry, cancellationToken);
                         break;
                     case AuditLogStorageType.EventStream:
                         await _eventBus.PublishAsync(new AuditEventOccurred(entry), cancellationToken);
                         break;
                 }
            }
            catch(Exception ex)
            {
                 _logger.LogError(ex, "Failed to persist audit log entry. Category: {Category}, Action: {Action}", category, action);
            }
        }
        private Task AppendAuditToFileAsync(AuditLogEntry entry, CancellationToken cancellationToken) { /* Implement file logging safely */ return Task.CompletedTask; }


        // Secure Token Generation (remains same)
        private static string GenerateSecureToken(int byteLength = 32) => Convert.ToBase64String(RandomNumberGenerator.GetBytes(byteLength));

        // JSON Options (remains same)
        private JsonSerializerOptions GetJsonOptions() => new() { WriteIndented = true };

        // Constants (remains same)
        private static class Constant { public const int MAX_SHOPS = 100; public const int SUPER_ADMIN_PLAYER_ID = 0; }

        // Result Pattern (remains same)
        public class ShopOperationResult { /* ... */ }
        public class ShopOperationResult<T> : ShopOperationResult { /* ... */ }

        // Coupon Code Generation (remains same)
        private string GenerateCouponCode(int length = 8) { /* ... */ }


        #endregion
    }

    #region Supporting Classes, Enums, EventArgs (Expanded)

    // --- Interfaces for new services used above ---
    public record ShippingQuoteResult(/* ... quotes ... */);
    public record LabelGenerationResult(/* ... label data/url ... */);
    public record TrackingUpdateResult(/* ... status, location ... */);
    public record TaxCalculationResult(decimal TaxAmount, decimal TaxableAmount, Dictionary<string, decimal> RateDetails);
    public record FraudCheckResult(bool IsFraudulent, float Score, string Reason, List<string> RulesTriggered);
    public record Subscription(/* ... details ... */);
    public record CreateSubscriptionRequest(/* ... */);
    public record ModerationResult(bool IsApproved, string EditedText, List<string> Flags);
    public enum ContentType { Review, ForumPost, ChatMessage }
    public record ShoppingCart(int PlayerId, List<CartItemInfo> Items, DateTime LastUpdated, bool RecoveryAttempted);
    public record CartItemInfo(int ItemId, int Quantity, string ItemNameSnapshot /* ... other needed fields ... */);
    public enum NotificationType { Welcome, OrderConfirmation, OrderShipped, PasswordReset, AbandonedCart, Outbid, AuctionWon, ReviewRequest, GiftReceived }

    // --- EventArgs for New Events ---
    public class CartEventArgs : EventArgs { public ShoppingCart Cart { get; } public CartEventArgs(ShoppingCart cart) => Cart = cart; }
    public class SubscriptionEventArgs : EventArgs { public Subscription Subscription { get; } public SubscriptionEventArgs(Subscription sub) => Subscription = sub; }
    public class NotificationEventArgs : EventArgs { public int PlayerId { get; } public NotificationType Type { get; } public NotificationEventArgs(int pId, NotificationType type) { PlayerId = pId; Type = type; } }
    public class SystemHealthEventArgs : EventArgs { public Shop.ShopGlobalStatus NewStatus { get; } public Shop.ShopGlobalStatus OldStatus { get; } public string Reason { get; } public SystemHealthEventArgs(Shop.ShopGlobalStatus n, Shop.ShopGlobalStatus o, string r) { NewStatus = n; OldStatus = o; Reason = r; } }
    public class FeatureFlagEventArgs : EventArgs { public string Key { get; } public bool IsEnabled { get; } public bool PreviousState { get; } public FeatureFlagEventArgs(string k, bool enabled, bool prev) { Key = k; IsEnabled = enabled; PreviousState = prev; } }

    // --- Event Classes for Event Bus (Expanded) ---
    // (Existing events remain: ShopSystemStatusEvent, AuditEventOccurred, ShopStatusChangedEvent, OrderPlacedEvent, etc.)
    public record GlobalConfigChangedEvent(Shop.ShopConfiguration NewConfig); // NEW
    public record FeatureFlagUpdatedEvent(string Key, bool IsEnabled); // NEW
    public record CartCreatedEvent(ShoppingCart Cart); // NEW
    public record CartItemAddedEvent(int PlayerId, CartItemInfo Item); // NEW
    public record CartItemRemovedEvent(int PlayerId, int ItemId); // NEW
    public record CartClearedEvent(int PlayerId); // NEW
    public record CartAbandonedEvent(ShoppingCart Cart); // NEW
    public record SubscriptionCreatedEvent(Subscription Subscription); // NEW
    public record SubscriptionCancelledEvent(Guid SubscriptionId); // NEW
    public record SubscriptionBillingSuccessEvent(Guid SubscriptionId, Guid OrderId); // NEW
    public record SubscriptionBillingFailedEvent(Guid SubscriptionId, string Error); // NEW
    public record ScheduledSaleStartedEvent(int ShopId, Shop.ScheduledSale Sale); // NEW
    public record ScheduledSaleEndedEvent(int ShopId, Guid SaleId); // NEW
    // ... add events for all other new features ...

    // --- Other Supporting Classes/Enums Used ---
    public record Address(/* Street, City, Zip, Country etc. */);
    public record ShippingMethod(/* Id, Name, Cost, EstimatedDelivery */);
    public class LimitedTimeOffer { public Guid OfferId; public int ItemId; public decimal DiscountPercentage; public DateTime Expiry; } // Define fully
    public class ScheduledSale { public Guid SaleId; public List<int> ItemIds; public List<string> Categories; public decimal DiscountPercentage; public DateTime StartTime; public DateTime EndTime; } // Define fully
    public class ForumPost { /* Id, PlayerId, Title, Content, Timestamp, Replies */ } // Define fully
    public class LoyaltyTier // Enhanced
    {
        public string Name { get; }
        public int PointsRequired { get; }
        public decimal DiscountPercentage { get; }
        public int BonusPointsPercentage { get; }
        public TimeSpan? TierExpiryDuration { get; } // NEW: How long tier lasts after being earned (Zero=permanent)
        // ... Other Benefits ...
        public LoyaltyTier(string name, int pts, decimal disc, int bonus, TimeSpan? expiry) { /* ... */ }
    }
    // ... Define other records/classes used (e.g., UserContext for fraud check) ...


    #endregion
}
