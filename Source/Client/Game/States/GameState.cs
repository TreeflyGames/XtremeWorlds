using Core;
using System.Reflection.Metadata;
using Color = Microsoft.Xna.Framework.Color;
using Point = Microsoft.Xna.Framework.Point;

namespace Client
{

    public class GameState
    {
        public GameState()
        {
        }

        public static int ResolutionHeight;
        public static int ResolutionWidth;

        public static bool MapAnim;

        // Global dialogue index
        public static string diaHeader;
        public static string diaBody;
        public static string diaBody2;
        public static long diaIndex;
        public static long diaData1;
        public static long diaData2;
        public static long diaData3;
        public static long diaData4;
        public static long diaData5;
        public static string diaDataString;
        public static byte diaStyle;

        // shop
        public static long shopSelectedSlot;
        public static long shopSelectedItem;
        public static bool shopIsSelling;

        // right click menu
        public static long PlayerMenuIndex;

        // description
        public static byte descType;
        public static long descItem;
        public static byte descLastType;
        public static long descLastItem;
        public static Core.Type.Text[] Description;

        // New char
        public static long NewCharSprite;
        public static long NewCharJob;
        public static long NewCnarGender;

        // chars
        public static string[] CharName = new string[(Core.Constant.MAX_CHARS)];
        public static long[] CharSprite = new long[(Core.Constant.MAX_CHARS)];
        public static long[] CharAccess = new long[(Core.Constant.MAX_CHARS)];
        public static long[] CharJob = new long[(Core.Constant.MAX_CHARS)];
        public static byte CharNum;

        // elastic bars
        public static long[] BarWidth_NpcHP = new long[(Core.Constant.MAX_MAP_NPCS)];
        public static long[] BarWidth_PlayerHP = new long[Core.Constant.MAX_PLAYERS];
        public static long[] BarWidth_PlayerSP = new long[Core.Constant.MAX_PLAYERS];
        public static long[] BarWidth_NpcHP_Max = new long[(Core.Constant.MAX_MAP_NPCS)];
        public static long[] BarWidth_PlayerHP_Max = new long[Core.Constant.MAX_PLAYERS];
        public static long[] BarWidth_PlayerSP_Max = new long[Core.Constant.MAX_PLAYERS];
        public static long BarWidth_GuiHP;
        public static long BarWidth_GuiSP;
        public static long BarWidth_GuiEXP;
        public static long BarWidth_GuiHP_Max;
        public static long BarWidth_GuiSP_Max;
        public static long BarWidth_GuiEXP_Max;

        public static int CurrentEvents;

        // Directional blocking
        public static byte[] DirArrowX = new byte[4];
        public static byte[] DirArrowY = new byte[4];

        public static bool UseFade;
        public static int FadeType;
        public static int FadeAmount;
        public static int FlashTimer;

        // Targetting
        public static int MyTarget;
        public static int MyTargetType;

        // Chat bubble
        public static int ChatBubbleindex;

        public static string ChatShowLine;

        public static string[] MapNames = new string[Core.Constant.MAX_MAPS];

        // chat
        public static bool inSmallChat;
        public static long actChatHeight;
        public static long actChatWidth;
        public static bool ChatButtonUp;
        public static bool ChatButtonDown;
        public static long ChatScroll;
        public static long Chat_HighIndex;

        public static int EditorTileX;
        public static int EditorTileY;
        public static int EditorTileWidth;
        public static int EditorTileHeight;
        public static int EditorWarpMap;
        public static int EditorWarpX;
        public static int EditorWarpY;
        public static int EditorShop;
        public static int EditorAnimation;
        public static byte EditorAttribute;
        public static Point EditorTileSelStart;
        public static Point EditorTileSelEnd;
        public static bool CopyMap;
        public static byte TmpMaxX;
        public static byte TmpMaxY;

        // Player variables
        public static int MyIndex; // Index of actual player

        public static bool InBank;

        public static int SkillBuffer;
        public static int SkillBufferTimer;
        public static int StunDuration;
        public static int NextlevelExp;

        // Stops movement when updating a map
        public static bool CanMoveNow;

        // Controls main gameloop
        public static bool InGame;
        public static bool InMenu;

        public static bool MapData;
        public static bool PlayerData;

        // Draw map name location
        public static float DrawLocX = 10f;
        public static float DrawLocY = 0f;
        public static Color DrawMapNameColor;

        // Game direction vars
        public static bool DirUp;
        public static bool DirDown;
        public static bool DirLeft;
        public static bool DirRight;

        // Used to freeze controls when getting a new map
        public static bool GettingMap;

        // Used to check if FPS needs to be drawn
        public static bool Bfps;
        public static bool Blps;
        public static bool BLoc;

        // FPS and Time-based movement vars
        public static int ElapsedTime;

        // Mouse cursor tile location
        public static int CurX;
        public static int CurY;
        public static int CurMouseX;
        public static int CurMouseY;
        public static bool Info;

        // Game editors
        public static int MyEditorType;
        public static int EditorIndex;
        public static bool AdminPanel;

        // Spawn
        public static int SpawnNpcNum;
        public static int SpawnNpcDir;

        // Items
        public static int ItemEditorNum;
        public static int ItemEditorValue;

        // Resources
        public static int ResourceEditorNum;

        // Used for map editor heal & trap & slide tiles
        public static int MapEditorHealType;
        public static int MapEditorHealAmount;
        public static int MapEditorSlideDir;

        public static Core.Type.Rect Camera;
        public static Core.Type.Rect TileView;

        // Pinging
        public static int PingStart;
        public static int PingEnd;
        public static int Ping;

        // Indexing
        public static byte ActionMsgIndex;
        public static byte BloodIndex;

        public static byte[] TempMapData;

        public static bool ShakeTimerEnabled;
        public static int ShakeTimer;
        public static byte ShakeCount;
        public static byte LastDir;

        public static bool ShowAnimLayers;
        public static int ShowAnimTimer;

        // Stream Content
        public static int[] Item_Loaded = new int[Core.Constant.MAX_ITEMS];
        public static int[] Npc_Loaded = new int[Core.Constant.MAX_NPCS];
        public static int[] Resource_Loaded = new int[Core.Constant.MAX_RESOURCES];
        public static int[] Animation_Loaded = new int[Core.Constant.MAX_RESOURCES];
        public static int[] Skill_Loaded = new int[Core.Constant.MAX_SKILLS];
        public static int[] Shop_Loaded = new int[Core.Constant.MAX_SHOPS];
        public static int[] Pet_Loaded = new int[Core.Constant.MAX_PETS];
        public static int[] Moral_Loaded = new int[(Core.Constant.MAX_MORALS)];
        public static int[] Projectile_Loaded = new int[(Core.Constant.MAX_PROJECTILES)];

        public static int[] AnimEditorFrame = new int[2];
        public static int[] AnimEditorTimer = new int[2];

        public static double CurrentCameraX;
        public static double CurrentCameraY;

        // Number of graphic files
        public static int NumTileSets;
        public static int NumCharacters;
        public static int NumPaperdolls;
        public static int NumItems;
        public static int NumResources;
        public static int NumAnimations;
        public static int NumSkills;
        public static int NumFogs;
        public static int NumEmotes;
        public static int NumPanoramas;
        public static int NumParallax;
        public static int NumPictures;
        public static int NumInterface;
        public static int NumGradients;
        public static int NumDesigns;

        public static bool VbKeyRight;
        public static bool VbKeyLeft;
        public static bool VbKeyUp;
        public static bool VbKeyDown;
        public static bool VbKeyShift;
        public static bool VbKeyControl;
        public static bool VbKeyAlt;
        public static bool VbKeyEnter;

        public static int LastLeftClickTime;
        public const int DoubleClickTImer = 500; // Time in milliseconds for double-click detection
        public static int ClickCount;

        public const int ChatBubbleWidth = 300;

        public const long Chat_Timer = 20000L;

        public const int EffectTypeFadein = 1;
        public const int EffectTypeFadeout = 2;
        public const int EffectTypeFlash = 3;
        public const int EffectTypeFog = 4;
        public const int EffectTypeWeather = 5;
        public const int EffectTypeTint = 6;

        // Bank constants
        public const long BankTop = 28L;
        public const long BankLeft = 9L;
        public const long BankOffsetY = 6L;
        public const long BankOffsetX = 6L;
        public const long BankColumns = 10L;

        // Inventory constants
        public const long InvTop = 28L;
        public const long InvLeft = 9L;
        public const long InvOffsetY = 6L;
        public const long InvOffsetX = 6L;
        public const long InvColumns = 5L;

        // Character consts
        public const long EqTop = 315L;
        public const long EqLeft = 10L;
        public const long EqOffsetX = 8L;
        public const long EqColumns = 4L;

        // Skill constants
        public const long SkillTop = 28L;
        public const long SkillLeft = 9L;
        public const long SkillOffsetY = 6L;
        public const long SkillOffsetX = 6L;
        public const long SkillColumns = 5L;

        // Hotbar constants
        public const long HotbarTop = 0L;
        public const long HotbarLeft = 8L;
        public const long HotbarOffsetX = 40L;

        // Shop constants
        public const long ShopTop = 28L;
        public const long ShopLeft = 9L;
        public const long ShopOffsetY = 6L;
        public const long ShopOffsetX = 6L;
        public const long ShopColumns = 7L;

        // Trade
        public const long TradeTop = 0L;
        public const long TradeLeft = 0L;
        public const long TradeOffsetY = 6L;
        public const long TradeOffsetX = 6L;
        public const long TradeColumns = 5L;

        // Gfx Path and variables
        public const string GfxExt = ".png";

        public static bool MapGrid;
        public static bool EyeDropper;
        public static int TileHistoryIndex;
        public static int TileHistoryHighIndex;
        public static bool HideLayers;

        // Tile size constants
        public const int PicX = 32;
        public const int PicY = 32;

        // Sprite, item, skill size constants
        public const int SizeX = 32;
        public const int SizeY = 32;

        // Map
        public const int MaxTileHistory = 500;
        public const byte TileSize = 32; // Tile size is 32x32 pixels

        // Autotiles
        public const byte AutoInner = 1;

        public const byte AutoOuter = 2;
        public const byte AutoHorizontal = 3;
        public const byte AutoVertical = 4;
        public const byte AutoFill = 5;

        // Autotile Type
        public const byte AutotileNone = 0;

        public const byte AutotileNormal = 1;
        public const byte AutotileFake = 2;
        public const byte AutotileAnim = 3;
        public const byte AutotileCliff = 4;
        public const byte AutotileWaterfall = 5;

        // Rendering
        public const int RenderStateNone = 0;

        public const int RenderStateNormal = 0;
        public const int RenderStateAutotile = 2;

        // Map animations
        public static int WaterfallFrame;

        public static int AutoTileFrame;

        public static int NumProjectiles;
        public static bool InitProjectileEditor;

        public static int ResourceIndex;
        public static bool ResourcesInit;

        public static Core.Type.WeatherParticle[] WeatherParticle = new Core.Type.WeatherParticle[Core.Constant.MAX_WEATHER_PARTICLES];

        public static int FogOffsetX;
        public static int FogOffsetY;

        public static int CurrentWeather;
        public static int CurrentWeatherIntensity;
        public static int CurrentFog;
        public static int CurrentFogSpeed;
        public static int CurrentFogOpacity;
        public static int CurrentTintR;
        public static int CurrentTintG;
        public static int CurrentTintB;
        public static int CurrentTintA;
        public static int DrawThunder;

        public static int InShop; // is the player in a shop?
        public static byte ShopAction; // stores the current shop action

        public static int MapEditorTab;
        public static int CurLayer;
        public static int CurAutotileType;
        public static int CurTileset;

        // Editors
        public static bool InitEditor;
        public static bool InitMapEditor;
        public static bool InitItemEditor;
        public static bool InitResourceEditor;
        public static bool InitNpcEditor;
        public static bool InitSkillEditor;
        public static bool InitShopEditor;
        public static bool InitAnimationEditor;
        public static bool InitJobEditor;
        public static bool InitMoralEditor;
        public static bool InitAdminForm;
        public static bool InitMapReport;
        public static bool InitEventEditor;
        public static bool InitScriptEditor;

        // Editor edited items array
        public static bool[] Item_Changed = new bool[Core.Constant.MAX_ITEMS];
        public static bool[] Npc_Changed = new bool[Core.Constant.MAX_NPCS];
        public static bool[] Resource_Changed = new bool[Core.Constant.MAX_RESOURCES];
        public static bool[] Animation_Changed = new bool[Core.Constant.MAX_ANIMATIONS];
        public static bool[] Skill_Changed = new bool[Core.Constant.MAX_SKILLS];
        public static bool[] Shop_Changed = new bool[Core.Constant.MAX_SHOPS];
        public static bool[] Pet_Changed = new bool[Core.Constant.MAX_PETS];
        public static bool[] Job_Changed = new bool[(Core.Constant.MAX_JOBS)];
        public static bool[] Moral_Changed = new bool[(Core.Constant.MAX_MORALS)];
        public static bool[] ProjectileChanged = new bool[Core.Constant.MAX_PROJECTILES];
    }
}