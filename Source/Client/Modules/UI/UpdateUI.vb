
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

        if InitEventEditorForm Then
            with frmEditor_Event
                .Show()
            End With
            InitEventEditorForm = False
        End If

        If InitAdminForm Then
            With FrmAdmin
                .Show()
            End With
            InitAdminForm = False
        End If

        If InitMapReport Then
            For i = 1 To MapNames.Length - 1
                Dim item1 As New ListViewItem(i.ToString)
                item1.SubItems.Add(Type.MapNames(i))
                FrmAdmin.lstMaps.Items.Add(item1)
            Next
            InitMapReport = False
        End If

        If InitMapEditor Then
            With frmEditor_Map
                MyEditorType = EditorType.Map
                EditorIndex = 1
                .Show()
                .MapEditorInit()
            End With
            InitMapEditor = False
        End If

        If InitPetEditor Then
            With frmEditor_Pet
                MyEditorType = EditorType.Pet
                EditorIndex = 1
                .Show()
                .lstIndex.SelectedIndex = 0
                PetEditorInit()
            End With
            InitPetEditor = False
        End If

        If InitAnimationEditor Then
            With frmEditor_Animation
                MyEditorType = EditorType.Animation
                EditorIndex = 1
                .Show()
                .lstIndex.SelectedIndex = 0
                AnimationEditorInit()
            End With
            InitAnimationEditor = False
        End If

        If InitItemEditor Then
            With frmEditor_Item
                MyEditorType = EditorType.Item
                EditorIndex = 1
                .Show()
                .lstIndex.SelectedIndex = 0
                ItemEditorInit()
            End With
            InitItemEditor = False
        End If

        If InitJobEditor Then
            With frmEditor_Job
                MyEditorType = EditorType.Job
                EditorIndex = 1
                .Show()
                .lstIndex.SelectedIndex = 0
                JobEditorInit()
            End With
            InitJobEditor = False
        End If
        
        If InitMoralEditor Then
            With frmEditor_Moral
                MyEditorType = EditorType.Moral
                EditorIndex = 1
                .Show()
                .lstIndex.SelectedIndex = 0
                MoralEditorInit()
            End With
            InitMoralEditor = False
        End If

        If InitResourceEditor Then
            With frmEditor_Resource
                MyEditorType = EditorType.Resource
                EditorIndex = 1
                .Show()
                .lstIndex.SelectedIndex = 0
                ResourceEditorInit()
            End With
            InitResourceEditor = False
        End If

        If InitNPCEditor Then
            With frmEditor_NPC
                MyEditorType = EditorType.NPC
                EditorIndex = 1
                .Show()
                .lstIndex.SelectedIndex = 0
                NpcEditorInit()
            End With
            InitNPCEditor = False
        End If

        If InitSkillEditor Then
            With frmEditor_Skill
                MyEditorType = EditorType.Skill
                EditorIndex = 1
                .Show()
                .lstIndex.SelectedIndex = 0
                SkillEditorInit()
            End With
            InitSkillEditor = False
        End If

        If InitShopEditor Then
            With frmEditor_Shop
                MyEditorType = EditorType.Shop
                EditorIndex = 1
                .Show()
                .lstIndex.SelectedIndex = 0
                ShopEditorInit()
            End With
            InitShopEditor = False
        End If

        If InitProjectileEditor Then
            With frmEditor_Projectile
                MyEditorType = EditorType.Projectile
                EditorIndex = 1
                .Show()
                .lstIndex.SelectedIndex = 0
                ProjectileEditorInit()
            End With

            InitProjectileEditor = False
        End If
    End Sub

End Module