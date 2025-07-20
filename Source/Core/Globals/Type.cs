using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using Core.Database;
using Core.Globals;
using MonoGame.Extended.Content.Tiled;

namespace Core
{
    public static class Type
    {
        // Common data structure arrays with improved initialization
        public static JobStruct[] Job = new JobStruct[Constant.MAX_JOBS];
        public static MoralStruct[] Moral = new MoralStruct[Constant.MAX_MORALS];
        public static ItemStruct[] Item = new ItemStruct[Constant.MAX_ITEMS];
        public static NPCStruct[] NPC = new NPCStruct[Constant.MAX_NPCS];
        public static ShopStruct[] Shop = new ShopStruct[Constant.MAX_SHOPS];
        public static SkillStruct[] Skill = new SkillStruct[Constant.MAX_SKILLS];
        public static MapResourceStruct[] MapResource = new MapResourceStruct[Constant.MAX_RESOURCES];
        public static MapResourceCacheStruct[] MyMapResource = new MapResourceCacheStruct[Constant.MAX_RESOURCES];
        public static AnimationStruct[] Animation = new AnimationStruct[Constant.MAX_ANIMATIONS];
        public static MapStruct[] Map = new MapStruct[Constant.MAX_MAPS];
        public static MapStruct MyMap;
        public static TileStruct[,] TempTile;
        public static MapItemStruct[,] MapItem = new MapItemStruct[Constant.MAX_MAPS, Constant.MAX_MAP_ITEMS];
        public static MapItemStruct[] MyMapItem = new MapItemStruct[Constant.MAX_MAP_ITEMS];
        public static MapDataStruct[] MapNPC = new MapDataStruct[Constant.MAX_MAPS];
        public static MapNPCStruct[] MyMapNPC = new MapNPCStruct[Constant.MAX_MAP_NPCS];
        public static BankStruct[] Bank = new BankStruct[Constant.MAX_PLAYERS];
        public static TempPlayerStruct[] TempPlayer = new TempPlayerStruct[Constant.MAX_PLAYERS];
        public static AccountStruct[] Account = new AccountStruct[Constant.MAX_PLAYERS];
        public static PlayerStruct[] Player = new PlayerStruct[Constant.MAX_PLAYERS];
        public static ProjectileStruct[] Projectile = new ProjectileStruct[Constant.MAX_PROJECTILES];
        public static MapProjectileStruct[,] MapProjectile = new MapProjectileStruct[Constant.MAX_MAPS, Constant.MAX_PROJECTILES];
        public static PlayerInvStruct[] TradeYourOffer = new PlayerInvStruct[Constant.MAX_INV];
        public static PlayerInvStruct[] TradeTheirOffer = new PlayerInvStruct[Constant.MAX_INV];
        public static PartyStruct[] Party = new PartyStruct[Constant.MAX_PARTY];
        public static PartyStruct MyParty;
        public static ResourceStruct[] Resource = new ResourceStruct[Constant.MAX_RESOURCES];
        public static CharList Char;
        public static PetStruct[] Pet = new PetStruct[Constant.MAX_PETS];
        public static ChatBubbleStruct[] ChatBubble = new ChatBubbleStruct[byte.MaxValue];
        public static ScriptStruct Script = new ScriptStruct(); 

        public static QuestStruct[] Quests = new QuestStruct[Constant.MAX_QUESTS];
        public static EventStruct[] Events = new EventStruct[Constant.MAX_EVENTS];
        public static GuildStruct[] Guilds = new GuildStruct[Constant.MAX_GUILDS];
        public static WeatherStruct Weather = new WeatherStruct();

        #region Struct Definitions

        public struct ResourceTypetruct
        {
            public int SkillLevel;
            public int SkillCurExp;
            public int SkillNextLvlExp;
        }

        public struct AnimationStruct
        {
            public string Name;
            public string Sound;
            public int[] Sprite;
            public int[] Frames;
            public int[] LoopCount;
            public int[] LoopTime;
        }

        public struct RectStruct
        {
            public double Top;
            public double Left;
            public double Right;
            public double Bottom;
        }

        public struct ResourceStruct
        {
            public string Name;
            public string SuccessMessage;
            public string EmptyMessage;
            public int ResourceType;
            public int ResourceImage;
            public int ExhaustedImage;
            public int ExpReward;
            public int ItemReward;
            public int LvlRequired;
            public int ToolRequired;
            public int Health;
            public int RespawnTime;
            public bool Walkthrough;
            public int Animation;
        }

        public struct SkillStruct
        {
            public string Name;
            public byte Type;
            public int MpCost;
            public int LevelReq;
            public int AccessReq;
            public int JobReq;
            public int CastTime;
            public int CdTime;
            public int Icon;
            public int Map;
            public int X;
            public int Y;
            public byte Dir;
            public int Vital;
            public int Duration;
            public int Interval;
            public int Range;
            public bool IsAoE;
            public int AoE;
            public int CastAnim;
            public int SkillAnim;
            public int StunDuration;
            public int IsProjectile;
            public int Projectile;
            public byte KnockBack;
            public byte KnockBackTiles;
        }

        public struct ShopStruct
        {
            public string Name;
            public int BuyRate;
            public TradeItemStruct[] TradeItem;
        }

        public struct PlayerInvStruct
        {
            public int Num;
            public int Value;
            public byte Bound;
        }

        public struct PlayerSkillStruct
        {
            public int Num;
            public int CD;
        }

        public struct BankStruct
        {
            public PlayerInvStruct[] Item;
        }

        public struct TileDataStruct
        {
            public int X;
            public int Y;
            public int Tileset;
            public byte AutoTile;
        }

        public struct TileStruct
        {
            public TileDataStruct[] Layer;
            public Enum.TileType Type;
            public int Data1;
            public int Data2;
            public int Data3;
            public Enum.TileType Type2;
            public int Data1_2;
            public int Data2_2;
            public int Data3_2;
            public byte DirBlock;
        }
        public struct TileHistoryStruct
        {
            public TileStruct[,] Tile;
        }

        public struct ItemStruct
        {
            public string Name;
            public int Icon;
            public string Description;
            public byte Type;
            public byte SubType;
            public int Data1;
            public int Data2;
            public int Data3;
            public int JobReq;
            public int AccessReq;
            public int LevelReq;
            public byte Mastery;
            public int Price;
            public byte[] Add_Stat;
            public byte Rarity;
            public int Speed;
            public byte BindType;
            public byte[] Stat_Req;
            public int Animation;
            public int Paperdoll;
            public byte Stackable;
            public byte ItemLevel;
            public byte KnockBack;
            public byte KnockBackTiles;
            public int Projectile;
            public int Ammo;
        }

        public struct AnimInstanceStruct
        {
            public int Animation;
            public int X;
            public int Y;
            public int LockIndex;
            public byte LockType;
            public int[] Timer;
            public bool[] Used;
            public int[] LoopIndex;
            public int[] FrameIndex;
        }

        public struct NPCStruct
        {
            public string Name;
            public string AttackSay;
            public int Sprite;
            public byte SpawnTime;
            public int SpawnSecs;
            public byte Behaviour;
            public byte Range;
            public int[] DropChance;
            public int[] DropItem;
            public int[] DropItemValue;
            public byte[] Stat;
            public byte Faction;
            public int HP;
            public int Exp;
            public int Animation;
            public byte[] Skill;
            public byte Level;
            public int Damage;
        }

        public struct TradeItemStruct
        {
            public int Item;
            public int ItemValue;
            public int CostItem;
            public int CostValue;
        }

        public struct JobStruct
        {
            public string Name;
            public string Desc;
            public int[] Stat;
            public int MaleSprite;
            public int FemaleSprite;
            public int[] StartItem;
            public int[] StartValue;
            public int StartMap;
            public byte StartX;
            public byte StartY;
            public int BaseExp;
        }

        public struct PetStruct
        {
            public int Num;
            public string Name;
            public int Sprite;
            public int Range;
            public byte Level;
            public int MaxLevel;
            public int ExpGain;
            public byte Points;
            public byte StatType;
            public byte LevelingType;
            public byte[] Stat;
            public int[] Skill;
            public byte Evolvable;
            public int EvolveLevel;
            public int EvolveNum;
        }

        public struct AccountStruct
        {
            public string Login;
            public string Password;
            public bool Banned;
        }

        public struct PlayerStruct
        {
            public string Name;
            public byte Sex;
            public byte Job;
            public int Sprite;
            public byte Level;
            public int Exp;
            public byte Access;
            public bool PK;
            public int[] Vital;
            public byte[] Stat;
            public byte Points;
            public int[] Equipment;
            public PlayerInvStruct[] Inv;
            public PlayerSkillStruct[] Skill;
            public int Map;
            public byte X;
            public byte Y;
            public byte Dir;
            public HotbarStruct[] Hotbar;
            public byte[] Switches;
            public int[] Variables;
            public ResourceTypetruct[] GatherSkills;
            public int XOffset;
            public int YOffset;
            public byte Moving;
            public bool IsMoving;
            public byte Attacking;
            public int AttackTimer;
            public int MapGetTimer;
            public byte Steps;
            public int Emote;
            public int EmoteTimer;
            public int EventTimer;
            public PlayerQuestStruct[] Quests;
            public int GuildId;
        }

        public struct TempPlayerStruct
        {
            public bool InGame;
            public int AttackTimer;
            public int DataTimer;
            public int DataBytes;
            public int DataPackets;
            public int PartyInvite;
            public int InParty;
            public byte TargetType;
            public int Target;
            public byte PartyStarter;
            public bool GettingMap;
            public double SkillBuffer;
            public int SkillBufferTimer;
            public int[] SkillCD;
            public double InShop;
            public int StunTimer;
            public int StunDuration;
            public bool InBank;
            public int TradeRequest;
            public double InTrade;
            public PlayerInvStruct[] TradeOffer;
            public bool AcceptTrade;
            public EventMapStruct EventMap;
            public int EventProcessingCount;
            public EventProcessingStruct[] EventProcessing;
            public int StopRegenTimer;
            public byte StopRegen;
            public int TmpMap;
            public int TmpX;
            public int TmpY;
            public int PetTarget;
            public int PetTargetType;
            public int PetBehavior;
            public int GoToX;
            public int GoToY;
            public int PetStunTimer;
            public int PetStunDuration;
            public int PetAttackTimer;
            public int[] PetSkillCD;
            public SkillBufferRec PetSkillBuffer;
            public DoTStruct[] PetDoT;
            public DoTStruct[] PetHoT;
            public bool PetStopRegen;
            public int PetStopRegenTimer;
            public int Editor;
            public byte Slot;
        }

        public struct MapStruct
        {
            public string Name;
            public string Music;
            public int Revision;
            public byte Moral;
            public int Tileset;
            public int Up;
            public int Down;
            public int Left;
            public int Right;
            public int BootMap;
            public byte BootX;
            public byte BootY;
            public byte MaxX;
            public byte MaxY;
            public TileStruct[,] Tile;
            public int[] NPC;
            public int EventCount;
            public EventStruct[] Event;
            public byte Weather;
            public int Fog;
            public int WeatherIntensity;
            public byte FogOpacity;
            public byte FogSpeed;
            public bool MapTint;
            public byte MapTintR;
            public byte MapTintG;
            public byte MapTintB;
            public byte MapTintA;
            public byte Panorama;
            public byte Parallax;
            public byte Brightness;
            public int Shop;
            public bool NoRespawn;
            public bool Indoors;
            public int[] SpawnPoints; // New: Multiple spawn points
            public int[] BossNPCs; // New: Boss NPCs on map
        }

        public struct MapItemStruct
        {
            public int Num;
            public int Value;
            public byte X;
            public byte Y;
            public string PlayerName;
            public long PlayerTimer;
            public bool CanDespawn;
            public long DespawnTimer;
        }

        public struct MapNPCStruct
        {
            public int Num;
            public int Target;
            public byte TargetType;
            public int[] Vital;
            public byte X;
            public byte Y;
            public int Dir;
            public int AttackTimer;
            public int SpawnWait;
            public int StunDuration;
            public int StunTimer;
            public int SkillBuffer;
            public int SkillBufferTimer;
            public int[] SkillCD;
            public byte StopRegen;
            public int StopRegenTimer;
            public int XOffset;
            public int YOffset;
            public byte Moving;
            public byte Attacking;
            public int Steps;
        }

        public struct MapDataStruct
        {
            public MapNPCStruct[] NPC;
        }

        public struct HotbarStruct
        {
            public int Slot;
            public byte SlotType;
        }

        public struct SkillBufferRec
        {
            public int Skill;
            public int Timer;
            public int Target;
            public byte TargetType;
        }

        public struct DoTStruct
        {
            public bool Used;
            public int Skill;
            public int Timer;
            public int Caster;
            public int StartTime;
            public int AttackerType;
        }

        public struct InstancedMap
        {
            public int OriginalMap;
        }

        public struct MoveRouteStruct
        {
            public int Index;
            public int Data1;
            public int Data2;
            public int Data3;
            public int Data4;
            public int Data5;
            public int Data6;
        }

        public struct GlobalEventStruct
        {
            public int X;
            public int Y;
            public int Dir;
            public int Active;
            public int WalkingAnim;
            public int FixedDir;
            public int WalkThrough;
            public int ShowName;
            public byte Position;
            public byte GraphicType;
            public int Graphic;
            public int GraphicX;
            public int GraphicX2;
            public int GraphicY;
            public int GraphicY2;
            public int MoveType;
            public byte MoveSpeed;
            public byte MoveFreq;
            public int MoveRouteCount;
            public MoveRouteStruct[] MoveRoute;
            public int MoveRouteStep;
            public int RepeatMoveRoute;
            public int IgnoreIfCannotMove;
            public int MoveTimer;
            public int MoveRouteComplete;
            public int PatrolStep;
        }

        public struct GlobalEventsStruct
        {
            public int EventCount;
            public GlobalEventStruct[] Event;
        }

        public struct ConditionalBranchStruct
        {
            public int Condition;
            public int Data1;
            public int Data2;
            public int Data3;
            public int CommandList;
            public int ElseCommandList;
        }

        public struct EventCommandStruct
        {
            public int Index;
            public string Text1;
            public string Text2;
            public string Text3;
            public string Text4;
            public string Text5;
            public int Data1;
            public int Data2;
            public int Data3;
            public int Data4;
            public int Data5;
            public int Data6;
            public ConditionalBranchStruct ConditionalBranch;
            public int MoveRouteCount;
            public MoveRouteStruct[] MoveRoute;
        }

        public struct CommandListStruct
        {
            public int CommandCount;
            public int ParentList;
            public EventCommandStruct[] Commands;
        }

        public struct EventPageStruct
        {
            public int ChkVariable;
            public int VariableIndex;
            public int VariableCondition;
            public int VariableCompare;
            public int ChkSwitch;
            public int SwitchIndex;
            public int SwitchCompare;
            public int ChkHasItem;
            public int HasItemIndex;
            public int HasItemAmount;
            public int ChkSelfSwitch;
            public int SelfSwitchIndex;
            public int SelfSwitchCompare;
            public byte GraphicType;
            public int Graphic;
            public int GraphicX;
            public int GraphicY;
            public int GraphicX2;
            public int GraphicY2;
            public byte MoveType;
            public byte MoveSpeed;
            public byte MoveFreq;
            public int MoveRouteCount;
            public MoveRouteStruct[] MoveRoute;
            public int IgnoreMoveRoute;
            public int RepeatMoveRoute;
            public int WalkAnim;
            public int DirFix;
            public int WalkThrough;
            public int ShowName;
            public byte Trigger;
            public int CommandListCount;
            public CommandListStruct[] CommandList;
            public byte Position;
            public int X;
            public int Y;
        }

        public struct EventStruct
        {
            public string Name;
            public byte Globals;
            public int PageCount;
            public EventPageStruct[] Pages;
            public int X;
            public int Y;
            public int[] SelfSwitches;
        }

        public struct GlobalMapEventsStruct
        {
            public int EventId;
            public int PageId;
            public int X;
            public int Y;
        }

        public struct MapEventStruct
        {
            public string Name;
            public int Steps;
            public int Dir;
            public int X;
            public int Y;
            public int WalkingAnim;
            public int FixedDir;
            public int WalkThrough;
            public int ShowName;
            public byte GraphicType;
            public int GraphicX;
            public int GraphicY;
            public int GraphicX2;
            public int GraphicY2;
            public int Graphic;
            public int MovementSpeed;
            public byte Position;
            public bool Visible;
            public int EventId;
            public int PageId;
            public byte MoveType;
            public byte MoveSpeed;
            public byte MoveFreq;
            public int MoveRouteCount;
            public MoveRouteStruct[] MoveRoute;
            public int MoveRouteStep;
            public int RepeatMoveRoute;
            public int IgnoreIfCannotMove;
            public int MoveTimer;
            public int[] SelfSwitches;
            public int MoveRouteComplete;
            public int XOffset;
            public int YOffset;
            public int Moving;
            public int ShowDir;
            public int WalkAnim;
            public int DirFix;
        }

        public struct EventMapStruct
        {
            public int CurrentEvents;
            public MapEventStruct[] EventPages;
        }

        public struct EventProcessingStruct
        {
            public int Active;
            public int CurList;
            public int CurSlot;
            public int EventId;
            public int PageId;
            public int WaitingForResponse;
            public int EventMovingId;
            public int EventMovingType;
            public int ActionTimer;
            public int[] ListLeftOff;
        }

        public struct PlayerQuestStruct
        {
            public int Status; // 0=not started, 1=started, 2=completed, 3=repeatable
            public int ActualTask;
            public int CurrentCount;
        }

        public struct TaskStruct
        {
            public int Order;
            public int NPC;
            public int Item;
            public int Map;
            public int Resource;
            public int Amount;
            public string Speech;
            public string TaskLog;
            public byte QuestEnd;
            public int TaskType;
        }

        public struct ProjectileStruct
        {
            public string Name;
            public int Sprite;
            public byte Range;
            public int Speed;
            public int Damage;
        }

        public struct MapProjectileStruct
        {
            public int ProjectileNum;
            public int Owner;
            public byte OwnerType;
            public int X;
            public int Y;
            public byte Dir;
            public int Range;
            public int TravelTime;
            public int Timer;
        }

        public struct EventListStruct
        {
            public int CommandList;
            public int CommandNum;
        }

        public struct PartyStruct
        {
            public int Leader;
            public int[] Member;
            public int MemberCount;
        }

        public struct MapResourceStruct
        {
            public int ResourceCount;
            public MapResourceCacheStruct[] ResourceData;
        }

        public struct MapResourceCacheStruct
        {
            public int X;
            public int Y;
            public byte State;
            public int Timer;
            public byte Health;
        }

        public struct PictureStruct
        {
            public byte Index;
            public byte SpriteType;
            public byte xOffset;
            public byte yOffset;
            public int EventId;
        }

        public struct ControlPartStruct
        {
            public Enum.PartType Type;
            public Enum.PartOriginType Origin;
            public long Value;
            public long Slot;
        }

        public struct CSMapStruct
        {
            public CSMapDataStruct MapData;
            public CSTileStruct[,] Tile;
        }

        public struct CSTileStruct
        {
            public CSTileDataStruct[] Layer;
            public byte[] Autotile;
            public byte Type;
            public int Data1;
            public int Data2;
            public int Data3;
            public int Data4;
            public int Data5;
            public byte DirBlock;
        }

        public struct CSTileDataStruct
        {
            public int x;
            public int y;
            public int TileSet;
        }

        public struct CSMapDataStruct
        {
            public string Name;
            public string Music;
            public byte Moral;
            public int Up;
            public int Down;
            public int Left;
            public int Right;
            public int BootMap;
            public byte BootX;
            public byte BootY;
            public byte MaxX;
            public byte MaxY;
            public int Weather;
            public int WeatherIntensity;
            public int Fog;
            public int FogSpeed;
            public int FogOpacity;
            public int Red;
            public int Green;
            public int Blue;
            public int Alpha;
            public int BossNPC;
            public int[] NPC;
        }

        public struct XWMapStruct
        {
            public string Name;
            public long Revision;
            public byte Moral;
            public int Up;
            public int Down;
            public int Left;
            public int Right;
            public int Music;
            public int BootMap;
            public byte BootX;
            public byte BootY;
            public int Shop;
            public byte Indoors;
            public XWTileStruct[,] Tile;
            public long[] NPC;
            public bool Server;
            public byte Respawn;
            public byte Weather;
        }

        public struct XWTileStruct
        {
            public short Ground;
            public short Mask;
            public short MaskAnim;
            public short Mask2;
            public short Mask2Anim;
            public short Fringe;
            public short FringeAnim;
            public short Roof;
            public short Fringe2Anim;
            public Enum.XWTileType Type;
            public Enum.XWTileType Type2;
            public short Data1;
            public short Data2;
            public short Data3;
            public short Data1_2;
            public short Data2_2;
            public short Data3_2;
        }

        public struct MoralStruct
        {
            public string Name;
            public byte Color;
            public bool CanCast;
            public bool CanPK;
            public bool CanUseItem;
            public bool DropItems;
            public bool LoseExp;
            public bool CanPickupItem;
            public bool CanDropItem;
            public bool PlayerBlock;
            public bool NPCBlock;
        }

        public struct SDLayerStruct
        {
            public List<SDMapLayerStruct> MapLayer;
        }

        public struct SDMapLayerStruct
        {
            public string Name;
            public SDTileStruct Tiles;
        }

        public struct SDTileStruct
        {
            public List<SDMapTileStruct> ArrayOfMapTile;
        }

        public struct SDMapTileStruct
        {
            public int TileIndex;
        }

        public struct SDWarpPosStruct
        {
            public int X;
            public int Y;
        }

        public struct SDWarpDesStruct
        {
            public int X;
            public int Y;
        }

        public struct SDWarpDataStruct
        {
            public SDWarpPosStruct Pos;
            public SDWarpDesStruct WarpDes;
            public int MapID;
        }

        public struct SDMapStruct
        {
            public string Name;
            public string Music;
            public int Revision;
            public int Up;
            public int Down;
            public int Left;
            public int Right;
            public int Tileset;
            public int MaxX;
            public int MaxY;
            public SDWarpDataStruct Warp;
            public SDLayerStruct MapLayer;
        }

        // New Structs for Enhanced Features
        public struct QuestStruct
        {
            public string Name;
            public string Description;
            public int RewardExp;
            public int RewardItem;
            public int RewardItemValue;
            public TaskStruct[] Tasks;
            public int TaskCount;
        }

        public struct GuildStruct
        {
            public string Name;
            public int Leader;
            public List<int> Members;
            public int MaxMembers;
            public int Level;
            public int Exp;
        }

        public struct WeatherStruct
        {
            public int Type; // 0: None, 1: Rain, 2: Snow, etc.
            public int Intensity;
            public int Duration;
        }

        #endregion

        #region Miscellaneous

        public static ActionMsgStruct[] ActionMsg = new ActionMsgStruct[byte.MaxValue];
        public static BloodStruct[] Blood = new BloodStruct[byte.MaxValue];
        public static ChatStruct[] Chat = new ChatStruct[Constant.CHAT_LINES];
        public static TileStruct[,] MapTile;
        public static TileHistoryStruct[] TileHistory;
        public static AutotileStruct[,] Autotile;
        public static MapEventStruct[] MapEvents;

        public struct RectangleStruct
        {
            public int Top;
            public int Right;
            public int Bottom;
            public int Left;
        }

        public struct PointStruct
        {
            public int X;
            public int Y;
        }

        public struct QuarterTileStruct
        {
            public PointStruct[] QuarterTile;
            public byte RenderState;
            public int[] SrcX;
            public int[] SrcY;
        }

        public struct AutotileStruct
        {
            public QuarterTileStruct[] Layer;
            public QuarterTileStruct[] ExLayer;
        }

        public static PointStruct[] AutoIn = new PointStruct[5];
        public static PointStruct[] AutoNw = new PointStruct[5];
        public static PointStruct[] AutoNe = new PointStruct[5];
        public static PointStruct[] AutoSw = new PointStruct[5];
        public static PointStruct[] AutoSe = new PointStruct[5];

        public struct ChatStruct
        {
            public string Text;
            public int Color;
            public byte Channel;
            public bool Visible;
            public long Timer;
        }

        public struct SkillAnimStruct
        {
            public int skillNum;
            public int Timer;
            public int FramePointer;
        }

        public struct ChatBubbleStruct
        {
            public string Msg;
            public int Color;
            public int Target;
            public byte TargetType;
            public int Timer;
            public bool Active;
        }

        public struct ActionMsgStruct
        {
            public string Message;
            public int Created;
            public int Type;
            public int Color;
            public int Scroll;
            public int X;
            public int Y;
            public int Timer;
        }

        public struct BloodStruct
        {
            public int Sprite;
            public int Timer;
            public int X;
            public int Y;
        }

        public struct TextStruct
        {
            public string Text;
            public Color Color;
        }

        public struct WeatherParticleStruct
        {
            public int Type;
            public int X;
            public int Y;
            public int Velocity;
            public int InUse;
        }

        public struct ScriptStruct
        {
            public string[] Code;
        }

        #endregion
    }
}
