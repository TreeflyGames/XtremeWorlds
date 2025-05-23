  using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Diagnostics;

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
                    new LocaleItem("ServerReconnect", "Reconnecting..."),
                    new LocaleItem("ServerOffline", "Offline"),
                    new LocaleItem("ButtonPlay", "Play"),
                    new LocaleItem("ButtonRegister", "Register"),
                    new LocaleItem("ButtonCredits", "Credits"),
                    new LocaleItem("ButtonExit", "Exit"),
                    new LocaleItem("NewsHeader", "Latest News"),
                    new LocaleItem("News", @"Welcome To the XtremeWorlds.
                                           This is a free open-source C# game engine!
                                           For help or support please visit our site at
                                           https://xtremeworlds.com/."),
                    new LocaleItem("Login", "Login"),
                    new LocaleItem("LoginName", "Name: "),
                    new LocaleItem("LoginPass", "Password: "),
                    new LocaleItem("LoginCheckBox", "Save Password?"),
                    new LocaleItem("LoginButton", "Submit"),
                    new LocaleItem("NewCharacter", "Create Character"),
                    new LocaleItem("NewCharacterName", "Name: "),
                    new LocaleItem("NewCharacterClass", "Class: "),
                    new LocaleItem("NewCharacterGender", "Gender: "),
                    new LocaleItem("NewCharacterMale", "Male"),
                    new LocaleItem("NewCharacterFemale", "Female"),
                    new LocaleItem("NewCharacterSprite", "Sprite"),
                    new LocaleItem("NewCharacterButton", "Submit"),
                    new LocaleItem("UseCharacter", "Character Selection"),
                    new LocaleItem("UseCharacterNew", "New Character"),
                    new LocaleItem("UseCharacterUse", "Use Character"),
                    new LocaleItem("UseCharacterDel", "Delete Character"),
                    new LocaleItem("Register", "Registration"),
                    new LocaleItem("RegisterName", "Username: "),
                    new LocaleItem("RegisterPass1", "Password: "),
                    new LocaleItem("RegisterPass2", "Retype Password: "),
                    new LocaleItem("Credits", "Credits"),
                    new LocaleItem("StringLegal", "You cannot use high ASCII characters In your name, please re-enter."),
                    new LocaleItem("SendLogin", "Connected, sending login information..."),
                    new LocaleItem("SendNewCharacter", "Connected, sending character data..."),
                    new LocaleItem("SendRegister", "Connected, sending registration information..."),
                    new LocaleItem("ConnectToServer", "Connecting to Server...( {0} )")
                },
                Game = new List<LocaleItem>
                {
                    new LocaleItem("Time", "Time: "),
                    new LocaleItem("Fps", "Fps: "),
                    new LocaleItem("Lps", "Lps: "),
                    new LocaleItem("Ping", "Ping: "),
                    new LocaleItem("PingSync", "Sync"),
                    new LocaleItem("PingLocal", "Local"),
                    new LocaleItem("MapReceive", "Receiving map..."),
                    new LocaleItem("DataReceive", "Receiving game data..."),
                    new LocaleItem("MapCurMap", "Map # {0}"),
                    new LocaleItem("MapCurLoc", "Loc() x: {0} y: {1}"),
                    new LocaleItem("MapLoc", "Cur Loc x: {0} y: {1}"),
                    new LocaleItem("Fullscreen", "Please restart the client for the changes to take effect."),
                    new LocaleItem("InvalidMap", "Invalid map index."),
                    new LocaleItem("AccessDenied", "Access Denied!")
                },
                Chat = new List<LocaleItem>
                {
                    new LocaleItem("Emote", "Usage : /emote [1-11]"),
                    new LocaleItem("Info", "Usage : /info [player]"),
                    new LocaleItem("Party", "Usage : /party [player]"),
                    new LocaleItem("Trade", "Usage : /trade [player]"),
                    new LocaleItem("PlayerMsg", "Usage : ![player] [message]"),
                    new LocaleItem("InvalidCmd", "Not a valid command!"),
                    new LocaleItem("Help1", "Social Commands : "),
                    new LocaleItem("Help2", "'[message] = Global Message"),
                    new LocaleItem("Help3", "-[message] = Party Message"),
                    new LocaleItem("Help4", "![player] [message] = Player Message"),
                    new LocaleItem("Help5", "@[message] = Admin Message"),
                    new LocaleItem("Help6", @"Available Commands: /help, /info, 
                                            /fps, /lps, /stats, /trade, 
                                            /party, /join, /leave"),
                    new LocaleItem("AdminGblMsg", "''msghere = Global Admin Message"),
                    new LocaleItem("AdminPvtMsg", "= msghere = Private Admin Message"),
                    new LocaleItem("Admin1", "Social Commands:"),
                    new LocaleItem("Admin2", @"Available Commands: /admin, /who, /access, /loc, 
                                            /warpmeto, /warptome, /warpto, 
                                            /sprite, /mapreport, /kick, 
                                            /ban, /respawn, /welcome,
                                            /editmap, /edititem, /editresource,
                                            /editskill, /editpet, /editshop
                                            /editprojectile, /editnpc, /editjob
                                            /editjob, /acp"),
                    new LocaleItem("Welcome", "Usage : /welcome [message]"),
                    new LocaleItem("Access", "Usage : /access [player] [access]"),
                    new LocaleItem("Sprite", "Usage : /sprite [index]"),
                    new LocaleItem("Kick", "Usage : /kick [player]"),
                    new LocaleItem("Ban", "Usage : /ban [player]"),
                    new LocaleItem("WarpMeTo", "Usage : /warpmeto [player]"),
                    new LocaleItem("WarpToMe", "Usage : /warptome [player]"),
                    new LocaleItem("WarpTo", "Usage : /warpto [map index]")
                },
                ItemDescription = new List<LocaleItem>
                {
                    new LocaleItem("NotAvailable", "Not Available"),
                    new LocaleItem("None", "None"),
                    new LocaleItem("Seconds", "Seconds"),
                    new LocaleItem("Currency", "Currency"),
                    new LocaleItem("CommonEvent", "Event"),
                    new LocaleItem("Potion", "Potion"),
                    new LocaleItem("Skill", "Skill"),
                    new LocaleItem("Weapon", "Weapon"),
                    new LocaleItem("Armor", "Armor"),
                    new LocaleItem("Helmet", "Helmet"),
                    new LocaleItem("Shield", "Shield"),
                    new LocaleItem("Shoes", "Shoes"),
                    new LocaleItem("Gloves", "Gloves"),
                    new LocaleItem("Amount", "Amount : "),
                    new LocaleItem("Restore", "Restore Amount : "),
                    new LocaleItem("Damage", "Damage : "),
                    new LocaleItem("Defense", "Defense : ")
                },
                SkillDescription = new List<LocaleItem>
                {
                    new LocaleItem("No", "No"),
                    new LocaleItem("None", "None"),
                    new LocaleItem("Warp", "Warp"),
                    new LocaleItem("Tiles", "Tiles"),
                    new LocaleItem("SelfCast", "Self-Cast"),
                    new LocaleItem("Gain", "Regen : "),
                    new LocaleItem("GainHp", "Regen HP"),
                    new LocaleItem("GainMp", "Regen MP"),
                    new LocaleItem("Lose", "Syphon : "),
                    new LocaleItem("LoseHp", "Syphon HP"),
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
                    new LocaleItem("Started", "Quest Started"),
                    new LocaleItem("Completed", "Quest Completed"),
                    new LocaleItem("Slay", "Defeat {0}/{1} {2}."),
                    new LocaleItem("Collect", "Collect {0}/{1} {2}."),
                    new LocaleItem("Talk", "Go talk To {0}."),
                    new LocaleItem("Reach", "Go To {0}."),
                    new LocaleItem("TurnIn", "Give {0} the {1} {2}/{3} they requested."),
                    new LocaleItem("Kill", "Defeat {0}/{1} Players In Battle."),
                    new LocaleItem("Gather", "Gather {0}/{1} {2}."),
                    new LocaleItem("Fetch", "Fetch {0} X {1} from {2}.")
                },
                Character = new List<LocaleItem>
                {
                    new LocaleItem("PName", "Name: "),
                    new LocaleItem("JobType", "Job: "),
                    new LocaleItem("Level", "Lv: "),
                    new LocaleItem("Exp", "Exp: "),
                    new LocaleItem("StatsLabel", "Stats:"),
                    new LocaleItem("Strength", "Strength: "),
                    new LocaleItem("Endurance", "Endurance: "),
                    new LocaleItem("Vitality", "Vitality: "),
                    new LocaleItem("Intelligence", "Intelligence: "),
                    new LocaleItem("Luck", "Luck: "),
                    new LocaleItem("Spirit", "Spirit: "),
                    new LocaleItem("Points", "Points Available: "),
                    new LocaleItem("SkillLabel", "Skills:")
                }
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
        /// Gets the localized string for the given key from the currently loaded LocaleData.
        /// </summary>
        /// <param name="key">The localization key.</param>
        /// <returns>The localized string, or a placeholder if the key is not found.</returns>
        public static string Get(string key)
        {
            if (_loadedLocaleData == null)
            {
                Debug.WriteLine("LocaleData is null.");
                return string.Format(MissingKeyFormat, key);
            }

            var value = _localizedStrings.TryGetValue(key, out var result) ? result : null;
            if (value != null)
            {
                return value;
            }
            Debug.WriteLine($"Missing localization key: {key}");
            return string.Format(MissingKeyFormat, key);
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
