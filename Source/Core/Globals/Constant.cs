using System;
using System.IO; // For Path combining if needed
using System.Text.RegularExpressions; // For name validation example

namespace Core
{
    /// <summary>
    /// Provides a central repository for compile-time constants and default values used throughout the game engine and systems.
    /// Constants here define limits, default behaviors, configurations, and fixed identifiers.
    /// </summary>
    public static class Constant // Made class static as it only contains static members/constants
    {
        // Corrected Typo
        /// <summary>Maximum number of global variables for event scripting.</summary>
        public const int MAX_VARIABLES = 100; // Was NAX_VARIABLES

        /// <summary>
        /// Contains constants defining maximum limits for various game elements (array sizes, counts, etc.).
        /// </summary>
        public static class Limits
        {
            // --- Original Limits (with documentation) ---
            /// <summary>Maximum number of distinct animations loadable.</summary>
            public const int MAX_ANIMATIONS = 100;
            /// <summary>Maximum number of items a player can store in their bank.</summary>
            public const byte MAX_BANK_SLOTS = 90; // Renamed for clarity
            /// <summary>Maximum number of player classes/jobs defined.</summary>
            public const byte MAX_JOBS = 3; // Low number, might need increase? Example value.
            /// <summary>Maximum value for morality alignment scale (or number of distinct moral states?). Needs context.</summary>
            public const byte MAX_MORALS = 50;
            /// <summary>Maximum number of 'Cots' (Containers? Checkpoints? Needs context). Assuming Containers/Chests.</summary>
            public const int MAX_CONTAINERS = 30; // Renamed MAX_COTS for clarity
            /// <summary>Maximum number of items a player can carry in their inventory.</summary>
            public const byte MAX_INVENTORY_SLOTS = 35; // Renamed for clarity
            /// <summary>Maximum number of distinct item definitions.</summary>
            public const int MAX_ITEMS = 500;
            /// <summary>Maximum level a player or NPC can achieve.</summary>
            public const byte MAX_LEVEL = 99;
            /// <summary>Maximum number of map definitions.</summary>
            public const int MAX_MAPS = 1000;
            /// <summary>Maximum number of item instances allowed on a single map simultaneously.</summary>
            public const byte MAX_MAP_ITEMS = 255;
            /// <summary>Maximum number of NPC instances allowed on a single map simultaneously.</summary>
            public const byte MAX_MAP_NPCS = 30;
            /// <summary>Maximum number of distinct NPC definitions.</summary>
            public const int MAX_NPCS = 500;
            /// <summary>Maximum number of skills an NPC can possess.</summary>
            public const byte MAX_NPC_SKILLS = 6;
            /// <summary>Maximum number of skills a Pet can possess.</summary>
            public const byte MAX_PET_SKILLS = 4;
            /// <summary>Maximum number of Parties/Groups that can exist simultaneously.</summary>
            public const int MAX_PARTIES = 100;
            /// <summary>Maximum number of members allowed in a single party.</summary>
            public const int MAX_PARTY_MEMBERS = 4;
            /// <summary>Maximum number of distinct Pet definitions or instances allowed globally?</summary>
            public const int MAX_PETS = 100;
            /// <summary>Maximum number of concurrent players allowed on the server.</summary>
            public const int MAX_PLAYERS = 500;
            /// <summary>Maximum number of skills a Player can learn/possess.</summary>
            public const byte MAX_PLAYER_SKILLS = 35;
            /// <summary>Maximum number of distinct gatherable resource node types or definitions.</summary>
            public const int MAX_RESOURCES = 100;
            /// <summary>Maximum number of distinct shop definitions.</summary>
            public const int MAX_SHOPS = 100;
            /// <summary>Maximum number of distinct skill definitions.</summary>
            public const int MAX_SKILLS = 255;
            /// <summary>Maximum number of items allowed in a single trade window session.</summary>
            public const byte MAX_TRADE_SLOTS = 35; // Renamed for clarity
            /// <summary>Maximum number of characters allowed for player/NPC names.</summary>
            public const byte MAX_NAME_LENGTH = 20; // Adjusted from 21 (often exclusive)
             /// <summary>Minimum number of characters required for player/NPC names.</summary>
            public const byte MIN_NAME_LENGTH = 3;
            /// <summary>Maximum number of characters allowed in a single chat message.</summary>
            public const byte MAX_CHAT_LENGTH = 70;
            /// <summary>Maximum number of slots on a player's hotbar.</summary>
            public const byte MAX_HOTBAR_SLOTS = 10; // Renamed for clarity
            /// <summary>Maximum horizontal size of a map grid (tile count - 1).</summary>
            public const byte MAX_MAP_X = 32; // Renamed for clarity
            /// <summary>Maximum vertical size of a map grid (tile count - 1).</summary>
            public const byte MAX_MAP_Y = 24; // Renamed for clarity
            /// <summary>Maximum number of active projectiles allowed simultaneously.</summary>
            public const int MAX_PROJECTILES = 255;
            /// <summary>Maximum number of distinct item types that can drop from a single source (e.g., monster death).</summary>
            public const byte MAX_DROP_ITEM_TYPES = 5; // Renamed for clarity
            /// <summary>Maximum number of items given to a player character upon creation.</summary>
            public const byte MAX_START_ITEMS = 5;
            /// <summary>Maximum number of global switches for event scripting.</summary>
            public const int MAX_SWITCHES = 100;
            /// <summary>Maximum number of 'Points' (Map waypoints? Skill points? Needs context).</summary>
            public const byte MAX_POINTS = 255;
            /// <summary>Maximum number of characters (player slots) allowed per account.</summary>
            public const byte MAX_CHARS_PER_ACCOUNT = 3; // Renamed for clarity
            /// <summary>Maximum number of lines stored in the chat history buffer.</summary>
            public const int MAX_CHAT_HISTORY = 1000; // Renamed for clarity
            /// <summary>Maximum number of player/NPC stats defined (e.g., STR, DEX, INT...).</summary>
            public const byte MAX_STATS = 8; // 255 seems high for core stats, adjusted as example. Use Enum count if possible.
            /// <summary>Maximum number of quest definitions.</summary>
            public const byte MAX_QUESTS = 100;
            /// <summary>Maximum number of active quests a player can have.</summary>
            public const byte MAX_ACTIVE_QUESTS = 20;
            /// <summary>Maximum number of distinct event definitions.</summary>
            public const int MAX_EVENTS = 500;
            /// <summary>Maximum number of player guilds that can exist.</summary>
            public const byte MAX_GUILDS = 100;
            /// <summary>Maximum number of members allowed in a guild.</summary>
            public const short MAX_GUILD_MEMBERS = 100; // Use short for potentially larger counts

            // --- New Limit Constants ---
            /// <summary>Maximum number of Buff/Debuff instances active on a single entity.</summary>
            public const byte MAX_ENTITY_AFFECTS = 32; // Buffs/Debuffs
            /// <summary>Maximum number of distinct Status Effect definitions (buffs/debuffs).</summary>
            public const int MAX_AFFECT_DEFINITIONS = 200;
            /// <summary>Maximum number of spells/abilities defined.</summary>
            public const int MAX_SPELLS = 255; // Or map to MAX_SKILLS? Depends on system.
            /// <summary>Maximum number of factions/reputations defined.</summary>
            public const byte MAX_FACTIONS = 32;
             /// <summary>Maximum number of items visible in a loot window.</summary>
            public const byte MAX_LOOT_WINDOW_ITEMS = 15;
            /// <summary>Maximum stack size for stackable items.</summary>
            public const short MAX_ITEM_STACK = 999;
            /// <summary>Maximum number of map layers (e.g., ground, objects, overhead).</summary>
            public const byte MAX_MAP_LAYERS = 5;
            /// <summary>Maximum number of tilesets.</summary>
            public const short MAX_TILESETS = 256;
             /// <summary>Maximum number of equipment slots on an entity (Head, Chest, etc.).</summary>
            public const byte MAX_EQUIPMENT_SLOTS = 12; // Example
             /// <summary>Maximum simultaneous network connections (might differ from MAX_PLAYERS).</summary>
            public const int MAX_CONNECTIONS = MAX_PLAYERS + 50; // Example buffer
        }

        /// <summary>
        /// Contains constants defining default values for gameplay mechanics, stats, and behaviors.
        /// </summary>
        public static class Defaults
        {
            // --- Gameplay Defaults ---
            public const int STARTING_HEALTH = 100;
            public const int STARTING_MANA = 50; // Or energy, stamina, etc.
            public const float BASE_MOVE_SPEED = 4.5f; // Units per second or similar
            public const int DEFAULT_RESPAWN_SECONDS = 10;
            public const int STARTING_GOLD = 50;
            public const byte STARTING_LEVEL = 1;
            public const float BASE_ATTACK_SPEED = 1.0f; // Attacks per second
            public const float BASE_HIT_CHANCE = 0.90f; // 90%
            public const float BASE_CRITICAL_CHANCE = 0.05f; // 5%
            public const float CRITICAL_DAMAGE_MULTIPLIER = 1.5f; // 150% damage
            public const float XP_LOSS_ON_DEATH_PERCENT = 0.10f; // 10% XP loss
            public const int MAX_STAT_VALUE = 99; // Max value for STR, DEX etc. if capped below MAX_LEVEL
            public const int BASE_SIGHT_RANGE = 10; // Tiles
             public const int BASE_HEALTH_REGEN_PER_TICK = 1; // Per game tick or second
             public const int BASE_MANA_REGEN_PER_TICK = 2;

            // --- AI Defaults ---
            public const float AI_DEFAULT_AGGRO_RADIUS = 8.0f; // Tiles or units
            public const float AI_DEFAULT_FLEE_HEALTH_PERCENT = 0.20f; // Flee below 20% HP
            public const float AI_DEFAULT_WANDER_RADIUS = 5.0f;
            public const float AI_PATHFINDING_RECALC_INTERVAL_S = 0.5f; // Seconds

            // --- UI Defaults ---
            public const string DEFAULT_FONT_NAME = "Arial"; // Consider using resource IDs
            public const int DEFAULT_FONT_SIZE = 12;
            public const string DEFAULT_UI_SKIN = "DefaultSkin";
            public const float DEFAULT_FADE_TIME_SECONDS = 0.25f;

             // --- World Defaults ---
             public const int DEFAULT_MAP_ID = 1;
             public const byte DEFAULT_MAP_X = 10;
             public const byte DEFAULT_MAP_Y = 10;
             public const int TICKS_PER_SECOND = 30; // Game loop ticks
        }

        /// <summary>
        /// Contains constants related to network communication.
        /// </summary>
        public static class Network
        {
            public const string DEFAULT_SERVER_IP = "127.0.0.1";
            public const ushort DEFAULT_SERVER_PORT = 5555;
            public const int DEFAULT_TIMEOUT_MS = 5000; // 5 seconds
            public const int MAX_PACKET_SIZE_BYTES = 1400; // Typical MTU safe size
            public const int NETWORK_BUFFER_SIZE_BYTES = 8192; // 8 KB
            public const short PROTOCOL_VERSION = 1; // Increment when breaking changes occur
            public const byte MAX_LOGIN_ATTEMPTS = 5;
        }

        /// <summary>
        /// Contains constants related to time values (intervals, durations).
        /// Uses static readonly for non-primitive types like TimeSpan.
        /// </summary>
        public static class Time
        {
            /// <summary>Interval at which player input is processed (milliseconds).</summary>
            public const int PLAYER_INPUT_INTERVAL_MS = 50; // 20 times per second
            /// <summary>Duration of a single server game logic tick (milliseconds).</summary>
            public const int SERVER_TICK_INTERVAL_MS = 100; // 10 times per second (example)
             /// <summary>Duration of a single server game logic tick (TimeSpan).</summary>
            public static readonly TimeSpan SERVER_TICK_TIMESPAN = TimeSpan.FromMilliseconds(SERVER_TICK_INTERVAL_MS);
            /// <summary>Default duration for one frame of animation (seconds).</summary>
            public const float DEFAULT_ANIMATION_FRAME_S = 0.1f;
            /// <summary>Interval for automatically saving player data (minutes).</summary>
            public const int AUTOSAVE_INTERVAL_MINUTES = 15;
            /// <summary>How long items stay on the ground before despawning (seconds).</summary>
            public const int MAP_ITEM_DESPAWN_SECONDS = 300; // 5 minutes
            /// <summary>How long buffs/debuffs with default duration last (seconds).</summary>
            public const int DEFAULT_AFFECT_DURATION_SECONDS = 60;
            /// <summary>Network ping interval (seconds).</summary>
            public const int PING_INTERVAL_SECONDS = 10;
        }


        /// <summary>
        /// Contains constants related to file system paths and names.
        /// </summary>
        public static class Files
        {
            public const string DATA_FOLDER = "Data";
            public const string SAVE_FOLDER = "Saves";
            public const string LOG_FOLDER = "Logs";
            public const string CONFIG_FILE = "config.ini"; // Or .json, .xml
            public const string LOG_FILE = "game.log";
            public const string PLAYER_FILE_EXT = ".player";
            public const string MAP_FILE_EXT = ".map";
            public const string ITEM_DATA_FILE = "items.dat"; // Example data file names
            public const string NPC_DATA_FILE = "npcs.dat";
            public const string SKILL_DATA_FILE = "skills.dat";

            // Example of using static readonly with Path.Combine for platform independence
            public static readonly string BaseDataPath = Path.Combine(AppContext.BaseDirectory, DATA_FOLDER);
        }

         /// <summary>
        /// Contains constants related to UI element identifiers or fixed text.
        /// </summary>
        public static class UI
        {
             public const string MAIN_MENU_ID = "MainMenu";
             public const string INVENTORY_WINDOW_ID = "InventoryWindow";
             public const string CHAT_WINDOW_ID = "ChatWindow";
             public const string CONFIRMATION_TITLE = "Confirm";
             public const string ERROR_TITLE = "Error";
             public const string INFO_TITLE = "Information";
             // Add IDs for buttons, panels, etc. if using string-based lookup
        }

        // --- Static Helper Functions ---

        /// <summary>
        /// Checks if the given name is valid according to length limits.
        /// Optionally checks for invalid characters.
        /// </summary>
        /// <param name="name">The name to validate.</param>
        /// <param name="checkCharacters">If true, checks for non-alphanumeric characters (basic example).</param>
        /// <returns>True if the name is valid, false otherwise.</returns>
        public static bool IsValidName(string? name, bool checkCharacters = true)
        {
            if (string.IsNullOrWhiteSpace(name)) return false;

            if (name.Length < Limits.MIN_NAME_LENGTH || name.Length > Limits.MAX_NAME_LENGTH)
            {
                return false;
            }

            if (checkCharacters)
            {
                // Basic check: only allow letters and numbers. Adjust regex as needed.
                // Consider allowing spaces, hyphens, etc. based on game rules.
                 if (!Regex.IsMatch(name, @"^[a-zA-Z0-9]+$"))
                 {
                     // Example: Allow spaces but not at start/end
                     // if (!Regex.IsMatch(name, @"^[a-zA-Z0-9][a-zA-Z0-9 ]*[a-zA-Z0-9]$"))
                     // return false;
                 }
            }

            return true;
        }

        /// <summary>
        /// Checks if the given map coordinates are within the valid map boundaries.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        /// <returns>True if coordinates are valid, false otherwise.</returns>
        public static bool IsValidMapCoord(int x, int y)
        {
            return x >= 0 && x < Limits.MAX_MAP_X && y >= 0 && y < Limits.MAX_MAP_Y;
        }

        /// <summary>
        /// Calculates the total number of tiles on a standard map.
        /// </summary>
        /// <returns>The total tile count.</returns>
        public static int GetMapTileCount()
        {
             // Assuming MAX_MAP_X and Y are max indices (0 to MAX-1)
             // If they represent SIZE, it should be MAX_MAP_X * MAX_MAP_Y
            return (Limits.MAX_MAP_X) * (Limits.MAX_MAP_Y); // Adjust if MAX means size instead of index
        }

        /// <summary>
        /// Clamps a value between a minimum and a maximum.
        /// </summary>
        /// <typeparam name="T">Type of the value (must be comparable).</typeparam>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The minimum allowed value.</param>
        /// <param name="max">The maximum allowed value.</param>
        /// <returns>The clamped value.</returns>
        public static T Clamp<T>(T value, T min, T max) where T : IComparable<T>
        {
            if (value.CompareTo(min) < 0) return min;
            if (value.CompareTo(max) > 0) return max;
            return value;
        }
    }
}
