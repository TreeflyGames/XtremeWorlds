using Core;
using Core.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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

namespace Server
{
    public static class General
    {
        private static readonly RandomUtility Random = new RandomUtility();
        private static IEngineContainer? Container;
        private static IConfiguration? Configuration;
        private static bool ServerDestroyed;
        private static string MyIPAddress = string.Empty;
        private static readonly Stopwatch MyStopwatch = new Stopwatch();
        private static readonly ILogger<General> Logger;
        private static readonly object SyncLock = new object();
        private static readonly CancellationTokenSource Cts = new CancellationTokenSource();
        private static Timer? SaveTimer;

        static General()
        {
            Logger = GetLogger<General>();
            InitializeSaveTimer();
        }

        #region Utility Methods

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

        private static async Task LoadGameDataAsync()
        {
            var stopwatch = Stopwatch.StartNew();
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
            await Database.SaveAllPlayersOnlineAsync(Cts.Token);

            await Parallel.ForEachAsync(Enumerable.Range(0, Core.Constant.MAX_PLAYERS), Cts.Token, async (i, ct) =>
            {
                await NetworkSend.SendLeftGameAsync(i);
                Player.LeftGame(i);
            });

            NetworkConfig.DestroyNetwork();
            ClearGameData();
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
                Database.ConnectionString = Configuration.GetConnectionString("Database");
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
            await NetworkConfig.InitNetworkAsync();
        }

        private static async Task InitializeDatabaseWithRetryAsync()
        {
            const int maxRetries = 3;
            Logger.LogInformation("Initializing database...");
            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                try
                {
                    await Database.CheckConnectionAsync(Cts.Token);
                    await Database.CreateDatabaseAsync("mirage", Cts.Token);
                    await Database.CreateTablesAsync(Cts.Token);
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
            var ids = await Database.GetDataAsync("account", Cts.Token);
            Core.Type.Char = new CharList();
            const int maxConcurrency = 4;
            using var semaphore = new SemaphoreSlim(maxConcurrency);

            var tasks = ids.Result.Select(async id =>
            {
                await semaphore.WaitAsync(Cts.Token);
                try
                {
                    for (int i = 1; i <= Core.Constant.MAX_CHARS; i++)
                    {
                        var data = await Database.SelectRowByColumnAsync("id", id, "account", $"character{i}", Cts.Token);
                        if (data != null)
                        {
                            var player = JObject.FromObject(data).ToObject<PlayerStruct>();
                            if (!string.IsNullOrWhiteSpace(player.Name))
                            {
                                await Core.Type.Char.AddAsync(player.Name);
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
            ClearGameData();
            const int maxConcurrency = 4;
            using var semaphore = new SemaphoreSlim(maxConcurrency);

            var tasks = new[]
            {
                LoadWithSemaphoreAsync(semaphore, () => Database.LoadJobsAsync(Cts.Token)),
                LoadWithSemaphoreAsync(semaphore, () => Moral.LoadMoralsAsync()),
                LoadWithSemaphoreAsync(semaphore, () => Database.LoadMapsAsync(Cts.Token)),
                LoadWithSemaphoreAsync(semaphore, () => Item.LoadItemsAsync()),
                LoadWithSemaphoreAsync(semaphore, () => Database.LoadNPCsAsync(Cts.Token)),
                LoadWithSemaphoreAsync(semaphore, () => Resource.LoadResourcesAsync()),
                LoadWithSemaphoreAsync(semaphore, () => Database.LoadShopsAsync(Cts.Token)),
                LoadWithSemaphoreAsync(semaphore, () => Database.LoadSkillsAsync(Cts.Token)),
                LoadWithSemaphoreAsync(semaphore, () => Animation.LoadAnimationsAsync()),
                LoadWithSemaphoreAsync(semaphore, () => Event.LoadSwitchesAsync()),
                LoadWithSemaphoreAsync(semaphore, () => Event.LoadVariablesAsync()),
                LoadWithSemaphoreAsync(semaphore, () => Projectile.LoadProjectilesAsync()),
                LoadWithSemaphoreAsync(semaphore, () => Pet.LoadPetsAsync())
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
                NPC.SpawnAllMapNPCsAsync(),
                EventLogic.SpawnAllMapGlobalEventsAsync()
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
                await Database.CheckConnectionAsync(Cts.Token);
                bool networkActive = NetworkConfig.Socket.IsListening();
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
            int intervalMinutes = Settings.Instance.PlayerSaveInterval > 0 ? Settings.Instance.PlayerSaveInterval : 5;
            SaveTimer = new Timer(async _ => await SavePlayersPeriodicallyAsync(), null,
                TimeSpan.FromMinutes(intervalMinutes), TimeSpan.FromMinutes(intervalMinutes));
        }

        private static async Task SavePlayersPeriodicallyAsync()
        {
            try
            {
                await Database.SaveAllPlayersOnlineAsync(Cts.Token);
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
                        await TeleportPlayerAsync(playerIndex, x, y);
                    else
                        await NetworkSend.SendMessageAsync(playerIndex, "Usage: /teleport <x> <y>");
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
                    await NetworkSend.SendMessageAsync(playerIndex, "Unknown command. Use /help for assistance.");
                    break;
            }
        }

        private static async Task TeleportPlayerAsync(int playerIndex, int x, int y)
        {
            try
            {
                var player = await Database.GetPlayerAsync(playerIndex, Cts.Token); // Hypothetical method
                if (player == null) return;

                // Validate coordinates (assuming a map size of 100x100 as an example)
                if (x < 0 || x >= 100 || y < 0 || y >= 100)
                {
                    await NetworkSend.SendMessageAsync(playerIndex, "Coordinates out of bounds.");
                    return;
                }

                player.X = x;
                player.Y = y;
                await Database.UpdatePlayerPositionAsync(playerIndex, x, y, Cts.Token);
                await NetworkSend.SendPlayerPositionAsync(playerIndex);
                Logger.LogInformation($"Player {playerIndex} teleported to ({x}, {y})");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Failed to teleport player {playerIndex}");
                await NetworkSend.SendMessageAsync(playerIndex, "Teleport failed.");
            }
        }

        private static async Task KickPlayerAsync(int playerIndex, int targetIndex)
        {
            try
            {
                // Placeholder authorization check
                if (!await IsAdminAsync(playerIndex))
                {
                    await NetworkSend.SendMessageAsync(playerIndex, "You are not authorized to kick players.");
                    return;
                }

                if (NetworkConfig.IsPlaying(targetIndex))
                {
                    await NetworkSend.SendLeftGameAsync(targetIndex);
                    Player.LeftGame(targetIndex);
                    Logger.LogInformation($"Player {targetIndex} kicked by {playerIndex}");
                    await NetworkSend.SendMessageAsync(playerIndex, $"Player {targetIndex} has been kicked.");
                }
                else
                {
                    await NetworkSend.SendMessageAsync(playerIndex, "Target player is not online.");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Failed to kick player {targetIndex}");
                await NetworkSend.SendMessageAsync(playerIndex, "Kick operation failed.");
            }
        }

        private static async Task BroadcastMessageAsync(int playerIndex, string message)
        {
            try
            {
                if (!await IsAdminAsync(playerIndex))
                {
                    await NetworkSend.SendMessageAsync(playerIndex, "You are not authorized to broadcast.");
                    return;
                }

                await Parallel.ForEachAsync(Enumerable.Range(0, Core.Constant.MAX_PLAYERS), Cts.Token, async (i, ct) =>
                {
                    if (NetworkConfig.IsPlaying(i))
                        await NetworkSend.SendMessageAsync(i, $"[Broadcast] {message}");
                });
                Logger.LogInformation($"Broadcast by {playerIndex}: {message}");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Broadcast failed");
                await NetworkSend.SendMessageAsync(playerIndex, "Broadcast failed.");
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
                await NetworkSend.SendMessageAsync(playerIndex, status);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Failed to send server status");
                await NetworkSend.SendMessageAsync(playerIndex, "Unable to retrieve server status.");
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
            await NetworkSend.SendMessageAsync(playerIndex, help);
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
                string backupDir = Core.Path.Backups;
                Directory.CreateDirectory(backupDir);
                string backupPath = Path.Combine(backupDir, $"backup_{DateTime.Now:yyyyMMdd_HHmmss}.bak");
                await Database.BackupAsync(backupPath, Cts.Token);
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
            string logPath = Path.Combine(Core.Path.Logs, "Errors.log");
            Directory.CreateDirectory(Core.Path.Logs);

            await File.AppendAllTextAsync(logPath,
                $"{DateTime.Now}\nContext: {context}\n{errorInfo}\n\n", Cts.Token);

            Interlocked.Increment(ref Global.ErrorCount);
            UpdateCaption();
        }

        #endregion
    }
}
