using System;
using System.Collections.Generic;
using System.Drawing;
// Using Concurrent collections for potential server-side thread safety
using System.Collections.Concurrent;

// --- Configuration (Example - Ideally loaded from file/DB) ---
public static class GameConfig
{
    public const int MAX_INV = 50;
    public const int MAX_BANK_SLOTS = 100;
    public const int MAX_EQUIP = 10; // Example: Head, Chest, Legs, etc.
    public const int MAX_SKILLS_PER_PLAYER = 50;
    public const int MAX_HOTBAR_SLOTS = 10;
    public const int MAX_STATS = 8; // Example: Str, Dex, Int, Vit, etc.
    public const int MAX_VITALS = 2; // HP, MP
    public const int MAX_QUEST_TASKS = 10;
    public const int MAX_PARTY_MEMBERS = 5;
    public const int MAX_GUILD_MEMBERS_INITIAL = 20;
    public const int MAX_CHAT_LINES = 100;
    public const int MAX_ACTION_MSG = 50;
    public const int MAX_BLOOD_SPLATS = 50;
    public const int MAX_PLAYER_SWITCHES = 100;
    public const int MAX_PLAYER_VARIABLES = 100;
    public const int MAX_TILE_LAYERS = 5;
    public const int MAX_SELF_SWITCHES = 4; // A, B, C, D per event
    public const int MAX_EVENT_COMMAND_PARAMS = 6;
    public const int MAX_EVENT_COMMAND_TEXTS = 5;

    // These might not be needed if using Dictionaries for definitions
    // public const int MAX_JOBS = 50;
    // public const int MAX_MORALS = 10;
    // public const int MAX_ITEMS = 10000;
    // public const int MAX_NPCS = 5000;
    // public const int MAX_SHOPS = 1000;
    // public const int MAX_SKILLS = 2000;
    // public const int MAX_RESOURCES = 500;
    // public const int MAX_ANIMATIONS = 1000;
    // public const int MAX_MAPS = 2000;
    // public const int MAX_PROJECTILES = 500;
    // public const int MAX_PETS = 500;
    // public const int MAX_QUESTS = 1000;
    // public const int MAX_GUILDS = 500; // Dynamic might be better
}

// --- Core Namespaces ---
namespace Core.Data.Definitions
{
    #region Definition Structs/Classes (Loaded once, represent templates)

    /// <summary>
    /// Defines the properties of a player job/class.
    /// </summary>
    public class JobDefinition
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int[] BaseStats { get; set; } = new int[GameConfig.MAX_STATS];
        public int MaleSpriteId { get; set; }
        public int FemaleSpriteId { get; set; }
        public List<StartingItem> StartingItems { get; set; } = new List<StartingItem>();
        public int StartMapId { get; set; }
        public Point StartPosition { get; set; }
        public int BaseExperience { get; set; } // For level 1 or scaling factor
        // Consider adding: Skill unlocks per level, equipment restrictions, etc.
    }

    public struct StartingItem
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }
    }

    /// <summary>
    /// Defines the rules for a specific map moral type (e.g., Safe Zone, PK Zone).
    /// </summary>
    public class MoralDefinition
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Color DisplayColor { get; set; } = Color.White; // Changed from byte index
        public bool CanCastSpells { get; set; } = true;
        public bool CanInitiatePK { get; set; } = true;
        public bool CanUseItems { get; set; } = true;
        public bool DropItemsOnDeath { get; set; } = false;
        public bool LoseExpOnDeath { get; set; } = false;
        public bool CanPickupItems { get; set; } = true;
        public bool CanDropItems { get; set; } = true;
        public bool PlayerBlockEnabled { get; set; } = true; // Collision
        public bool NPCBlockEnabled { get; set; } = true; // Collision
        // Consider adding: Experience modifiers, PvP damage scaling, etc.
    }

    /// <summary>
    /// Defines the properties of an item template.
    /// </summary>
    public class ItemDefinition
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int IconId { get; set; }
        public string Description { get; set; } = string.Empty;
        public ItemType Type { get; set; }
        public int SubType { get; set; } // Could be an enum specific to ItemType
        public int Data1 { get; set; } // Usage depends on Type/SubType (e.g., Heal amount, Damage, Skill ID)
        public int Data2 { get; set; }
        public int Data3 { get; set; }
        public int JobRequirement { get; set; } // Job ID, 0 for none
        public int AccessRequirement { get; set; } // Access Level (e.g., GM only)
        public int LevelRequirement { get; set; }
        public int MasteryRequirement { get; set; } // Specific skill/mastery level needed?
        public int Price { get; set; }
        public int[] StatModifiers { get; set; } = new int[GameConfig.MAX_STATS]; // Renamed from Add_Stat
        public ItemRarity Rarity { get; set; }
        public int Speed { get; set; } // Attack speed for weapons, cast speed modifier?
        public BindType BindType { get; set; }
        public int[] StatRequirements { get; set; } = new int[GameConfig.MAX_STATS]; // Renamed from Stat_Req
        public int EquipAnimationId { get; set; } // Animation played when equipped/used
        public int PaperdollSpriteId { get; set; } // Visual representation on character
        public bool IsStackable { get; set; }
        public int MaxStackSize { get; set; } = 1; // New: Max stack size if stackable
        public int ItemLevel { get; set; } // For gear progression, scaling, etc.
        public int KnockbackPower { get; set; } // Renamed from KnockBack
        public int KnockbackTiles { get; set; }
        public int ProjectileId { get; set; } // For projectile weapons
        public int AmmoItemId { get; set; } // Item ID required as ammo
        public int Durability { get; set; } = -1; // Max durability, -1 for infinite
        // Consider adding: Set bonuses, sockets, enchantments, cosmetic flag
    }

    // --- Enums for ItemDefinition ---
    public enum ItemType { None, Weapon, Armor, Accessory, Consumable, Material, Quest, Currency, Container, Blueprint, PetEgg, MountLicense }
    public enum ItemRarity { Common, Uncommon, Rare, Epic, Legendary, Mythic }
    public enum BindType { None, OnPickup, OnEquip, AccountBound }

    /// <summary>
    /// Defines the properties of an NPC template.
    /// </summary>
    public class NPCDefinition
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string AttackQuote { get; set; } = string.Empty; // Renamed from AttackSay
        public int SpriteId { get; set; }
        public TimeSpan SpawnCooldown { get; set; } // Changed from SpawnTime/SpawnSecs
        public NPCBehaviorType Behavior { get; set; }
        public int AggroRange { get; set; } // Renamed from Range
        public List<NPCDrop> Drops { get; set; } = new List<NPCDrop>();
        public int[] BaseStats { get; set; } = new int[GameConfig.MAX_STATS];
        public int FactionId { get; set; }
        public int BaseMaxHP { get; set; }
        public int BaseMaxMP { get; set; } // New: Added MP for caster NPCs
        public int ExperienceReward { get; set; } // Renamed from Exp
        public int AttackAnimationId { get; set; } // Renamed from Animation
        public List<int> SkillIds { get; set; } = new List<int>();
        public int Level { get; set; }
        public int BaseDamage { get; set; } // Physical damage
        public int BaseMagicDamage { get; set; } // New: Magic damage
        public int BaseDefense { get; set; } // New: Physical defense
        public int BaseMagicDefense { get; set; } // New: Magic defense
        public NPCType NPCType { get; set; } = NPCType.Monster; // New: Differentiate types
        public bool IsBoss { get; set; } // New: Flag for boss mechanics
        // Consider adding: Movement speed, resistances, immunities, AI script link
    }

    public struct NPCDrop
    {
        public int ItemId { get; set; }
        public int MinQuantity { get; set; } // Changed from DropItemValue
        public int MaxQuantity { get; set; } // New: For ranges
        public double Chance { get; set; } // Use double for probability (0.0 to 1.0)
        // Consider adding: Quest requirement for drop, condition (e.g., only if player has X)
    }

    public enum NPCBehaviorType { Still, Wander, Guard, Patrol, Flee, Scripted }
    public enum NPCType { Monster, Friendly, Vendor, QuestGiver, Guard, Trainer, Special }

    /// <summary>
    /// Defines a shop and its inventory.
    /// </summary>
    public class ShopDefinition
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double BuyRateModifier { get; set; } = 1.0; // Multiplier for default item prices
        public double SellRateModifier { get; set; } = 0.5; // New: Rate at which shop buys items from player
        public List<ShopItem> ItemsForSale { get; set; } = new List<ShopItem>();
        // Consider adding: Faction requirement, quest unlock requirement, limited stock/refresh timers
    }

    public struct ShopItem
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; } // -1 for infinite
        public int? OverridePrice { get; set; } // Optional price override
        public int? RequiredCurrencyItemId { get; set; } // Optional alternative currency
        public int? RequiredCurrencyAmount { get; set; }
        // Consider adding: Required item trade (Item A for Item B)
    }

    /// <summary>
    /// Defines a skill or ability.
    /// </summary>
    public class SkillDefinition
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public SkillType Type { get; set; }
        public int MpCost { get; set; }
        public int HpCost { get; set; } // New: Skills that cost HP
        public int LevelRequirement { get; set; }
        public int AccessRequirement { get; set; }
        public int JobRequirement { get; set; }
        public TimeSpan CastTime { get; set; }
        public TimeSpan CooldownTime { get; set; }
        public int IconId { get; set; }
        public int RequiredItemId { get; set; } // New: Item needed to cast (e.g., reagent)
        public int ConsumedItemId { get; set; } // New: Item consumed on cast
        public int Range { get; set; } // 0 for self, 1 for melee, >1 for ranged
        public bool IsAreaOfEffect { get; set; }
        public int AoeRadius { get; set; } // Radius if IsAoE is true
        public SkillTargetType TargetType { get; set; } = SkillTargetType.Enemy; // New: Define allowed targets
        public int CastAnimationId { get; set; }
        public int ActionAnimationId { get; set; } // Animation of the skill effect itself
        public int ProjectileId { get; set; } // If the skill fires a projectile
        public TimeSpan StunDuration { get; set; }
        public int KnockbackPower { get; set; }
        public int KnockbackTiles { get; set; }
        // Direct Effects (Can be expanded into a more complex effect system)
        public int DamageAmount { get; set; }
        public DamageType DamageType { get; set; } = DamageType.Physical; // New
        public int HealAmount { get; set; }
        public int BuffId { get; set; } // New: Apply a status effect (buff/debuff)
        public TimeSpan BuffDuration { get; set; } // New
        public int SummonNpcId { get; set; } // New: Summon an NPC
        public int TeleportMapId { get; set; } // New: For teleport skills
        public Point TeleportPosition { get; set; } // New
        // Consider adding: Scaling based on stats, combo potential, prerequisite skills
    }

    public enum SkillType { Passive, ActiveTargeted, ActiveAoE, ActiveSelf, Toggle }
    public enum SkillTargetType { Self, Ally, Enemy, AllyOrSelf, EnemyOnly, GroundLocation, Corpse }
    public enum DamageType { Physical, Magical, Fire, Ice, Lightning, Poison, Holy, Shadow, True } // New

    /// <summary>
    /// Defines a gatherable resource node.
    /// </summary>
    public class ResourceNodeDefinition
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SuccessMessage { get; set; } = string.Empty;
        public string EmptyMessage { get; set; } = string.Empty;
        public ResourceType ResourceType { get; set; } // Mining, Herbalism, etc.
        public int ActiveSpriteId { get; set; } // Renamed from ResourceImage
        public int DepletedSpriteId { get; set; } // Renamed from ExhaustedImage
        public int ExperienceReward { get; set; } // Exp for the associated skill
        public int ItemRewardId { get; set; }
        public int ItemRewardMinQuantity { get; set; } = 1; // New
        public int ItemRewardMaxQuantity { get; set; } = 1; // New
        public int SkillLevelRequired { get; set; } // Renamed from LvlRequired
        public int RequiredToolItemId { get; set; } // e.g., Pickaxe ID
        public int MaxHealth { get; set; } // How many "hits" it takes
        public TimeSpan RespawnTime { get; set; }
        public bool IsWalkable { get; set; } // Renamed from Walkthrough
        public int GatherAnimationId { get; set; } // Player animation when gathering
        // Consider adding: Rare drop chance, tool quality requirement
    }

    public enum ResourceType { None, Mining, Herbalism, Logging, Fishing, Skinning, Archaeology }

    /// <summary>
    /// Defines an animation sequence.
    /// </summary>
    public class AnimationDefinition
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SoundFileName { get; set; } = string.Empty;
        public List<AnimationFrame> Frames { get; set; } = new List<AnimationFrame>();
        // Original had Sprite[], Frames[], LoopCount[], LoopTime[] which is confusing.
        // Let's simplify to a list of frames, each with duration and sprite.
        public bool Loops { get; set; } // Does the animation loop?
        // Consider adding: Blend modes, particle effects tied to frames
    }

    public struct AnimationFrame
    {
        public int SpriteId { get; set; }
        public TimeSpan Duration { get; set; }
        // Consider: Sound trigger, effect trigger
    }

    /// <summary>
    /// Defines a map layout and properties.
    /// </summary>
    public class MapDefinition
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string MusicTrackName { get; set; } = string.Empty;
        public int Revision { get; set; }
        public int MoralId { get; set; } // Link to MoralDefinition ID
        public int TilesetId { get; set; }
        public int LinkMapUp { get; set; }
        public int LinkMapDown { get; set; }
        public int LinkMapLeft { get; set; }
        public int LinkMapRight { get; set; }
        public int RespawnMapId { get; set; } // Renamed from BootMap
        public Point RespawnPosition { get; set; } // Renamed from BootX/Y
        public int MaxX { get; set; } // Width - 1
        public int MaxY { get; set; } // Height - 1
        public TileData[,] Tiles { get; set; } // Now a single consistent Tile structure
        public List<MapSpawnPoint> NpcSpawns { get; set; } = new List<MapSpawnPoint>();
        public List<MapResourceNode> ResourceNodeSpawns { get; set; } = new List<MapResourceNode>(); // New
        public List<int> MapEventIds { get; set; } = new List<int>(); // Global events active on this map
        public WeatherType DefaultWeather { get; set; } = WeatherType.None;
        public int FogSpriteId { get; set; }
        public int WeatherIntensity { get; set; }
        public byte FogOpacity { get; set; }
        public byte FogSpeed { get; set; }
        public bool UseTint { get; set; }
        public Color TintColor { get; set; } // Replaced R, G, B, A bytes
        public int PanoramaSpriteId { get; set; }
        public int ParallaxSpriteId { get; set; }
        public byte Brightness { get; set; } = 255; // 0-255
        public int ZoneShopId { get; set; } // Default shop for the area? Might be better tied to NPCs/Triggers
        public bool IsSafeZone { get; set; } // Simplified flag, potentially redundant with MoralId
        public bool IsIndoors { get; set; }
        // Consider adding: Day/Night cycle effects, region definitions within map, sound zones
    }

    /// <summary>
    /// Represents a single tile on a map. Consolidated from various formats.
    /// </summary>
    public struct TileData
    {
        public TileLayerData[] Layers { get; set; } // Ground, Mask, Mask2, Fringe, Fringe2, Roof etc.
        public TileType Type { get; set; }
        public int Data1 { get; set; } // Warp MapID, NPC ID, Resource ID, Sign Text ID etc.
        public int Data2 { get; set; } // Warp X, Shop ID, etc.
        public int Data3 { get; set; } // Warp Y, etc.
        public Direction BlockedDirections { get; set; } // Bitmask for blocked movement

        // Consider adding Autotile data if needed, or handle rendering logic elsewhere
        // public byte AutotileType { get; set; } // Ground autotile?
        // public byte AutotileMask { get; set; } // Mask autotile?
    }

    public struct TileLayerData
    {
        public int TilesetId { get; set; }
        public int TileSheetX { get; set; } // X coord on the tilesheet
        public int TileSheetY { get; set; } // Y coord on the tilesheet
        public bool IsAnimated { get; set; } // If this layer uses an animated tile
        public int AnimationFrameCount { get; set; }
        public TimeSpan AnimationSpeed { get; set; }
    }

    [Flags]
    public enum Direction : byte
    {
        None = 0,
        Up = 1,
        Down = 2,
        Left = 4,
        Right = 8,
        All = Up | Down | Left | Right
    }

    public enum TileType { Walkable, Blocked, Warp, NpcSpawn, Resource, Shop, Bank, Heal, Damage, EventTrigger, QuestTrigger, Sign, Water, BlockProjectile }

    public struct MapSpawnPoint
    {
        public int NpcId { get; set; }
        public Point Position { get; set; }
        public int Range { get; set; } // Wander range from spawn point
        public Direction FacingDirection { get; set; } = Direction.Down;
        public TimeSpan? CustomRespawnTime { get; set; } // Override default NPC respawn
    }

    public struct MapResourceNode
    {
        public int ResourceNodeId { get; set; }
        public Point Position { get; set; }
        public TimeSpan? CustomRespawnTime { get; set; } // Override default resource respawn
    }


    /// <summary>
    /// Defines a projectile.
    /// </summary>
    public class ProjectileDefinition
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int SpriteId { get; set; }
        public int Speed { get; set; } // Pixels per second or similar unit
        public int MaxRange { get; set; } // Tiles it can travel
        public int HitAnimationId { get; set; } // Animation on impact
        public bool PiercesTargets { get; set; } // New: Does it hit multiple targets?
        public bool IsHoming { get; set; } // New: Does it track a target?
        // Consider adding: AoE effect on impact, status effect application
    }

    /// <summary>
    /// Defines a Pet type.
    /// </summary>
    public class PetDefinition
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int SpriteId { get; set; }
        public int FollowRange { get; set; } // How far it stays from owner
        public int MaxLevel { get; set; }
        public int BaseExpToLevel { get; set; } // Experience scaling factor
        public int StatPointsPerLevel { get; set; }
        public Stat PrimaryStat { get; set; } // Stat that primarily scales its abilities
        public PetLevelingType LevelingType { get; set; } // How it gains EXP
        public int[] BaseStats { get; set; } = new int[GameConfig.MAX_STATS];
        public List<int> LearnableSkillIds { get; set; } = new List<int>();
        public bool CanEvolve { get; set; }
        public int EvolveLevel { get; set; }
        public int EvolveToPetId { get; set; }
        public PetBehavior DefaultBehavior { get; set; } = PetBehavior.Assist; // New
        public int BaseMaxHP { get; set; } // New
        public int BaseMaxMP { get; set; } // New
        public int BaseDamage { get; set; } // New
        // Consider: Special abilities, equipment slots, food requirements
    }

    public enum Stat { Strength, Dexterity, Intelligence, Vitality /*, ... */ } // Example stats
    public enum PetLevelingType { ShareOwnerExp, KillStealExp, TimeBased, ManualFeed }
    public enum PetBehavior { Passive, Defensive, Assist, Aggressive } // New


    /// <summary>
    /// Defines a quest structure.
    /// </summary>
    public class QuestDefinition
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LogSummary { get; set; } = string.Empty; // Displayed in quest log
        public string NpcStartDialogue { get; set; } = string.Empty; // Dialogue when accepting
        public string NpcEndDialogue { get; set; } = string.Empty; // Dialogue when completing
        public int RequiredLevel { get; set; } // New
        public List<int> PrerequisiteQuestIds { get; set; } = new List<int>(); // New
        public int QuestGiverNpcId { get; set; } // New: NPC who gives the quest
        public int QuestCompleterNpcId { get; set; } // New: NPC to turn in the quest
        public List<QuestTask> Tasks { get; set; } = new List<QuestTask>();
        public int RewardExperience { get; set; }
        public int RewardGold { get; set; } // New: Gold reward
        public List<QuestRewardItem> RewardItems { get; set; } = new List<QuestRewardItem>();
        public bool IsRepeatable { get; set; } // New
        public TimeSpan RepeatCooldown { get; set; } // New: If repeatable
        public int NextQuestId { get; set; } // New: Chain quest automatically offered
        // Consider: Faction reward, title reward, skill point reward
    }

    public struct QuestTask
    {
        public QuestTaskType Type { get; set; }
        public int TargetId { get; set; } // NPC ID to kill/talk, Item ID to collect, Map ID to reach, Resource ID to gather
        public int RequiredAmount { get; set; }
        public string Description { get; set; } = string.Empty; // Displayed during the task
        public Point? TargetLocation { get; set; } // Optional specific location for Reach tasks
        public string TargetNpcDialogue { get; set; } // Optional dialogue for TalkTo tasks
        // Consider: Timed tasks
    }

    public struct QuestRewardItem
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public bool IsChoice { get; set; } // New: Part of a choice of rewards?
        public int ChoiceGroupId { get; set; } // New: Link items belonging to the same choice
    }

    public enum QuestTaskType { KillNpc, CollectItem, TalkToNpc, ReachLocation, GatherResource, UseItemOnTarget, InteractObject }

    /// <summary>
    /// Defines properties of a Faction
    /// </summary>
    public class FactionDefinition
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Dictionary<int, FactionStanding> DefaultStandings { get; set; } = new Dictionary<int, FactionStanding>(); // Default relation to other factions (by ID)
        public List<ReputationLevel> ReputationLevels { get; set; } = new List<ReputationLevel>(); // Hated, Hostile, Neutral, Friendly, Honored, Exalted
        // Consider: Associated NPCs, quests, rewards unlocked at certain levels
    }

    public enum FactionStanding { Hostile, Neutral, Friendly }

    public struct ReputationLevel
    {
        public string Name { get; set; } // e.g., "Friendly"
        public int Threshold { get; set; } // Points required to reach this level
        public Color DisplayColor { get; set; }
    }

    /// <summary>
    /// Defines an achievement criteria and rewards.
    /// </summary>
    public class AchievementDefinition
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int IconId { get; set; }
        public AchievementCategory Category { get; set; }
        public AchievementCriteria Criteria { get; set; }
        public int Points { get; set; } // Achievement points awarded
        public string TitleReward { get; set; } = string.Empty; // Optional title unlocked
        public int ItemRewardId { get; set; } // Optional item awarded
        public int ItemRewardQuantity { get; set; }
        public bool IsHiddenUntilCompleted { get; set; } // New
        // Consider: Linking achievements (meta-achievements)
    }

    public enum AchievementCategory { General, Questing, Exploration, PvP, PvE, Crafting, Social }

    public struct AchievementCriteria // This would likely need specific subclasses or a more complex structure
    {
        public AchievementCriteriaType Type { get; set; }
        public int TargetId { get; set; } // Quest ID, Item ID, NPC ID, Map ID, Skill ID etc.
        public int RequiredValue { get; set; } // Level, Amount, Count etc.
    }

    public enum AchievementCriteriaType { ReachLevel, CompleteQuest, KillNpc, DiscoverMap, ObtainItem, CraftItem, EarnCurrency, ReachReputation, UseSkill }


    /// <summary>
    /// Defines a Mount.
    /// </summary>
    public class MountDefinition
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int IconId { get; set; } // Icon for the mount item/skill
        public int SpriteId { get; set; } // Sprite used when mounted (could replace player sprite or be layered)
        public float SpeedModifier { get; set; } // Movement speed multiplier (e.g., 1.5 for 50% faster)
        public bool CanFly { get; set; } // New
        public bool UsableIndoors { get; set; } // New
        public bool UsableInCombat { get; set; } // New
        public TimeSpan CastTime { get; set; } // Time to summon/mount
        public int RequiredItemId { get; set; } // Item consumed or needed to learn the mount
        public int RequiredLevel { get; set; } // New
        // Consider: Mount equipment, special abilities while mounted
    }

    /// <summary>
    /// Defines a crafting recipe.
    /// </summary>
    public class RecipeDefinition
    {
        public int Id { get; set; }
        public string Name { get; set; } // Optional, often derived from result item
        public int ResultItemId { get; set; }
        public int ResultQuantity { get; set; } = 1;
        public CraftingSkillType SkillRequired { get; set; }
        public int SkillLevelRequired { get; set; }
        public List<RecipeIngredient> Ingredients { get; set; } = new List<RecipeIngredient>();
        public TimeSpan CraftTime { get; set; }
        public int ExpReward { get; set; } // Crafting skill EXP gained
        public int RequiredCraftingStationId { get; set; } // e.g., Anvil, Forge, Alchemy Lab (could be 0 for anywhere)
        // Consider: Chance of skill-up, chance of creating higher quality item, discovery mechanics
    }

    public struct RecipeIngredient
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public bool IsConsumed { get; set; } = true; // Some ingredients might be tools that aren't consumed
    }

    public enum CraftingSkillType { None, Blacksmithing, Leatherworking, Tailoring, Alchemy, Jewelcrafting, Cooking, Engineering, Inscription }

    /// <summary>
    /// Defines an event structure (more complex scripting).
    /// This replaces the numerous Event structs from the original code.
    /// A full event system is complex, this is a basic placeholder.
    /// </summary>
    public class EventDefinition
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // For editor/debugging
        public List<EventPage> Pages { get; set; } = new List<EventPage>();
        // Position might be stored per-map instance rather than in the definition
        // public Point DefaultPosition { get; set; }
        // public bool IsGlobal { get; set; } // Activated across all maps?
    }

    public class EventPage
    {
        public List<EventCondition> Conditions { get; set; } = new List<EventCondition>(); // Conditions for this page to be active
        public EventAppearance Appearance { get; set; } = new EventAppearance();
        public EventMovement Movement { get; set; } = new EventMovement();
        public EventTrigger Trigger { get; set; } = EventTrigger.ActionKey;
        public List<EventCommand> Commands { get; set; } = new List<EventCommand>(); // Script executed when triggered
    }

    // --- Supporting Event Structures ---
    public struct EventCondition
    {
        public EventConditionType Type { get; set; }
        public int Param1 { get; set; } // e.g., Switch ID, Variable ID, Item ID, Quest ID
        public int Param2 { get; set; } // e.g., Required Value/State, Amount
        public object? Param3 { get; set; } // e.g., Operator (==, >=, <), Self-switch letter ('A')
    }
    public enum EventConditionType { Switch, Variable, SelfSwitch, Item, Actor, QuestStatus /* ... more ... */ }

    public struct EventAppearance
    {
        public GraphicType GraphicType { get; set; }
        public int GraphicId { get; set; } // Sprite ID or Tile ID
        // Add GraphicX/Y offsets or animation details if needed
        public bool UseDirectionFix { get; set; }
        public bool WalkThrough { get; set; } // Collision override
        public bool ShowNameTag { get; set; }
        // Consider Opacity, BlendMode, etc.
    }
    public enum GraphicType { None, Sprite, Tile }

    public struct EventMovement
    {
        public MovementType Type { get; set; } = MovementType.Fixed;
        public MovementSpeed Speed { get; set; } = MovementSpeed.Normal;
        public MovementFrequency Frequency { get; set; } = MovementFrequency.Normal;
        public List<MovementCommand>? Route { get; set; } // Optional custom route
        public bool RepeatRoute { get; set; }
        public bool SkipInvalidMoves { get; set; }
    }
    public enum MovementType { Fixed, Random, ApproachPlayer, CustomRoute, Patrol }
    public enum MovementSpeed { VerySlow, Slow, Normal, Fast, VeryFast }
    public enum MovementFrequency { Lowest, Low, Normal, High, Highest }
    public struct MovementCommand { /* ... Details depend on needed complexity ... */ } // e.g., MoveUp, TurnLeft, Wait, ChangeSpeed

    public enum EventTrigger { ActionKey, PlayerTouch, EventTouch, Autorun, Parallel }

    public class EventCommand // Base class for polymorphism or use a type enum
    {
        public EventCommandType Type { get; set; }
        public List<object> Parameters { get; set; } = new List<object>(); // Flexible parameters
        public string Comment { get; set; } = string.Empty; // For editor readability
        // For branching commands:
        public List<EventCommand>? SubCommands { get; set; } // Commands inside an IF or LOOP
        public List<EventCommand>? ElseCommands { get; set; } // Commands for the ELSE branch
    }
    public enum EventCommandType
    {
        ShowMessage, ShowChoices, InputVariable, ModifySwitch, ModifyVariable, ModifySelfSwitch,
        TransferPlayer, ChangeItem, ChangeGold, PlaySound, PlayAnimation, MoveEvent,
        ConditionalBranch, Loop, BreakLoop, CallCommonEvent, ScriptCall, Wait, /* ... many more ... */
        StartQuest, AdvanceQuest, CompleteQuest, OpenShop, OpenBank, HealParty, DamageTarget
    }


    #endregion
}

namespace Core.Data.Instances
{
    using Core.Data.Definitions; // Need access to definitions

    #region Instance Structs/Classes (Represent runtime state)

    /// <summary>
    /// Represents an item stack in an inventory or bank.
    /// </summary>
    public class InventorySlot
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public bool IsBound { get; set; } // If the specific stack is bound
        public int CurrentDurability { get; set; } = -1; // -1 if item has no durability

        // Reference to definition for quick access (optional, can be looked up via ItemId)
        // public ItemDefinition Definition => DataManager.GetItem(ItemId); // Example lookup
    }

    /// <summary>
    /// Represents a skill learned by a player, including cooldown state.
    /// </summary>
    public class PlayerSkill
    {
        public int SkillId { get; set; }
        public DateTime CooldownEndTime { get; set; } = DateTime.MinValue; // Time when cooldown expires

        // public SkillDefinition Definition => DataManager.GetSkill(SkillId);
    }

    /// <summary>
    /// Represents a player's progress in a quest.
    /// </summary>
    public class PlayerQuestProgress
    {
        public int QuestId { get; set; }
        public QuestStatus Status { get; set; } = QuestStatus.NotStarted;
        public Dictionary<int, int> TaskProgress { get; set; } = new Dictionary<int, int>(); // Key: Task Index, Value: Current Count
        public DateTime? CompletionTime { get; set; } // For repeatable cooldown tracking

        // public QuestDefinition Definition => DataManager.GetQuest(QuestId);
    }
    public enum QuestStatus { NotStarted, Started, TaskComplete, ReadyToComplete, Completed, Failed }


    /// <summary>
    /// Represents the state of a gatherable resource node instance on a map.
    /// </summary>
    public class MapResourceNodeInstance
    {
        public int UniqueInstanceId { get; set; } // Unique ID for this specific node on this map instance
        public int DefinitionId { get; set; } // Link to ResourceNodeDefinition
        public Point Position { get; set; }
        public int CurrentHealth { get; set; }
        public bool IsDepleted => CurrentHealth <= 0;
        public DateTime RespawnTime { get; set; } = DateTime.MinValue;

        // public ResourceNodeDefinition Definition => DataManager.GetResourceNode(DefinitionId);
    }


    /// <summary>
    /// Represents an instance of an NPC spawned on a map.
    /// </summary>
    public class NpcInstance
    {
        public int UniqueInstanceId { get; set; } // Unique ID for this specific NPC on this map instance
        public int DefinitionId { get; set; } // Link to NPCDefinition
        public Point Position { get; set; }
        public PointF Offset { get; set; } // For smooth movement rendering
        public Direction CurrentDirection { get; set; } = Direction.Down;
        public int CurrentHP { get; set; }
        public int CurrentMP { get; set; }
        public MovementState CurrentMovementState { get; set; } = MovementState.Idle;
        public CombatState CurrentCombatState { get; set; } = CombatState.Idle;
        public int TargetEntityId { get; set; } // Player ID or other NPC ID
        public EntityType TargetEntityType { get; set; } = EntityType.None;
        public DateTime SpawnTimestamp { get; set; }
        public DateTime NextActionTime { get; set; } // Timer for attacks, wandering, etc.
        public DateTime StunEndTime { get; set; }
        public List<ActiveStatusEffect> StatusEffects { get; set; } = new List<ActiveStatusEffect>();
        public Dictionary<int, DateTime> SkillCooldowns { get; set; } = new Dictionary<int, DateTime>(); // Skill ID -> Cooldown End Time

        // public NPCDefinition Definition => DataManager.GetNpc(DefinitionId);

        // Add methods for AI, pathfinding, combat logic etc. here or in an associated AI controller class
    }

    public enum MovementState { Idle, Moving, Pathfinding }
    public enum CombatState { Idle, Attacking, Casting, Fleeing, Stunned }
    public enum EntityType { None, Player, Npc, Pet, Object }

    /// <summary>
    /// Represents an item dropped on the ground on a map.
    /// </summary>
    public class MapItemInstance
    {
        public int UniqueInstanceId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public Point Position { get; set; }
        public DateTime DropTimestamp { get; set; }
        public DateTime DespawnTime { get; set; }
        public int? OwnerPlayerId { get; set; } // For temporary player ownership
        public DateTime OwnershipReleaseTime { get; set; } // When it becomes free for all

        // public ItemDefinition Definition => DataManager.GetItem(ItemId);
    }

    /// <summary>
    /// Represents a projectile currently in flight on a map.
    /// </summary>
    public class MapProjectileInstance
    {
        public int UniqueInstanceId { get; set; }
        public int DefinitionId { get; set; }
        public int OwnerEntityId { get; set; }
        public EntityType OwnerEntityType { get; set; }
        public PointF CurrentPosition { get; set; }
        public Direction FlightDirection { get; set; } // Or a target point/entity
        public float DistanceTraveled { get; set; }
        public DateTime LaunchTime { get; set; }
        public int? TargetEntityId { get; set; } // For homing projectiles

        // public ProjectileDefinition Definition => DataManager.GetProjectile(DefinitionId);
    }

    /// <summary>
    /// Represents a player character's data and state.
    /// </summary>
    public class Player
    {
        // --- Identifiers ---
        public int AccountId { get; set; } // Link to the account
        public int CharacterId { get; set; } // Unique character ID
        public string Name { get; set; } = string.Empty;

        // --- Basic Info ---
        public byte Sex { get; set; } // Consider Enum: Sex { Male, Female, Other }
        public int JobId { get; set; } // Link to JobDefinition
        public int SpriteId { get; set; } // Base sprite, potentially overridden by job/equipment
        public int Level { get; set; } = 1;
        public long Experience { get; set; } // Use long for potentially large numbers
        public long ExpToNextLevel { get; set; } // Cached value for next level requirement
        public AccessLevel Access { get; set; } = AccessLevel.Player;
        public PkStatus PkStatus { get; set; } = PkStatus.Neutral; // Player Kill status

        // --- Vitals & Stats ---
        public int[] CurrentVitals { get; set; } = new int[GameConfig.MAX_VITALS]; // 0: HP, 1: MP
        public int[] MaxVitals { get; set; } = new int[GameConfig.MAX_VITALS]; // Calculated from stats/level/gear
        public int[] BaseStats { get; set; } = new int[GameConfig.MAX_STATS]; // From level up, job
        public int[] BonusStats { get; set; } = new int[GameConfig.MAX_STATS]; // From gear, buffs
        public int StatPointsAvailable { get; set; } // Points to allocate on level up
        public Dictionary<ResourceType, ResourceSkillProgress> GatheringSkills { get; set; } = new Dictionary<ResourceType, ResourceSkillProgress>();

        // --- Equipment & Inventory ---
        public Dictionary<EquipmentSlot, InventorySlot?> Equipment { get; set; } = new Dictionary<EquipmentSlot, InventorySlot?>(); // Null if slot is empty
        public List<InventorySlot> Inventory { get; set; } = new List<InventorySlot>(GameConfig.MAX_INV);
        public List<InventorySlot> Bank { get; set; } = new List<InventorySlot>(GameConfig.MAX_BANK_SLOTS); // Bank stored with player data
        public long Gold { get; set; } // Player currency

        // --- Skills & Hotbar ---
        public Dictionary<int, PlayerSkill> LearnedSkills { get; set; } = new Dictionary<int, PlayerSkill>(); // Key: SkillId
        public HotbarSlot[] Hotbar { get; set; } = new HotbarSlot[GameConfig.MAX_HOTBAR_SLOTS];

        // --- Location & State ---
        public int CurrentMapId { get; set; }
        public Point Position { get; set; }
        public PointF Offset { get; set; } // For smooth client-side rendering
        public Direction CurrentDirection { get; set; } = Direction.Down;
        public MovementState CurrentMovementState { get; set; } = MovementState.Idle;
        public CombatState CurrentCombatState { get; set; } = CombatState.Idle;
        public DateTime LastRegenTime { get; set; } = DateTime.UtcNow; // HP/MP Regen tracking
        public DateTime StunEndTime { get; set; } = DateTime.MinValue;
        public DateTime LastMapRequestTime { get; set; } = DateTime.MinValue; // Prevent map spam
        public bool IsGettingMap { get; set; } // Flag during map transfer

        // --- Social ---
        public int? PartyId { get; set; }
        public int? GuildId { get; set; }
        public Dictionary<int, FactionReputation> Factions { get; set; } = new Dictionary<int, FactionReputation>(); // Player's standing with factions

        // --- Quests & Achievements ---
        public Dictionary<int, PlayerQuestProgress> QuestProgress { get; set; } = new Dictionary<int, PlayerQuestProgress>(); // Key: QuestId
        public HashSet<int> CompletedAchievements { get; set; } = new HashSet<int>(); // Set of completed Achievement IDs

        // --- Customization & Progression ---
        public PlayerPetInstance? ActivePet { get; set; }
        public int? ActiveMountId { get; set; } // Currently mounted Mount ID
        public HashSet<int> KnownMountIds { get; set; } = new HashSet<int>(); // Mounts the player owns
        public HashSet<int> KnownRecipeIds { get; set; } = new HashSet<int>(); // Recipes the player has learned

        // --- Event System State ---
        public bool[] Switches { get; set; } = new bool[GameConfig.MAX_PLAYER_SWITCHES]; // Global switches affecting this player
        public int[] Variables { get; set; } = new int[GameConfig.MAX_PLAYER_VARIABLES]; // Global variables affecting this player

        // --- Temporary Runtime Data (May not need to be saved) ---
        public int TargetEntityId { get; set; }
        public EntityType TargetEntityType { get; set; } = EntityType.None;
        public List<ActiveStatusEffect> StatusEffects { get; set; } = new List<ActiveStatusEffect>();
        public int? TradingWithPlayerId { get; set; }
        public List<InventorySlot> TradeOffer { get; set; } = new List<InventorySlot>();
        public bool HasAcceptedTrade { get; set; }
        public int? InteractionTargetId { get; set; } // NPC, Bank, Shop etc. being interacted with
        public InteractionType CurrentInteraction { get; set; } = InteractionType.None;

        // public JobDefinition Job => DataManager.GetJob(JobId); // Example accessors

        // Add methods for Update, Attack, CastSkill, Move, Interact, Save, Load, etc.
    }

    // --- Enums and Structs for Player ---
    public enum AccessLevel { Player, Moderator, GameMaster, Administrator }
    public enum PkStatus { Neutral, Killer, Outlaw } // Example PK states
    public enum EquipmentSlot { Head, Chest, Legs, Feet, Hands, Weapon, Shield, Accessory1, Accessory2, Ring1, Ring2, Ammo, Mount, Pet } // Example slots
    public struct ResourceSkillProgress { public int Level; public int Experience; }
    public struct HotbarSlot { public HotbarItemType Type; public int ItemOrSkillId; }
    public enum HotbarItemType { None, Item, Skill }
    public struct FactionReputation { public int Points; /* Cache current level name/color? */ }
    public enum InteractionType { None, NpcDialogue, Shop, Bank, Trade, PartyInvite, GuildInvite, CraftingStation }

    /// <summary>
    /// Represents the state of a player's active pet.
    /// </summary>
    public class PlayerPetInstance
    {
        public int DefinitionId { get; set; } // Link to PetDefinition
        public string CustomName { get; set; } = string.Empty; // Optional player-given name
        public int Level { get; set; }
        public long Experience { get; set; }
        public long ExpToNextLevel { get; set; }
        public int CurrentHP { get; set; }
        public int CurrentMP { get; set; }
        public int MaxHP { get; set; } // Calculated
        public int MaxMP { get; set; } // Calculated
        public int[] CurrentStats { get; set; } = new int[GameConfig.MAX_STATS]; // Includes base + allocated points
        public int StatPointsAvailable { get; set; }
        public HashSet<int> LearnedSkillIds { get; set; } = new HashSet<int>();
        public PetBehavior CurrentBehavior { get; set; }
        public bool IsSummoned { get; set; } // Is the pet currently active in the world?
        // Runtime state (similar to NpcInstance if it acts independently)
        public Point Position { get; set; } // If summoned
        public PointF Offset { get; set; }
        public Direction CurrentDirection { get; set; }
        public MovementState CurrentMovementState { get; set; }
        public CombatState CurrentCombatState { get; set; }
        public int TargetEntityId { get; set; }
        public EntityType TargetEntityType { get; set; }
        public DateTime NextActionTime { get; set; }
        public DateTime StunEndTime { get; set; }
        public List<ActiveStatusEffect> StatusEffects { get; set; } = new List<ActiveStatusEffect>();
        public Dictionary<int, DateTime> SkillCooldowns { get; set; } = new Dictionary<int, DateTime>();

        // public PetDefinition Definition => DataManager.GetPet(DefinitionId);
    }

    /// <summary>
    /// Represents an active status effect (buff/debuff) on an entity.
    /// </summary>
    public class ActiveStatusEffect
    {
        public int BuffDefinitionId { get; set; } // Link to a BuffDefinition class/struct (not defined here)
        public int CasterEntityId { get; set; }
        public EntityType CasterEntityType { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Stacks { get; set; } = 1; // For stackable buffs/debuffs
        public DateTime NextTickTime { get; set; } // For DoT/HoT effects

        // public BuffDefinition Definition => DataManager.GetBuff(BuffDefinitionId);
    }


    /// <summary>
    /// Represents an active party/group.
    /// </summary>
    public class Party
    {
        public int PartyId { get; set; }
        public int LeaderPlayerId { get; set; }
        public List<int> MemberPlayerIds { get; set; } = new List<int>(GameConfig.MAX_PARTY_MEMBERS);
        public LootRule CurrentLootRule { get; set; } = LootRule.RoundRobin; // New
        // Consider: Party buffs, experience sharing rules
    }

    public enum LootRule { FreeForAll, RoundRobin, MasterLooter, NeedBeforeGreed }


    /// <summary>
    /// Represents an active Guild.
    /// </summary>
    public class Guild
    {
        public int GuildId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Tag { get; set; } // Optional short tag [TAG]
        public int LeaderPlayerId { get; set; }
        public List<GuildMember> Members { get; set; } = new List<GuildMember>();
        public int MaxMembers { get; set; } // Can increase with guild level/perks
        public int Level { get; set; } = 1;
        public long Experience { get; set; } // Earned through guild activities
        public long ExpToNextLevel { get; set; }
        public string MessageOfTheDay { get; set; } = string.Empty; // New
        public List<GuildRank> Ranks { get; set; } = new List<GuildRank>(); // New: Customizable ranks and permissions
        public List<InventorySlot> GuildBank { get; set; } = new List<InventorySlot>(); // New: Shared bank
        public long GuildGold { get; set; } // New: Shared currency
        // Consider: Guild perks, alliances, rivalries, emblems
    }

    public struct GuildMember
    {
        public int PlayerId { get; set; }
        public int RankId { get; set; }
        public DateTime JoinDate { get; set; }
        public string? Note { get; set; } // Officer note about the member
    }

    public struct GuildRank
    {
        public int RankId { get; set; } // 0 is usually Leader, 1 default member, etc.
        public string Name { get; set; }
        public GuildPermissions Permissions { get; set; }
    }

    [Flags]
    public enum GuildPermissions
    {
        None = 0,
        InviteMember = 1,
        KickMember = 2,
        PromoteMember = 4,
        DemoteMember = 8,
        ChangeMotd = 16,
        AccessBankTab1 = 32,
        AccessBankTab2 = 64,
        WithdrawGold = 128,
        EditRanks = 256,
        // ... more permissions
        All = ~0
    }

    /// <summary>
    /// Represents the current state of weather globally or per map instance.
    /// </summary>
    public class CurrentWeather
    {
        public WeatherType Type { get; set; } = WeatherType.None;
        public int Intensity { get; set; } // Controls particle count/speed/opacity
        public DateTime EndTime { get; set; } // When the current weather effect should stop
        // Consider: Wind direction/speed, temperature effects
    }
    public enum WeatherType { None, Rain, HeavyRain, Thunderstorm, Snow, Blizzard, Sandstorm, Fog }


    /// <summary>
    /// Represents an active instance of a map, potentially instanced for dungeons/parties.
    /// </summary>
    public class MapInstance
    {
        public int InstanceId { get; private set; } // Unique ID for this running instance
        public int DefinitionId { get; private set; } // The MapDefinition this is based on
        public DateTime CreationTime { get; private set; }

        // Using ConcurrentDictionary for thread-safe access if needed on server
        public ConcurrentDictionary<int, Player> Players { get; } = new ConcurrentDictionary<int, Player>();
        public ConcurrentDictionary<int, NpcInstance> Npcs { get; } = new ConcurrentDictionary<int, NpcInstance>();
        public ConcurrentDictionary<int, MapItemInstance> Items { get; } = new ConcurrentDictionary<int, MapItemInstance>();
        public ConcurrentDictionary<int, MapProjectileInstance> Projectiles { get; } = new ConcurrentDictionary<int, MapProjectileInstance>();
        public ConcurrentDictionary<int, MapResourceNodeInstance> Resources { get; } = new ConcurrentDictionary<int, MapResourceNodeInstance>();
        public ConcurrentDictionary<int, ActiveEventInstance> Events { get; } = new ConcurrentDictionary<int, ActiveEventInstance>(); // Active events on this map

        public CurrentWeather CurrentWeather { get; set; } = new CurrentWeather(); // Weather specific to this instance

        // public MapDefinition Definition => DataManager.GetMap(DefinitionId);

        public MapInstance(int definitionId, int instanceId)
        {
            DefinitionId = definitionId;
            InstanceId = instanceId;
            CreationTime = DateTime.UtcNow;
            // Initialize Npcs, Resources etc. based on MapDefinition spawns
        }

        // Add methods for Update, AddPlayer, RemovePlayer, SpawnNpc, GetEntitiesInArea, etc.
    }

    /// <summary>
    /// Represents the runtime state of an event on a specific map instance.
    /// </summary>
    public class ActiveEventInstance
    {
        public int UniqueInstanceId { get; set; } // Unique runtime ID
        public int DefinitionId { get; set; } // Link to EventDefinition
        public int CurrentPageId { get; set; } // Which page is currently active based on conditions
        public Point Position { get; set; }
        public PointF Offset { get; set; }
        public Direction CurrentDirection { get; set; }
        public MovementState CurrentMovementState { get; set; }
        public bool IsRunningCommands { get; set; } // If currently executing its command list
        public int CurrentCommandIndex { get; set; }
        public Dictionary<char, bool> SelfSwitches { get; set; } = new Dictionary<char, bool>(); // 'A', 'B', 'C', 'D' -> bool

        // Add state related to movement route, timers, waiting for input etc.

        // public EventDefinition Definition => DataManager.GetEvent(DefinitionId);
        // public EventPage CurrentPage => Definition?.Pages.FirstOrDefault(p => /* Check conditions */); // Logic to find active page
    }

    #endregion
}


namespace Core.Managers
{
    using Core.Data.Definitions;
    using Core.Data.Instances;
    using System.Collections.Concurrent;

    #region Manager Classes (Singletons or Injected Instances)

    /// <summary>
    /// Manages loading and accessing game data definitions.
    /// Consider making this non-static and using Dependency Injection.
    /// </summary>
    public static class DataManager
    {
        // Dictionaries for fast ID lookup. Loaded at startup.
        public static ConcurrentDictionary<int, ItemDefinition> Items { get; } = new ConcurrentDictionary<int, ItemDefinition>();
        public static ConcurrentDictionary<int, NpcDefinition> Npcs { get; } = new ConcurrentDictionary<int, NpcDefinition>();
        public static ConcurrentDictionary<int, SkillDefinition> Skills { get; } = new ConcurrentDictionary<int, SkillDefinition>();
        public static ConcurrentDictionary<int, MapDefinition> Maps { get; } = new ConcurrentDictionary<int, MapDefinition>();
        public static ConcurrentDictionary<int, QuestDefinition> Quests { get; } = new ConcurrentDictionary<int, QuestDefinition>();
        public static ConcurrentDictionary<int, JobDefinition> Jobs { get; } = new ConcurrentDictionary<int, JobDefinition>();
        public static ConcurrentDictionary<int, MoralDefinition> Morals { get; } = new ConcurrentDictionary<int, MoralDefinition>();
        public static ConcurrentDictionary<int, ShopDefinition> Shops { get; } = new ConcurrentDictionary<int, ShopDefinition>();
        public static ConcurrentDictionary<int, ResourceNodeDefinition> ResourceNodes { get; } = new ConcurrentDictionary<int, ResourceNodeDefinition>();
        public static ConcurrentDictionary<int, AnimationDefinition> Animations { get; } = new ConcurrentDictionary<int, AnimationDefinition>();
        public static ConcurrentDictionary<int, ProjectileDefinition> Projectiles { get; } = new ConcurrentDictionary<int, ProjectileDefinition>();
        public static ConcurrentDictionary<int, PetDefinition> Pets { get; } = new ConcurrentDictionary<int, PetDefinition>();
        public static ConcurrentDictionary<int, FactionDefinition> Factions { get; } = new ConcurrentDictionary<int, FactionDefinition>();
        public static ConcurrentDictionary<int, AchievementDefinition> Achievements { get; } = new ConcurrentDictionary<int, AchievementDefinition>();
        public static ConcurrentDictionary<int, MountDefinition> Mounts { get; } = new ConcurrentDictionary<int, MountDefinition>();
        public static ConcurrentDictionary<int, RecipeDefinition> Recipes { get; } = new ConcurrentDictionary<int, RecipeDefinition>();
        public static ConcurrentDictionary<int, EventDefinition> Events { get; } = new ConcurrentDictionary<int, EventDefinition>();
        // Add BuffDefinitions, etc.

        public static void LoadAllData()
        {
            // Implement logic to load data from files (JSON, XML, Binary) or database
            // Populate the dictionaries above.
            Console.WriteLine("Loading all game definitions...");
            // Example: LoadItems(); LoadNpcs(); ...
            Console.WriteLine("Game definitions loaded.");
        }

        // Add Getters (e.g., GetItem(int id), TryGetNpc(int id, out NpcDefinition npc))
        public static ItemDefinition? GetItem(int id) => Items.TryGetValue(id, out var item) ? item : null;
        public static NpcDefinition? GetNpc(int id) => Npcs.TryGetValue(id, out var npc) ? npc : null;
        public static MapDefinition? GetMap(int id) => Maps.TryGetValue(id, out var map) ? map : null;
        // ... other getters ...
    }

    /// <summary>
    /// Manages active map instances.
    /// </summary>
    public class MapManager
    {
        private readonly ConcurrentDictionary<int, MapInstance> _activeMaps = new ConcurrentDictionary<int, MapInstance>();
        private int _nextInstanceId = 0;

        public MapInstance GetOrCreateMapInstance(int mapDefinitionId, bool forceNewInstance = false)
        {
            // Simple logic: assumes one instance per map def unless forced (e.g., dungeon)
            // More complex logic needed for multiple instances, dynamic loading/unloading.
            if (!forceNewInstance && _activeMaps.TryGetValue(mapDefinitionId, out var existingInstance))
            {
                return existingInstance;
            }

            var mapDef = DataManager.GetMap(mapDefinitionId);
            if (mapDef == null)
            {
                throw new ArgumentException($"Map definition not found: {mapDefinitionId}");
            }

            var newInstanceId = System.Threading.Interlocked.Increment(ref _nextInstanceId);
            var newInstance = new MapInstance(mapDefinitionId, newInstanceId);

            // TODO: Spawn initial NPCs, resources, events based on mapDef

            if (_activeMaps.TryAdd(newInstanceId, newInstance)) // Use instance ID as key if multiple instances allowed
            {
                 Console.WriteLine($"Created map instance {newInstanceId} for map {mapDef.Name} ({mapDefinitionId})");
                 return newInstance;
            }
            else
            {
                // Handle unlikely error case where add fails
                Console.WriteLine($"Error: Failed to add map instance {newInstanceId} to active maps.");
                // Might need to return an existing instance if concurrency issue arose
                return _activeMaps[newInstanceId]; // Assuming it must be there if Add failed concurrently
            }
        }

        public void UpdateAllMaps(TimeSpan deltaTime)
        {
            foreach (var mapInstance in _activeMaps.Values)
            {
                // mapInstance.Update(deltaTime); // Implement Update logic within MapInstance
            }
        }

        // Add methods: TransferPlayerToMap, FindPlayer, etc.
    }

    /// <summary>
    /// Manages online players.
    /// </summary>
    public class PlayerManager
    {
        private readonly ConcurrentDictionary<int, Player> _onlinePlayers = new ConcurrentDictionary<int, Player>(); // Key: CharacterId

        public bool TryAddPlayer(Player player)
        {
            return _onlinePlayers.TryAdd(player.CharacterId, player);
        }

        public Player? GetPlayer(int characterId)
        {
            _onlinePlayers.TryGetValue(characterId, out var player);
            return player;
        }

        public bool TryRemovePlayer(int characterId, out Player? player)
        {
            return _onlinePlayers.TryRemove(characterId, out player);
        }

        public IEnumerable<Player> GetAllOnlinePlayers() => _onlinePlayers.Values;

        // Add methods: LoadPlayerData, SavePlayerData, HandlePlayerDisconnect, etc.
    }

    /// <summary>
    /// Manages active parties.
    /// </summary>
    public class PartyManager
    {
        private readonly ConcurrentDictionary<int, Party> _parties = new ConcurrentDictionary<int, Party>();
        private int _nextPartyId = 0;

        // Methods: CreateParty, AddMember, RemoveMember, DisbandParty, GetParty, GetPlayerParty etc.
    }

    /// <summary>
    /// Manages active guilds.
    /// </summary>
    public class GuildManager
    {
        private readonly ConcurrentDictionary<int, Guild> _guilds = new ConcurrentDictionary<int, Guild>();
        private int _nextGuildId = 0;

        // Methods: CreateGuild, InviteMember, KickMember, Promote/Demote, DisbandGuild, GetGuild, GetPlayerGuild, LoadGuilds, SaveGuilds etc.
    }

    // --- Other Potential Managers ---
    // CombatManager (Handles combat logic, damage calculation)
    // QuestManager (Tracks player quest progress updates)
    // EventManager (Processes active events and commands)
    // WeatherManager (Controls global or regional weather changes)
    // NetworkManager (Handles client connections and packet sending/receiving - VERY IMPORTANT)

    #endregion
}

// --- Miscellaneous (Can be moved to UI/Client specific namespaces) ---
namespace Core.Client.UI
{
    /// <summary>
    /// Represents a chat message entry.
    /// </summary>
    public struct ChatMessage
    {
        public string Text { get; set; }
        public Color Color { get; set; }
        public ChatChannel Channel { get; set; }
        public DateTime Timestamp { get; set; }
        public string? SenderName { get; set; } // Optional: Name of the sender
    }
    public enum ChatChannel { System, Say, Party, Guild, Whisper, Trade, World }

    /// <summary>
    /// Represents a floating text message (damage, heal, status).
    /// </summary>
    public struct ActionMessage
    {
        public string Text { get; set; }
        public Color Color { get; set; }
        public Point StartPosition { get; set; } // World position where it originates
        public ActionMessageType Type { get; set; } // Damage, Heal, Miss, Exp Gain etc.
        public DateTime CreationTime { get; set; }
        // Client handles animation/scrolling
    }
    public enum ActionMessageType { Damage, Heal, Miss, Dodge, CriticalDamage, ExpGain, ItemPickup, GoldPickup, BuffGain, DebuffGain }


    /// <summary>
    /// Represents a temporary blood splat effect.
    /// </summary>
    public struct BloodEffect
    {
        public int SpriteId { get; set; }
        public Point Position { get; set; }
        public DateTime CreationTime { get; set; }
        public TimeSpan Duration { get; set; }
    }

    /// <summary>
    /// Represents a chat bubble above an entity.
    /// </summary>
    public struct ChatBubble
    {
        public string Text { get; set; }
        public int TargetEntityId { get; set; }
        public EntityType TargetEntityType { get; set; }
        public DateTime CreationTime { get; set; }
        public TimeSpan Duration { get; set; }
    }

     /// <summary>
    /// Represents weather particle state (Client side).
    /// </summary>
    public struct WeatherParticle
    {
        public WeatherType Type { get; set; } // Raindrop, snowflake etc.
        public PointF Position { get; set; }
        public PointF Velocity { get; set; }
        public float Rotation { get; set; }
        public float Alpha { get; set; }
        public bool IsActive { get; set; }
    }
}
