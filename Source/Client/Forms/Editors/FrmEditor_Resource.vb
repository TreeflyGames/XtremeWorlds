﻿Imports Core
Friend Class frmEditor_Resource

    Private Sub ScrlNormalPic_Scroll(sender As Object, e As EventArgs) Handles nudNormalPic.ValueChanged
        Client.EditorResource_DrawSprite()
        Type.Resource(EditorIndex).ResourceImage = nudNormalPic.Value
    End Sub

    Private Sub CmbType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbType.SelectedIndexChanged
        Type.Resource(EditorIndex).ResourceType = cmbType.SelectedIndex
    End Sub

    Private Sub ScrlExhaustedPic_Scroll(sender As Object, e As EventArgs) Handles nudExhaustedPic.ValueChanged
        Client.EditorResource_DrawSprite()
        Type.Resource(EditorIndex).ExhaustedImage = nudExhaustedPic.Value
    End Sub

    Private Sub ScrlRewardItem_Scroll(sender As Object, e As EventArgs) Handles cmbRewardItem.SelectedIndexChanged
        Type.Resource(EditorIndex).ItemReward = cmbRewardItem.SelectedIndex
    End Sub

    Private Sub ScrlRewardExp_Scroll(sender As Object, e As EventArgs) Handles nudRewardExp.ValueChanged
        Type.Resource(EditorIndex).ExpReward = nudRewardExp.Value
    End Sub

    Private Sub ScrlLvlReq_Scroll(sender As Object, e As EventArgs) Handles nudLvlReq.ValueChanged
        Type.Resource(EditorIndex).LvlRequired = nudLvlReq.Value
    End Sub

    Private Sub CmbTool_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTool.SelectedIndexChanged
        Type.Resource(EditorIndex).ToolRequired = cmbTool.SelectedIndex
    End Sub

    Private Sub ScrlHealth_Scroll(sender As Object, e As EventArgs) Handles nudHealth.ValueChanged
        Type.Resource(EditorIndex).Health = nudHealth.Value
    End Sub

    Private Sub ScrlRespawn_Scroll(sender As Object, e As EventArgs) Handles nudRespawn.ValueChanged
        Type.Resource(EditorIndex).RespawnTime = nudRespawn.Value
    End Sub

    Private Sub ScrlAnim_Scroll(sender As Object, e As EventArgs) Handles cmbAnimation.SelectedIndexChanged
        Type.Resource(EditorIndex).Animation = cmbAnimation.SelectedIndex
    End Sub

    Private Sub lstIndex_Click(sender As Object, e As EventArgs) Handles lstIndex.Click
        ResourceEditorInit()
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        ResourceEditorOk()
        Dispose()
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim tmpindex As Integer

        ClearResource(EditorIndex)

        tmpindex = lstIndex.SelectedIndex
        lstIndex.Items.RemoveAt(EditorIndex - 1)
        lstIndex.Items.Insert(EditorIndex - 1, EditorIndex & ": " & Type.Resource(EditorIndex).Name)
        lstIndex.SelectedIndex = tmpindex

        ResourceEditorInit()
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ResourceEditorCancel()
        Dispose()
    End Sub

    Private Sub frmEditor_Resource_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lstIndex.Items.Clear()

        ' Add the names
        For i = 1 To MAX_RESOURCES
            lstIndex.Items.Add(i & ": " & Type.Resource(i).Name)
        Next

        'populate combo boxes
        cmbRewardItem.Items.Clear()
        cmbRewardItem.Items.Add("None")
        For i = 1 To MAX_ITEMS
            cmbRewardItem.Items.Add(i & ": " & Type.Item(i).Name)
        Next

        cmbAnimation.Items.Clear()
        cmbAnimation.Items.Add("None")
        For i = 1 To MAX_ANIMATIONS
            cmbAnimation.Items.Add(i & ": " & Type.Animation(i).Name)
        Next

        nudExhaustedPic.Maximum = NumResources
        nudNormalPic.Maximum = NumResources
        nudRespawn.Maximum = 1000000
    End Sub

    Private Sub TxtName_TextChanged(sender As Object, e As EventArgs) Handles txtName.TextChanged
        Dim tmpindex As Integer

        tmpindex = lstIndex.SelectedIndex
        Type.Resource(EditorIndex).Name = txtName.Text
        lstIndex.Items.RemoveAt(EditorIndex - 1)
        lstIndex.Items.Insert(EditorIndex - 1, EditorIndex & ": " & Type.Resource(EditorIndex).Name)
        lstIndex.SelectedIndex = tmpindex
    End Sub

    Private Sub TxtMessage_TextChanged(sender As Object, e As EventArgs) Handles txtMessage.TextChanged
        Type.Resource(EditorIndex).SuccessMessage = txtMessage.Text
    End Sub

    Private Sub TxtMessage2_TextChanged(sender As Object, e As EventArgs) Handles txtMessage2.TextChanged
        Type.Resource(EditorIndex).EmptyMessage = txtMessage2.Text
    End Sub

    Private Sub frmEditor_Resource_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ResourceEditorCancel
    End Sub
End Class