using Core;
using Microsoft.VisualBasic.CompilerServices; // Consider removing if not actually used for VB-specific functions
using Mirage.Sharp.Asfw; // Assumed networking/utility library
using Microsoft.Xna.Framework; // For Color, Rectangle
using Microsoft.Xna.Framework.Graphics; // For Texture2D (assuming GameClient uses it)
using System; // For Math
using System.Collections.Generic; // For Lists
using static Core.Global.Command; // Assuming this provides GetPlayerAccess, GetPlayerPK etc.
using Path = Core.Path; // Assuming Core.Path exists

namespace Client
{
    #region Enums (Consider moving to Core.Enum or a dedicated Enums file)

    public enum PetMovementStance
    {
        FollowClose,    // Stays very near the owner
        FollowMedium,   // Default follow distance
        FollowFar,      // Stays further back
        Stay            // Stays at the commanded position (within reason)
    }

    public enum PetCombatStance
    {
        Passive,        // Will not attack, flees if attacked (maybe)
        Defensive,      // Attacks only if owner or itself is attacked
        Aggressive,     // Attacks nearby hostiles on sight
        GuardOwner      // Prioritizes enemies attacking the owner
    }

    public enum PetTargetingPriority
    {
        Closest,
        LowestHealth,
        HighestHealth,
        HighestThreat, // If a threat system exists
        PlayersTarget,
        Self           // For support skills
    }

    public enum PetEquipmentSlot
    {
        Collar,
        Charm,
        Accessory1,
        Accessory2,
        Count // Keep this last for array sizing
    }

    #endregion

    public class Pet
    {
        #region Constants and Globals

        // UI Constants (Original)
        internal const byte PetbarTop = 2;
        internal const byte PetbarLeft = 2;
        internal const byte PetbarOffsetX = 4;
        internal const byte MaxPetbar = 7; // Might need more for new skills/commands
        internal const int PetHpBarWidth = 129;
        internal const int PetMpBarWidth = 129;
        internal const int PetExpBarWidth = 129; // Added for EXP bar
        internal const int PetLoyaltyBarWidth = 129; // Added for Loyalty
        internal const int PetHungerBarWidth = 129; // Added for Hunger

        // Skill Buffering (Original)
        public static double PetSkillBuffer; // Consider making this non-static if multiple pets can be controlled/buffered
        public static int PetSkillBufferTimer;
        public static int[] PetSkillCD = new int[Constant.MAX_PET_SKILLS]; // Keep MAX_PET_SKILLS updated

        // Behavior Constants (Original - Superseded by Enums, kept for potential legacy use or quick ref)
        internal const byte PetBehaviourFollow_Legacy = 0;
        internal const byte PetBehaviourGoto_Legacy = 1;
        internal const byte PetAttackBehaviourAttackonsight_Legacy = 2;
        internal const byte PetAttackBehaviourGuard_Legacy = 3;
        internal const byte PetAttackBehaviourDonothing_Legacy = 4;

        // New System Constants
        internal const int MaxPetTalentTiers = 5; // Example
        internal const int MaxTalentsPerTier = 3; // Example
        internal const int MaxPetInventorySlots = 4; // Example
        internal const int MaxPetStatusEffects = 10; // Max buffs/debuffs displayed/tracked client-side
        internal const int LoyaltyUpdateInterval = 60000; // How often loyalty might decay/update (example)
        internal const int HungerUpdateInterval = 30000; // How often hunger might decay (example)
        internal const float MaxLoyalty = 100.0f;
        internal const float MaxHunger = 100.0f;
        internal const int PetActionCooldown = 500; // General cooldown for pet actions like feeding/interacting

        #endregion

        #region Static Pet Data (Shared across all instances of a pet type) - Loaded from Server

        // Assumes Core.Type.PetStruct is expanded or replaced
        // Example Expansion of Core.Type.PetStruct (Conceptual - definition is in Core project)
        /*
        public struct PetStruct {
            public int Num;
            public string Name;
            public int Sprite;
            public int Range;
            public int Level; // Base level? Or current? Clarify usage.
            public int MaxLevel;
            public int ExpGain; // Base XP gain modifier?
            public int LevelPnts; // Points gained per level
            public byte StatType; // Base stat growth type?
            public byte LevelingType; // EXP curve type?
            public byte[] Stat = new byte[(int)Core.Enum.StatType.Count]; // Base stats? Or points allocated? Clarify.
            public int[] Skill = new int[Constant.MAX_PET_SKILLS]; // Base learnable skills?
            public byte Evolvable;
            public int EvolveLevel;
            public int EvolveNum; // Target pet num after evolution

            // --- NEW FIELDS ---
            public List<int> LearnableSkills; // All skills this pet type *can* learn
            public List<PetTalentData> AvailableTalents; // Definition of talents for this pet type
            public PetEquipmentSlot[] AllowedEquipmentSlots; // Which slots this pet type can use
            public float BaseMoveSpeed;
            public float BaseAttackSpeed;
            public string Description;
            public int FoodItemPreference; // ID of preferred food for extra loyalty/hunger gain
            public int SizeCategory; // For collision/rendering adjustments
            public List<int> PossibleEvolutions; // Support for branching evolutions
            // ... other static data
        }
        */

        // Example Talent Data Structure (Conceptual)
        /*
        public struct PetTalentData {
            public int ID;
            public string Name;
            public string Description;
            public int Tier;
            public int MaxPoints;
            public List<int> PrerequisiteTalentIDs;
            public StatModifier[] StatModifiers; // Effects of the talent
            public int GrantsSkillID; // If the talent grants a skill
        }
        */


        #endregion

        #region Active Pet Data (Instance specific, often part of Player data) - Updated by Server

        // Assumes Core.Type.Player[n].Pet is expanded
        // Example Expansion of Player's Pet Data (Conceptual - definition is in Core project)
        /*
         public class PlayerPetData {
            public int Num; // ID of the Pet Type (links to PetStruct)
            public int UniqueID; // Unique instance ID if pets are not just types (e.g., for trading)
            public string CustomName; // Player-given name override
            public int Health;
            public int MaxHp;
            public int Mana;
            public int MaxMp;
            public int Level;
            public int Exp;
            public int Tnl; // To Next Level Exp
            public byte[] Stat = new byte[(int)Core.Enum.StatType.Count]; // Current allocated stats
            public int[] Skill = new int[Constant.MAX_PET_SKILLS]; // Currently learned/equipped skills
            public int X;
            public int Y;
            public int Dir;
            public int XOffset;
            public int YOffset;
            public byte Moving; // (byte)Core.Enum.MovementType
            public byte Steps; // Animation frame step
            public byte Alive; // 1 = alive, 0 = dead
            public int AttackBehaviour; // Use PetCombatStance enum
            public int Points; // Unspent stat points
            public byte Attacking;
            public int AttackTimer;

            // --- NEW FIELDS ---
            public PetMovementStance MovementStance;
            public PetCombatStance CombatStance;
            public PetTargetingPriority TargetingPriority;
            public int[] Equipment = new int[(int)PetEquipmentSlot.Count]; // Item IDs equipped
            public List<ActiveStatusEffect> StatusEffects; // List of current buffs/debuffs
            public float Loyalty; // 0-100
            public float Hunger; // 0-100
            public Dictionary<int, int> LearnedTalents; // Key: TalentID, Value: Points invested
            public int UnspentTalentPoints;
            public int[] Inventory = new int[MaxPetInventorySlots]; // Item IDs carried by pet
            public int TargetIndex; // Index of the current target (player, NPC, etc.)
            public int TargetType; // Enum: TargetIsPlayer, TargetIsNPC etc.
            public int LastFedTimestamp;
            public int LastLoyaltyTickTimestamp;
            public int OwnerPlayerIndex; // Index of the player who owns this pet
         }
         */

        // Helper struct for active status effects (Conceptual - define in Core project)
        /*
        public struct ActiveStatusEffect {
            public int StatusID; // Links to a definition of the status effect
            public int RemainingDurationTicks; // Time left in game ticks or ms
            public int SourceID; // Who applied it
            public int Stacks; // If the effect can stack
        }
        */

        // Local state (Client only)
        private static Dictionary<int, bool> PetDataRequested = new Dictionary<int, bool>(); // Track requests to prevent spamming
        private static int lastPetActionTime = 0;

        #endregion

        #region Database / Data Management (Modified)

        // Clears the static data cache for a specific pet type index
        public static void ClearPetStaticData(int petTypeIndex)
        {
            if (petTypeIndex < 0 || petTypeIndex >= Core.Type.Pet.Length) return;

            Core.Type.Pet[petTypeIndex] = default; // Assumes default clears it reasonably
            Core.Type.Pet[petTypeIndex].Name = "";
            Core.Type.Pet[petTypeIndex].Stat = new byte[(int)Core.Enum.StatType.Count];
            Core.Type.Pet[petTypeIndex].Skill = new int[Constant.MAX_PET_SKILLS];
             // NEW: Clear learnable skills, talents etc. if they are lists/dictionaries
            // Core.Type.Pet[petTypeIndex].LearnableSkills?.Clear();
            // Core.Type.Pet[petTypeIndex].AvailableTalents?.Clear();
            // Core.Type.Pet[petTypeIndex].PossibleEvolutions?.Clear();

            for (int i = 0; i < Constant.MAX_PET_SKILLS; i++)
                Core.Type.Pet[petTypeIndex].Skill[i] = -1; // Assuming -1 indicates no skill

             PetDataRequested.Remove(petTypeIndex); // Allow requesting again
        }

        // Clears all static pet data cache
        public static void ClearAllPetStaticData()
        {
            Core.Type.Pet = new Core.Type.PetStruct[Constant.MAX_PETS]; // Reinitialize array
            PetSkillCD = new int[Constant.MAX_PET_SKILLS]; // Reset cooldowns array

            for (int i = 0; i < Constant.MAX_PETS; i++)
                ClearPetStaticData(i); // Clear each entry

            PetSkillBuffer = -1; // Reset skill buffer
            PetSkillBufferTimer = 0;
            PetDataRequested.Clear(); // Clear request tracking
        }

        // Requests static data for a specific pet type if not already loaded/requested
        public static void RequestPetStaticDataIfNeeded(int petTypeIndex)
        {
            if (petTypeIndex < 0 || petTypeIndex >= Core.Type.Pet.Length) return;

            // Check if data exists (using Name as a proxy for loaded data) OR if already requested
            if (string.IsNullOrEmpty(Core.Type.Pet[petTypeIndex].Name) && !PetDataRequested.GetValueOrDefault(petTypeIndex, false))
            {
                PetDataRequested[petTypeIndex] = true; // Mark as requested
                SendRequestPetStaticData(petTypeIndex); // Send network request
            }
        }

        // Original StreamPet - adapted slightly to use the new request logic
        // This seems intended to load the *static* data for a player's *active* pet type.
        public static void StreamPlayerActivePetData(int playerIndex)
        {
             if (playerIndex < 0 || playerIndex >= Core.Type.Player.Length) return;
             int petTypeIndex = Core.Type.Player[playerIndex].Pet.Num;
             RequestPetStaticDataIfNeeded(petTypeIndex);
        }

        #endregion

        #region Outgoing Packets (Expanded)

        // Renamed for clarity - requests STATIC data for a pet type
        public static void SendRequestPetStaticData(int petTypeIndex)
        {
            var buffer = new ByteStream(4 + 4); // Header + Int32
            buffer.WriteInt32((int)Packets.ClientPackets.CRequestPetData); // Use a dedicated packet ID
            buffer.WriteInt32(petTypeIndex);
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        // Original SendRequestPet - Assuming this requests the player's active pet *instance* data update? Renamed for clarity.
        // OR maybe it requests the list of pets the player owns? The name is ambiguous.
        // Let's assume it forces an update of the currently summoned pet for the player sending it.
        public static void SendRequestMySummonedPetUpdate()
        {
            var buffer = new ByteStream(4);
            buffer.WriteInt32((int)Packets.ClientPackets.CRequestMyPetUpdate); // New, clearer packet ID
            // No PetNum needed if it's always the player's currently summoned pet
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }


        // Original SendPetBehaviour - updated to use enums
        public static void SendSetPetStance(PetMovementStance moveStance, PetCombatStance combatStance, PetTargetingPriority targetPriority)
        {
            var buffer = new ByteStream(4 + 1 + 1 + 1); // Header + 3 bytes (enums)
            buffer.WriteInt32((int)Packets.ClientPackets.CSetPetStance); // New, clearer packet ID
            buffer.WriteByte((byte)moveStance);
            buffer.WriteByte((byte)combatStance);
            buffer.WriteByte((byte)targetPriority);
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
            // Client-side prediction (optional, update local player's pet data immediately)
            // Core.Type.Player[GameState.MyIndex].Pet.MovementStance = moveStance;
            // Core.Type.Player[GameState.MyIndex].Pet.CombatStance = combatStance;
            // Core.Type.Player[GameState.MyIndex].Pet.TargetingPriority = targetPriority;
        }

        // Original SendTrainPetStat
        public static void SendTrainPetStat(Core.Enum.StatType statNum) // Use the actual Enum type
        {
            if (Core.Type.Player[GameState.MyIndex].Pet.Points <= 0)
            {
                 // Optional: Show message "No stat points available"
                 return;
            }
            var buffer = new ByteStream(4 + 1); // Header + Byte (enum)
            buffer.WriteInt32((int)Packets.ClientPackets.CPetUseStatPoint);
            buffer.WriteByte((byte)statNum); // Send the byte value
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
             // Optional: Client-side prediction (decrement points, visually update stat)
             // Core.Type.Player[GameState.MyIndex].Pet.Points--;
             // Core.Type.Player[GameState.MyIndex].Pet.Stat[(int)statNum]++; // Or however stats increase
        }

        // Original SendRequestPets - Assuming this requests the list of *all* pets the player owns (not just static data)
        public static void SendRequestOwnedPetsList()
        {
            var buffer = new ByteStream(4);
            buffer.WriteInt32((int)Packets.ClientPackets.CRequestOwnedPets); // Clearer Packet ID
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        // Original SendUsePetSkill
        public static void SendUsePetSkill(int skillSlotIndex) // Use slot index for clarity
        {
            if (skillSlotIndex < 0 || skillSlotIndex >= Constant.MAX_PET_SKILLS) return;

            int myIndex = GameState.MyIndex;
            int skillId = Core.Type.Player[myIndex].Pet.Skill[skillSlotIndex];
            if (skillId <= 0) { /* Optional: Message "No skill in this slot" */ return; }

            // Check client-side cooldown
            if (PetSkillCD[skillSlotIndex] > General.GetTickCount())
            {
                // Optional: Message "Skill on cooldown"
                // Calculate remaining CD: (PetSkillCD[skillSlotIndex] - General.GetTickCount()) / 1000.0f seconds
                return;
            }
            // Check mana cost (requires skill definitions)
            // if (Core.Type.Player[myIndex].Pet.Mana < GetSkillManaCost(skillId)) { /* Optional: Message "Not enough mana" */ return; }

             // Check existing buffer
             if (PetSkillBuffer != -1 && PetSkillBufferTimer + Constant.NETWORK_BUFFER_TIMEOUT > General.GetTickCount())
             {
                // Optional: Message "Another skill is being cast"
                return;
             }

            var buffer = new ByteStream(4 + 4); // Header + Int32
            buffer.WriteInt32((int)Packets.ClientPackets.CPetUseSkill); // Keep original or rename
            buffer.WriteInt32(skillSlotIndex); // Send slot index
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();

            // Set client-side buffer and cooldown (server will confirm/override)
            PetSkillBuffer = skillSlotIndex; // Buffer the slot index
            PetSkillBufferTimer = General.GetTickCount();
            // Assuming cooldown data comes from skill definition (needs access here)
            // PetSkillCD[skillSlotIndex] = General.GetTickCount() + GetSkillCooldownDuration(skillId);
        }

        // Original SendSummonPet - Needs Pet ID/Index to summon
        public static void SendSummonPet(int ownedPetIndex) // Index in the player's list of owned pets
        {
            var buffer = new ByteStream(4 + 4); // Header + Int32
            buffer.WriteInt32((int)Packets.ClientPackets.CSummonPet);
            buffer.WriteInt32(ownedPetIndex);
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        // Original SendReleasePet (Unsummon)
        public static void SendUnsummonPet()
        {
            // Check if a pet is actually summoned
            if (Core.Type.Player[GameState.MyIndex].Pet.Num <= 0 || Core.Type.Player[GameState.MyIndex].Pet.Alive == 0) return;

            var buffer = new ByteStream(4);
            buffer.WriteInt32((int)Packets.ClientPackets.CUnsummonPet); // Renamed from CReleasePet for clarity
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        // --- NEW --- Permanently release/delete a pet
        public static void SendDeleteOwnedPet(int ownedPetIndex)
        {
             // Add confirmation dialog here! "Are you sure you want to release [Pet Name] forever?"
             var buffer = new ByteStream(4 + 4);
             buffer.WriteInt32((int)Packets.ClientPackets.CDeleteOwnedPet); // New Packet ID
             buffer.WriteInt32(ownedPetIndex);
             NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
             buffer.Dispose();
        }


        // Original SendRequestEditPet - Assuming this opens an editor UI?
        // If it means sending data *from* an editor, see SendSavePetData
        public static void SendRequestEnterPetEditMode(int petTypeIndex) // Maybe request edit for a specific type
        {
            ByteStream buffer = new ByteStream(4 + 4);
            buffer.WriteInt32((int)Packets.ClientPackets.CRequestEnterPetEditMode); // Clearer Packet ID
            buffer.WriteInt32(petTypeIndex);
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        // Original SendSavePet - Renamed, assuming it saves STATIC data from an editor
        public static void SendSavePetStaticData(int petTypeIndex)
        {
             // Permissions check should happen server-side, but could add client check
             // if (GetPlayerAccess(GameState.MyIndex) < AdminLevel) return;

            if (petTypeIndex < 0 || petTypeIndex >= Core.Type.Pet.Length) return;

            ByteStream buffer = new ByteStream(1024); // Increased buffer size for potentially more data
            buffer.WriteInt32((int)Packets.ClientPackets.CSavePetData); // Clearer Packet ID
            buffer.WriteInt32(petTypeIndex); // Use Int32, consistent with reads

            // Use the PetStruct definition assumed earlier
            ref var petData = ref Core.Type.Pet[petTypeIndex];
            buffer.WriteInt32(petData.Num); // Num should match petTypeIndex?
            buffer.WriteString(petData.Name ?? ""); // Null check
            buffer.WriteInt32(petData.Sprite);
            buffer.WriteInt32(petData.Range);
            // buffer.WriteInt32(petData.Level); // Base Level? Clarify what this means for static data
            buffer.WriteInt32(petData.MaxLevel);
            // buffer.WriteInt32(petData.ExpGain); // Base EXP gain modifier?
            buffer.WriteInt32(petData.LevelPnts); // Points per level
            buffer.WriteByte(petData.StatType); // Growth type enum?
            buffer.WriteByte(petData.LevelingType); // Level curve enum?

            // Base Stats? Or Points? Needs clarification. Assuming Base Stats for static data.
            for (int i = 0; i < (int)Core.Enum.StatType.Count; i++)
                buffer.WriteInt32(petData.Stat[i]); // Send as Int32 if that's how it's read

            // Base Learnable Skills? Needs clarification. Sending the current Skill array for now.
            for (int i = 0; i < Core.Constant.MAX_PET_SKILLS; i++)
                buffer.WriteInt32(petData.Skill[i]); // Send as Int32

            buffer.WriteByte(petData.Evolvable);
            buffer.WriteInt32(petData.EvolveLevel);
            buffer.WriteInt32(petData.EvolveNum);

            // --- NEW FIELDS ---
            // buffer.WriteFloat(petData.BaseMoveSpeed);
            // buffer.WriteFloat(petData.BaseAttackSpeed);
            // buffer.WriteString(petData.Description ?? "");
            // buffer.WriteInt32(petData.FoodItemPreference);
            // buffer.WriteInt32(petData.SizeCategory);
            // Write List<int> LearnableSkills
            // Write List<PetTalentData> AvailableTalents (complex serialization)
            // Write PetEquipmentSlot[] AllowedEquipmentSlots
            // Write List<int> PossibleEvolutions

            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        // --- NEW PACKETS ---

        public static void SendPetEquipItem(int inventorySlot, PetEquipmentSlot petSlot)
        {
            var buffer = new ByteStream(4 + 4 + 1); // Header + InvSlot + PetSlot (byte)
            buffer.WriteInt32((int)Packets.ClientPackets.CPetEquipItem);
            buffer.WriteInt32(inventorySlot);
            buffer.WriteByte((byte)petSlot);
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        public static void SendPetUnequipItem(PetEquipmentSlot petSlot)
        {
            var buffer = new ByteStream(4 + 1); // Header + PetSlot (byte)
            buffer.WriteInt32((int)Packets.ClientPackets.CPetUnequipItem);
            buffer.WriteByte((byte)petSlot);
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

         public static void SendPetLearnTalent(int talentId)
         {
             // Client-side checks (optional)
             // if (Core.Type.Player[GameState.MyIndex].Pet.UnspentTalentPoints <= 0) return;
             // if (!IsTalentLearnable(talentId)) return; // Check prerequisites, etc.

             var buffer = new ByteStream(4 + 4); // Header + TalentID
             buffer.WriteInt32((int)Packets.ClientPackets.CPetLearnTalent);
             buffer.WriteInt32(talentId);
             NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
             buffer.Dispose();
         }

         public static void SendPetFeed(int inventorySlot)
         {
              // Basic client-side cooldown to prevent spam
             if (lastPetActionTime + PetActionCooldown > General.GetTickCount()) return;

             var buffer = new ByteStream(4 + 4); // Header + InvSlot
             buffer.WriteInt32((int)Packets.ClientPackets.CPetFeed);
             buffer.WriteInt32(inventorySlot);
             NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
             buffer.Dispose();
             lastPetActionTime = General.GetTickCount();
         }

         public static void SendPetRename(string newName)
         {
            if (string.IsNullOrWhiteSpace(newName) || newName.Length > Constant.MAX_NAME_LENGTH)
            {
                // Optional: Message "Invalid name."
                return;
            }
             var buffer = new ByteStream(4 + newName.Length * 2 + 4); // Header + String
             buffer.WriteInt32((int)Packets.ClientPackets.CPetRename);
             buffer.WriteString(newName);
             NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
             buffer.Dispose();
         }

         public static void SendPetDirectCommand(int commandType, int targetIndex, int targetType) // e.g., Command: Attack, Target: NPC index 5
         {
             var buffer = new ByteStream(4 + 4 + 4 + 4); // Header + Command + TargetIdx + TargetType
             buffer.WriteInt32((int)Packets.ClientPackets.CPetDirectCommand);
             buffer.WriteInt32(commandType); // Define command enums (AttackTarget, MoveTo, GuardTarget)
             buffer.WriteInt32(targetIndex);
             buffer.WriteInt32(targetType); // Define target type enums (Player, NPC, MapPoint)
             NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
             buffer.Dispose();
         }

        #endregion

        #region Incoming Packets (Expanded & Refined)

        // Existing packet, updated to read new fields based on conceptual PlayerPetData
        public static void Packet_UpdatePlayerPet(ref byte[] data)
        {
            var buffer = new ByteStream(data);
            int playerIndex = buffer.ReadInt32();

            if (playerIndex < 0 || playerIndex >= Core.Type.Player.Length)
            {
                buffer.Dispose();
                return; // Invalid index
            }

            ref var petData = ref Core.Type.Player[playerIndex].Pet;

            petData.Num = buffer.ReadInt32();
            // petData.UniqueID = buffer.ReadInt32(); // If unique IDs are added
            // petData.CustomName = buffer.ReadString(); // If custom names are added

            petData.Health = buffer.ReadInt32();
            petData.Mana = buffer.ReadInt32();
            petData.Level = buffer.ReadInt32();

            for (int i = 0; i < (int)Core.Enum.StatType.Count; i++)
                petData.Stat[i] = buffer.ReadByte(); // Read as Byte

            for (int i = 0; i < Core.Constant.MAX_PET_SKILLS; i++)
                petData.Skill[i] = buffer.ReadInt32(); // Current skills

            petData.X = buffer.ReadInt32();
            petData.Y = buffer.ReadInt32();
            petData.Dir = buffer.ReadInt32();

            petData.MaxHp = buffer.ReadInt32();
            petData.MaxMp = buffer.ReadInt32();

            petData.Alive = buffer.ReadByte(); // Read as Byte

            petData.AttackBehaviour = buffer.ReadInt32(); // Consider reading byte and casting to enum
            // petData.CombatStance = (PetCombatStance)buffer.ReadByte(); // Read new stances
            // petData.MovementStance = (PetMovementStance)buffer.ReadByte();
            // petData.TargetingPriority = (PetTargetingPriority)buffer.ReadByte();

            petData.Points = buffer.ReadInt32(); // Unspent stat points
            petData.Exp = buffer.ReadInt32();
            petData.Tnl = buffer.ReadInt32();

            // --- Read NEW Fields ---
            petData.Loyalty = buffer.ReadFloat();
            petData.Hunger = buffer.ReadFloat();
            petData.UnspentTalentPoints = buffer.ReadInt32();

            // Equipment
            for(int i = 0; i < (int)PetEquipmentSlot.Count; i++)
            {
                petData.Equipment[i] = buffer.ReadInt32(); // Item ID or 0
            }

            // Status Effects (Example: count followed by data)
            // int statusCount = buffer.ReadByte();
            // petData.StatusEffects = new List<ActiveStatusEffect>(statusCount);
            // for(int i = 0; i < statusCount; i++)
            // {
            //     ActiveStatusEffect effect = new ActiveStatusEffect();
            //     effect.StatusID = buffer.ReadInt32();
            //     effect.RemainingDurationTicks = buffer.ReadInt32();
            //     effect.Stacks = buffer.ReadByte();
            //     petData.StatusEffects.Add(effect);
            // }

            // Learned Talents (Example: count followed by key-value pairs)
            // int talentCount = buffer.ReadByte();
            // petData.LearnedTalents = new Dictionary<int, int>(talentCount);
            // for(int i = 0; i < talentCount; i++)
            // {
            //     int talentId = buffer.ReadInt32();
            //     int points = buffer.ReadByte();
            //     petData.LearnedTalents[talentId] = points;
            // }

            // Pet Inventory (Example: fixed size array)
            // for(int i = 0; i < MaxPetInventorySlots; i++)
            // {
            //     petData.Inventory[i] = buffer.ReadInt32(); // Item ID or 0
            // }


            buffer.Dispose();

            // After receiving data, ensure the static data for this pet type is loaded
            if (playerIndex == GameState.MyIndex && petData.Num > 0 && petData.Alive == 1)
            {
                 RequestPetStaticDataIfNeeded(petData.Num);
                 // Potentially update pet UI elements here
                 // UI.PetWindow.UpdateDisplay();
            }
        }

        // Existing packet, updated to read new fields based on conceptual PetStruct
        public static void Packet_ReceivePetStaticData(ref byte[] data) // Renamed from Packet_UpdatePet
        {
            var buffer = new ByteStream(data);
            int petTypeIndex = buffer.ReadInt32();

             if (petTypeIndex < 0 || petTypeIndex >= Core.Type.Pet.Length)
            {
                buffer.Dispose();
                PetDataRequested.Remove(petTypeIndex); // Remove invalid request flag
                return; // Invalid index
            }

            ref var petData = ref Core.Type.Pet[petTypeIndex];

            petData.Num = buffer.ReadInt32(); // Should match petTypeIndex
            petData.Name = buffer.ReadString();
            petData.Sprite = buffer.ReadInt32();
            petData.Range = buffer.ReadInt32();
            // petData.Level = buffer.ReadInt32(); // Base Level?
            petData.MaxLevel = buffer.ReadInt32();
            // petData.ExpGain = buffer.ReadInt32(); // Base EXP modifier?
            petData.LevelPnts = buffer.ReadInt32(); // Points per level
            petData.StatType = buffer.ReadByte(); // Growth Type
            petData.LevelingType = buffer.ReadByte(); // Level Curve Type

            for (int i = 0; i < (int)Core.Enum.StatType.Count; i++)
                petData.Stat[i] = buffer.ReadByte(); // Base Stats? Reading as Byte

            for (int i = 0; i < Core.Constant.MAX_PET_SKILLS; i++) // Use constant
                petData.Skill[i] = buffer.ReadInt32(); // Default skills?

            petData.Evolvable = buffer.ReadByte();
            petData.EvolveLevel = buffer.ReadInt32();
            petData.EvolveNum = buffer.ReadInt32();

            // --- Read NEW static fields ---
            // petData.BaseMoveSpeed = buffer.ReadFloat();
            // petData.BaseAttackSpeed = buffer.ReadFloat();
            // petData.Description = buffer.ReadString();
            // petData.FoodItemPreference = buffer.ReadInt32();
            // petData.SizeCategory = buffer.ReadInt32();

            // Read Lists/Arrays (Example for Learnable Skills)
            // int learnableSkillCount = buffer.ReadInt32();
            // petData.LearnableSkills = new List<int>(learnableSkillCount);
            // for(int i = 0; i < learnableSkillCount; i++)
            // {
            //     petData.LearnableSkills.Add(buffer.ReadInt32());
            // }

            // Read Allowed Equipment Slots (Example)
            // int allowedSlotCount = buffer.ReadByte();
            // petData.AllowedEquipmentSlots = new PetEquipmentSlot[allowedSlotCount];
            // for(int i = 0; i < allowedSlotCount; i++)
            // {
            //    petData.AllowedEquipmentSlots[i] = (PetEquipmentSlot)buffer.ReadByte();
            // }

            // Complex data like talents would require careful serialization/deserialization

            buffer.Dispose();
            PetDataRequested[petTypeIndex] = false; // Mark as loaded (or failed if name is still empty)
        }


        // Original movement packet - No changes needed unless speed varies
        public static void Packet_PetMove(ref byte[] data)
        {
            var buffer = new ByteStream(data);
            int playerIndex = buffer.ReadInt32();
            int x = buffer.ReadInt32();
            int y = buffer.ReadInt32();
            int dir = buffer.ReadInt32();
            int movement = buffer.ReadInt32(); // Movement Type

             if (playerIndex < 0 || playerIndex >= Core.Type.Player.Length) { buffer.Dispose(); return; }

            ref var pet = ref Core.Type.Player[playerIndex].Pet;
            pet.X = x;
            pet.Y = y;
            pet.Dir = dir;
            pet.XOffset = 0;
            pet.YOffset = 0;
            pet.Moving = (byte)movement;

            // Set starting offset for interpolation based on direction
            switch ((Core.Enum.DirectionType)pet.Dir) // Cast to Enum for clarity
            {
                case Core.Enum.DirectionType.Up:
                    pet.YOffset = GameState.PicY; // Assuming PicY is tile height
                    break;
                case Core.Enum.DirectionType.Down:
                    pet.YOffset = -GameState.PicY;
                    break;
                case Core.Enum.DirectionType.Left:
                    pet.XOffset = GameState.PicX; // Assuming PicX is tile width
                    break;
                case Core.Enum.DirectionType.Right:
                    pet.XOffset = -GameState.PicX;
                    break;
            }
            buffer.Dispose();
        }

        // Original direction packet - No changes needed
        public static void Packet_PetDir(ref byte[] data)
        {
            var buffer = new ByteStream(data);
            int playerIndex = buffer.ReadInt32();
            int dir = buffer.ReadInt32();

            if (playerIndex >= 0 && playerIndex < Core.Type.Player.Length)
            {
                Core.Type.Player[playerIndex].Pet.Dir = dir;
            }
            buffer.Dispose();
        }

        // Original vital packet - Updated variable names for clarity
        public static void Packet_PetVital(ref byte[] data)
        {
            var buffer = new ByteStream(data);
            int playerIndex = buffer.ReadInt32();
            int vitalType = buffer.ReadInt32(); // 1 for HP, 0 for MP (or use enum)

            if (playerIndex < 0 || playerIndex >= Core.Type.Player.Length) { buffer.Dispose(); return; }

            ref var pet = ref Core.Type.Player[playerIndex].Pet;

            if (vitalType == 1) // HP Update
            {
                pet.MaxHp = buffer.ReadInt32();
                pet.Health = buffer.ReadInt32();
            }
            else // MP Update (assuming 0 or other value)
            {
                pet.MaxMp = buffer.ReadInt32();
                pet.Mana = buffer.ReadInt32();
            }

             // If it's our pet, update UI
            // if (playerIndex == GameState.MyIndex) { /* Update Pet UI */ }

            buffer.Dispose();
        }

        // Original buffer clear - Updated variable names
        public static void Packet_ClearPetSkillBuffer(ref byte[] data) // No data needed usually
        {
            // Called by server if skill cast fails or completes immediately
            PetSkillBuffer = -1;
            PetSkillBufferTimer = 0;
            // May need to reset cooldown if the server indicates failure
            // var buffer = new ByteStream(data);
            // int failedSkillSlot = buffer.ReadInt32(); // If server tells us which skill failed
            // PetSkillCD[failedSkillSlot] = 0; // Reset cooldown if needed
            // buffer.Dispose();
        }

        // Original attack packet - No changes needed
        public static void Packet_PetAttack(ref byte[] data)
        {
            var buffer = new ByteStream(data);
            int playerIndex = buffer.ReadInt32();
             if (playerIndex < 0 || playerIndex >= Core.Type.Player.Length) { buffer.Dispose(); return; }

            // Set pet to attacking state for animation
            Core.Type.Player[playerIndex].Pet.Attacking = 1;
            Core.Type.Player[playerIndex].Pet.AttackTimer = General.GetTickCount(); // Start animation timer

            buffer.Dispose();
        }

         // Original XY update - simplified, ensure player index 'i' is correctly obtained
         // This packet seems redundant if Packet_UpdatePlayerPet or Packet_PetMove updates position.
         // If it's for instant teleport/position correction:
        public static void Packet_PetXY(ref byte[] data)
        {
            var buffer = new ByteStream(data);
            int playerIndex = buffer.ReadInt32(); // Need player index
            if (playerIndex < 0 || playerIndex >= Core.Type.Player.Length) { buffer.Dispose(); return; }

            ref var pet = ref Core.Type.Player[playerIndex].Pet;
            pet.X = buffer.ReadInt32();
            pet.Y = buffer.ReadInt32();
            pet.Moving = (byte)Core.Enum.MovementType.None; // Stop any current movement interpolation
            pet.XOffset = 0;
            pet.YOffset = 0;

            buffer.Dispose();
        }


        // Original experience packet - Updated variables
        public static void Packet_PetExperience(ref byte[] data)
        {
            var buffer = new ByteStream(data);
            int myIndex = GameState.MyIndex; // Assumes this packet is only sent to the owner

            Core.Type.Player[myIndex].Pet.Exp = buffer.ReadInt32();
            Core.Type.Player[myIndex].Pet.Tnl = buffer.ReadInt32();

            // Check for level up (server should handle actual level up, client might show effect)
            // if (Core.Type.Player[myIndex].Pet.Exp >= Core.Type.Player[myIndex].Pet.Tnl && Core.Type.Player[myIndex].Pet.Tnl > 0)
            // {
            //    // Play level up animation/sound? Server sends updated level/stats via Packet_UpdatePlayerPet
            // }

             // Update EXP bar UI
             // UI.PetWindow.UpdateExpBar();

            buffer.Dispose();
        }

        // --- NEW PACKETS ---

        public static void Packet_PetAddStatusEffect(ref byte[] data)
        {
             var buffer = new ByteStream(data);
             int playerIndex = buffer.ReadInt32();
             if (playerIndex < 0 || playerIndex >= Core.Type.Player.Length) { buffer.Dispose(); return; }

             // Read status effect data (example)
             // ActiveStatusEffect effect = new ActiveStatusEffect();
             // effect.StatusID = buffer.ReadInt32();
             // effect.RemainingDurationTicks = buffer.ReadInt32();
             // effect.Stacks = buffer.ReadByte();

             // Add or update the effect in the pet's list
             // AddOrUpdateStatusEffect(Core.Type.Player[playerIndex].Pet.StatusEffects, effect);

             // If it's our pet, update UI
             // if (playerIndex == GameState.MyIndex) { /* Update Pet Status Icon UI */ }

             buffer.Dispose();
        }

        public static void Packet_PetRemoveStatusEffect(ref byte[] data)
        {
             var buffer = new ByteStream(data);
             int playerIndex = buffer.ReadInt32();
             int statusIdToRemove = buffer.ReadInt32();
             if (playerIndex < 0 || playerIndex >= Core.Type.Player.Length) { buffer.Dispose(); return; }

             // Remove the effect from the list
             // RemoveStatusEffect(Core.Type.Player[playerIndex].Pet.StatusEffects, statusIdToRemove);

             // If it's our pet, update UI
             // if (playerIndex == GameState.MyIndex) { /* Update Pet Status Icon UI */ }

             buffer.Dispose();
        }

         public static void Packet_PetSkillCooldown(ref byte[] data)
         {
             var buffer = new ByteStream(data);
             int skillSlotIndex = buffer.ReadInt32();
             int cooldownEndTime = buffer.ReadInt32(); // Server sends absolute tick time when CD ends

             if (skillSlotIndex >= 0 && skillSlotIndex < Constant.MAX_PET_SKILLS)
             {
                 PetSkillCD[skillSlotIndex] = cooldownEndTime;
                 // Update UI for this skill slot to show cooldown timer
                 // UI.PetSkillBar.UpdateCooldown(skillSlotIndex, cooldownEndTime);
             }
             buffer.Dispose();
         }

         public static void Packet_PetUpdateLoyaltyHunger(ref byte[] data)
         {
              var buffer = new ByteStream(data);
              int myIndex = GameState.MyIndex; // Assume only sent to owner

              Core.Type.Player[myIndex].Pet.Loyalty = buffer.ReadFloat();
              Core.Type.Player[myIndex].Pet.Hunger = buffer.ReadFloat();

              // Update UI
              // UI.PetWindow.UpdateLoyaltyBar();
              // UI.PetWindow.UpdateHungerBar();

              buffer.Dispose();
         }

        public static void Packet_PetDied(ref byte[] data)
        {
            var buffer = new ByteStream(data);
            int playerIndex = buffer.ReadInt32();
            if (playerIndex < 0 || playerIndex >= Core.Type.Player.Length) { buffer.Dispose(); return; }

            Core.Type.Player[playerIndex].Pet.Alive = 0;
            Core.Type.Player[playerIndex].Pet.Health = 0;
            // Play death animation/sound?
            // If it's our pet, update UI significantly (grey out, show "Dead")
            // if (playerIndex == GameState.MyIndex) { /* Update Pet UI to show dead state */ }

            buffer.Dispose();
        }

        public static void Packet_PetRevived(ref byte[] data) // Sent after player uses revive item/skill
        {
            var buffer = new ByteStream(data);
            int playerIndex = buffer.ReadInt32();
            if (playerIndex < 0 || playerIndex >= Core.Type.Player.Length) { buffer.Dispose(); return; }

            ref var pet = ref Core.Type.Player[playerIndex].Pet;
            pet.Alive = 1;
            pet.Health = buffer.ReadInt32(); // Server sends starting health after revive
            pet.MaxHp = buffer.ReadInt32();
            pet.Mana = buffer.ReadInt32();
            pet.MaxMp = buffer.ReadInt32();

             // Update UI to show alive state
            // if (playerIndex == GameState.MyIndex) { /* Update Pet UI */ }

            buffer.Dispose();
        }


         // Packet containing the list of pets owned by the player
         public static void Packet_ReceiveOwnedPetsList(ref byte[] data)
         {
             // This data needs to be stored somewhere, perhaps in GameState or a dedicated PetManager class
             // Example: GameState.OwnedPets = new List<OwnedPetSummary>();
             var buffer = new ByteStream(data);
             int count = buffer.ReadInt32();
             // GameState.OwnedPets.Clear();
             // for (int i = 0; i < count; i++)
             // {
             //     OwnedPetSummary summary = new OwnedPetSummary();
             //     summary.OwnedIndex = buffer.ReadInt32(); // Unique index in the player's owned list
             //     summary.PetTypeNum = buffer.ReadInt32(); // The type ID of the pet
             //     summary.CustomName = buffer.ReadString();
             //     summary.Level = buffer.ReadInt32();
             //     summary.IsSummoned = buffer.ReadBoolean(); // Is this the currently summoned one?
             //     // Add other useful summary info...
             //     GameState.OwnedPets.Add(summary);
             //     // Also request static data for this pet type if needed
             //     RequestPetStaticDataIfNeeded(summary.PetTypeNum);
             // }
             buffer.Dispose();
             // Update Pet Stable UI, Summoning UI etc.
             // UI.PetStableWindow.RefreshList();
         }


        #endregion

        #region Movement (Refined)

        public static void ProcessPetMovement(int playerIndex)
        {
            if (playerIndex < 0 || playerIndex >= Core.Type.Player.Length) return;
            ref var pet = ref Core.Type.Player[playerIndex].Pet;

            // Only process movement if the pet is actively moving between tiles
            if (pet.Moving != (byte)Core.Enum.MovementType.Walking) // Assuming Walking is the only interpolated type
            {
                 // If not walking, ensure offsets are zero
                 if (pet.XOffset != 0 || pet.YOffset != 0)
                 {
                     pet.XOffset = 0;
                     pet.YOffset = 0;
                 }
                 // Reset animation steps if not moving (optional, server might control this)
                 // if (pet.Moving == (byte)Core.Enum.MovementType.None) pet.Steps = 0;
                 return;
            }

            // Calculate movement speed - could be dynamic based on pet stats/buffs
            // float currentMoveSpeed = GetPetCurrentMoveSpeed(playerIndex); // Needs implementation
            float currentMoveSpeed = GameState.WalkSpeed; // Using player walk speed as default

            // Calculate distance to move this frame
            double deltaMove = (GameState.ElapsedTime / 1000.0) * currentMoveSpeed; // Tiles per second
            int pixelMoveX = (int)Math.Round(deltaMove * GameState.SizeX); // Pixels to move
            int pixelMoveY = (int)Math.Round(deltaMove * GameState.SizeY);

            bool finishedMoving = false;

            // Update offset based on direction
            switch ((Core.Enum.DirectionType)pet.Dir)
            {
                case Core.Enum.DirectionType.Up:
                    pet.YOffset -= pixelMoveY;
                    if (pet.YOffset <= 0) finishedMoving = true;
                    break;
                case Core.Enum.DirectionType.Down:
                    pet.YOffset += pixelMoveY;
                    if (pet.YOffset >= 0) finishedMoving = true;
                    break;
                case Core.Enum.DirectionType.Left:
                    pet.XOffset -= pixelMoveX;
                    if (pet.XOffset <= 0) finishedMoving = true;
                    break;
                case Core.Enum.DirectionType.Right:
                    pet.XOffset += pixelMoveX;
                    if (pet.XOffset >= 0) finishedMoving = true;
                    break;
            }

            // Check if completed walking over to the next tile
            if (finishedMoving)
            {
                pet.Moving = (byte)Core.Enum.MovementType.None; // Stop interpolation
                pet.XOffset = 0; // Snap to grid
                pet.YOffset = 0;

                // Cycle animation steps (simple 1-2 cycle)
                pet.Steps = (pet.Steps == 1) ? (byte)2 : (byte)1;
                // Consider a more robust animation system link here
            }
            else
            {
                 // Determine animation frame based on progress (optional, simple is based on Steps)
                 // Could tie animation frame to YOffset/XOffset percentage
            }
        }

        // Original PetMove - Renamed to reflect it sends a *request* to move
        public static void SendRequestPetMove(int targetX, int targetY)
        {
            // Add client-side validation: Is target tile walkable? Is it within pet's allowed range?
            // if (!IsTileWalkable(targetX, targetY) || !IsWithinLeashRange(targetX, targetY)) return;

            var buffer = new ByteStream(4 + 4 + 4); // Header + X + Y
            buffer.WriteInt32((int)Packets.ClientPackets.CPetMove); // Or CPetGotoXY
            buffer.WriteInt32(targetX);
            buffer.WriteInt32(targetY);
            NetworkConfig.Socket.SendData(buffer.Data, buffer.Head);
            buffer.Dispose();
        }

        #endregion

        #region Drawing (Expanded)

        public static void DrawPet(int playerIndex)
        {
             if (!IsPetVisibleAndValid(playerIndex)) return; // Check if pet exists, is alive, has sprite etc.

            ref var pet = ref Core.Type.Player[playerIndex].Pet;
            ref var petStaticData = ref Core.Type.Pet[pet.Num]; // Get static data

            int spriteNum = petStaticData.Sprite;
            if (spriteNum <= 0 || spriteNum > GameState.NumCharacters) return; // Validate sprite number

            // Determine Animation Frame (more robustly)
            byte animFrame = DeterminePetAnimationFrame(playerIndex); // New helper function

            // Determine Sprite Sheet Row (Direction)
            int spriteRow = GetSpriteRowFromDirection(pet.Dir); // New helper function

            // Get Sprite Dimensions
            // Assuming GameClient.GetCharacterGfxInfo caches or efficiently gets Texture info
            var spriteInfo = GameClient.GetCharacterGfxInfo(System.IO.Path.Combine(Path.Characters, spriteNum.ToString()));
            if (spriteInfo == null || spriteInfo.Width == 0 || spriteInfo.Height == 0) return; // Handle missing graphics

            int frameWidth = spriteInfo.Width / 4; // Assuming 4 frames horizontally
            int frameHeight = spriteInfo.Height / 4; // Assuming 4 directions vertically

            // Define source rectangle on the sprite sheet
            Rectangle sourceRect = new Rectangle(animFrame * frameWidth, spriteRow * frameHeight, frameWidth, frameHeight);

            // Calculate target screen position (incorporating map scroll etc.)
            Point screenPos = CalculatePetScreenPosition(playerIndex, frameWidth, frameHeight); // New helper function

            // Get pet's color tint (e.g., poisoned = green tint, frozen = blue tint)
             Color tint = GetPetColorTint(playerIndex); // New helper function, checks status effects

            // --- Draw Pet Sprite ---
            GameClient.DrawCharacterSprite(spriteNum, screenPos.X, screenPos.Y, sourceRect, tint); // Assuming DrawCharacterSprite takes Color

            // --- Draw Equipment (Optional) ---
            DrawPetEquipment(playerIndex, screenPos, animFrame, spriteRow); // New helper function

            // --- Draw Status Effect Icons (Optional) ---
            DrawPetStatusIcons(playerIndex, screenPos, frameHeight); // New helper function
        }

        // Helper to determine animation frame
        private static byte DeterminePetAnimationFrame(int playerIndex)
        {
             ref var pet = ref Core.Type.Player[playerIndex].Pet;
             int attackSpeed = GetPetAttackSpeed(playerIndex); // Get current attack speed (ms)

             // Attacking animation takes priority
             if (pet.Attacking == 1 && pet.AttackTimer > 0)
             {
                 // Simple two-frame attack animation (e.g., frame 2 and 3)
                 float attackProgress = (General.GetTickCount() - pet.AttackTimer) / (float)attackSpeed;
                 if (attackProgress < 0.5f) return 2; // First half of attack anim
                 else if (attackProgress < 1.0f) return 3; // Second half
                 else { pet.Attacking = 0; pet.AttackTimer = 0; } // Attack finished
             }

             // Walking animation
             if (pet.Moving == (byte)Core.Enum.MovementType.Walking && (pet.XOffset != 0 || pet.YOffset != 0))
             {
                 // Use Steps (1 or 2) for alternating walk frames (e.g., frame 1 and 2)
                 // Frame 0 is often idle
                 return (pet.Steps == 1) ? (byte)1 : (byte)2;
             }

             // Idle animation (frame 0)
             return 0;
        }

         // Helper to get sprite sheet row from direction
        private static int GetSpriteRowFromDirection(int direction)
        {
            switch ((Core.Enum.DirectionType)direction)
            {
                case Core.Enum.DirectionType.Up: return 3;
                case Core.Enum.DirectionType.Down: return 0;
                case Core.Enum.DirectionType.Left: return 1;
                case Core.Enum.DirectionType.Right: return 2;
                default: return 0; // Default to Down
            }
        }

        // Helper to calculate final screen position
        private static Point CalculatePetScreenPosition(int playerIndex, int frameWidth, int frameHeight)
        {
             ref var pet = ref Core.Type.Player[playerIndex].Pet;

             // Base position on map grid, converted to screen coords (including map scroll)
             int baseX = GameLogic.ConvertMapX(pet.X * GameState.PicX);
             int baseY = GameLogic.ConvertMapY(pet.Y * GameState.PicY);

             // Add interpolation offsets
             baseX += pet.XOffset;
             baseY += pet.YOffset;

             // Adjust based on sprite size relative to tile size (centering or bottom-aligning)
             int drawX = baseX + (GameState.PicX / 2) - (frameWidth / 2); // Center horizontally
             int drawY = baseY + GameState.PicY - frameHeight; // Align bottom of sprite with bottom of tile

             return new Point(drawX, drawY);
        }

        // Helper to get color tint based on status effects
        private static Color GetPetColorTint(int playerIndex)
        {
            // Example: Check pet.StatusEffects
            // if (HasStatusEffect(playerIndex, StatusEffectType.Poison)) return Color.Green;
            // if (HasStatusEffect(playerIndex, StatusEffectType.Frozen)) return Color.LightBlue;
            return Color.White; // Default: No tint
        }

        // Placeholder for drawing equipped items on the pet
        private static void DrawPetEquipment(int playerIndex, Point baseScreenPos, byte animFrame, int spriteRow)
        {
            ref var pet = ref Core.Type.Player[playerIndex].Pet;
            // Loop through pet.Equipment
            // For each equipped item ID:
            // 1. Get the item's visual representation (e.g., an overlay sprite).
            // 2. Determine the correct source rect on the equipment sprite sheet based on animFrame and spriteRow.
            // 3. Calculate the offset relative to baseScreenPos where the equipment should be drawn.
            // 4. Draw the equipment sprite using GameClient.DrawSprite or similar.
            // Example:
            // int collarItemId = pet.Equipment[(int)PetEquipmentSlot.Collar];
            // if (collarItemId > 0) {
            //    Texture2D collarTexture = GameClient.GetItemTexture(collarItemId);
            //    Rectangle collarSourceRect = CalculateEquipmentSourceRect(collarItemId, animFrame, spriteRow);
            //    Point collarPos = CalculateEquipmentPosition(PetEquipmentSlot.Collar, baseScreenPos, animFrame, spriteRow);
            //    GameClient.DrawSprite(collarTexture, collarPos.X, collarPos.Y, collarSourceRect, Color.White);
            // }
        }

        // Placeholder for drawing status icons near the pet
        private static void DrawPetStatusIcons(int playerIndex, Point baseScreenPos, int petFrameHeight)
        {
            ref var pet = ref Core.Type.Player[playerIndex].Pet;
            // Loop through pet.StatusEffects (limit to MaxPetStatusEffects?)
            // For each active effect:
            // 1. Get the icon texture for the status effect ID.
            // 2. Calculate the position for the icon (e.g., above the pet's head, in a row).
            // 3. Draw the icon using GameClient.DrawSprite.
            // 4. Optionally draw duration/stacks text.
            // Example:
            // int iconX = baseScreenPos.X;
            // int iconY = baseScreenPos.Y - 20; // Position above pet
            // foreach (var effect in pet.StatusEffects) {
            //     Texture2D iconTexture = GameClient.GetStatusEffectIcon(effect.StatusID);
            //     if (iconTexture != null) {
            //         GameClient.DrawSprite(iconTexture, iconX, iconY, null, Color.White); // null for whole texture
            //         iconX += iconTexture.Width + 2; // Move to next icon position
            //     }
            // }
        }


        // Original Name Drawing - Refined
        public static void DrawPlayerPetName(int playerIndex)
        {
            if (!IsPetVisibleAndValid(playerIndex)) return; // Use helper check

            ref var pet = ref Core.Type.Player[playerIndex].Pet;
            ref var petStaticData = ref Core.Type.Pet[pet.Num]; // Get static data

            // Determine the name to display (custom or default)
            string nameToShow = string.IsNullOrEmpty(pet.CustomName) ? petStaticData.Name : pet.CustomName;
            string ownerName = GetPlayerName(playerIndex); // Assuming GetPlayerName exists
            string displayName = $"{ownerName}'s {nameToShow}";

            // Determine text color based on owner's status (PK, Admin, etc.)
            Color nameColor = GetPlayerNameColor(playerIndex); // Use helper if available
            Color backColor = Color.Black; // Default outline/background

            // Calculate text position above the pet sprite
            var spriteInfo = GameClient.GetCharacterGfxInfo(System.IO.Path.Combine(Path.Characters, petStaticData.Sprite.ToString()));
            if (spriteInfo == null) return; // Need sprite info for positioning
            int frameHeight = spriteInfo.Height / 4;

            Point petScreenPos = CalculatePetScreenPosition(playerIndex, spriteInfo.Width / 4, frameHeight);
            int textWidth = Text.GetTextWidth(displayName); // Assuming Text rendering class exists
            int textX = petScreenPos.X + (spriteInfo.Width / 8) - (textWidth / 2); // Center above sprite center
            int textY = petScreenPos.Y - 18; // Position slightly above the sprite

            // Draw name using Text rendering system
            Text.RenderText(displayName, textX, textY, nameColor, backColor); // Assuming RenderText handles outline/shadow via backColor

             // Optionally draw Level next to name
             string levelText = $"Lv.{pet.Level}";
             int levelTextWidth = Text.GetTextWidth(levelText);
             Text.RenderText(levelText, textX + textWidth + 5, textY, Color.White, backColor); // Draw level to the right

             // Optionally draw mini health bar below name (if not part of main UI)
             // DrawMiniPetHealthBar(playerIndex, textX, textY + 15, textWidth);
        }

        #endregion

        #region Misc Helpers (Expanded)

        // Check if pet data is valid and pet is visible/active
        private static bool IsPetVisibleAndValid(int playerIndex)
        {
            if (playerIndex < 0 || playerIndex >= Core.Type.Player.Length) return false;
            ref var pet = ref Core.Type.Player[playerIndex].Pet;
            if (pet.Num <= 0 || pet.Alive == 0) return false; // Not summoned or dead

             // Check if static data is loaded
             if (pet.Num >= Core.Type.Pet.Length || string.IsNullOrEmpty(Core.Type.Pet[pet.Num].Name))
             {
                 // Request data if missing and return false for this frame
                 RequestPetStaticDataIfNeeded(pet.Num);
                 return false;
             }
            // Add culling checks? Is the pet on screen?
            // if (!IsPetOnScreen(playerIndex)) return false;

            return true;
        }

        // Original PetAlive - Simplified check
        public static bool IsPetAlive(int playerIndex)
        {
            if (playerIndex < 0 || playerIndex >= Core.Type.Player.Length) return false;
            // Check Num >= 0 for valid pet type and Alive == 1
            return Core.Type.Player[playerIndex].Pet.Num > 0 && Core.Type.Player[playerIndex].Pet.Alive == 1;
        }

        // Helper to get calculated stat value (including buffs, equipment)
        public static int GetPetCalculatedStat(int playerIndex, Core.Enum.StatType stat)
        {
            if (!IsPetAlive(playerIndex)) return 0;

            ref var pet = ref Core.Type.Player[playerIndex].Pet;
            int baseStat = pet.Stat[(int)stat]; // Base allocated points/value
            int modifiedStat = baseStat;

            // 1. Add equipment bonuses
            // foreach (int itemId in pet.Equipment) {
            //     if (itemId > 0) modifiedStat += GetItemStatBonus(itemId, stat);
            // }

            // 2. Apply status effect modifiers (positive and negative)
            // foreach (var effect in pet.StatusEffects) {
            //    modifiedStat += GetStatusEffectStatModifier(effect.StatusID, stat);
            //}

            // 3. Apply talent modifiers
            // foreach (var kvp in pet.LearnedTalents) {
            //     modifiedStat += GetTalentStatModifier(kvp.Key, kvp.Value, stat);
            // }

            // 4. Apply loyalty modifier (example: small boost at high loyalty)
            // if (pet.Loyalty > 80) modifiedStat = (int)(modifiedStat * 1.05f);
            // else if (pet.Loyalty < 20) modifiedStat = (int)(modifiedStat * 0.9f);

            // 5. Apply hunger modifier (example: penalty when starving)
            // if (pet.Hunger < 10) modifiedStat = (int)(modifiedStat * 0.8f);


            return Math.Max(0, modifiedStat); // Ensure stat doesn't go below 0
        }

        // Helper to get current attack speed (considering buffs/debuffs)
        public static int GetPetAttackSpeed(int playerIndex)
        {
             if (!IsPetVisibleAndValid(playerIndex)) return 1000; // Default
             ref var petStatic = ref Core.Type.Pet[Core.Type.Player[playerIndex].Pet.Num];
             float baseSpeed = petStatic.BaseAttackSpeed > 0 ? petStatic.BaseAttackSpeed : 1000; // Default 1000ms

             // Apply modifiers from buffs/debuffs/equipment/talents (similar to GetPetCalculatedStat)
             // ...

             return (int)Math.Max(100, baseSpeed); // Ensure minimum attack speed
        }

         // Helper to get current move speed
         public static float GetPetMoveSpeed(int playerIndex)
         {
             if (!IsPetVisibleAndValid(playerIndex)) return GameState.WalkSpeed; // Default
             ref var petStatic = ref Core.Type.Pet[Core.Type.Player[playerIndex].Pet.Num];
             float baseSpeed = petStatic.BaseMoveSpeed > 0 ? petStatic.BaseMoveSpeed : GameState.WalkSpeed;

             // Apply modifiers...

             return Math.Max(0.5f, baseSpeed); // Ensure minimum speed
         }

        // Placeholder for checking if pet is within leash range of owner
        private static bool IsWithinLeashRange(int targetX, int targetY)
        {
             // Get owner position
             // Get pet position
             // Calculate distance
             // Return distance <= Constant.PET_LEASH_RANGE;
             return true; // Placeholder
        }

         // Example helper for Status Effects (needs proper implementation)
        // private static void AddOrUpdateStatusEffect(List<ActiveStatusEffect> effects, ActiveStatusEffect newEffect) { ... }
        // private static void RemoveStatusEffect(List<ActiveStatusEffect> effects, int statusIdToRemove) { ... }
        // private static bool HasStatusEffect(int playerIndex, int statusId) { ... }

        #endregion
    }
}
