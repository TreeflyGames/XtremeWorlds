using Core;
using Core.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Reoria.Engine.Base.Container;
using Reoria.Engine.Base.Container.Interfaces;
using Reoria.Engine.Base.Container.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net;
using System.Text.RegularExpressions;
using static Core.Type;

namespace Server
{
    static class General
    {
        private static readonly RandomUtility Random = new RandomUtility();
        private static IEngineContainer? Container;
        private static IConfiguration? Configuration;
        private static bool ServerDestroyed;
        private static string MyIPAddress = string.Empty;
        private static readonly Stopwatch MyStopwatch = new Stopwatch();
        private static readonly Stopwatch ShutdownTimer = new Stopwatch();
        private static int ShutdownLastTimer;
        private static int ShutdownDuration;
        private static readonly ILogger<General> Logger;
        private static readonly object SyncLock = new object();

        static General()
        {
            Logger = GetLogger<General>();
        }

        public static ILogger<T> GetLogger<T>() where T : class =>
            Container?.RetrieveService<Logger<T>>() ?? throw new NullReferenceException("Container not initialized");

        public static int GetTimeMs() => (int)MyStopwatch.ElapsedMilliseconds;

        public static async Task InitServerAsync()
        {
            try
            {
                MyStopwatch.Start();
                int time1 = GetTimeMs();

                await InitializeContainerAsync();
                await LoadConfigurationAsync();
                await InitializeNetworkAsync();
                await InitializeDatabaseAsync();
                await LoadGameContentAsync();

                time1 = GetTimeMs();
                await SpawnGameObjectsAsync();
                Time.InitTime();

                DisplayServerBanner(time1);
                await StartServerLoopAsync();
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex, "Server initialization failed");
                await HandleCriticalErrorAsync(ex);
            }
        }

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
                Clock.Instance.GameSpeed = Settings.Instance.TimeSpeed;
                Console.Title = "XtremeWorlds Server";
                MyIPAddress = GetLocalIPAddress();
            });
        }

        private static async Task InitializeNetworkAsync()
        {
            Global.EKeyPair.GenerateKeys();
            await NetworkConfig.InitNetworkAsync();
        }

        private static async Task InitializeDatabaseAsync()
        {
            Logger.LogInformation("Initializing database...");
            await Database.CreateDatabaseAsync("mirage");
            await Database.CreateTablesAsync();
            await LoadCharacterListAsync();
        }

        private static async Task LoadCharacterListAsync()
        {
            var ids = await Database.GetDataAsync("account");
            Core.Type.Char = new CharList();
            
            await Parallel.ForEachAsync(ids.Result, new ParallelOptions { MaxDegreeOfParallelism = 4 }, async (id, ct) =>
            {
                for (int i = 1; i <= Core.Constant.MAX_CHARS; i++)
                {
                    var data = await Database.SelectRowByColumnAsync("id", id, "account", $"character{i}");
                    if (data != null)
                    {
                        var player = JObject.FromObject(data).ToObject<PlayerStruct>();
                        if (!string.IsNullOrWhiteSpace(player.Name))
                        {
                            await Core.Type.Char.AddAsync(player.Name);
                        }
                    }
                }
            });
        }

        private static async Task LoadGameContentAsync()
        {
            ClearGameData();
            await Task.WhenAll(
                Database.LoadJobsAsync(),
                Moral.LoadMoralsAsync(),
                Database.LoadMapsAsync(),
                Item.LoadItemsAsync(),
                Database.LoadNPCsAsync(),
                Resource.LoadResourcesAsync(),
                Database.LoadShopsAsync(),
                Database.LoadSkillsAsync(),
                Animation.LoadAnimationsAsync(),
                Event.LoadSwitchesAsync(),
                Event.LoadVariablesAsync(),
                Projectile.LoadProjectilesAsync(),
                Pet.LoadPetsAsync()
            );
        }

        private static async Task SpawnGameObjectsAsync()
        {
            await Task.WhenAll(
                Item.SpawnAllMapsItemsAsync(),
                NPC.SpawnAllMapNPCsAsync(),
                EventLogic.SpawnAllMapGlobalEventsAsync()
            );
        }

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

        private static async Task StartServerLoopAsync()
        {
            UpdateCaption();
            await NetworkConfig.Socket.StartListeningAsync(Settings.Instance.Port, 5);
            await Loop.ServerAsync();
        }

        public static int CountPlayersOnline()
        {
            lock SyncLock)
            {
                return Enumerable.Range(0, NetworkConfig.Socket.HighIndex + 1)
                    .Count(i => NetworkConfig.IsPlaying(i));
            }
        }

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

        public static async Task DestroyServerAsync()
        {
            ServerDestroyed = true;
            NetworkConfig.Socket.StopListening();
            
            Logger.LogInformation("Server shutdown initiated...");
            await Database.SaveAllPlayersOnlineAsync();

            await Parallel.ForEachAsync(Enumerable.Range(0, Core.Constant.MAX_PLAYERS), async (i, ct) =>
            {
                await NetworkSend.SendLeftGameAsync(i);
                Player.LeftGame(i);
            });

            NetworkConfig.DestroyNetwork();
            ClearGameData();
            Environment.Exit(0);
        }

        // New utility methods
        public static bool IsValidUsername(string username)
        {
            return !string.IsNullOrWhiteSpace(username) &&
                   username.Length >= 3 &&
                   username.Length <= 20 &&
                   Regex.IsMatch(username, @"^[a-zA-Z0-9_ ]+$");
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            return host.AddressList
                .FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                ?.ToString() ?? "127.0.0.1";
        }

        public static async Task BackupDatabaseAsync()
        {
            try
            {
                string backupPath = Path.Combine(Core.Path.Backups, $"backup_{DateTime.Now:yyyyMMdd_HHmmss}.bak");
                CheckDir(Path.GetDirectoryName(backupPath));
                await Database.BackupAsync(backupPath);
                Logger.LogInformation($"Database backup created: {backupPath}");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Database backup failed");
            }
        }

        private static async Task HandleCriticalErrorAsync(Exception ex)
        {
            await BackupDatabaseAsync();
            Logger.LogCritical(ex, "Critical error occurred. Initiating emergency shutdown");
            await DestroyServerAsync();
        }

        // Enhanced error handling
        public static async Task LogErrorAsync(Exception ex, string context = "")
        {
            string errorInfo = GetExceptionInfo(ex);
            string logPath = Path.Combine(Core.Path.Logs, "Errors.log");
            
            await File.AppendAllTextAsync(logPath, 
                $"{DateTime.Now}\nContext: {context}\n{errorInfo}\n\n");
            
            Interlocked.Increment(ref Global.ErrorCount);
            UpdateCaption();
        }
    }
}
