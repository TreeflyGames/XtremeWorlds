Imports Core
Imports SFML.Graphics

Friend Class frmEditor_Skill

    Private Sub TxtName_TextChanged(sender As Object, e As EventArgs) Handles txtName.TextChanged
        Dim tmpindex As Integer
        tmpindex = lstIndex.SelectedIndex
        Type.Skill(EditorIndex).Name = txtName.Text
        lstIndex.Items.RemoveAt(EditorIndex - 1)
        lstIndex.Items.Insert(EditorIndex - 1, EditorIndex & ": " & Type.Skill(EditorIndex).Name)
        lstIndex.SelectedIndex = tmpindex
    End Sub

    Private Sub CmbType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbType.SelectedIndexChanged
        Type.Skill(EditorIndex).Type = cmbType.SelectedIndex
    End Sub

    Private Sub NudMp_ValueChanged(sender As Object, e As EventArgs) Handles nudMp.ValueChanged
        Type.Skill(EditorIndex).MpCost = nudMp.Value
    End Sub

    Private Sub NudLevel_ValueChanged(sender As Object, e As EventArgs) Handles nudLevel.ValueChanged
        Type.Skill(EditorIndex).LevelReq = nudLevel.Value
    End Sub

    Private Sub CmbAccessReq_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAccessReq.SelectedIndexChanged
        Type.Skill(EditorIndex).AccessReq = cmbAccessReq.SelectedIndex
    End Sub

    Private Sub CmbClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbJob.SelectedIndexChanged
        Type.Skill(EditorIndex).JobReq = cmbJob.SelectedIndex
    End Sub

    Private Sub NudCast_Scroll(sender As Object, e As EventArgs) Handles nudCast.ValueChanged
        Type.Skill(EditorIndex).CastTime = nudCast.Value
    End Sub

    Private Sub NudCool_Scroll(sender As Object, e As EventArgs) Handles nudCool.ValueChanged
        Type.Skill(EditorIndex).CdTime = nudCool.Value
    End Sub

    Private Sub NudMap_Scroll(sender As Object, e As EventArgs) Handles nudMap.ValueChanged
        Type.Skill(EditorIndex).Map = nudMap.Value
    End Sub

    Private Sub CmbDir_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDir.SelectedIndexChanged
        Type.Skill(EditorIndex).Dir = cmbDir.SelectedIndex
    End Sub

    Private Sub NudX_Scroll(sender As Object, e As EventArgs) Handles nudX.ValueChanged
        Type.Skill(EditorIndex).X = nudX.Value
    End Sub

    Private Sub NudY_Scroll(sender As Object, e As EventArgs) Handles nudY.ValueChanged
        Type.Skill(EditorIndex).Y = nudY.Value
    End Sub

    Private Sub NudVital_Scroll(sender As Object, e As EventArgs) Handles nudVital.ValueChanged
        Type.Skill(EditorIndex).Vital = nudVital.Value
    End Sub

    Private Sub NudDuration_Scroll(sender As Object, e As EventArgs) Handles nudDuration.ValueChanged
        Type.Skill(EditorIndex).Duration = nudDuration.Value
    End Sub

    Private Sub NudInterval_Scroll(sender As Object, e As EventArgs) Handles nudInterval.ValueChanged
        Type.Skill(EditorIndex).Interval = nudInterval.Value
    End Sub

    Private Sub NudRange_Scroll(sender As Object, e As EventArgs) Handles nudRange.ValueChanged
        Type.Skill(EditorIndex).Range = nudRange.Value
    End Sub

    Private Sub ChkAOE_CheckedChanged(sender As Object, e As EventArgs) Handles chkAoE.CheckedChanged
        If chkAoE.Checked = False Then
            Type.Skill(EditorIndex).IsAoE = False
        Else
            Type.Skill(EditorIndex).IsAoE = True
        End If
    End Sub

    Private Sub NudAoE_Scroll(sender As Object, e As EventArgs) Handles nudAoE.ValueChanged
        Type.Skill(EditorIndex).AoE = nudAoE.Value
    End Sub

    Private Sub CmbAnimCast_Scroll(sender As Object, e As EventArgs) Handles cmbAnimCast.SelectedIndexChanged
        Type.Skill(EditorIndex).CastAnim = cmbAnimCast.SelectedIndex
    End Sub

    Private Sub CmbAnim_Scroll(sender As Object, e As EventArgs) Handles cmbAnim.SelectedIndexChanged
        Type.Skill(EditorIndex).SkillAnim = cmbAnim.SelectedIndex
    End Sub

    Private Sub NudStun_Scroll(sender As Object, e As EventArgs) Handles nudStun.ValueChanged
        Type.Skill(EditorIndex).StunDuration = nudStun.Value
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

        ClearSkill(EditorIndex)

        tmpindex = lstIndex.SelectedIndex
        lstIndex.Items.RemoveAt(EditorIndex - 1)
        lstIndex.Items.Insert(EditorIndex - 1, EditorIndex & ": " & Type.Skill(EditorIndex).Name)
        lstIndex.SelectedIndex = tmpindex

        SkillEditorInit()
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        SkillEditorCancel()
        Dispose()
    End Sub

    Private Sub frmEditor_Skill_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        nudIcon.Maximum = NumSkills
        nudAoE.Maximum = Byte.MaxValue
        nudRange.Maximum = Byte.MaxValue
        nudMap.Maximum = MAX_MAPS
        lstIndex.Items.Clear()

        ' Add the names
        For i = 1 To MAX_SKILLS
            lstIndex.Items.Add(i & ": "  & Type.Skill(i).Name)
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
            cmbJob.Items.Add(i & ": " & Type.Job(i).Name)
        Next
    End Sub

    Private Sub ChkProjectile_CheckedChanged(sender As Object, e As EventArgs) Handles chkProjectile.CheckedChanged
        If chkProjectile.Checked = False Then
            Type.Skill(EditorIndex).IsProjectile = 0
        Else
            Type.Skill(EditorIndex).IsProjectile = 1
        End If
    End Sub

    Private Sub ScrlProjectile_Scroll(sender As System.Object, e As System.EventArgs) Handles cmbProjectile.SelectedIndexChanged
        Type.Skill(EditorIndex).Projectile = cmbProjectile.SelectedIndex
    End Sub

    Private Sub ChkKnockBack_CheckedChanged(sender As Object, e As EventArgs) Handles chkKnockBack.CheckedChanged
        If chkKnockBack.Checked = True Then
            Type.Skill(EditorIndex).KnockBack = 1
        Else
            Type.Skill(EditorIndex).KnockBack = 0
        End If
    End Sub

    Private Sub CmbKnockBackTiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbKnockBackTiles.SelectedIndexChanged
        Type.Skill(EditorIndex).KnockBackTiles = cmbKnockBackTiles.SelectedIndex
    End Sub

    Private Sub frmEditor_Skill_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        SkillEditorCancel()
    End Sub

    Private Sub btnLearn_Click(sender As Object, e As EventArgs) Handles btnLearn.Click
        SendLearnSkill(EditorIndex)
    End Sub

    Private Sub nudIcon_Click(sender As Object, e As EventArgs) Handles nudIcon.Click
        Type.Skill(EditorIndex).Icon = nudIcon.Value
        Client.EditorSkill_DrawIcon()
    End Sub
End Class