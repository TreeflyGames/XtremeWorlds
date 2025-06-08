using System;
using System.IO;
using System.Xml.Serialization;

namespace Core
{
    public class SettingsManager
    {
        // Singleton instance
        private static SettingsManager _instance;
        public static SettingsManager Instance => _instance ??= new SettingsManager();

        // Settings fields
        public string Language { get; set; } = "English";
        public string Username { get; set; } = "";
        public bool SaveUsername { get; set; } = true;

        public string MenuMusic { get; set; } = "menu.mid";
        public bool Music { get; set; } = true;
        public bool Sound { get; set; } = true;
        public float MusicVolume { get; set; } = 100.0f;
        public float SoundVolume { get; set; } = 100.0f;

        public string MusicExt { get; set; } = ".mid";
        public string SoundExt { get; set; } = ".ogg";

        public byte Resolution { get; set; } = 13;
        public bool Vsync { get; set; } = false;
        public bool ShowNPCBar { get; set; } = true;
        public bool Fullscreen { get; set; } = false;
        public byte CameraWidth { get; set; } = 32;
        public byte CameraHeight { get; set; } = 24;
        public bool OpenAdminPanelOnLogin { get; set; } = true;
        public bool DynamicLightRendering { get; set; } = true;
        public byte[] ChannelState { get; set; } = { 1, 1, 1, 1, 1, 1, 1 };

        public string IP { get; set; } = "127.0.0.1";
        public int Port { get; set; } = 7001;

        public string GameName { get; set; } = "XtremeWorlds";
        public string Website { get; set; } = "https://xtremeworlds.com/";

        public string Welcome { get; set; } = "Welcome to XtremeWorlds, enjoy your stay!";
        public double TimeSpeed { get; set; }
        public bool Autotile { get; set; } = true;

        public int MaxBackups { get; set; } = 5;

        public int ServerShutdown { get; set; } = 60;

        public int SaveInterval { get; set; } = 5;

        public int MaxSQLClients { get; set; } = 10;

        // Methods to load and save settings
        public static void Load()
        {
            string configPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config");
            string configFile = System.IO.Path.Combine(configPath, "Settings.xml");

            if (File.Exists(configFile))
            {
                try
                {
                    using var reader = new StreamReader(configFile);
                    var serializer = new XmlSerializer(typeof(SettingsManager));
                    _instance = (SettingsManager)serializer.Deserialize(reader);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to load settings: " + ex.Message);
                    _instance = new SettingsManager();
                }
            }
            else
            {
                Save(); // Save default settings if no file exists
            }
        }

        public static void Save()
        {
            string configPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config");
            string configFile = System.IO.Path.Combine(configPath, "Settings.xml");

            Directory.CreateDirectory(configPath);

            try
            {
                using var writer = new StreamWriter(configFile);
                var serializer = new XmlSerializer(typeof(SettingsManager));
                serializer.Serialize(writer, Instance);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to save settings: " + ex.Message);
            }
        }
    }
}
