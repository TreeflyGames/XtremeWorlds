using System;
using Core;
using Core.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Reoria.Engine.Common.Security.Encryption;
using Reoria.Engine.Container;
using Reoria.Engine.Container.Configuration;
using Reoria.Engine.Container.Configuration.Interfaces;
using Reoria.Engine.Container.Interfaces;
using Reoria.Engine.Container.Logging;
using Reoria.Engine.Container.Logging.Interfaces;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using static Core.Type;
using Path = System.IO.Path;

namespace Server
{
    public class General
    {
        public static Dictionary<int, AesEncryption> Aes = new Dictionary<int, AesEncryption>();
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
            if (OperatingSystem.IsMacOS())
            {
                string configDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "XtremeWorlds");
                string targetFile = Path.Combine(configDir, "appsettings.json");

                if (!File.Exists(targetFile))
                {
                    string bundledFile = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
                    if (File.Exists(bundledFile))
                    {
                        Directory.CreateDirectory(configDir);
                        File.Copy(bundledFile, targetFile);
                    }
                }
            }
            
            IServiceCollection services = new ServiceCollection()
                .AddTransient<IEngineConfigurationSources, EngineConfigurationSources>()
                .AddTransient<IEngineConfigurationProvider, EngineConfigurationProvider>()
                .AddTransient<IEngineLoggerFactory, SerilogLoggerFactory>();

            Container = new EngineContainer(services);

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
        public static async System.Threading.Tasks.Task InitServerAsync()
        {
            if (!System.Diagnostics.Debugger.IsAttached)
            {
                try
                {
                    await ServerStartAsync();
                }
                catch (Exception ex)
                {
                    Logger.LogCritical(ex, "Server initialization failed");
                    await HandleCriticalErrorAsync(ex);
                }
            }
            else
            {
                await ServerStartAsync();
            }
        }

        private static async System.Threading.Tasks.Task ServerStartAsync()
        {
            MyStopwatch.Start();
            int startTime = GetTimeMs();

            await InitializeCoreComponentsAsync();
            await LoadGameDataAsync();
            await StartGameLoopAsync(startTime);
            TimeManager = new TimeManager();
        }

        private static async System.Threading.Tasks.Task InitializeCoreComponentsAsync()
        {
            await System.Threading.Tasks.Task.WhenAll(LoadConfigurationAsync(), InitializeNetworkAsync(), InitializeChatSystemAsync());
            await InitializeDatabaseWithRetryAsync();
        }

        public static void InitalizeCoreData()
        {
            Data.Job = new Core.Type.Job[Core.Constant.MAX_JOBS];
            Data.Moral = new Core.Type.Moral[Core.Constant.MAX_MORALS];
            Data.Map = new Core.Type.Map[Core.Constant.MAX_MAPS];
            Core.Data.Item = new Core.Type.Item[Core.Constant.MAX_ITEMS];
            Data.Npc = new Core.Type.Npc[Core.Constant.MAX_NPCS];
            Data.Resource = new Core.Type.Resource[Core.Constant.MAX_RESOURCES];
            Data.Projectile = new Core.Type.Projectile[Core.Constant.MAX_PROJECTILES];
            Data.Animation = new Core.Type.Animation[Core.Constant.MAX_ANIMATIONS];
            Data.Pet = new Core.Type.Pet[Core.Constant.MAX_PETS];
            Data.Shop = new Core.Type.Shop[Core.Constant.MAX_SHOPS];
            Core.Data.Player = new Core.Type.Player[Core.Constant.MAX_PLAYERS];
            Data.Party = new Core.Type.Party[Core.Constant.MAX_PARTY];
            Data.MapItem = new Core.Type.MapItem[Core.Constant.MAX_MAPS, Core.Constant.MAX_MAP_ITEMS];
            Data.Npc = new Core.Type.Npc[Core.Constant.MAX_NPCS];
            Data.MapNpc = new MapData[Core.Constant.MAX_MAPS];

            for (int i = 0; i < Core.Constant.MAX_MAPS; i++)
            {
                Data.MapNpc[i].Npc = new MapNpc[Core.Constant.MAX_MAP_NPCS];
                for (int x = 0; x < Core.Constant.MAX_MAP_NPCS; x++)
                {
                    Data.MapNpc[i].Npc[x].Vital = new int[Enum.GetValues(typeof(Core.Vital)).Length];
                    Data.MapNpc[i].Npc[x].SkillCD = new int[Core.Constant.MAX_NPC_SKILLS];
                    Data.MapNpc[i].Npc[x].Num = -1;
                    Data.MapNpc[i].Npc[x].SkillBuffer = -1;
                }

                for (int x = 0; x < Core.Constant.MAX_MAP_ITEMS; x++)
                {
                    Data.MapItem[i, x].Num = -1;
                }
            }

            Data.Shop = new Core.Type.Shop[Core.Constant.MAX_SHOPS];
            Data.Skill = new Skill[Core.Constant.MAX_SKILLS];
            Data.MapResource = new Core.Type.MapResource[Core.Constant.MAX_MAPS];
            Core.Data.TempPlayer = new Core.Type.TempPlayer[Core.Constant.MAX_PLAYERS];
            Core.Data.Account = new Core.Type.Account[Core.Constant.MAX_PLAYERS];

            for (int i = 0; i < Core.Constant.MAX_PLAYERS; i++)
            {
                Database.ClearPlayer(i);
            }

            for (int i = 0; i < Core.Constant.MAX_PARTY_MEMBERS; i++)
            {
                Party.ClearParty(i);
            }

            Event.TempEventMap = new Core.Type.GlobalEvents[Core.Constant.MAX_MAPS];
            Data.MapProjectile = new Core.Type.MapProjectile[Core.Constant.MAX_MAPS, Core.Constant.MAX_PROJECTILES];
        }

        private static async System.Threading.Tasks.Task LoadGameDataAsync()
        {
            var stopwatch = Stopwatch.StartNew();
            InitalizeCoreData();
            await LoadGameContentAsync();
            await SpawnGameObjectsAsync();           
            Logger.LogInformation($"Game data loaded in {stopwatch.ElapsedMilliseconds}ms");
        }

        private static async System.Threading.Tasks.Task StartGameLoopAsync(int startTime)
        {
            InitializeSaveTimer();
            InitializeAnnouncementTimer();
            DisplayServerBanner(startTime);
            UpdateCaption();
            await NetworkConfig.Socket.StartListeningAsync(SettingsManager.Instance.Port, 5);

            if (!System.Diagnostics.Debugger.IsAttached)
            {
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
            else
            {
                await Loop.ServerAsync();
            }

        }

        /// <summary>
        /// Shuts down the server gracefully, cleaning up all resources.
        /// </summary>
        public static async System.Threading.Tasks.Task DestroyServerAsync()
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

        private static async System.Threading.Tasks.Task LoadConfigurationAsync()
        {
            await System.Threading.Tasks.Task.Run(() =>
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

        private static async System.Threading.Tasks.Task InitializeNetworkAsync()
        {
            NetworkConfig.InitNetwork();
        }

        private static async System.Threading.Tasks.Task InitializeDatabaseWithRetryAsync()
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
                    await System.Threading.Tasks.Task.Delay(retryDelayMs * attempt, Cts.Token);
                }
            }
        }

        private static async System.Threading.Tasks.Task LoadCharacterListAsync()
        {
           var ids = await Database.GetDataAsync("account");
            Data.Char = new CharList();
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
                                Data.Char.Add(name);
                            }
                        }
                    }
                }
                finally
                {
                    semaphore.Release();
                }
            });

            await System.Threading.Tasks.Task.WhenAll(tasks);
            Logger.LogInformation($"Loaded {Data.Char.Count} character(s).");
        }

        private static async System.Threading.Tasks.Task LoadGameContentAsync()
        {
            const int maxConcurrency = 4;
            using var semaphore = new SemaphoreSlim(maxConcurrency);

            var tasks = new[]
            {
                LoadWithSemaphoreAsync(semaphore, async () => { Logger.LogInformation("Loading jobs..."); await Database.LoadJobsAsync(); Logger.LogInformation("Jobs loaded."); }),
                LoadWithSemaphoreAsync(semaphore, async () => { Logger.LogInformation("Loading morals..."); await Moral.LoadMoralsAsync(); Logger.LogInformation("Morals loaded."); }),
                LoadWithSemaphoreAsync(semaphore, async () => { Logger.LogInformation("Loading maps..."); await Database.LoadMapsAsync(); Logger.LogInformation("Maps loaded."); }),
                LoadWithSemaphoreAsync(semaphore, async () => { Logger.LogInformation("Loading items..."); await Item.LoadItemsAsync(); Logger.LogInformation("Items loaded."); }),
                LoadWithSemaphoreAsync(semaphore, async () => { Logger.LogInformation("Loading Npcs..."); await Database.LoadNpcsAsync(); Logger.LogInformation("Npcs loaded."); }),
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

            await System.Threading.Tasks.Task.WhenAll(tasks);
            Logger.LogInformation("Game content loaded successfully.");
        }

        private static async System.Threading.Tasks.Task LoadWithSemaphoreAsync(SemaphoreSlim semaphore, Func<System.Threading.Tasks.Task> loadFunc)
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

        private static async System.Threading.Tasks.Task SpawnGameObjectsAsync()
        {
            await System.Threading.Tasks.Task.WhenAll(
                Item.SpawnAllMapsItemsAsync(),
                Npc.SpawnAllMapNpcs(),
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
        public static async System.Threading.Tasks.Task<bool> PerformHealthCheckAsync()
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

        private static async System.Threading.Tasks.Task SavePlayersPeriodicallyAsync()
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

        private static async System.Threading.Tasks.Task SendServerAnnouncementAsync(string message)
        {
            await Parallel.ForEachAsync(Enumerable.Range(0, Core.Constant.MAX_PLAYERS), Cts.Token, async (i, ct) =>
            {
                if (NetworkConfig.IsPlaying(i))
                    NetworkSend.PlayerMsg(i, message, (int)Core.Color.Yellow);
            });
            Logger.LogInformation("Server announcement sent.");
        }

        private static async System.Threading.Tasks.Task InitializeChatSystemAsync()
        {
            Logger.LogInformation("Chat system initialized.");
            // Additional initialization logic can be added here if needed
        }

        /// <summary>
        /// Handles player commands with expanded functionality.
        /// </summary>
        public static async System.Threading.Tasks.Task HandlePlayerCommandAsync(int playerIndex, string command)
        {
            if (string.IsNullOrWhiteSpace(command)) return;

            var parts = command.Trim().Split(' ');
            switch (parts[0].ToLower())
            {
                case "/teleport":
                    if (parts.Length == 3 && int.TryParse(parts[1], out int x) && int.TryParse(parts[2], out int y))
                        await TeleportPlayerAsync(playerIndex, (byte)x, (byte)y);
                    else
                        NetworkSend.PlayerMsg(playerIndex, "Usage: /teleport <x> <y>", (int)Core.Color.BrightRed);
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
                        NetworkSend.PlayerMsg(playerIndex, "Usage: /whisper <player> <message>", (int)Core.Color.BrightRed);
                    break;

                case "/party":
                    if (parts.Length >= 2)
                        await HandlePartyCommandAsync(playerIndex, parts[1], parts.Length > 2 ? parts[2] : null);
                    else
                        NetworkSend.PlayerMsg(playerIndex, "Usage: /party <create|invite|leave> [player]", (int)Core.Color.BrightRed);
                    break;

                case "/stats":
                    await SendPlayerStatsAsync(playerIndex);
                    break;

                case "/save":
                    await SavePlayerDataAsync(playerIndex);
                    break;

                default:
                    NetworkSend.PlayerMsg(playerIndex, "Unknown command. Use /help for assistance.", (int)Core.Color.BrightRed);
                    break;
            }
        }

        private static async System.Threading.Tasks.Task TeleportPlayerAsync(int playerIndex, byte x, byte y)
        {
            try
            {
                ref var player = ref Core.Data.Player[playerIndex];
                if (x < 0 || x >= 100 || y < 0 || y >= 100)
                {
                    NetworkSend.PlayerMsg(playerIndex, "Coordinates out of bounds.", (int)Core.Color.BrightRed);
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
                NetworkSend.PlayerMsg(playerIndex, "Teleport failed.", (int)Core.Color.BrightRed);
            }
        }

        private static async System.Threading.Tasks.Task KickPlayerAsync(int playerIndex, int targetIndex)
        {
            try
            {
                if (!await IsAdminAsync(playerIndex))
                {
                    NetworkSend.PlayerMsg(playerIndex, "You are not authorized to kick players.", (int)Core.Color.BrightRed);
                    return;
                }

                if (NetworkConfig.IsPlaying(targetIndex))
                {
                    NetworkSend.SendLeftGame(targetIndex);
                    Player.LeftGame(targetIndex);
                    Logger.LogInformation($"Player {targetIndex} kicked by {playerIndex}");
                    NetworkSend.PlayerMsg(playerIndex, $"Player {targetIndex} has been kicked.", (int)Core.Color.BrightGreen);
                }
                else
                {
                    NetworkSend.PlayerMsg(playerIndex, "Target player is not online.", (int)Core.Color.BrightRed);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Failed to kick player {targetIndex}");
                NetworkSend.PlayerMsg(playerIndex, "Kick operation failed.", (int)Core.Color.BrightRed);
            }
        }

        private static async System.Threading.Tasks.Task BroadcastMessageAsync(int playerIndex, string message)
        {
            try
            {
                if (!await IsAdminAsync(playerIndex))
                {
                    NetworkSend.PlayerMsg(playerIndex, "You are not authorized to broadcast.", (int)Core.Color.BrightRed);
                    return;
                }

                await SendChatMessageAsync(playerIndex, "global", $"[Broadcast] {message}", Color.BrightGreen);
                Logger.LogInformation($"Broadcast by {playerIndex}: {message}");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Broadcast failed");
                NetworkSend.PlayerMsg(playerIndex, "Broadcast failed.", (int)Core.Color.BrightRed);
            }
        }

        private static async System.Threading.Tasks.Task SendServerStatusAsync(int playerIndex)
        {
            try
            {
                bool isHealthy = await PerformHealthCheckAsync();
                string status = $"Server Status: {(isHealthy ? "Healthy" : "Unhealthy")}\n" +
                                $"Players Online: {CountPlayersOnline()}\n" +
                                $"Uptime: {MyStopwatch.Elapsed}\n" +
                                $"Errors: {Global.ErrorCount}";
                NetworkSend.PlayerMsg(playerIndex, status, (int)Core.Color.BrightGreen);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Failed to send server status");
                NetworkSend.PlayerMsg(playerIndex, "Unable to retrieve server status.", (int)Core.Color.BrightRed);
            }
        }

        private static async System.Threading.Tasks.Task SendHelpMessageAsync(int playerIndex)
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
            NetworkSend.PlayerMsg(playerIndex, help, (int)Core.Color.BrightGreen);
        }

        private static async System.Threading.Tasks.Task SendWhisperAsync(int senderIndex, string targetName, string message)
        {
            try
            {
                int targetIndex = await FindPlayerByNameAsync(targetName);
                if (targetIndex == -1)
                {
                    NetworkSend.PlayerMsg(senderIndex, $"Player '{targetName}' not found.", (int)Core.Color.BrightRed);
                    return;
                }

                await SendChatMessageAsync(senderIndex, $"private:{targetIndex}", $"[Whisper] {message}", Color.BrightCyan);
                Logger.LogInformation($"Whisper from {senderIndex} to {targetIndex}: {message}");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Failed to send whisper from {senderIndex} to {targetName}");
                NetworkSend.PlayerMsg(senderIndex, "Failed to send whisper.", (int)Core.Color.BrightRed);
            }
        }

        private static async System.Threading.Tasks.Task HandlePartyCommandAsync(int playerIndex, string subCommand, string targetName)
        {
            try
            {
                var player = Core.Data.TempPlayer[playerIndex];
                switch (subCommand.ToLower())
                {
                    case "create":
                        if (player.InParty != -1)
                        {
                            NetworkSend.PlayerMsg(playerIndex, "You are already in a party.", (int)Core.Color.BrightRed);
                            return;
                        }
                        player.InParty = (int)GenerateUniqueId();
                        NetworkSend.PlayerMsg(playerIndex, "Party created.", (int)Core.Color.BrightGreen);
                        break;

                    case "invite":
                        if (player.InParty == -1)
                        {
                            NetworkSend.PlayerMsg(playerIndex, "You must create a party first.", (int)Core.Color.BrightRed);
                            return;
                        }
                        int targetIndex = await FindPlayerByNameAsync(targetName);
                        if (targetIndex == -1)
                        {
                            NetworkSend.PlayerMsg(playerIndex, $"Player '{targetName}' not found.", (int)Core.Color.BrightRed);
                            return;
                        }
                        var targetPlayer = Core.Data.TempPlayer[targetIndex];
                        if (targetPlayer.InParty != -1)
                        {
                            NetworkSend.PlayerMsg(playerIndex, $"{targetName} is already in a party.", (int)Core.Color.BrightRed);
                            return;
                        }
                        targetPlayer.InParty = player.InParty;
                        NetworkSend.PlayerMsg(targetIndex, $"You have joined {Core.Data.Player[playerIndex].Name}'s party.", (int)Core.Color.BrightGreen);
                        NetworkSend.PlayerMsg(playerIndex, $"{targetName} has joined your party.", (int)Core.Color.BrightGreen);
                        break;

                    case "leave":
                        if (player.InParty == -1)
                        {
                            NetworkSend.PlayerMsg(playerIndex, "You are not in a party.", (int)Core.Color.BrightRed);
                            return;
                        }
                        player.InParty = -1;
                        NetworkSend.PlayerMsg(playerIndex, "You have left the party.", (int)Core.Color.BrightGreen);
                        break;

                    default:
                        NetworkSend.PlayerMsg(playerIndex, "Invalid party command. Use: create, invite, leave.", (int)Core.Color.BrightRed);
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Failed to handle party command for player {playerIndex}");
                NetworkSend.PlayerMsg(playerIndex, "Party command failed.", (int)Core.Color.BrightRed);
            }
        }

        private static async System.Threading.Tasks.Task SendPlayerStatsAsync(int playerIndex)
        {
            try
            {
                var stats = PlayerStatistics.GetOrAdd(playerIndex, new PlayerStats());
                string statsMessage = $"Your Stats:\n" +
                                      $"Kills: {stats.Kills}\n" +
                                      $"Deaths: {stats.Deaths}\n" +
                                      $"Playtime: {stats.PlayTime.TotalHours:F2} hours";
                NetworkSend.PlayerMsg(playerIndex, statsMessage, (int)Core.Color.BrightGreen);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Failed to send stats for player {playerIndex}");
                NetworkSend.PlayerMsg(playerIndex, "Failed to retrieve stats.", (int)Core.Color.BrightRed);
            }
        }

        private static async System.Threading.Tasks.Task SavePlayerDataAsync(int playerIndex)
        {
            try
            {
                await Database.SaveAccountAsync(playerIndex); // Assuming this method exists
                NetworkSend.PlayerMsg(playerIndex, "Your data has been saved.", (int)Core.Color.BrightGreen);
                Logger.LogInformation($"Player {playerIndex} data saved manually.");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Failed to save data for player {playerIndex}");
                NetworkSend.PlayerMsg(playerIndex, "Failed to save data.", (int)Core.Color.BrightRed);
            }
        }

        private static async System.Threading.Tasks.Task<int> FindPlayerByNameAsync(string name)
        {
            for (int i = 0; i < Core.Constant.MAX_PLAYERS; i++)
            {
                if (NetworkConfig.IsPlaying(i) && Core.Data.Player[i].Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }
            return -1;
        }

        private static async System.Threading.Tasks.Task SendChatMessageAsync(int senderIndex, string channel, string message, Core.Color color)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(message) || message.Length > 200) // Basic filtering
                {
                    NetworkSend.PlayerMsg(senderIndex, "Invalid message.", (int)Core.Color.BrightRed);
                    return;
                }

                if (channel.StartsWith("private:"))
                {
                    int targetIndex = int.Parse(channel.Split(':')[1]);
                    NetworkSend.PlayerMsg(targetIndex, $"[From {Core.Data.Player[senderIndex].Name}] {message}", (int)color);
                    NetworkSend.PlayerMsg(senderIndex, $"[To {Core.Data.Player[targetIndex].Name}] {message}", (int)color);
                }
                else if (channel == "party" && Core.Data.TempPlayer[senderIndex].InParty != 0)
                {
                    await Parallel.ForEachAsync(Enumerable.Range(0, Core.Constant.MAX_PLAYERS), Cts.Token, async (i, ct) =>
                    {
                        if (NetworkConfig.IsPlaying(i) && Core.Data.TempPlayer[i].InParty == Core.Data.TempPlayer[senderIndex].InParty)
                            NetworkSend.PlayerMsg(i, $"[Party] {Core.Data.Player[senderIndex].Name}: {message}", (int)color);
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
                NetworkSend.PlayerMsg(senderIndex, "Failed to send message.", (int)Core.Color.BrightRed);
            }
        }

        /// <summary>
        /// Handles player login events.
        /// </summary>
        public static async System.Threading.Tasks.Task OnPlayerLoginAsync(int playerIndex)
        {
            Logger.LogInformation($"Player {playerIndex} logged in.");
            NetworkSend.PlayerMsg(playerIndex, "Welcome to the server!", (int)Core.Color.BrightGreen);
            PlayerStatistics.GetOrAdd(playerIndex, new PlayerStats()).LoginTime = GetServerTime();
        }

        /// <summary>
        /// Handles player logout events.
        /// </summary>
        public static async System.Threading.Tasks.Task OnPlayerLogoutAsync(int playerIndex)
        {
            if (PlayerStatistics.TryGetValue(playerIndex, out var stats) && stats.LoginTime.HasValue)
            {
                stats.PlayTime += GetServerTime() - stats.LoginTime.Value;
                stats.LoginTime = null;
            }
            Logger.LogInformation($"Player {playerIndex} logged out.");
        }

        private static Task<bool> IsAdminAsync(int playerIndex) =>
            System.Threading.Tasks.Task.FromResult(playerIndex == 0); // Example admin check

        /// <summary>
        /// Creates a backup of the database asynchronously.
        /// </summary>
        public static async System.Threading.Tasks.Task BackupDatabaseAsync()
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
                    await System.Threading.Tasks.Task.Run(() => File.Delete(oldBackup), Cts.Token);
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

        private static async System.Threading.Tasks.Task HandleCriticalErrorAsync(Exception ex)
        {
            await BackupDatabaseAsync();
            Logger.LogCritical(ex, "Critical error occurred. Initiating emergency shutdown");
            await SendServerAnnouncementAsync("Server shutting down due to critical error.");
            await DestroyServerAsync();
        }

        /// <summary>
        /// Logs an error to a file and updates error count asynchronously.
        /// </summary>
        public static async System.Threading.Tasks.Task LogErrorAsync(Exception ex, string context = "")
        {
            string errorInfo = $"{ex.Message}\nStackTrace: {ex.StackTrace}";
            string logPath = System.IO.Path.Combine(Core.Path.Logs, "Errors.log");
            Directory.CreateDirectory(Core.Path.Logs);

            await File.AppendAllTextAsync(logPath,
                $"{DateTime.Now}\nContext: {context}\n{errorInfo}\n\n", Cts.Token);

            Interlocked.Increment(ref Global.ErrorCount);
            UpdateCaption();
        }

        public static async System.Threading.Tasks.Task CheckShutDownCountDownAsync()
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
