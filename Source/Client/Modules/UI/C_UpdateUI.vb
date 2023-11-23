
Imports Core
Module C_UpdateUI

#Region "Defines"

    Friend GameDestroyed As Boolean
    Friend TxtChatAdd As String

    'Mapreport
    Friend UpdateMapnames As Boolean

    Friend Adminvisible As Boolean

    'GUI drawing
    Friend HudVisible As Boolean = True

    Friend PnlCharacterVisible As Boolean
    Friend PnlInventoryVisible As Boolean
    Friend PnlSkillsVisible As Boolean
    Friend PnlBankVisible As Boolean
    Friend PnlShopVisible As Boolean
    Friend PnlTradeVisible As Boolean
    Friend PnlEventChatVisible As Boolean
    Friend PnlRClickVisible As Boolean
    Friend OptionsVisible As Boolean

    Friend VbKeyRight As Boolean
    Friend VbKeyLeft As Boolean
    Friend VbKeyUp As Boolean
    Friend VbKeyDown As Boolean
    Friend VbKeyShift As Boolean
    Friend VbKeyControl As Boolean
    Friend VbKeyAlt As Boolean
    Friend VbKeyEnter As Boolean

    Friend PicHpWidth As Integer
    Friend PicManaWidth As Integer
    Friend PicExpWidth As Integer

    Friend LblHpText As String
    Friend LblManaText As String
    Friend LblExpText As String

    Friend UpdateCharacterPanel As Boolean

    Friend NeedToOpenShop As Boolean
    Friend NeedToOpenShopNum As Integer
    Friend NeedToOpenBank As Boolean
    Friend NeedToOpenTrade As Boolean
    Friend NeedtoCloseTrade As Boolean
    Friend NeedtoUpdateTrade As Boolean

    Friend InitMapProperties As Boolean

    Friend Tradername As String

    'UI Panels Coordinates
    Friend HudWindowX As Integer = 0

    Friend HudWindowY As Integer = 0
    Friend HudFaceX As Integer = 4
    Friend HudFaceY As Integer = 4

    'bars
    Friend HudhpBarX As Integer = 80
    Friend HudhpBarY As Integer = 19

    Friend HudmpBarX As Integer = 82
    Friend HudmpBarY As Integer = 46

    Friend HudexpBarX As Integer = 510
    Friend HudexpBarY As Integer = 750

    'Set the Chat Position

    Friend MyChatX As Integer = 1
    Friend MyChatY As Integer = FrmGame.Height - 55

    Friend ChatWindowX As Integer = 1
    Friend ChatWindowY As Integer = 705

    Friend ShowItemDesc As Boolean
    Friend ItemDescItemNum As Integer
    Friend ItemDescName As String
    Friend ItemDescDescription As String
    Friend ItemDescValue As Integer
    Friend ItemDescInfo As String
    Friend ItemDescType As String
    Friend ItemDescCost As String
    Friend ItemDescLevel As String
    Friend ItemDescSpeed As String
    Friend ItemDescStr As String
    Friend ItemDescEnd As String
    Friend ItemDescInt As String
    Friend ItemDescSpr As String
    Friend ItemDescVit As String
    Friend ItemDescLuck As String
    Friend ItemDescRarityColor As SFML.Graphics.Color
    Friend ItemDescRarityBackColor As SFML.Graphics.Color

    'Action Panel Coordinates
    Friend ActionPanelX As Integer = 942

    Friend ActionPanelY As Integer = 755

    Friend InvBtnX As Integer = 23
    Friend InvBtnY As Integer = 5
    Friend SkillBtnX As Integer = 82
    Friend SkillBtnY As Integer = 5
    Friend CharBtnX As Integer = 141
    Friend CharBtnY As Integer = 5

    Friend QuestBtnX As Integer = 23
    Friend QuestBtnY As Integer = 59
    Friend OptBtnX As Integer = 82
    Friend OptBtnY As Integer = 59
    Friend ExitBtnX As Integer = 141
    Friend ExitBtnY As Integer = 59

    'Character window Coordinates
    Friend CharWindowX As Integer = 943

    Friend CharWindowY As Integer = 475
    Friend Const EqTop As Byte = 85
    Friend Const EqLeft As Byte = 8
    Friend Const EqOffsetX As Byte = 125
    Friend Const EqOffsetY As Byte = 5
    Friend Const EqColumns As Byte = 2

    Friend StrengthUpgradeX As Integer = 370
    Friend StrengthUpgradeY As Integer = 33
    Friend EnduranceUpgradeX As Integer = 370
    Friend EnduranceUpgradeY As Integer = 53
    Friend VitalityUpgradeX As Integer = 370
    Friend VitalityUpgradeY As Integer = 72
    Friend IntellectUpgradeX As Integer = 370
    Friend IntellectUpgradeY As Integer = 91
    Friend LuckUpgradeX As Integer = 370
    Friend LuckUpgradeY As Integer = 110
    Friend SpiritUpgradeX As Integer = 370
    Friend SpiritUpgradeY As Integer = 129

    'Hotbar Coordinates
    Friend HotbarX As Integer = 489

    Friend HotbarY As Integer = 825

    'pet bar
    Friend PetbarX As Integer = 489

    Friend PetbarY As Integer = 800
    Friend PetStatX As Integer = 943
    Friend PetStatY As Integer = 575

    'Inventory window Coordinates
    Friend InvWindowX As Integer = 943

    Friend InvWindowY As Integer = 475
    Friend Const InvTop As Byte = 9
    Friend Const InvLeft As Byte = 10
    Friend Const InvOffsetY As Byte = 5
    Friend Const InvOffsetX As Byte = 6
    Friend Const InvColumns As Byte = 5

    'Skill window Coordinates
    Friend SkillWindowX As Integer = 943

    Friend SkillWindowY As Integer = 475

    ' skills constants
    Friend Const SkillTop As Byte = 9

    Friend Const SkillLeft As Byte = 10
    Friend Const SkillOffsetY As Byte = 5
    Friend Const SkillOffsetX As Byte = 6
    Friend Const SkillColumns As Byte = 5

    Friend ShowSkillDesc As Boolean
    Friend SkillDescSize As Byte
    Friend SkillDescSkillNum As Integer
    Friend SkillDescName As String
    Friend SkillDescVital As String
    Friend SkillDescInfo As String
    Friend SkillDescType As String
    Friend SkillDescCastTime As String
    Friend SkillDescCoolDown As String
    Friend SkillDescDamage As String
    Friend SkillDescAoe As String
    Friend SkillDescRange As String
    Friend SkillDescReqMp As String
    Friend SkillDescReqLvl As String
    Friend SkillDescReqClass As String
    Friend SkillDescReqAccess As String

    'dialog panel
    Friend DialogPanelVisible As Boolean

    Friend DialogPanelX As Integer = 250
    Friend DialogPanelY As Integer = 400
    Friend OkButtonX As Integer = 50
    Friend OkButtonY As Integer = 130
    Friend CancelButtonX As Integer = 200
    Friend CancelButtonY As Integer = 130

    'bank window Coordinates
    Friend BankWindowX As Integer = 319
    Friend BankWindowY As Integer = 105

    ' Bank constants
    Friend Const BankTop As Byte = 30
    Friend Const BankLeft As Byte = 16
    Friend Const BankOffsetY As Byte = 5
    Friend Const BankOffsetX As Byte = 6
    Friend Const BankColumns As Byte = 9

    ' shop coordinates
    Friend ShopWindowX As Integer = 250

    Friend ShopWindowY As Integer = 125
    Friend ShopFaceX As Integer = 60
    Friend ShopFaceY As Integer = 60

    Friend ShopButtonBuyX As Integer = 150
    Friend ShopButtonBuyY As Integer = 140

    Friend ShopButtonSellX As Integer = 150
    Friend ShopButtonSellY As Integer = 190

    Friend ShopButtonCloseX As Integer = 10
    Friend ShopButtonCloseY As Integer = 215

    ' shop constants
    Friend Const ShopTop As Byte = 46

    Friend Const ShopLeft As Integer = 271
    Friend Const ShopOffsetY As Byte = 5
    Friend Const ShopOffsetX As Byte = 5
    Friend Const ShopColumns As Byte = 6

    'trade constants
    Friend Const TradeWindowX As Integer = 200

    Friend Const TradeWindowY As Byte = 100
    Friend Const OurTradeX As Integer = 2
    Friend Const OurTradeY As Byte = 17
    Friend Const TheirTradeX As Integer = 201
    Friend Const TheirTradeY As Byte = 17

    Friend TradeButtonAcceptX As Integer = 50
    Friend TradeButtonAcceptY As Integer = 320

    Friend TradeButtonDeclineX As Integer = 250
    Friend TradeButtonDeclineY As Integer = 320

    'event chat constants
    Friend Const EventChatX As Integer = 250

    Friend Const EventChatY As Byte = 210
    Friend EventChatTextX As Integer = 113
    Friend EventChatTextY As Integer = 14

    'right click menu
    Friend RClickname As String

    Friend RClickX As Integer
    Friend RClickY As Integer

    Friend DrawChar As Boolean

    Friend CraftPanelX As Integer = 25
    Friend CraftPanelY As Integer = 25

    Friend SelHotbarSlot As Integer
    Friend SelSkillSlot As Boolean

    'hotbar constants
    Friend Const HotbarTop As Byte = 2

    Friend Const HotbarLeft As Byte = 2
    Friend Const HotbarOffsetX As Byte = 2

#End Region

    Sub UpdateUi()
        If InitPetEditor = True Then
            With frmEditor_Pet
                Editor = EditorType.Pet
                Editorindex = 1
                .Show()
                .lstIndex.SelectedIndex = 0
                PetEditorInit()
            End With
            InitPetEditor = False
        End If

        If InitAnimationEditor = True Then
            With FrmEditor_Animation
                Editor = EditorType.Animation
                Editorindex = 1
                .Show()
                .lstIndex.SelectedIndex = 0
                AnimationEditorInit()
            End With
            InitAnimationEditor = False
        End If

        If InitItemEditor = True Then
            With frmEditor_Item
                Editor = EditorType.Item
                Editorindex = 1
                .Show()
                .lstIndex.SelectedIndex = 0
                ItemEditorInit()
            End With
            InitItemEditor = False
        End If

        If InitJobEditor = True Then
            With frmEditor_Job
                Editor = EditorType.Job
                Editorindex = 1
                .Show()
                .lstIndex.SelectedIndex = 0
                JobEditorInit()
            End With
            InitJobEditor = False
        End If

        If InitResourceEditor = True Then
            With frmEditor_Resource
                Editor = EditorType.Resource
                Editorindex = 1
                .Show()
                .lstIndex.SelectedIndex = 0
                ResourceEditorInit()
            End With
            InitResourceEditor = False
        End If

        If InitNPCEditor = True Then
            With frmEditor_NPC
                Editor = EditorType.NPC
                Editorindex = 1
                .Show()
                .lstIndex.SelectedIndex = 0
                NpcEditorInit()
            End With
            InitNPCEditor = False
        End If

        If InitSkillEditor = True Then
            With frmEditor_Skill
                Editor = EditorType.Skill
                Editorindex = 1
                .Show()
                .lstIndex.SelectedIndex = 0
                SkillEditorInit()
            End With
            InitSkillEditor = False
        End If

        If InitShopEditor = True Then
            With frmEditor_Shop
                Editor = EditorType.Shop
                Editorindex = 1
                .Show()
                .lstIndex.SelectedIndex = 0
                ShopEditorInit()
            End With
            InitShopEditor = False
        End If

        If InitProjectileEditor = True Then
            With frmEditor_Projectile
                Editor = EditorType.Projectile
                Editorindex = 1
                .Show()
                .lstIndex.SelectedIndex = 0
                ProjectileEditorInit()
            End With

            InitProjectileEditor = False
        End If

        If frmEditor_Projectile.Visible Then
            EditorProjectile_DrawProjectile()
        End If
    End Sub

End Module