
Imports System.Windows.Forms
Imports Core
Module UpdateUI

#Region "Defines"

    Friend GameDestroyed As Boolean

    Friend VbKeyRight As Boolean
    Friend VbKeyLeft As Boolean
    Friend VbKeyUp As Boolean
    Friend VbKeyDown As Boolean
    Friend VbKeyShift As Boolean
    Friend VbKeyControl As Boolean
    Friend VbKeyAlt As Boolean
    Friend VbKeyEnter As Boolean

    Friend SkillDescName As String
    Friend SkillDescVital As String
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

    'right click menu
    Friend RClickname As String

    Friend RClickX As Integer
    Friend RClickY As Integer

#End Region

    Sub UpdateUi()

        If InitEventEditorForm Then
            With frmEditor_Event.Instance
                .Show()
            End With
            InitEventEditorForm = False
        End If

        If GameState.InitAdminForm Then
            With FrmAdmin.Instance
                .Show()
            End With
            GameState.InitAdminForm = False
        End If

        If GameState.InitMapReport Then
            For i = 1 To MapNames.Length - 1
                Dim item1 As New ListViewItem(i.ToString)
                item1.SubItems.Add(Type.MapNames(i))
                FrmAdmin.Instance.lstMaps.Items.Add(item1)
            Next
            GameState.InitMapReport = False
        End If

        If GameState.InitMapEditor Then
            With frmEditor_Map.Instance
                GameState.MyEditorType = EditorType.Map
                GameState.EditorIndex = 1
                .Show()
                .MapEditorInit()
            End With
            GameState.InitMapEditor = False
        End If

        If GameState.InitPetEditor Then
            With frmEditor_Pet.Instance
                GameState.MyEditorType = EditorType.Pet
                GameState.EditorIndex = 1
                .Show()
                .lstIndex.SelectedIndex = 0
                PetEditorInit()
            End With
            GameState.InitPetEditor = False
        End If

        If GameState.InitAnimationEditor Then
            With frmEditor_Animation.Instance
                GameState.MyEditorType = EditorType.Animation
                GameState.EditorIndex = 1
                .Show()
                .lstIndex.SelectedIndex = 0
                AnimationEditorInit()
            End With
            GameState.InitAnimationEditor = False
        End If

        If GameState.InitItemEditor Then
            With frmEditor_item.Instance
                GameState.MyEditorType = EditorType.Item
                GameState.EditorIndex = 1
                .Show()
                .lstIndex.SelectedIndex = 0
                ItemEditorInit()
            End With
            GameState.InitItemEditor = False
        End If

        If GameState.InitJobEditor Then
            With frmEditor_Job.Instance
                GameState.MyEditorType = EditorType.Job
                GameState.EditorIndex = 1
                .Show()
                .lstIndex.SelectedIndex = 0

                JobEditorInit()
            End With
            GameState.InitJobEditor = False
        End If

        If GameState.InitMoralEditor Then
            With frmEditor_Moral.Instance
                GameState.MyEditorType = EditorType.Moral
                GameState.EditorIndex = 1
                .Show()
                .lstIndex.SelectedIndex = 0
                MoralEditorInit()
            End With
            GameState.InitMoralEditor = False
        End If

        If GameState.InitResourceEditor Then
            With frmEditor_Resource.Instance
                GameState.MyEditorType = EditorType.Resource
                GameState.EditorIndex = 1
                .Show()
                .lstIndex.SelectedIndex = 0
                ResourceEditorInit()
            End With
            GameState.InitResourceEditor = False
        End If

        If GameState.InitNPCEditor Then
            With frmEditor_NPC.Instance
                GameState.MyEditorType = EditorType.NPC
                GameState.EditorIndex = 1
                .Show()
                .lstIndex.SelectedIndex = 0
                NpcEditorInit()
            End With
            GameState.InitNPCEditor = False
        End If

        If GameState.InitSkillEditor Then
            With frmEditor_Skill.Instance
                GameState.MyEditorType = EditorType.Skill
                GameState.EditorIndex = 1
                .Show()
                .lstIndex.SelectedIndex = 0
                SkillEditorInit()
            End With
            GameState.InitSkillEditor = False
        End If

        If GameState.InitShopEditor Then
            With frmEditor_Shop.Instance
                GameState.MyEditorType = EditorType.Shop
                GameState.EditorIndex = 1
                .Show()
                .lstIndex.SelectedIndex = 0
                ShopEditorInit()
            End With
            GameState.InitShopEditor = False
        End If

        If GameState.InitProjectileEditor Then
            With frmEditor_Projectile.Instance
                GameState.MyEditorType = EditorType.Projectile
                GameState.EditorIndex = 1
                .Show()
                .lstIndex.SelectedIndex = 0
                ProjectileEditorInit()
            End With

            GameState.InitProjectileEditor = False
        End If
    End Sub

End Module