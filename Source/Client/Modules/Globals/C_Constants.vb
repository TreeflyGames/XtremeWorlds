Imports System.Drawing
Imports Core

Public Module C_Constants

    'Chatbubble
    Public Const ChatBubbleWidth As Integer = 100

    Public Const EffectTypeFadein As Integer = 1
    Public Const EffectTypeFadeout As Integer = 2
    Public Const EffectTypeFlash As Integer = 3
    Public Const EffectTypeFog As Integer = 4
    Public Const EffectTypeWeather As Integer = 5
    Public Const EffectTypeTint As Integer = 6

    ' Font variables
    Public Const Georgia As String = "Georgia.ttf"
    Public Const Arial As String = "Arial.ttf"
    Public Const Verdana As String = "Verdana.ttf"
    Public Const FontSize As Byte = 10

    ' Chat variables
    Public Const ChatLines As Long = 200
    Public Const ChatWidth As Long = 316

    ' Gfx Path and variables
    Public Const GfxExt As String = ".png"

    Public MapGrid As Boolean
    Public EyeDropper As Boolean
    Public HistoryIndex As Integer
    Public HideLayers As Boolean
    Public Night As Boolean

    ' Speed moving vars
    Public Const WalkSpeed As Byte = 6

    Public Const RunSpeed As Byte = 12

    ' Tile size constants
    Public Const PicX As Integer = 32

    Public Const PicY As Integer = 32

    ' Sprite, item, skill size constants
    Public Const SizeX As Integer = 32

    Public Const SizeY As Integer = 32

    ' ********************************************************
    ' * The values below must match with the server's values *
    ' ********************************************************

    ' Map constants
    Public ScreenMapx As Byte = 35

    Public ScreenMapy As Byte = 26

    Public ItemRarityColor0 = SFML.Graphics.Color.White ' white
    Public ItemRarityColor1 = New SFML.Graphics.Color(102, 255, 0) ' green
    Public ItemRarityColor2 = New SFML.Graphics.Color(73, 151, 208) ' blue
    Public ItemRarityColor3 = New SFML.Graphics.Color(255, 0, 0) ' red
    Public ItemRarityColor4 = New SFML.Graphics.Color(159, 0, 197) ' purple
    Public ItemRarityColor5 = New SFML.Graphics.Color(255, 215, 0) ' gold

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
    Public TileHistoryHighIndex As Integer
    Public MaxTileHistory As Byte = 50

    Public HalfX As Integer = ((ScreenMapx + 1) \ 2) * PicX
    Public HalfY As Integer = ((ScreenMapy + 1) \ 2) * PicY
    Public ScreenX As Integer = (ScreenMapx + 1) * PicX
    Public ScreenY As Integer = (ScreenMapy + 1) * PicY

    'dialog types
    Public Const DialogueTypeBuyhome As Byte = 1

    Public Const DialogueTypeVisit As Byte = 2
    Public Const DialogueTypeParty As Byte = 3
    Public Const DialogueTypeQuest As Byte = 4
    Public Const DialogueTypeTrade As Byte = 5
End Module