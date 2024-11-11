Imports System.Windows.Forms
Imports Core

Public Class frmEditor_Moral
#Region "Form Code"

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        MoralEditorOk()
        Dispose()
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        MoralEditorCancel()
        Dispose()
    End Sub

    Private Sub frmEditor_Moral_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        MoralEditorCancel()
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim tmpindex As Integer

        ClearMoral(GameState.EditorIndex)

        tmpindex = lstIndex.SelectedIndex
        lstIndex.Items.RemoveAt(GameState.EditorIndex - 1)
        lstIndex.Items.Insert(GameState.EditorIndex - 1, GameState.EditorIndex & ": " & Type.Moral(GameState.EditorIndex).Name)
        lstIndex.SelectedIndex = tmpindex

        MoralEditorInit()
    End Sub

    Private Sub lstIndex_Click(sender As Object, e As EventArgs) Handles lstIndex.Click
        MoralEditorInit()
    End Sub

    Private Sub frmEditor_Moral_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lstIndex.Items.Clear()

        For i = 1 To MAX_MORALS
            lstIndex.Items.Add(i & ": " & Type.Moral(i).Name)
        Next
    End Sub

    Private Sub txtName_TextChanged(sender As Object, e As EventArgs) Handles txtName.TextChanged
        Dim tmpindex As Integer

        tmpindex = lstIndex.SelectedIndex
        Type.Moral(GameState.EditorIndex).Name = Trim$(txtName.Text)
        lstIndex.Items.RemoveAt(GameState.EditorIndex - 1)
        lstIndex.Items.Insert(GameState.EditorIndex - 1, GameState.EditorIndex & ": " & Type.Moral(GameState.EditorIndex).Name)
        lstIndex.SelectedIndex = tmpindex
    End Sub

    Private Sub chkCanCast_CheckedChanged(sender As Object, e As EventArgs) Handles chkCanCast.CheckedChanged
        Type.Moral(GameState.EditorIndex).CanCast = chkCanCast.Checked
    End Sub

    Private Sub chkCanPK_CheckedChanged(sender As Object, e As EventArgs) Handles chkCanPK.CheckedChanged
        Type.Moral(GameState.EditorIndex).CanPK = chkCanPK.Checked
    End Sub

    Private Sub chkCanPickupItem_CheckedChanged(sender As Object, e As EventArgs) Handles chkCanPickupItem.CheckedChanged
        Type.Moral(GameState.EditorIndex).CanPickupItem = chkCanPickupItem.Checked
    End Sub

    Private Sub chkCanDropItem_CheckedChanged(sender As Object, e As EventArgs) Handles chkCanDropItem.CheckedChanged
        Type.Moral(GameState.EditorIndex).CanDropItem = chkCanDropItem.Checked
    End Sub

    Private Sub chkCanUseItem_CheckedChanged(sender As Object, e As EventArgs) Handles chkCanUseItem.CheckedChanged
        Type.Moral(GameState.EditorIndex).CanUseItem = chkCanUseItem.Checked
    End Sub

    Private Sub chkDropItems_CheckedChanged(sender As Object, e As EventArgs) Handles chkDropItems.CheckedChanged
        Type.Moral(GameState.EditorIndex).DropItems = chkDropItems.Checked
    End Sub

    Private Sub chkLoseExp_CheckedChanged(sender As Object, e As EventArgs) Handles chkLoseExp.CheckedChanged
        Type.Moral(GameState.EditorIndex).LoseExp = chkLoseExp.Checked
    End Sub

    Private Sub chkPlayerBlock_CheckedChanged(sender As Object, e As EventArgs) Handles chkPlayerBlock.CheckedChanged
        Type.Moral(GameState.EditorIndex).PlayerBlock = chkPlayerBlock.Checked
    End Sub

    Private Sub chkNPCBlock_CheckedChanged(sender As Object, e As EventArgs) Handles chkNPCBlock.CheckedChanged
        Type.Moral(GameState.EditorIndex).NPCBlock = chkNPCBlock.Checked
    End Sub

    Private Sub cmbColor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbColor.SelectedIndexChanged
        Type.Moral(GameState.EditorIndex).Color = cmbColor.SelectedIndex
    End Sub

#End Region

End Class