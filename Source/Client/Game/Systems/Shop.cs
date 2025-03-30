#region Usings
// --- System ---
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // For validation attributes
using System.Diagnostics; // For DebuggerStepThrough, Stopwatch
using System.Diagnostics.CodeAnalysis; // For MaybeNullWhen
using System.Globalization; // For Currency formatting, i18n
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

namespace Client.EnhancedShopV2 // Renamed namespace for clarity
{
    #region Enhanced Interfaces (for DI and Testability)

    // (Existing interfaces: IShopEventBus, IPaymentGatewayService, IInventoryNotifier, IRecommendationEngine, IShopPersistenceService)
    // (New interfaces from previous example: IShippingService, ITaxCalculationService, IFraudDetectionService, ISubscriptionService, IContentModerationService, ICartPersistenceService, IUserNotificationService)

    // --- Further New Interfaces ---

    public interface IProductVariantService // NEW: Handles SKU generation, variant options
    {
        Task<IEnumerable<ProductVariant>> GetVariantsForItemAsync(int baseItemId, CancellationToken cancellationToken = default);
        Task<ProductVariant> GetVariantBySkuAsync(string sku, CancellationToken cancellationToken = default);
        Task<bool> ValidateVariantSelectionAsync(int baseItemId, Dictionary<string, string> selectedOptions, CancellationToken cancellationToken = default);
        Task<string> GenerateSkuAsync(int baseItemId, Dictionary<string, string> options, CancellationToken cancellationToken = default);
    }

    public interface IProductBundleService // NEW: Manages bundled products
    {
        Task<ProductBundle> GetBundleDefinitionAsync(int bundleItemId, CancellationToken cancellationToken = default);
        Task<IEnumerable<BundleComponent>> GetBundleComponentsAsync(int bundleItemId, CancellationToken cancellationToken = default);
    }

    public interface IDigitalAssetDeliveryService // NEW: Handles delivery of digital goods
    {
        Task<DigitalDeliveryResult> DeliverAssetAsync(Guid orderId, int orderItemId, int playerId, string email, CancellationToken cancellationToken = default);
        Task<IEnumerable<UserLicense>> GetUserLicensesAsync(int playerId, CancellationToken cancellationToken = default);
    }

    public interface IReturnManagementService // NEW: Handles RMA processes
    {
        Task<RmaResult> RequestReturnAsync(RmaRequest request, CancellationToken cancellationToken = default);
        Task<RmaStatusUpdateResult> UpdateReturnStatusAsync(Guid rmaId, RmaStatus newStatus, string reason, int performingUserId, CancellationToken cancellationToken = default);
        Task<RmaDetails> GetReturnDetailsAsync(Guid rmaId, CancellationToken cancellationToken = default);
    }

    public interface ICustomerGroupService // NEW: Manages customer segments/groups for pricing/promos
    {
        Task<IEnumerable<string>> GetCustomerGroupsAsync(int playerId, CancellationToken cancellationToken = default);
        Task AssignCustomerToGroupAsync(int playerId, string groupName, CancellationToken cancellationToken = default);
    }

    public interface IAddressValidationService // NEW: Validates shipping/billing addresses
    {
        Task<AddressValidationResult> ValidateAddressAsync(Address address, CancellationToken cancellationToken = default);
    }

    public interface IReportingService // NEW: Hooks for generating reports
    {
        Task QueueReportGenerationAsync(ReportRequest request, CancellationToken cancellationToken = default);
        Task<ReportStatus> GetReportStatusAsync(Guid reportId, CancellationToken cancellationToken = default);
    }

    public interface ICmsService // NEW: Hooks for fetching CMS content (banners, descriptions)
    {
        Task<CmsContent> GetContentAsync(string contentKey, string locale, CancellationToken cancellationToken = default);
    }

    public interface IApiKeyService // NEW: Manages API keys for external integrations
    {
        Task<ApiKeyValidationResult> ValidateApiKeyAsync(string apiKey, string requiredScope, CancellationToken cancellationToken = default);
        Task<ApiKey> GenerateApiKeyAsync(int userId, string description, IEnumerable<string> scopes, CancellationToken cancellationToken = default);
    }

    public interface IDataWarehouseExporter // NEW: Service to push data to a DWH
    {
        Task ExportOrderDataAsync(Order order, CancellationToken cancellationToken = default);
        Task ExportUserDataAsync(UserProfile user, CancellationToken cancellationToken = default);
        // ... other export methods
    }

    public interface ILocalizationService // NEW: Handles string translations and regional formats
    {
        Task<string> GetLocalizedStringAsync(string resourceKey, string locale, CancellationToken cancellationToken = default);
        string FormatCurrency(decimal amount, string currencyCode, string locale);
        string FormatDateTime(DateTime dt, string formatSpecifier, string locale);
    }

    public interface IGeoIpService // NEW: Provides location based on IP
    {
        Task<GeoLocationInfo> GetLocationFromIpAsync(string ipAddress, CancellationToken cancellationToken = default);
    }

    #endregion

    // Main Shop Service Class
    public class Shop : IDisposable
    {
        #region Fields and Properties

        // --- Dependencies (Expanded) ---
        private readonly IMemoryCache _cache;
        private readonly ILogger<Shop> _logger;
        private readonly IShopEventBus _eventBus;
        private readonly IOptionsMonitor<ShopConfiguration> _configMonitor;
        private readonly IPaymentGatewayService _paymentGateway; // Consider supporting multiple gateways
        private readonly IInventoryNotifier _inventoryNotifier;
        private readonly IRecommendationEngine _recommendationEngine;
        private readonly IShopPersistenceService _persistenceService;
        private readonly IShippingService _shippingService;
        private readonly ITaxCalculationService _taxService;
        private readonly IFraudDetectionService _fraudService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IContentModerationService _contentModerationService;
        private readonly ICartPersistenceService _cartPersistenceService;
        private readonly IUserNotificationService _userNotificationService;
        // --- Further New Dependencies ---
        private readonly IProductVariantService _variantService;
        private readonly IProductBundleService _bundleService;
        private readonly IDigitalAssetDeliveryService _digitalDeliveryService;
        private readonly IReturnManagementService _rmaService;
        private readonly ICustomerGroupService _customerGroupService;
        private readonly IAddressValidationService _addressValidationService;
        private readonly IReportingService _reportingService;
        private readonly ICmsService _cmsService;
        private readonly IApiKeyService _apiKeyService;
        private readonly IDataWarehouseExporter _dwhExporter;
        private readonly ILocalizationService _localizationService;
        private readonly IGeoIpService _geoIpService;

        private ShopConfiguration _config; // Populated via IOptionsMonitor

        // --- Concurrency & State (Expanded) ---
        private readonly SemaphoreSlim _globalConfigLock = new(1, 1);
        private readonly ConcurrentDictionary<int, SemaphoreSlim> _shopLocks = new();
        private readonly ConcurrentDictionary<Guid, SemaphoreSlim> _orderLocks = new();
        private readonly ConcurrentDictionary<Guid, SemaphoreSlim> _auctionLocks = new();
        private readonly ConcurrentDictionary<Guid, SemaphoreSlim> _rmaLocks = new(); // NEW: Per-RMA locks
        private readonly SemaphoreSlim _operationThrottle;
        private readonly CancellationTokenSource _cts = new();
        private readonly ConcurrentDictionary<int, ShopState> _shopStates = new(); // Holds all data per shop
        private readonly ConcurrentDictionary<string, Coupon> _activeCoupons = new();
        private readonly ConcurrentDictionary<int, UserSession> _userSessions = new();
        private readonly ConcurrentDictionary<int, ShoppingCart> _activeCarts = new();
        private readonly ConcurrentDictionary<Guid, Subscription> _activeSubscriptions = new();
        private readonly ConcurrentDictionary<Guid, RmaDetails> _activeRmas = new(); // NEW
        private readonly ConcurrentDictionary<string, string> _localeSettings = new(); // NEW: Loaded available locales

        // --- Background Task Timers (Expanded) ---
        private Timer _healthMonitorTimer;
        private Timer _abandonedCartTimer;
        private Timer _autoRestockTimer;
        private Timer _auctionProcessingTimer;
        private Timer _recommendationModelUpdateTimer;
        private Timer _scheduledSalesTimer;
        private Timer _subscriptionBillingTimer;
        private Timer _statusUpdateFromScheduleTimer;
        private Timer _dataArchivalTimer;
        private Timer _rmaProcessingTimer; // NEW: Process pending returns
        private Timer _loyaltyTierExpiryTimer; // NEW: Check for expired tiers
        private Timer _reportGenerationWorkerTimer; // NEW: Check queued reports
        private Timer _wishlistNotificationTimer; // NEW: Check for back-in-stock etc.

        // --- Public Accessors ---
        public IReadOnlyDictionary<int, ShopState> ShopStates => _shopStates;
        public ShopGlobalStatus GlobalStatus { get; private set; } = ShopGlobalStatus.Initializing;

        // --- Events (Expanded) ---
        // (Existing events remain)
        public event EventHandler<CartEventArgs> CartUpdated;
        public event EventHandler<CartEventArgs> CartAbandonedWarning;
        public event EventHandler<SubscriptionEventArgs> SubscriptionStatusChanged;
        public event EventHandler<NotificationEventArgs> UserNotificationSent;
        public event EventHandler<SystemHealthEventArgs> SystemHealthChanged;
        public event EventHandler<FeatureFlagEventArgs> FeatureFlagToggled;
        public event EventHandler<RmaEventArgs> RmaStatusChanged; // NEW
        public event EventHandler<InventoryLevelChangedEventArgs> InventoryLevelChanged; // NEW (more specific)
        public event EventHandler<PriceChangedEventArgs> PriceChanged; // NEW
        public event EventHandler<UserSegmentChangedEventArgs> UserSegmentChanged; // NEW

        #endregion

        #region Constructor and Initialization (Expanded)

        public Shop(
            IMemoryCache cache,
            ILogger<Shop> logger,
            IShopEventBus eventBus,
            IOptionsMonitor<ShopConfiguration> configMonitor,
            IPaymentGatewayService paymentGateway,
            IInventoryNotifier inventoryNotifier,
            IRecommendationEngine recommendationEngine,
            IShopPersistenceService persistenceService,
            IShippingService shippingService,
            ITaxCalculationService taxService,
            IFraudDetectionService fraudService,
            ISubscriptionService subscriptionService,
            IContentModerationService contentModerationService,
            ICartPersistenceService cartPersistenceService,
            IUserNotificationService userNotificationService,
            // --- Further New Dependencies ---
            IProductVariantService variantService,
            IProductBundleService bundleService,
            IDigitalAssetDeliveryService digitalDeliveryService,
            IReturnManagementService rmaService,
            ICustomerGroupService customerGroupService,
            IAddressValidationService addressValidationService,
            IReportingService reportingService,
            ICmsService cmsService,
            IApiKeyService apiKeyService,
            IDataWarehouseExporter dwhExporter,
            ILocalizationService localizationService,
            IGeoIpService geoIpService
            )
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
            _variantService = variantService ?? throw new ArgumentNullException(nameof(variantService));
            _bundleService = bundleService ?? throw new ArgumentNullException(nameof(bundleService));
            _digitalDeliveryService = digitalDeliveryService ?? throw new ArgumentNullException(nameof(digitalDeliveryService));
            _rmaService = rmaService ?? throw new ArgumentNullException(nameof(rmaService));
            _customerGroupService = customerGroupService ?? throw new ArgumentNullException(nameof(customerGroupService));
            _addressValidationService = addressValidationService ?? throw new ArgumentNullException(nameof(addressValidationService));
            _reportingService = reportingService ?? throw new ArgumentNullException(nameof(reportingService));
            _cmsService = cmsService ?? throw new ArgumentNullException(nameof(cmsService));
            _apiKeyService = apiKeyService ?? throw new ArgumentNullException(nameof(apiKeyService));
            _dwhExporter = dwhExporter ?? throw new ArgumentNullException(nameof(dwhExporter));
            _localizationService = localizationService ?? throw new ArgumentNullException(nameof(localizationService));
            _geoIpService = geoIpService ?? throw new ArgumentNullException(nameof(geoIpService));


            _configMonitor.OnChange(UpdateConfiguration);
            _config = _configMonitor.CurrentValue ?? new ShopConfiguration();

            if (!ValidateConfiguration(_config))
            {
                throw new InvalidOperationException("Initial shop configuration is invalid.");
            }

            _operationThrottle = new SemaphoreSlim(_config.MaxConcurrentOperations, _config.MaxConcurrentOperations);

            _logger.LogInformation("Enhanced Shop system V2 initializing with significantly expanded features...");
            _ = InitializeSystemAsync(_cts.Token); // Fire-and-forget initialization
        }

        private async Task InitializeSystemAsync(CancellationToken cancellationToken)
        {
            try
            {
                SetGlobalStatus(ShopGlobalStatus.LoadingData);

                // --- Load Global Data ---
                await LoadGlobalCouponsAsync(cancellationToken);
                await LoadFeatureFlagStatesAsync(cancellationToken);
                await LoadSupportedLocalesAsync(cancellationToken); // NEW

                // --- Load Shop Instances ---
                var shopIdsToLoad = await _persistenceService.GetAllShopIdsAsync(cancellationToken)
                                        ?? Enumerable.Range(0, Constant.MAX_SHOPS); // Fallback/Simulate

                var loadTasks = shopIdsToLoad.Select(shopId => LoadShopAsync(shopId, cancellationToken)).ToList();
                await Task.WhenAll(loadTasks);

                foreach (var shopId in _shopStates.Keys)
                {
                    _shopLocks.TryAdd(shopId, new SemaphoreSlim(1, 1));
                    _ = GetShopInventoryAsync(shopId, cancellationToken: cancellationToken); // Pre-warm cache
                }

                // --- Load Other Global/Cross-Shop Data ---
                await LoadActiveSubscriptionsAsync(cancellationToken);
                await LoadActiveRmasAsync(cancellationToken); // NEW

                StartBackgroundTasks(); // Starts all timers
                SetGlobalStatus(ShopGlobalStatus.Online);
                _logger.LogInformation("Enhanced Shop system V2 initialized and online.");
            }
            catch (OperationCanceledException)
            {
                 _logger.LogWarning("Shop system V2 initialization cancelled.");
                 SetGlobalStatus(ShopGlobalStatus.ShuttingDown);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "CRITICAL FAILURE during Shop system V2 initialization.");
                SetGlobalStatus(ShopGlobalStatus.Error);
                await NotifyAdminOfFailureAsync("Initialization Failure", ex.ToString(), CancellationToken.None);
            }
        }

        // UpdateConfiguration remains largely the same, but RestartBackgroundTasksIfNeeded needs updating
        private void UpdateConfiguration(ShopConfiguration newConfig, string _)
        {
            _logger.LogInformation("Shop V2 configuration reloading...");
            if (!ValidateConfiguration(newConfig))
            {
                 _logger.LogError("Reloaded shop V2 configuration is invalid. Changes rejected.");
                 return;
            }

            _globalConfigLock.Wait();
            try
            {
                var oldConfig = _config;
                _config = newConfig ?? new ShopConfiguration();

                // Adjust dynamic components based on config changes
                // Resize throttle semaphore (consider implications)
                // Update background timer intervals
                RestartBackgroundTasksIfNeeded(oldConfig, _config); // Needs to check NEW interval settings

                _logger.LogInformation("Shop V2 configuration reloaded successfully.");
                _ = _eventBus.PublishAsync(new GlobalConfigChangedEvent(_config), CancellationToken.None);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to reload shop V2 configuration.");
            }
            finally
            {
                _globalConfigLock.Release();
            }
        }

        // ValidateConfiguration needs to check new config properties
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
            // --- Add Custom cross-field validation for NEW settings ---
            if(config.RmaWindowDuration <= TimeSpan.Zero)
            {
                 _logger.LogError("Configuration Error: RmaWindowDuration must be positive.");
                 isValid = false;
            }
            if(string.IsNullOrWhiteSpace(config.DefaultLocale) || config.SupportedLocales?.Any() != true)
            {
                 _logger.LogError("Configuration Error: DefaultLocale and SupportedLocales must be configured.");
                 isValid = false;
            }
            // ... etc ...

            return isValid;
        }

        // Dispose needs to dispose NEW timers and locks
        public void Dispose()
        {
            _logger.LogInformation("Enhanced Shop system V2 shutting down...");
            SetGlobalStatus(ShopGlobalStatus.ShuttingDown);

            _cts.Cancel();

            DisposeTimers(); // Disposes all timers

            // Dispose semaphores
            _globalConfigLock.Dispose();
            _operationThrottle.Dispose();
            foreach (var kvp in _shopLocks) kvp.Value.Dispose();
            foreach (var kvp in _orderLocks) kvp.Value.Dispose();
            foreach (var kvp in _auctionLocks) kvp.Value.Dispose();
            foreach (var kvp in _rmaLocks) kvp.Value.Dispose(); // NEW
            _cts.Dispose();

            _logger.LogInformation("Enhanced Shop system V2 shut down complete.");
            GC.SuppressFinalize(this); // Suppress finalizer if Dispose is called correctly
        }

        #endregion

        #region Enhanced State Management & Status (Expanded)

        public enum ShopGlobalStatus
        {
            Initializing, LoadingData, Online, Degraded, Maintenance, ShuttingDown, Error
        }

        public enum ShopInstanceStatus
        {
            Open, Closed, ScheduledClosure, Maintenance, Initializing, Locked, Archived, Deleted, PendingReview // NEW: For newly created shops
        }

        // ShopState holds all data and state for a single shop instance (Expanded)
        public class ShopState
        {
            public int ShopId { get; }
            public ShopInstanceStatus Status { get; set; } = ShopInstanceStatus.Initializing;
            public ShopConfiguration Configuration { get; set; } // Per-shop config overrides global
            public ShopReputation Reputation { get; set; } = new();
            public ConcurrentDictionary<int, ShopItem> Inventory { get; } = new(); // Items now might be Base Products
            public ConcurrentDictionary<string, ProductVariant> VariantInventory { get; } = new(); // NEW: Variant SKU -> Variant Details/Stock
            public ConcurrentDictionary<int, ProductBundle> Bundles { get; } = new(); // NEW: Bundle Item ID -> Bundle Definition
            public ConcurrentDictionary<string, List<int>> Categories { get; } = new();
            public ConcurrentDictionary<string, HashSet<int>> Tags { get; } = new();
            public List<LimitedTimeOffer> LimitedOffers { get; } = new();
            public List<ScheduledSale> ScheduledSales { get; } = new(); // Enhanced definition later
            public ConcurrentDictionary<string, (int PlayerId, DateTime Expiry)> ReservedItems { get; } = new(); // Key might be SKU now
            public List<ForumPost> ForumPosts { get; } = new();
            public List<ProductQuestion> ProductQuestions { get; } = new(); // NEW
            public ConcurrentDictionary<Guid, Auction> ActiveAuctions { get; } = new();
            public List<ShopSchedule> Schedules { get; } = new();
            public List<LoyaltyRecord> LoyaltyRecords { get; } = new();
            public List<string> BlockedPlayerIds { get; } = new(); // Consider ConcurrentBag or HashSet
            public DateTime LastRestockTime { get; set; } = DateTime.MinValue;
            public DateTime LastPriceUpdateTime { get; set; } = DateTime.MinValue;
            public string OwnerPlayerId { get; set; }
            public DateTime DateCreated { get; set; } = DateTime.UtcNow;
            public Dictionary<string, string> CustomFields { get; set; } = new(); // For extensibility
            public List<PickupLocation> PickupLocations { get; set; } = new(); // NEW
            public Dictionary<string, decimal> CustomerGroupPriceAdjustments { get; set; } = new(); // NEW: GroupName -> +/- Percentage or Fixed Amount
            public MultiLocationInventoryInfo MultiLocationInventory { get; set; } // NEW: Holds stock per location if enabled

            // Add LastUpdated timestamp
            public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

            public ShopState(int shopId, ShopConfiguration initialConfig)
            {
                ShopId = shopId;
                Configuration = initialConfig ?? new ShopConfiguration(); // Ensure non-null
            }
        }

        // ShopConfiguration (Expanded)
        public class ShopConfiguration
        {
            // --- Existing ---
            [Range(0.0, 1.0)] public decimal DefaultTaxRate { get; set; } = 0.05m; // Consider moving fully to TaxService config
            [Range(1, 1000)] public int DefaultMaxItemsPerOrder { get; set; } = 50;
            public bool EnableDynamicPricingGlobally { get; set; } = true;
            [Required][MaxLength(10)] public string DefaultCurrency { get; set; } = "USD"; // Changed default
            public bool AllowPlayerTradesGlobally { get; set; } = false;
            [Range(1, 10000)] public int MaxConcurrentOperations { get; set; } = 200; // Increased default
            [Range(typeof(TimeSpan), "00:00:10", "01:00:00")] public TimeSpan DefaultReservationTime { get; set; } = TimeSpan.FromMinutes(10); // Reduced default
            [Range(typeof(TimeSpan), "00:00:05", "00:30:00")] public TimeSpan CacheDurationShort { get; set; } = TimeSpan.FromSeconds(30); // Further reduced
            [Range(typeof(TimeSpan), "00:01:00", "02:00:00")] public TimeSpan CacheDurationMedium { get; set; } = TimeSpan.FromMinutes(5); // Reduced
            [Range(typeof(TimeSpan), "00:10:00", "12:00:00")] public TimeSpan CacheDurationLong { get; set; } = TimeSpan.FromHours(1); // Reduced
            [Range(1, 10)] public int MaxReviewsPerPlayerPerShop { get; set; } = 1;
            [Range(0.1f, 0.9f)] public float ReputationWeightRecent { get; set; } = 0.7f;
            [Range(typeof(TimeSpan), "00:00:10", "00:10:00")] public TimeSpan AuctionEndTimeExtension { get; set; } = TimeSpan.FromMinutes(1);
            [Range(0, 1000)] public int PointsPerCurrencyUnitSpent { get; set; } = 1;
            public List<LoyaltyTier> LoyaltyTiers { get; set; } = GetDefaultLoyaltyTiers();
            [Range(5, 200)] public int MaxWishlistItems { get; set; } = 50; // Increased
            public bool Require2FAForHighValueTrades { get; set; } = true;
            [Range(100, 100000)] public decimal HighValueTradeThreshold { get; set; } = 1000m;
            public string AuditLogStoragePath { get; set; } = "/var/log/shop/audit";
            public AuditLogStorageType AuditLogType { get; set; } = AuditLogStorageType.File;
            [Range(10, 10000)] public int MaxInventorySize { get; set; } = 1000;
            [Range(typeof(TimeSpan), "00:05:00", "24:00:00")] public TimeSpan AutoRestockCheckInterval { get; set; } = TimeSpan.FromHours(1);
            [Range(typeof(TimeSpan), "00:01:00", "01:00:00")] public TimeSpan HealthCheckInterval { get; set; } = TimeSpan.FromMinutes(5);
            [Range(typeof(TimeSpan), "00:00:01", "00:05:00")] public TimeSpan AuctionProcessingInterval { get; set; } = TimeSpan.FromSeconds(15); // Increased slightly
            [Range(typeof(TimeSpan), "00:15:00", "48:00:00")] public TimeSpan RecommendationModelUpdateInterval { get; set; } = TimeSpan.FromHours(4); // Reduced
            [Range(typeof(TimeSpan), "00:05:00", "24:00:00")] public TimeSpan AbandonedCartCheckInterval { get; set; } = TimeSpan.FromMinutes(30); // Reduced
            [Range(typeof(TimeSpan), "00:00:30", "01:00:00")] public TimeSpan AbandonedCartThreshold { get; set; } = TimeSpan.FromMinutes(60); // Reduced
            public List<AbandonedCartStep> AbandonedCartSequence { get; set; } = GetDefaultAbandonedCartSequence(); // NEW: Multi-step recovery
            [Range(typeof(TimeSpan), "00:05:00", "24:00:00")] public TimeSpan SubscriptionBillingInterval { get; set; } = TimeSpan.FromHours(1); // Reduced
            [Range(typeof(TimeSpan), "01:00:00", "7.00:00:00")] public TimeSpan DataArchivalInterval { get; set; } = TimeSpan.FromDays(1);
            [Range(typeof(TimeSpan), "30.00:00:00", "3650.00:00:00")] public TimeSpan OrderArchivalAge { get; set; } = TimeSpan.FromDays(180);
            public Dictionary<string, bool> FeatureFlags { get; set; } = new();

            // --- NEW Configuration Properties ---
            public bool EnableProductVariants { get; set; } = true;
            public bool EnableProductBundles { get; set; } = true;
            public bool EnableDigitalGoods { get; set; } = true;
            public bool EnablePreOrders { get; set; } = false;
            public bool EnableReturns { get; set; } = true;
            [Range(typeof(TimeSpan), "1.00:00:00", "90.00:00:00")] public TimeSpan RmaWindowDuration { get; set; } = TimeSpan.FromDays(30); // Window to request return
            public List<string> AllowedReturnReasons { get; set; } = new() { "Defective", "Wrong Item", "Changed Mind" };
            public bool EnableCustomerGroups { get; set; } = true;
            public bool ValidateAddressesOnCheckout { get; set; } = true; // Use AddressValidationService
            public bool EnableMultiCurrencyDisplay { get; set; } = true;
            [Required] public string DefaultLocale { get; set; } = "en-US";
            public List<string> SupportedLocales { get; set; } = new() { "en-US", "en-GB", "es-ES", "fr-FR", "de-DE" };
            public string GeoIpDatabasePath { get; set; } // Path to GeoIP database if using local provider
            public bool EnableGDPRTools { get; set; } = true;
            public bool EnableWishlistNotifications { get; set; } = true;
            public bool EnablePickupLocations { get; set; } = false;
            [Range(typeof(TimeSpan), "00:05:00", "24:00:00")] public TimeSpan RmaProcessingInterval { get; set; } = TimeSpan.FromHours(2);
            [Range(typeof(TimeSpan), "01:00:00", "7.00:00:00")] public TimeSpan LoyaltyTierExpiryInterval { get; set; } = TimeSpan.FromDays(1);
            [Range(typeof(TimeSpan), "00:01:00", "01:00:00")] public TimeSpan ReportGenerationInterval { get; set; } = TimeSpan.FromMinutes(15);
            [Range(typeof(TimeSpan), "00:05:00", "24:00:00")] public TimeSpan WishlistNotificationInterval { get; set; } = TimeSpan.FromHours(1);
            public Dictionary<string, string> ApiKeysForServices { get; set; } = new(); // e.g., {"TaxJarApiKey": "...", "EasyPostApiKey": "..."}
            public string CmsEndpoint { get; set; } // URL for CMS service
            public string DwhConnectionString { get; set; } // Connection for Data Warehouse export


            private static List<LoyaltyTier> GetDefaultLoyaltyTiers() => new() { /* As before */ };
            private static List<AbandonedCartStep> GetDefaultAbandonedCartSequence() => new()
            {
                new AbandonedCartStep(TimeSpan.FromHours(1), NotificationType.AbandonedCart, null), // Initial reminder
                new AbandonedCartStep(TimeSpan.FromDays(1), NotificationType.AbandonedCartFollowUp, "COMEBACK10"), // Follow-up with 10% discount
                new AbandonedCartStep(TimeSpan.FromDays(3), NotificationType.AbandonedCartFinalOffer, "LASTCHANCE15") // Final offer
            };
        }

        public enum AuditLogStorageType { File, Database, EventStream, Null } // Added Null for disabling

        // UserSession (Expanded)
        public class UserSession
        {
            public int PlayerId { get; }
            public HashSet<string> Roles { get; set; } = new();
            public DateTime LastActivity { get; set; }
            public DateTime SessionStartTime { get; }
            public string SessionToken { get; private set; }
            public string IpAddress { get; set; }
            public string UserAgent { get; set; }
            public string LocalePreference { get; set; } // NEW
            public string CurrencyPreference { get; set; } // NEW
            public GeoLocationInfo GeoLocation { get; set; } // NEW
            public Dictionary<string, DateTime> FeatureAccessLog { get; set; } = new(); // NEW: Track feature usage

            public UserSession(int playerId, string ipAddress, string userAgent, string defaultLocale, string defaultCurrency, GeoLocationInfo geoInfo)
            {
                PlayerId = playerId;
                var now = DateTime.UtcNow;
                SessionStartTime = now;
                LastActivity = now;
                SessionToken = GenerateSecureToken();
                IpAddress = ipAddress;
                UserAgent = userAgent;
                LocalePreference = defaultLocale; // Can be updated later
                CurrencyPreference = defaultCurrency; // Can be updated later
                GeoLocation = geoInfo;
            }

            public bool IsExpired(TimeSpan timeout) => DateTime.UtcNow - LastActivity > timeout;
            public void RefreshToken() => SessionToken = GenerateSecureToken();
            public bool HasRole(string role) => Roles.Contains(role, StringComparer.OrdinalIgnoreCase);
            public void UpdateActivity(string featureKey = null)
            {
                LastActivity = DateTime.UtcNow;
                if (featureKey != null) FeatureAccessLog[featureKey] = LastActivity;
            }
        }

        // SetGlobalStatus, UpdateShopStatusAsync remain similar, potentially adding more logging/events

        // DeleteShopAsync: Needs modification to handle deletion of related data (RMAs, Subscriptions, etc.) - VERY complex.

        // UpdateShopConfigurationAsync remains similar, validates new config

        #endregion

        #region --- Core Feature Expansions (Examples) ---

        // --- Place Order (Significantly Expanded Workflow) ---
        public async Task<ShopOperationResult<Order>> PlaceOrderAsync(int playerId, PlaceOrderRequest orderRequest, CancellationToken cancellationToken = default)
        {
            // 0. Throttle & Basic Validation
            await _operationThrottle.WaitAsync(cancellationToken);
            try
            {
                if (GlobalStatus != ShopGlobalStatus.Online) return ShopOperationResult<Order>.Fail("Shop system is not online.", ShopOperationError.SystemOffline);
                var validationResult = ValidatePlaceOrderRequest(orderRequest);
                if (!validationResult.IsValid) return ShopOperationResult<Order>.Fail(validationResult.ErrorMessage, ShopOperationError.InvalidInput);

                // 1. Load/Verify User Session & Context
                var userContext = await GetUserContextAsync(playerId, orderRequest.IpAddress, orderRequest.UserAgent, cancellationToken);
                if (userContext == null) return ShopOperationResult<Order>.Fail("Invalid user session.", ShopOperationError.AuthenticationRequired);
                userContext.Session.UpdateActivity(nameof(PlaceOrderAsync)); // Mark activity

                // 2. Load Cart / Items requested
                var cartItems = await GetCartItemsForOrderAsync(playerId, orderRequest.UseCartId, orderRequest.DirectItems, cancellationToken);
                if (!cartItems.Any()) return ShopOperationResult<Order>.Fail("Cannot place an order with no items.", ShopOperationError.CartEmpty);

                // 3. Lock necessary resources (e.g., shop if inventory check needs locking, though preferably optimistic)

                // 4. Validate Addresses (Billing/Shipping)
                AddressValidationResult shippingAddressValidation = null;
                if (_config.ValidateAddressesOnCheckout && orderRequest.ShippingAddress != null)
                {
                    shippingAddressValidation = await _addressValidationService.ValidateAddressAsync(orderRequest.ShippingAddress, cancellationToken);
                    if (!shippingAddressValidation.IsValid)
                        return ShopOperationResult<Order>.Fail($"Shipping address invalid: {shippingAddressValidation.Suggestion ?? shippingAddressValidation.Error}", ShopOperationError.InvalidAddress);
                    // Potentially update address based on suggestion if user confirmed?
                }
                // Validate Billing Address too...

                // 5. Check Inventory Availability (Optimistic or Pessimistic) & Reserve Items
                // This is complex with variants and bundles. Needs dedicated logic.
                var reservationResult = await ReserveInventoryForOrderAsync(cartItems, playerId, cancellationToken);
                if (!reservationResult.Success)
                    return ShopOperationResult<Order>.Fail($"Inventory unavailable: {reservationResult.ErrorMessage}", ShopOperationError.InsufficientStock);

                try // Use try/finally for reservation release
                {
                    // 6. Calculate Subtotal, Apply Promotions/Coupons
                    var pricingContext = await CalculateInitialPricingAsync(cartItems, playerId, orderRequest.CouponCode, cancellationToken);
                    if (!pricingContext.IsValid)
                    {
                        await ReleaseInventoryReservationAsync(reservationResult.ReservationId, cancellationToken); // Release inventory
                        return ShopOperationResult<Order>.Fail($"Pricing error: {pricingContext.ErrorMessage}", ShopOperationError.PricingError);
                    }

                    // 7. Calculate Shipping Costs
                    ShippingQuoteResult shippingQuotes = null;
                    if (orderRequest.ShippingAddress != null) // Physical goods
                    {
                        shippingQuotes = await _shippingService.GetShippingQuotesAsync(orderRequest.ShippingAddress, cartItems, cancellationToken);
                        if (shippingQuotes?.Quotes?.Any() != true)
                            return ShopOperationResult<Order>.Fail("Could not retrieve shipping quotes.", ShopOperationError.ShippingError); // Maybe allow proceeding without shipping if only digital?

                        var selectedShipping = shippingQuotes.Quotes.FirstOrDefault(q => q.Id == orderRequest.SelectedShippingMethodId);
                        if (selectedShipping == null)
                            return ShopOperationResult<Order>.Fail("Invalid shipping method selected.", ShopOperationError.InvalidInput);
                        pricingContext.ApplyShippingCost(selectedShipping.Cost);
                    }

                    // 8. Calculate Taxes
                    TaxCalculationResult taxResult = null;
                    if (orderRequest.ShippingAddress != null || _config.TaxDigitalGoods) // Need origin address too (from shop config?)
                    {
                         var originAddress = GetShopOriginAddress(cartItems.First().ShopId); // Get from shop config/state
                         taxResult = await _taxService.CalculateTaxesAsync(orderRequest.ShippingAddress, originAddress, cartItems, userContext.TaxId, cancellationToken);
                         if (taxResult == null) // Handle tax calculation failure - configurable (fail order or allow zero tax?)
                              return ShopOperationResult<Order>.Fail("Failed to calculate taxes.", ShopOperationError.TaxError);
                         pricingContext.ApplyTax(taxResult.TaxAmount);
                    }

                    // 9. Create Initial Order Record (Status: PendingPayment)
                    var order = CreateOrderFromContext(playerId, orderRequest, userContext, cartItems, pricingContext, shippingQuotes, taxResult);
                    await _persistenceService.SaveOrderAsync(order, cancellationToken); // Save initial state
                    await GetOrderLock(order.Id).WaitAsync(cancellationToken); // Lock order for payment processing
                    try
                    {
                        // 10. Fraud Detection (if enabled)
                        if (_config.EnableFraudDetection)
                        {
                            var fraudResult = await _fraudService.AnalyzeTransactionAsync(order, userContext, cancellationToken);
                            if (fraudResult.IsFraudulent)
                            {
                                await UpdateOrderStatusAsync(order.Id, OrderStatus.OnHoldFraudReview, "Fraud check failed", Constant.SYSTEM_USER_ID, cancellationToken);
                                await ReleaseInventoryReservationAsync(reservationResult.ReservationId, cancellationToken); // Release inventory
                                // Optionally notify admin/customer
                                return ShopOperationResult<Order>.Fail($"Order held for review: {fraudResult.Reason}", ShopOperationError.FraudCheckFailed);
                            }
                            order.FraudScore = fraudResult.Score; // Record score
                        }

                        // 11. Process Payment
                        var paymentDetails = CreatePaymentDetails(order, orderRequest.PaymentInfo);
                        var paymentResult = await _paymentGateway.ProcessPaymentAsync(paymentDetails, cancellationToken);

                        if (!paymentResult.IsSuccess)
                        {
                            await UpdateOrderStatusAsync(order.Id, OrderStatus.PaymentFailed, paymentResult.ErrorMessage, Constant.SYSTEM_USER_ID, cancellationToken);
                            await ReleaseInventoryReservationAsync(reservationResult.ReservationId, cancellationToken); // Release inventory
                            return ShopOperationResult<Order>.Fail($"Payment failed: {paymentResult.ErrorMessage}", ShopOperationError.PaymentFailed);
                        }

                        // 12. Update Order Status (Status: Processing or Completed if digital)
                        order.PaymentTransactionId = paymentResult.TransactionId;
                        order.PaymentProvider = _paymentGateway.Name; // Store gateway used
                        bool allDigital = cartItems.All(i => i.IsDigital); // Check item types
                        var nextStatus = allDigital ? OrderStatus.Completed : OrderStatus.Processing;
                        await UpdateOrderStatusAsync(order.Id, nextStatus, "Payment successful", Constant.SYSTEM_USER_ID, cancellationToken);

                        // 13. Finalize Inventory Update (Confirm reservation -> Deduct stock)
                        await FinalizeInventoryUpdateAsync(reservationResult.ReservationId, order.Id, cancellationToken);

                        // 14. Trigger Post-Order Actions (async)
                        // Run these in parallel or background queue
                        var postOrderTasks = new List<Task>();
                        postOrderTasks.Add(UpdateLoyaltyPointsAsync(playerId, order.TotalAmount, cancellationToken));
                        postOrderTasks.Add(_userNotificationService.SendNotificationAsync(playerId, NotificationType.OrderConfirmation, CreateOrderParams(order), cancellationToken));
                        postOrderTasks.Add(_dwhExporter.ExportOrderDataAsync(order, CancellationToken.None)); // Export to DWH
                        if (orderRequest.ClearCartAfterOrder && orderRequest.UseCartId.HasValue)
                        {
                            postOrderTasks.Add(ClearCartAsync(playerId, cancellationToken));
                        }
                        if (allDigital)
                        {
                             postOrderTasks.Add(DeliverDigitalGoodsAsync(order, cancellationToken));
                        }
                        // Trigger recommendation engine update
                        postOrderTasks.Add(_recommendationEngine.RecordPurchaseAsync(playerId, cartItems.Select(i => i.ItemId).ToList(), cancellationToken));

                        // Don't wait for all background tasks here, but log potential errors later
                        _ = Task.WhenAll(postOrderTasks).ContinueWith(t => {
                             if (t.IsFaulted) _logger.LogError(t.Exception, "Error during post-order background tasks for Order {OrderId}", order.Id);
                         }, CancellationToken.None);

                        // 15. Return Success
                        return ShopOperationResult<Order>.Success(order);

                    } // End Order Lock Try
                    finally
                    {
                        GetOrderLock(order.Id).Release();
                    }
                } // End Inventory Reservation Try
                finally
                {
                     // Ensure reservation is released if anything failed *after* reservation but *before* finalization
                     if (!reservationResult.IsFinalized)
                     {
                         await ReleaseInventoryReservationAsync(reservationResult.ReservationId, cancellationToken);
                     }
                }
            } // End Throttle Try
            catch (OperationCanceledException)
            {
                 _logger.LogWarning("PlaceOrderAsync cancelled for Player {PlayerId}.", playerId);
                 return ShopOperationResult<Order>.Fail("Operation cancelled.", ShopOperationError.Cancelled);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during PlaceOrderAsync for Player {PlayerId}.", playerId);
                return ShopOperationResult<Order>.Fail($"An unexpected error occurred: {ex.Message}", ShopOperationError.Unknown);
            }
            finally
            {
                _operationThrottle.Release();
            }
        }

        // --- Search Items (Enhanced with Facets) ---
        public async Task<ShopOperationResult<ItemSearchResult>> SearchItemsAsync(int? shopId, SearchQuery query, CancellationToken cancellationToken = default)
        {
            // 0. Throttle & Basic Validation
            await _operationThrottle.WaitAsync(cancellationToken);
            try
            {
                if (GlobalStatus == ShopGlobalStatus.ShuttingDown || GlobalStatus == ShopGlobalStatus.Error)
                    return ShopOperationResult<ItemSearchResult>.Fail("Shop system unavailable.", ShopOperationError.SystemOffline);

                // 1. Determine Search Scope (Global or Specific Shop)
                IEnumerable<ShopState> shopsToSearch = shopId.HasValue
                    ? (_shopStates.TryGetValue(shopId.Value, out var state) ? new[] { state } : Enumerable.Empty<ShopState>())
                    : _shopStates.Values;

                shopsToSearch = shopsToSearch.Where(s => s.Status == ShopInstanceStatus.Open || s.Status == ShopInstanceStatus.ScheduledClosure); // Only search open shops

                if (!shopsToSearch.Any())
                    return ShopOperationResult<ItemSearchResult>.Success(ItemSearchResult.Empty); // No shops match scope

                // 2. Build Search Predicate (Keywords, Filters, Sorting)
                // This is complex and might involve calling an external search service (Elasticsearch, Algolia)
                // Or implementing in-memory filtering/ranking
                Func<ShopItem, bool> filterPredicate = item =>
                {
                    bool match = true;
                    // Keyword match (name, description, tags) - case insensitive
                    if (!string.IsNullOrWhiteSpace(query.Keywords))
                    {
                        match &= (item.Name.Contains(query.Keywords, StringComparison.OrdinalIgnoreCase) ||
                                  item.Description.Contains(query.Keywords, StringComparison.OrdinalIgnoreCase) ||
                                  item.Tags.Any(t => t.Contains(query.Keywords, StringComparison.OrdinalIgnoreCase)));
                    }
                    if (!match) return false;

                    // Category filter
                    if (!string.IsNullOrWhiteSpace(query.Category))
                    {
                         match &= item.Categories.Contains(query.Category, StringComparer.OrdinalIgnoreCase);
                    }
                    if (!match) return false;

                    // Tag filter
                    if(query.Tags?.Any() == true)
                    {
                         match &= query.Tags.All(qt => item.Tags.Contains(qt, StringComparer.OrdinalIgnoreCase));
                    }
                    if (!match) return false;

                    // Price range filter
                    if(query.MinPrice.HasValue) match &= item.CurrentPrice >= query.MinPrice.Value;
                    if(query.MaxPrice.HasValue) match &= item.CurrentPrice <= query.MaxPrice.Value;
                    if (!match) return false;

                    // Custom attribute filters (e.g., Color=Red, Size=XL)
                    if(query.AttributeFilters?.Any() == true)
                    {
                         // This requires variants or attributes on ShopItem
                         // match &= query.AttributeFilters.All(kvp => item.Attributes.TryGetValue(kvp.Key, out var val) && val == kvp.Value);
                    }

                    // Availability filter (InStockOnly)
                    if(query.InStockOnly) match &= item.Quantity > 0;

                    return match;
                };

                // 3. Execute Search & Apply Sorting/Pagination
                var allMatchingItems = shopsToSearch
                    .SelectMany(s => s.Inventory.Values.Select(i => new { Shop = s, Item = i })) // Combine shop state and item
                    .Where(si => filterPredicate(si.Item));

                // Apply Sorting
                // (Complex: Needs mapping query.SortBy to properties, handling direction)
                // Example: if(query.SortBy == "PriceAsc") allMatchingItems = allMatchingItems.OrderBy(si => si.Item.CurrentPrice);

                // Apply Pagination
                var totalMatches = allMatchingItems.Count(); // Can be expensive without optimization
                var paginatedItems = allMatchingItems
                    .Skip(query.PageIndex * query.PageSize)
                    .Take(query.PageSize)
                    .Select(si => si.Item) // Select only the item for the result list
                    .ToList();

                // 4. Generate Facets (Counts for Categories, Tags, Price Ranges, Attributes)
                // Run this on the *unpaginated* results for accurate counts
                var facets = GenerateFacets(allMatchingItems.Select(si => si.Item)); // Helper method

                // 5. Assemble Result
                var searchResult = new ItemSearchResult
                {
                    Items = paginatedItems,
                    TotalCount = totalMatches,
                    PageIndex = query.PageIndex,
                    PageSize = query.PageSize,
                    Facets = facets
                };

                return ShopOperationResult<ItemSearchResult>.Success(searchResult);
            }
            catch (Exception ex)
            {
                 _logger.LogError(ex, "Error during SearchItemsAsync. Query: {@Query}", query);
                 return ShopOperationResult<ItemSearchResult>.Fail($"An unexpected error occurred: {ex.Message}", ShopOperationError.Unknown);
            }
            finally
            {
                _operationThrottle.Release();
            }
        }

        private List<Facet> GenerateFacets(IEnumerable<ShopItem> items)
        {
            // This needs optimization for large datasets (e.g., do counts in DB or search engine)
            var facets = new List<Facet>();

            // Category Facet
            var categoryCounts = items
                .SelectMany(i => i.Categories)
                .GroupBy(c => c, StringComparer.OrdinalIgnoreCase)
                .Select(g => new FacetValue(g.Key, g.Count()))
                .OrderByDescending(fv => fv.Count)
                .ToList();
            if (categoryCounts.Any()) facets.Add(new Facet("Category", categoryCounts));

            // Tag Facet
            var tagCounts = items
                .SelectMany(i => i.Tags)
                .GroupBy(t => t, StringComparer.OrdinalIgnoreCase)
                .Select(g => new FacetValue(g.Key, g.Count()))
                .OrderByDescending(fv => fv.Count)
                .Take(50) // Limit number of tag facets shown
                .ToList();
            if (tagCounts.Any()) facets.Add(new Facet("Tags", tagCounts));

            // Price Range Facet (Example ranges)
            // Define ranges based on data distribution or fixed buckets
            // ... calculation logic ...
            // facets.Add(new Facet("Price", priceRangeCounts));

            // Attribute Facets (e.g., Color, Size) - Requires variant data
            // ... calculation logic ...

            return facets;
        }

        #endregion

        #region --- Background Task Implementations (Expanded Stubs) ---

        // DisposeTimers needs to dispose NEW timers
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
             _rmaProcessingTimer?.Dispose(); _rmaProcessingTimer = null; // NEW
             _loyaltyTierExpiryTimer?.Dispose(); _loyaltyTierExpiryTimer = null; // NEW
             _reportGenerationWorkerTimer?.Dispose(); _reportGenerationWorkerTimer = null; // NEW
             _wishlistNotificationTimer?.Dispose(); _wishlistNotificationTimer = null; // NEW
        }

        // StartBackgroundTasks needs to create NEW timers
        private void StartBackgroundTasks()
        {
            _logger.LogInformation("Starting V2 enhanced background tasks...");
            Stopwatch sw = Stopwatch.StartNew();
            DisposeTimers(); // Ensure clean start

            Timer CreateTimer(Func<object, Task> callback, TimeSpan dueTime, TimeSpan period, string taskName)
            {
                 // Robust timer creation logic (as before)
                 // ...
            }

            // Existing timers... (use CreateTimer helper)
            _healthMonitorTimer = CreateTimer(MonitorHealthAsync, TimeSpan.FromSeconds(30), _config.HealthCheckInterval, "HealthMonitor");
            _abandonedCartTimer = CreateTimer(CheckAbandonedCartsAsync, TimeSpan.FromMinutes(2), _config.AbandonedCartCheckInterval, "AbandonedCart");
            // ... other existing timers

            // --- NEW Timers ---
            _rmaProcessingTimer = CreateTimer(ProcessPendingRmasAsync, TimeSpan.FromMinutes(5), _config.RmaProcessingInterval, "RmaProcessing");
            _loyaltyTierExpiryTimer = CreateTimer(CheckLoyaltyTierExpiriesAsync, TimeSpan.FromHours(1), _config.LoyaltyTierExpiryInterval, "LoyaltyTierExpiry");
            _reportGenerationWorkerTimer = CreateTimer(ProcessQueuedReportsAsync, TimeSpan.FromMinutes(1), _config.ReportGenerationInterval, "ReportGeneration");
            _wishlistNotificationTimer = CreateTimer(ProcessWishlistNotificationsAsync, TimeSpan.FromMinutes(15), _config.WishlistNotificationInterval, "WishlistNotifications");

            sw.Stop();
            _logger.LogInformation("V2 background tasks started in {ElapsedMilliseconds}ms.", sw.ElapsedMilliseconds);
        }

        // RestartBackgroundTasksIfNeeded needs to check NEW config intervals
         private void RestartBackgroundTasksIfNeeded(ShopConfiguration oldConfig, ShopConfiguration newConfig)
         {
             // Compare ALL relevant interval properties
             if (oldConfig.HealthCheckInterval != newConfig.HealthCheckInterval ||
                 oldConfig.AbandonedCartCheckInterval != newConfig.AbandonedCartCheckInterval ||
                 oldConfig.RmaProcessingInterval != newConfig.RmaProcessingInterval || // NEW
                 oldConfig.LoyaltyTierExpiryInterval != newConfig.LoyaltyTierExpiryInterval || // NEW
                 oldConfig.ReportGenerationInterval != newConfig.ReportGenerationInterval || // NEW
                 oldConfig.WishlistNotificationInterval != newConfig.WishlistNotificationInterval || // NEW
                 /* ... other interval comparisons ... */ true) // Placeholder true for example
             {
                  _logger.LogInformation("Configuration change detected requires restarting V2 background tasks.");
                  StartBackgroundTasks();
             }
         }

        // Placeholder async methods for NEW background tasks
        private async Task ProcessPendingRmasAsync(object state)
        {
            var cancellationToken = (CancellationToken)state;
            _logger.LogTrace("Checking pending RMAs...");
            // Logic: Find RMAs in statuses like 'PendingApproval', 'PendingReceipt'
            // Check conditions (e.g., time limits), call _rmaService methods to update status
            await Task.Delay(100, cancellationToken); // Simulate work
        }

        private async Task CheckLoyaltyTierExpiriesAsync(object state)
        {
             var cancellationToken = (CancellationToken)state;
             _logger.LogTrace("Checking loyalty tier expiries...");
            // Logic: Iterate through LoyaltyRecords, check EarnedDate + TierExpiryDuration
            // If expired, potentially downgrade tier and notify user
            await Task.Delay(100, cancellationToken); // Simulate work
        }

        private async Task ProcessQueuedReportsAsync(object state)
        {
            var cancellationToken = (CancellationToken)state;
            _logger.LogTrace("Processing queued reports...");
            // Logic: Check for reports queued via _reportingService.QueueReportGenerationAsync
            // Fetch data, generate report file/data, update status, notify user
            await Task.Delay(100, cancellationToken); // Simulate work
        }

        private async Task ProcessWishlistNotificationsAsync(object state)
        {
            var cancellationToken = (CancellationToken)state;
            _logger.LogTrace("Processing wishlist notifications...");
            // Logic: Check wishlisted items that came back in stock or dropped in price
            // Call _userNotificationService.SendNotificationAsync
            await Task.Delay(100, cancellationToken); // Simulate work
        }

        // Update other existing task methods (CheckAbandonedCartsAsync, etc.) if their logic changes significantly

        #endregion

        #region Helper Methods (Expanded)

        // GetShopLock, GetOrderLock, GetAuctionLock remain
        // NEW: Get or create RMA lock
        private SemaphoreSlim GetRmaLock(Guid rmaId) => _rmaLocks.GetOrAdd(rmaId, _ => new SemaphoreSlim(1, 1));

        // LoadShopAsync: Needs to handle loading NEW state properties (Variants, Bundles, etc.)
        private async Task<bool> LoadShopAsync(int shopId, CancellationToken cancellationToken)
        {
             _logger.LogDebug("Loading V2 data for Shop {ShopId}...", shopId);
             try
             {
                 ShopData data = await _persistenceService.LoadShopDataAsync(shopId, cancellationToken);
                 ShopState state = (data != null)
                     ? ConvertFromShopData(shopId, data) // Conversion needs massive update
                     : new ShopState(shopId, _config) { Status = ShopInstanceStatus.Closed };

                 // Merge global defaults into shop-specific config if needed
                 MergeConfigs(state.Configuration, _config);

                 _shopStates[shopId] = state;
                 await UpdateCachedReputationScoreAsync(shopId, state);
                 _logger.LogDebug("Shop {ShopId} V2 data loaded. Status: {Status}", shopId, state.Status);
                 return true;
             }
             catch (Exception ex)
             {
                  _logger.LogError(ex, "Failed to load V2 data for Shop {ShopId}", shopId);
                  // Optionally mark shop as Error state
                  return false;
             }
        }

        // LoadGlobalCouponsAsync, LoadFeatureFlagStatesAsync remain similar

        // NEW: Load supported locales
        private async Task LoadSupportedLocalesAsync(CancellationToken cancellationToken)
        {
            _localeSettings.Clear();
            // Load from config or a dedicated localization source
            foreach(var locale in _config.SupportedLocales ?? Enumerable.Empty<string>())
            {
                // Validate locale format?
                _localeSettings.TryAdd(locale, locale); // Simple add for now
            }
             _logger.LogInformation("Loaded {Count} supported locales.", _localeSettings.Count);
             await Task.CompletedTask; // Placeholder if loading is async
        }


        // LoadActiveSubscriptionsAsync remains similar

        // NEW: Load Active RMAs
        private async Task LoadActiveRmasAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug("Loading active RMAs...");
            _activeRmas.Clear();
            var rmas = await _persistenceService.LoadActiveRmasAsync(cancellationToken); // Needs persistence method
            int count = 0;
            foreach (var rma in rmas ?? Enumerable.Empty<RmaDetails>())
            {
                _activeRmas.TryAdd(rma.RmaId, rma);
                _rmaLocks.TryAdd(rma.RmaId, new SemaphoreSlim(1, 1)); // Ensure lock exists
                count++;
            }
            _logger.LogInformation("Loaded {Count} active RMAs.", count);
        }

        // DTOs & Conversion: Need significant expansion for all new classes/state
        public class ShopData { /* ... properties for ALL ShopState collections/fields ... */ }
        private ShopData ConvertToShopData(ShopState state) { /* ... Convert ALL state inc. new fields to DTO ... */ }
        private ShopState ConvertFromShopData(int shopId, ShopData data) { /* ... Convert DTO back to state, populating ConcurrentCollections etc ... */ }


        // CheckPermissionAsync: Needs enhancement for new permissions (RMA_Manage, Product_ManageVariants, etc.)
        public async Task<bool> CheckPermissionAsync(int playerId, string permission, int? resourceId = null, CancellationToken cancellationToken = default)
        {
             // Existing logic...
             // Add checks for new permissions based on roles or resource ownership
             // Example: Check if player owns the shop for shop-specific permissions
             // Example: Check if player has 'SupportAgent' role for RMA permissions
             // await _permissionService.HasPermissionAsync(playerId, permission, resourceId, cancellationToken); // Ideal: delegate to service
             return true; // Placeholder
        }

        // LogAuditEventAsync: Add new AuditAction cases
        public enum AuditAction { /* ... Existing + NEW ... */ RmaRequested, RmaStatusUpdate, ProductVariantCreated, BundleCreated, DigitalGoodDelivered, CustomerGroupAssigned, AddressValidationAttempt, ReportGenerated, CmsContentUpdated, ApiKeyGenerated, DataExported, LocaleChanged, PriceRuleApplied, /* etc */ }
        public record AuditLogEntry(/* ... fields ... */);
        private async Task LogAuditEventAsync(int? shopId, int performingPlayerId, string category, AuditAction action, string details, CancellationToken cancellationToken = default, string ipAddress = "N/A", Guid? entityId = null)
        {
             // Create entry, log, persist (as before)
        }

        // GenerateSecureToken, GetJsonOptions, Constants, Result Pattern, GenerateCouponCode remain similar

        // NEW: Get User Context Helper
        private async Task<UserContext> GetUserContextAsync(int playerId, string ipAddress, string userAgent, CancellationToken cancellationToken)
        {
             // Try loading from active session cache
             if (_userSessions.TryGetValue(playerId, out var session) && !session.IsExpired(_config.SessionTimeout)) // Add SessionTimeout to config
             {
                  session.IpAddress = ipAddress; // Update IP/UA if changed
                  session.UserAgent = userAgent;
                  // Update GeoLocation? Maybe only on login or periodically
                  return new UserContext(playerId, session, /* other loaded data */);
             }

             // Load from persistence (simulate session creation/loading)
             _logger.LogDebug("Loading context for Player {PlayerId} from persistence.", playerId);
             var roles = await _persistenceService.LoadUserRolesAsync(playerId, cancellationToken);
             var profile = await _persistenceService.LoadUserProfileAsync(playerId, cancellationToken); // Need UserProfile and persistence method
             var geoInfo = await _geoIpService.GetLocationFromIpAsync(ipAddress, cancellationToken); // Get location

             if (profile == null) return null; // User not found

             session = new UserSession(playerId, ipAddress, userAgent, profile.PreferredLocale ?? _config.DefaultLocale, profile.PreferredCurrency ?? _config.DefaultCurrency, geoInfo);
             session.Roles = roles ?? new HashSet<string>();
             // Set locale/currency from profile
             session.LocalePreference = profile.PreferredLocale ?? _config.DefaultLocale;
             session.CurrencyPreference = profile.PreferredCurrency ?? _config.DefaultCurrency;

             _userSessions[playerId] = session; // Cache the session

             return new UserContext(playerId, session, profile, /* loyalty status, customer groups etc. */);
        }


        // Other helpers needed for expanded logic (e.g., GetCartItemsForOrderAsync, ReserveInventoryForOrderAsync, CalculateInitialPricingAsync, CreateOrderFromContext, UpdateLoyaltyPointsAsync, DeliverDigitalGoodsAsync etc.)


        #endregion
    }

    #region Supporting Classes, Enums, EventArgs (Massively Expanded)

    // --- Interfaces for new services used above (Placeholder Definitions) ---
    public record ProductVariant(/* Options, SKU, Stock, PriceDiff */);
    public record ProductBundle(int BundleItemId, List<BundleComponent> Components, BundlePricingType PricingType);
    public record BundleComponent(int ItemId, int Quantity);
    public enum BundlePricingType { SumOfComponents, FixedPrice }
    public record DigitalDeliveryResult(bool Success, string DownloadUrl, string LicenseKey, string Error);
    public record UserLicense(Guid LicenseId, int ItemId, string LicenseKey, DateTime ActivationDate);
    public record RmaRequest(Guid OrderId, int OrderItemId, int Quantity, string Reason, string Comments, int PlayerId);
    public record RmaResult(bool Success, Guid RmaId, RmaStatus InitialStatus, string Error);
    public enum RmaStatus { PendingApproval, ApprovedPendingReturn, Rejected, ItemReceived, ItemInspected, Refunded, Replaced, Closed }
    public record RmaStatusUpdateResult(bool Success, string Error);
    public record RmaDetails(Guid RmaId, Guid OrderId, /* ... full details ... */ RmaStatus Status, List<RmaHistoryEntry> History);
    public record RmaHistoryEntry(DateTime Timestamp, RmaStatus Status, string Notes, int UserId);
    public record AddressValidationResult(bool IsValid, Address CorrectedAddress, string Suggestion, string Error);
    public record ReportRequest(string ReportName, Dictionary<string, object> Parameters, int RequestedByPlayerId, ReportFormat Format);
    public enum ReportFormat { Csv, Pdf, Json }
    public record ReportStatus(Guid ReportId, bool IsComplete, string DownloadUrl, string Error);
    public record CmsContent(string ContentKey, string ContentHtml, Dictionary<string, string> Metadata);
    public record ApiKeyValidationResult(bool IsValid, int UserId, List<string> GrantedScopes);
    public record ApiKey(string Key, int UserId, List<string> Scopes, DateTime CreatedDate, DateTime? ExpiryDate);
    public record GeoLocationInfo(string CountryCode, string Region, string City, string PostalCode, decimal Latitude, decimal Longitude);

    // --- DTOs for Complex Operations ---
    public record PlaceOrderRequest(
        int? UseCartId, // Use items from saved cart ID
        List<DirectOrderItem> DirectItems, // Or specify items directly
        Address ShippingAddress,
        Address BillingAddress,
        Guid SelectedShippingMethodId, // From ShippingQuoteResult
        PaymentInfo PaymentInfo, // Contains token/details
        string CouponCode,
        bool ClearCartAfterOrder,
        string IpAddress,
        string UserAgent
    );
    public record DirectOrderItem(int ShopId, int ItemId, string Sku, int Quantity); // SKU needed for variants
    public record PaymentInfo(/* Payment method token, card details (if PCI compliant), etc. */);
    public record PricingContext( /* Holds subtotal, discounts, shipping, tax, total */ bool IsValid, string ErrorMessage)
    {
         public decimal Subtotal { get; private set; }
         public decimal DiscountAmount { get; private set; }
         public decimal ShippingCost { get; private set; }
         public decimal TaxAmount { get; private set; }
         public decimal GrandTotal => Subtotal - DiscountAmount + ShippingCost + TaxAmount;
         // Methods: ApplyDiscount, ApplyShippingCost, ApplyTax
    }
    public record InventoryReservationResult(bool Success, Guid ReservationId, bool IsFinalized, string ErrorMessage);
    public record SearchQuery(
        string Keywords,
        string Category,
        List<string> Tags,
        decimal? MinPrice,
        decimal? MaxPrice,
        Dictionary<string, string> AttributeFilters, // e.g., {"Color": "Blue", "Size": "L"}
        bool InStockOnly,
        string SortBy, // e.g., "PriceAsc", "PriceDesc", "NameAsc", "PopularityDesc"
        int PageIndex,
        int PageSize
    );
    public record ItemSearchResult(List<ShopItem> Items, int TotalCount, int PageIndex, int PageSize, List<Facet> Facets)
    {
         public static ItemSearchResult Empty => new ItemSearchResult(new List<ShopItem>(), 0, 0, 0, new List<Facet>());
    }
    public record Facet(string Name, List<FacetValue> Values);
    public record FacetValue(string Value, int Count);


    // --- EventArgs for New Events (Expanded) ---
    // CartEventArgs, SubscriptionEventArgs, NotificationEventArgs, SystemHealthEventArgs, FeatureFlagEventArgs (as before)
    public class RmaEventArgs : EventArgs { public RmaDetails Rma { get; } public RmaStatus PreviousStatus { get; } public RmaEventArgs(RmaDetails rma, RmaStatus prev) { Rma = rma; PreviousStatus = prev; } }
    public class InventoryLevelChangedEventArgs : EventArgs { public int ShopId { get; } public int ItemId { get; } public string Sku { get; } public int NewQuantity { get; } public int ChangeAmount { get; } /* ... */ }
    public class PriceChangedEventArgs : EventArgs { public int ShopId { get; } public int ItemId { get; } public string Sku { get; } public decimal NewPrice { get; } public decimal OldPrice { get; } /* ... */ }
    public class UserSegmentChangedEventArgs : EventArgs { public int PlayerId { get; } public string GroupName { get; } public bool WasAdded { get; } /* ... */ }


    // --- Event Classes for Event Bus (Expanded) ---
    // (Existing events remain)
    // GlobalConfigChangedEvent, FeatureFlagUpdatedEvent, CartEvents, SubscriptionEvents, ScheduledSaleEvents (as before)
    public record RmaRequestedEvent(RmaDetails Rma);
    public record RmaStatusChangedEvent(RmaDetails Rma, RmaStatus PreviousStatus);
    public record InventoryAdjustedEvent(int ShopId, int ItemId, string Sku, int ChangeAmount, string Reason, int? ActorId);
    public record PriceAdjustedEvent(int ShopId, int ItemId, string Sku, decimal NewPrice, string Reason, int? ActorId);
    public record UserProfileUpdatedEvent(int PlayerId, List<string> ChangedFields);
    public record UserAddressAddedEvent(int PlayerId, Address Address);
    public record UserAddressRemovedEvent(int PlayerId, Guid AddressId);
    public record UserGdprDataRequestedEvent(int PlayerId, GdprRequestType RequestType);
    public record UserGdprDataDeletedEvent(int PlayerId);
    public record ReportGenerationCompletedEvent(Guid ReportId, string DownloadUrl);
    public record ReportGenerationFailedEvent(Guid ReportId, string Error);
    // ... add events for all other significant actions ...

    // --- Other Supporting Classes/Enums Used (Expanded) ---
    public record Address(Guid Id, string Street1, string Street2, string City, string StateProvince, string PostalCode, string CountryCode, string Name, string Phone, bool IsDefaultBilling, bool IsDefaultShipping);
    public record ShippingMethod(Guid Id, string Name, string Carrier, decimal Cost, TimeSpan EstimatedMinDelivery, TimeSpan EstimatedMaxDelivery);
    // LimitedTimeOffer, ScheduledSale (consider adding specific types like BOGO, TieredDiscount)
    public enum SaleType { PercentageOff, FixedAmountOff, BuyXGetYFree }
    public record ScheduledSale(Guid SaleId, SaleType Type, /* Conditions */ object Parameters, /* Targets */ DateTime StartTime, DateTime EndTime);
    // ForumPost, LoyaltyTier (as before, maybe add more benefits)
    public record ProductQuestion(Guid Id, int ShopId, int ItemId, int PlayerId, string QuestionText, DateTime Timestamp, ProductAnswer Answer);
    public record ProductAnswer(Guid Id, int AdminPlayerId, string AnswerText, DateTime Timestamp);
    public record PickupLocation(Guid Id, string Name, Address LocationAddress, string OperatingHours);
    public record MultiLocationInventoryInfo(/* Dictionary<Guid LocationId, int Quantity> StockPerLocation */);
    public record UserContext(int PlayerId, UserSession Session, UserProfile Profile /*, LoyaltyStatus, List<string> CustomerGroups */)
    {
         public string TaxId => Profile?.TaxId; // Example access
    }
    public record UserProfile(int PlayerId, string Username, string Email, string FirstName, string LastName, string TaxId, List<Address> Addresses, string PreferredLocale, string PreferredCurrency, DateTime DateJoined, bool IsVerified);
    public record ShopItem : IEntity // Assuming IEntity provides Id
    {
         public int Id { get; set; } // Usually the DB key for the base product
         public int ShopId { get; set; }
         public string ItemCode { get; set; } // Base item code
         public string Name { get; set; }
         public string Description { get; set; }
         public List<string> ImageUrls { get; set; } = new();
         public decimal BasePrice { get; set; }
         public decimal CurrentPrice { get; set; } // After dynamic pricing/sales
         public string Currency { get; set; }
         public int Quantity { get; set; } // Stock for simple items, or total across variants if managed differently
         public bool IsAvailable { get; set; } // Can be unavailable even if stock > 0
         public List<string> Categories { get; set; } = new();
         public List<string> Tags { get; set; } = new();
         public Dictionary<string, string> Attributes { get; set; } = new(); // e.g., Weight, Dimensions, Material (for non-variant info)
         public ItemType ItemType { get; set; } = ItemType.Physical; // NEW
         public List<Review> Reviews { get; set; } = new(); // May lazy load
         public double AverageRating { get; set; } // Calculated or stored
         public DateTime DateAdded { get; set; }
         public DateTime LastUpdated { get; set; }
         // --- Variant/Bundle Info ---
         public bool HasVariants { get; set; }
         public bool IsBundle { get; set; }
         // --- Digital Good Info ---
         public Guid? DigitalAssetId { get; set; }
         // --- PreOrder Info ---
         public bool IsPreOrder { get; set; }
         public DateTime? ExpectedReleaseDate { get; set; }
    }
    public enum ItemType { Physical, Digital, Service, Bundle }
    public record Review(Guid Id, int PlayerId, int Rating, string Title, string Body, DateTime Timestamp, ReviewStatus Status);
    public enum ReviewStatus { Pending, Approved, Rejected }
    public record Order : IEntity // Assuming IEntity provides Id
    {
         public Guid Id { get; set; }
         public int PlayerId { get; set; }
         public int ShopId { get; set; } // Or multiple if multi-shop cart allowed
         public List<OrderItem> Items { get; set; } = new();
         public decimal Subtotal { get; set; }
         public decimal DiscountAmount { get; set; }
         public string CouponCode { get; set; }
         public decimal ShippingCost { get; set; }
         public ShippingMethod SelectedShippingMethod { get; set; }
         public decimal TaxAmount { get; set; }
         public Dictionary<string, decimal> TaxDetails { get; set; }
         public decimal TotalAmount { get; set; }
         public string Currency { get; set; }
         public OrderStatus Status { get; set; }
         public Address ShippingAddress { get; set; }
         public Address BillingAddress { get; set; }
         public DateTime OrderDate { get; set; }
         public DateTime LastUpdated { get; set; }
         public string PaymentTransactionId { get; set; }
         public string PaymentProvider { get; set; }
         public float? FraudScore { get; set; }
         public List<OrderHistoryEntry> History { get; set; } = new();
         public List<Shipment> Shipments { get; set; } = new(); // NEW for partial shipments
    }
    public record OrderItem(int Id, Guid OrderId, int ShopItemId, string Sku, string NameSnapshot, int Quantity, decimal UnitPrice, decimal TotalPrice, ItemType ItemType);
    public enum OrderStatus { PendingPayment, PaymentFailed, Processing, OnHoldFraudReview, OnHoldInventory, PartiallyShipped, Shipped, Completed, Cancelled, Refunded, PartiallyRefunded }
    public record OrderHistoryEntry(DateTime Timestamp, OrderStatus Status, string Notes, int? ActorId);
    public record Shipment(Guid Id, Guid OrderId, List<int> OrderItemIds, string TrackingNumber, string Carrier, DateTime ShipDate);
    public record AbandonedCartStep(TimeSpan Delay, NotificationType Notification, string CouponCode); // For sequence config
    public enum GdprRequestType { ExportData, DeleteData }

    // ... many other supporting classes/records needed ...

    #endregion
}
