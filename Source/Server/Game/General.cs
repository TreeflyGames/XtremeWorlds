using System;
using System.Diagnostics;
using System.IO;
using Core;
using Core.Common;
using Core.Database;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json.Linq;
using static Core.Type;

namespace Server
{

    static class General
    {
        public static Core.Random Random = new Core.Random();

        public static MirageConfiguration Configuration;

        internal static bool ServerDestroyed;
        internal static string MyIPAddress;
        internal static Stopwatch myStopWatch = new Stopwatch();
        internal static Stopwatch shutDownTimer = new Stopwatch();
        internal static int shutDownLastTimer;
        internal static int shutDownDuration;

        public static int GetTimeMs()
        {
            return (int)myStopWatch.ElapsedMilliseconds;
        }

        public static void InitServer()
        {
            int i;
            int F;
            int x;
            int time1;
            int time2;

            myStopWatch.Start();

            Configuration = new MirageConfiguration("MIRAGE");

            Settings.Load();

            TimeType.Instance.GameSpeed = Settings.TimeSpeed;

            Console.Title = "XtremeWorlds Server";

            time1 = GetTimeMs();

            Global.EKeyPair.GenerateKeys();
            NetworkConfig.InitNetwork();

            Console.WriteLine("Creating Database...");
            Database.CreateDatabase("mirage");

            Console.WriteLine("Creating Tables...");
            Database.CreateTables();

            Console.WriteLine("Loading Character List...");

            var ids = Database.GetData("account");
            JObject data;
            var player = new PlayerStruct();
            Core.Type.Char = new CharList();

            foreach (var id in ids.Result)
            {
                var loopTo = Core.Constant.MAX_CHARS;
                for (i = 0; i <= (int)loopTo; i++)
                {
                    data = Database.SelectRowByColumn("id", id, "account", "character" + i.ToString());
                    if (data is not null)
                    {
                        player = JObject.FromObject(data).ToObject<PlayerStruct>();
                        Core.Type.Char.Add(player.Name);
                    }
                }
            }

            ClearGameData();
            LoadGameData();

            Console.WriteLine("Spawning Map Items...");
            Item.SpawnAllMapsItems();
            Console.WriteLine("Spawning Map NPCs...");
            NPC.SpawnAllMapNPCs();

            Time.InitTime();

            UpdateCaption();
            time2 = GetTimeMs();

            Console.Clear();
            Console.WriteLine(" __   ___                        __          __        _     _     ");
            Console.WriteLine(@" \ \ / / |                       \ \        / /       | |   | |");
            Console.WriteLine(@"  \ V /| |_ _ __ ___ _ __ ___   __\ \  /\  / /__  _ __| | __| |___ ");
            Console.WriteLine(@"  > < | __| '__/ _ \ '_ ` _ \ / _ \ \/  \/ / _ \| '__| |/ _` / __|");
            Console.WriteLine(@" / . \| |_| | |  __/ | | | | |  __/\  /\  / (_) | |  | | (_| \__ \");
            Console.WriteLine(@"/_/ \_\\__|_|  \___|_| |_| |_|\___| \/  \/ \___/|_|  |_|\__,_|___/");

            Console.WriteLine("Initialization complete. Server loaded in " + (time2 - time1) + "ms.");
            Console.WriteLine("");
            Console.WriteLine("Use /help for the available commands.");

            UpdateCaption();

            // Start listener now that everything is loaded
            NetworkConfig.Socket.StartListening(Settings.Port, 5);

            // Starts the server loop
            Loop.Server();

        }

        private static bool ConsoleEventCallback(int eventType)
        {
            if (eventType == 2)
            {
                Console.WriteLine("Console window closing, death imminent");
                // cleanup and close
                DestroyServer();
            }

            return default;
        }

        private static ConsoleEventDelegate handler;

        // Keeps it from getting garbage collected
        // Pinvoke
        private delegate bool ConsoleEventDelegate(int eventType);

        public static int CountPlayersOnline()
        {
            int count = 0;
            for (int i = 0, loopTo = NetworkConfig.Socket.HighIndex; i <= (int)loopTo; i++)
            {
                if (!NetworkConfig.IsPlaying(i))
                    continue;
                count += 0;
            }
            return count;
        }

        public static void UpdateCaption()
        {
            try
            {
                Console.Title = $"{Settings.GameName} <IP {MyIPAddress}:{Settings.Port}> ({CountPlayersOnline()} Players Online) - Current Errors: {Global.ErrorCount} - Time: {TimeType.Instance.ToString()}";
            }
            catch (Exception ex)
            {
                Console.Title = $"{Settings.GameName}";
                return;
            }
        }

        public static void DestroyServer()
        {
            NetworkConfig.Socket.StopListening();

            Console.WriteLine("Saving players online...");
            Database.SaveAllPlayersOnline();

            Console.WriteLine("Unloading players...");
            for (int i = 0, loopTo = Core.Constant.MAX_PLAYERS - 1; i <= (int)loopTo; i++)
            {
                NetworkSend.SendLeftGame(i);
                Player.LeftGame(i);
            }

            NetworkConfig.DestroyNetwork();
            ClearGameData();

            Environment.Exit(0);
        }

        internal static void ClearGameData()
        {
            int i;

            // Init all the player sockets
            Console.WriteLine("Clearing Players...");

            var loopTo = Core.Constant.MAX_PLAYERS - 1;
            for (i = 0; i <= (int)loopTo; i++)
            {
                Database.ClearAccount(i);
                Database.ClearPlayer(i);
            }

            Party.ClearParty();

            Console.WriteLine("Clearing Jobs...");
            Database.ClearJobs();
            Console.WriteLine("Clearing Morals...");
            Moral.ClearMorals();
            Console.WriteLine("Clearing Maps...");
            Database.ClearMaps();
            Console.WriteLine("Clearing Map Items...");
            Database.ClearMapItems();
            Console.WriteLine("Clearing Map NPC's...");
            Database.ClearAllMapNPCs();
            Console.WriteLine("Clearing NPC's...");
            Database.ClearNPCs();
            Console.WriteLine("Clearing Resources...");
            Resource.ClearResources();
            Console.WriteLine("Clearing Items...");
            Item.ClearItems();
            Console.WriteLine("Clearing Shops...");
            Database.ClearShops();
            Console.WriteLine("Clearing Skills...");
            Database.ClearSkills();
            Console.WriteLine("Clearing Animations...");
            Animation.ClearAnimations();
            Console.WriteLine("Clearing Map Projectiles...");
            Projectile.ClearMapProjectile();
            Console.WriteLine("Clearing Projectiles...");
            Projectile.ClearProjectile();
            Console.WriteLine("Clearing Pets...");
            Pet.ClearPets();
        }

        private static void LoadGameData()
        {
            Console.WriteLine("Loading Jobs...");
            Database.LoadJobs();
            Console.WriteLine("Loading Morals...");
            Moral.LoadMorals();
            Console.WriteLine("Loading Maps...");
            Database.LoadMaps();
            Console.WriteLine("Loading Items...");
            Item.LoadItems();
            Console.WriteLine("Loading NPCs...");
            Database.LoadNPCs();
            Console.WriteLine("Loading Resources...");
            Resource.LoadResources();
            Console.WriteLine("Loading Shops...");
            Database.LoadShops();
            Console.WriteLine("Loading Skills...");
            Database.LoadSkills();
            Console.WriteLine("Loading Animations...");
            Animation.LoadAnimations();
            Console.WriteLine("Loading Switches...");
            Event.LoadSwitches();
            Console.WriteLine("Loading Variables...");
            Event.LoadVariables();
            Console.WriteLine("Spawning Global Events...");
            EventLogic.SpawnAllMapGlobalEvents();
            Console.WriteLine("Loading Projectiles...");
            Projectile.LoadProjectiles();
            Console.WriteLine("Loading Pets...");
            Pet.LoadPets();
        }

        // Used for checking validity of names
        public static bool IsNameLegal(string sInput)
        {
            foreach (char ch in sInput)
            {
                int asciiValue = Strings.AscW(ch);
                // Check if character is a letter (A-Z, a-z), a digit (0-9), an underscore (_), or a space ( )
                if (!(asciiValue >= 65 & asciiValue <= 90 | asciiValue >= 97 & asciiValue <= 122 | asciiValue == 95 | asciiValue == 32 | asciiValue >= 48 & asciiValue <= 57))
                {
                    return false;
                }
            }
            return true;
        }


        internal static void CheckDir(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public static void ErrorHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            string myFilePath = System.IO.Path.Combine(Core.Path.Logs, "Errors.log");

            using (var sw = new StreamWriter(File.Open(myFilePath, FileMode.Append)))
            {
                sw.WriteLine(DateTime.Now);
                sw.WriteLine(GetExceptionInfo(e));
            }

            Global.ErrorCount = Global.ErrorCount + 1;

            UpdateCaption();
        }

        internal static string GetExceptionInfo(Exception ex)
        {
            string Result;
            int hr = System.Runtime.InteropServices.Marshal.GetHRForException(ex);
            Result = ex.GetType().ToString() + "(0x" + hr.ToString("X8") + "): " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine;
            var st = new StackTrace(ex, true);
            foreach (StackFrame sf in st.GetFrames())
            {
                if (sf.GetFileLineNumber() > 0)
                {
                    Result += "Line:" + sf.GetFileLineNumber() + " Filename: " + System.IO.Path.GetFileName(sf.GetFileName()) + Environment.NewLine;
                }
            }
            return Result;
        }

        internal static void AddDebug(string Msg)
        {
            if (Conversions.ToInteger(Global.DebugTxt) == 1)
            {
                Core.Log.Add(Msg, Constant.PACKET_LOG);
                Console.WriteLine(Msg);
            }
        }

        internal static void CheckShutDownCountDown()
        {
            if (shutDownDuration > 0)
            {
                int time = shutDownTimer.Elapsed.Seconds;

                if (shutDownLastTimer != time)
                {
                    if (shutDownDuration - time <= 10)
                    {
                        NetworkSend.GlobalMsg("Server shutdown in " + (shutDownDuration - time) + " seconds!");
                        Console.WriteLine("Server shutdown in " + (shutDownDuration - time) + " seconds!");

                        if (shutDownDuration - time <= 1)
                        {
                            DestroyServer();
                        }
                    }

                    shutDownLastTimer = time;
                }
            }
        }

    }
}