// Filename: CoreEnums.cs
// It's recommended to rename the file to avoid confusion with the old "Enum.cs"

namespace Core
{
    /// <summary>
    /// Specifies the display color for text.
    /// </summary>
    public enum Color : byte
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

    /// <summary>
    /// Defines character sex.
    /// </summary>
    public enum Sex : byte
    {
        Male,
        Female
    }

    /// <summary>
    /// Represents the high-level logical type of a map tile.
    /// </summary>
    public enum TileType : byte
    {
        None,
        Blocked,
        Warp,
        Item,
        NpcAvoid,
        Resource,
        NpcSpawn,
        Shop,
        Bank,
        Heal,
        Trap,
        Animation,
        NoCrossing,
        Key,
        KeyOpen,
        Door,
        WalkThrough,
        Arena,
        Roof
    }

    /// <summary>
    /// Defines special attributes or data flags for a map tile, often used for map serialization.
    /// Note the explicit integer values, which differ from the logical TileType enum.
    /// </summary>
    public enum TileAttribute : byte
    {
        None = 0,
        Block = 1,
        Door = 2,
        Item = 3,
        NpcAvoid = 4,
        Key = 5,
        KeyOpen = 6,
        Damage = 7,
        Heal = 8,
        Arena = 9,
        Warp = 10,
        Sign = 11,
        NpcSpawn = 12,
        Shop = 13,
        DirectionBlock = 15,
        NoCrossing = 19,
        WalkThrough = 20,
        Roof = 21
    }

    /// <summary>
    /// Specifies the primary category of an item.
    /// </summary>
    public enum ItemCategory : byte
    {
        Equipment,
        Consumable,
        Event,
        Currency,
        Skill,
        Projectile,
        Pet
    }

    /// <summary>
    /// Specifies the effect of a consumable item.
    /// </summary>
    public enum ConsumableEffect : byte
    {
        RestoresHealth,
        RestoresMana,
        RestoresStamina,
        GrantsExperience
    }
    
    /// <summary>
    /// Defines the sub-category of an item.
    /// NOTE: This enum mixes multiple concepts and could be a candidate for a more advanced class-based design.
    /// </summary>
    public enum ItemSubCategory : byte
    {
        // Equipment Sub-types
        Weapon,
        Armor,
        Helmet,
        Shield,
        Shoes,
        Gloves,

        // Consumable Effect Sub-types
        RestoresHealth,
        RestoresMana,
        RestoresStamina,
        DealsHealthDamage,
        DealsManaDamage,
        DealsStaminaDamage,
        GrantsExperience,
        
        // Other Sub-types
        CommonEvent,
        Currency,
        SkillBook
    }

    /// <Summary> Equipment used by Players </Summary>
    public enum Equipment : byte
    {
        Weapon,
        Armor,
        Helmet,
        Shield
    }

    /// <summary>
    /// Represents cardinal and ordinal directions.
    /// </summary>
    public enum Direction : byte
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

    /// <summary>
    /// Defines the movement state of a character.
    /// </summary>
    public enum MovementState : byte
    {
        Standing,
        Walking,
        Running
    }

    /// <summary>
    /// Defines user access levels or permissions.
    /// </summary>
    public enum AccessLevel : byte
    {
        None,
        Player,
        Moderator,
        Mapper,
        Developer,
        Owner
    }

    /// <summary>
    /// Defines the behavior patterns for Npcs.
    /// </summary>
    public enum NpcBehavior : byte
    {
        AttackOnSight,
        AttackWhenAttacked,
        Friendly,
        ShopKeeper,
        Guard,
        QuestGiver
    }

    /// <summary>
    /// Defines the primary effect of a skill.
    /// </summary>
    public enum SkillEffect : byte
    {
        DamageHealth,
        DamageMana,
        HealHealth,
        HealMana,
        Warp,
        SummonPet
    }

    /// <summary>
    /// Specifies the valid targets for an action or skill.
    /// </summary>
    public enum TargetType : byte
    {
        None,
        Player,
        Npc,
        Event,
        Pet,
        Self
    }

    /// <summary>
    /// Defines how an action message is displayed on screen.
    /// </summary>
    public enum ActionMessageType : byte
    {
        Static,
        Scroll,
        Screen
    }

    /// <summary>
    /// Represents core character stats.
    /// </summary>
    public enum Stat : byte
    {
        Strength,
        Vitality,
        Luck,
        Intelligence,
        Spirit
    }

    /// <summary>
    /// Represents character vitals (resource pools).
    /// </summary>
    public enum Vital : byte
    {
        Health,
        Mana,
        Stamina
    }

    /// <summary>
    /// Represents layers in a map render.
    /// </summary>
    public enum MapLayer : byte
    {
        Ground,
        Mask,
        MaskAnimation,
        Cover,
        CoverAnimation,
        Fringe,
        FringeAnimation,
        Roof,
        RoofAnimation
    }

    /// <summary>
    /// Defines resource gathering skills.
    /// </summary>
    public enum ResourceSkill : byte
    {
        Herbalism,
        Woodcutting,
        Mining,
        Fishing
    }
    
    /// <summary>
    /// Defines the rarity levels of items or loot.
    /// </summary>
    public enum Rarity
    {
        Broken,
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary // Added for more granularity
    }
    
    /// <summary>
    /// Defines weather conditions.
    /// </summary>
    public enum WeatherType
    {
        None,
        Rain,
        Snow,
        Hail,
        Sandstorm,
        Storm,
        Fog
    }
    
    /// <summary>
    /// Defines commands for an entity's movement route.
    /// </summary>
    public enum MoveRouteCommand
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
        Turn90DegreesRight,
        Turn90DegreesLeft,
        Turn180Degrees,
        TurnRandom,
        TurnTowardPlayer,
        TurnAwayFromPlayer,
        SetSpeed8xSlower,
        SetSpeed4xSlower,
        SetSpeed2xSlower,
        SetSpeedNormal,
        SetSpeed2xFaster,
        SetSpeed4xFaster,
        SetFrequencyLowest,
        SetFrequencyLower,
        SetFrequencyNormal,
        SetFrequencyHigher,
        SetFrequencyHighest,
        WalkingAnimationOn,
        WalkingAnimationOff,
        DirectionFixOn,
        DirectionFixOff,
        WalkThroughOn,
        WalkThroughOff,
        SetZPositionBelowPlayer,
        SetZPositionWithPlayer,
        SetZPositionAbovePlayer,
        ChangeGraphic
    }

    /// <summary>
    /// Defines commands available in the event system.
    /// </summary>
    public enum EventCommand
    {
        // Message
        AddText,
        ShowText,
        ShowChoices,
        ShowChatBubble,

        // Game Progression
        ModifyVariable,
        ModifySwitch,
        ModifySelfSwitch,

        // Flow Control
        ConditionalBranch,
        ExitEventProcess,
        Label,
        GoToLabel,
        Wait,
        WaitMovementCompletion,

        // Player
        ChangeItems,
        ChangeGold,
        RestoreHealth,
        RestoreStamina,
        RestoreMana,
        GiveExperience,
        LevelUp,
        ChangeLevel,
        ChangeSkills,
        ChangeJob,
        ChangeSprite,
        ChangeSex,
        SetPlayerKillable,
        HoldPlayer,
        ReleasePlayer,

        // Movement
        WarpPlayer,
        SetMoveRoute,
        
        // Character
        PlayAnimation,

        // Audio & Screen Effects
        PlayBgm,
        FadeOutBgm,
        PlaySound,
        StopSound,
        FadeIn,
        FadeOut,
        FlashScreen,
        SetFog,
        SetWeather,
        SetScreenTint,

        // Pictures
        ShowPicture,
        HidePicture,

        // System
        OpenBank,
        OpenShop,
        SetAccessLevel,
        SpawnNpc,
        Key,
    }

    /// <summary>
    /// Defines the trigger type for a common event.
    /// </summary>
    public enum CommonEventTrigger
    {
        Switch,
        Variable,
        ItemUsed,
        CustomScript
    }

    /// <summary>
    /// Specifies the different data editors in the toolset.
    /// </summary>
    public enum EditorType
    {
        Item,
        Map,
        Npc,
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

    /// <summary>
    /// Specifies the anchor point for a picture on the screen.
    /// </summary>
    public enum PictureOrigin
    {
        TopLeft,
        CenterScreen,
        CenterOnEvent,
        CenterOnPlayer
    }

    /// <summary>
    /// Represents a 2D quadrant.
    /// </summary>
    public enum Quadrant
    {
        Northeast,
        Southeast,
        Southwest,
        Northwest
    }

    /// <summary>
    /// Defines UI control types.
    /// </summary>
    public enum ControlType
    {
        Label,
        Window,
        Button,
        TextBox,
        Scrollbar,
        PictureBox,
        Checkbox,
        ComboBox,
        ComboMenu
    }
    
    /// <summary>
    /// Defines predefined graphical styles for UI elements.
    /// </summary>
    public enum UiDesign
    {
        // Boxes
        Wood = 1,
        WoodSmall,
        WoodEmpty,
        Green,
        GreenHover,
        GreenClick,
        Red,
        RedHover,
        RedClick,
        Blue,
        BlueHover,
        BlueClick,
        Orange,
        OrangeHover,
        OrangeClick,
        Grey,
        DescriptionPicture,
        // Windows
        WindowBlack,
        WindowNormal,
        WindowNoBar,
        WindowEmpty,
        WindowDescription,
        WindowWithShadow,
        WindowParty,
        // Pictures
        Parchment,
        BlackOval,
        // Textboxes
        TextBlack,
        TextWhite,
        TextBlackSquare,
        // Checkboxes
        CheckboxNormal,
        CheckboxChat,
        CheckboxBuying,
        CheckboxSelling,
        // Right-click Menu
        MenuHeader,
        MenuOption,
        // Comboboxes
        ComboBoxNormal,
        ComboMenuNormal,
        // Tile Selection
        TileSelectionBox
    }

    /// <summary>
    /// Represents the state of a UI entity or control.
    /// </summary>
    public enum ControlState
    {
        Normal,
        Hover,
        MouseDown,
        MouseMove,
        MouseUp,
        DoubleClick,
        FocusEnter,
        MouseScroll,
        KeyDown,
        KeyUp
    }

    /// <summary>
    /// Defines text alignment.
    /// </summary>
    public enum Alignment
    {
        Left,
        Right,
        Center
    }
    
    /// <summary>
    /// Defines the data type of a draggable UI part.
    /// </summary>
    public enum DraggablePartType
    {
        None,
        Item,
        Skill
    }

    /// <summary>
    /// Defines the origin container of a draggable UI part.
    /// </summary>
    public enum PartOrigin
    {
        None,
        Inventory,
        SkillTree,
        Hotbar,
        Bank
    }

    /// <summary>
    /// Defines available fonts.
    /// </summary>
    public enum Font
    {
        None,
        Georgia,
        Arial,
        Verdana
    }

    /// <summary>
    /// Defines the main game menus or scenes.
    /// </summary>
    public enum Menu
    {
        MainMenu,
        Login,
        Register,
        Credits,
        JobSelection,
        NewCharacter,
        CharacterSelect
    }

    /// <summary>
    /// Predefined system dialogue messages.
    /// </summary>
    public enum SystemMessage
    {
        Connection,
        Banned,
        Kicked,
        ClientOutdated,
        ServerMaintenance,
        NameTaken,
        NameLengthInvalid,
        NameContainsIllegalChars,
        DatabaseError,
        WrongPassword,
        AccountActivationRequired,
        MaxCharactersReached,
        ConfirmCharacterDeletion,
        CreateAccount,
        MultipleAccountsNotAllowed,
        Login,
        Crashed,
        Disconnected
    }

    /// <summary>
    // Specifies the purpose of a confirmation dialogue.
    /// </summary>
    public enum DialogueType
    {
        Trade,
        ForgetSkill,
        PartyInvite,
        LootConfirmation,
        Alert,
        DeleteCharacter,
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
        Information,
        CopyMap,
        PasteMap
    }

    /// <summary>
    /// Defines the button layout for a dialogue box.
    /// </summary>
    public enum DialogueStyle
    {
        Okay,
        YesNo,
        Input
    }

    /// <summary>
    /// Defines chat channels.
    /// </summary>
    public enum ChatChannel
    {
        Game,
        Map,
        Broadcast,
        Party,
        Guild,
        Private, // Renamed from Player
    }

    /// <summary>
    /// Defines mouse buttons.
    /// </summary>
    public enum MouseButton
    {
        Left,
        Right,
        Middle
    }
    
    /// <summary>
    /// Specifies the type of asset to be rendered.
    /// </summary>
    public enum RenderType
    {
        Texture,
        Font
    }

    /// <summary>
    /// Defines the tabs in the map editor.
    /// </summary>
    public enum MapEditorTab
    {
        Tiles,
        Attributes,
        Npcs,
        Settings,
        Directions,
        Events,
        Effects
    }
}
