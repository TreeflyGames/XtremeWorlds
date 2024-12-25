using System;
using System.IO;
using System.Xml.Serialization;

namespace Core
{

    public class Settings
    {
        public static string Language = "English";

        public static string Username = "";
        public static bool SaveUsername = true;

        public static string MenuMusic = "menu.mid";
        public static bool Music = true;
        public static bool Sound = true;
        public static float MusicVolume = 100.0f;
        public static float SoundVolume = 100.0f;

        public static string MusicExt = ".mid";
        public static string SoundExt = ".ogg";

        public static byte Resolution = 13;
        public static bool Vsync = true;
        public static bool ShowNPCBar = true;
        public static bool Fullscreen = false;
        public static byte CameraWidth = 32;
        public static byte CameraHeight = 24;
        public static bool OpenAdminPanelOnLogin = true;
        public static bool DynamicLightRendering = true;
        public static byte[] ChannelState = new byte[7];

        public static string Ip = "127.0.0.1";
        public static int Port = 7001;

        [XmlIgnore()]
        public static string GameName = "XtremeWorlds";
        [XmlIgnore()]
        public static string Website = "https://xtremeworlds.com/";

        public static string Welcome = "Welcome to XtremeWorlds, enjoy your stay!";

        public static double TimeSpeed;

        public static bool Autotile = true;

        public static void Load()
        {
            string configPath = Path.Config;
            string configFile = System.IO.Path.Combine(configPath, "Settings.xml");

            Directory.CreateDirectory(configPath);

            if (!File.Exists(configFile))
            {
                try
                {
                    using (var writer = new StreamWriter(File.Create(configFile)))
                    {
                        var serializer = new XmlSerializer(typeof(Settings), new XmlRootAttribute("Settings"));
                        serializer.Serialize(writer, new Settings()); // Serialize default settings
                    }
                }
                catch (IOException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (UnauthorizedAccessException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            try
            {
                using (var reader = new StreamReader(configFile))
                {
                    var serializer = new XmlSerializer(typeof(Settings), new XmlRootAttribute("Settings"));
                    Type.Setting = (Settings)serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                Type.Setting = new Settings(); // Default to new settings if reading fails
            }
        }

        public static void Save()
        {
            string configPath = Path.Config;
            string configFile = System.IO.Path.Combine(configPath, "Settings.xml");

            Directory.CreateDirectory(configPath);

            try
            {
                using (var writer = new StreamWriter(configFile))
                {
                    var serializer = new XmlSerializer(typeof(Settings), new XmlRootAttribute("Settings"));
                    serializer.Serialize(writer, Type.Setting);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}