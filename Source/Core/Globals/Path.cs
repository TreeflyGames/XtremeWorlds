using System;
using System.ComponentModel;
using System.Diagnostics.SymbolStore;
using System.IO;
using System.Reflection;

namespace Core
{

    public class Path
    {
        /// <summary> Returns the application directory </summary>
        public static string Local
        {
            get
            {
                string assemblyPath = Assembly.GetEntryAssembly().Location;
                return Directory.GetParent(assemblyPath).FullName;
            }
        }

        /// <summary> Returns content directory </summary>
        public static string Asset
        {
            get
            {
                return System.IO.Path.Combine(Local, "Content");
            }
        }

        /// <summary> Returns config directory </summary>
        public static string Config
        {
            get
            {
                return System.IO.Path.Combine(Local, "Config");
            }
        }

        /// <summary> Returns skins directory </summary>
        public static string Skins
        {
            get
            {
                if (OperatingSystem.IsMacOS())
                {
                    var appData = System.IO.Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                        "XtremeWorlds"
                    );
                    Directory.CreateDirectory(appData); // Ensure it exists
                    return System.IO.Path.Combine(appData, "Config");
                }
                else
                {
                    return System.IO.Path.Combine(Local, "Config");
                }
            }
        }

        /// <summary> Returns graphics directory </summary>
        public static string Graphics
        {
            get
            {
                return System.IO.Path.Combine(Asset, "Graphics");
            }
        }

        /// <summary> Returns Fonts directory </summary>
        public static string Fonts
        {
            get
            {
                return System.IO.Path.Combine("", "Fonts");
            }
        }

        /// <summary> Returns GUI directory </summary>
        public static string Gui
        {
            get
            {
                return System.IO.Path.Combine(Graphics, "Gui");
            }
        }

        /// <summary> Returns gradients directory </summary>
        public static string Gradients
        {
            get
            {
                return System.IO.Path.Combine(Gui, "Gradients");
            }
        }

        /// <summary> Returns designs directory </summary>
        public static string Designs
        {
            get
            {
                return System.IO.Path.Combine(Gui, "Designs");
            }
        }

        /// <summary> Returns tilesets directory </summary>
        public static string Tilesets
        {
            get
            {
                return System.IO.Path.Combine(Graphics, "Tilesets");
            }
        }

        /// <summary> Returns characters directory </summary>
        public static string Characters
        {
            get
            {
                return System.IO.Path.Combine(Graphics, "Characters");
            }
        }

        /// <summary> Returns emotes directory </summary>
        public static string Emotes
        {
            get
            {
                return System.IO.Path.Combine(Graphics, "Emotes");
            }
        }

        /// <summary> Returns paperdolls directory </summary>
        public static string Paperdolls
        {
            get
            {
                return System.IO.Path.Combine(Graphics, "Paperdolls");
            }
        }

        /// <summary> Returns fogs directory </summary>
        public static string Fogs
        {
            get
            {
                return System.IO.Path.Combine(Graphics, "Fogs");
            }
        }

        /// <summary> Returns parallax directory </summary>
        public static string Parallax
        {
            get
            {
                return System.IO.Path.Combine(Graphics, "Parallax");
            }
        }

        /// <summary> Returns panoramas directory </summary>
        public static string Panoramas
        {
            get
            {
                return System.IO.Path.Combine(Graphics, "Panoramas");
            }
        }

        /// <summary> Returns pictures directory </summary>
        public static string Pictures
        {
            get
            {
                return System.IO.Path.Combine(Graphics, "Pictures");
            }
        }

        /// <summary> Returns logs directory </summary>
        public static string Logs
        {
            get
            {
                return System.IO.Path.Combine(Local, "Logs");
            }
        }

        /// <summary> Returns database directory </summary>
        public static string Database
        {
            get
            {
                return System.IO.Path.Combine(Local, "Database");
            }
        }

        /// <summary> Returns music directory </summary>
        public static string Music
        {
            get
            {
                return System.IO.Path.Combine(Asset, "Music");
            }
        }

        /// <summary> Returns sounds directory </summary>
        public static string Sounds
        {
            get
            {
                return System.IO.Path.Combine(Asset, "Sounds");
            }
        }

        /// <summary> Returns items directory </summary>
        public static string Items
        {
            get
            {
                return System.IO.Path.Combine(Graphics, "Items");
            }
        }

        /// <summary> Returns maps directory </summary>
        public static string Maps
        {
            get
            {
                return System.IO.Path.Combine(Graphics, "Maps");
            }
        }

        /// <summary> Returns animations directory </summary>
        public static string Animations
        {
            get
            {
                return System.IO.Path.Combine(Graphics, "Animations");
            }
        }

        /// <summary> Returns skills directory </summary>
        public static string Skills
        {
            get
            {
                return System.IO.Path.Combine(Graphics, "Skills");
            }
        }

        /// <summary> Returns projectiles directory </summary>
        public static string Projectiles
        {
            get
            {
                return System.IO.Path.Combine(Graphics, "Projectiles");
            }
        }

        /// <summary> Returns resources directory </summary>
        public static string Resources
        {
            get
            {
                return System.IO.Path.Combine(Graphics, "Resources");
            }
        }

        /// <summary> Returns misc directory </summary>
        public static string Misc
        {
            get
            {
                return System.IO.Path.Combine(Graphics, "Misc");
            }
        }

        // Helper function to check if a file path has an extension
        public static string EnsureFileExtension(string path, string defaultExtension = ".png")
        {
            // Check if the path has an extension
            if (string.IsNullOrWhiteSpace(System.IO.Path.GetExtension(path)))
            {
                // If not, add the default extension
                return path + defaultExtension;
            }

            // Return the original path if it already has an extension
            return path;
        }

    }
}