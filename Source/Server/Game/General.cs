using Core;
using Core.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Xna.Framework.Content;
using Newtonsoft.Json.Linq;
using Reoria.Engine.Base.Container;
using Reoria.Engine.Base.Container.Interfaces;
using Reoria.Engine.Base.Container.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using static Core.Type;

namespace Server
{
    public class General
    {
        private static readonly RandomUtility Random = new RandomUtility();
        private static IEngineContainer? Container;
        private static IConfiguration? Configuration;
        private static bool ServerDestroyed;
        private static string MyIPAddress = string.Empty;
        private static readonly Stopwatch MyStopwatch = new Stopwatch();
        private static ILogger Logger;
        private static readonly object SyncLock = new object();
        private static readonly CancellationTokenSource Cts = new CancellationTokenSource();
        private static Timer? SaveTimer;
        private static Stopwatch ShutDownTimer = new Stopwatch();
        private static int ShutDownLastTimer = 0;

        static General()
        {
            InitializeSaveTimer();
        }

        #region Utility Methods

        /// <summary>
        /// Retrieves the shut down timer to destroy the server after a specified time.
        /// </summary>
        public static Stopwatch? GetShutDownTimer => ShutDownTimer;

        /// <summary>
        /// Retrives the current server destroy status.
        /// </summary>
        public static bool IsServerDestroyed => ServerDestroyed;

        /// <summary>
        /// Retrieves the current random number generator.
        /// </summary>
        public static RandomUtility GetRandom => Random;

        /// <summary>
        /// Retrieves a config isntance for the specified type.
        /// </summary>
        public static IConfiguration GetConfig => Configuration ?? throw new NullReferenceException("Configuration not initialized");

        /// <summary>
        /// Retrieves a logger instance for the specified type.
        /// </summary>
        public static ILogger<T> GetLogger<T>() where T : class =>
            Container?.RetrieveService<Logger<T>>() ?? throw new NullReferenceException("Container not initialized");

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
        public static bool IsValidUsername(string username) =>
            !string.IsNullOrWhiteSpace(username) &&
            username.Length >= 3 &&
            username.Length <= 20 &&
            Regex.IsMatch(username, @"^[a-zA-Z0-9_ ]+$");

        #endregion

        #region Server Lifecycle

        /// <summary>
        /// Initializes the game server asynchronously.
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
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex, "Server initialization failed");
                await HandleCriticalErrorAsync(ex);
            }
        }

        private static async Task InitializeCoreComponentsAsync()
        {
            await InitializeContainerAsync();
            await Task.WhenAll(LoadConfigurationAsync(), InitializeNetworkAsync()); // Parallelize independent tasks
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
            Core.Type.Shop = new ShopStruct[Core.Constant.MAX_SHOPS];
            Core.Type.Skill = new SkillStruct[Core.Constant.MAX_SKILLS];
            Core.Type.MapResource = new Core.Type.MapResourceStruct[Core.Constant.MAX_MAPS];
            Core.Type.TempPlayer = new Core.Type.TempPlayerStruct[Core.Constant.MAX_PLAYERS];
            Core.Type.Account = new Core.Type.AccountStruct[Core.Constant.MAX_PLAYERS];
            Core.Type.MapProjectile = new Core.Type.MapProjectileStruct[Core.Constant.MAX_MAPS, Core.Constant.MAX_PROJECTILES];
        }

        private static async Task LoadGameDataAsync()
        {
            var stopwatch = Stopwatch.StartNew();
            InitalizeCoreData();
            await LoadGameContentAsync();
            await SpawnGameObjectsAsync();
            Time.InitTime();
            Logger.LogInformation($"Game data loaded in {stopwatch.ElapsedMilliseconds}ms");
        }

        private static async Task StartGameLoopAsync(int startTime)
        {
            DisplayServerBanner(startTime);
            UpdateCaption();
            await NetworkConfig.Socket.StartListeningAsync(Settings.Instance.Port, 5);
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
        /// Shuts down the server gracefully, saving player data and cleaning up resources.
        /// </summary>
        public static async Task DestroyServerAsync()
        {
            if (ServerDestroyed) return;
            ServerDestroyed = true;
            Cts.Cancel();
            SaveTimer?.Dispose();
            NetworkConfig.Socket.StopListening();

            Logger.LogInformation("Server shutdown initiated...");
            await Database.SaveAllPlayersOnlineAsync();

            await Parallel.ForEachAsync(Enumerable.Range(0, Core.Constant.MAX_PLAYERS), Cts.Token, async (i, ct) =>
            {
                NetworkSend.SendLeftGame(i);
                Player.LeftGame(i);
            });

            NetworkConfig.DestroyNetwork();
            Logger.LogInformation("Server shutdown completed.");
            Environment.Exit(0);
        }

        #endregion

        #region Initialization Methods

        private static async Task InitializeContainerAsync()
        {
            Container = new EngineContainer<SerilogLoggingInitializer>()
                .DiscoverContainerServiceClasses()
                .DiscoverConfigurationSources()
                .BuildContainerConfiguration()
                .BuildContainerLogger()
                .DiscoverContainerServices()
                .BuildContainerServices()
                .BuildContainerServiceProvider();

            Configuration = Container?.RetrieveService<IConfiguration>() ??
                throw new NullReferenceException("Failed to initialize configuration");

            Logger = Container?.RetrieveService<ILogger<General>>() ??
                throw new NullReferenceException("Failed to initialize logger");
        }

        private static async Task LoadConfigurationAsync()
        {
            await Task.Run(() =>
            {
                Settings.Load();
                ValidateConfiguration();
                Clock.Instance.GameSpeed = Settings.Instance.TimeSpeed;
                Console.Title = "XtremeWorlds Server";
                MyIPAddress = GetLocalIPAddress();
            });
        }

        private static void ValidateConfiguration()
        {
            if (string.IsNullOrWhiteSpace(Settings.Instance.GameName))
                throw new InvalidOperationException("GameName is not set in configuration");
            if (Settings.Instance.Port <= 0 || Settings.Instance.Port > 65535)
                throw new InvalidOperationException("Invalid Port number in configuration");
        }

        private static async Task InitializeNetworkAsync()
        {
            Global.EKeyPair.GenerateKeys();
            NetworkConfig.InitNetwork();
        }

        private static async Task InitializeDatabaseWithRetryAsync()
        {
            const int maxRetries = 3;
            Logger.LogInformation("Initializing database...");
            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                try
                {
                    Database.CreateDatabase("mirage");
                    Database.CreateTables();
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
                    await Task.Delay(1000 * attempt, Cts.Token); // Exponential backoff
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
                    for (int i = 1; i <= Core.Constant.MAX_CHARS; i++)
                    {
                        var data = Database.SelectRowByColumn("id", id, "account", $"character{i}");
                        if (data != null)
                        {
                            var player = JObject.FromObject(data).ToObject<PlayerStruct>();
                            if (!string.IsNullOrWhiteSpace(player.Name))
                            {
                                Core.Type.Char.Add(player.Name);
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
            Logger.LogInformation($"Loaded {Core.Type.Char.Count} characters");
        }

        private static async Task LoadGameContentAsync()
        {
            const int maxConcurrency = 4;
            using var semaphore = new SemaphoreSlim(maxConcurrency);

            var tasks = new[]
            {
                LoadWithSemaphoreAsync(semaphore, async () => Database.LoadJobsAsync()),
                LoadWithSemaphoreAsync(semaphore, async () => await Moral.LoadMoralsAsync()),
                LoadWithSemaphoreAsync(semaphore, async () => await Database.LoadMapsAsync()),
                LoadWithSemaphoreAsync(semaphore, async () => await Item.LoadItemsAsync()),
                LoadWithSemaphoreAsync(semaphore, async () => await Database.LoadNPCsAsync()),
                LoadWithSemaphoreAsync(semaphore, async () => await Resource.LoadResourcesAsync()),
                LoadWithSemaphoreAsync(semaphore, async () => await Database.LoadShopsAsync()),
                LoadWithSemaphoreAsync(semaphore, async () => await Database.LoadSkillsAsync()),
                LoadWithSemaphoreAsync(semaphore, async () => await Animation.LoadAnimationsAsync()),
                LoadWithSemaphoreAsync(semaphore, async () => await Event.LoadSwitchesAsync()),
                LoadWithSemaphoreAsync(semaphore, async () => await Event.LoadVariablesAsync()),
                LoadWithSemaphoreAsync(semaphore, async () => await Projectile.LoadProjectilesAsync()),
                LoadWithSemaphoreAsync(semaphore, async () => await Pet.LoadPetsAsync())
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
                return Enumerable.Range(0, NetworkConfig.Socket.HighIndex + 1)
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
                Console.Title = $"{Settings.Instance.GameName} <IP {MyIPAddress}:{Settings.Instance.Port}> " +
                    $"({CountPlayersOnline()} Players Online) - Errors: {Global.ErrorCount} - Time: {Clock.Instance}";
            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex, "Failed to update console title");
                Console.Title = Settings.Instance.GameName;
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
            int intervalMinutes = Settings.Instance.SaveInterval;
            SaveTimer = new Timer(async _ => await SavePlayersPeriodicallyAsync(), null,
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

        /// <summary>
        /// Handles player commands asynchronously with enhanced functionality.
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

                default:
                    NetworkSend.PlayerMsg(playerIndex, "Unknown command. Use /help for assistance.", (int)Core.Enum.ColorType.BrightRed);
                    break;
            }
        }

        private static async Task TeleportPlayerAsync(int playerIndex, byte x, byte y)
        {
            try
            {
                ref var player = ref Core.Type.Player[playerIndex]; // Hypothetical method

                // Validate coordinates (assuming a map size of 100x100 as an example)
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
                // Placeholder authorization check
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

                await Parallel.ForEachAsync(Enumerable.Range(0, Core.Constant.MAX_PLAYERS), Cts.Token, async (i, ct) =>
                {
                    if (NetworkConfig.IsPlaying(i))
                        NetworkSend.PlayerMsg(i, $"[Broadcast] {message}", (int)Core.Enum.ColorType.BrightGreen);
                });
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
                          "/help - Show this message";
            NetworkSend.PlayerMsg(playerIndex, help, (int)Core.Enum.ColorType.BrightGreen);
        }

        // Placeholder method for admin check
        private static Task<bool> IsAdminAsync(int playerIndex) =>
            Task.FromResult(playerIndex == 0); // Example: player 0 is admin

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
                await General.BackupDatabaseAsync();
                Logger.LogInformation($"Database backup created: {backupPath}");

                var backups = Directory.GetFiles(backupDir, "backup_*.bak")
                    .OrderByDescending(f => f)
                    .Skip(Settings.Instance.MaxBackups)
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
            await DestroyServerAsync();
        }

        /// <summary>
        /// Logs an error to a file and updates error count asynchronously.
        /// </summary>
        public static async Task LogErrorAsync(Exception ex, string context = "")
        {
            string errorInfo = ex.ToString(); // Simplified; replace with GetExceptionInfo if available
            string logPath = System.IO.Path.Combine(Core.Path.Logs, "Errors.log");
            Directory.CreateDirectory(Core.Path.Logs);

            await File.AppendAllTextAsync(logPath,
                $"{DateTime.Now}\nContext: {context}\n{errorInfo}\n\n", Cts.Token);

            Interlocked.Increment(ref Global.ErrorCount);
            UpdateCaption();
        }

        public static async Task CheckShutDownCountDownAsync()
        {
            if (General.ShutDownTimer.ElapsedTicks > 0)
            {
                int time = General.ShutDownTimer.Elapsed.Seconds;

                if (General.ShutDownLastTimer != time)
                {
                    if (General.ShutDownLastTimer - time <= 10)
                    {
                        NetworkSend.GlobalMsg("Server shutdown in " + (-time) + " seconds!");
                        Console.WriteLine("Server shutdown in " + (Settings.Instance.ServerShutdown - time) + " seconds!");

                        if (Settings.Instance.ServerShutdown - time <= 1)
                        {
                            await General.DestroyServerAsync();
                        }
                    }

                    General.ShutDownLastTimer = time;
                }
            }

            #endregion
        }
    }
}
