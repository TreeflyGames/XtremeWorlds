Public Module Enumerations

    ''' <Summary> Text Color Contstant </Summary>
    Enum ColorType As Byte
        Black
        Blue
        Green
        Cyan
        Red
        Magenta
        Brown
        Gray
        DarkGray
        BrightBlue
        BrightGreen
        BrightCyan
        BrightRed
        Pink
        Yellow
        White
    End Enum

    ''' <Summary> Quick Access/Constant Color References </Summary>
    Enum QColorType As Byte
        SayColor = ColorType.White
        GlobalColor = ColorType.BrightBlue
        BroadcastColor = ColorType.White
        TellColor = ColorType.BrightGreen
        EmoteColor = ColorType.BrightCyan
        AdminColor = ColorType.BrightCyan
        HelpColor = ColorType.BrightBlue
        WhoColor = ColorType.BrightBlue
        JoinLeftColor = ColorType.Gray
        NpcColor = ColorType.Brown
        AlertColor = ColorType.BrightRed
        NewMapColor = ColorType.BrightBlue
    End Enum

    ''' <Summary> Sex Constant </Summary>
    Enum SexType As Byte
        Male = 1
        Female
    End Enum

    ''' <Summary> Map Moral Constant </Summary>
    Enum MapMoralType As Byte
        Danger = 1
        Safe
        Indoors
    End Enum

    ''' <Summary> Tile Constant </Summary>
    Enum TileType As Byte
        None = 0
        Blocked
        Warp
        Item
        NpcAvoid
        Resource
        NpcSpawn
        Shop
        Bank
        Heal
        Trap
        Light
        Animation

        Count
    End Enum

    ''' <Summary> Item Constant </Summary>
    Enum ItemType As Byte
        Equipment = 1
        Consumable
        CommonEvent
        Currency
        Skill
        Projectile
        Pet

        Count
    End Enum

    ''' <Summary> Consumable Constant </Summary>
    Enum ConsumableType As Byte
        HP = 1
        MP
        Sp
        Exp
    End Enum

    ''' <Summary> Direction Constant </Summary>
    Enum DirectionType As Byte
        Up = 1
        Down
        Left
        Right
    End Enum

    ''' <Summary> Movement Constant </Summary>
    Enum MovementType As Byte
        Standing = 1
        Walking
        Running
    End Enum

    ''' <Summary> Admin Constant </Summary>
    Enum AdminType As Byte
        Player = 1
        Moderator
        Mapper
        Developer
        Creator
    End Enum

    ''' <Summary> Npc Behavior Constant </Summary>
    Enum NpcBehavior As Byte
        AttackOnSight = 1
        AttackWhenAttacked
        Friendly
        ShopKeeper
        Guard
        Quest
    End Enum

    ''' <Summary> Skill Constant </Summary>
    Enum SkillType As Byte
        DamageHp = 1
        DamageMp
        HealHp
        HealMp
        Warp
        Pet
    End Enum

    ''' <Summary> Target Constant </Summary>
    Enum TargetType As Byte
        Player = 1
        Npc
        [Event]
        Pet
    End Enum

    ''' <Summary> Action Message Constant </Summary>
    Enum ActionMsgType As Byte
        [Static] = 1
        Scroll
        Screen
    End Enum

    ''' <Summary> Stats used by Players, Npcs and Job </Summary>
    Public Enum StatType As Byte
        Strength = 1
        Endurance
        Vitality
        Luck
        Intelligence
        Spirit

        Count
    End Enum

    ''' <Summary> Vitals used by Players, Npcs, and Job </Summary>
    Public Enum VitalType As Byte
        HP = 1
        MP
        SP

        Count
    End Enum

    ''' <Summary> Equipment used by Players </Summary>
    Public Enum EquipmentType As Byte
        Weapon = 1
        Armor
        Helmet
        Shield
        Shoes
        Gloves

        Count
    End Enum

    ''' <Summary> Layers in a map </Summary>
    Public Enum LayerType As Byte
        Ground = 1
        Mask
        Cover
        Fringe
        Roof

        Count
    End Enum

    ''' <Summary> Resource Skills </Summary>
    Public Enum ResourceType As Byte
        Herbing = 1
        Woodcutting
        Mining
        Fishing

        Count
    End Enum

    Public Enum RarityType
        Broken = 1
        Common
        Uncommon
        Rare
        Epic
    End Enum

    Public Enum WeatherType
        None = 0
        Rain
        Snow
        Hail
        Sandstorm
        Storm
        Fog
    End Enum

    Public Enum QuestType
        Slay = 1
        Collect
        Talk
        Reach
        Give
        Kill
        Gather
        Fetch
        TalkEvent
    End Enum

    Public Enum QuestStatusType
        NotStarted = 1
        Started
        Completed
        Repeatable
    End Enum

    Public Enum MoveRouteOpts
        MoveUp = 1
        MoveDown
        MoveLeft
        MoveRight
        MoveRandom
        MoveTowardsPlayer
        MoveAwayFromPlayer
        StepForward
        StepBack
        Wait100Ms
        Wait500Ms
        Wait1000Ms
        TurnUp
        TurnDown
        TurnLeft
        TurnRight
        Turn90Right
        Turn90Left
        Turn180
        TurnRandom
        TurnTowardPlayer
        TurnAwayFromPlayer
        SetSpeed8XSlower
        SetSpeed4XSlower
        SetSpeed2XSlower
        SetSpeedNormal
        SetSpeed2XFaster
        SetSpeed4XFaster
        SetFreqLowest
        SetFreqLower
        SetFreqNormal
        SetFreqHigher
        SetFreqHighest
        WalkingAnimOn
        WalkingAnimOff
        DirFixOn
        DirFixOff
        WalkThroughOn
        WalkThroughOff
        PositionBelowPlayer
        PositionWithPlayer
        PositionAbovePlayer
        ChangeGraphic
    End Enum

    ' Event Types
    Public Enum EventType
        ' Message
        AddText = 1

        ShowText
        ShowChoices

        ' Game Progression
        PlayerVar

        PlayerSwitch
        SelfSwitch

        ' Flow Control
        Condition

        ExitProcess

        ' Player
        ChangeItems

        RestoreHP
        RestoreMP
        LevelUp
        ChangeLevel
        ChangeSkills
        ChangeJob
        ChangeSprite
        ChangeSex
        ChangePk

        ' Movement
        WarpPlayer

        SetMoveRoute

        ' Character
        PlayAnimation

        ' Music and Sounds
        PlayBgm

        FadeoutBgm
        PlaySound
        StopSound

        'Etc...
        CustomScript

        SetAccess

        'Shop/Bank
        OpenBank

        OpenShop

        'New
        GiveExp

        ShowChatBubble
        Label
        GotoLabel
        SpawnNpc
        FadeIn
        FadeOut
        FlashWhite
        SetFog
        SetWeather
        SetTint
        Wait
        ShowPicture
        HidePicture
        WaitMovement
        HoldPlayer
        ReleasePlayer
    End Enum

    Public Enum CommonEventType
        Switch = 1
        Variable
        Key
        CustomScript
    End Enum

    Public Enum EditorType
        Item = 1
        Map
        NPC
        Skill
        Shop
        Resource
        Animation
        Pet
        Quest
        Job
        Projectile
    End Enum

    Public Enum Quadrant
        NE = 1
        SE = 2
        SW = 3
        NW = 4
    End Enum

    Public Enum WrapMode
        Characters
        Font
    End Enum

    Public Enum WrapType
        None
        BreakWord
        Whitespace
        Smart
    End Enum

End Module