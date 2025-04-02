#region Usings
// --- System ---
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // For validation attributes
using System.Diagnostics; // For DebuggerStepThrough, Stopwatch, ActivitySource
using System.Diagnostics.CodeAnalysis; // For MaybeNullWhen
using System.Diagnostics.Metrics; // For Metrics
using System.Globalization; // For Currency formatting, i18n
using System.IO;
using System.Linq;
using System.Net; // For IPAddress
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

namespace Client.EnhancedShopV3 // Renamed namespace for further clarity
{
    #region Massively Enhanced Interfaces (for DI and Testability)

    // --- Existing Interfaces (Assumed Present) ---
    // IShopEventBus, IPaymentGatewayService, IInventoryNotifier, IRecommendationEngine, IShopPersistenceService
    // IShippingService, ITaxCalculationService, IFraudDetectionService, ISubscriptionService
    // IContentModerationService, ICartPersistenceService, IUserNotificationService
    // IProductVariantService, IProductBundleService, IDigitalAssetDeliveryService, IReturnManagementService
    // ICustomerGroupService, IAddressValidationService, IReportingService, ICmsService, IApiKeyService
    // IDataWarehouseExporter, ILocalizationService, IGeoIpService

    // --- Further NEW Interfaces for V3 Expansion ---

    public interface IGiftCardService // NEW V3: Manages Gift Cards and Store Credit
    {
        Task<ShopOperationResult<GiftCard>> CreateGiftCardAsync(decimal amount, string currency, int? buyerPlayerId, string recipientEmail, DateTime? expiryDate, CancellationToken cancellationToken = default);
        Task<ShopOperationResult> ActivateGiftCardAsync(string code, CancellationToken cancellationToken = default);
        Task<ShopOperationResult<GiftCard>> GetGiftCardAsync(string code, CancellationToken cancellationToken = default);
        Task<ShopOperationResult<decimal>> RedeemGiftCardAsync(string code, Guid orderId, int playerId, decimal amountToRedeem, CancellationToken cancellationToken = default);
        Task<ShopOperationResult<decimal>> GetUserStoreCreditBalanceAsync(int playerId, string currency, CancellationToken cancellationToken = default);
        Task<ShopOperationResult> AdjustUserStoreCreditAsync(int playerId, decimal amount, string currency, StoreCreditAdjustmentReason reason, Guid? relatedEntityId, int? actorPlayerId, CancellationToken cancellationToken = default);
    }

    public interface IPromotionEngine // NEW V3: Advanced rule-based promotions
    {
        Task<PromotionEvaluationResult> EvaluateCartAsync(IEnumerable<PricedCartItem> items, UserContext userContext, IEnumerable<string> enteredCouponCodes, Address shippingAddress, CancellationToken cancellationToken = default);
        Task<IEnumerable<PromotionDefinition>> GetActivePromotionsAsync(int? shopId, UserContext userContext, CancellationToken cancellationToken = default);
        // Potentially methods to manage promotion definitions if not done externally
    }

    public interface IStockSubscriptionService // NEW V3: Back-in-stock notifications
    {
        Task<ShopOperationResult> SubscribeAsync(int playerId, int itemId, string sku, CancellationToken cancellationToken = default);
        Task<ShopOperationResult> UnsubscribeAsync(int playerId, int itemId, string sku, CancellationToken cancellationToken = default);
        Task<IEnumerable<StockSubscription>> GetPendingSubscriptionsForItemAsync(int itemId, string sku, CancellationToken cancellationToken = default);
        Task MarkNotificationsSentAsync(IEnumerable<Guid> subscriptionIds, CancellationToken cancellationToken = default);
        Task<IEnumerable<StockSubscription>> GetUserSubscriptionsAsync(int playerId, CancellationToken cancellationToken = default);
    }

    public interface ISearchService // NEW V3: Interface for external/advanced search engine
    {
        Task<ShopOperationResult> IndexItemAsync(ShopItem item, IEnumerable<ProductVariant> variants, CancellationToken cancellationToken = default);
        Task<ShopOperationResult> DeleteItemIndexAsync(int itemId, CancellationToken cancellationToken = default);
        Task<ShopOperationResult<ItemSearchResult>> SearchItemsAsync(SearchQuery query, UserContext userContext, CancellationToken cancellationToken = default);
        Task<ShopOperationResult<IEnumerable<string>>> SuggestAsync(string partialQuery, int? shopId, UserContext userContext, CancellationToken cancellationToken = default);
        Task<ShopOperationResult> BulkIndexItemsAsync(IEnumerable<ShopItem> items, CancellationToken cancellationToken = default);
    }

    public interface IPaymentPlanService // NEW V3: Integration with "Buy Now, Pay Later" services
    {
        Task<ShopOperationResult<PaymentPlanQuote>> GetQuoteAsync(decimal amount, string currency, UserContext userContext, Address billingAddress, CancellationToken cancellationToken = default);
        Task<ShopOperationResult<PaymentPlanAuthorization>> AuthorizePaymentPlanAsync(Guid quoteId, Guid orderId, CancellationToken cancellationToken = default);
        // Potentially methods for handling callbacks/webhooks from provider
    }

    public interface IAffiliateService // NEW V3: Manages affiliate tracking and commissions
    {
        Task<ShopOperationResult> TrackAffiliateVisitAsync(string affiliateCode, string ipAddress, string userAgent, string landingPage, CancellationToken cancellationToken = default);
        Task<ShopOperationResult> RecordAffiliateConversionAsync(Guid orderId, decimal orderTotal, string? trackedAffiliateCode, CancellationToken cancellationToken = default);
        Task<ShopOperationResult<AffiliateStats>> GetAffiliateStatsAsync(int affiliatePlayerId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
        Task<ShopOperationResult> ProcessPayoutsAsync(DateTime payoutPeriodEndDate, CancellationToken cancellationToken = default);
    }

    public interface IUserStoredPaymentService // NEW V3: Securely manage saved payment methods (tokens)
    {
        Task<ShopOperationResult<IEnumerable<StoredPaymentMethod>>> GetUserStoredPaymentsAsync(int playerId, CancellationToken cancellationToken = default);
        Task<ShopOperationResult<StoredPaymentMethod>> AddStoredPaymentAsync(int playerId, string paymentToken, string cardType, string lastFourDigits, int expiryMonth, int expiryYear, Address billingAddress, bool isDefault, CancellationToken cancellationToken = default);
        Task<ShopOperationResult> RemoveStoredPaymentAsync(int playerId, Guid storedPaymentId, CancellationToken cancellationToken = default);
        Task<ShopOperationResult> SetDefaultStoredPaymentAsync(int playerId, Guid storedPaymentId, CancellationToken cancellationToken = default);
    }

    public interface IRateLimiter // NEW V3: Service for checking rate limits
    {
        Task<RateLimitCheckResult> CheckRateLimitAsync(string resourceKey, string identifier, CancellationToken cancellationToken = default);
    }

    public interface IMetricsRegistry // NEW V3: Abstraction for recording metrics
    {
        // Using System.Diagnostics.Metrics is preferred now, but an interface can abstract specific implementations/registrations
        Counter<T> CreateCounter<T>(string name, string unit = null, string description = null) where T : struct;
        Histogram<T> CreateHistogram<T>(string name, string unit = null, string description = null) where T : struct;
        // Potentially methods to add tags/dimensions easily
    }

    public interface IABTestingService // NEW V3: Manages A/B test definitions and assignments
    {
        Task<ShopOperationResult<string>> GetTreatmentAssignmentAsync(string experimentKey, UserContext userContext, CancellationToken cancellationToken = default);
        Task<ShopOperationResult> RecordExperimentConversionAsync(string experimentKey, string treatment, UserContext userContext, CancellationToken cancellationToken = default);
        Task<IEnumerable<ExperimentDefinition>> GetActiveExperimentsAsync(CancellationToken cancellationToken = default);
    }

    public interface IShopAdminService // NEW V3: Centralized service for admin operations (used by API/Tools)
    {
        // --- Shop Management ---
        Task<ShopOperationResult> UpdateShopStatusAsync(int adminPlayerId, int shopId, ShopInstanceStatus newStatus, string reason);
        Task<ShopOperationResult> UpdateShopConfigAsync(int adminPlayerId, int shopId, ShopConfiguration overrides);
        Task<ShopOperationResult> CreateShopAsync(int adminPlayerId, /* Shop Creation Details */);
        // --- Product Management ---
        Task<ShopOperationResult<ShopItem>> CreateProductAsync(int adminPlayerId, int shopId, /* Product Details */);
        Task<ShopOperationResult> UpdateProductAsync(int adminPlayerId, int shopId, int itemId, /* Updated Details */);
        Task<ShopOperationResult> UpdateInventoryAsync(int adminPlayerId, int shopId, string sku, int changeAmount, string reason /*, Location? */);
        Task<ShopOperationResult<BulkOperationStatus>> PerformBulkProductOperationAsync(int adminPlayerId, int shopId, BulkOperationType operationType, Stream dataStream, CancellationToken cancellationToken = default);
        // --- Order Management ---
        Task<ShopOperationResult<Order>> GetOrderDetailsAsync(int adminPlayerId, Guid orderId);
        Task<ShopOperationResult> UpdateOrderStatusAsync(int adminPlayerId, Guid orderId, OrderStatus newStatus, string notes);
        Task<ShopOperationResult> IssueRefundAsync(int adminPlayerId, Guid orderId, /* Refund Details */);
        Task<ShopOperationResult<Shipment>> CreateShipmentAsync(int adminPlayerId, Guid orderId, List<int> orderItemIds, string trackingNumber, string carrier);
        // --- RMA Management ---
        Task<ShopOperationResult> UpdateRmaStatusAsync(int adminPlayerId, Guid rmaId, RmaStatus newStatus, string notes);
        // --- User Management ---
        Task<ShopOperationResult> AssignUserToGroupAsync(int adminPlayerId, int targetPlayerId, string groupName);
        Task<ShopOperationResult> BanUserAsync(int adminPlayerId, int targetPlayerId, TimeSpan duration, string reason);
        Task<ShopOperationResult> GetUserProfileAdminAsync(int adminPlayerId, int targetPlayerId);
        // --- Promotion/Gift Card Management ---
        Task<ShopOperationResult> CreatePromotionAsync(int adminPlayerId, PromotionDefinition definition);
        Task<ShopOperationResult> DeactivatePromotionAsync(int adminPlayerId, Guid promotionId);
        Task<ShopOperationResult> CreateGiftCardManuallyAsync(int adminPlayerId, decimal amount, string currency, string code = null);
        // --- System / Config ---
        Task<ShopOperationResult> TriggerDataExportAsync(int adminPlayerId, ExportDataType dataType, Dictionary<string, object> filters);
        Task<ShopOperationResult> UpdateGlobalFeatureFlagAsync(int adminPlayerId, string flagName, bool isEnabled);
        // ... many more admin functions ...
    }


    #endregion

    // Main Shop Service Class V3
    public class Shop : IDisposable
    {
        #region Fields and Properties (Massively Expanded)

        // --- Dependencies (Massively Expanded) ---
        private readonly IMemoryCache _cache;
        private readonly ILogger<Shop> _logger;
        private readonly IShopEventBus _eventBus;
        private readonly IOptionsMonitor<ShopConfiguration> _configMonitor;
        private readonly IPaymentGatewayService _paymentGateway; // Consider supporting multiple gateways (via Factory/Strategy)
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
        // --- NEW V3 Dependencies ---
        private readonly IGiftCardService _giftCardService;
        private readonly IPromotionEngine _promotionEngine;
        private readonly IStockSubscriptionService _stockSubscriptionService;
        private readonly ISearchService _searchService;
        private readonly IPaymentPlanService _paymentPlanService;
        private readonly IAffiliateService _affiliateService;
        private readonly IUserStoredPaymentService _userStoredPaymentService;
        private readonly IRateLimiter _rateLimiter;
        private readonly IMetricsRegistry _metricsRegistry; // Or direct use of System.Diagnostics.Metrics
        private readonly IABTestingService _abTestingService;
        private readonly IShopAdminService _adminService; // Core service for admin actions
        private readonly ActivitySource _activitySource; // For OpenTelemetry tracing

        private ShopConfiguration _config; // Populated via IOptionsMonitor

        // --- Concurrency & State (Expanded) ---
        private readonly SemaphoreSlim _globalConfigLock = new(1, 1);
        private readonly ConcurrentDictionary<int, SemaphoreSlim> _shopLocks = new();
        private readonly ConcurrentDictionary<Guid, SemaphoreSlim> _orderLocks = new();
        private readonly ConcurrentDictionary<Guid, SemaphoreSlim> _auctionLocks = new();
        private readonly ConcurrentDictionary<Guid, SemaphoreSlim> _rmaLocks = new();
        private readonly ConcurrentDictionary<Guid, SemaphoreSlim> _giftCardLocks = new(); // NEW V3
        private readonly ConcurrentDictionary<int, SemaphoreSlim> _userCreditLocks = new(); // NEW V3
        private readonly SemaphoreSlim _operationThrottle;
        private readonly CancellationTokenSource _cts = new();
        private readonly ConcurrentDictionary<int, ShopState> _shopStates = new(); // Holds all data per shop
        private readonly ConcurrentDictionary<string, Coupon> _activeCoupons = new(); // Maybe move to PromotionEngine/Persistence?
        private readonly ConcurrentDictionary<int, UserSession> _userSessions = new();
        private readonly ConcurrentDictionary<int, ShoppingCart> _activeCarts = new(); // Consider Cart Persistence service primary
        private readonly ConcurrentDictionary<Guid, Subscription> _activeSubscriptions = new();
        private readonly ConcurrentDictionary<Guid, RmaDetails> _activeRmas = new();
        private readonly ConcurrentDictionary<string, GiftCard> _activeGiftCards = new(); // NEW V3: Short-term cache?
        private readonly ConcurrentDictionary<string, string> _localeSettings = new();
        private readonly ConcurrentDictionary<string, ExperimentDefinition> _activeExperiments = new(); // NEW V3
        private readonly ConcurrentDictionary<string, bool> _featureFlags = new(); // Loaded state

        // --- Background Task Timers (Expanded) ---
        private Timer _healthMonitorTimer;
        private Timer _abandonedCartTimer;
        private Timer _autoRestockTimer;
        private Timer _auctionProcessingTimer;
        private Timer _recommendationModelUpdateTimer;
        private Timer _scheduledSalesTimer; // Might be handled by Promotion Engine now
        private Timer _subscriptionBillingTimer;
        private Timer _statusUpdateFromScheduleTimer;
        private Timer _dataArchivalTimer;
        private Timer _rmaProcessingTimer;
        private Timer _loyaltyTierExpiryTimer;
        private Timer _reportGenerationWorkerTimer;
        private Timer _stockSubscriptionNotifierTimer; // Renamed/Dedicated V3
        private Timer _affiliateProcessingTimer; // NEW V3 (e.g., for payouts)
        private Timer _experimentDataSyncTimer; // NEW V3 (Sync experiment definitions)
        private Timer _dataRetentionCleanupTimer; // NEW V3 (GDPR / Policy based cleanup)
        private Timer _searchIndexSyncTimer; // NEW V3 (Incremental sync if needed)


        // --- Public Accessors ---
        public IReadOnlyDictionary<int, ShopState> ShopStates => _shopStates;
        public ShopGlobalStatus GlobalStatus { get; private set; } = ShopGlobalStatus.Initializing;
        public string ServiceVersion { get; } = "3.0.0"; // Example version

        // --- Events (Expanded) ---
        // (Existing events remain)
        public event EventHandler<CartEventArgs> CartUpdated;
        public event EventHandler<CartEventArgs> CartAbandonedWarning;
        public event EventHandler<SubscriptionEventArgs> SubscriptionStatusChanged;
        public event EventHandler<NotificationEventArgs> UserNotificationSent;
        public event EventHandler<SystemHealthEventArgs> SystemHealthChanged;
        public event EventHandler<FeatureFlagEventArgs> FeatureFlagToggled;
        public event EventHandler<RmaEventArgs> RmaStatusChanged;
        public event EventHandler<InventoryLevelChangedEventArgs> InventoryLevelChanged;
        public event EventHandler<PriceChangedEventArgs> PriceChanged;
        public event EventHandler<UserSegmentChangedEventArgs> UserSegmentChanged;
        // --- NEW V3 Events ---
        public event EventHandler<GiftCardEventArgs> GiftCardStatusChanged;
        public event EventHandler<StoreCreditEventArgs> StoreCreditBalanceChanged;
        public event EventHandler<PromotionEventArgs> PromotionStatusChanged;
        public event EventHandler<StockSubscriptionEventArgs> StockNotificationSent;
        public event EventHandler<AffiliateEventArgs> AffiliateCommissionEarned;
        public event EventHandler<ExperimentEventArgs> ExperimentConversionRecorded;
        public event EventHandler<AdminActionEventArgs> AdminActionExecuted;


        // --- Metrics (Example Declarations - Init in Constructor) ---
        private readonly Counter<long> _ordersPlacedCounter;
        private readonly Histogram<double> _orderProcessingDurationHistogram;
        private readonly Counter<long> _cacheHitsCounter;
        private readonly Counter<long> _cacheMissesCounter;
        // ... more metrics

        #endregion

        #region Constructor and Initialization (Massively Expanded)

        public Shop(
            // --- Existing Dependencies ---
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
            IGeoIpService geoIpService,
            // --- NEW V3 Dependencies ---
            IGiftCardService giftCardService,
            IPromotionEngine promotionEngine,
            IStockSubscriptionService stockSubscriptionService,
            ISearchService searchService,
            IPaymentPlanService paymentPlanService,
            IAffiliateService affiliateService,
            IUserStoredPaymentService userStoredPaymentService,
            IRateLimiter rateLimiter,
            IMetricsRegistry metricsRegistry, // Or null if using static System.Diagnostics.Metrics
            IABTestingService abTestingService,
            IShopAdminService adminService,
            ActivitySource activitySource = null // Optional for tracing
            )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _logger.LogInformation("Initializing Enhanced Shop System V3...");

            // Assign all dependencies (null checks omitted for brevity, but crucial)
            _cache = cache;
            _eventBus = eventBus;
            _configMonitor = configMonitor;
            _paymentGateway = paymentGateway;
            _inventoryNotifier = inventoryNotifier;
            _recommendationEngine = recommendationEngine;
            _persistenceService = persistenceService;
            _shippingService = shippingService;
            _taxService = taxService;
            _fraudService = fraudService;
            _subscriptionService = subscriptionService;
            _contentModerationService = contentModerationService;
            _cartPersistenceService = cartPersistenceService;
            _userNotificationService = userNotificationService;
            _variantService = variantService;
            _bundleService = bundleService;
            _digitalDeliveryService = digitalDeliveryService;
            _rmaService = rmaService;
            _customerGroupService = customerGroupService;
            _addressValidationService = addressValidationService;
            _reportingService = reportingService;
            _cmsService = cmsService;
            _apiKeyService = apiKeyService;
            _dwhExporter = dwhExporter;
            _localizationService = localizationService;
            _geoIpService = geoIpService;
            // V3 Dependencies
            _giftCardService = giftCardService ?? throw new ArgumentNullException(nameof(giftCardService));
            _promotionEngine = promotionEngine ?? throw new ArgumentNullException(nameof(promotionEngine));
            _stockSubscriptionService = stockSubscriptionService ?? throw new ArgumentNullException(nameof(stockSubscriptionService));
            _searchService = searchService ?? throw new ArgumentNullException(nameof(searchService));
            _paymentPlanService = paymentPlanService ?? throw new ArgumentNullException(nameof(paymentPlanService));
            _affiliateService = affiliateService ?? throw new ArgumentNullException(nameof(affiliateService));
            _userStoredPaymentService = userStoredPaymentService ?? throw new ArgumentNullException(nameof(userStoredPaymentService));
            _rateLimiter = rateLimiter ?? throw new ArgumentNullException(nameof(rateLimiter));
            _metricsRegistry = metricsRegistry; // Optional
            _abTestingService = abTestingService ?? throw new ArgumentNullException(nameof(abTestingService));
            _adminService = adminService ?? throw new ArgumentNullException(nameof(adminService));
            _activitySource = activitySource ?? new ActivitySource("Client.EnhancedShopV3"); // Default name

            // Initialize Metrics (Example)
            // Prefer using System.Diagnostics.Metrics directly if possible
            var meter = new Meter("Client.EnhancedShopV3");
            _ordersPlacedCounter = meter.CreateCounter<long>("shop.orders.placed", "orders", "Number of orders successfully placed.");
            _orderProcessingDurationHistogram = meter.CreateHistogram<double>("shop.order.processing.duration", "ms", "Duration of the PlaceOrderAsync operation.");
            _cacheHitsCounter = meter.CreateCounter<long>("shop.cache.hits", "hits", "Number of cache hits.");
            _cacheMissesCounter = meter.CreateCounter<long>("shop.cache.misses", "misses", "Number of cache misses.");
            // Initialize other metrics...

            _configMonitor.OnChange(UpdateConfiguration);
            _config = _configMonitor.CurrentValue ?? new ShopConfiguration();

            if (!ValidateConfiguration(_config))
            {
                _logger.LogCritical("Initial shop configuration V3 is invalid. Aborting initialization.");
                throw new InvalidOperationException("Initial shop configuration V3 is invalid.");
            }

            _operationThrottle = new SemaphoreSlim(_config.MaxConcurrentOperations, _config.MaxConcurrentOperations);

            _logger.LogInformation("Shop V3 system core dependencies assigned. Starting asynchronous initialization...");
            _ = InitializeSystemAsync(_cts.Token); // Fire-and-forget initialization
        }

        private async Task InitializeSystemAsync(CancellationToken cancellationToken)
        {
            using var activity = _activitySource.StartActivity(nameof(InitializeSystemAsync)); // Tracing
            Stopwatch sw = Stopwatch.StartNew();
            try
            {
                SetGlobalStatus(ShopGlobalStatus.LoadingData);
                _logger.LogInformation("Shop V3 system initialization started.");

                // --- Load Global Data ---
                var globalTasks = new List<Task>
                {
                    LoadGlobalCouponsAsync(cancellationToken), // Might be deprecated by PromotionEngine
                    LoadFeatureFlagStatesAsync(cancellationToken),
                    LoadSupportedLocalesAsync(cancellationToken),
                    LoadActiveExperimentsAsync(cancellationToken) // NEW V3
                };
                await Task.WhenAll(globalTasks);
                _logger.LogInformation("Global data loaded.");

                // --- Load Shop Instances ---
                var shopIdsToLoad = await _persistenceService.GetAllShopIdsAsync(cancellationToken)
                                     ?? Enumerable.Range(0, _config.SimulatedShopCount); // Use config for simulation

                var loadTasks = shopIdsToLoad.Select(shopId => LoadShopAsync(shopId, cancellationToken)).ToList();
                await Task.WhenAll(loadTasks);
                _logger.LogInformation("Loaded {ShopCount} shop instances.", _shopStates.Count);

                foreach (var shopId in _shopStates.Keys)
                {
                    _shopLocks.TryAdd(shopId, new SemaphoreSlim(1, 1));
                    // Optionally pre-warm other caches (promotions, etc.)
                    _ = GetShopInventoryAsync(shopId, cancellationToken: cancellationToken); // Pre-warm inventory cache
                }

                // --- Load Other Global/Cross-Shop Data ---
                var crossShopTasks = new List<Task>
                {
                     LoadActiveSubscriptionsAsync(cancellationToken),
                     LoadActiveRmasAsync(cancellationToken),
                     // Potentially load active global gift cards if needed centrally
                     // LoadAffiliateDataAsync(cancellationToken) // If needed globally
                };
                await Task.WhenAll(crossShopTasks);
                _logger.LogInformation("Cross-shop data loaded.");

                StartBackgroundTasks(); // Starts all timers
                SetGlobalStatus(ShopGlobalStatus.Online);
                sw.Stop();
                _logger.LogInformation("Enhanced Shop system V3 initialized and online in {ElapsedMilliseconds}ms.", sw.ElapsedMilliseconds);
                activity?.SetTag("status", "Success");
                activity?.SetTag("duration_ms", sw.ElapsedMilliseconds);
            }
            catch (OperationCanceledException)
            {
                 _logger.LogWarning("Shop system V3 initialization cancelled.");
                 SetGlobalStatus(ShopGlobalStatus.ShuttingDown);
                 activity?.SetTag("status", "Cancelled");
                 activity?.SetStatus(ActivityStatusCode.Error, "Initialization cancelled");
            }
            catch (Exception ex)
            {
                sw.Stop();
                _logger.LogCritical(ex, "CRITICAL FAILURE during Shop system V3 initialization after {ElapsedMilliseconds}ms.", sw.ElapsedMilliseconds);
                SetGlobalStatus(ShopGlobalStatus.Error);
                await NotifyAdminOfFailureAsync("V3 Initialization Failure", ex.ToString(), CancellationToken.None);
                activity?.SetTag("status", "Failed");
                activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
                // Rethrow or handle appropriately depending on application lifetime management
                throw;
            }
        }

        // UpdateConfiguration needs to handle NEW V3 config settings
        private void UpdateConfiguration(ShopConfiguration newConfig, string _)
        {
             using var activity = _activitySource.StartActivity(nameof(UpdateConfiguration));
             _logger.LogInformation("Shop V3 configuration reloading detected...");
             if (!ValidateConfiguration(newConfig))
             {
                 _logger.LogError("Reloaded shop V3 configuration is invalid. Changes rejected.");
                 activity?.SetTag("status", "Invalid");
                 return;
             }

             _globalConfigLock.Wait();
             try
             {
                 var oldConfig = _config;
                 _config = newConfig ?? new ShopConfiguration();

                 // Adjust dynamic components based on config changes
                 // Resize throttle semaphore (consider implications carefully)
                 if (oldConfig.MaxConcurrentOperations != newConfig.MaxConcurrentOperations)
                 {
                     _logger.LogWarning("MaxConcurrentOperations changed from {Old} to {New}. Resizing throttle (potential impact).", oldConfig.MaxConcurrentOperations, newConfig.MaxConcurrentOperations);
                     // Simple semaphore replacement isn't safe if waiters exist. Requires careful handling or app restart.
                     // For now, log warning. A more robust solution might involve a dedicated controller.
                 }

                 // Reload feature flags from the new config
                 LoadFeatureFlagStatesAsync(CancellationToken.None).GetAwaiter().GetResult(); // Synchronous for simplicity during config reload lock

                 // Update background timer intervals
                 RestartBackgroundTasksIfNeeded(oldConfig, _config); // Checks NEW V3 interval settings

                 _logger.LogInformation("Shop V3 configuration reloaded successfully.");
                 _ = _eventBus.PublishAsync(new GlobalConfigChangedEvent(_config), CancellationToken.None);
                 activity?.SetTag("status", "Success");
             }
             catch (Exception ex)
             {
                 _logger.LogError(ex, "Failed to reload shop V3 configuration.");
                 activity?.SetTag("status", "Error");
                 activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
                 // Consider reverting to old config?
             }
             finally
             {
                 _globalConfigLock.Release();
             }
        }

        // ValidateConfiguration needs to check NEW V3 config properties
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
            // --- Add Custom cross-field validation for NEW V3 settings ---
            if(config.EnableGiftCards && config.GiftCardExpiryDuration <= TimeSpan.Zero)
            {
                _logger.LogError("Configuration Error: GiftCardExpiryDuration must be positive if gift cards are enabled.");
                isValid = false;
            }
             if(config.EnableMultiWarehouse && config.WarehouseLocations?.Any() != true)
            {
                 _logger.LogError("Configuration Error: WarehouseLocations must be configured if multi-warehouse is enabled.");
                 isValid = false;
            }
            if(config.EnableAffiliateProgram && config.DefaultAffiliateCommissionRate < 0)
            {
                 _logger.LogError("Configuration Error: DefaultAffiliateCommissionRate cannot be negative.");
                 isValid = false;
            }
            if(config.EnableSearchIntegration && string.IsNullOrWhiteSpace(config.SearchServiceEndpoint))
            {
                 _logger.LogError("Configuration Error: SearchServiceEndpoint must be configured if search integration is enabled.");
                 isValid = false;
            }
            // ... validate other V3 settings (Rate Limiting config, A/B test config, etc.) ...

            return isValid;
        }

        // Dispose needs to dispose NEW V3 timers and locks
        public void Dispose()
        {
            _logger.LogInformation("Enhanced Shop system V3 shutting down...");
            SetGlobalStatus(ShopGlobalStatus.ShuttingDown);

            _cts.Cancel(); // Signal cancellation to ongoing operations and timers

            DisposeTimers(); // Disposes all timers

            // Dispose semaphores
            _globalConfigLock.Dispose();
            _operationThrottle.Dispose();
            foreach (var kvp in _shopLocks) kvp.Value.Dispose();
            foreach (var kvp in _orderLocks) kvp.Value.Dispose();
            foreach (var kvp in _auctionLocks) kvp.Value.Dispose();
            foreach (var kvp in _rmaLocks) kvp.Value.Dispose();
            foreach (var kvp in _giftCardLocks) kvp.Value.Dispose(); // NEW V3
            foreach (var kvp in _userCreditLocks) kvp.Value.Dispose(); // NEW V3
            // Dispose other locks if added...

            _activitySource.Dispose(); // Dispose ActivitySource
            _cts.Dispose();

            _logger.LogInformation("Enhanced Shop system V3 shut down complete.");
            GC.SuppressFinalize(this); // Suppress finalizer if Dispose is called correctly
        }

        #endregion

        #region Enhanced State Management & Status (V3 - Minor Additions Here, Major in Config/State Classes)

        // Enums ShopGlobalStatus, ShopInstanceStatus remain the same conceptually

        // ShopState (V3 - Expanded for Multi-Warehouse etc.)
        public class ShopState
        {
            public int ShopId { get; }
            public ShopInstanceStatus Status { get; set; } = ShopInstanceStatus.Initializing;
            public ShopConfiguration Configuration { get; set; } // Per-shop config overrides global
            public ShopReputation Reputation { get; set; } = new();
            public ConcurrentDictionary<int, ShopItem> Inventory { get; } = new(); // Base product info
            public ConcurrentDictionary<string, ProductVariant> VariantInventory { get; } = new(); // SKU -> Variant Details (stock might move)
            public ConcurrentDictionary<int, ProductBundle> Bundles { get; } = new();
            public ConcurrentDictionary<string, List<int>> Categories { get; } = new();
            public ConcurrentDictionary<string, HashSet<int>> Tags { get; } = new();
            public List<LimitedTimeOffer> LimitedOffers { get; } = new(); // Consider integration with PromotionEngine
            public List<ScheduledSale> ScheduledSales { get; } = new(); // Consider integration with PromotionEngine
            public ConcurrentDictionary<string, InventoryReservation> ReservedItems { get; } = new(); // Key: SKU -> Reservation details
            public List<ForumPost> ForumPosts { get; } = new();
            public List<ProductQuestion> ProductQuestions { get; } = new();
            public ConcurrentDictionary<Guid, Auction> ActiveAuctions { get; } = new();
            public List<ShopSchedule> Schedules { get; } = new();
            public List<LoyaltyRecord> LoyaltyRecords { get; } = new();
            public List<string> BlockedPlayerIds { get; } = new();
            public DateTime LastRestockTime { get; set; } = DateTime.MinValue;
            public DateTime LastPriceUpdateTime { get; set; } = DateTime.MinValue;
            public string OwnerPlayerId { get; set; }
            public DateTime DateCreated { get; set; } = DateTime.UtcNow;
            public Dictionary<string, string> CustomFields { get; set; } = new();
            public List<PickupLocation> PickupLocations { get; set; } = new();
            public Dictionary<string, decimal> CustomerGroupPriceAdjustments { get; set; } = new(); // May integrate with PromotionEngine
            public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

            // --- NEW/Modified V3 State ---
            public bool IsMultiWarehouseEnabled { get; set; } // Determined from config on load
            public ConcurrentDictionary<string, MultiLocationStockInfo> StockByLocation { get; } = new(); // Key: SKU -> Stock per location
            public Address PrimaryOriginAddress { get; set; } // Needed for tax/shipping calcs

            public ShopState(int shopId, ShopConfiguration initialConfig)
            {
                ShopId = shopId;
                Configuration = initialConfig ?? new ShopConfiguration(); // Ensure non-null
                // Initialize based on config
                IsMultiWarehouseEnabled = Configuration.EnableMultiWarehouse;
                PrimaryOriginAddress = Configuration.DefaultShopOriginAddress; // Load from config/persistence
            }
        }

        // ShopConfiguration (V3 - Massively Expanded)
        public class ShopConfiguration
        {
            // --- Existing V2 ---
            [Range(0.0, 1.0)] public decimal DefaultTaxRate { get; set; } = 0.05m; // Less relevant if TaxService is primary
            [Range(1, 1000)] public int DefaultMaxItemsPerOrder { get; set; } = 50;
            public bool EnableDynamicPricingGlobally { get; set; } = true;
            [Required][MaxLength(10)] public string DefaultCurrency { get; set; } = "USD";
            public bool AllowPlayerTradesGlobally { get; set; } = false;
            [Range(1, 10000)] public int MaxConcurrentOperations { get; set; } = 200;
            [Range(typeof(TimeSpan), "00:00:10", "01:00:00")] public TimeSpan DefaultReservationTime { get; set; } = TimeSpan.FromMinutes(10);
            [Range(typeof(TimeSpan), "00:00:05", "00:30:00")] public TimeSpan CacheDurationShort { get; set; } = TimeSpan.FromSeconds(30);
            [Range(typeof(TimeSpan), "00:01:00", "02:00:00")] public TimeSpan CacheDurationMedium { get; set; } = TimeSpan.FromMinutes(5);
            [Range(typeof(TimeSpan), "00:10:00", "12:00:00")] public TimeSpan CacheDurationLong { get; set; } = TimeSpan.FromHours(1);
            [Range(1, 10)] public int MaxReviewsPerPlayerPerShop { get; set; } = 1;
            [Range(0.1f, 0.9f)] public float ReputationWeightRecent { get; set; } = 0.7f;
            [Range(typeof(TimeSpan), "00:00:10", "00:10:00")] public TimeSpan AuctionEndTimeExtension { get; set; } = TimeSpan.FromMinutes(1);
            [Range(0, 1000)] public int PointsPerCurrencyUnitSpent { get; set; } = 1;
            public List<LoyaltyTier> LoyaltyTiers { get; set; } = GetDefaultLoyaltyTiers();
            [Range(5, 200)] public int MaxWishlistItems { get; set; } = 50;
            public bool Require2FAForHighValueTrades { get; set; } = true;
            [Range(100, 100000)] public decimal HighValueTradeThreshold { get; set; } = 1000m;
            public string AuditLogStoragePath { get; set; } = "/var/log/shop/audit";
            public AuditLogStorageType AuditLogType { get; set; } = AuditLogStorageType.File;
            [Range(10, 10000)] public int MaxInventorySize { get; set; } = 1000; // Per location if multi-warehouse?
            [Range(typeof(TimeSpan), "00:05:00", "24:00:00")] public TimeSpan AutoRestockCheckInterval { get; set; } = TimeSpan.FromHours(1);
            [Range(typeof(TimeSpan), "00:01:00", "01:00:00")] public TimeSpan HealthCheckInterval { get; set; } = TimeSpan.FromMinutes(5);
            [Range(typeof(TimeSpan), "00:00:01", "00:05:00")] public TimeSpan AuctionProcessingInterval { get; set; } = TimeSpan.FromSeconds(15);
            [Range(typeof(TimeSpan), "00:15:00", "48:00:00")] public TimeSpan RecommendationModelUpdateInterval { get; set; } = TimeSpan.FromHours(4);
            [Range(typeof(TimeSpan), "00:05:00", "24:00:00")] public TimeSpan AbandonedCartCheckInterval { get; set; } = TimeSpan.FromMinutes(30);
            [Range(typeof(TimeSpan), "00:00:30", "01:00:00")] public TimeSpan AbandonedCartThreshold { get; set; } = TimeSpan.FromMinutes(60);
            public List<AbandonedCartStep> AbandonedCartSequence { get; set; } = GetDefaultAbandonedCartSequence();
            [Range(typeof(TimeSpan), "00:05:00", "24:00:00")] public TimeSpan SubscriptionBillingInterval { get; set; } = TimeSpan.FromHours(1);
            [Range(typeof(TimeSpan), "01:00:00", "7.00:00:00")] public TimeSpan DataArchivalInterval { get; set; } = TimeSpan.FromDays(1);
            [Range(typeof(TimeSpan), "30.00:00:00", "3650.00:00:00")] public TimeSpan OrderArchivalAge { get; set; } = TimeSpan.FromDays(180);
            public bool EnableProductVariants { get; set; } = true;
            public bool EnableProductBundles { get; set; } = true;
            public bool EnableDigitalGoods { get; set; } = true;
            public bool EnablePreOrders { get; set; } = false;
            public bool EnableReturns { get; set; } = true;
            [Range(typeof(TimeSpan), "1.00:00:00", "90.00:00:00")] public TimeSpan RmaWindowDuration { get; set; } = TimeSpan.FromDays(30);
            public List<string> AllowedReturnReasons { get; set; } = new() { "Defective", "Wrong Item", "Changed Mind" };
            public bool EnableCustomerGroups { get; set; } = true;
            public bool ValidateAddressesOnCheckout { get; set; } = true;
            public bool EnableMultiCurrencyDisplay { get; set; } = true;
            [Required] public string DefaultLocale { get; set; } = "en-US";
            public List<string> SupportedLocales { get; set; } = new() { "en-US", "en-GB", "es-ES", "fr-FR", "de-DE" };
            public string GeoIpDatabasePath { get; set; }
            public bool EnableGDPRTools { get; set; } = true;
            public bool EnableWishlistNotifications { get; set; } = true;
            public bool EnablePickupLocations { get; set; } = false;
            [Range(typeof(TimeSpan), "00:05:00", "24:00:00")] public TimeSpan RmaProcessingInterval { get; set; } = TimeSpan.FromHours(2);
            [Range(typeof(TimeSpan), "01:00:00", "7.00:00:00")] public TimeSpan LoyaltyTierExpiryInterval { get; set; } = TimeSpan.FromDays(1);
            [Range(typeof(TimeSpan), "00:01:00", "01:00:00")] public TimeSpan ReportGenerationInterval { get; set; } = TimeSpan.FromMinutes(15);
            [Range(typeof(TimeSpan), "00:05:00", "24:00:00")] public TimeSpan StockSubscriptionNotifyInterval { get; set; } = TimeSpan.FromHours(1); // Renamed V3
            public Dictionary<string, string> ApiKeysForServices { get; set; } = new();
            public string CmsEndpoint { get; set; }
            public string DwhConnectionString { get; set; }
            public int SimulatedShopCount { get; set; } = 10; // For dev/testing if persistence returns null
            [Range(typeof(TimeSpan), "00:01:00", "30.00:00:00")] public TimeSpan SessionTimeout { get; set; } = TimeSpan.FromHours(1);

            // --- NEW V3 Configuration Properties ---
            public bool EnableGiftCards { get; set; } = true;
            [Range(typeof(TimeSpan), "30.00:00:00", "3650.00:00:00")] public TimeSpan GiftCardExpiryDuration { get; set; } = TimeSpan.FromDays(365 * 2);
            public decimal MaxGiftCardRedemptionPerOrderPercentage { get; set; } = 1.0m; // Allow 100% by default
            public bool EnableStoreCredit { get; set; } = true;

            public bool EnableAdvancedPromotions { get; set; } = true;
            // PromotionEngine configuration might be external or complex object here

            public bool EnableMultiWarehouse { get; set; } = false;
            public List<WarehouseLocation> WarehouseLocations { get; set; } = new();
            public InventoryAllocationStrategy DefaultAllocationStrategy { get; set; } = InventoryAllocationStrategy.OptimizeForCost;
            public Address DefaultShopOriginAddress { get; set; } // Used if not multi-warehouse or for single origin

            public bool EnableSearchIntegration { get; set; } = false; // Use ISearchService?
            public string SearchServiceEndpoint { get; set; }
            public string SearchServiceApiKey { get; set; } // Store securely!

            public bool EnablePaymentPlans { get; set; } = false;
            public string PaymentPlanProviderName { get; set; } = "DefaultProvider"; // E.g. "Afterpay", "Klarna"
            public Dictionary<string, string> PaymentPlanProviderSettings { get; set; } = new(); // API Keys etc.

            public bool EnableAffiliateProgram { get; set; } = false;
            [Range(typeof(TimeSpan), "1.00:00:00", "90.00:00:00")] public TimeSpan AffiliateCookieDuration { get; set; } = TimeSpan.FromDays(30);
            [Range(0.0, 0.5)] public decimal DefaultAffiliateCommissionRate { get; set; } = 0.05m; // 5%
            [Range(typeof(TimeSpan), "1.00:00:00", "7.00:00:00")] public TimeSpan AffiliateProcessingInterval { get; set; } = TimeSpan.FromDays(1);

            public bool EnableStoredPayments { get; set; } = true;

            public bool EnableRateLimiting { get; set; } = true;
            public RateLimitOptions DefaultRateLimit { get; set; } = new RateLimitOptions { PermitLimit = 100, Window = TimeSpan.FromMinutes(1) };
            public Dictionary<string, RateLimitOptions> SpecificRateLimits { get; set; } = new(); // e.g., "PlaceOrder" -> options

            public bool EnableMetrics { get; set; } = true; // Toggle metrics emission
            public bool EnableTracing { get; set; } = true; // Toggle trace emission

            public bool EnableABTesting { get; set; } = false;
            [Range(typeof(TimeSpan), "01:00:00", "24:00:00")] public TimeSpan ExperimentDataSyncInterval { get; set; } = TimeSpan.FromHours(4);

            public bool EnableDataRetentionPolicies { get; set; } = false;
            [Range(typeof(TimeSpan), "90.00:00:00", "3650.00:00:00")] public TimeSpan UserDataRetentionPeriod { get; set; } = TimeSpan.FromDays(365 * 5);
            [Range(typeof(TimeSpan), "1.00:00:00", "30.00:00:00")] public TimeSpan DataRetentionCleanupInterval { get; set; } = TimeSpan.FromDays(7);

            public bool EnableIncrementalSearchIndexSync { get; set; } = false;
            [Range(typeof(TimeSpan), "00:00:10", "01:00:00")] public TimeSpan SearchIndexSyncInterval { get; set; } = TimeSpan.FromMinutes(5);

            // Default static methods remain...
            private static List<LoyaltyTier> GetDefaultLoyaltyTiers() => new() { /* As before */ };
            private static List<AbandonedCartStep> GetDefaultAbandonedCartSequence() => new() { /* As before */ };
        }

        // UserSession (V3 - Minor Additions like Affiliate tracking)
        public class UserSession
        {
            public int PlayerId { get; }
            public HashSet<string> Roles { get; set; } = new();
            public DateTime LastActivity { get; set; }
            public DateTime SessionStartTime { get; }
            public string SessionToken { get; private set; }
            public string IpAddress { get; set; }
            public string UserAgent { get; set; }
            public string LocalePreference { get; set; }
            public string CurrencyPreference { get; set; }
            public GeoLocationInfo GeoLocation { get; set; }
            public Dictionary<string, DateTime> FeatureAccessLog { get; set; } = new();
            // --- NEW V3 ---
            public string? CurrentAffiliateCode { get; set; } // Tracked affiliate for this session
            public Dictionary<string, string> ActiveABTestAssignments { get; set; } = new(); // Key: ExperimentKey -> Treatment

            public UserSession(int playerId, string ipAddress, string userAgent, string defaultLocale, string defaultCurrency, GeoLocationInfo geoInfo)
            {
                PlayerId = playerId;
                var now = DateTime.UtcNow;
                SessionStartTime = now;
                LastActivity = now;
                SessionToken = GenerateSecureToken();
                IpAddress = ipAddress;
                UserAgent = userAgent;
                LocalePreference = defaultLocale;
                CurrencyPreference = defaultCurrency;
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
            public void AssignTreatment(string experimentKey, string treatment) => ActiveABTestAssignments[experimentKey] = treatment;
            public string GetAssignedTreatment(string experimentKey) => ActiveABTestAssignments.GetValueOrDefault(experimentKey);
        }

        // Methods like SetGlobalStatus, UpdateShopStatusAsync remain conceptually similar

        #endregion

        #region --- Core Feature Expansions (V3 Examples - High Level) ---

        // --- Place Order (V3 - EXTREMELY Complex Workflow) ---
        public async Task<ShopOperationResult<Order>> PlaceOrderAsync(int playerId, PlaceOrderRequest orderRequest, CancellationToken cancellationToken = default)
        {
            using var activity = _activitySource.StartActivity(nameof(PlaceOrderAsync), ActivityKind.Server); // Tracing
            var sw = Stopwatch.StartNew();
            Guid orderId = Guid.NewGuid(); // Generate order ID early for logging/locking
            activity?.SetTag("order.id_initial", orderId.ToString());
            activity?.SetTag("player.id", playerId);

            // 0. Rate Limit Check
            if (_config.EnableRateLimiting)
            {
                var rateLimitResult = await _rateLimiter.CheckRateLimitAsync($"PlaceOrder:{playerId}", playerId.ToString(), cancellationToken);
                if (!rateLimitResult.IsAllowed)
                {
                    _logger.LogWarning("Rate limit exceeded for PlaceOrderAsync by Player {PlayerId}.", playerId);
                    activity?.SetTag("failure_reason", "RateLimited");
                    return ShopOperationResult<Order>.Fail("Operation rate limited. Please try again later.", ShopOperationError.RateLimited);
                }
            }

            // 1. Throttle & Basic Validation
            if (!await _operationThrottle.WaitAsync(_config.ApiOperationTimeout, cancellationToken)) // Add Timeout config
            {
                 _logger.LogWarning("Operation throttler timeout for PlaceOrderAsync by Player {PlayerId}.", playerId);
                 activity?.SetTag("failure_reason", "ThrottlerTimeout");
                 return ShopOperationResult<Order>.Fail("System busy, please try again.", ShopOperationError.Timeout);
            }
            try
            {
                if (GlobalStatus != ShopGlobalStatus.Online)
                {
                    activity?.SetTag("failure_reason", "SystemOffline");
                    return ShopOperationResult<Order>.Fail("Shop system is not online.", ShopOperationError.SystemOffline);
                }
                var validationResult = ValidatePlaceOrderRequest(orderRequest); // Needs more V3 validation
                if (!validationResult.IsValid)
                {
                    activity?.SetTag("failure_reason", "InvalidInput");
                    activity?.SetTag("validation_error", validationResult.ErrorMessage);
                    return ShopOperationResult<Order>.Fail(validationResult.ErrorMessage, ShopOperationError.InvalidInput);
                }

                // 2. Load/Verify User Session & Context (including A/B tests, Affiliate Code)
                var userContext = await GetUserContextAsync(playerId, orderRequest.IpAddress, orderRequest.UserAgent, cancellationToken);
                if (userContext == null)
                {
                    activity?.SetTag("failure_reason", "AuthenticationRequired");
                    return ShopOperationResult<Order>.Fail("Invalid user session.", ShopOperationError.AuthenticationRequired);
                }
                userContext.Session.UpdateActivity(nameof(PlaceOrderAsync)); // Mark activity
                activity?.SetTag("user.locale", userContext.Session.LocalePreference);
                activity?.SetTag("user.currency", userContext.Session.CurrencyPreference);
                if(!string.IsNullOrEmpty(userContext.Session.CurrentAffiliateCode)) activity?.SetTag("user.affiliate_code", userContext.Session.CurrentAffiliateCode);

                // 3. Load Cart / Items requested
                var cartItems = await GetCartItemsForOrderAsync(playerId, orderRequest.UseCartId, orderRequest.DirectItems, cancellationToken);
                if (!cartItems.Any())
                {
                    activity?.SetTag("failure_reason", "CartEmpty");
                    return ShopOperationResult<Order>.Fail("Cannot place an order with no items.", ShopOperationError.CartEmpty);
                }
                activity?.SetTag("order.item_count", cartItems.Count);

                // 4. Lock Order ID early to prevent concurrent payment attempts
                var orderLock = GetOrderLock(orderId);
                if (!await orderLock.WaitAsync(_config.LockTimeout, cancellationToken)) // Add LockTimeout config
                {
                     _logger.LogWarning("Timeout acquiring lock for Order {OrderId}.", orderId);
                     activity?.SetTag("failure_reason", "OrderLockTimeout");
                     return ShopOperationResult<Order>.Fail("Failed to process order due to contention, please try again.", ShopOperationError.Concurrency);
                }
                try // Order Lock scope
                {
                    // --- Pre-computation Steps ---
                    // 5. Validate Addresses (Billing/Shipping) if required
                    // ... (Address Validation Logic as before) ...

                    // 6. Preliminary Pricing & Promotion Evaluation
                    // Get base prices, potentially apply customer group pricing BEFORE promotions
                    var pricedCartItems = await GetPricedCartItemsAsync(cartItems, userContext, cancellationToken); // Applies base/group pricing
                    PromotionEvaluationResult promotionResult = null;
                    if (_config.EnableAdvancedPromotions)
                    {
                        promotionResult = await _promotionEngine.EvaluateCartAsync(pricedCartItems, userContext, orderRequest.AppliedCouponCodes, orderRequest.ShippingAddress, cancellationToken);
                        // Apply promotion discounts to pricedCartItems for further calculations
                        ApplyPromotionDiscountsToItems(pricedCartItems, promotionResult);
                        activity?.SetTag("order.promotion_discount", promotionResult?.TotalDiscount ?? 0);
                    }

                    // 7. Calculate Shipping Costs (Requires potentially priced items and destination)
                    ShippingQuoteResult shippingQuotes = null;
                    ShippingMethod selectedShipping = null;
                    if (orderRequest.ShippingAddress != null && pricedCartItems.Any(i => i.ItemType == ItemType.Physical)) // Physical goods
                    {
                        // Consider if multi-warehouse is enabled, need to determine origin(s) first
                        var originLocationId = DetermineShippingOrigin(pricedCartItems, orderRequest.ShippingAddress); // Complex logic V3
                        shippingQuotes = await _shippingService.GetShippingQuotesAsync(originLocationId, orderRequest.ShippingAddress, pricedCartItems, cancellationToken);
                        if (shippingQuotes?.Quotes?.Any() != true && !promotionResult.GrantedFreeShipping) // Allow proceeding if free shipping promo exists
                        {
                            activity?.SetTag("failure_reason", "ShippingError");
                            return ShopOperationResult<Order>.Fail("Could not retrieve shipping quotes.", ShopOperationError.ShippingError);
                        }
                        selectedShipping = shippingQuotes?.Quotes?.FirstOrDefault(q => q.Id == orderRequest.SelectedShippingMethodId);
                        if (selectedShipping == null && !promotionResult.GrantedFreeShipping) // Allow proceeding if free shipping promo exists
                        {
                            activity?.SetTag("failure_reason", "InvalidShippingMethod");
                            return ShopOperationResult<Order>.Fail("Invalid shipping method selected.", ShopOperationError.InvalidInput);
                        }
                         activity?.SetTag("order.shipping_cost", selectedShipping?.Cost ?? 0);
                    }


                    // 8. Calculate Taxes (Requires final item prices after promo, shipping cost, addresses)
                    TaxCalculationResult taxResult = null;
                    // ... (Tax Calculation Logic as before, using pricedCartItems, selectedShipping cost, addresses) ...
                    activity?.SetTag("order.tax_amount", taxResult?.TaxAmount ?? 0);

                    // 9. Calculate FINAL Order Total (incl. store credit/gift card preview if possible)
                    var finalAmounts = CalculateFinalOrderAmounts(pricedCartItems, promotionResult, selectedShipping, taxResult);
                    activity?.SetTag("order.total_amount", finalAmounts.GrandTotal);

                    // 10. Check Inventory Availability & Reserve Items (Consider Multi-Warehouse)
                    var reservationResult = await ReserveInventoryForOrderAsync(pricedCartItems, playerId, DetermineShippingOrigin(pricedCartItems, orderRequest.ShippingAddress), cancellationToken); // Updated V3
                    if (!reservationResult.Success)
                    {
                        activity?.SetTag("failure_reason", "InsufficientStock");
                        activity?.SetTag("stock_error", reservationResult.ErrorMessage);
                        return ShopOperationResult<Order>.Fail($"Inventory unavailable: {reservationResult.ErrorMessage}", ShopOperationError.InsufficientStock);
                    }
                    try // Reservation scope
                    {
                        // 11. Create Initial Order Record (Status: PendingPayment)
                        var order = CreateOrderFromContextV3(orderId, playerId, orderRequest, userContext, pricedCartItems, finalAmounts, promotionResult, selectedShipping, taxResult, reservationResult);
                        await _persistenceService.SaveOrderAsync(order, cancellationToken); // Save initial state
                         activity?.SetTag("order.id", order.Id.ToString()); // Confirm ID used


                        // 12. Fraud Detection (if enabled)
                        // ... (Fraud Detection Logic as before) ...

                        // --- Payment Processing ---
                        decimal amountDue = order.TotalAmount;
                        List<PaymentAttempt> paymentAttempts = new List<PaymentAttempt>();

                        // 13. Apply Store Credit (if enabled and available)
                        if (_config.EnableStoreCredit && orderRequest.UseStoreCreditAmount > 0)
                        {
                            var creditResult = await ApplyStoreCreditToOrderAsync(order, orderRequest.UseStoreCreditAmount, cancellationToken);
                            if (!creditResult.IsSuccess) { /* Handle failure, maybe rollback reservation? */ return ShopOperationResult<Order>.Fail(creditResult.ErrorMessage, creditResult.ErrorType); }
                            amountDue -= creditResult.Data.AmountApplied;
                            paymentAttempts.Add(creditResult.Data);
                        }

                        // 14. Apply Gift Card(s) (if enabled and provided)
                        if (_config.EnableGiftCards && orderRequest.GiftCardCodes?.Any() == true && amountDue > 0)
                        {
                           foreach(var code in orderRequest.GiftCardCodes)
                           {
                                if(amountDue <= 0) break;
                                var gcResult = await ApplyGiftCardToOrderAsync(order, code, Math.Min(amountDue, order.TotalAmount * _config.MaxGiftCardRedemptionPerOrderPercentage), cancellationToken); // Apply cap
                                if (!gcResult.IsSuccess) { /* Handle failure */ return ShopOperationResult<Order>.Fail(gcResult.ErrorMessage, gcResult.ErrorType); }
                                amountDue -= gcResult.Data.AmountApplied;
                                paymentAttempts.Add(gcResult.Data);
                           }
                        }

                        // 15. Process Primary Payment (Gateway or Payment Plan) if amount still due
                        PaymentResult primaryPaymentResult = null;
                        if (amountDue > 0.01m) // Use small tolerance
                        {
                             if(orderRequest.UsePaymentPlan && _config.EnablePaymentPlans)
                             {
                                 // Request quote & authorization from Payment Plan Service
                                 // ... Complex logic involving _paymentPlanService ...
                                 // If successful, primaryPaymentResult = success...
                                 // If failed, update order status and fail
                             }
                             else if (orderRequest.PaymentInfo != null) // Standard Payment Gateway
                             {
                                 var paymentDetails = CreatePaymentDetails(order, orderRequest.PaymentInfo, amountDue); // Pass remaining amount
                                 primaryPaymentResult = await _paymentGateway.ProcessPaymentAsync(paymentDetails, cancellationToken);
                                 paymentAttempts.Add(new PaymentAttempt(PaymentMethodType.Gateway, amountDue, primaryPaymentResult.IsSuccess, primaryPaymentResult.TransactionId, primaryPaymentResult.ErrorMessage));
                             }
                             else
                             {
                                  // No payment method provided for remaining amount
                                  primaryPaymentResult = new PaymentResult { IsSuccess = false, ErrorMessage = "No payment method provided for remaining balance." };
                             }
                        }
                        else // Fully paid by credit/gift card
                        {
                            primaryPaymentResult = new PaymentResult { IsSuccess = true, TransactionId = "InternalCredit" };
                        }


                        // 16. Handle Payment Outcome
                        if (primaryPaymentResult == null || !primaryPaymentResult.IsSuccess)
                        {
                            // Payment failed - Rollback Credit/Gift Card Usage, Release Inventory, Update Order Status
                            await RollbackPaymentAttemptsAsync(order, paymentAttempts, cancellationToken);
                            await ReleaseInventoryReservationAsync(reservationResult.ReservationId, cancellationToken);
                            await UpdateOrderStatusAsync(order.Id, OrderStatus.PaymentFailed, primaryPaymentResult?.ErrorMessage ?? "Payment processing failed.", Constant.SYSTEM_USER_ID, cancellationToken);
                            activity?.SetTag("failure_reason", "PaymentFailed");
                            activity?.SetStatus(ActivityStatusCode.Error, "Payment Failed");
                             _logger.LogError("Payment failed for Order {OrderId}: {Error}", order.Id, primaryPaymentResult?.ErrorMessage);
                            return ShopOperationResult<Order>.Fail($"Payment failed: {primaryPaymentResult?.ErrorMessage}", ShopOperationError.PaymentFailed);
                        }

                        // --- Post-Payment Success ---
                        order.PaymentTransactionId = primaryPaymentResult.TransactionId; // Record primary transaction ID
                        order.PaymentProvider = orderRequest.UsePaymentPlan ? _config.PaymentPlanProviderName : _paymentGateway.Name;
                        order.PaymentAttempts = paymentAttempts; // Store attempt details

                        // 17. Update Order Status (Processing/Completed)
                        bool allDigitalOrService = pricedCartItems.All(i => i.ItemType == ItemType.Digital || i.ItemType == ItemType.Service || i.ItemType == ItemType.GiftCard);
                        var nextStatus = allDigitalOrService ? OrderStatus.Completed : OrderStatus.Processing;
                        await UpdateOrderStatusAsync(order.Id, nextStatus, "Payment successful", Constant.SYSTEM_USER_ID, cancellationToken);

                        // 18. Finalize Inventory Update (Confirm reservation -> Deduct stock)
                        await FinalizeInventoryUpdateAsync(reservationResult.ReservationId, order.Id, cancellationToken);

                        // 19. Trigger Post-Order Actions (Asynchronous)
                        var postOrderTasks = TriggerPostOrderProcessing(order, userContext, orderRequest, pricedCartItems, allDigitalOrService);

                        // Log potential errors but don't wait
                        _ = Task.WhenAll(postOrderTasks).ContinueWith(t => {
                            if (t.IsFaulted) _logger.LogError(t.Exception, "Error during V3 post-order background tasks for Order {OrderId}", order.Id);
                        }, CancellationToken.None);


                        // 20. Return Success
                        sw.Stop();
                        _logger.LogInformation("Order {OrderId} placed successfully for Player {PlayerId} in {ElapsedMilliseconds}ms.", order.Id, playerId, sw.ElapsedMilliseconds);
                         _ordersPlacedCounter.Add(1, new KeyValuePair<string, object>("shop.id", order.ShopId)); // Metrics
                         _orderProcessingDurationHistogram.Record(sw.Elapsed.TotalMilliseconds); // Metrics
                         activity?.SetTag("status", "Success");
                         activity?.SetStatus(ActivityStatusCode.Ok);
                         activity?.SetTag("duration_ms", sw.ElapsedMilliseconds);

                        return ShopOperationResult<Order>.Success(order);

                    } // End Reservation Scope Try
                    finally
                    {
                        // Ensure reservation is released if anything failed *after* reservation but *before* finalization
                        if (reservationResult != null && reservationResult.Success && !reservationResult.IsFinalized)
                        {
                             _logger.LogWarning("Releasing inventory reservation {ReservationId} due to incomplete order flow for Order {OrderId}.", reservationResult.ReservationId, orderId);
                            await ReleaseInventoryReservationAsync(reservationResult.ReservationId, cancellationToken);
                        }
                    }

                } // End Order Lock Scope Try
                finally
                {
                    orderLock.Release();
                }
            } // End Throttle Scope Try
            catch (OperationCanceledException ex)
            {
                 _logger.LogWarning(ex, "PlaceOrderAsync cancelled for Player {PlayerId}.", playerId);
                 activity?.SetTag("status", "Cancelled");
                 activity?.SetStatus(ActivityStatusCode.Error, "Operation Cancelled");
                 return ShopOperationResult<Order>.Fail("Operation cancelled.", ShopOperationError.Cancelled);
            }
            catch (Exception ex)
            {
                sw.Stop();
                 _logger.LogError(ex, "Unexpected error during PlaceOrderAsync for Player {PlayerId} after {ElapsedMilliseconds}ms. OrderId attempt: {OrderId}", playerId, sw.ElapsedMilliseconds, orderId);
                 activity?.SetTag("status", "Error");
                 activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
                 // Attempt to log audit failure
                 _ = LogAuditEventAsync(null, playerId, "Order", AuditAction.OrderPlacementFailed, $"Unexpected error: {ex.Message}", CancellationToken.None, orderRequest?.IpAddress, orderId);
                 return ShopOperationResult<Order>.Fail($"An unexpected error occurred: {ex.Message}", ShopOperationError.Unknown);
            }
            finally
            {
                _operationThrottle.Release();
            }
        }


        // --- Search Items (V3 - Using ISearchService) ---
        public async Task<ShopOperationResult<ItemSearchResult>> SearchItemsAsync(SearchQuery query, UserContext userContext, CancellationToken cancellationToken = default)
        {
            using var activity = _activitySource.StartActivity(nameof(SearchItemsAsync));
            activity?.SetTag("search.keywords", query.Keywords);
            activity?.SetTag("search.shop_id", query.ShopId);

            // 0. Rate Limit Check (Optional - depends if search is protected)
            // ... Rate Limiter Call ...

            // 1. Throttle & Basic Validation
            if (!await _operationThrottle.WaitAsync(_config.ApiOperationTimeout, cancellationToken))
                 return ShopOperationResult<ItemSearchResult>.Fail("System busy.", ShopOperationError.Timeout);
            try
            {
                 if (GlobalStatus == ShopGlobalStatus.ShuttingDown || GlobalStatus == ShopGlobalStatus.Error)
                     return ShopOperationResult<ItemSearchResult>.Fail("Shop system unavailable.", ShopOperationError.SystemOffline);

                 // Validate query object (e.g., PageSize limits)
                 if (query.PageSize > _config.MaxSearchResultsPerPage) // Add config setting
                 {
                     _logger.LogWarning("Search query requested PageSize {RequestedSize} exceeding limit {Limit}.", query.PageSize, _config.MaxSearchResultsPerPage);
                     query = query with { PageSize = _config.MaxSearchResultsPerPage }; // Clamp page size
                     activity?.SetTag("warning", "PageSizeClamped");
                 }

                 // 2. Delegate to Search Service
                 if (!_config.EnableSearchIntegration)
                 {
                     // Fallback to basic in-memory search if needed (or return error)
                     _logger.LogWarning("Search attempted but ISearchService integration is disabled in configuration.");
                     activity?.SetTag("warning", "SearchIntegrationDisabled");
                     return ShopOperationResult<ItemSearchResult>.Fail("Search functionality is currently limited.", ShopOperationError.FeatureDisabled);
                 }

                 var searchResult = await _searchService.SearchItemsAsync(query, userContext, cancellationToken);

                 activity?.SetTag("search.results_count", searchResult.Data?.TotalCount ?? 0);
                 activity?.SetTag("search.page_index", query.PageIndex);
                 activity?.SetTag("search.page_size", query.PageSize);
                 if(!searchResult.IsSuccess) activity?.SetStatus(ActivityStatusCode.Error, searchResult.ErrorMessage);

                 return searchResult;
            }
            catch (OperationCanceledException ex)
            {
                 _logger.LogWarning(ex, "SearchItemsAsync cancelled. Query: {@Query}", query);
                  activity?.SetStatus(ActivityStatusCode.Error, "Operation Cancelled");
                 return ShopOperationResult<ItemSearchResult>.Fail("Operation cancelled.", ShopOperationError.Cancelled);
            }
            catch (Exception ex)
            {
                 _logger.LogError(ex, "Error during SearchItemsAsync. Query: {@Query}", query);
                  activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
                 return ShopOperationResult<ItemSearchResult>.Fail($"An unexpected error occurred during search: {ex.Message}", ShopOperationError.Unknown);
            }
            finally
            {
                _operationThrottle.Release();
            }
        }

        // --- GDPR Data Request ---
        public async Task<ShopOperationResult<GdprDataResult>> RequestGdprDataAsync(int playerId, GdprRequestType requestType, CancellationToken cancellationToken = default)
        {
             using var activity = _activitySource.StartActivity(nameof(RequestGdprDataAsync));
             activity?.SetTag("player.id", playerId);
             activity?.SetTag("gdpr.request_type", requestType.ToString());

             if (!_config.EnableGDPRTools)
                 return ShopOperationResult<GdprDataResult>.Fail("GDPR tools are not enabled.", ShopOperationError.FeatureDisabled);

             if (!await CheckPermissionAsync(playerId, Permissions.RequestOwnData, playerId, cancellationToken)) // Self-service permission
                 return ShopOperationResult<GdprDataResult>.Fail("Permission denied.", ShopOperationError.PermissionDenied);

             // TODO: Implement actual data gathering/deletion logic
             // This would involve querying IShopPersistenceService for all data related to the player
             // Orders, Profile, Reviews, Addresses, Subscriptions, Loyalty, Cart, Sessions, RMAs, etc.
             // For deletion, it's complex due to relational data and potential legal holds. Usually involves anonymization or soft-delete first.
             // For export, gather data and format it (e.g., JSON).

              _logger.LogInformation("Processing GDPR Request for Player {PlayerId}, Type: {RequestType}", playerId, requestType);

             // Simulate queuing the request
             var trackingId = Guid.NewGuid();
             _ = LogAuditEventAsync(null, playerId, "GDPR", requestType == GdprRequestType.ExportData ? AuditAction.GdprExportRequested : AuditAction.GdprDeletionRequested, $"Request Tracking ID: {trackingId}", cancellationToken);
             // Publish event for a background worker to handle actual processing
             await _eventBus.PublishAsync(new UserGdprRequestQueuedEvent(playerId, requestType, trackingId), cancellationToken);

             return ShopOperationResult<GdprDataResult>.Success(new GdprDataResult(trackingId, "Request received and queued for processing. You will be notified upon completion."));
        }

        // --- Bulk Product Operation ---
        public async Task<ShopOperationResult<Guid>> StartBulkProductOperationAsync(int adminPlayerId, int shopId, BulkOperationType operationType, Stream dataStream, string originalFileName, CancellationToken cancellationToken = default)
        {
             using var activity = _activitySource.StartActivity(nameof(StartBulkProductOperationAsync));
             activity?.SetTag("admin.id", adminPlayerId);
             activity?.SetTag("shop.id", shopId);
             activity?.SetTag("bulk.operation_type", operationType.ToString());
             activity?.SetTag("bulk.filename", originalFileName);

             if (!await CheckPermissionAsync(adminPlayerId, Permissions.ManageProducts, shopId, cancellationToken))
                 return ShopOperationResult<Guid>.Fail("Permission denied.", ShopOperationError.PermissionDenied);

             // TODO: Implement proper handling:
             // 1. Validate file type/size.
             // 2. Upload the stream data to temporary storage (e.g., blob storage).
             // 3. Queue a background job (using message queue preferably) to process the file from storage.
             // 4. Store job status (using IShopPersistenceService or dedicated job service).
             // 5. Return a Job ID for tracking.

             _logger.LogInformation("Admin {AdminId} initiated Bulk Operation {OperationType} for Shop {ShopId}, File: {FileName}", adminPlayerId, operationType, shopId, originalFileName);

             var jobId = Guid.NewGuid();
             // Simulate queuing
             await _persistenceService.SaveBulkOperationJobAsync(new BulkOperationJob(jobId, shopId, adminPlayerId, operationType, originalFileName, BulkJobStatus.Queued), cancellationToken);
             await _eventBus.PublishAsync(new BulkOperationRequestedEvent(jobId), cancellationToken); // Trigger background worker
             await LogAuditEventAsync(shopId, adminPlayerId, "BulkOps", AuditAction.BulkOperationQueued, $"JobId: {jobId}, Type: {operationType}, File: {originalFileName}", cancellationToken);

             return ShopOperationResult<Guid>.Success(jobId);
        }

        public async Task<ShopOperationResult<BulkOperationStatus>> GetBulkProductOperationStatusAsync(int playerId, Guid jobId, CancellationToken cancellationToken = default)
        {
             using var activity = _activitySource.StartActivity(nameof(GetBulkProductOperationStatusAsync));
             activity?.SetTag("player.id", playerId);
             activity?.SetTag("bulk.job_id", jobId);

             var job = await _persistenceService.GetBulkOperationJobAsync(jobId, cancellationToken);
             if (job == null) return ShopOperationResult<BulkOperationStatus>.Fail("Job not found.", ShopOperationError.NotFound);

             // Check permission (owner or original requester)
             if (!await CheckPermissionAsync(playerId, Permissions.ViewBulkOpsStatus, job.ShopId, cancellationToken) && job.RequestedByPlayerId != playerId)
                 return ShopOperationResult<BulkOperationStatus>.Fail("Permission denied.", ShopOperationError.PermissionDenied);

             return ShopOperationResult<BulkOperationStatus>.Success(
                 new BulkOperationStatus(job.JobId, job.Status, job.ProgressPercentage, job.ResultMessage, job.ResultFileUrl, job.TimestampCompleted)
             );
        }

        // --- Other NEW V3 Public Methods ---
        // Add methods for Gift Cards, Promotions, Stock Subscriptions, Affiliate, Stored Payments, A/B Testing, etc.
        // Example Signatures:
        public async Task<ShopOperationResult<GiftCard>> PurchaseGiftCardAsync(/*...params...*/ CancellationToken cancellationToken = default) { /* ... validation, call _giftCardService ... */ }
        public async Task<ShopOperationResult> SubscribeToStockNotificationAsync(int playerId, int itemId, string sku, CancellationToken cancellationToken = default) { /* ... validation, call _stockSubscriptionService ... */ }
        public async Task<ShopOperationResult<IEnumerable<PromotionDefinition>>> GetApplicablePromotionsAsync(int? shopId, int playerId, CancellationToken cancellationToken = default) { /* ... get user context, call _promotionEngine ... */ }
        public async Task<ShopOperationResult<IEnumerable<StoredPaymentMethod>>> GetSavedPaymentsAsync(int playerId, CancellationToken cancellationToken = default) { /* ... validation, call _userStoredPaymentService ... */ }
        public async Task TrackAffiliateVisitAsync(string affiliateCode, string ipAddress, string userAgent, string landingPage, CancellationToken cancellationToken = default) { /* ... validation, call _affiliateService ... */ }

        #endregion

        #region --- Background Task Implementations (V3 Expanded Stubs) ---

        // DisposeTimers needs to dispose NEW V3 timers
        private void DisposeTimers()
        {
             _healthMonitorTimer?.Dispose(); _healthMonitorTimer = null;
             _abandonedCartTimer?.Dispose(); _abandonedCartTimer = null;
             _autoRestockTimer?.Dispose(); _autoRestockTimer = null;
             _auctionProcessingTimer?.Dispose(); _auctionProcessingTimer = null;
             _recommendationModelUpdateTimer?.Dispose(); _recommendationModelUpdateTimer = null;
             _scheduledSalesTimer?.Dispose(); _scheduledSalesTimer = null; // Obsolete?
             _subscriptionBillingTimer?.Dispose(); _subscriptionBillingTimer = null;
             _statusUpdateFromScheduleTimer?.Dispose(); _statusUpdateFromScheduleTimer = null;
             _dataArchivalTimer?.Dispose(); _dataArchivalTimer = null;
             _rmaProcessingTimer?.Dispose(); _rmaProcessingTimer = null;
             _loyaltyTierExpiryTimer?.Dispose(); _loyaltyTierExpiryTimer = null;
             _reportGenerationWorkerTimer?.Dispose(); _reportGenerationWorkerTimer = null;
             _stockSubscriptionNotifierTimer?.Dispose(); _stockSubscriptionNotifierTimer = null; // V3
             _affiliateProcessingTimer?.Dispose(); _affiliateProcessingTimer = null; // V3
             _experimentDataSyncTimer?.Dispose(); _experimentDataSyncTimer = null; // V3
             _dataRetentionCleanupTimer?.Dispose(); _dataRetentionCleanupTimer = null; // V3
             _searchIndexSyncTimer?.Dispose(); _searchIndexSyncTimer = null; // V3
        }

        // StartBackgroundTasks needs to create NEW V3 timers
        private void StartBackgroundTasks()
        {
            _logger.LogInformation("Starting V3 enhanced background tasks...");
            Stopwatch sw = Stopwatch.StartNew();
            DisposeTimers(); // Ensure clean start

            // Timer Creation Helper (Robust Error Handling)
            Timer CreateTimer(Func<object, Task> callback, TimeSpan dueTime, TimeSpan period, string taskName, bool runImmediately = false)
            {
                if (period <= TimeSpan.Zero) // Prevent zero/negative periods
                {
                    _logger.LogWarning("Background task '{TaskName}' configured with invalid period {Period}. Task will not run periodically.", taskName, period);
                    period = Timeout.InfiniteTimeSpan; // Run only once if dueTime is valid
                }

                 // Don't start timer if initial due time is invalid (e.g. negative) unless RunImmediately is true
                 if (dueTime < TimeSpan.Zero && !runImmediately)
                 {
                     _logger.LogWarning("Background task '{TaskName}' configured with invalid due time {DueTime}. Task will not start.", taskName, dueTime);
                     return null;
                 }

                 var effectiveDueTime = runImmediately ? TimeSpan.Zero : (dueTime < TimeSpan.Zero ? TimeSpan.Zero : dueTime); // Start immediately if requested or dueTime invalid but runOnce

                return new Timer(async state =>
                {
                    var token = (CancellationToken)state;
                    if (token.IsCancellationRequested) return;

                    _logger.LogDebug("Background task '{TaskName}' starting.", taskName);
                    var taskSw = Stopwatch.StartNew();
                    try
                    {
                        // Optional: Add rate limiting or concurrency control per task type if needed
                        await callback(token); // Execute the actual task logic
                        taskSw.Stop();
                        _logger.LogDebug("Background task '{TaskName}' completed successfully in {ElapsedMilliseconds}ms.", taskName, taskSw.ElapsedMilliseconds);
                    }
                    catch (OperationCanceledException)
                    {
                        taskSw.Stop();
                         _logger.LogWarning("Background task '{TaskName}' was cancelled after {ElapsedMilliseconds}ms.", taskName, taskSw.ElapsedMilliseconds);
                    }
                    catch (Exception ex)
                    {
                        taskSw.Stop();
                         _logger.LogError(ex, "Background task '{TaskName}' failed after {ElapsedMilliseconds}ms.", taskName, taskSw.ElapsedMilliseconds);
                         // Consider adding circuit breaker logic or specific error handling here
                    }
                }, _cts.Token, effectiveDueTime, period);
            }

            // --- Standard Timers ---
            _healthMonitorTimer = CreateTimer(MonitorHealthAsync, TimeSpan.FromSeconds(30), _config.HealthCheckInterval, "HealthMonitor");
            _abandonedCartTimer = CreateTimer(CheckAbandonedCartsAsync, TimeSpan.FromMinutes(2), _config.AbandonedCartCheckInterval, "AbandonedCart");
            _autoRestockTimer = CreateTimer(ProcessAutoRestockAsync, TimeSpan.FromMinutes(5), _config.AutoRestockCheckInterval, "AutoRestock");
            _auctionProcessingTimer = CreateTimer(ProcessAuctionsAsync, TimeSpan.FromSeconds(10), _config.AuctionProcessingInterval, "AuctionProcessing");
            _recommendationModelUpdateTimer = CreateTimer(UpdateRecommendationModelAsync, TimeSpan.FromMinutes(15), _config.RecommendationModelUpdateInterval, "RecommendationModelUpdate");
            _subscriptionBillingTimer = CreateTimer(ProcessSubscriptionBillingAsync, TimeSpan.FromMinutes(5), _config.SubscriptionBillingInterval, "SubscriptionBilling");
            _statusUpdateFromScheduleTimer = CreateTimer(ProcessStatusUpdatesFromScheduleAsync, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(5), "StatusUpdateFromSchedule"); // Example: Fixed 5 min interval
            _dataArchivalTimer = CreateTimer(ProcessDataArchivalAsync, TimeSpan.FromHours(1), _config.DataArchivalInterval, "DataArchival");
            _rmaProcessingTimer = CreateTimer(ProcessPendingRmasAsync, TimeSpan.FromMinutes(5), _config.RmaProcessingInterval, "RmaProcessing");
            _loyaltyTierExpiryTimer = CreateTimer(CheckLoyaltyTierExpiriesAsync, TimeSpan.FromHours(1), _config.LoyaltyTierExpiryInterval, "LoyaltyTierExpiry");
            _reportGenerationWorkerTimer = CreateTimer(ProcessQueuedReportsAsync, TimeSpan.FromMinutes(1), _config.ReportGenerationInterval, "ReportGeneration");
            _stockSubscriptionNotifierTimer = CreateTimer(ProcessStockNotificationsAsync, TimeSpan.FromMinutes(15), _config.StockSubscriptionNotifyInterval, "StockSubscriptionNotifier"); // V3

            // --- NEW V3 Timers ---
            if (_config.EnableAffiliateProgram)
                 _affiliateProcessingTimer = CreateTimer(ProcessAffiliateActionsAsync, TimeSpan.FromHours(1), _config.AffiliateProcessingInterval, "AffiliateProcessing");
             if (_config.EnableABTesting)
                 _experimentDataSyncTimer = CreateTimer(SyncExperimentDefinitionsAsync, TimeSpan.FromMinutes(5), _config.ExperimentDataSyncInterval, "ExperimentDataSync");
             if (_config.EnableDataRetentionPolicies)
                 _dataRetentionCleanupTimer = CreateTimer(ProcessDataRetentionCleanupAsync, TimeSpan.FromHours(2), _config.DataRetentionCleanupInterval, "DataRetentionCleanup");
             if (_config.EnableSearchIntegration && _config.EnableIncrementalSearchIndexSync)
                 _searchIndexSyncTimer = CreateTimer(ProcessSearchIndexSyncAsync, TimeSpan.FromMinutes(1), _config.SearchIndexSyncInterval, "SearchIndexSync");


            sw.Stop();
            _logger.LogInformation("V3 background tasks started/configured in {ElapsedMilliseconds}ms.", sw.ElapsedMilliseconds);
        }

        // RestartBackgroundTasksIfNeeded needs to check NEW V3 config intervals
        private void RestartBackgroundTasksIfNeeded(ShopConfiguration oldConfig, ShopConfiguration newConfig)
        {
            // Check if any relevant interval or enable flag changed
            bool needsRestart =
                oldConfig.HealthCheckInterval != newConfig.HealthCheckInterval ||
                oldConfig.AbandonedCartCheckInterval != newConfig.AbandonedCartCheckInterval ||
                oldConfig.AutoRestockCheckInterval != newConfig.AutoRestockCheckInterval ||
                oldConfig.AuctionProcessingInterval != newConfig.AuctionProcessingInterval ||
                oldConfig.RecommendationModelUpdateInterval != newConfig.RecommendationModelUpdateInterval ||
                oldConfig.SubscriptionBillingInterval != newConfig.SubscriptionBillingInterval ||
                oldConfig.DataArchivalInterval != newConfig.DataArchivalInterval ||
                oldConfig.RmaProcessingInterval != newConfig.RmaProcessingInterval ||
                oldConfig.LoyaltyTierExpiryInterval != newConfig.LoyaltyTierExpiryInterval ||
                oldConfig.ReportGenerationInterval != newConfig.ReportGenerationInterval ||
                oldConfig.StockSubscriptionNotifyInterval != newConfig.StockSubscriptionNotifyInterval || // V3
                oldConfig.EnableAffiliateProgram != newConfig.EnableAffiliateProgram || // V3 Enable/Disable
                oldConfig.AffiliateProcessingInterval != newConfig.AffiliateProcessingInterval || // V3 Interval
                oldConfig.EnableABTesting != newConfig.EnableABTesting || // V3 Enable/Disable
                oldConfig.ExperimentDataSyncInterval != newConfig.ExperimentDataSyncInterval || // V3 Interval
                oldConfig.EnableDataRetentionPolicies != newConfig.EnableDataRetentionPolicies || // V3 Enable/Disable
                oldConfig.DataRetentionCleanupInterval != newConfig.DataRetentionCleanupInterval || // V3 Interval
                oldConfig.EnableSearchIntegration != newConfig.EnableSearchIntegration || // V3 Enable/Disable
                oldConfig.EnableIncrementalSearchIndexSync != newConfig.EnableIncrementalSearchIndexSync || // V3 Enable/Disable
                oldConfig.SearchIndexSyncInterval != newConfig.SearchIndexSyncInterval; // V3 Interval
                // Add checks for other intervals if needed

            if (needsRestart)
            {
                _logger.LogInformation("Configuration change detected requires restarting V3 background tasks.");
                StartBackgroundTasks(); // Re-evaluates all timers based on new config
            }
        }

        // --- Placeholder Async Methods for Background Tasks (Existing & NEW V3) ---
        private Task MonitorHealthAsync(object state) { /* Check dependencies, DB, cache, queues, update GlobalStatus */ return Task.CompletedTask; }
        private Task CheckAbandonedCartsAsync(object state) { /* Find old carts, trigger IUserNotificationService based on AbandonedCartSequence */ return Task.CompletedTask; }
        private Task ProcessAutoRestockAsync(object state) { /* Check shops needing restock based on rules/schedules, update inventory */ return Task.CompletedTask; }
        private Task ProcessAuctionsAsync(object state) { /* Find ending auctions, determine winners, create orders/notify */ return Task.CompletedTask; }
        private Task UpdateRecommendationModelAsync(object state) { /* Trigger RecommendationEngine update/retraining */ return Task.CompletedTask; }
        private Task ProcessSubscriptionBillingAsync(object state) { /* Find subscriptions due for renewal, attempt payment, update status */ return Task.CompletedTask; }
        private Task ProcessStatusUpdatesFromScheduleAsync(object state) { /* Check Shop Schedules, update ShopInstanceStatus */ return Task.CompletedTask; }
        private Task ProcessDataArchivalAsync(object state) { /* Find old orders/logs, archive to cold storage via IShopPersistenceService */ return Task.CompletedTask; }
        private Task ProcessPendingRmasAsync(object state) { /* Find RMAs needing action, call IRmaService */ return Task.CompletedTask; }
        private Task CheckLoyaltyTierExpiriesAsync(object state) { /* Check user loyalty tiers, potentially downgrade, notify */ return Task.CompletedTask; }
        private Task ProcessQueuedReportsAsync(object state) { /* Check reports queued via IReportingService, generate, update status */ return Task.CompletedTask; }
        private Task ProcessStockNotificationsAsync(object state) { /* Check recent stock changes, find subscriptions via IStockSubscriptionService, notify users */ return Task.CompletedTask; } // V3
        private Task ProcessAffiliateActionsAsync(object state) { /* Call IAffiliateService for payout processing or cleanup */ return Task.CompletedTask; } // NEW V3
        private async Task SyncExperimentDefinitionsAsync(object state) // NEW V3
        {
             _logger.LogTrace("Syncing A/B experiment definitions...");
             var experiments = await _abTestingService.GetActiveExperimentsAsync((CancellationToken)state);
             _activeExperiments.Clear();
             foreach(var exp in experiments) _activeExperiments.TryAdd(exp.ExperimentKey, exp);
             _logger.LogDebug("Synced {Count} active A/B experiments.", _activeExperiments.Count);
        }
        private Task ProcessDataRetentionCleanupAsync(object state) { /* Find data exceeding retention policies (GDPR), trigger anonymization/deletion via persistence */ return Task.CompletedTask; } // NEW V3
        private Task ProcessSearchIndexSyncAsync(object state) { /* Find recently changed products/inventory, call ISearchService.IndexItemAsync */ return Task.CompletedTask; } // NEW V3

        #endregion

        #region Helper Methods (V3 Expanded)

        // Lock Getters remain similar (GetShopLock, GetOrderLock, GetAuctionLock, GetRmaLock)
        private SemaphoreSlim GetGiftCardLock(string code) => _giftCardLocks.GetOrAdd(code, _ => new SemaphoreSlim(1, 1)); // V3
        private SemaphoreSlim GetUserCreditLock(int playerId) => _userCreditLocks.GetOrAdd(playerId, _ => new SemaphoreSlim(1, 1)); // V3


        // LoadShopAsync: Needs to handle loading NEW V3 state properties
        private async Task<bool> LoadShopAsync(int shopId, CancellationToken cancellationToken)
        {
             using var activity = _activitySource.StartActivity(nameof(LoadShopAsync));
             activity?.SetTag("shop.id", shopId);
             _logger.LogDebug("Loading V3 data for Shop {ShopId}...", shopId);
             try
             {
                 // Load base shop data
                 ShopData data = await _persistenceService.LoadShopDataAsync(shopId, cancellationToken);
                 ShopState state = (data != null)
                     ? ConvertFromShopData(shopId, data) // Conversion needs massive update for V3
                     : new ShopState(shopId, _config) { Status = ShopInstanceStatus.Closed }; // Default closed if not found

                 // Merge global defaults into shop-specific config if needed
                 MergeConfigs(state.Configuration, _config); // Ensure shop-specific overrides are applied correctly

                 // --- Load V3 Specific Shop Data ---
                 if (state.Status != ShopInstanceStatus.Closed && state.Status != ShopInstanceStatus.Deleted) // Only load extra data for active shops
                 {
                      // Load Multi-Warehouse Stock if enabled for this shop
                      if(state.IsMultiWarehouseEnabled)
                      {
                           var stockData = await _persistenceService.LoadShopStockByLocationAsync(shopId, cancellationToken);
                           if(stockData != null)
                           {
                                foreach(var kvp in stockData)
                                {
                                     state.StockByLocation.TryAdd(kvp.Key, kvp.Value);
                                }
                           }
                      }
                      // Load shop-specific promotions? (Or handle globally/via engine)
                      // Load shop pickup locations
                      state.PickupLocations.AddRange(await _persistenceService.LoadShopPickupLocationsAsync(shopId, cancellationToken));
                 }


                 _shopStates[shopId] = state;
                 await UpdateCachedReputationScoreAsync(shopId, state); // Example of derived data caching

                 _logger.LogDebug("Shop {ShopId} V3 data loaded. Status: {Status}, MultiWarehouse: {MW}", shopId, state.Status, state.IsMultiWarehouseEnabled);
                  activity?.SetTag("status", "Success");
                 return true;
             }
             catch (Exception ex)
             {
                 _logger.LogError(ex, "Failed to load V3 data for Shop {ShopId}", shopId);
                 // Optionally mark shop as Error state in memory
                 _shopStates[shopId] = new ShopState(shopId, _config) { Status = ShopInstanceStatus.ErrorLoading }; // Add new status
                  activity?.SetTag("status", "Error");
                  activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
                 return false;
             }
        }

        // LoadGlobalCouponsAsync - Potentially less relevant if PromotionEngine is primary
        // LoadFeatureFlagStatesAsync - Load from _config or persistence
        private async Task LoadFeatureFlagStatesAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug("Loading feature flag states...");
            _featureFlags.Clear();
            // Prefer loading from persistence or dedicated service, fallback to config
            var flags = await _persistenceService.LoadAllFeatureFlagsAsync(cancellationToken) ?? _config.FeatureFlags ?? new Dictionary<string, bool>();
            foreach (var flag in flags)
            {
                _featureFlags.TryAdd(flag.Key, flag.Value);
            }
            _logger.LogInformation("Loaded {Count} feature flags.", _featureFlags.Count);
        }

        // LoadSupportedLocalesAsync - From config or persistence
        private async Task LoadSupportedLocalesAsync(CancellationToken cancellationToken) { /* As before */ }

        // NEW V3: Load Active Experiments
        private async Task LoadActiveExperimentsAsync(CancellationToken cancellationToken)
        {
             if (!_config.EnableABTesting) return;
             _logger.LogDebug("Loading active A/B experiments...");
             _activeExperiments.Clear();
             var experiments = await _abTestingService.GetActiveExperimentsAsync(cancellationToken);
             foreach (var exp in experiments ?? Enumerable.Empty<ExperimentDefinition>())
             {
                 _activeExperiments.TryAdd(exp.ExperimentKey, exp);
             }
             _logger.LogInformation("Loaded {Count} active A/B experiments.", _activeExperiments.Count);
        }

        // LoadActiveSubscriptionsAsync - As before
        // LoadActiveRmasAsync - As before

        // DTOs & Conversion: Need significant expansion for all new V3 classes/state
        // ShopData, ConvertToShopData, ConvertFromShopData need massive updates

        // CheckPermissionAsync: Needs enhancement for NEW V3 permissions
        public async Task<bool> CheckPermissionAsync(int playerId, string permission, object? resourceContext = null, CancellationToken cancellationToken = default)
        {
            using var activity = _activitySource.StartActivity(nameof(CheckPermissionAsync));
            activity?.SetTag("player.id", playerId);
            activity?.SetTag("permission", permission);

            // 1. Get User Context (Roles)
            var userContext = await GetUserContextAsync(playerId, null, null, cancellationToken); // IP/UA not needed for permission check
            if (userContext == null) return false; // User not found/invalid session

            // 2. Check Super Admin Role
            if (userContext.Session.HasRole(Roles.SuperAdmin)) return true; // Super admins bypass checks

            // 3. Check Specific Roles / Permissions
            // This should ideally delegate to a dedicated Authorization Service or use policy-based auth
            // Simple role check example:
            bool hasPermission = permission switch
            {
                Permissions.ManageShops => userContext.Session.HasRole(Roles.ShopAdmin),
                Permissions.ManageOwnShop => userContext.Session.HasRole(Roles.ShopOwner) && IsUserOwnerOfShop(userContext, resourceContext),
                Permissions.PlaceOrder => true, // All authenticated users can place orders (further checks in place order logic)
                Permissions.ManageProducts => userContext.Session.HasRole(Roles.ShopAdmin) || (userContext.Session.HasRole(Roles.ShopOwner) && IsUserOwnerOfShop(userContext, resourceContext)),
                Permissions.ManageRMAs => userContext.Session.HasRole(Roles.SupportAgent) || userContext.Session.HasRole(Roles.ShopAdmin),
                Permissions.RequestOwnData => true, // Player requesting their own data
                Permissions.ManageUsers => userContext.Session.HasRole(Roles.UserAdmin) || userContext.Session.HasRole(Roles.SuperAdmin),
                Permissions.ManagePromotions => userContext.Session.HasRole(Roles.MarketingAdmin) || userContext.Session.HasRole(Roles.SuperAdmin),
                Permissions.ManageGiftCards => userContext.Session.HasRole(Roles.FinanceAdmin) || userContext.Session.HasRole(Roles.SuperAdmin),
                Permissions.ViewAdminDashboard => userContext.Session.HasRole(Roles.AdminReadOnly) || userContext.Session.HasRole(Roles.SupportAgent) || userContext.Session.HasRole(Roles.ShopAdmin) /* etc */,
                // ... add all other permissions ...
                _ => false // Default deny
            };

             activity?.SetTag("permission_granted", hasPermission);
            if (!hasPermission) _logger.LogDebug("Permission {Permission} denied for Player {PlayerId}.", permission, playerId);
            return hasPermission;
        }

        // Helper for CheckPermissionAsync
        private bool IsUserOwnerOfShop(UserContext userContext, object? resourceContext)
        {
             if (resourceContext is int shopId)
             {
                 return _shopStates.TryGetValue(shopId, out var shopState) && shopState.OwnerPlayerId == userContext.PlayerId.ToString();
             }
             if (resourceContext is ShopItem item) // If resource is an item
             {
                  return _shopStates.TryGetValue(item.ShopId, out var shopState) && shopState.OwnerPlayerId == userContext.PlayerId.ToString();
             }
             // Add checks for Order, RMA, etc. if needed
             return false;
        }


        // LogAuditEventAsync: Add new V3 AuditAction cases
        public enum AuditAction { /* ... Existing V2 + NEW V3 ... */
            GiftCardCreated, GiftCardRedeemed, StoreCreditAdjusted, PromotionApplied, PromotionCreated, PromotionDeactivated,
            StockSubscriptionCreated, StockNotificationSent, AffiliateVisitTracked, AffiliateConversion, AffiliatePayout,
            PaymentPlanQuoted, PaymentPlanAuthorized, StoredPaymentAdded, StoredPaymentRemoved, RateLimitTriggered,
            ABTestTreatmentAssigned, ABTestConversion, GdprExportRequested, GdprDeletionRequested, BulkOperationQueued, BulkOperationProcessed,
            SearchPerformed, LoginSuccess, LoginFailed, ConfigUpdated, ShopCreated, UserBanned, OrderPlacementFailed // Added failure case
        }
        // AuditLogEntry needs fields like Timestamp, PlayerId, ActorId, IPAddress, Action, Category, EntityType, EntityId, Details, OldValue, NewValue
        // LogAuditEventAsync implementation remains similar but uses enriched AuditLogEntry

        // Get User Context Helper (V3 Expanded - Affiliate, A/B Tests)
        private async Task<UserContext> GetUserContextAsync(int playerId, string ipAddress, string userAgent, CancellationToken cancellationToken)
        {
             using var activity = _activitySource.StartActivity(nameof(GetUserContextAsync));
             activity?.SetTag("player.id", playerId);

            // Try loading from active session cache first
            if (_userSessions.TryGetValue(playerId, out var session) && !session.IsExpired(_config.SessionTimeout))
            {
                // Update dynamic parts if provided
                if (ipAddress != null) session.IpAddress = ipAddress;
                if (userAgent != null) session.UserAgent = userAgent;
                // Avoid GeoIP lookup on every call unless needed/stale
                 activity?.SetTag("session_source", "Cache");
                // Load supplementary context data (profile, loyalty etc) if not already attached or if stale
                // For simplicity, assuming session cache hit is sufficient for now. A real implementation might need to refresh profile data periodically.
                var cachedProfile = await GetUserProfileFromCacheOrDbAsync(playerId, cancellationToken); // Example helper
                return new UserContext(playerId, session, cachedProfile);
            }

             activity?.SetTag("session_source", "Persistence");
            // Load from persistence
            _logger.LogDebug("Loading context for Player {PlayerId} from persistence.", playerId);
            var loadProfileTask = GetUserProfileFromCacheOrDbAsync(playerId, cancellationToken);
            var loadRolesTask = _persistenceService.LoadUserRolesAsync(playerId, cancellationToken);
            var loadGeoInfoTask = (ipAddress != null) ? _geoIpService.GetLocationFromIpAsync(ipAddress, cancellationToken) : Task.FromResult<GeoLocationInfo>(null);
            // --- V3 Additions ---
            var loadLoyaltyTask = _persistenceService.LoadUserLoyaltyStatusAsync(playerId, cancellationToken);
            var loadGroupsTask = _customerGroupService.GetCustomerGroupsAsync(playerId, cancellationToken);


            await Task.WhenAll(loadProfileTask, loadRolesTask, loadGeoInfoTask, loadLoyaltyTask, loadGroupsTask);

            var profile = loadProfileTask.Result;
            if (profile == null)
            {
                 activity?.SetTag("status", "UserNotFound");
                 return null; // User not found
            }

            var roles = loadRolesTask.Result ?? new HashSet<string>();
            var geoInfo = loadGeoInfoTask.Result;
            var loyaltyStatus = loadLoyaltyTask.Result; // Nullable
            var customerGroups = loadGroupsTask.Result?.ToList() ?? new List<string>();


            // Create new session object
            session = new UserSession(playerId, ipAddress, userAgent, profile.PreferredLocale ?? _config.DefaultLocale, profile.PreferredCurrency ?? _config.DefaultCurrency, geoInfo);
            session.Roles = roles;
            session.LocalePreference = profile.PreferredLocale ?? _config.DefaultLocale;
            session.CurrencyPreference = profile.PreferredCurrency ?? _config.DefaultCurrency;

            // --- V3 Session Setup ---
            // Assign A/B Tests
            if (_config.EnableABTesting)
            {
                 var tempContext = new UserContext(playerId, session, profile, loyaltyStatus, customerGroups); // Temporary context for assignment rules
                 foreach (var experiment in _activeExperiments.Values)
                 {
                     var treatmentResult = await _abTestingService.GetTreatmentAssignmentAsync(experiment.ExperimentKey, tempContext, cancellationToken);
                     if (treatmentResult.IsSuccess)
                     {
                         session.AssignTreatment(experiment.ExperimentKey, treatmentResult.Data);
                     }
                 }
            }
            // Check for affiliate code (e.g., from query param if this was triggered by web request)
            // session.CurrentAffiliateCode = GetAffiliateCodeFromRequest(); // Logic depends on how it's passed

            _userSessions[playerId] = session; // Cache the new session

             activity?.SetTag("status", "Success");
            return new UserContext(playerId, session, profile, loyaltyStatus, customerGroups);
        }

        // Helper to get profile with caching
        private async Task<UserProfile> GetUserProfileFromCacheOrDbAsync(int playerId, CancellationToken cancellationToken)
        {
             string cacheKey = $"UserProfile_{playerId}";
             if (_cache.TryGetValue(cacheKey, out UserProfile profile))
             {
                 _cacheHitsCounter.Add(1);
                 return profile;
             }

             _cacheMissesCounter.Add(1);
             profile = await _persistenceService.LoadUserProfileAsync(playerId, cancellationToken);
             if (profile != null)
             {
                 _cache.Set(cacheKey, profile, _config.CacheDurationMedium); // Cache profile data
             }
             return profile;
        }


        // --- MANY other V3 Helper Methods needed ---
        // ValidatePlaceOrderRequest (needs more checks for V3 fields)
        // GetCartItemsForOrderAsync (largely same)
        // GetPricedCartItemsAsync (NEW: applies base/group pricing before promos)
        // ApplyPromotionDiscountsToItems (NEW: Modifies PricedCartItem based on promo results)
        // DetermineShippingOrigin (NEW: Complex logic for multi-warehouse)
        // CalculateFinalOrderAmounts (NEW: Consolidates pricing components)
        // ReserveInventoryForOrderAsync (Updated for Multi-Warehouse)
        // ReleaseInventoryReservationAsync (Updated for Multi-Warehouse)
        // FinalizeInventoryUpdateAsync (Updated for Multi-Warehouse)
        // CreateOrderFromContextV3 (Updated for V3 fields)
        // TriggerPostOrderProcessing (NEW: Bundles all async post-order tasks)
        // DeliverDigitalGoodsAsync (Potentially updated)
        // UpdateLoyaltyPointsAsync (Potentially updated)
        // ApplyStoreCreditToOrderAsync (NEW)
        // ApplyGiftCardToOrderAsync (NEW)
        // RollbackPaymentAttemptsAsync (NEW)
        // GetShopOriginAddress (Updated for Multi-Warehouse)
        // MergeConfigs (Helper for merging global/shop config)
        // GenerateSecureToken, GetJsonOptions, Constants, NotifyAdminOfFailureAsync etc. (Likely remain similar)
        // ... and many more specific helpers for new features ...


        #endregion
    }

    #region Supporting Classes, Enums, EventArgs (V3 - Massively Expanded)

    // --- Constants & Static Data ---
    public static class Constant
    {
        public const int MAX_SHOPS = 1000; // Example limit
        public const int SYSTEM_USER_ID = -1;
    }

    public static class Roles
    {
        public const string SuperAdmin = "SuperAdmin";
        public const string ShopAdmin = "ShopAdmin";
        public const string ShopOwner = "ShopOwner";
        public const string UserAdmin = "UserAdmin";
        public const string SupportAgent = "SupportAgent";
        public const string MarketingAdmin = "MarketingAdmin";
        public const string FinanceAdmin = "FinanceAdmin";
        public const string AdminReadOnly = "AdminReadOnly";
        public const string Affiliate = "Affiliate";
        // Add other roles as needed
    }

    public static class Permissions // Define granular permissions
    {
        public const string ManageShops = "shops:manage";
        public const string ManageOwnShop = "shops:manage_own";
        public const string PlaceOrder = "orders:place";
        public const string ManageProducts = "products:manage";
        public const string ManageRMAs = "rmas:manage";
        public const string RequestOwnData = "gdpr:request_own";
        public const string ManageUsers = "users:manage";
        public const string ManagePromotions = "promotions:manage";
        public const string ManageGiftCards = "giftcards:manage";
        public const string ViewAdminDashboard = "admin:view";
        public const string ViewBulkOpsStatus = "bulkops:view_status";
        // ... many more permissions ...
    }


    // --- Interfaces for new services V3 (Placeholder Definitions) ---
    // IGiftCardService, IPromotionEngine, IStockSubscriptionService, ISearchService etc. defined earlier

    // --- Supporting Records & Enums for NEW V3 Services & Features ---

    // Gift Cards / Store Credit
    public record GiftCard(string Code, decimal InitialAmount, decimal CurrentBalance, string Currency, DateTime ExpiryDate, bool IsActive, DateTime CreatedDate, int? CreatedByPlayerId);
    public enum StoreCreditAdjustmentReason { OrderRefund, ManualAdjustment, Goodwill, Promotion, GiftCardConversion }
    public record StoreCreditTransaction(Guid Id, int PlayerId, DateTime Timestamp, decimal Amount, string Currency, StoreCreditAdjustmentReason Reason, Guid? RelatedEntityId, int? ActorPlayerId, decimal NewBalance);
    public class GiftCardEventArgs : EventArgs { public GiftCard Card { get; } public GiftCardEventArgs(GiftCard card) { Card = card; } }
    public class StoreCreditEventArgs : EventArgs { public StoreCreditTransaction Transaction { get; } public StoreCreditEventArgs(StoreCreditTransaction tx) { Transaction = tx; } }

    // Promotions
    public record PromotionCondition(/* Complex: UserSegment, CartTotal, ItemId, Category, UsageLimit, DateRange etc. */);
    public record PromotionAction(/* Discount%, FixedAmount, FreeItem, FreeShipping etc. */);
    public record PromotionDefinition(Guid Id, string Name, string Description, PromotionCondition Conditions, PromotionAction Action, bool IsActive, DateTime StartDate, DateTime? EndDate, bool IsCouponBased, string CouponCode, int MaxUses, int Uses);
    public record AppliedPromotionInfo(Guid PromotionId, string Name, string Description, decimal DiscountApplied, bool GrantedFreeShipping /*, AppliedCouponCode? */);
    public record PromotionEvaluationResult(List<AppliedPromotionInfo> AppliedPromotions, decimal TotalDiscount, bool GrantedFreeShipping, List<string> ErrorsOrMessages);
    public class PromotionEventArgs : EventArgs { public PromotionDefinition Promotion { get; } public PromotionEventArgs(PromotionDefinition promo) { Promotion = promo; } }

    // Stock Subscriptions
    public record StockSubscription(Guid Id, int PlayerId, int ItemId, string Sku, DateTime SubscribedDate, DateTime? NotifiedDate);
    public class StockSubscriptionEventArgs : EventArgs { public StockSubscription Subscription { get; } public DateTime NotifyTimestamp { get; } public StockSubscriptionEventArgs(StockSubscription sub) { Subscription = sub; NotifyTimestamp = DateTime.UtcNow; } }

    // Search Service
    // SearchQuery, ItemSearchResult, Facet, FacetValue defined earlier - potentially add more fields to Query (e.g., UserId for personalization)

    // Payment Plans
    public record PaymentPlanQuote(Guid QuoteId, string ProviderName, decimal TotalAmount, string Currency, List<Installment> Installments, string RedirectUrl, DateTime Expiry);
    public record Installment(DateTime DueDate, decimal Amount);
    public record PaymentPlanAuthorization(bool Success, string TransactionId, string ErrorMessage);

    // Affiliate Program
    public record AffiliateVisit(Guid Id, string AffiliateCode, DateTime Timestamp, string IpAddress, string UserAgent, string LandingPage);
    public record AffiliateCommission(Guid Id, Guid OrderId, string AffiliateCode, int AffiliatePlayerId, decimal OrderTotal, decimal CommissionRate, decimal CommissionAmount, CommissionStatus Status, DateTime EarnedDate, DateTime? PayoutDate);
    public enum CommissionStatus { Pending, Approved, Paid, Rejected }
    public record AffiliateStats(int AffiliatePlayerId, int Visits, int Conversions, decimal TotalSales, decimal TotalCommissionEarned, decimal TotalCommissionPaid);
    public class AffiliateEventArgs : EventArgs { public AffiliateCommission Commission { get; } public AffiliateEventArgs(AffiliateCommission comm) { Commission = comm; } }

    // Stored Payments
    public record StoredPaymentMethod(Guid Id, int PlayerId, string PaymentProviderToken, string CardType, string LastFourDigits, int ExpiryMonth, int ExpiryYear, Address BillingAddressSnapshot, bool IsDefault, DateTime DateAdded);

    // Rate Limiting
    public record RateLimitOptions { public int PermitLimit { get; set; } public TimeSpan Window { get; set; } public int QueueLimit { get; set; } = 0; /* Other options */ }
    public record RateLimitCheckResult(bool IsAllowed, TimeSpan? RetryAfter);

    // A/B Testing
    public record ExperimentDefinition(string ExperimentKey, string Description, List<string> Treatments, bool IsActive /*, TargetingRules? */);
    public class ExperimentEventArgs : EventArgs { public string ExperimentKey { get; } public string Treatment { get; } public UserContext User { get; } public ExperimentEventArgs(string key, string treat, UserContext ctx) { ExperimentKey = key; Treatment = treat; User = ctx; } }

    // Admin / Bulk Ops
    public enum BulkOperationType { ProductImportCreate, ProductImportUpdate, PriceUpdate, InventoryUpdate, CategoryAssign }
    public enum BulkJobStatus { Queued, Processing, Completed, Failed, PartiallyCompleted }
    public record BulkOperationJob(Guid JobId, int ShopId, int RequestedByPlayerId, BulkOperationType OperationType, string OriginalFileName, BulkJobStatus Status, DateTime TimestampCreated, DateTime? TimestampCompleted, double ProgressPercentage = 0, string ResultMessage = null, string ResultFileUrl = null);
    public record BulkOperationStatus(Guid JobId, BulkJobStatus Status, double ProgressPercentage, string ResultMessage, string ResultFileUrl, DateTime? TimestampCompleted);
    public class AdminActionEventArgs : EventArgs { public int AdminPlayerId { get; } public AuditAction Action { get; } public string Details { get; } public DateTime Timestamp { get; } = DateTime.UtcNow; /*...*/ }

    // Data Export Types (for admin / GDPR)
    public enum ExportDataType { Orders, Users, Products, Inventory, RMAs, Subscriptions, AllUserData }

    // --- DTOs for Complex Operations (V3 Expanded) ---
    public record PlaceOrderRequest(
        int? UseCartId,
        List<DirectOrderItem> DirectItems,
        Address ShippingAddress,
        Address BillingAddress,
        Guid? SelectedShippingMethodId, // Nullable if free shipping promo applies
        PaymentInfo PaymentInfo, // Existing card, token, or saved payment ID
        List<string> AppliedCouponCodes, // Support multiple codes
        List<string> GiftCardCodes, // NEW V3: Apply gift cards
        decimal UseStoreCreditAmount, // NEW V3: Apply store credit
        bool UsePaymentPlan, // NEW V3: Flag to use BNPL
        bool ClearCartAfterOrder,
        string IpAddress,
        string UserAgent,
        string? AffiliateCode, // Optional affiliate tracking code for this order
        Dictionary<string,string> CustomOrderFields // Extensibility
    );
    // DirectOrderItem, PricingContext defined earlier - PricingContext may need more fields (e.g., AppliedPromotions list)
    // InventoryReservationResult defined earlier - may need LocationId if multi-warehouse
    public record InventoryReservation(int PlayerId, DateTime Expiry, Guid? LocationId); // Added LocationId
    public record MultiLocationStockInfo(ConcurrentDictionary<Guid, int> StockPerLocation); // Key: LocationId
    public record WarehouseLocation(Guid LocationId, string Name, Address Address, bool IsActive /*, Capabilities? */);
    public enum InventoryAllocationStrategy { OptimizeForCost, OptimizeForSpeed, SingleShipment }

    // PaymentInfo needs expansion
    public record PaymentInfo(
        PaymentMethodType MethodType, // e.g., NewCard, Token, SavedPaymentId, PayPalNonce etc.
        string? CardNumber, // Only if directly capturing (Requires PCI Compliance!)
        string? ExpiryMonth,
        string? ExpiryYear,
        string? Cvv, // NEVER STORE THIS
        string? CardholderName,
        string? ProviderToken, // e.g., Stripe Token, Braintree Nonce
        Guid? StoredPaymentMethodId // Reference to saved payment V3
    );
    public enum PaymentMethodType { DirectCardEntry, ProviderToken, SavedPaymentMethod, PayPal, PaymentPlan, GiftCard, StoreCredit, Gateway } // Expanded
    public record PaymentAttempt(PaymentMethodType Method, decimal Amount, bool Success, string TransactionId, string ErrorMessage);
    public record PaymentResult // From Payment Gateway
    {
        public bool IsSuccess { get; init; }
        public string TransactionId { get; init; }
        public string ErrorCode { get; init; }
        public string ErrorMessage { get; init; }
        public PaymentStatus GatewayStatus { get; init; } // e.g. Authorized, Captured, Declined, Error
        public decimal AmountProcessed { get; init; }
        // Add AVS/CVV results if provided by gateway
    }
     public enum PaymentStatus { Authorized, Captured, Voided, Refunded, Declined, Error, Pending }


    // --- EventArgs for Existing & New V3 Events (Expanded Definitions) ---
    // CartEventArgs, SubscriptionEventArgs, NotificationEventArgs, SystemHealthEventArgs, FeatureFlagEventArgs, RmaEventArgs, InventoryLevelChangedEventArgs, PriceChangedEventArgs, UserSegmentChangedEventArgs (as before - potentially add more detail)

    // --- Event Classes for Event Bus (V3 Expanded) ---
    // (Existing events remain) - Add V3 events
    public record GiftCardCreatedEvent(GiftCard Card);
    public record StoreCreditChangedEvent(StoreCreditTransaction Transaction);
    public record PromotionCreatedEvent(PromotionDefinition Promotion);
    public record StockNotificationSubscriptionEvent(StockSubscription Subscription);
    public record AffiliateConversionRecordedEvent(AffiliateCommission Commission);
    public record StoredPaymentMethodAddedEvent(int PlayerId, Guid StoredPaymentId);
    public record UserGdprRequestQueuedEvent(int PlayerId, GdprRequestType RequestType, Guid TrackingId);
    public record BulkOperationRequestedEvent(Guid JobId);
    public record AdminActionLoggedEvent(AuditLogEntry LogEntry);
    // ... add events for all other significant V3 actions ...

    // --- Other Supporting Classes/Enums Used (V3 Expanded) ---
    // Address, ShippingMethod, ScheduledSale, ProductQuestion, ProductAnswer, PickupLocation, UserContext, UserProfile, ShopItem, Review, Order, OrderItem, OrderHistoryEntry, Shipment, AbandonedCartStep, GdprRequestType (Most defined earlier - check for necessary V3 field additions)

    // Refine Order with V3 fields
    public record Order : IEntity // Assuming IEntity provides Id
    {
        public Guid Id { get; set; }
        public int PlayerId { get; set; }
        public int ShopId { get; set; } // Or multiple if multi-shop cart allowed
        public List<OrderItem> Items { get; set; } = new();
        public decimal Subtotal { get; set; } // Before discounts/tax/shipping
        public decimal DiscountAmount { get; set; } // From promotions/coupons
        public List<AppliedPromotionInfo> AppliedPromotions { get; set; } = new(); // V3: Details of applied promos
        public List<PaymentAttempt> PaymentAttempts { get; set; } = new(); // V3: Record how it was paid
        public decimal ShippingCost { get; set; }
        public ShippingMethod SelectedShippingMethod { get; set; }
        public decimal TaxAmount { get; set; }
        public Dictionary<string, decimal> TaxDetails { get; set; } // Tax per jurisdiction/type
        public decimal TotalAmount { get; set; } // Final amount charged
        public string Currency { get; set; }
        public OrderStatus Status { get; set; }
        public Address ShippingAddress { get; set; }
        public Address BillingAddress { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime LastUpdated { get; set; }
        public string PaymentTransactionId { get; set; } // Primary transaction ID
        public string PaymentProvider { get; set; } // Gateway, BNPL Provider, InternalCredit
        public float? FraudScore { get; set; }
        public string? AffiliateCodeApplied { get; set; } // V3: Affiliate credited
        public List<OrderHistoryEntry> History { get; set; } = new();
        public List<Shipment> Shipments { get; set; } = new();
        public Dictionary<string, string> CustomFields { get; set; } = new(); // V3: Custom data
    }
    // OrderItem: Add fields like OriginLocationId if multi-warehouse and shipped per item
    // OrderStatus: Add statuses like 'PendingShipment', 'PendingPickup' ?
    // Review: Add fields for VerifiedPurchase, Image/Video URLs, HelpfulnessVotes
    // ShopItem: Add fields for Dimensions, Weight (important for shipping), IsTaxable, PreOrderDepositRequired?

    public record PricedCartItem // Used during order calculation V3
    {
       public int ShopId { get; init; }
       public int ItemId { get; init; }
       public string Sku { get; init; }
       public string Name {get; init; }
       public int Quantity { get; init; }
       public decimal BaseUnitPrice { get; init; } // Price before any discounts
       public decimal UnitPriceAfterDiscount { get; set; } // Price after item/promo discounts
       public decimal TotalPrice => UnitPriceAfterDiscount * Quantity;
       public ItemType ItemType { get; init; }
       public bool IsTaxable { get; init; }
       public decimal Weight { get; init; } // For shipping
       public Dimensions Dimensions { get; init; } // For shipping
       // Add other relevant fields needed for calc (Categories, Tags...)
    }
    public record Dimensions(decimal Length, decimal Width, decimal Height, string Unit = "cm");

    // GDPR Result
    public record GdprDataResult(Guid TrackingId, string Message, string ExportDataUrl = null);


    // ... many other supporting classes/records/enums would be needed for full V3 implementation ...

    #endregion
}
