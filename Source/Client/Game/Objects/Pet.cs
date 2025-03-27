using Core;
using Microsoft.VisualBasic.CompilerServices; // CONSIDER REMOVING: Only needed if using VB-specific functions. Evaluate if it's truly necessary.
using Mirage.Sharp.Asfw; // Assumed networking/utility library
using Microsoft.Xna.Framework; // For Color, Rectangle, Point
using Microsoft.Xna.Framework.Graphics; // For Texture2D (assuming GameClient uses it)
using System; // For Math
using System.Collections.Generic; // For Lists, Dictionary
using static Core.Global.Command; // Assuming this provides GetPlayerAccess, GetPlayerPK etc. (May need GetPlayerName, GetPlayerNameColor helpers too)
using Path = Core.Path; // Assuming Core.Path provides necessary file paths

namespace Client
{
    #region Enums (Consider moving to Core.Enum or a dedicated Enums file)

    public enum PetMovementStance
    {
        FollowClose,   // Stays very near the owner
        FollowMedium,  // Default follow distance
        FollowFar,     // Stays further back
        Stay           // Stays at the commanded position (within reason)
    }

    public enum PetCombatStance
    {
        Passive,       // Will not attack, flees if attacked (maybe)
        Defensive,     // Attacks only if owner or itself is attacked
        Aggressive,    // Attacks nearby hostiles on sight
        GuardOwner     // Prioritizes enemies attacking the owner
    }

    public enum PetTargetingPriority
    {
        Closest,
        LowestHealth,
        HighestHealth,
        HighestThreat, // If a threat system exists
        PlayersTarget, // Focuses whatever the player is targeting
        Self           // For support skills on itself
    }

    public enum PetEquipmentSlot
    {
        Collar,
        Charm,
        Accessory1,
        Accessory2,
        // Add more slots as needed (e.g., Pet Armor, Weapon?)
        Count // Keep this last for array sizing
    }

    // --- NEW --- Example enum for direct commands
    public enum PetDirectCommandType
    {
        None,
        AttackTarget,
        MoveToPoint,
        GuardTarget,
        FollowTarget,
        InteractTarget // e.g., Fetch item?
    }

    // --- NEW --- Example enum for target types
    public enum TargetType
    {
        None,
        Player,
        Npc,
        MapPoint,
        Item
    }


    #endregion

    public static class Pet // Changed to static class as all methods were static
    {
        #region Constants and Globals

        // UI Constants (Original + New)
        internal const byte PetbarTop = 2;
        internal const byte PetbarLeft = 2;
        internal const byte PetbarOffsetX = 4;
        internal const byte MaxPetbar = 10; // Increased for potential new buttons (stance, feed, etc.)
        internal const int PetHpBarWidth = 129;
        internal const int PetMpBarWidth = 129;
        internal const int PetExpBarWidth = 129;
        internal const int PetLoyaltyBarWidth = 129;
        internal const int PetHungerBarWidth = 129;

        // Skill Buffering (Original) - Consider making non-static if multiple pets could be controlled/buffered *simultaneously*,
        // but current structure implies one active pet per player.
        public static int PetSkillBuffer = -1; // Use int, -1 for none
        public static int PetSkillBufferTimer;
        public static int[] PetSkillCD = new int[Constant.MAX_PET_SKILLS]; // Keep MAX_PET_SKILLS updated

        // New System Constants
        internal const int MaxPetTalentTiers = 5; // Example
        internal const int MaxTalentsPerTier = 3; // Example
        internal const int MaxPetInventorySlots = 8; // Example size for pet's own inventory
        internal const int MaxPetStatusEffects = 10; // Max buffs/debuffs displayed/tracked client-side
        internal const int LoyaltyUpdateInterval = 60000; // How often loyalty might decay/update (example: 1 min)
        internal const int HungerUpdateInterval = 30000; // How often hunger might decay (example: 30 sec)
        internal const float MaxLoyalty = 100.0f;
        internal const float MaxHunger = 100.0f;
        internal const int PetActionCooldown = 500; // General cooldown for pet actions like feeding/interacting (client-side spam prevention)
        internal const int PetAttackAnimBaseSpeed = 1000; // Default ms for attack animation if not otherwise specified
        internal const float PetLeashRange = 20.0f; // Max distance pet can be commanded away from owner (example, in tiles)

        #endregion

        #region Static Pet Data (Shared across all instances of a pet type) - Loaded from Server

        // Assumes Core.Type.PetStruct is expanded or replaced
        // --- CONCEPTUAL EXPANSION of Core.Type.PetStruct (definition is in Core project) ---
        /*
        public struct PetStruct {
            // --- Existing Fields ---
            public int Num;                 // Pet Type ID
            public string Name;             // Default Name
            public int Sprite;              // Base sprite index
            public int Range;               // Attack/Interaction Range
            public int Level; // Base level? Or starting level? Clarify usage. -> Likely Starting Level
            public int MaxLevel;
            public int ExpGain; // Base XP gain modifier? -> Probably not needed here, calculated server-side
            public int LevelPnts;           // Stat Points gained per level
            public byte StatType; // Base stat growth type? -> Maybe link to a growth curve ID
            public byte LevelingType; // EXP curve type? -> Link to EXP table ID
            public int[] Stat = new int[(int)Core.Enum.StatType.Count]; // Base stats at Level 1
            public int[] Skill = new int[Constant.MAX_PET_SKILLS]; // Skills learned automatically at specific levels? OR Default skills available? Clarify.
            public byte Evolvable;          // Boolean (0 or 1)
            public int EvolveLevel;         // Level required to evolve
            public int EvolveNum;           // Target pet type num after *primary* evolution

            // --- NEW FIELDS ---
            public List<int> LearnableSkills; // List of ALL Skill IDs this pet type *can* potentially learn (e.g., via items, trainers, talents)
            public List<PetTalentData> AvailableTalents; // Definition of the talent tree/options for this pet type
            public List<PetEquipmentSlot> AllowedEquipmentSlots; // Which slots this pet type can use (use List for flexibility)
            public float BaseMoveSpeed;     // Base movement speed (tiles per second)
            public float BaseAttackSpeed;   // Base attack speed (attacks per second, or time between attacks in ms)
            public string Description;       // Flavor text/info
            public int FoodItemPreference;  // Item ID of preferred food for extra loyalty/hunger gain
            public int SizeCategory;        // For collision/rendering adjustments (e.g., Small, Medium, Large enum)
            public List<int> PossibleEvolutions; // Support for branching evolutions (List of target Pet Type Nums)
            public List<EvolutionRequirement> EvolutionRequirements; // More complex requirements (e.g. level, item, location, loyalty)
            // ... other static data like resistances, base abilities, AI hints, sound effect IDs etc.
        }

        // Example Evolution Requirement Struct (Conceptual)
        public struct EvolutionRequirement {
            public int TargetPetNum;
            public int RequiredLevel;
            public int RequiredItemID; // Item consumed on evolution
            public int RequiredMapID; // Must be on specific map?
            public float RequiredLoyalty;
            // ... other conditions
        }
        */

        // --- CONCEPTUAL Talent Data Structure (definition in Core project) ---
        /*
        public struct PetTalentData {
            public int ID;
            public string Name;
            public string Description;
            public int Tier; // Which tier it belongs to
            public int Column; // Visual position in the tier
            public int MaxPoints; // How many points can be invested
            public List<int> PrerequisiteTalentIDs; // IDs of talents required before learning this one
            public int RequiredPetLevel; // Min pet level to learn this talent
            // public StatModifier[] StatModifiers; // Effects of the talent (needs StatModifier struct definition)
            public int GrantsSkillID; // If the talent grants/unlocks an active or passive skill
            public int IconTextureID; // UI Icon for the talent
            // ... other effects like modifying existing skills, resistances, etc.
        }
        */

        #endregion

        #region Active Pet Data (Instance specific, often part of Player data) - Updated by Server

        // Assumes Core.Type.Player[n].Pet is expanded
        // --- CONCEPTUAL EXPANSION of Player's Pet Data (definition is in Core project) ---
        /*
         public class PlayerPetData { // Might be a struct if performance critical and copying is acceptable
            // --- Existing Fields ---
            public int Num;             // ID of the Pet Type (links to PetStruct)
            public int Health;
            public int Mana;            // Or Stamina/Energy
            public int Level;
            public int Exp;
            public int Tnl;             // To Next Level Exp
            public int[] Stat = new int[(int)Core.Enum.StatType.Count]; // Current allocated stats/values (calculated on server, sent to client)
            public int[] Skill = new int[Constant.MAX_PET_SKILLS]; // Currently learned/equipped ACTIVE skills
            public int X;               // Map X coord
            public int Y;               // Map Y coord
            public int Dir;             // Direction enum value
            public int XOffset;         // Pixel offset for smooth movement
            public int YOffset;         // Pixel offset for smooth movement
            public byte Moving;         // (byte)Core.Enum.MovementType
            public byte Steps;          // Animation frame step for walking
            public byte Alive;          // 1 = alive, 0 = dead/inactive
            public int AttackBehaviour; // DEPRECATED -> Use CombatStance
            public int Points;          // Unspent stat points
            public byte Attacking;      // 1 if currently in attack animation
            public int AttackTimer;     // Timer for attack animation progress

             // --- NEW FIELDS ---
             public int UniqueID;        // Unique instance ID for this specific pet (for trading, identification)
             public string CustomName;    // Player-given name override
             public int MaxHp;           // Calculated Max HP
             public int MaxMp;           // Calculated Max MP
             public PetMovementStance MovementStance;
             public PetCombatStance CombatStance;
             public PetTargetingPriority TargetingPriority;
             public int[] Equipment = new int[(int)PetEquipmentSlot.Count]; // Item IDs equipped in each slot (0 if empty)
             public List<ActiveStatusEffect> StatusEffects; // List of current buffs/debuffs
             public float Loyalty;       // 0-100
             public float Hunger;        // 0-100
             public Dictionary<int, int> LearnedTalents; // Key: TalentID, Value: Points invested
             public int UnspentTalentPoints;
             public int[] Inventory = new int[MaxPetInventorySlots]; // Item IDs carried by pet (0 if empty) - Simple version
             // public List<InventorySlot> PetInventory; // More complex version if items need quantity/metadata
             public int TargetIndex;     // Index of the current target (player, NPC, etc.)
             public TargetType TargetType; // Enum: TargetIsPlayer, TargetIsNPC etc.
             public int LastFedTimestamp; // Server tick when last fed (for hunger calculation)
             public int LastLoyaltyTickTimestamp; // Server tick for loyalty updates
             public int OwnerPlayerIndex; // Index of the player who owns this pet (useful if pet data isn't directly inside player data)
        }
        */

        // --- CONCEPTUAL Helper struct for active status effects (definition in Core project) ---
        /*
        public struct ActiveStatusEffect {
            public int StatusID; // Links to a definition of the status effect (stats, duration, icon, etc.)
            public int RemainingDurationTicks; // Time left in game ticks or ms
            public int SourceID; // Who applied it (player index, npc index, item id?)
            public byte Stacks; // If the effect can stack
            public int IconTextureID; // Cached from status definition for UI
            public bool IsBuff; // Is it beneficial? For UI sorting/coloring
        }
        */

        // --- NEW --- Structure for summarizing owned pets (used in Stable UI)
        public struct OwnedPetSummary
        {
            public int OwnedIndex; // Unique index *within the player's owned list* (used for summoning/releasing)
            public int UniqueID;   // The pet instance's global unique ID
            public int PetTypeNum; // The type ID of the pet (links to PetStruct)
            public string CustomName; // Player-given name
            public string DefaultName; // Type default name (fetched from static data)
            public int Level;
            public int Sprite;     // Base sprite (fetched from static data)
            public bool IsSummoned; // Is this the currently active pet?
            public bool IsDead; // Simplistic dead status
            // Add other useful summary info: HP/MaxHP?, Loyalty?, Icon?
        }
        public static List<OwnedPetSummary> OwnedPets = new List<OwnedPetSummary>(); // Cache of owned pets

        // Local state (Client only)
        private static Dictionary<int, bool> PetStaticDataRequested = new Dictionary<int, bool>(); // Track requests to prevent spamming
        private static int lastPetActionTime = 0; // Client-side timer for spam prevention (feeding etc.)

        #endregion

        #region Database / Data Management (Modified)

        // Clears the static data cache for a specific pet type index
        public static void ClearPetStaticData(int petTypeIndex)
        {
            if (petTypeIndex < 0 || petTypeIndex >= Core.Type.Pet.Length) return;

            // Assuming Core.Type.Pet is an array of the conceptual PetStruct
            Core.Type.Pet[petTypeIndex] = default; // Reset struct to default values
            // Ensure complex types are cleared/reinitialized if 'default' isn't sufficient
            // For example, if using classes or lists within the struct:
            // Core.Type.Pet[petTypeIndex].Name = "";
            // Core.Type.Pet[petTypeIndex].Stat = new int[(int)Core.Enum.StatType.Count];
            // Core.Type.Pet[petTypeIndex].Skill = new int[Constant.MAX_PET_SKILLS];
            // Core.Type.Pet[petTypeIndex].LearnableSkills?.Clear();
            // Core.Type.Pet[petTypeIndex].AvailableTalents?.Clear();
            // Core.Type.Pet[petTypeIndex].AllowedEquipmentSlots?.Clear();
            // Core.Type.Pet[petTypeIndex].PossibleEvolutions?.Clear();
            // Core.Type.Pet[petTypeIndex].EvolutionRequirements?.Clear();

            PetDataRequested.Remove(petTypeIndex); // Allow requesting again
        }

        // Clears all static pet data cache and owned pet list
        public static void ClearAllPetData()
        {
            // Clear Static Data Cache
            if (Core.Type.Pet != null)
            {
                for (int i = 0; i < Core.Type.Pet.Length; i++)
                    ClearPetStaticData(i);
            }
            // Core.Type.Pet = new Core.Type.PetStruct[Constant.MAX_PETS]; // Reinitialize if needed

            // Clear Active State
            PetSkillCD = new int[Constant.MAX_PET_SKILLS]; // Reset cooldowns array
            PetSkillBuffer = -1; // Reset skill buffer
            PetSkillBufferTimer = 0;
            PetStaticDataRequested.Clear(); // Clear request tracking

            // Clear Owned Pet List Cache
            OwnedPets.Clear();

            // Clear active pet data for all players (important on disconnect/map change)
            if (Core.Type.Player != null)
            {
                for (int i = 0; i < Core.Type.Player.Length; i++)
                {
                     // Assuming Core.Type.Player[i].Pet exists and is the conceptual PlayerPetData
                     // Core.Type.Player[i].Pet = default; // Or new PlayerPetData();
                     // Ensure complex types within PlayerPetData are cleared too (Lists, Dictionaries)
                }
            }
        }

        // Requests static data for a specific pet type if not already loaded/requested
        public static void RequestPetStaticDataIfNeeded(int petTypeIndex)
        {
            if (petTypeIndex <= 0 || petTypeIndex >= Core.Type.Pet.Length) return; // Use 0 as invalid ID

            // Check if data exists (using Name as a proxy for loaded data) OR if already requested
            // Add null check for Core.Type.Pet itself
            if (Core.Type.Pet == null) return; // Cannot request if array is null

            if (string.IsNullOrEmpty(Core.Type.Pet[petTypeIndex].Name) && !PetStaticDataRequested.GetValueOrDefault(petTypeIndex, false))
            {
                PetStaticDataRequested[petTypeIndex] = true; // Mark as requested
                SendRequestPetStaticData(petTypeIndex); // Send network request
            }
        }

        // Ensures static data for the player's *currently summoned* pet is loaded.
        public static void StreamPlayerActivePetData(int playerIndex)
        {
             if (playerIndex < 0 || playerIndex >= Core.Type.Player.Length) return;
             // Add null check for Player array
             if (Core.Type.Player == null) return;

             // Assuming Player[playerIndex].Pet holds the active pet data
             int petTypeIndex = Core.Type.Player[playerIndex].Pet.Num;
             RequestPetStaticDataIfNeeded(petTypeIndex);
        }

        #endregion

        #region Outgoing Packets (Expanded & Refined)

        // Renamed for clarity - requests STATIC data for a specific pet type
        public static void SendRequestPetStaticData(int petTypeIndex)
        {
            if (petTypeIndex <= 0) return; // Guard against invalid index
            var buffer = new ByteStream(4 + 4); // Header + Int32
            buffer.WriteInt32((int)Packets.ClientPackets.CRequestPetStaticData); // Use a dedicated packet ID
            buffer.WriteInt32(petTypeIndex);
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        // Requests an update for the player's currently summoned pet INSTANCE data.
        public static void SendRequestMySummonedPetUpdate()
        {
            var buffer = new ByteStream(4);
            buffer.WriteInt32((int)Packets.ClientPackets.CRequestMyPetUpdate); // New, clearer packet ID
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        // Send updated pet stance settings
        public static void SendSetPetStance(PetMovementStance moveStance, PetCombatStance combatStance, PetTargetingPriority targetPriority)
        {
            var buffer = new ByteStream(4 + 1 + 1 + 1); // Header + 3 bytes (enums)
            buffer.WriteInt32((int)Packets.ClientPackets.CSetPetStance); // New, clearer packet ID
            buffer.WriteByte((byte)moveStance);
            buffer.WriteByte((byte)combatStance);
            buffer.WriteByte((byte)targetPriority);
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();

            // Client-side prediction (optional, update local player's pet data immediately for responsiveness)
            if (GameState.MyIndex >= 0 && GameState.MyIndex < Core.Type.Player.Length)
            {
                 // Check if player and pet data are valid before predicting
                 if (Core.Type.Player != null && Core.Type.Player[GameState.MyIndex].Pet.Num > 0)
                 {
                    Core.Type.Player[GameState.MyIndex].Pet.MovementStance = moveStance;
                    Core.Type.Player[GameState.MyIndex].Pet.CombatStance = combatStance;
                    Core.Type.Player[GameState.MyIndex].Pet.TargetingPriority = targetPriority;
                    // Update Pet UI stance indicators immediately
                    // UI.PetWindow.UpdateStanceIndicators();
                 }
            }
        }

        // Send request to spend a stat point
        public static void SendTrainPetStat(Core.Enum.StatType statNum) // Use the actual Enum type
        {
            if (GameState.MyIndex < 0 || GameState.MyIndex >= Core.Type.Player.Length || Core.Type.Player == null) return;

            if (Core.Type.Player[GameState.MyIndex].Pet.Points <= 0)
            {
                 // Optional: Show message "No stat points available"
                 // UIManager.ShowMessage("You have no pet stat points to spend.", MessageType.Warning);
                 return;
            }
            var buffer = new ByteStream(4 + 1); // Header + Byte (enum)
            buffer.WriteInt32((int)Packets.ClientPackets.CPetUseStatPoint);
            buffer.WriteByte((byte)statNum); // Send the byte value
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();

            // Optional: Client-side prediction (decrement points, visually update stat) - Risky, server should confirm.
            // Core.Type.Player[GameState.MyIndex].Pet.Points--;
            // UI.PetWindow.UpdateStats(); // Refresh UI
        }

        // Requests the list of *all* pets the player owns (for stable/summoning UI)
        public static void SendRequestOwnedPetsList()
        {
            var buffer = new ByteStream(4);
            buffer.WriteInt32((int)Packets.ClientPackets.CRequestOwnedPets); // Clearer Packet ID
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        // Request to use a pet skill from a specific slot
        public static void SendUsePetSkill(int skillSlotIndex) // Use slot index for clarity
        {
            if (skillSlotIndex < 0 || skillSlotIndex >= Constant.MAX_PET_SKILLS) return;
            if (GameState.MyIndex < 0 || GameState.MyIndex >= Core.Type.Player.Length || Core.Type.Player == null) return;

            ref var petData = ref Core.Type.Player[GameState.MyIndex].Pet;
            if (petData.Num <= 0 || petData.Alive == 0) return; // No pet or dead pet

            int skillId = petData.Skill[skillSlotIndex];
            if (skillId <= 0) { /* Optional: Message "No skill in this slot" */ return; }

            // Check client-side cooldown
            if (PetSkillCD.Length > skillSlotIndex && PetSkillCD[skillSlotIndex] > General.GetTickCount())
            {
                 // Optional: Message "Skill on cooldown"
                 // float remainingCD = (PetSkillCD[skillSlotIndex] - General.GetTickCount()) / 1000.0f;
                 // UIManager.ShowMessage($"Skill on cooldown ({remainingCD:F1}s remaining).", MessageType.Warning);
                 return;
            }

            // Check mana cost (requires skill definitions accessible client-side)
            // int manaCost = GetSkillManaCost(skillId); // Needs helper function
            // if (petData.Mana < manaCost) {
            //     UIManager.ShowMessage("Pet does not have enough mana.", MessageType.Warning);
            //     return;
            // }

            // Check existing buffer (prevent spamming skill use packets)
            if (PetSkillBuffer != -1 && PetSkillBufferTimer + Constant.NETWORK_BUFFER_TIMEOUT > General.GetTickCount())
            {
                 // Optional: Message "Another skill is being cast"
                 // UIManager.ShowMessage("Pet is busy.", MessageType.Info);
                 return;
            }

            var buffer = new ByteStream(4 + 4); // Header + Int32 (skill slot index)
            buffer.WriteInt32((int)Packets.ClientPackets.CPetUseSkill);
            buffer.WriteInt32(skillSlotIndex); // Send slot index
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();

            // Set client-side buffer and PREDICTIVE cooldown (server will confirm/override via Packet_PetSkillCooldown)
            PetSkillBuffer = skillSlotIndex; // Buffer the slot index
            PetSkillBufferTimer = General.GetTickCount();
            // int cooldownDuration = GetSkillCooldownDuration(skillId); // Needs helper function
            // PetSkillCD[skillSlotIndex] = General.GetTickCount() + cooldownDuration;
            // UIManager.PetSkillBar.UpdateCooldown(skillSlotIndex, PetSkillCD[skillSlotIndex]); // Update UI immediately
        }

        // Summon a specific pet from the owned list
        public static void SendSummonPet(int ownedPetIndex) // Index in the player's OwnedPets list
        {
            if (ownedPetIndex < 0) return;
            var buffer = new ByteStream(4 + 4); // Header + Int32
            buffer.WriteInt32((int)Packets.ClientPackets.CSummonPet);
            buffer.WriteInt32(ownedPetIndex);
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        // Unsummon the currently active pet
        public static void SendUnsummonPet()
        {
            if (GameState.MyIndex < 0 || GameState.MyIndex >= Core.Type.Player.Length || Core.Type.Player == null) return;

            // Check if a pet is actually summoned and alive
            if (Core.Type.Player[GameState.MyIndex].Pet.Num <= 0 || Core.Type.Player[GameState.MyIndex].Pet.Alive == 0) return;

            var buffer = new ByteStream(4);
            buffer.WriteInt32((int)Packets.ClientPackets.CUnsummonPet); // Renamed from CReleasePet for clarity
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        // --- NEW --- Permanently delete a pet from the owned list
        public static void SendDeleteOwnedPet(int ownedPetIndex)
        {
             if (ownedPetIndex < 0 || ownedPetIndex >= OwnedPets.Count) return;

             // IMPORTANT: Add confirmation dialog here!
             // string petNameToConfirm = string.IsNullOrEmpty(OwnedPets[ownedPetIndex].CustomName) ? OwnedPets[ownedPetIndex].DefaultName : OwnedPets[ownedPetIndex].CustomName;
             // UIManager.ShowConfirmationDialog($"Are you sure you want to permanently release {petNameToConfirm}?",
             //    () => { // On Confirm Action:
                     var buffer = new ByteStream(4 + 4);
                     buffer.WriteInt32((int)Packets.ClientPackets.CDeleteOwnedPet); // New Packet ID
                     buffer.WriteInt32(ownedPetIndex); // Send the index in the owned list
                     NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
                     buffer.Dispose();
             //    },
             //    null // On Cancel Action
             // );
        }


        // Request to enter edit mode (Admin only) - Assuming this triggers server-side checks and potentially locks the pet data
        public static void SendRequestEnterPetEditMode(int petTypeIndex) // Request edit for a specific type
        {
            // Client-side admin check (optional, server MUST verify)
            // if (GetPlayerAccess(GameState.MyIndex) < AdminLevel.Admin) return;

            if (petTypeIndex <= 0) return;
            ByteStream buffer = new ByteStream(4 + 4);
            buffer.WriteInt32((int)Packets.ClientPackets.CRequestEnterPetEditMode); // Clearer Packet ID
            buffer.WriteInt32(petTypeIndex);
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        // Send updated STATIC data for a pet type (Admin only)
        public static void SendSavePetStaticData(int petTypeIndex)
        {
             // Client-side admin check (optional, server MUST verify)
             // if (GetPlayerAccess(GameState.MyIndex) < AdminLevel.Admin) return;

             if (petTypeIndex <= 0 || petTypeIndex >= Core.Type.Pet.Length || Core.Type.Pet == null) return;

             // Assume data is held in Core.Type.Pet[petTypeIndex] after being edited in some UI
             ref var petData = ref Core.Type.Pet[petTypeIndex];

             // Check if essential data is present
             if (string.IsNullOrWhiteSpace(petData.Name) || petData.Sprite <= 0)
             {
                 // UIManager.ShowMessage("Cannot save pet data: Name and Sprite are required.", MessageType.Error);
                 return;
             }

             ByteStream buffer = new ByteStream(2048); // Increased buffer size for complex data
             buffer.WriteInt32((int)Packets.ClientPackets.CSavePetStaticData); // Renamed packet ID
             buffer.WriteInt32(petTypeIndex);

             // --- Serialize the conceptual PetStruct ---
             // This needs careful implementation matching the server's deserialization
             buffer.WriteInt32(petData.Num); // Num should match petTypeIndex
             buffer.WriteString(petData.Name ?? ""); // Null check
             buffer.WriteInt32(petData.Sprite);
             buffer.WriteInt32(petData.Range);
             buffer.WriteInt32(petData.MaxLevel);
             buffer.WriteInt32(petData.LevelPnts);
             buffer.WriteByte(petData.StatType); // Growth curve ID?
             buffer.WriteByte(petData.LevelingType); // EXP table ID?

             // Base Stats
             for (int i = 0; i < (int)Core.Enum.StatType.Count; i++)
                 buffer.WriteInt32(petData.Stat[i]); // Send base stats as Int32

             // Default/Learnable Skills (Clarify exact meaning - Sending the Skill array for now)
             for (int i = 0; i < Core.Constant.MAX_PET_SKILLS; i++)
                 buffer.WriteInt32(petData.Skill[i]); // Send as Int32

             buffer.WriteByte(petData.Evolvable);
             buffer.WriteInt32(petData.EvolveLevel);
             buffer.WriteInt32(petData.EvolveNum); // Primary evolution target

             // --- NEW FIELDS Serialization (Example) ---
             // buffer.WriteFloat(petData.BaseMoveSpeed);
             // buffer.WriteFloat(petData.BaseAttackSpeed);
             // buffer.WriteString(petData.Description ?? "");
             // buffer.WriteInt32(petData.FoodItemPreference);
             // buffer.WriteInt32(petData.SizeCategory);

             // Write List<int> LearnableSkills
             // buffer.WriteInt32(petData.LearnableSkills?.Count ?? 0);
             // if (petData.LearnableSkills != null) foreach(int id in petData.LearnableSkills) buffer.WriteInt32(id);

             // Write List<PetTalentData> AvailableTalents (Complex serialization - maybe send as JSON string or dedicated binary format)

             // Write List<PetEquipmentSlot> AllowedEquipmentSlots
             // buffer.WriteInt32(petData.AllowedEquipmentSlots?.Count ?? 0);
             // if (petData.AllowedEquipmentSlots != null) foreach(var slot in petData.AllowedEquipmentSlots) buffer.WriteByte((byte)slot);

             // Write List<int> PossibleEvolutions
             // buffer.WriteInt32(petData.PossibleEvolutions?.Count ?? 0);
             // if (petData.PossibleEvolutions != null) foreach(int id in petData.PossibleEvolutions) buffer.WriteInt32(id);

             // Write List<EvolutionRequirement> EvolutionRequirements (Complex serialization)

             NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
             buffer.Dispose();
        }

        // --- NEW PACKETS ---

        // Equip an item from player inventory to pet slot
        public static void SendPetEquipItem(int playerInventorySlot, PetEquipmentSlot petSlot)
        {
            // Add client-side checks: Is item valid? Can pet use this slot? Is slot empty?
            // if (!IsValidItem(playerInventorySlot) || !CanPetUseSlot(petSlot) || IsPetSlotOccupied(petSlot)) return;

            var buffer = new ByteStream(4 + 4 + 1); // Header + InvSlot + PetSlot (byte)
            buffer.WriteInt32((int)Packets.ClientPackets.CPetEquipItem);
            buffer.WriteInt32(playerInventorySlot);
            buffer.WriteByte((byte)petSlot);
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        // Unequip an item from pet slot back to player inventory
        public static void SendPetUnequipItem(PetEquipmentSlot petSlot)
        {
            // Add client-side checks: Is slot occupied? Does player have inventory space?
            // if (!IsPetSlotOccupied(petSlot) || !HasInventorySpace()) return;

            var buffer = new ByteStream(4 + 1); // Header + PetSlot (byte)
            buffer.WriteInt32((int)Packets.ClientPackets.CPetUnequipItem);
            buffer.WriteByte((byte)petSlot);
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

         // Learn or rank up a talent
         public static void SendPetLearnTalent(int talentId)
         {
            if (GameState.MyIndex < 0 || GameState.MyIndex >= Core.Type.Player.Length || Core.Type.Player == null) return;
            ref var petData = ref Core.Type.Player[GameState.MyIndex].Pet;

             // Client-side checks (optional, server MUST validate)
             if (petData.UnspentTalentPoints <= 0) { /* Msg: No points */ return; }
             // PetTalentData talentInfo = GetTalentData(talentId); // Needs helper
             // if (!IsTalentLearnable(petData, talentInfo)) { /* Msg: Requirements not met */ return; }
             // int currentPoints = petData.LearnedTalents.GetValueOrDefault(talentId, 0);
             // if (currentPoints >= talentInfo.MaxPoints) { /* Msg: Talent maxed */ return; }

             var buffer = new ByteStream(4 + 4); // Header + TalentID
             buffer.WriteInt32((int)Packets.ClientPackets.CPetLearnTalent);
             buffer.WriteInt32(talentId);
             NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
             buffer.Dispose();

             // Client-side prediction (optional)
             // petData.UnspentTalentPoints--;
             // petData.LearnedTalents[talentId] = currentPoints + 1;
             // UI.PetTalentWindow.UpdateTalent(talentId);
             // UI.PetWindow.UpdateStats(); // If talent affects stats shown
         }

         // Feed the pet using an item from player inventory
         public static void SendPetFeed(int playerInventorySlot)
         {
            if (GameState.MyIndex < 0 || GameState.MyIndex >= Core.Type.Player.Length || Core.Type.Player == null) return;
             ref var petData = ref Core.Type.Player[GameState.MyIndex].Pet;
             if (petData.Num <= 0 || petData.Alive == 0) return; // No pet or dead pet

             // Basic client-side cooldown to prevent spam
             if (lastPetActionTime + PetActionCooldown > General.GetTickCount()) return;

             // Client-side check: Is item valid food?
             // ItemData foodItem = GetItemData(playerInventorySlot);
             // if (foodItem == null || !foodItem.IsPetFood) { /* Msg: Not food */ return; }

             var buffer = new ByteStream(4 + 4); // Header + InvSlot
             buffer.WriteInt32((int)Packets.ClientPackets.CPetFeed);
             buffer.WriteInt32(playerInventorySlot);
             NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
             buffer.Dispose();
             lastPetActionTime = General.GetTickCount();
             // Optional: Play feeding animation/sound immediately
         }

         // Rename the currently summoned pet
         public static void SendPetRename(string newName)
         {
            if (GameState.MyIndex < 0 || GameState.MyIndex >= Core.Type.Player.Length || Core.Type.Player == null) return;
             ref var petData = ref Core.Type.Player[GameState.MyIndex].Pet;
             if (petData.Num <= 0 || petData.Alive == 0) return; // No pet or dead pet

             // Validate name (basic checks, server should do more thorough validation)
             if (string.IsNullOrWhiteSpace(newName) || newName.Length > Constant.MAX_NAME_LENGTH)
             {
                 // Optional: Message "Invalid name."
                 // UIManager.ShowMessage($"Invalid pet name. Max length: {Constant.MAX_NAME_LENGTH}", MessageType.Warning);
                 return;
             }
             // Potentially check for profanity client-side (basic filter)

             var buffer = new ByteStream(4 + ByteStream.GetStringSize(newName)); // Header + String
             buffer.WriteInt32((int)Packets.ClientPackets.CPetRename);
             buffer.WriteString(newName);
             NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
             buffer.Dispose();

             // Client prediction (optional)
             // petData.CustomName = newName;
             // UI.PetWindow.UpdateName();
         }

         // Give the pet a direct command (attack specific target, move to point, etc.)
         public static void SendPetDirectCommand(PetDirectCommandType commandType, int targetIndex, TargetType targetType, int targetX = 0, int targetY = 0)
         {
            if (GameState.MyIndex < 0 || GameState.MyIndex >= Core.Type.Player.Length || Core.Type.Player == null) return;
             ref var petData = ref Core.Type.Player[GameState.MyIndex].Pet;
             if (petData.Num <= 0 || petData.Alive == 0) return; // No pet or dead pet

             // Add client-side validation (e.g., is target valid? is location reachable?)
             // if (!IsValidTarget(targetIndex, targetType) || !IsWithinLeashRange(targetX, targetY)) return;

             var buffer = new ByteStream(4 + 1 + 4 + 1 + 4 + 4); // Header + CommandType + TargetIdx + TargetType + TargetX + TargetY
             buffer.WriteInt32((int)Packets.ClientPackets.CPetDirectCommand);
             buffer.WriteByte((byte)commandType);
             buffer.WriteInt32(targetIndex); // e.g., Player index, NPC index, Item unique ID
             buffer.WriteByte((byte)targetType); // Player, NPC, MapPoint, Item
             buffer.WriteInt32(targetX); // X coordinate if targetType is MapPoint
             buffer.WriteInt32(targetY); // Y coordinate if targetType is MapPoint
             NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
             buffer.Dispose();
             // Optional: Show visual indicator of the command (e.g., marker on target/ground)
         }

        #endregion

        #region Incoming Packets (Expanded & Refined)

        // Handles updates for a specific player's ACTIVE pet instance data
        public static void Packet_UpdatePlayerPet(ref byte[] data)
        {
            var buffer = new ByteStream(data);
            int playerIndex = buffer.ReadInt32();

            if (playerIndex < 0 || playerIndex >= Core.Type.Player.Length || Core.Type.Player == null)
            {
                buffer.Dispose();
                return; // Invalid index
            }

            // Get reference to the specific player's pet data structure
            // This ASSUMES Core.Type.Player[playerIndex].Pet exists and matches the conceptual PlayerPetData
            ref var petData = ref Core.Type.Player[playerIndex].Pet;

            // --- Read Base Data ---
            petData.Num = buffer.ReadInt32();
            petData.UniqueID = buffer.ReadInt32(); // Read unique instance ID
            petData.CustomName = buffer.ReadString(); // Read custom name

            petData.Level = buffer.ReadInt32();
            petData.Exp = buffer.ReadInt32();
            petData.Tnl = buffer.ReadInt32();

            petData.Health = buffer.ReadInt32();
            petData.MaxHp = buffer.ReadInt32();
            petData.Mana = buffer.ReadInt32();
            petData.MaxMp = buffer.ReadInt32();

            petData.Alive = buffer.ReadByte(); // Read as Byte (0 or 1)

            // --- Read Position & Movement ---
            petData.X = buffer.ReadInt32();
            petData.Y = buffer.ReadInt32();
            petData.Dir = buffer.ReadInt32();
            // Note: Offsets, Moving, Steps, Attacking, AttackTimer are usually managed client-side or via specific movement/attack packets

            // --- Read Stats & Points ---
            petData.Points = buffer.ReadInt32(); // Unspent stat points
            for (int i = 0; i < (int)Core.Enum.StatType.Count; i++)
            {
                 if (petData.Stat == null || petData.Stat.Length <= i) break; // Safety check
                 petData.Stat[i] = buffer.ReadInt32(); // Read calculated stat values as Int32
            }

            // --- Read Active Skills ---
            for (int i = 0; i < Core.Constant.MAX_PET_SKILLS; i++)
            {
                 if (petData.Skill == null || petData.Skill.Length <= i) break; // Safety check
                 petData.Skill[i] = buffer.ReadInt32(); // Current active skills
            }

            // --- Read Stances ---
            petData.MovementStance = (PetMovementStance)buffer.ReadByte();
            petData.CombatStance = (PetCombatStance)buffer.ReadByte();
            petData.TargetingPriority = (PetTargetingPriority)buffer.ReadByte();

            // --- Read Loyalty & Hunger ---
            petData.Loyalty = buffer.ReadFloat();
            petData.Hunger = buffer.ReadFloat();

            // --- Read Equipment ---
            if (petData.Equipment == null) petData.Equipment = new int[(int)PetEquipmentSlot.Count]; // Initialize if null
            for(int i = 0; i < (int)PetEquipmentSlot.Count; i++)
            {
                petData.Equipment[i] = buffer.ReadInt32(); // Item ID or 0
            }

            // --- Read Talent Points ---
            petData.UnspentTalentPoints = buffer.ReadInt32();

            // --- Read Learned Talents (Example: count followed by key-value pairs) ---
            int talentCount = buffer.ReadInt32();
            if (petData.LearnedTalents == null) petData.LearnedTalents = new Dictionary<int, int>(talentCount);
            petData.LearnedTalents.Clear();
            for(int i = 0; i < talentCount; i++)
            {
                 int talentId = buffer.ReadInt32();
                 int points = buffer.ReadByte(); // Points invested likely a small number
                 petData.LearnedTalents[talentId] = points;
            }

            // --- Read Status Effects (Example: count followed by data) ---
            int statusCount = buffer.ReadInt32();
            if (petData.StatusEffects == null) petData.StatusEffects = new List<ActiveStatusEffect>(statusCount);
            petData.StatusEffects.Clear();
            // StatusEffect definition likely needed here to deserialize properly
            // for(int i = 0; i < statusCount; i++)
            // {
            //      ActiveStatusEffect effect = new ActiveStatusEffect();
            //      effect.StatusID = buffer.ReadInt32();
            //      effect.RemainingDurationTicks = buffer.ReadInt32();
            //      effect.Stacks = buffer.ReadByte();
            //      effect.SourceID = buffer.ReadInt32(); // If needed
            //      // Fetch icon/isBuff from status definitions based on StatusID
            //      // effect.IconTextureID = GetStatusEffectDefinition(effect.StatusID).IconID;
            //      // effect.IsBuff = GetStatusEffectDefinition(effect.StatusID).IsBuff;
            //      petData.StatusEffects.Add(effect);
            // }

            // --- Read Pet Inventory (Example: fixed size array) ---
            // if (petData.Inventory == null) petData.Inventory = new int[MaxPetInventorySlots];
            // for(int i = 0; i < MaxPetInventorySlots; i++)
            // {
            //      petData.Inventory[i] = buffer.ReadInt32(); // Item ID or 0
            // }


            buffer.Dispose();

            // After receiving data, ensure the static data for this pet type is loaded if it's visible/relevant
            if (petData.Num > 0 && petData.Alive == 1)
            {
                 RequestPetStaticDataIfNeeded(petData.Num);

                 // If it's our pet, update all relevant UI elements
                 if (playerIndex == GameState.MyIndex)
                 {
                     // UI.PetWindow.UpdateDisplay(); // Full update
                     // UI.PetSkillBar.UpdateSkills();
                     // UI.PetStatusIcons.UpdateEffects();
                     // ... etc
                 }
            }
            else if (playerIndex == GameState.MyIndex)
            {
                 // If our pet is now Num=0 or Alive=0, ensure UI reflects this (e.g., hide pet window)
                 // UI.PetWindow.Hide(); or UI.PetWindow.ShowInactiveState();
            }
        }

        // Handles receiving STATIC data for a specific pet type
        public static void Packet_ReceivePetStaticData(ref byte[] data) // Renamed from Packet_UpdatePet
        {
            var buffer = new ByteStream(data);
            int petTypeIndex = buffer.ReadInt32();

             if (petTypeIndex <= 0 || petTypeIndex >= Core.Type.Pet.Length || Core.Type.Pet == null)
            {
                buffer.Dispose();
                PetStaticDataRequested.Remove(petTypeIndex); // Remove potentially invalid request flag
                return; // Invalid index or array
            }

            // Get reference to the static data structure
            ref var petData = ref Core.Type.Pet[petTypeIndex];

            // Clear previous request flag (even if read fails, don't spam requests)
            PetStaticDataRequested.Remove(petTypeIndex);

            try // Wrap in try-catch as malformed data could crash client
            {
                // --- Deserialize the conceptual PetStruct ---
                // Matches SendSavePetStaticData serialization order

                petData.Num = buffer.ReadInt32(); // Should match petTypeIndex
                if (petData.Num != petTypeIndex) { /* Log warning? */ }

                petData.Name = buffer.ReadString();
                petData.Sprite = buffer.ReadInt32();
                petData.Range = buffer.ReadInt32();
                petData.MaxLevel = buffer.ReadInt32();
                petData.LevelPnts = buffer.ReadInt32();
                petData.StatType = buffer.ReadByte();
                petData.LevelingType = buffer.ReadByte();

                // Base Stats
                if (petData.Stat == null) petData.Stat = new int[(int)Core.Enum.StatType.Count];
                for (int i = 0; i < (int)Core.Enum.StatType.Count; i++)
                    petData.Stat[i] = buffer.ReadInt32();

                // Default/Learnable Skills?
                if (petData.Skill == null) petData.Skill = new int[Core.Constant.MAX_PET_SKILLS];
                for (int i = 0; i < Core.Constant.MAX_PET_SKILLS; i++)
                    petData.Skill[i] = buffer.ReadInt32();

                petData.Evolvable = buffer.ReadByte();
                petData.EvolveLevel = buffer.ReadInt32();
                petData.EvolveNum = buffer.ReadInt32();

                // --- NEW FIELDS Deserialization (Example) ---
                // petData.BaseMoveSpeed = buffer.ReadFloat();
                // petData.BaseAttackSpeed = buffer.ReadFloat();
                // petData.Description = buffer.ReadString();
                // petData.FoodItemPreference = buffer.ReadInt32();
                // petData.SizeCategory = buffer.ReadInt32();

                // Read List<int> LearnableSkills
                // int learnableCount = buffer.ReadInt32();
                // petData.LearnableSkills = new List<int>(learnableCount);
                // for(int i=0; i<learnableCount; i++) petData.LearnableSkills.Add(buffer.ReadInt32());

                // Read List<PetTalentData> AvailableTalents (Complex deserialization)

                // Read List<PetEquipmentSlot> AllowedEquipmentSlots
                // int allowedSlotCount = buffer.ReadInt32();
                // petData.AllowedEquipmentSlots = new List<PetEquipmentSlot>(allowedSlotCount);
                // for(int i=0; i<allowedSlotCount; i++) petData.AllowedEquipmentSlots.Add((PetEquipmentSlot)buffer.ReadByte());

                // Read List<int> PossibleEvolutions
                // int evoCount = buffer.ReadInt32();
                // petData.PossibleEvolutions = new List<int>(evoCount);
                // for(int i=0; i<evoCount; i++) petData.PossibleEvolutions.Add(buffer.ReadInt32());

                // Read List<EvolutionRequirement> EvolutionRequirements (Complex deserialization)

                // Mark as successfully loaded only if name is valid
                if (!string.IsNullOrEmpty(petData.Name))
                {
                    // Potentially update any UI elements that were waiting for this data
                    // UI.PetStableWindow.UpdatePetTypeInfo(petTypeIndex);
                }
                else
                {
                     // Data might be corrupt or incomplete, clear it to avoid issues
                     ClearPetStaticData(petTypeIndex);
                     // Log error: Received invalid static data for pet type petTypeIndex
                }

            }
            catch (Exception ex)
            {
                 // Log error: Failed to deserialize PetStaticData for index petTypeIndex. Exception: ex.Message
                 ClearPetStaticData(petTypeIndex); // Clear potentially corrupt data
            }
            finally
            {
                 buffer.Dispose();
            }
        }


        // Handles pet movement start
        public static void Packet_PetMove(ref byte[] data)
        {
            var buffer = new ByteStream(data);
            int playerIndex = buffer.ReadInt32();
            int x = buffer.ReadInt32();
            int y = buffer.ReadInt32();
            int dir = buffer.ReadInt32();
            int movement = buffer.ReadInt32(); // Movement Type (e.g., Walking, Running)

             if (playerIndex < 0 || playerIndex >= Core.Type.Player.Length || Core.Type.Player == null) { buffer.Dispose(); return; }

            ref var pet = ref Core.Type.Player[playerIndex].Pet;
            // Only update if the pet is alive and matches the expected state
            if (pet.Num <= 0 || pet.Alive == 0) { buffer.Dispose(); return; }

            // If already at the target or moving, this might be a correction or new move starting
            // Let ProcessPetMovement handle interpolation, just set the target state here.
            pet.X = x;
            pet.Y = y;
            pet.Dir = dir;
            pet.Moving = (byte)movement; // Set the type of movement
            pet.Steps = 0; // Reset step animation for new move
            pet.XOffset = 0; // Reset offsets initially
            pet.YOffset = 0;

            // Set starting offset based on direction for smooth interpolation IN
            // This assumes the packet means "Pet *started* moving FROM the previous tile *towards* X,Y"
            switch ((Core.Enum.DirectionType)pet.Dir)
            {
                case Core.Enum.DirectionType.Up:    pet.YOffset = GameState.SizeY; break; // Start one tile below, move up
                case Core.Enum.DirectionType.Down:  pet.YOffset = -GameState.SizeY; break;// Start one tile above, move down
                case Core.Enum.DirectionType.Left:  pet.XOffset = GameState.SizeX; break; // Start one tile right, move left
                case Core.Enum.DirectionType.Right: pet.XOffset = -GameState.SizeX; break;// Start one tile left, move right
            }
            buffer.Dispose();
        }

        // Handles pet direction change when not moving
        public static void Packet_PetDir(ref byte[] data)
        {
            var buffer = new ByteStream(data);
            int playerIndex = buffer.ReadInt32();
            int dir = buffer.ReadInt32();

            if (playerIndex >= 0 && playerIndex < Core.Type.Player.Length && Core.Type.Player != null)
            {
                 ref var pet = ref Core.Type.Player[playerIndex].Pet;
                 // Only update dir if not currently mid-move to avoid visual snap
                 if (pet.Num > 0 && pet.Alive == 1 && pet.Moving == (byte)Core.Enum.MovementType.None)
                 {
                     pet.Dir = dir;
                 }
            }
            buffer.Dispose();
        }

        // Handles updates to HP or MP
        public static void Packet_PetVital(ref byte[] data)
        {
            var buffer = new ByteStream(data);
            int playerIndex = buffer.ReadInt32();
            int vitalType = buffer.ReadInt32(); // Example: 0=HP, 1=MP. Use Enum if available Core.Enum.VitalType?

            if (playerIndex < 0 || playerIndex >= Core.Type.Player.Length || Core.Type.Player == null) { buffer.Dispose(); return; }

            ref var pet = ref Core.Type.Player[playerIndex].Pet;
            if (pet.Num <= 0 || pet.Alive == 0) { buffer.Dispose(); return; } // Ignore if no pet/dead

            if (vitalType == 0) // HP Update
            {
                pet.MaxHp = buffer.ReadInt32();
                pet.Health = buffer.ReadInt32();
                // Clamp health just in case
                pet.Health = Math.Clamp(pet.Health, 0, pet.MaxHp);
            }
            else if (vitalType == 1) // MP Update
            {
                pet.MaxMp = buffer.ReadInt32();
                pet.Mana = buffer.ReadInt32();
                // Clamp mana
                pet.Mana = Math.Clamp(pet.Mana, 0, pet.MaxMp);
            }
            // Add other vitals like EXP here if needed, though EXP has its own packet usually

            // If it's our pet, update UI
            if (playerIndex == GameState.MyIndex)
            {
                 // UI.PetWindow.UpdateVitals();
                 // UI.PetBar.UpdateVitals();
            }
            // Maybe update overhead mini-bar if implemented
            // DrawMiniPetHealthBar needs access to this data

            buffer.Dispose();
        }

        // Clears the client-side skill buffer (e.g., server confirmed skill started/failed)
        public static void Packet_ClearPetSkillBuffer(ref byte[] data) // Can optionally include reason/skill slot
        {
            // var buffer = new ByteStream(data);
            // int skillSlot = buffer.ReadInt32(); // Optional: Which skill buffer cleared
            // bool success = buffer.ReadBoolean(); // Optional: Did it succeed or fail?
            // buffer.Dispose();

            PetSkillBuffer = -1;
            PetSkillBufferTimer = 0;

            // If server indicated failure, potentially reset cooldown (though server should manage authoritative CD)
            // if (!success && skillSlot >= 0 && skillSlot < PetSkillCD.Length) {
            //    PetSkillCD[skillSlot] = 0;
            //    UIManager.PetSkillBar.UpdateCooldown(skillSlot, 0);
            // }
        }

        // Indicates pet started its attack animation
        public static void Packet_PetAttack(ref byte[] data)
        {
            var buffer = new ByteStream(data);
            int playerIndex = buffer.ReadInt32();
            // int targetType = buffer.ReadByte(); // Optional: Type of target being attacked
            // int targetIndex = buffer.ReadInt32(); // Optional: Index of target being attacked

             if (playerIndex < 0 || playerIndex >= Core.Type.Player.Length || Core.Type.Player == null) { buffer.Dispose(); return; }

            ref var pet = ref Core.Type.Player[playerIndex].Pet;
            if (pet.Num <= 0 || pet.Alive == 0) { buffer.Dispose(); return; } // Ignore if no pet/dead

            // Set pet to attacking state for animation
            pet.Attacking = 1;
            pet.AttackTimer = General.GetTickCount(); // Start animation timer

            // Optional: Play attack sound based on pet type/weapon
            // SoundManager.PlaySound(GetPetAttackSound(pet.Num));

            buffer.Dispose();
        }

        // Instantaneous position update (teleport, correction)
         public static void Packet_PetXY(ref byte[] data)
         {
             // Note: Consider if Packet_UpdatePlayerPet or Packet_PetMove already cover necessary position updates.
             // This should be used for corrections or non-interpolated movements.
             var buffer = new ByteStream(data);
             int playerIndex = buffer.ReadInt32();
             if (playerIndex < 0 || playerIndex >= Core.Type.Player.Length || Core.Type.Player == null) { buffer.Dispose(); return; }

             ref var pet = ref Core.Type.Player[playerIndex].Pet;
             if (pet.Num <= 0) { buffer.Dispose(); return; } // Ignore if no pet

             pet.X = buffer.ReadInt32();
             pet.Y = buffer.ReadInt32();
             pet.Moving = (byte)Core.Enum.MovementType.None; // Stop any current movement interpolation
             pet.XOffset = 0; // Snap to grid
             pet.YOffset = 0;
             pet.Attacking = 0; // Stop attack animation if correcting position

             buffer.Dispose();
         }


        // Handles EXP updates for the owner's pet
        public static void Packet_PetExperience(ref byte[] data)
        {
            var buffer = new ByteStream(data);
            int myIndex = GameState.MyIndex; // Assumes this packet is only sent to the owner

            if (myIndex < 0 || myIndex >= Core.Type.Player.Length || Core.Type.Player == null) { buffer.Dispose(); return; }
             ref var pet = ref Core.Type.Player[myIndex].Pet;
             if (pet.Num <= 0 || pet.Alive == 0) { buffer.Dispose(); return; } // Ignore if no pet/dead

            pet.Exp = buffer.ReadInt32();
            pet.Tnl = buffer.ReadInt32(); // TNL might be 0 if max level

            // Clamp EXP
            if (pet.Tnl > 0)
                pet.Exp = Math.Clamp(pet.Exp, 0, pet.Tnl);
            else
                pet.Exp = 0; // At max level, show 0/0 or similar

            // Server handles level up logic; client receives Packet_UpdatePlayerPet with new level/stats.
            // Client *could* play a level up visual/sound effect based on receiving this packet IF the EXP causes a "rollover",
            // but it's safer to trigger effects based on the actual level change from Packet_UpdatePlayerPet.

            // Update EXP bar UI
            // UI.PetWindow.UpdateExpBar();
            // UI.PetBar.UpdateExpBar();

            buffer.Dispose();
        }

        // --- NEW PACKET HANDLERS ---

        // Adds or updates a status effect on a pet
        public static void Packet_PetAddStatusEffect(ref byte[] data)
        {
             var buffer = new ByteStream(data);
             int playerIndex = buffer.ReadInt32();
             if (playerIndex < 0 || playerIndex >= Core.Type.Player.Length || Core.Type.Player == null) { buffer.Dispose(); return; }
             ref var pet = ref Core.Type.Player[playerIndex].Pet;
             if (pet.Num <= 0) { buffer.Dispose(); return; } // Ignore if no pet

             // Read status effect data (matches conceptual ActiveStatusEffect)
             // ActiveStatusEffect receivedEffect = new ActiveStatusEffect();
             // receivedEffect.StatusID = buffer.ReadInt32();
             // receivedEffect.RemainingDurationTicks = buffer.ReadInt32();
             // receivedEffect.Stacks = buffer.ReadByte();
             // receivedEffect.SourceID = buffer.ReadInt32(); // If sent by server
             // // Fetch icon/isBuff from status definitions based on StatusID
             // // receivedEffect.IconTextureID = GetStatusEffectDefinition(receivedEffect.StatusID).IconID;
             // // receivedEffect.IsBuff = GetStatusEffectDefinition(receivedEffect.StatusID).IsBuff;

             // if (pet.StatusEffects == null) pet.StatusEffects = new List<ActiveStatusEffect>();

             // // Find existing effect to update, or add new one
             // int existingIndex = pet.StatusEffects.FindIndex(e => e.StatusID == receivedEffect.StatusID);
             // if (existingIndex != -1) {
             //     pet.StatusEffects[existingIndex] = receivedEffect; // Update existing
             // } else {
             //     pet.StatusEffects.Add(receivedEffect); // Add new
             // }

             // // Optional: Sort effects (e.g., buffs first, shortest duration first)
             // // pet.StatusEffects.Sort(...);

             // If it's our pet or a visible pet, update UI/Visuals
             // if (playerIndex == GameState.MyIndex) {
             //     UI.PetStatusIcons.UpdateEffects();
             // }
             // // Potentially trigger visual effect particle/sound for applying the status

             buffer.Dispose();
        }

        // Removes a status effect from a pet
        public static void Packet_PetRemoveStatusEffect(ref byte[] data)
        {
             var buffer = new ByteStream(data);
             int playerIndex = buffer.ReadInt32();
             int statusIdToRemove = buffer.ReadInt32();
             if (playerIndex < 0 || playerIndex >= Core.Type.Player.Length || Core.Type.Player == null) { buffer.Dispose(); return; }
             ref var pet = ref Core.Type.Player[playerIndex].Pet;
             if (pet.Num <= 0 || pet.StatusEffects == null) { buffer.Dispose(); return; }

             // Remove the effect(s) with the matching ID
             // pet.StatusEffects.RemoveAll(e => e.StatusID == statusIdToRemove);

             // Update UI if relevant
             // if (playerIndex == GameState.MyIndex) {
             //     UI.PetStatusIcons.UpdateEffects();
             // }

             buffer.Dispose();
        }

        // Server authoritative update for a skill's cooldown end time
         public static void Packet_PetSkillCooldown(ref byte[] data)
         {
             var buffer = new ByteStream(data);
             int skillSlotIndex = buffer.ReadInt32();
             int cooldownEndTime = buffer.ReadInt32(); // Server sends absolute tick time when CD ends

             // Check if this packet is for the current player's pet (most likely)
             // If cooldowns are tracked per player/pet instance server-side, packet might need playerIndex

             if (skillSlotIndex >= 0 && skillSlotIndex < Constant.MAX_PET_SKILLS)
             {
                 if (PetSkillCD.Length > skillSlotIndex)
                 {
                     PetSkillCD[skillSlotIndex] = cooldownEndTime;
                     // Update UI for this skill slot to show the accurate cooldown timer
                     // UI.PetSkillBar.UpdateCooldown(skillSlotIndex, cooldownEndTime);
                 }
             }
             buffer.Dispose();
         }

        // Server update for loyalty and hunger (likely only sent to owner)
         public static void Packet_PetUpdateLoyaltyHunger(ref byte[] data)
         {
             var buffer = new ByteStream(data);
             int myIndex = GameState.MyIndex; // Assume only sent to owner

             if (myIndex < 0 || myIndex >= Core.Type.Player.Length || Core.Type.Player == null) { buffer.Dispose(); return; }
             ref var pet = ref Core.Type.Player[myIndex].Pet;
             if (pet.Num <= 0 || pet.Alive == 0) { buffer.Dispose(); return; } // Ignore if no pet/dead

             pet.Loyalty = buffer.ReadFloat();
             pet.Hunger = buffer.ReadFloat();

             // Clamp values
             pet.Loyalty = Math.Clamp(pet.Loyalty, 0f, MaxLoyalty);
             pet.Hunger = Math.Clamp(pet.Hunger, 0f, MaxHunger);

             // Update UI
             // UI.PetWindow.UpdateLoyaltyBar();
             // UI.PetWindow.UpdateHungerBar();
             // UI.PetBar.UpdateLoyaltyHungerIndicators();

             buffer.Dispose();
         }

        // Pet has died
        public static void Packet_PetDied(ref byte[] data)
        {
            var buffer = new ByteStream(data);
            int playerIndex = buffer.ReadInt32();
            if (playerIndex < 0 || playerIndex >= Core.Type.Player.Length || Core.Type.Player == null) { buffer.Dispose(); return; }

            ref var pet = ref Core.Type.Player[playerIndex].Pet;
            if (pet.Num <= 0) { buffer.Dispose(); return; } // Ignore if no pet

            pet.Alive = 0;
            pet.Health = 0;
            pet.Moving = (byte)Core.Enum.MovementType.None; // Stop movement
            pet.Attacking = 0; // Stop attacking
            pet.XOffset = 0; // Reset offsets
            pet.YOffset = 0;
            // Clear status effects on death? (Server decision)
            // pet.StatusEffects?.Clear();

            // Play death animation/sound? (Could be triggered by DrawPet when Alive == 0)
            // SoundManager.PlaySound(GetPetDeathSound(pet.Num));

            // If it's our pet, update UI significantly
            if (playerIndex == GameState.MyIndex)
            {
                // UI.PetWindow.ShowDeadState();
                // UI.PetSkillBar.DisableAll();
                // UIManager.ShowMessage($"{GetPetName(playerIndex)} has died!", MessageType.Warning);
            }

            buffer.Dispose();
        }

        // Pet has been revived (sent after player uses revive item/skill)
        public static void Packet_PetRevived(ref byte[] data)
        {
            var buffer = new ByteStream(data);
            int playerIndex = buffer.ReadInt32();
            if (playerIndex < 0 || playerIndex >= Core.Type.Player.Length || Core.Type.Player == null) { buffer.Dispose(); return; }

            ref var pet = ref Core.Type.Player[playerIndex].Pet;
            if (pet.Num <= 0) { buffer.Dispose(); return; } // Ignore if no pet was assigned

            pet.Alive = 1;
            // Server sends starting vitals after revive
            pet.MaxHp = buffer.ReadInt32();
            pet.Health = buffer.ReadInt32();
            pet.MaxMp = buffer.ReadInt32();
            pet.Mana = buffer.ReadInt32();

             // Clamp values
             pet.Health = Math.Clamp(pet.Health, 1, pet.MaxHp); // Ensure at least 1 HP on revive
             pet.Mana = Math.Clamp(pet.Mana, 0, pet.MaxMp);

            // Play revive animation/sound?
            // EffectManager.PlayEffectAt(EffectType.Revive, pet.X, pet.Y);
            // SoundManager.PlaySound(Sounds.PetRevive);

            // Update UI to show alive state
            if (playerIndex == GameState.MyIndex)
            {
                 // UI.PetWindow.ShowAliveState();
                 // UI.PetSkillBar.EnableAll(); // Cooldowns should be resent by server if applicable
                 // UIManager.ShowMessage($"{GetPetName(playerIndex)} has been revived!", MessageType.Info);
            }

            buffer.Dispose();
        }


         // Receives the list of pets owned by the player
         public static void Packet_ReceiveOwnedPetsList(ref byte[] data)
         {
             var buffer = new ByteStream(data);
             int count = buffer.ReadInt32();

             OwnedPets.Clear(); // Clear the existing cached list

             for (int i = 0; i < count; i++)
             {
                 OwnedPetSummary summary = new OwnedPetSummary();
                 summary.OwnedIndex = buffer.ReadInt32(); // Unique index IN THE PLAYER'S OWNED LIST
                 summary.UniqueID = buffer.ReadInt32();   // Pet's global unique instance ID
                 summary.PetTypeNum = buffer.ReadInt32(); // The type ID of the pet
                 summary.CustomName = buffer.ReadString();
                 summary.Level = buffer.ReadInt32();
                 summary.IsSummoned = buffer.ReadBoolean(); // Is this the currently summoned one?
                 summary.IsDead = buffer.ReadBoolean(); // Basic dead status from server

                 // Request static data for this pet type if we don't have it yet
                 RequestPetStaticDataIfNeeded(summary.PetTypeNum);

                 // Try to fill default name and sprite from cached static data immediately if available
                 if (summary.PetTypeNum > 0 && summary.PetTypeNum < Core.Type.Pet.Length && Core.Type.Pet != null && !string.IsNullOrEmpty(Core.Type.Pet[summary.PetTypeNum].Name))
                 {
                    summary.DefaultName = Core.Type.Pet[summary.PetTypeNum].Name;
                    summary.Sprite = Core.Type.Pet[summary.PetTypeNum].Sprite;
                 } else {
                    summary.DefaultName = "Loading..."; // Placeholder if static data not yet loaded
                    summary.Sprite = 0;
                 }


                 OwnedPets.Add(summary);
             }
             buffer.Dispose();

             // Update Pet Stable UI, Summoning UI etc.
             // UI.PetStableWindow.RefreshList(OwnedPets);
             // UI.SummonPetDialog.RefreshList(OwnedPets);
         }


        #endregion

        #region Movement (Refined)

        // Processes interpolation for smooth pet movement
        public static void ProcessPetMovement(int playerIndex)
        {
            if (playerIndex < 0 || playerIndex >= Core.Type.Player.Length || Core.Type.Player == null) return;
            ref var pet = ref Core.Type.Player[playerIndex].Pet;

            // Only process if alive and actually moving between tiles via interpolation
            if (pet.Alive == 0 || pet.Moving == (byte)Core.Enum.MovementType.None)
            {
                // If not moving, ensure offsets are zeroed (can help fix slight drifts)
                if (pet.XOffset != 0 || pet.YOffset != 0)
                {
                    pet.XOffset = 0;
                    pet.YOffset = 0;
                }
                 // If not moving, reset animation steps to idle (optional, depends on animation system)
                 // pet.Steps = 0; // Or specific idle frame index
                return;
            }

            // Calculate movement speed - should be dynamic based on pet stats/buffs/debuffs
            float currentMoveSpeedTilesPerSec = GetPetMoveSpeed(playerIndex); // Tiles per second

            // Calculate distance to move this frame in pixels
            double deltaMovePixels = (GameState.ElapsedTime / 1000.0) * currentMoveSpeedTilesPerSec * GameState.SizeX; // Assuming SizeX=SizeY (tile size)
            // If tiles aren't square, calculate separately for X and Y
            // int pixelMoveX = (int)Math.Round((GameState.ElapsedTime / 1000.0) * currentMoveSpeedTilesPerSec * GameState.SizeX);
            // int pixelMoveY = (int)Math.Round((GameState.ElapsedTime / 1000.0) * currentMoveSpeedTilesPerSec * GameState.SizeY);
            int pixelMove = (int)Math.Round(deltaMovePixels);


            bool finishedMoving = false;

            // Update offset based on direction - moving towards Zero offset
            switch ((Core.Enum.DirectionType)pet.Dir)
            {
                case Core.Enum.DirectionType.Up:
                    pet.YOffset -= pixelMove;
                    if (pet.YOffset <= 0) { pet.YOffset = 0; finishedMoving = true; }
                    break;
                case Core.Enum.DirectionType.Down:
                    pet.YOffset += pixelMove;
                    if (pet.YOffset >= 0) { pet.YOffset = 0; finishedMoving = true; }
                    break;
                case Core.Enum.DirectionType.Left:
                    pet.XOffset -= pixelMove;
                    if (pet.XOffset <= 0) { pet.XOffset = 0; finishedMoving = true; }
                    break;
                case Core.Enum.DirectionType.Right:
                    pet.XOffset += pixelMove;
                    if (pet.XOffset >= 0) { pet.XOffset = 0; finishedMoving = true; }
                    break;
                default:
                     // Invalid direction? Stop movement.
                     finishedMoving = true;
                     break;
            }

            // Check if completed walking over to the next tile
            if (finishedMoving)
            {
                pet.Moving = (byte)Core.Enum.MovementType.None; // Stop interpolation
                pet.XOffset = 0; // Ensure snapped to grid
                pet.YOffset = 0;

                // Cycle animation steps for next potential move, or reset to idle frame
                // This simple 1-2 cycle might need improvement for more frames
                pet.Steps = (pet.Steps == 1) ? (byte)2 : (byte)1;
            }
            else
            {
                 // Determine animation frame based on progress (optional, more complex)
                 // e.g., float progress = Math.Abs(pet.YOffset) / (float)GameState.SizeY;
                 // Calculate frame based on progress...

                 // Simpler: just keep cycling steps based on timer or distance remaining
                 // This part often links closely with DeterminePetAnimationFrame
            }
        }

        // Send request for pet to move towards a target coordinate (server handles pathfinding)
        public static void SendRequestPetMove(int targetX, int targetY)
        {
            if (GameState.MyIndex < 0 || GameState.MyIndex >= Core.Type.Player.Length || Core.Type.Player == null) return;
             ref var pet = ref Core.Type.Player[GameState.MyIndex].Pet;
             if (pet.Num <= 0 || pet.Alive == 0) return; // No pet or dead pet

            // Add client-side validation: Is target tile potentially reachable? Is it within leash range?
            // float dist = Vector2.Distance(new Vector2(pet.X, pet.Y), new Vector2(targetX, targetY));
            // if (!IsTileWalkable(targetX, targetY) || dist > PetLeashRange * 1.5f /* Allow some buffer */) return;

            var buffer = new ByteStream(4 + 4 + 4); // Header + TargetX + TargetY
            buffer.WriteInt32((int)Packets.ClientPackets.CPetMove); // Or CPetGotoXY
            buffer.WriteInt32(targetX);
            buffer.WriteInt32(targetY);
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        #endregion

        #region Drawing (Expanded & Refined)

        // Main function to draw a pet on the screen
        public static void DrawPet(int playerIndex)
        {
             if (!IsPetVisibleAndValid(playerIndex)) return; // Check if pet exists, is alive, has sprite etc.

             ref var pet = ref Core.Type.Player[playerIndex].Pet;
             // Static data should already be loaded by IsPetVisibleAndValid check if successful
             ref var petStaticData = ref Core.Type.Pet[pet.Num];

             int spriteNum = petStaticData.Sprite;
             // Ensure sprite index is valid for the graphics cache/system
             if (spriteNum <= 0 /*|| spriteNum >= MaxSpriteIndex */) return;

             // Determine Animation Frame (more robustly)
             byte animFrame = DeterminePetAnimationFrame(playerIndex);

             // Determine Sprite Sheet Row (Direction)
             int spriteRow = GetSpriteRowFromDirection(pet.Dir);

             // Get Sprite Dimensions (assuming a helper or cache exists)
             // This needs implementation based on how character graphics are handled.
             // Example using a hypothetical GameClient method:
             var spriteInfo = GameClient.GetCharacterGfxInfo(System.IO.Path.Combine(Path.Characters, spriteNum.ToString()));
             if (spriteInfo == null || spriteInfo.Width == 0 || spriteInfo.Height == 0)
             {
                  // Log error or draw placeholder if sprite info missing?
                  return; // Cannot draw without dimensions
             }

             // Assuming standard 4x4 sprite sheet (4 directions, 4 frames per direction: Idle, Walk1, Walk2/Attack1, Attack2)
             int frameWidth = spriteInfo.Width / 4;
             int frameHeight = spriteInfo.Height / 4;

             // Define source rectangle on the sprite sheet
             Rectangle sourceRect = new Rectangle(animFrame * frameWidth, spriteRow * frameHeight, frameWidth, frameHeight);

             // Calculate target screen position (incorporating map scroll, interpolation, centering)
             Point screenPos = CalculatePetScreenPosition(playerIndex, frameWidth, frameHeight);

             // Get pet's color tint (e.g., poisoned = green tint, frozen = blue tint)
             Color tint = GetPetColorTint(playerIndex);

             // --- Draw Pet Sprite ---
             // Assumes DrawCharacterSprite handles texture loading/caching based on spriteNum
             // Might need adjustments based on the actual drawing method signature
             GameClient.DrawCharacterSprite(spriteNum, screenPos.X, screenPos.Y, sourceRect, tint);

             // --- Draw Equipment Overlays (Optional) ---
             // This requires equipment items to have associated overlay sprites and positioning data
             DrawPetEquipment(playerIndex, screenPos, animFrame, spriteRow, frameWidth, frameHeight, tint); // Pass more info if needed

             // --- Draw Status Effect Icons Above Pet (Optional) ---
             DrawPetStatusIcons(playerIndex, screenPos, frameWidth);
        }

        // Helper to determine animation frame based on current state
        private static byte DeterminePetAnimationFrame(int playerIndex)
        {
             ref var pet = ref Core.Type.Player[playerIndex].Pet;

             // --- Attacking Animation ---
             if (pet.Attacking == 1 && pet.AttackTimer > 0)
             {
                 // Calculate attack speed (considering stats/buffs)
                 int attackAnimDuration = GetPetAttackAnimDuration(playerIndex); // Helper needed, e.g., 600ms

                 float attackProgress = (General.GetTickCount() - pet.AttackTimer) / (float)attackAnimDuration;

                 if (attackProgress >= 1.0f)
                 {
                     pet.Attacking = 0; // Attack animation finished
                     pet.AttackTimer = 0;
                     return 0; // Return to idle frame
                 }
                 else
                 {
                     // Example: Use frames 2 and 3 for attack. Frame 2 for first half, 3 for second.
                     // Adjust frame indices based on sprite sheet layout.
                     return (attackProgress < 0.5f) ? (byte)2 : (byte)3;
                 }
             }

             // --- Walking Animation ---
             if (pet.Moving == (byte)Core.Enum.MovementType.Walking && (pet.XOffset != 0 || pet.YOffset != 0))
             {
                 // Use Steps (1 or 2) for alternating walk frames (e.g., frame 1 and 2)
                 // Frame 0 is typically idle.
                 // This assumes ProcessPetMovement correctly cycles pet.Steps (1 <-> 2) when movement finishes.
                 return (pet.Steps == 1) ? (byte)1 : (byte)2; // Use frame 1 or 2 for walking
             }

             // --- Idle Animation ---
             // Could add slight idle variations based on timer (e.g., blink)
             // float idleTimer = (General.GetTickCount() % 2000) / 2000f; // Example 2-second cycle
             // if (idleTimer > 0.95f) return some_blink_frame;
             return 0; // Default to frame 0 for idle
        }

        // Helper to get sprite sheet row from direction (assuming standard layout)
        private static int GetSpriteRowFromDirection(int direction)
        {
            switch ((Core.Enum.DirectionType)direction)
            {
                case Core.Enum.DirectionType.Up: return 3;    // Top row on sheet
                case Core.Enum.DirectionType.Down: return 0;  // Bottom row on sheet
                case Core.Enum.DirectionType.Left: return 1;  // Second row
                case Core.Enum.DirectionType.Right: return 2; // Third row
                default: return 0; // Default to Down facing
            }
        }

        // Helper to calculate final screen position for drawing
        private static Point CalculatePetScreenPosition(int playerIndex, int frameWidth, int frameHeight)
        {
             ref var pet = ref Core.Type.Player[playerIndex].Pet;

             // Base position on map grid, converted to screen coords (Needs GameLogic.ConvertMapX/Y)
             int mapScreenX = GameLogic.ConvertMapX(pet.X * GameState.SizeX);
             int mapScreenY = GameLogic.ConvertMapY(pet.Y * GameState.SizeY);

             // Add interpolation offsets (already in screen pixel space)
             mapScreenX += pet.XOffset;
             mapScreenY += pet.YOffset;

             // Adjust based on sprite size relative to tile size to achieve desired alignment
             // Center horizontally, align bottom of sprite with bottom of logical tile position
             int drawX = mapScreenX + (GameState.SizeX / 2) - (frameWidth / 2);
             int drawY = mapScreenY + GameState.SizeY - frameHeight;
             // Add adjustments based on Pet SizeCategory if needed (e.g., raise small pets slightly)

             return new Point(drawX, drawY);
        }

        // Helper to get color tint based on status effects
        private static Color GetPetColorTint(int playerIndex)
        {
             ref var pet = ref Core.Type.Player[playerIndex].Pet;
             if (pet.StatusEffects == null || pet.StatusEffects.Count == 0) return Color.White;

             // Example: Prioritize certain effects for tinting
             // foreach (var effect in pet.StatusEffects)
             // {
             //     if (effect.StatusID == StatusEffectIDs.Poison) return Color.LimeGreen;
             //     if (effect.StatusID == StatusEffectIDs.Frozen) return Color.LightSkyBlue;
             //     if (effect.StatusID == StatusEffectIDs.Stun) return Color.Yellow;
             //     // ... add more effects
             // }

             return Color.White; // Default: No tint
        }

        // Placeholder for drawing equipped items on the pet
        private static void DrawPetEquipment(int playerIndex, Point baseScreenPos, byte animFrame, int spriteRow, int frameWidth, int frameHeight, Color tint)
        {
            ref var pet = ref Core.Type.Player[playerIndex].Pet;
            if (pet.Equipment == null) return;

            // Loop through equipment slots
            for (int i = 0; i < (int)PetEquipmentSlot.Count; i++)
            {
                int itemId = pet.Equipment[i];
                if (itemId <= 0) continue; // Slot empty

                // 1. Get Item's Equipment Sprite Info (needs data structure linking item ID to sprite sheet + offsets)
                // EquipmentSpriteInfo equipInfo = GetEquipmentSpriteInfo(itemId);
                // if (equipInfo == null || equipInfo.Texture == null) continue;

                // 2. Determine the correct source rect on the equipment sprite sheet
                // Assumes equipment sprites follow the same 4x4 layout as the base pet sprite
                // Rectangle equipSourceRect = new Rectangle(animFrame * equipInfo.FrameWidth, spriteRow * equipInfo.FrameHeight, equipInfo.FrameWidth, equipInfo.FrameHeight);

                // 3. Calculate the offset relative to baseScreenPos
                // This requires offset data per item/slot, potentially varying by pet animation frame/direction
                // Point equipOffset = GetEquipmentDrawOffset(itemId, (PetEquipmentSlot)i, animFrame, spriteRow);
                // Point drawPos = new Point(baseScreenPos.X + equipOffset.X, baseScreenPos.Y + equipOffset.Y);

                // 4. Draw the equipment sprite
                // GameClient.DrawSprite(equipInfo.Texture, drawPos.X, drawPos.Y, equipSourceRect, tint); // Apply same tint? Or maybe not?
            }
        }

        // Placeholder for drawing status icons near the pet (e.g., above head)
        private static void DrawPetStatusIcons(int playerIndex, Point baseScreenPos, int petFrameWidth)
        {
            ref var pet = ref Core.Type.Player[playerIndex].Pet;
            if (pet.StatusEffects == null || pet.StatusEffects.Count == 0) return;

            int iconSize = 16; // Example icon size
            int spacing = 2;
            int maxIcons = MaxPetStatusEffects; // Limit displayed icons

            // Calculate starting position (e.g., centered above the pet)
            int totalWidth = Math.Min(pet.StatusEffects.Count, maxIcons) * (iconSize + spacing) - spacing;
            int startX = baseScreenPos.X + (petFrameWidth / 2) - (totalWidth / 2);
            int startY = baseScreenPos.Y - iconSize - spacing; // Position above pet sprite

            int currentX = startX;
            int count = 0;

            // Sort effects? (e.g., Debuffs first, shortest duration?)
            // var sortedEffects = pet.StatusEffects.OrderBy(e => e.IsBuff).ThenBy(e => e.RemainingDurationTicks);

            //foreach (var effect in sortedEffects) // Iterate through sorted effects
             foreach (var effect in pet.StatusEffects) // Or just iterate in received order
             {
                 if (count >= maxIcons) break;

                 // 1. Get the icon texture for the status effect ID
                 // Texture2D iconTexture = GameClient.GetStatusEffectIcon(effect.StatusID); // Or use effect.IconTextureID if cached
                 // if (iconTexture == null) continue;

                 // 2. Draw the icon
                 // Rectangle destRect = new Rectangle(currentX, startY, iconSize, iconSize);
                 // GameClient.DrawSprite(iconTexture, destRect, null, Color.White); // null sourceRect for whole texture

                 // 3. Optionally draw duration/stacks text (small font) below or on the icon
                 // string overlayText = "";
                 // if (effect.Stacks > 1) overlayText = effect.Stacks.ToString();
                 // else if (effect.RemainingDurationTicks > 0) {
                 //     float seconds = effect.RemainingDurationTicks / 1000f;
                 //     overlayText = seconds < 10 ? $"{seconds:F1}" : $"{Math.Floor(seconds)}";
                 // }
                 // Text.RenderSmallText(overlayText, currentX, startY + iconSize, Color.White, Color.Black);

                 currentX += iconSize + spacing; // Move to next icon position
                 count++;
            }
        }


        // Draws the pet's name (and optionally level/owner) above it
        public static void DrawPlayerPetName(int playerIndex)
        {
            // Use helper check - includes checking if static data is loaded
            if (!IsPetVisibleAndValid(playerIndex)) return;

            ref var pet = ref Core.Type.Player[playerIndex].Pet;
            ref var petStaticData = ref Core.Type.Pet[pet.Num]; // Static data is available here

            // Determine the name to display (custom or default)
            string nameToShow = string.IsNullOrEmpty(pet.CustomName) ? petStaticData.Name : pet.CustomName;

            // Determine prefix/context (e.g., show owner name for other players' pets)
            string displayName;
            Color nameColor;
            if (playerIndex == GameState.MyIndex)
            {
                displayName = nameToShow;
                // Use a specific color for own pet? Or maybe based on loyalty?
                nameColor = Color.White; // Example: White for own pet
            }
            else
            {
                string ownerName = GetPlayerName(playerIndex); // Needs implementation (GetPlayerName helper)
                displayName = $"{ownerName}'s {nameToShow}";
                // Determine text color based on owner's status (PK, Admin, Guild, etc.)
                nameColor = GetPlayerNameColor(playerIndex); // Needs implementation (GetPlayerNameColor helper)
            }

            Color backColor = Color.Black; // Default outline/shadow color

            // Get sprite info for positioning
            var spriteInfo = GameClient.GetCharacterGfxInfo(System.IO.Path.Combine(Path.Characters, petStaticData.Sprite.ToString()));
            if (spriteInfo == null) return; // Cannot position name without sprite info
            int frameWidth = spriteInfo.Width / 4;
            int frameHeight = spriteInfo.Height / 4;

            // Calculate base position using the same helper as DrawPet
            Point petScreenPos = CalculatePetScreenPosition(playerIndex, frameWidth, frameHeight);

            // --- Draw Name ---
            int textWidth = Text.GetTextWidth(displayName); // Assuming Text rendering class exists
            int textX = petScreenPos.X + (frameWidth / 2) - (textWidth / 2); // Center above sprite center
            int textY = petScreenPos.Y - 18; // Position slightly above the sprite (adjust as needed)

            // Draw name using Text rendering system
            Text.RenderText(displayName, textX, textY, nameColor, backColor); // Assuming RenderText handles outline/shadow

            // --- Optionally draw Level next to name ---
            string levelText = $"Lv.{pet.Level}";
            int levelTextWidth = Text.GetTextWidth(levelText);
            // Draw level slightly to the right of the name
            Text.RenderText(levelText, textX + textWidth + 4, textY, Color.Khaki, backColor);

            // --- Optionally draw mini health bar below name (if not part of main UI or for other players) ---
             if (playerIndex != GameState.MyIndex || !UIManager.IsPetWindowVisible()) // Example condition
             {
                  // DrawMiniPetHealthBar(playerIndex, textX, textY + 15, textWidth + 4 + levelTextWidth); // Pass position and total width
             }
        }

        // --- Placeholder for drawing a mini health bar above the pet ---
        // private static void DrawMiniPetHealthBar(int playerIndex, int x, int y, int width)
        // {
        //     ref var pet = ref Core.Type.Player[playerIndex].Pet;
        //     if (pet.MaxHp <= 0) return;
        //
        //     float healthPercent = (float)pet.Health / pet.MaxHp;
        //     int barHeight = 5;
        //     int filledWidth = (int)(width * healthPercent);
        //
        //     // Draw Background
        //     Drawing.DrawRectangle(x - 1, y - 1, width + 2, barHeight + 2, Color.Black);
        //     // Draw Empty Bar
        //     Drawing.DrawRectangle(x, y, width, barHeight, Color.DarkRed);
        //     // Draw Filled Bar
        //     if (filledWidth > 0) Drawing.DrawRectangle(x, y, filledWidth, barHeight, Color.LimeGreen);
        // }


        #endregion

        #region Misc Helpers (Expanded & Refined)

        // Comprehensive check if pet data is valid and pet should be visible/interactive
        public static bool IsPetVisibleAndValid(int playerIndex) // Changed to public if needed elsewhere
        {
            if (playerIndex < 0 || playerIndex >= Core.Type.Player.Length || Core.Type.Player == null) return false;

            ref var pet = ref Core.Type.Player[playerIndex].Pet;
            if (pet.Num <= 0 || pet.Alive == 0) return false; // Not summoned or dead

            // Check if static data is loaded
            if (pet.Num >= Core.Type.Pet.Length || Core.Type.Pet == null || string.IsNullOrEmpty(Core.Type.Pet[pet.Num].Name))
            {
                // Request data if missing and return false for this frame (it won't be ready to draw/use)
                RequestPetStaticDataIfNeeded(pet.Num);
                return false;
            }

            // Add culling checks? Is the pet's map tile within the visible screen area?
            // if (!GameLogic.IsTileOnScreen(pet.X, pet.Y)) return false;

            // Add checks for invisibility status effects?
            // if (HasStatusEffect(playerIndex, StatusEffectIDs.Invisibility)) return false;

            return true; // All checks passed
        }

        // Simplified check for just being alive (used internally and potentially by AI/Logic)
        public static bool IsPetAlive(int playerIndex)
        {
            if (playerIndex < 0 || playerIndex >= Core.Type.Player.Length || Core.Type.Player == null) return false;
            // Check Num > 0 for valid pet type AND Alive == 1
            return Core.Type.Player[playerIndex].Pet.Num > 0 && Core.Type.Player[playerIndex].Pet.Alive == 1;
        }

        // Helper to get the displayed name (Custom or Default)
        public static string GetPetName(int playerIndex)
        {
             if (!IsPetVisibleAndValid(playerIndex)) return ""; // Or "Invalid Pet"

             ref var pet = ref Core.Type.Player[playerIndex].Pet;
             ref var petStaticData = ref Core.Type.Pet[pet.Num];

             return string.IsNullOrEmpty(pet.CustomName) ? petStaticData.Name : pet.CustomName;
        }


        // Helper to get calculated stat value (including buffs, equipment, talents, loyalty, hunger)
        public static int GetPetCalculatedStat(int playerIndex, Core.Enum.StatType stat)
        {
            // Use IsPetVisibleAndValid as it ensures static data is loaded too
            if (!IsPetVisibleAndValid(playerIndex)) return 0;

            ref var pet = ref Core.Type.Player[playerIndex].Pet;
            ref var petStatic = ref Core.Type.Pet[pet.Num];

            // Start with base stat (depends on how stats are stored - points allocated? or base value?)
            // Assuming pet.Stat[] holds the final calculated value *before* temporary effects are applied
            // OR it holds points allocated, and base value comes from petStatic.Stat[] + level ups.
            // Let's assume pet.Stat holds the value *including* level ups and allocated points for simplicity here.
            double modifiedStat = pet.Stat[(int)stat];

            // 1. Add equipment bonuses
            // foreach (int itemId in pet.Equipment) {
            //     if (itemId > 0) modifiedStat += GetItemStatBonus(itemId, stat); // Needs helper: lookup item stats
            // }

            // 2. Apply talent modifiers (can be additive or multiplicative)
            // if (pet.LearnedTalents != null) {
            //     foreach (var kvp in pet.LearnedTalents) {
            //         // GetTalentStatModifier needs complex lookup based on talent definition
            //         modifiedStat += GetTalentStatModifier(kvp.Key, kvp.Value, stat); // Additive example
            //         // modifiedStat *= GetTalentStatMultiplier(kvp.Key, kvp.Value, stat); // Multiplicative example
            //     }
            // }

            // 3. Apply status effect modifiers (positive and negative, additive and multiplicative)
            // if (pet.StatusEffects != null) {
            //     double additiveBonus = 0;
            //     double multiplicativeBonus = 1.0;
            //     foreach (var effect in pet.StatusEffects) {
            //         // GetStatusEffectStatModifier needs lookup based on status effect definition
            //         additiveBonus += GetStatusEffectStatModifier(effect.StatusID, effect.Stacks, stat);
            //         multiplicativeBonus *= GetStatusEffectStatMultiplier(effect.StatusID, effect.Stacks, stat);
            //     }
            //     modifiedStat = (modifiedStat + additiveBonus) * multiplicativeBonus;
            // }

            // 4. Apply loyalty modifier (example: +/- 10% bonus/penalty)
            // float loyaltyFactor = 1.0f + ((pet.Loyalty - 50.0f) / 50.0f) * 0.10f; // Linear from 0.9 to 1.1
            // modifiedStat *= loyaltyFactor;

            // 5. Apply hunger modifier (example: penalty when very hungry)
            // if (pet.Hunger < 10.0f) modifiedStat *= 0.8f; // 20% penalty if starving
            // else if (pet.Hunger < 30.0f) modifiedStat *= 0.95f; // 5% penalty if very hungry


            return (int)Math.Max(0, Math.Round(modifiedStat)); // Ensure stat doesn't go below 0 and round
        }

        // Helper to get current attack animation duration in milliseconds
        public static int GetPetAttackAnimDuration(int playerIndex)
        {
            if (!IsPetVisibleAndValid(playerIndex)) return PetAttackAnimBaseSpeed; // Default
            // Could potentially be modified by attack speed stats or specific skills/effects in future
            // For now, let's assume it's relatively constant per pet or uses a base value
            // Could also potentially fetch from petStatic data if defined there
            return PetAttackAnimBaseSpeed;
        }


        // Helper to get current attack speed (time between attack actions in milliseconds)
        public static int GetPetAttackSpeed(int playerIndex)
        {
             if (!IsPetVisibleAndValid(playerIndex)) return 2000; // Default: 2 seconds between attacks

             ref var pet = ref Core.Type.Player[playerIndex].Pet;
             ref var petStatic = ref Core.Type.Pet[pet.Num];

             // Start with base attack speed from static data (e.g., value in milliseconds)
             double baseSpeedMs = petStatic.BaseAttackSpeed > 100 ? petStatic.BaseAttackSpeed : 2000; // Default 2000ms if not set or invalid

             // Apply modifiers (e.g., from Agility stat, buffs/debuffs, equipment, talents)
             // Example: Haste buff reduces attack speed duration
             // double hasteMultiplier = 1.0;
             // if (HasStatusEffect(playerIndex, StatusEffectIDs.Haste)) hasteMultiplier *= 0.75; // 25% faster
             // if (HasStatusEffect(playerIndex, StatusEffectIDs.Slow)) hasteMultiplier *= 1.50; // 50% slower

             // Example: Stat influence (e.g., Agility reduces attack time)
             // int agility = GetPetCalculatedStat(playerIndex, Core.Enum.StatType.Agility);
             // double agilityFactor = 1.0 - (agility * 0.001); // Example: 0.1% reduction per point

             // Calculate final speed
             // double finalSpeedMs = baseSpeedMs * agilityFactor * hasteMultiplier;

             // Apply equipment/talent multipliers/additives...

             // Ensure minimum attack speed
             // return (int)Math.Max(200, Math.Round(finalSpeedMs)); // Minimum 200ms between attacks
             return (int)baseSpeedMs; // Placeholder: return base for now
        }

        // Helper to get current move speed in tiles per second
        public static float GetPetMoveSpeed(int playerIndex)
        {
            if (!IsPetVisibleAndValid(playerIndex)) return GameState.WalkSpeed; // Default to player walk speed

            ref var pet = ref Core.Type.Player[playerIndex].Pet;
            ref var petStatic = ref Core.Type.Pet[pet.Num];

            // Start with base move speed from static data (tiles per second)
            float baseSpeed = petStatic.BaseMoveSpeed > 0.1f ? petStatic.BaseMoveSpeed : GameState.WalkSpeed;

            // Apply modifiers from stats (e.g., Agility), buffs/debuffs (Haste/Slow), equipment, talents
            // double speedMultiplier = 1.0;
            // if (HasStatusEffect(playerIndex, StatusEffectIDs.Haste)) speedMultiplier *= 1.30; // 30% faster
            // if (HasStatusEffect(playerIndex, StatusEffectIDs.Slow)) speedMultiplier *= 0.60; // 40% slower
            // if (HasStatusEffect(playerIndex, StatusEffectIDs.Root)) return 0f; // Cannot move if rooted

            // int agility = GetPetCalculatedStat(playerIndex, Core.Enum.StatType.Agility);
            // float agilityFactor = 1.0f + (agility * 0.005f); // Example: 0.5% increase per point

            // Apply equipment/talent multipliers/additives...

            // float finalSpeed = baseSpeed * agilityFactor * (float)speedMultiplier;

             // Ensure minimum/maximum speed?
             // return Math.Clamp(finalSpeed, 0.5f, GameState.RunSpeed * 1.2f); // Example bounds
             return baseSpeed; // Placeholder: return base for now
        }

        // Placeholder for checking if target location is within allowed leash range of the owner
        private static bool IsWithinLeashRange(int targetX, int targetY)
        {
            if (GameState.MyIndex < 0 || GameState.MyIndex >= Core.Type.Player.Length || Core.Type.Player == null) return false;

            // Get owner position
            int ownerX = Core.Type.Player[GameState.MyIndex].X;
            int ownerY = Core.Type.Player[GameState.MyIndex].Y;

            // Calculate distance using Vector2 or simple Pythagorean theorem
            float deltaX = targetX - ownerX;
            float deltaY = targetY - ownerY;
            float distance = (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY);

            return distance <= PetLeashRange;
        }

        // --- TODO: Add helpers for Status Effects lookup/checking ---
        // private static bool HasStatusEffect(int playerIndex, int statusId) { ... }
        // private static StatusEffectDefinition GetStatusEffectDefinition(int statusId) { ... } // Needs cache/lookup

        // --- TODO: Add helpers for Talent lookup/checking ---
        // private static PetTalentData GetTalentData(int talentId) { ... } // Needs cache/lookup from static data
        // private static bool IsTalentLearnable(PlayerPetData petData, PetTalentData talentInfo) { ... } // Check level, prerequisites

        // --- TODO: Add helpers for Item lookup ---
        // private static ItemData GetItemData(int itemId) { ... } // Needs cache/lookup
        // private static float GetItemStatBonus(int itemId, Core.Enum.StatType stat) { ... }

        // --- TODO: Add helpers for Player Name/Color ---
         private static string GetPlayerName(int playerIndex) {
             if (playerIndex >= 0 && playerIndex < Core.Type.Player.Length && Core.Type.Player != null)
                 return Core.Type.Player[playerIndex].Name ?? "??"; // Handle null name
             return "Unknown";
         }
         private static Color GetPlayerNameColor(int playerIndex) {
             // Implement logic based on player status (PK, Admin, Guild relation, etc.)
             // Example:
             // if (GetPlayerAccess(playerIndex) >= AdminLevel.Admin) return Color.Yellow;
             // if (GetPlayerPK(playerIndex)) return Color.Red;
             return Color.White; // Default
         }

        #endregion
    }
}
