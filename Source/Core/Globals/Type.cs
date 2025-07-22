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
        #region  Definitions

        public struct ResourceType
        {
            public int SkillLevel;
            public int SkillCurExp;
            public int SkillNextLvlExp;
        }

        public struct Animation
        {
            public string Name;
            public string Sound;
            public int[] Sprite;
            public int[] Frames;
            public int[] LoopCount;
            public int[] LoopTime;
        }

        public struct Rect
        {
            public double Top;
            public double Left;
            public double Right;
            public double Bottom;
        }

        public struct Resource
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

        public struct Skill
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

        public struct Shop
        {
            public string Name;
            public int BuyRate;
            public TradeItem[] TradeItem;
        }

        public struct PlayerInv
        {
            public int Num;
            public int Value;
            public byte Bound;
        }

        public struct PlayerSkill
        {
            public int Num;
            public int CD;
        }

        public struct Bank
        {
            public PlayerInv[] Item;
        }

        public struct Layer
        {
            public int X;
            public int Y;
            public int Tileset;
            public byte AutoTile;
        }

        public struct Tile
        {
            public Layer[] Layer;
            public TileType Type;
            public int Data1;
            public int Data2;
            public int Data3;
            public TileType Type2;
            public int Data1_2;
            public int Data2_2;
            public int Data3_2;
            public byte DirBlock;
        }
        public struct TileHistory
        {
            public Tile[,] Tile;
        }

        public struct Item
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

        public struct AnimInstance
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

        public struct Npc
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

        public struct TradeItem
        {
            public int Item;
            public int ItemValue;
            public int CostItem;
            public int CostValue;
        }

        public struct Job
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

        public struct Pet
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

        public struct Account
        {
            public string Login;
            public string Password;
            public bool Banned;
        }

        public struct Player
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
            public PlayerInv[] Inv;
            public PlayerSkill[] Skill;
            public int Map;
            public byte X;
            public byte Y;
            public byte Dir;
            public Hotbar[] Hotbar;
            public byte[] Switches;
            public int[] Variables;
            public ResourceType[] GatherSkills;
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
            public PlayerQuest[] Quests;
            public int GuildId;
        }

        public struct TempPlayer
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
            public PlayerInv[] TradeOffer;
            public bool AcceptTrade;
            public EventMap EventMap;
            public int EventProcessingCount;
            public EventProcessing[] EventProcessing;
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
            public SkillBuffer PetSkillBuffer;
            public DoT[] PetDoT;
            public DoT[] PetHoT;
            public bool PetStopRegen;
            public int PetStopRegenTimer;
            public int Editor;
            public byte Slot;
        }

        public struct Map
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
            public Tile[,] Tile;
            public int[] Npc;
            public int EventCount;
            public Event[] Event;
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
            public int[] BossNpcs; // New: Boss Npcs on map
        }

        public struct MapItem
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

        public struct MapNpc
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

        public struct MapData
        {
            public MapNpc[] Npc;
        }

        public struct Hotbar
        {
            public int Slot;
            public byte SlotType;
        }

        public struct SkillBuffer
        {
            public int Skill;
            public int Timer;
            public int Target;
            public byte TargetType;
        }

        public struct DoT
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

        public struct MoveRoute
        {
            public int Index;
            public int Data1;
            public int Data2;
            public int Data3;
            public int Data4;
            public int Data5;
            public int Data6;
        }

        public struct GlobalEvent
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
            public MoveRoute[] MoveRoute;
            public int MoveRouteStep;
            public int RepeatMoveRoute;
            public int IgnoreIfCannotMove;
            public int MoveTimer;
            public int MoveRouteComplete;
            public int PatrolStep;
        }

        public struct GlobalEvents
        {
            public int EventCount;
            public GlobalEvent[] Event;
        }

        public struct ConditionalBranch
        {
            public int Condition;
            public int Data1;
            public int Data2;
            public int Data3;
            public int CommandList;
            public int ElseCommandList;
        }

        public struct EventCommand
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
            public ConditionalBranch ConditionalBranch;
            public int MoveRouteCount;
            public MoveRoute[] MoveRoute;
        }

        public struct CommandList
        {
            public int CommandCount;
            public int ParentList;
            public EventCommand[] Commands;
        }

        public struct EventPage
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
            public MoveRoute[] MoveRoute;
            public int IgnoreMoveRoute;
            public int RepeatMoveRoute;
            public int WalkAnim;
            public int DirFix;
            public int WalkThrough;
            public int ShowName;
            public byte Trigger;
            public int CommandListCount;
            public CommandList[] CommandList;
            public byte Position;
            public int X;
            public int Y;
        }

        public struct Event
        {
            public string Name;
            public byte Globals;
            public int PageCount;
            public EventPage[] Pages;
            public int X;
            public int Y;
            public int[] SelfSwitches;
        }

        public struct GlobalMapEvents
        {
            public int EventId;
            public int PageId;
            public int X;
            public int Y;
        }

        public struct MapEvent
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
            public MoveRoute[] MoveRoute;
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

        public struct EventMap
        {
            public int CurrentEvents;
            public MapEvent[] EventPages;
        }

        public struct EventProcessing
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

        public struct PlayerQuest
        {
            public int Status; // 0=not started, 1=started, 2=completed, 3=repeatable
            public int ActualTask;
            public int CurrentCount;
        }

        public struct Task
        {
            public int Order;
            public int Npc;
            public int Item;
            public int Map;
            public int Resource;
            public int Amount;
            public string Speech;
            public string TaskLog;
            public byte QuestEnd;
            public int TaskType;
        }

        public struct Projectile
        {
            public string Name;
            public int Sprite;
            public byte Range;
            public int Speed;
            public int Damage;
        }

        public struct MapProjectile
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

        public struct EventList
        {
            public int CommandList;
            public int CommandNum;
        }

        public struct Party
        {
            public int Leader;
            public int[] Member;
            public int MemberCount;
        }

        public struct MapResource
        {
            public int ResourceCount;
            public MapResourceCache[] ResourceData;
        }

        public struct MapResourceCache
        {
            public int X;
            public int Y;
            public byte State;
            public int Timer;
            public byte Health;
        }

        public struct Picture
        {
            public byte Index;
            public byte SpriteType;
            public byte xOffset;
            public byte yOffset;
            public int EventId;
        }

        public struct ControlPart
        {
            public DraggablePartType Type;
            public PartOrigin Origin;
            public long Value;
            public long Slot;
        }

        public struct CSMap
        {
            public CSMapData MapData;
            public CSTile[,] Tile;
        }

        public struct CSTile
        {
            public CSTileType[] Layer;
            public byte[] Autotile;
            public byte Type;
            public int Data1;
            public int Data2;
            public int Data3;
            public int Data4;
            public int Data5;
            public byte DirBlock;
        }

        public struct CSTileType
        {
            public int x;
            public int y;
            public int TileSet;
        }

        public struct CSMapData
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
            public int BossNpc;
            public int[] Npc;
        }

        public struct XWMap
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
            public XWTile[,] Tile;
            public long[] Npc;
            public bool Server;
            public byte Respawn;
            public byte Weather;
        }

        public struct XWTile
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
            public TileType Type;
            public TileType Type2;
            public short Data1;
            public short Data2;
            public short Data3;
            public short Data1_2;
            public short Data2_2;
            public short Data3_2;
        }

        public struct Moral
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
            public bool NpcBlock;
        }

        public struct SDLayer
        {
            public List<SDMapLayer> MapLayer;
        }

        public struct SDMapLayer
        {
            public string Name;
            public SDTile Tiles;
        }

        public struct SDTile
        {
            public List<SDMapTile> ArrayOfMapTile;
        }

        public struct SDMapTile
        {
            public int TileIndex;
        }

        public struct SDWarpPos
        {
            public int X;
            public int Y;
        }

        public struct SDWarpDes
        {
            public int X;
            public int Y;
        }

        public struct SDWarpData
        {
            public SDWarpPos Pos;
            public SDWarpDes WarpDes;
            public int MapID;
        }

        public struct SDMap
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
            public SDWarpData Warp;
            public SDLayer MapLayer;
        }

        // New s for Enhanced Features
        public struct Quest
        {
            public string Name;
            public string Description;
            public int RewardExp;
            public int RewardItem;
            public int RewardItemValue;
            public Task[] Tasks;
            public int TaskCount;
        }

        public struct Guild
        {
            public string Name;
            public int Leader;
            public List<int> Members;
            public int MaxMembers;
            public int Level;
            public int Exp;
        }

        public struct Weather
        {
            public int Type; // 0: None, 1: Rain, 2: Snow, etc.
            public int Intensity;
            public int Duration;
        }

        #endregion

        #region Miscellaneous

        public struct Rectangle
        {
            public int Top;
            public int Right;
            public int Bottom;
            public int Left;
        }

        public struct Point
        {
            public int X;
            public int Y;
        }

        public struct QuarterTile
        {
            public Point[] Tile;
            public byte RenderState;
            public int[] SrcX;
            public int[] SrcY;
        }

        public struct Autotile
        {
            public QuarterTile[] Layer;
            public QuarterTile[] ExLayer;
        }

        public static Point[] AutoIn = new Point[5];
        public static Point[] AutoNw = new Point[5];
        public static Point[] AutoNe = new Point[5];
        public static Point[] AutoSw = new Point[5];
        public static Point[] AutoSe = new Point[5];

        public struct Chat
        {
            public string Text;
            public int Color;
            public byte Channel;
            public bool Visible;
            public long Timer;
        }

        public struct SkillAnim
        {
            public int skillNum;
            public int Timer;
            public int FramePointer;
        }

        public struct ChatBubble
        {
            public string Msg;
            public int Color;
            public int Target;
            public byte TargetType;
            public int Timer;
            public bool Active;
        }

        public struct ActionMsg
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

        public struct Blood
        {
            public int Sprite;
            public int Timer;
            public int X;
            public int Y;
        }

        public struct Text
        {
            public string Caption;
            public System.Drawing.Color Color;
        }

        public struct WeatherParticle
        {
            public int Type;
            public int X;
            public int Y;
            public int Velocity;
            public int InUse;
        }

        public struct Script
        {
            public string[] Code;
        }

        #endregion
    }
}
