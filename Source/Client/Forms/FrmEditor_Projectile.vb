Imports System.Windows.Forms
Imports Core

Friend Class frmEditor_Projectile

    Private Sub frmEditor_Projectile_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lstIndex.Items.Clear()

        ' Add the names
        For i = 1 To MAX_PROJECTILES
            lstIndex.Items.Add(i & ": " & Type.Projectile(i).Name)
        Next
        nudPic.Maximum = GameState.NumProjectiles
    End Sub

    Private Sub lstIndex_Click(sender As Object, e As EventArgs) Handles lstIndex.Click
        ProjectileEditorInit()
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        ProjectileEditorOk()
        Dispose()
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ProjectileEditorCancel()
        Dispose()
    End Sub

    Private Sub TxtName_TextChanged(sender As System.Object, e As EventArgs) Handles txtName.TextChanged
        Dim tmpindex As Integer

        tmpindex = lstIndex.SelectedIndex
        Type.Projectile(GameState.EditorIndex).Name = Trim$(txtName.Text)
        lstIndex.Items.RemoveAt(GameState.EditorIndex - 1)
        lstIndex.Items.Insert(GameState.EditorIndex - 1, GameState.EditorIndex & ": " & Type.Projectile(GameState.EditorIndex).Name)
        lstIndex.SelectedIndex = tmpindex
    End Sub

    Private Sub NudPic_ValueChanged(sender As Object, e As EventArgs) Handles nudPic.Click
        Type.Projectile(GameState.EditorIndex).Sprite = nudPic.Value
    End Sub

    Private Sub NudRange_ValueChanged(sender As Object, e As EventArgs) Handles nudRange.Click
        Type.Projectile(GameState.EditorIndex).Range = nudRange.Value
    End Sub

    Private Sub NudSpeed_ValueChanged(sender As Object, e As EventArgs) Handles nudSpeed.Click
        Type.Projectile(GameState.EditorIndex).Speed = nudSpeed.Value
    End Sub

    Private Sub NudDamage_ValueChanged(sender As Object, e As EventArgs) Handles nudDamage.Click
        Type.Projectile(GameState.EditorIndex).Damage = nudDamage.Value
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim tmpindex As Integer

        ClearProjectile(GameState.EditorIndex)

        tmpindex = lstIndex.SelectedIndex
        lstIndex.Items.RemoveAt(GameState.EditorIndex - 1)
        lstIndex.Items.Insert(GameState.EditorIndex - 1, GameState.EditorIndex & ": " & Type.Projectile(GameState.EditorIndex).Name)
        lstIndex.SelectedIndex = tmpindex

        ProjectileEditorInit()
    End Sub

    Private Sub frmEditor_Projectile_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ProjectileEditorCancel
    End Sub
End Class