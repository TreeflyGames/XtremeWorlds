
namespace Core
{
    public class Enum
    {

        /// <Summary> Text Color Contstant </Summary>
        public enum ColorType : byte
        {
            Black,
            Blue,
            Green,
            Cyan,
            Red,
            Magenta,
            Brown,
            Gray,
            DarkGray,
            BrightBlue,
            BrightGreen,
            BrightCyan,
            BrightRed,
            Pink,
            Yellow,
            White
        }

        /// <Summary> Sex Constant </Summary>
        public enum SexType : byte
        {
            Male,
            Female
        }

        /// <Summary> Tile Constant </Summary>
        public enum TileType : byte
        {
            None,
            Blocked,
            Warp,
            Item,
            NPCAvoid,
            Resource,
            NPCSpawn,
            Shop,
            Bank,
            Heal,
            Trap,
            Animation,
            NoXing,
            Key,
            KeyOpen,
            Door,
            WalkThrough,
            Arena,
            Roof,
            Count
        }

        public enum XWTileType : byte
        {
            None = 0,
            Block = 1,
            Direction_Block = 15,
            Door = 2,
            Warp = 10,
            Item = 3,
            NPC_Avoid = 4,
            Key = 5,
            Key_Open = 6,
            Heal = 8,
            Damage = 7,
            Sign = 11,
            Shop = 13,
            NPC = 12,
            No_Xing = 19,
            Walkthru = 20,
            Arena = 9,
            Roof = 21
        }

        /// <Summary> Item Constant </Summary>
        public enum ItemType : byte
        {
            Equipment,
            Consumable,
            Event,
            Currency,
            Skill,
            Projectile,
            Pet,

            Count
        }

        /// <Summary> Consume Constant </Summary>
        public enum ConsumableType : byte
        {
            HP,
            MP,
            SP,
            Exp
        }

        /// <Summary> Sub Constant </Summary>
        public enum ItemSubType : byte
        {
            Weapon,
            Armor,
            Helmet,
            Shield,
            Shoes,
            Gloves,
            AddHP,
            AddMP,
            AddSP,
            SubHP,
            SubMP,
            SubSP,
            Exp,
            CommonEvent,
            Currency,
            Skill,

            Count
        }

        /// <Summary> Direction Constant </Summary>
        public enum DirectionType : byte
        {
            Up,
            Right,
            Down,
            Left,
            UpRight,
            UpLeft,
            DownLeft,
            DownRight
        }

        /// <Summary> Movement Constant </Summary>
        public enum MovementType : byte
        {
            Standing,
            Walking,
            Running
        }

        /// <Summary> Admin Constant </Summary>
        public enum AccessType : byte
        {
            None,
            Player,
            Moderator,
            Mapper,
            Developer,
            Owner
        }

        /// <Summary> NPC Behavior Constant </Summary>
        public enum NPCBehavior : byte
        {
            AttackOnSight,
            AttackWhenAttacked,
            Friendly,
            ShopKeeper,
            Guard,
            Quest
        }

        /// <Summary> Skill Constant </Summary>
        public enum SkillType : byte
        {
            DamageHp,
            DamageMp,
            HealHp,
            HealMp,
            Warp,
            Pet
        }

        /// <Summary> Target Constant </Summary>
        public enum TargetType : byte
        {
            None,
            Player,
            NPC,
            Event,
            Pet
        }

        /// <Summary> Action Message Constant </Summary>
        public enum ActionMsgType : byte
        {
            Static,
            Scroll,
            Screen
        }

        /// <Summary> Stats used by Players, NPCs and Job </Summary>
        public enum StatType : byte
        {
            Strength,
            Vitality,
            Luck,
            Intelligence,
            Spirit,

            Count
        }

        /// <Summary> Vitals used by Players, NPCs, and Job </Summary>
        public enum VitalType : byte
        {
            HP,
            SP,

            Count
        }

        /// <Summary> Equipment used by Players </Summary>
        public enum EquipmentType : byte
        {
            Weapon,
            Armor,
            Helmet,
            Shield,

            Count
        }

        /// <Summary> Layers in a map </Summary>
        public enum LayerType : byte
        {
            Ground,
            Mask,
            MaskAnim,
            Cover,
            CoverAnim,
            Fringe,
            FringeAnim,
            Roof,
            RoofAnim,
            Count
        }

        /// <Summary> Resource Skills </Summary>
        public enum ResourceType : byte
        {
            Herb,
            Woodcut,
            Mine,
            Fish,
            Count
        }

        public enum RarityType
        {
            Broken,
            Common,
            Uncommon,
            Rare,
            Epic
        }

        public enum Weather
        {
            None,
            Rain,
            Snow,
            Hail,
            Sandstorm,
            Storm,
            Fog
        }

        public enum MoveRouteOpts
        {
            MoveUp,
            MoveDown,
            MoveLeft,
            MoveRight,
            MoveRandom,
            MoveTowardsPlayer,
            MoveAwayFromPlayer,
            StepForward,
            StepBack,
            Wait100Ms,
            Wait500Ms,
            Wait1000Ms,
            TurnUp,
            TurnDown,
            TurnLeft,
            TurnRight,
            Turn90Right,
            Turn90Left,
            Turn180,
            TurnRandom,
            TurnTowardPlayer,
            TurnAwayFromPlayer,
            SetSpeed8XSlower,
            SetSpeed4XSlower,
            SetSpeed2XSlower,
            SetSpeedNormal,
            SetSpeed2XFaster,
            SetSpeed4XFaster,
            SetFreqLowest,
            SetFreqLower,
            SetFreqNormal,
            SetFreqHigher,
            SetFreqHighest,
            WalkingAnimOn,
            WalkingAnimOff,
            DirFixOn,
            DirFixOff,
            WalkThroughOn,
            WalkThroughOff,
            PositionBelowPlayer,
            PositionWithPlayer,
            PositionAbovePlayer,
            ChangeGraphic
        }

        // Event Type
        public enum EventType
        {
            // Message
            AddText,

            ShowText,
            ShowChoices,

            // Game Progression
            PlayerVar,

            PlayerSwitch,
            SelfSwitch,

            // Flow Control
            Condition,

            ExitProcess,

            // Player
            ChangeItems,

            RestoreHP,
            RestoreSP,
            LevelUp,
            ChangeLevel,
            ChangeSkills,
            ChangeJob,
            ChangeSprite,
            ChangeSex,
            ChangePk,

            // Movement
            WarpPlayer,

            SetMoveRoute,

            // Character
            PlayAnimation,

            // Music and Sounds
            PlayBgm,

            FadeoutBgm,
            PlaySound,
            StopSound,

            SetAccess,

            // Shop/Bank
            OpenBank,

            OpenShop,

            // New
            GiveExp,

            ShowChatBubble,
            Label,
            GoToLabel,
            SpawnNPC,
            FadeIn,
            FadeOut,
            FlashWhite,
            SetFog,
            SetWeather,
            SetTint,
            Wait,
            ShowPicture,
            HidePicture,
            WaitMovement,
            HoldPlayer,
            ReleasePlayer
        }

        public enum CommonEventType
        {
            Switch,
            Variable,
            Key,
            CustomScript
        }

        public enum EditorType
        {
            Item,
            Map,
            NPC,
            Skill,
            Shop,
            Resource,
            Animation,
            Pet,
            Quest,
            Job,
            Projectile,
            Moral,
            Script
        }

        public enum PictureType
        {
            TopLeft,
            CenterScreen,
            CenterEvent,
            CenterPlayer,
            Count
        }

        public enum QuadrantType
        {
            NE,
            SE,
            SW,
            NW
        }

        public enum WrapModeType
        {
            Characters,
            Font
        }

        public enum WrapType
        {
            BreakWord,
            Whitespace,
            Smart
        }

        public enum ControlType
        {
            Label,
            Window,
            Button,
            TextBox,
            Scrollbar,
            PictureBox,
            Checkbox,
            Combobox,
            Combomenu
        }

        public enum DesignType
        {
            // Boxes
            Wood = 1,
            Wood_Small,
            Wood_Empty,
            Green,
            Green_Hover,
            Green_Click,
            Red,
            Red_Hover,
            Red_Click,
            Blue,
            Blue_Hover,
            Blue_Click,
            Orange,
            Orange_Hover,
            Orange_Click,
            Grey,
            DescPic,
            // Windows
            Win_Black,
            Win_Norm,
            Win_NoBar,
            Win_Empty,
            Win_Desc,
            Win_Shadow,
            Win_Party,
            // Pictures
            Parchment,
            BlackOval,
            // Textboxes
            TextBlack,
            TextWhite,
            TextBlack_Sq,
            // Checkboxes
            ChkNorm,
            ChkChat,
            ChkBuying,
            ChkSelling,
            // Right-click Menu
            MenuHeader,
            MenuOption,
            // Comboboxes
            ComboNorm,
            ComboMenuNorm,
            // tile Selection
            TileBox
        }

        public enum EntState
        {
            Normal,
            Hover,
            MouseDown,
            MouseMove,
            MouseUp,
            DblClick,
            Enter,
            MouseScroll,
            KeyDown,
            KeyUp,
            Count
        }

        public enum AlignmentType
        {
            Left,
            Right,
            Center
        }

        public enum PartType
        {
            None,
            Item,
            Skill
        }

        public enum PartOriginType
        {
            None,
            Inventory,
            Skill,
            Hotbar,
            Bank
        }

        public enum FontType
        {
            None,
            Georgia,
            Arial,
            Verdana,
            Count
        }

        public enum MenuType
        {
            Main,
            Login,
            Register,
            Credits,
            Job,
            NewChar,
            Chars
        }

        public enum DialogueMsg
        {
            Connection,
            Banned,
            Kicked,
            Outdated,
            Maintenance,
            NameTaken,
            NameLength,
            NameIllegal,
            Database,
            WrongPass,
            Activate,
            MaxChar,
            DelChar,
            CreateAccount,
            MultiAccount,
            Login,
            Crash,
            Disconnect
        }

        public enum DialogueType
        {
            Trade,
            Forget,
            Party,
            LootItem,
            Alert,
            DelChar,
            DropItem,
            DepositItem,
            WithdrawItem,
            TradeAmount,
            UntradeAmount,
            ClearLayer,
            FillLayer,
            ClearAttributes,
            FillAttributes,
            ClearMap,
            Info,
            CopyMap,
            PasteMap,
        }

        public enum DialogueStyle
        {
            Okay,
            YesNo,
            Input
        }

        public enum ChatChannel
        {
            Game,
            Map,
            Broadcast,
            Party,
            Guild,
            Player,
            Count
        }

        public enum PartOriginsType
        {
            None,
            Inventory,
            Skill,
            Hotbar,
            Bank
        }

        public enum MouseButton
        {
            Left,
            Right,
            Middle
        }

        public enum RenderType
        {
            Texture,
            Font
        }

        public enum  MapTab
        {
            Tiles,
            Attributes,
            NPCs,
            Settings,
            Directions,
            Events,
            Effects
        }
    }
}