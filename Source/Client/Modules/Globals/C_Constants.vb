Imports System.Drawing
Imports Mirage.Basic.Engine.Types

Module C_Constants

    'Chatbubble
    Friend Const ChatBubbleWidth As Integer = 100

    Friend Const EffectTypeFadein As Integer = 1
    Friend Const EffectTypeFadeout As Integer = 2
    Friend Const EffectTypeFlash As Integer = 3
    Friend Const EffectTypeFog As Integer = 4
    Friend Const EffectTypeWeather As Integer = 5
    Friend Const EffectTypeTint As Integer = 6

    ' Font variables
    Friend Const Georgia As String = "Georgia.ttf"
    Friend Const Rockwell As String = "ROCK.TTF"
    Friend Const FontSize As Byte = 11

    ' Log Path and variables
    Friend Const LogDebug As String = "debug.txt"

    ' Gfx Path and variables
    Friend Const GfxExt As String = ".png"

    ' Menu states
    Friend Const MenuStateLogin As Byte = 1
    Friend Const MenuStateGetchars As Byte = 2
    Friend Const MenuStateNewchar As Byte = 3
    Friend Const MenuStateAddchar As Byte = 4
    Friend Const MenuStateDelchar As Byte = 5
    Friend Const MenuStateUsechar As Byte = 6
    Friend Const MenuStateInit As Byte = 7

    Friend MapGrid As Boolean
    Friend EyeDropper As Boolean
    Friend HistoryIndex As Integer
    Friend HideLayers As Boolean
    Friend Night As Boolean

    ' Speed moving vars
    Friend Const WalkSpeed As Byte = 6

    Friend Const RunSpeed As Byte = 12

    ' Tile size constants
    Friend Const PicX As Integer = 32

    Friend Const PicY As Integer = 32

    ' Sprite, item, skill size constants
    Friend Const SizeX As Integer = 32

    Friend Const SizeY As Integer = 32

    ' ********************************************************
    ' * The values below must match with the server's values *
    ' ********************************************************

    ' Map constants
    Friend ScreenMapx As Byte = 35

    Friend ScreenMapy As Byte = 26

    Friend ItemRarityColor0 = SFML.Graphics.Color.White ' white
    Friend ItemRarityColor1 = New SFML.Graphics.Color(102, 255, 0) ' green
    Friend ItemRarityColor2 = New SFML.Graphics.Color(73, 151, 208) ' blue
    Friend ItemRarityColor3 = New SFML.Graphics.Color(255, 0, 0) ' red
    Friend ItemRarityColor4 = New SFML.Graphics.Color(159, 0, 197) ' purple
    Friend ItemRarityColor5 = New SFML.Graphics.Color(255, 215, 0) ' gold

    Public EditorTileX As Integer
    Public EditorTileY As Integer
    Public EditorTileWidth As Integer
    Public EditorTileHeight As Integer
    Public EditorWarpMap As Integer
    Public EditorWarpX As Integer
    Public EditorWarpY As Integer
    Public EditorShop As Integer
    Public EditorAnimation As Integer
    Public EditorLight As Integer
    Public EditorShadow As Byte
    Public EditorFlicker As Byte
    Public EditorTileSelStart As Point
    Public EditorTileSelEnd As Point
    Public CopyMap As Boolean
    Public TmpMaxX As Byte
    Public TmpMaxY As Byte
    Public TmpTile(,) As TileStruct
    Public TileHistory() as TileHistoryStruct
    Public TileHistoryHighIndex As Integer
    Public MaxTileHistory As Byte = 50

    Friend HalfX As Integer = ((ScreenMapx + 1) \ 2) * PicX
    Friend HalfY As Integer = ((ScreenMapy + 1) \ 2) * PicY
    Friend ScreenX As Integer = (ScreenMapx + 1) * PicX
    Friend ScreenY As Integer = (ScreenMapy + 1) * PicY

    'dialog types
    Friend Const DialogueTypeBuyhome As Byte = 1

    Friend Const DialogueTypeVisit As Byte = 2
    Friend Const DialogueTypeParty As Byte = 3
    Friend Const DialogueTypeQuest As Byte = 4
    Friend Const DialogueTypeTrade As Byte = 5
End Module