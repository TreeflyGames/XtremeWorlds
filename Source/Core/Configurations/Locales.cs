  using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using System.ComponentModel; // For DefaultValue attribute (optional, more for schema)
using System.Diagnostics; // For Debug.WriteLine

// Assuming Core.Path.Config exists and provides the configuration directory path.
// For example:
namespace Core
{
    public static class Path // Mockup for Core.Path
    {
        public static string Config { get; } = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config");
    }
}

namespace Core.Localization
{
    // Represents a single localizable string item for XML serialization
    public class LocaleItem
    {
        [XmlAttribute("Key")]
        public string Key { get; set; }

        [XmlAttribute("Value")]
        public string Value { get; set; }

        // Parameterless constructor for XML serialization
        public LocaleItem() { }

        public LocaleItem(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }

    // Represents all localizable strings for a given language, structured for XML.
    // This class is primarily for serialization/deserialization.
    [XmlRoot("Locales")]
    public class LocaleData
    {
        // These lists are public for XmlSerializer to work.
        // They will be used to populate a dictionary for fast lookups.
        [XmlElement("Load")]
        public List<LocaleItem> Load { get; set; } = new List<LocaleItem>();

        [XmlElement("MainMenu")]
        public List<LocaleItem> MainMenu { get; set; } = new List<LocaleItem>();

        [XmlElement("Game")]
        public List<LocaleItem> Game { get; set; } = new List<LocaleItem>();

        [XmlElement("Chat")]
        public List<LocaleItem> Chat { get; set; } = new List<LocaleItem>();

        [XmlElement("ItemDescription")]
        public List<LocaleItem> ItemDescription { get; set; } = new List<LocaleItem>();

        [XmlElement("SkillDescription")]
        public List<LocaleItem> SkillDescription { get; set; } = new List<LocaleItem>();

        [XmlElement("Crafting")]
        public List<LocaleItem> Crafting { get; set; } = new List<LocaleItem>();

        [XmlElement("Trade")]
        public List<LocaleItem> Trade { get; set; } = new List<LocaleItem>();

        [XmlElement("Events")]
        public List<LocaleItem> Events { get; set; } = new List<LocaleItem>();

        [XmlElement("Quest")]
        public List<LocaleItem> Quest { get; set; } = new List<LocaleItem>();

        [XmlElement("Character")]
        public List<LocaleItem> Character { get; set; } = new List<LocaleItem>();

        /// <summary>
        /// Aggregates all LocaleItem lists into a single list.
        /// This is useful for populating the runtime dictionary or iterating all items.
        /// </summary>
        [XmlIgnore] // Don't serialize this helper
        public IEnumerable<LocaleItem> AllItems
        {
            get
            {
                // Use reflection to get all List<LocaleItem> properties
                // Or explicitly chain them:
                return Load.Concat(MainMenu)
                           .Concat(Game)
                           .Concat(Chat)
                           .Concat(ItemDescription)
                           .Concat(SkillDescription)
                           .Concat(Crafting)
                           .Concat(Trade)
                           .Concat(Events)
                           .Concat(Quest)
                           .Concat(Character);
            }
        }

        /// <summary>
        /// Creates a LocaleData instance with default English values.
        /// This is used if no XML file is found.
        /// </summary>
        public static LocaleData CreateDefaultEnglish()
        {
            return new LocaleData
            {
                Load = new List<LocaleItem>
                {
                    new LocaleItem("Loading", "Loading..."),
                    new LocaleItem("Graphics", "Initializing Graphics.."),
                    new LocaleItem("Network", "Initializing Network..."),
                    new LocaleItem("Starting", "Starting Game...")
                },
                MainMenu = new List<LocaleItem>
                {
                    new LocaleItem("ServerStatus", "Server Status:"),
                    new LocaleItem("ServerOnline", "Online"),
                    // ... (add all other default MainMenu items)
                    new LocaleItem("ConnectToServer", "Connecting to Server...( {0} )")
                },
                Game = new List<LocaleItem>
                {
                    new LocaleItem("Time", "Time: "),
                    // ... (add all other default Game items)
                    new LocaleItem("AccessDenied", "Access Denied!")
                },
                Chat = new List<LocaleItem>
                {
                    new LocaleItem("Emote", "Usage : /emote [1-11]"),
                    // ... (add all other default Chat items)
                    new LocaleItem("WarpTo", "Usage : /warpto [map index]")
                },
                ItemDescription = new List<LocaleItem>
                {
                    new LocaleItem("NotAvailable", "Not Available"),
                    // ... (add all other default ItemDescription items)
                    new LocaleItem("Defense", "Defense : ")
                },
                SkillDescription = new List<LocaleItem>
                {
                    new LocaleItem("No", "No"),
                    // ... (add all other default SkillDescription items)
                    new LocaleItem("LoseMp", "Syphon MP")
                },
                Crafting = new List<LocaleItem>
                {
                    new LocaleItem("NotEnough", "Not enough materials!"),
                    new LocaleItem("NotSelected", "Nothing selected")
                },
                Trade = new List<LocaleItem>
                {
                    new LocaleItem("Request", "{0} is requesting to trade."),
                    new LocaleItem("Timeout", "You took too long to decide. Please try again."),
                    new LocaleItem("Value", "Total Value : {0}g"),
                    new LocaleItem("StatusOther", "Other player confirmed offer."),
                    new LocaleItem("StatusSelf", "You confirmed the offer.")
                },
                Events = new List<LocaleItem>
                {
                    new LocaleItem("OptContinue", "- Continue -")
                },
                Quest = new List<LocaleItem>
                {
                    new LocaleItem("Cancel", "Cancel Started"),
                    // ... (add all other default Quest items)
                    new LocaleItem("Fetch", "Fetch {0} X {1} from {2}.")
                },
                Character = new List<LocaleItem>
                {
                    new LocaleItem("PName", "Name: "),
                    // ... (add all other default Character items)
                    new LocaleItem("SkillLabel", "Skills:")
                }
                // ... Initialize all other lists similarly
            };
        }
    }

    public static class LocalesManager
    {
        private static Dictionary<string, string> _localizedStrings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        private static LocaleData _loadedLocaleData; // Keep the raw data if needed for saving or inspection

        public static string DefaultLanguageCode { get; set; } = "en"; // e.g., "en", "de", "fr"
        public static string CurrentLanguageCode { get; private set; }

        // Placeholder for a missing key. Could be configurable.
        public const string MissingKeyFormat = "[MISSING_LOC_KEY: {0}]";

        public static void Initialize(string languageCode = null)
        {
            CurrentLanguageCode = languageCode ?? DefaultLanguageCode;
            string languageFile = GetLanguageFilePath(CurrentLanguageCode);

            LocaleData dataToLoad;

            if (File.Exists(languageFile))
            {
                try
                {
                    var serializer = new XmlSerializer(typeof(LocaleData));
                    using (var reader = new StreamReader(languageFile))
                    {
                        dataToLoad = (LocaleData)serializer.Deserialize(reader);
                    }
                    Debug.WriteLine($"Localization file loaded: {languageFile}");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error loading localization file '{languageFile}': {ex.Message}. Falling back to defaults.");
                    dataToLoad = LocaleData.CreateDefaultEnglish(); // Fallback to hardcoded defaults
                    // Optionally, try to save the default file if loading failed and it was supposed to exist
                    // Save(dataToLoad, languageFile); // Be careful with this to not overwrite a corrupted user file without backup
                }
            }
            else
            {
                Debug.WriteLine($"Localization file not found: {languageFile}. Creating and using default English localization.");
                dataToLoad = LocaleData.CreateDefaultEnglish();
                Save(dataToLoad, languageFile); // Save the default file
            }

            _loadedLocaleData = dataToLoad;
            PopulateDictionary(dataToLoad);
        }

        private static string GetLanguageFilePath(string langCode)
        {
            // Example: Locales_en.xml, Locales_de.xml
            string fileName = $"Locales_{langCode}.xml";
            return System.IO.Path.Combine(Core.Path.Config, fileName);
        }

        private static void PopulateDictionary(LocaleData localeData)
        {
            _localizedStrings.Clear();
            foreach (var item in localeData.AllItems)
            {
                if (!_localizedStrings.TryAdd(item.Key, item.Value))
                {
                    Debug.WriteLine($"Warning: Duplicate localization key '{item.Key}' found. Value '{_localizedStrings[item.Key]}' will be used. New value '{item.Value}' ignored.");
                }
            }
        }

        public static void SaveCurrentLanguageFile()
        {
            if (_loadedLocaleData != null && !string.IsNullOrEmpty(CurrentLanguageCode))
            {
                Save(_loadedLocaleData, GetLanguageFilePath(CurrentLanguageCode));
            }
            else
            {
                Debug.WriteLine("Cannot save language file: No locale data loaded or current language code is not set.");
            }
        }

        private static void Save(LocaleData data, string filePath)
        {
            try
            {
                Directory.CreateDirectory(System.IO.Path.GetDirectoryName(filePath)); // Ensure directory exists
                var serializer = new XmlSerializer(typeof(LocaleData));
                using (var writer = new StreamWriter(filePath))
                {
                    serializer.Serialize(writer, data);
                }
                Debug.WriteLine($"Localization file saved: {filePath}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving localization file '{filePath}': {ex.Message}");
            }
        }

        /// <summary>
        /// Gets the localized string for the given key.
        /// </summary>
        /// <param name="key">The localization key.</param>
        /// <returns>The localized string, or a placeholder if the key is not found.</returns>
        public static string Get(string key)
        {
            if (_localizedStrings.TryGetValue(key, out var value))
            {
                return value;
            }
            Debug.WriteLine($"Missing localization key: {key}");
            return string.Format(MissingKeyFormat, key); // Return a placeholder
        }

        /// <summary>
        /// Gets the localized string for the given key and formats it with the provided arguments.
        /// </summary>
        /// <param name="key">The localization key.</param>
        /// <param name="args">Arguments for string formatting.</param>
        /// <returns>The formatted localized string, or a placeholder if the key is not found.</returns>
        public static string Get(string key, params object[] args)
        {
            string formatString = Get(key);
            // Check if the formatString is actually the missing key placeholder itself
            if (formatString == string.Format(MissingKeyFormat, key))
            {
                return formatString; // Don't try to format the placeholder
            }

            try
            {
                return string.Format(formatString, args);
            }
            catch (FormatException ex)
            {
                Debug.WriteLine($"Error formatting localization key '{key}' with value '{formatString}': {ex.Message}");
                return formatString; // Return the unformatted string in case of format error
            }
        }

        /// <summary>
        /// (Optional) Allows changing the language at runtime.
        /// </summary>
        /// <param name="languageCode">The new language code (e.g., "fr", "de").</param>
        public static void ChangeLanguage(string languageCode)
        {
            Initialize(languageCode);
            // Here you might want to trigger an event for UI elements to refresh their text.
            // OnLanguageChanged?.Invoke(this, EventArgs.Empty);
        }
        // public static event EventHandler OnLanguageChanged;
    }
}
