using Core;
using Core.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Reoria.Engine.Container.Configuration.Interfaces;
using Reoria.Engine.Container.Interfaces;
using Reoria.Engine.Container.Logging;
using Reoria.Engine.Container.Logging.Interfaces;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;
using static Core.Type;

namespace Server
{
    public class General
    {
        private static readonly RandomUtility Random = new RandomUtility();
        private static readonly IEngineContainer? Container;
        private static readonly IConfiguration? Configuration;
        private static bool ServerDestroyed;
        private static string MyIPAddress = string.Empty;
        private static readonly Stopwatch MyStopwatch = new Stopwatch();
        public static readonly ILogger Logger;
        private static readonly object SyncLock = new object();
        private static readonly CancellationTokenSource Cts = new CancellationTokenSource();
        private static Timer? SaveTimer;
        private static Timer? AnnouncementTimer;
        private static Stopwatch ShutDownTimer = new Stopwatch();
        private static int ShutDownLastTimer = 0;
        private static readonly ConcurrentDictionary<int, PlayerStats> PlayerStatistics = new();
        private static TimeManager? TimeManager;

        static General()
        {
            IServiceCollection services = new ServiceCollection()
                .AddSingleton<IEngineConfigurationProvider, XWConfigurationProvider>()
                .AddSingleton<ILoggingInitializer, SerilogLoggingInitializer>();

            Container = new XWContainer(services)
                .CreateConfiguration()
                .CreateServiceCollection()
                .CreateServiceProvider();

            Configuration = Container?.Provider.GetRequiredService<IConfiguration>() 
                ?? throw new NullReferenceException("Failed to initialize configuration");

            Logger = Container?.Provider.GetRequiredService<ILogger<General>>() ??
                throw new NullReferenceException("Failed to initialize logger");
        }

        #region Utility Methods

        /// <summary>
        /// Retrieves the shutdown timer for server destruction.
        /// </summary>
        public static Stopwatch? GetShutDownTimer => ShutDownTimer;

        /// <summary>
        /// Gets the current server destruction status.
        /// </summary>
        public static bool IsServerDestroyed => ServerDestroyed;

        /// <summary>
        /// Retrieves the random number generator utility.
        /// </summary>
        public static RandomUtility GetRandom => Random;

        /// <summary>
        /// Retrieves the server configuration instance.
        /// </summary>
        public static IConfiguration GetConfig => Configuration ?? throw new NullReferenceException("Configuration not initialized");

        /// <summary>
        /// Retrieves a logger instance for the specified type.
        /// </summary>
        public static ILogger<T> GetLogger<T>() where T : class =>
            Container?.Provider.GetRequiredService<Logger<T>>() ?? throw new NullReferenceException("Container not initialized");

        /// <summary>
        /// Gets the elapsed time in milliseconds since the server started.
        /// </summary>
        public static int GetTimeMs() => (int)MyStopwatch.ElapsedMilliseconds;

        /// <summary>
        /// Retrieves the local IP address of the server.
        /// </summary>
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            return host.AddressList
                .FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                ?.ToString() ?? "127.0.0.1";
        }

        /// <summary>
        /// Validates a username based on length and allowed characters.
        /// </summary>
        public static int IsValidUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return -1;

            if (username.Length < Core.Constant.MIN_NAME_LENGTH || username.Length > Core.Constant.NAME_LENGTH)
                return 0;

            return Regex.IsMatch(username, @"^[a-zA-Z0-9_ ]+$") ? 1 : -1;
        }


        /// <summary>
        /// Gets the current server time synchronized across all operations.
        /// </summary>
        public static DateTime GetServerTime() => DateTime.UtcNow;

        /// <summary>
        /// Generates a unique identifier for server entities.
        /// </summary>
        public static long GenerateUniqueId() => Interlocked.Increment(ref Global.UniqueIdCounter);

        #endregion

        #region Server Lifecycle

        /// <summary>
        /// Initializes the game server asynchronously with enhanced features.
        /// </summary>
        public static async Task InitServerAsync()
        {
            try
            {
                MyStopwatch.Start();
                int startTime = GetTimeMs();

                await InitializeCoreComponentsAsync();
                await LoadGameDataAsync();
                await StartGameLoopAsync(startTime);
                TimeManager = new TimeManager();
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex, "Server initialization failed");
                await HandleCriticalErrorAsync(ex);
            }
        }

        private static async Task InitializeCoreComponentsAsync()
        {
            await Task.WhenAll(LoadConfigurationAsync(), InitializeNetworkAsync(), InitializeChatSystemAsync());
            await InitializeDatabaseWithRetryAsync();
        }

        public static void InitalizeCoreData()
        {
            Core.Type.Job = new Core.Type.JobStruct[Core.Constant.MAX_JOBS];
            Core.Type.Moral = new Core.Type.MoralStruct[Core.Constant.MAX_MORALS];
            Core.Type.Map = new Core.Type.MapStruct[Core.Constant.MAX_MAPS];
            Core.Type.Item = new Core.Type.ItemStruct[Core.Constant.MAX_ITEMS];
            Core.Type.NPC = new Core.Type.NPCStruct[Core.Constant.MAX_NPCS];
            Core.Type.Resource = new Core.Type.ResourceStruct[Core.Constant.MAX_RESOURCES];
            Core.Type.Projectile = new Core.Type.ProjectileStruct[Core.Constant.MAX_PROJECTILES];
            Core.Type.Animation = new Core.Type.AnimationStruct[Core.Constant.MAX_ANIMATIONS];
            Core.Type.Pet = new Core.Type.PetStruct[Core.Constant.MAX_PETS];
            Core.Type.Shop = new Core.Type.ShopStruct[Core.Constant.MAX_SHOPS];
            Core.Type.Player = new Core.Type.PlayerStruct[Core.Constant.MAX_PLAYERS];
            Core.Type.Party = new Core.Type.PartyStruct[Core.Constant.MAX_PARTY];
            Core.Type.MapItem = new Core.Type.MapItemStruct[Core.Constant.MAX_MAPS, Core.Constant.MAX_MAP_ITEMS];
            Core.Type.NPC = new NPCStruct[Core.Constant.MAX_NPCS];
            Core.Type.MapNPC = new MapDataStruct[Core.Constant.MAX_MAPS];

            for (int i = 0; i < Core.Constant.MAX_MAPS; i++)
            {
                Core.Type.MapNPC[i].NPC = new MapNPCStruct[Core.Constant.MAX_MAP_NPCS];
                for (int x = 0; x < Core.Constant.MAX_MAP_NPCS; x++)
                {
                    Core.Type.MapNPC[i].NPC[x].Vital = new int[(int)Core.Enum.VitalType.Count];
                    Core.Type.MapNPC[i].NPC[x].SkillCD = new int[Core.Constant.MAX_NPC_SKILLS];
                    Core.Type.MapNPC[i].NPC[x].Num = -1;
                }

                for (int x = 0; x < Core.Constant.MAX_MAP_ITEMS; x++)
                {
                    Core.Type.MapItem[i, x].Num = -1;
                }
            }

            Core.Type.Shop = new ShopStruct[Core.Constant.MAX_SHOPS];
            Core.Type.Skill = new SkillStruct[Core.Constant.MAX_SKILLS];
            Core.Type.MapResource = new Core.Type.MapResourceStruct[Core.Constant.MAX_MAPS];
            Core.Type.TempPlayer = new Core.Type.TempPlayerStruct[Core.Constant.MAX_PLAYERS];
            Core.Type.Account = new Core.Type.AccountStruct[Core.Constant.MAX_PLAYERS];

            for (int i = 0; i < Core.Constant.MAX_PLAYERS; i++)
            {
                Database.ClearPlayer(i);
            }

            for (int i = 0; i < Core.Constant.MAX_PARTY_MEMBERS; i++)
            {
                Party.ClearParty(i);
            }

            Event.TempEventMap = new Core.Type.GlobalEventsStruct[Core.Constant.MAX_MAPS];
            Core.Type.MapProjectile = new Core.Type.MapProjectileStruct[Core.Constant.MAX_MAPS, Core.Constant.MAX_PROJECTILES];
        }

        private static async Task LoadGameDataAsync()
        {
            var stopwatch = Stopwatch.StartNew();
            InitalizeCoreData();
            await LoadGameContentAsync();
            await SpawnGameObjectsAsync();           
            Logger.LogInformation($"Game data loaded in {stopwatch.ElapsedMilliseconds}ms");
        }

        private static async Task StartGameLoopAsync(int startTime)
        {
            InitializeSaveTimer();
            InitializeAnnouncementTimer();
            DisplayServerBanner(startTime);
            UpdateCaption();
            await NetworkConfig.Socket.StartListeningAsync(SettingsManager.Instance.Port, 5);

            try
            {
                await Loop.ServerAsync();
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex, "Server loop crashed");
                await HandleCriticalErrorAsync(ex);
            }
        }

        /// <summary>
        /// Shuts down the server gracefully, cleaning up all resources.
        /// </summary>
        public static async Task DestroyServerAsync()
        {
            if (ServerDestroyed) return;
            ServerDestroyed = true;
            Cts.Cancel();
            SaveTimer?.Dispose();
            AnnouncementTimer?.Dispose();
            NetworkConfig.Socket.StopListening();

            Logger.LogInformation("Server shutdown initiated...");

            await Database.SaveAllPlayersOnlineAsync();

            try
            {
                await Parallel.ForEachAsync(Enumerable.Range(0, Core.Constant.MAX_PLAYERS), Cts.Token, async (i, ct) =>
                {
                    NetworkSend.SendLeftGame(i);
                    await Player.LeftGame(i);
                });
            }
            catch (TaskCanceledException)
            {
                Logger.LogWarning("Server shutdown tasks were canceled.");
            }

            NetworkConfig.DestroyNetwork();
            Logger.LogInformation("Server shutdown completed.");
            Environment.Exit(0);
        }

        #endregion

        #region Initialization Methods

        private static async Task LoadConfigurationAsync()
        {
            await Task.Run(() =>
            {
                SettingsManager.Load();
                ValidateConfiguration();
                Clock.Instance.GameSpeed = SettingsManager.Instance.TimeSpeed;
                Console.Title = "XtremeWorlds Server";
                MyIPAddress = GetLocalIPAddress();
            });
        }

        private static void ValidateConfiguration()
        {
            if (string.IsNullOrWhiteSpace(SettingsManager.Instance.GameName))
                throw new InvalidOperationException("GameName is not set in configuration");

            if (SettingsManager.Instance.Port <= 0 || SettingsManager.Instance.Port > 65535)
                throw new InvalidOperationException("Invalid Port number in configuration");
        }

        private static async Task InitializeNetworkAsync()
        {
            Global.EKeyPair.GenerateKeys();
            NetworkConfig.InitNetwork();
        }

        private static async Task InitializeDatabaseWithRetryAsync()
        {
            int maxRetries = Configuration.GetValue<int>("Database:MaxRetries", 3);
            int retryDelayMs = Configuration.GetValue<int>("Database:RetryDelayMs", 1000);
            Logger.LogInformation("Initializing database...");
            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                try
                {
                    await Database.CreateDatabaseAsync("mirage");
                    await Database.CreateTablesAsync();
                    await LoadCharacterListAsync();
                    Logger.LogInformation("Database initialized successfully.");
                    return;
                }
                catch (Exception ex)
                {
                    if (attempt == maxRetries)
                    {
                        Logger.LogCritical(ex, "Failed to initialize database after multiple attempts");
                        throw;
                    }
                    Logger.LogWarning(ex, $"Database initialization failed, attempt {attempt} of {maxRetries}");
                    await Task.Delay(retryDelayMs * attempt, Cts.Token);
                }
            }
        }

        private static async Task LoadCharacterListAsync()
        {
           var ids = await Database.GetDataAsync("account");
            Core.Type.Char = new CharList();
            const int maxConcurrency = 4;
            using var semaphore = new SemaphoreSlim(maxConcurrency);

            var tasks = ids.Select(async id =>
            {
                await semaphore.WaitAsync(Cts.Token);
                try
                {
                    for (int i = 0; i < Core.Constant.MAX_CHARS; i++)
                    {
                        var data = await Database.SelectRowByColumnAsync("id", id, "account", $"character{i + 1}");
                        if (data != null && data["Name"] != null)
                        {
                            string name = data["Name"].ToString();
                            if (!string.IsNullOrWhiteSpace(name))
                            {
                                Core.Type.Char.Add(name);
                            }
                        }
                    }
                }
                finally
                {
                    semaphore.Release();
                }
            });

            await Task.WhenAll(tasks);
            Logger.LogInformation($"Loaded {Core.Type.Char.Count} character(s).");
        }

        private static async Task LoadGameContentAsync()
        {
            const int maxConcurrency = 4;
            using var semaphore = new SemaphoreSlim(maxConcurrency);

            var tasks = new[]
            {
                LoadWithSemaphoreAsync(semaphore, async () => { Logger.LogInformation("Loading jobs..."); await Database.LoadJobsAsync(); Logger.LogInformation("Jobs loaded."); }),
                LoadWithSemaphoreAsync(semaphore, async () => { Logger.LogInformation("Loading morals..."); await Moral.LoadMoralsAsync(); Logger.LogInformation("Morals loaded."); }),
                LoadWithSemaphoreAsync(semaphore, async () => { Logger.LogInformation("Loading maps..."); await Database.LoadMapsAsync(); Logger.LogInformation("Maps loaded."); }),
                LoadWithSemaphoreAsync(semaphore, async () => { Logger.LogInformation("Loading items..."); await Item.LoadItemsAsync(); Logger.LogInformation("Items loaded."); }),
                LoadWithSemaphoreAsync(semaphore, async () => { Logger.LogInformation("Loading NPCs..."); await Database.LoadNPCsAsync(); Logger.LogInformation("NPCs loaded."); }),
                LoadWithSemaphoreAsync(semaphore, async () => { Logger.LogInformation("Loading resources..."); await Resource.LoadResourcesAsync(); Logger.LogInformation("Resources loaded."); }),
                LoadWithSemaphoreAsync(semaphore, async () => { Logger.LogInformation("Loading shops..."); await Database.LoadShopsAsync(); Logger.LogInformation("Shops loaded."); }),
                LoadWithSemaphoreAsync(semaphore, async () => { Logger.LogInformation("Loading skills..."); await Database.LoadSkillsAsync(); Logger.LogInformation("Skills loaded."); }),
                LoadWithSemaphoreAsync(semaphore, async () => { Logger.LogInformation("Loading animations..."); await Animation.LoadAnimationsAsync(); Logger.LogInformation("Animations loaded."); }),
                LoadWithSemaphoreAsync(semaphore, async () => { Logger.LogInformation("Loading switches..."); await Event.LoadSwitchesAsync(); Logger.LogInformation("Switches loaded."); }),
                LoadWithSemaphoreAsync(semaphore, async () => { Logger.LogInformation("Loading variables..."); await Event.LoadVariablesAsync(); Logger.LogInformation("Variables loaded."); }),
                LoadWithSemaphoreAsync(semaphore, async () => { Logger.LogInformation("Loading projectiles..."); await Projectile.LoadProjectilesAsync(); Logger.LogInformation("Projectiles loaded."); }),
                LoadWithSemaphoreAsync(semaphore, async () => { Logger.LogInformation("Loading pets..."); await Pet.LoadPetsAsync(); Logger.LogInformation("Pets loaded."); }),
                LoadWithSemaphoreAsync(semaphore, async () => { Logger.LogInformation("Loading script..."); await Script.LoadScriptAsync(0); Logger.LogInformation("Script compiled and loaded."); })
            };

            await Task.WhenAll(tasks);
            Logger.LogInformation("Game content loaded successfully.");
        }

        private static async Task LoadWithSemaphoreAsync(SemaphoreSlim semaphore, Func<Task> loadFunc)
        {
            await semaphore.WaitAsync(Cts.Token);
            try
            {
                await loadFunc();
            }
            finally
            {
                semaphore.Release();
            }
        }

        private static async Task SpawnGameObjectsAsync()
        {
            await Task.WhenAll(
                Item.SpawnAllMapsItemsAsync(),
                NPC.SpawnAllMapNPCs(),
                EventLogic.SpawnAllMapGlobalEvents()
            );
            Logger.LogInformation("Game objects spawned.");
        }

        #endregion

        #region Display and Monitoring

        private static void DisplayServerBanner(int startTime)
        {
            Console.Clear();
            string[] banner = {
                " __   ___                        __          __        _     _     ",
                @" \ \ / / |                       \ \        / /       | |   | |",
                @"  \ V /| |_ _ __ ___ _ __ ___   __\ \  /\  / /__  _ __| | __| |___ ",
                @"  > < | __| '__/ _ \ '_ ` _ \ / _ \ \/  \/ / _ \| '__| |/ _` / __|",
                @" / . \| |_| | |  __/ | | | | |  __/\  /\  / (_) | |  | | (_| \__ \",
                @"/_/ \_\\__|_|  \___|_| |_| |_|\___| \/  \/ \___/|_|  |_|\__,_|___/"
            };

            foreach (var line in banner) Console.WriteLine(line);
            Console.WriteLine($"Initialization complete. Server loaded in {GetTimeMs() - startTime}ms.");
            Console.WriteLine("Use /help for available commands.");
        }

        /// <summary>
        /// Counts the number of players currently online.
        /// </summary>
        public static int CountPlayersOnline()
        {
            lock (SyncLock)
            {
                return Enumerable.Range(0, NetworkConfig.Socket.HighIndex)
                    .Count(i => NetworkConfig.IsPlaying(i));
            }
        }

        /// <summary>
        /// Updates the console title with server status information.
        /// </summary>
        public static void UpdateCaption()
        {
            try
            {
                Console.Title = $"{SettingsManager.Instance.GameName} <IP {MyIPAddress}:{SettingsManager.Instance.Port}> " +
                    $"({CountPlayersOnline()} Players Online) - Errors: {Global.ErrorCount} - Time: {Clock.Instance}";
            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex, "Failed to update console title");
                Console.Title = SettingsManager.Instance.GameName;
            }
        }

        /// <summary>
        /// Performs a health check on critical server components.
        /// </summary>
        public static async Task<bool> PerformHealthCheckAsync()
        {
            try
            {
                bool networkActive = NetworkConfig.Socket.IsListening;
                if (!networkActive) Logger.LogWarning("Network socket is not listening.");
                return networkActive && !Cts.IsCancellationRequested;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Health check failed");
                return false;
            }
        }

        #endregion

        #region New and Enhanced Functionality

        private static void InitializeSaveTimer()
        {
            int intervalMinutes = SettingsManager.Instance.SaveInterval;
            SaveTimer = new Timer(async _ => await SavePlayersPeriodicallyAsync(), null,
                TimeSpan.FromMinutes(intervalMinutes), TimeSpan.FromMinutes(intervalMinutes));
        }

        private static void InitializeAnnouncementTimer()
        {
            int intervalMinutes = Configuration?.GetValue<int>("Events:AnnouncementIntervalMinutes", 60) ?? 60;
            AnnouncementTimer = new Timer(async _ => await SendServerAnnouncementAsync(""), null,
                TimeSpan.FromMinutes(intervalMinutes), TimeSpan.FromMinutes(intervalMinutes));
        }

        private static async Task SavePlayersPeriodicallyAsync()
        {
            try
            {
                await Database.SaveAllPlayersOnlineAsync();
                Logger.LogInformation("Periodic player save completed.");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Periodic player save failed");
            }
        }

        private static async Task SendServerAnnouncementAsync(string message)
        {
            await Parallel.ForEachAsync(Enumerable.Range(0, Core.Constant.MAX_PLAYERS), Cts.Token, async (i, ct) =>
            {
                if (NetworkConfig.IsPlaying(i))
                    NetworkSend.PlayerMsg(i, message, (int)Core.Enum.ColorType.Yellow);
            });
            Logger.LogInformation("Server announcement sent.");
        }

        private static async Task InitializeChatSystemAsync()
        {
            Logger.LogInformation("Chat system initialized.");
            // Additional initialization logic can be added here if needed
        }

        /// <summary>
        /// Handles player commands with expanded functionality.
        /// </summary>
        public static async Task HandlePlayerCommandAsync(int playerIndex, string command)
        {
            if (string.IsNullOrWhiteSpace(command)) return;

            var parts = command.Trim().Split(' ');
            switch (parts[0].ToLower())
            {
                case "/teleport":
                    if (parts.Length == 3 && int.TryParse(parts[1], out int x) && int.TryParse(parts[2], out int y))
                        await TeleportPlayerAsync(playerIndex, (byte)x, (byte)y);
                    else
                        NetworkSend.PlayerMsg(playerIndex, "Usage: /teleport <x> <y>", (int)Core.Enum.ColorType.BrightRed);
                    break;

                case "/kick":
                    if (parts.Length == 2 && int.TryParse(parts[1], out int targetIndex))
                        await KickPlayerAsync(playerIndex, targetIndex);
                    break;

                case "/broadcast":
                    if (parts.Length > 1)
                        await BroadcastMessageAsync(playerIndex, string.Join(" ", parts[1..]));
                    break;

                case "/status":
                    await SendServerStatusAsync(playerIndex);
                    break;

                case "/help":
                    await SendHelpMessageAsync(playerIndex);
                    break;

                case "/whisper":
                    if (parts.Length >= 3)
                        await SendWhisperAsync(playerIndex, parts[1], string.Join(" ", parts[2..]));
                    else
                        NetworkSend.PlayerMsg(playerIndex, "Usage: /whisper <player> <message>", (int)Core.Enum.ColorType.BrightRed);
                    break;

                case "/party":
                    if (parts.Length >= 2)
                        await HandlePartyCommandAsync(playerIndex, parts[1], parts.Length > 2 ? parts[2] : null);
                    else
                        NetworkSend.PlayerMsg(playerIndex, "Usage: /party <create|invite|leave> [player]", (int)Core.Enum.ColorType.BrightRed);
                    break;

                case "/stats":
                    await SendPlayerStatsAsync(playerIndex);
                    break;

                case "/save":
                    await SavePlayerDataAsync(playerIndex);
                    break;

                default:
                    NetworkSend.PlayerMsg(playerIndex, "Unknown command. Use /help for assistance.", (int)Core.Enum.ColorType.BrightRed);
                    break;
            }
        }

        private static async Task TeleportPlayerAsync(int playerIndex, byte x, byte y)
        {
            try
            {
                ref var player = ref Core.Type.Player[playerIndex];
                if (x < 0 || x >= 100 || y < 0 || y >= 100)
                {
                    NetworkSend.PlayerMsg(playerIndex, "Coordinates out of bounds.", (int)Core.Enum.ColorType.BrightRed);
                    return;
                }

                player.X = x;
                player.Y = y;
                NetworkSend.SendPlayerXYToMap(playerIndex);
                Logger.LogInformation($"Player {playerIndex} teleported to ({x}, {y})");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Failed to teleport player {playerIndex}");
                NetworkSend.PlayerMsg(playerIndex, "Teleport failed.", (int)Core.Enum.ColorType.BrightRed);
            }
        }

        private static async Task KickPlayerAsync(int playerIndex, int targetIndex)
        {
            try
            {
                if (!await IsAdminAsync(playerIndex))
                {
                    NetworkSend.PlayerMsg(playerIndex, "You are not authorized to kick players.", (int)Core.Enum.ColorType.BrightRed);
                    return;
                }

                if (NetworkConfig.IsPlaying(targetIndex))
                {
                    NetworkSend.SendLeftGame(targetIndex);
                    Player.LeftGame(targetIndex);
                    Logger.LogInformation($"Player {targetIndex} kicked by {playerIndex}");
                    NetworkSend.PlayerMsg(playerIndex, $"Player {targetIndex} has been kicked.", (int)Core.Enum.ColorType.BrightGreen);
                }
                else
                {
                    NetworkSend.PlayerMsg(playerIndex, "Target player is not online.", (int)Core.Enum.ColorType.BrightRed);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Failed to kick player {targetIndex}");
                NetworkSend.PlayerMsg(playerIndex, "Kick operation failed.", (int)Core.Enum.ColorType.BrightRed);
            }
        }

        private static async Task BroadcastMessageAsync(int playerIndex, string message)
        {
            try
            {
                if (!await IsAdminAsync(playerIndex))
                {
                    NetworkSend.PlayerMsg(playerIndex, "You are not authorized to broadcast.", (int)Core.Enum.ColorType.BrightRed);
                    return;
                }

                await SendChatMessageAsync(playerIndex, "global", $"[Broadcast] {message}", Core.Enum.ColorType.BrightGreen);
                Logger.LogInformation($"Broadcast by {playerIndex}: {message}");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Broadcast failed");
                NetworkSend.PlayerMsg(playerIndex, "Broadcast failed.", (int)Core.Enum.ColorType.BrightRed);
            }
        }

        private static async Task SendServerStatusAsync(int playerIndex)
        {
            try
            {
                bool isHealthy = await PerformHealthCheckAsync();
                string status = $"Server Status: {(isHealthy ? "Healthy" : "Unhealthy")}\n" +
                                $"Players Online: {CountPlayersOnline()}\n" +
                                $"Uptime: {MyStopwatch.Elapsed}\n" +
                                $"Errors: {Global.ErrorCount}";
                NetworkSend.PlayerMsg(playerIndex, status, (int)Core.Enum.ColorType.BrightGreen);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Failed to send server status");
                NetworkSend.PlayerMsg(playerIndex, "Unable to retrieve server status.", (int)Core.Enum.ColorType.BrightRed);
            }
        }

        private static async Task SendHelpMessageAsync(int playerIndex)
        {
            string help = "Available Commands:\n" +
                          "/teleport <x> <y> - Teleport to coordinates\n" +
                          "/kick <playerId> - Kick a player (admin only)\n" +
                          "/broadcast <message> - Send a message to all players (admin only)\n" +
                          "/status - View server status\n" +
                          "/whisper <player> <message> - Send a private message\n" +
                          "/party <create|invite|leave> [player] - Manage parties\n" +
                          "/stats - View your statistics\n" +
                          "/save - Manually save your data\n" +
                          "/help - Show this message";
            NetworkSend.PlayerMsg(playerIndex, help, (int)Core.Enum.ColorType.BrightGreen);
        }

        private static async Task SendWhisperAsync(int senderIndex, string targetName, string message)
        {
            try
            {
                int targetIndex = await FindPlayerByNameAsync(targetName);
                if (targetIndex == -1)
                {
                    NetworkSend.PlayerMsg(senderIndex, $"Player '{targetName}' not found.", (int)Core.Enum.ColorType.BrightRed);
                    return;
                }

                await SendChatMessageAsync(senderIndex, $"private:{targetIndex}", $"[Whisper] {message}", Core.Enum.ColorType.BrightCyan);
                Logger.LogInformation($"Whisper from {senderIndex} to {targetIndex}: {message}");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Failed to send whisper from {senderIndex} to {targetName}");
                NetworkSend.PlayerMsg(senderIndex, "Failed to send whisper.", (int)Core.Enum.ColorType.BrightRed);
            }
        }

        private static async Task HandlePartyCommandAsync(int playerIndex, string subCommand, string targetName)
        {
            try
            {
                var player = Core.Type.TempPlayer[playerIndex];
                switch (subCommand.ToLower())
                {
                    case "create":
                        if (player.InParty != -1)
                        {
                            NetworkSend.PlayerMsg(playerIndex, "You are already in a party.", (int)Core.Enum.ColorType.BrightRed);
                            return;
                        }
                        player.InParty = (int)GenerateUniqueId();
                        NetworkSend.PlayerMsg(playerIndex, "Party created.", (int)Core.Enum.ColorType.BrightGreen);
                        break;

                    case "invite":
                        if (player.InParty == -1)
                        {
                            NetworkSend.PlayerMsg(playerIndex, "You must create a party first.", (int)Core.Enum.ColorType.BrightRed);
                            return;
                        }
                        int targetIndex = await FindPlayerByNameAsync(targetName);
                        if (targetIndex == -1)
                        {
                            NetworkSend.PlayerMsg(playerIndex, $"Player '{targetName}' not found.", (int)Core.Enum.ColorType.BrightRed);
                            return;
                        }
                        var targetPlayer = Core.Type.TempPlayer[targetIndex];
                        if (targetPlayer.InParty != -1)
                        {
                            NetworkSend.PlayerMsg(playerIndex, $"{targetName} is already in a party.", (int)Core.Enum.ColorType.BrightRed);
                            return;
                        }
                        targetPlayer.InParty = player.InParty;
                        NetworkSend.PlayerMsg(targetIndex, $"You have joined {Core.Type.Player[playerIndex].Name}'s party.", (int)Core.Enum.ColorType.BrightGreen);
                        NetworkSend.PlayerMsg(playerIndex, $"{targetName} has joined your party.", (int)Core.Enum.ColorType.BrightGreen);
                        break;

                    case "leave":
                        if (player.InParty == -1)
                        {
                            NetworkSend.PlayerMsg(playerIndex, "You are not in a party.", (int)Core.Enum.ColorType.BrightRed);
                            return;
                        }
                        player.InParty = -1;
                        NetworkSend.PlayerMsg(playerIndex, "You have left the party.", (int)Core.Enum.ColorType.BrightGreen);
                        break;

                    default:
                        NetworkSend.PlayerMsg(playerIndex, "Invalid party command. Use: create, invite, leave.", (int)Core.Enum.ColorType.BrightRed);
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Failed to handle party command for player {playerIndex}");
                NetworkSend.PlayerMsg(playerIndex, "Party command failed.", (int)Core.Enum.ColorType.BrightRed);
            }
        }

        private static async Task SendPlayerStatsAsync(int playerIndex)
        {
            try
            {
                var stats = PlayerStatistics.GetOrAdd(playerIndex, new PlayerStats());
                string statsMessage = $"Your Stats:\n" +
                                      $"Kills: {stats.Kills}\n" +
                                      $"Deaths: {stats.Deaths}\n" +
                                      $"Playtime: {stats.PlayTime.TotalHours:F2} hours";
                NetworkSend.PlayerMsg(playerIndex, statsMessage, (int)Core.Enum.ColorType.BrightGreen);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Failed to send stats for player {playerIndex}");
                NetworkSend.PlayerMsg(playerIndex, "Failed to retrieve stats.", (int)Core.Enum.ColorType.BrightRed);
            }
        }

        private static async Task SavePlayerDataAsync(int playerIndex)
        {
            try
            {
                await Database.SaveAccountAsync(playerIndex); // Assuming this method exists
                NetworkSend.PlayerMsg(playerIndex, "Your data has been saved.", (int)Core.Enum.ColorType.BrightGreen);
                Logger.LogInformation($"Player {playerIndex} data saved manually.");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Failed to save data for player {playerIndex}");
                NetworkSend.PlayerMsg(playerIndex, "Failed to save data.", (int)Core.Enum.ColorType.BrightRed);
            }
        }

        private static async Task<int> FindPlayerByNameAsync(string name)
        {
            for (int i = 0; i < Core.Constant.MAX_PLAYERS; i++)
            {
                if (NetworkConfig.IsPlaying(i) && Core.Type.Player[i].Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }
            return -1;
        }

        private static async Task SendChatMessageAsync(int senderIndex, string channel, string message, Core.Enum.ColorType color)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(message) || message.Length > 200) // Basic filtering
                {
                    NetworkSend.PlayerMsg(senderIndex, "Invalid message.", (int)Core.Enum.ColorType.BrightRed);
                    return;
                }

                if (channel.StartsWith("private:"))
                {
                    int targetIndex = int.Parse(channel.Split(':')[1]);
                    NetworkSend.PlayerMsg(targetIndex, $"[From {Core.Type.Player[senderIndex].Name}] {message}", (int)color);
                    NetworkSend.PlayerMsg(senderIndex, $"[To {Core.Type.Player[targetIndex].Name}] {message}", (int)color);
                }
                else if (channel == "party" && Core.Type.TempPlayer[senderIndex].InParty != 0)
                {
                    await Parallel.ForEachAsync(Enumerable.Range(0, Core.Constant.MAX_PLAYERS), Cts.Token, async (i, ct) =>
                    {
                        if (NetworkConfig.IsPlaying(i) && Core.Type.TempPlayer[i].InParty == Core.Type.TempPlayer[senderIndex].InParty)
                            NetworkSend.PlayerMsg(i, $"[Party] {Core.Type.Player[senderIndex].Name}: {message}", (int)color);
                    });
                }
                else if (channel == "global")
                {
                    await Parallel.ForEachAsync(Enumerable.Range(0, Core.Constant.MAX_PLAYERS), Cts.Token, async (i, ct) =>
                    {
                        if (NetworkConfig.IsPlaying(i))
                            NetworkSend.PlayerMsg(i, message, (int)color);
                    });
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Failed to send chat message from {senderIndex} to {channel}");
                NetworkSend.PlayerMsg(senderIndex, "Failed to send message.", (int)Core.Enum.ColorType.BrightRed);
            }
        }

        /// <summary>
        /// Handles player login events.
        /// </summary>
        public static async Task OnPlayerLoginAsync(int playerIndex)
        {
            Logger.LogInformation($"Player {playerIndex} logged in.");
            NetworkSend.PlayerMsg(playerIndex, "Welcome to the server!", (int)Core.Enum.ColorType.BrightGreen);
            PlayerStatistics.GetOrAdd(playerIndex, new PlayerStats()).LoginTime = GetServerTime();
        }

        /// <summary>
        /// Handles player logout events.
        /// </summary>
        public static async Task OnPlayerLogoutAsync(int playerIndex)
        {
            if (PlayerStatistics.TryGetValue(playerIndex, out var stats) && stats.LoginTime.HasValue)
            {
                stats.PlayTime += GetServerTime() - stats.LoginTime.Value;
                stats.LoginTime = null;
            }
            Logger.LogInformation($"Player {playerIndex} logged out.");
        }

        private static Task<bool> IsAdminAsync(int playerIndex) =>
            Task.FromResult(playerIndex == 0); // Example admin check

        /// <summary>
        /// Creates a backup of the database asynchronously.
        /// </summary>
        public static async Task BackupDatabaseAsync()
        {
            try
            {
                string backupDir = Core.Path.Database;
                Directory.CreateDirectory(backupDir);
                string backupPath = System.IO.Path.Combine(backupDir, $"backup_{DateTime.Now:yyyyMMdd_HHmmss}.bak");
                //await Database.BackupAsync(backupPath); // Assuming this method exists
                Logger.LogInformation($"Database backup created: {backupPath}");

                var backups = Directory.GetFiles(backupDir, "backup_*.bak")
                    .OrderByDescending(f => f)
                    .Skip(SettingsManager.Instance.MaxBackups)
                    .ToList();
                foreach (var oldBackup in backups)
                {
                    await Task.Run(() => File.Delete(oldBackup), Cts.Token);
                    Logger.LogInformation($"Deleted old backup: {oldBackup}");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Database backup failed");
            }
        }

        #endregion

        #region Error Handling

        private static async Task HandleCriticalErrorAsync(Exception ex)
        {
            await BackupDatabaseAsync();
            Logger.LogCritical(ex, "Critical error occurred. Initiating emergency shutdown");
            await SendServerAnnouncementAsync("Server shutting down due to critical error.");
            await DestroyServerAsync();
        }

        /// <summary>
        /// Logs an error to a file and updates error count asynchronously.
        /// </summary>
        public static async Task LogErrorAsync(Exception ex, string context = "")
        {
            string errorInfo = $"{ex.Message}\nStackTrace: {ex.StackTrace}";
            string logPath = System.IO.Path.Combine(Core.Path.Logs, "Errors.log");
            Directory.CreateDirectory(Core.Path.Logs);

            await File.AppendAllTextAsync(logPath,
                $"{DateTime.Now}\nContext: {context}\n{errorInfo}\n\n", Cts.Token);

            Interlocked.Increment(ref Global.ErrorCount);
            UpdateCaption();
        }

        public static async Task CheckShutDownCountDownAsync()
        {
            if (ShutDownTimer.ElapsedTicks <= 0) return;

            int time = ShutDownTimer.Elapsed.Seconds;
            if (ShutDownLastTimer != time)
            {
                if (SettingsManager.Instance.ServerShutdown - time <= 10)
                {
                    NetworkSend.GlobalMsg($"Server shutdown in {SettingsManager.Instance.ServerShutdown - time} seconds!");
                    Console.WriteLine($"Server shutdown in {SettingsManager.Instance.ServerShutdown - time} seconds!");
                    if (SettingsManager.Instance.ServerShutdown - time <= 1)
                    {
                        await DestroyServerAsync();
                    }
                }
                ShutDownLastTimer = time;
            }
        }

        #endregion

        #region Helper Classes

        private class PlayerStats
        {
            public int Kills { get; set; }
            public int Deaths { get; set; }
            public TimeSpan PlayTime { get; set; }
            public DateTime? LoginTime { get; set; }
        }

        #endregion
    }
}
