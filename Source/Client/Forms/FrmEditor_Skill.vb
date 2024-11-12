Imports System.IO
Imports System.Windows.Forms
Imports Core

Friend Class frmEditor_Skill

    Private Sub TxtName_TextChanged(sender As Object, e As EventArgs) Handles txtName.TextChanged
        Dim tmpindex As Integer
        tmpindex = lstIndex.SelectedIndex
        Skill(GameState.EditorIndex).Name = Trim$(txtName.Text)
        lstIndex.Items.RemoveAt(GameState.EditorIndex - 1)
        lstIndex.Items.Insert(GameState.EditorIndex - 1, GameState.EditorIndex & ": " & Skill(GameState.EditorIndex).Name)
        lstIndex.SelectedIndex = tmpindex
    End Sub

    Private Sub CmbType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbType.SelectedIndexChanged
        Skill(GameState.EditorIndex).Type = cmbType.SelectedIndex
    End Sub

    Private Sub NudMp_ValueChanged(sender As Object, e As EventArgs) Handles nudMp.ValueChanged
        Skill(GameState.EditorIndex).MpCost = nudMp.Value
    End Sub

    Private Sub NudLevel_ValueChanged(sender As Object, e As EventArgs) Handles nudLevel.ValueChanged
        Skill(GameState.EditorIndex).LevelReq = nudLevel.Value
    End Sub

    Private Sub CmbAccessReq_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAccessReq.SelectedIndexChanged
        Skill(GameState.EditorIndex).AccessReq = cmbAccessReq.SelectedIndex
    End Sub

    Private Sub CmbClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbJob.SelectedIndexChanged
        Skill(GameState.EditorIndex).JobReq = cmbJob.SelectedIndex
    End Sub

    Private Sub NudCast_Scroll(sender As Object, e As EventArgs) Handles nudCast.ValueChanged
        Skill(GameState.EditorIndex).CastTime = nudCast.Value
    End Sub

    Private Sub NudCool_Scroll(sender As Object, e As EventArgs) Handles nudCool.ValueChanged
        Skill(GameState.EditorIndex).CdTime = nudCool.Value
    End Sub

    Private Sub NudMap_Scroll(sender As Object, e As EventArgs) Handles nudMap.ValueChanged
        Skill(GameState.EditorIndex).Map = nudMap.Value
    End Sub

    Private Sub CmbDir_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDir.SelectedIndexChanged
        Skill(GameState.EditorIndex).Dir = cmbDir.SelectedIndex
    End Sub

    Private Sub NudX_Scroll(sender As Object, e As EventArgs) Handles nudX.ValueChanged
        Skill(GameState.EditorIndex).X = nudX.Value
    End Sub

    Private Sub NudY_Scroll(sender As Object, e As EventArgs) Handles nudY.ValueChanged
        Skill(GameState.EditorIndex).Y = nudY.Value
    End Sub

    Private Sub NudVital_Scroll(sender As Object, e As EventArgs) Handles nudVital.ValueChanged
        Skill(GameState.EditorIndex).Vital = nudVital.Value
    End Sub

    Private Sub NudDuration_Scroll(sender As Object, e As EventArgs) Handles nudDuration.ValueChanged
        Skill(GameState.EditorIndex).Duration = nudDuration.Value
    End Sub

    Private Sub NudInterval_Scroll(sender As Object, e As EventArgs) Handles nudInterval.ValueChanged
        Skill(GameState.EditorIndex).Interval = nudInterval.Value
    End Sub

    Private Sub NudRange_Scroll(sender As Object, e As EventArgs) Handles nudRange.ValueChanged
        Skill(GameState.EditorIndex).Range = nudRange.Value
    End Sub

    Private Sub ChkAOE_CheckedChanged(sender As Object, e As EventArgs) Handles chkAoE.CheckedChanged
        If chkAoE.Checked = False Then
            Skill(GameState.EditorIndex).IsAoE = False
        Else
            Skill(GameState.EditorIndex).IsAoE = True
        End If
    End Sub

    Private Sub NudAoE_Scroll(sender As Object, e As EventArgs) Handles nudAoE.ValueChanged
        Skill(GameState.EditorIndex).AoE = nudAoE.Value
    End Sub

    Private Sub CmbAnimCast_Scroll(sender As Object, e As EventArgs) Handles cmbAnimCast.SelectedIndexChanged
        Skill(GameState.EditorIndex).CastAnim = cmbAnimCast.SelectedIndex
    End Sub

    Private Sub CmbAnim_Scroll(sender As Object, e As EventArgs) Handles cmbAnim.SelectedIndexChanged
        Skill(GameState.EditorIndex).SkillAnim = cmbAnim.SelectedIndex
    End Sub

    Private Sub NudStun_Scroll(sender As Object, e As EventArgs) Handles nudStun.ValueChanged
        Skill(GameState.EditorIndex).StunDuration = nudStun.Value
    End Sub

    Private Sub lstIndex_Click(sender As Object, e As EventArgs) Handles lstIndex.Click
        SkillEditorInit()
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SkillEditorOk()
        Dispose()
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim tmpindex As Integer

        ClearSkill(GameState.EditorIndex)

        tmpindex = lstIndex.SelectedIndex
        lstIndex.Items.RemoveAt(GameState.EditorIndex - 1)
        lstIndex.Items.Insert(GameState.EditorIndex - 1, GameState.EditorIndex & ": " & Skill(GameState.EditorIndex).Name)
        lstIndex.SelectedIndex = tmpindex

        SkillEditorInit()
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        SkillEditorCancel()
        Dispose()
    End Sub

    Private Sub frmEditor_Skill_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        nudIcon.Maximum = GameState.NumSkills
        nudAoE.Maximum = Byte.MaxValue
        nudRange.Maximum = Byte.MaxValue
        nudMap.Maximum = MAX_MAPS
        lstIndex.Items.Clear()

        ' Add the names
        For i = 1 To MAX_SKILLS
            lstIndex.Items.Add(i & ": " & Trim$(Skill(i).Name))
        Next

        cmbAnimCast.Items.Clear()
        cmbAnim.Items.Clear()
        For i = 1 To MAX_ANIMATIONS
            cmbAnimCast.Items.Add(i & ": " & Type.Animation(i).Name)
            cmbAnim.Items.Add(i & ": " & Type.Animation(i).Name)
        Next

        cmbProjectile.Items.Clear()
        For i = 1 To MAX_ANIMATIONS
            cmbProjectile.Items.Add(i & ": " & Type.Projectile(i).Name)
        Next

        cmbJob.Items.Clear()
        For i = 1 To MAX_JOBS
            cmbJob.Items.Add(i & ": " & Job(i).Name.Trim)
        Next
    End Sub

    Private Sub ChkProjectile_CheckedChanged(sender As Object, e As EventArgs) Handles chkProjectile.CheckedChanged
        If chkProjectile.Checked = False Then
            Skill(GameState.EditorIndex).IsProjectile = 0
        Else
            Skill(GameState.EditorIndex).IsProjectile = 1
        End If
    End Sub

    Private Sub ScrlProjectile_Scroll(sender As System.Object, e As System.EventArgs) Handles cmbProjectile.SelectedIndexChanged
        Skill(GameState.EditorIndex).Projectile = cmbProjectile.SelectedIndex
    End Sub

    Private Sub ChkKnockBack_CheckedChanged(sender As Object, e As EventArgs) Handles chkKnockBack.CheckedChanged
        If chkKnockBack.Checked = True Then
            Skill(GameState.EditorIndex).KnockBack = 1
        Else
            Skill(GameState.EditorIndex).KnockBack = 0
        End If
    End Sub

    Private Sub CmbKnockBackTiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbKnockBackTiles.SelectedIndexChanged
        Skill(GameState.EditorIndex).KnockBackTiles = cmbKnockBackTiles.SelectedIndex
    End Sub

    Private Sub frmEditor_Skill_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        SkillEditorCancel()
    End Sub

    Private Sub btnLearn_Click(sender As Object, e As EventArgs) Handles btnLearn.Click
        SendLearnSkill(GameState.EditorIndex)
    End Sub

    Private Sub nudIcon_Click(sender As Object, e As EventArgs) Handles nudIcon.Click
        Skill(GameState.EditorIndex).Icon = nudIcon.Value
        DrawIcon()
    End Sub

    Private Sub DrawIcon()
        Dim skillNum As Integer
        skillNum = nudIcon.Value

        If skillNum < 1 Or skillNum > GameState.NumItems Then
            picSprite.BackgroundImage = Nothing
            Exit Sub
        End If

        If File.Exists(IO.Path.Combine(Core.Path.Skills, skillNum & GameState.GfxExt)) Then
            picSprite.BackgroundImage = Drawing.Image.FromFile(IO.Path.Combine(Core.Path.Skills, skillNum & GameState.GfxExt))
        Else
            picSprite.BackgroundImage = Nothing
        End If
    End Sub
End Class