Imports System.Drawing
Imports Core

Public Module C_Constants
    Public Const ChatBubbleWidth As Integer = 300

    Public Const Chat_Timer As Long = 20000

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
    Public Const ScreenMapx As Byte = 32
    Public Const ScreenMapy As Byte = 24

    Public Const MaxTileHistory As Byte = 50

    Public Const HalfX As Integer = ((ScreenMapx + 1) \ 2) * PicX
    Public Const HalfY As Integer = ((ScreenMapy + 1) \ 2) * PicY
    Public Const ScreenX As Integer = (ScreenMapx + 1) * PicX
    Public Const ScreenY As Integer = (ScreenMapy + 1) * PicY

    'dialog types
    Public Const DialogueTypeBuyhome As Byte = 1
    Public Const DialogueTypeVisit As Byte = 2
    Public Const DialogueTypeParty As Byte = 3
    Public Const DialogueTypeQuest As Byte = 4
    Public Const DialogueTypeTrade As Byte = 5
End Module