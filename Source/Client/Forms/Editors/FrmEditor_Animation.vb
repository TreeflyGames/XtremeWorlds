﻿Imports Core
Imports Microsoft.Xna.Framework.Graphics
Imports SharpDX.Direct2D1

Friend Class FrmEditor_Animation
    Private Sub NudSprite0_ValueChanged(sender As Object, e As EventArgs) Handles nudSprite0.Click
        Type.Animation(EditorIndex).Sprite(0) = nudSprite0.Value
    End Sub

    Private Sub NudSprite1_ValueChanged(sender As Object, e As EventArgs) Handles nudSprite1.Click
        Type.Animation(EditorIndex).Sprite(1) = nudSprite1.Value
    End Sub

    Private Sub NudLoopCount0_ValueChanged(sender As Object, e As EventArgs) Handles nudLoopCount0.Click
        Type.Animation(EditorIndex).LoopCount(0) = nudLoopCount0.Value
    End Sub

    Private Sub NudLoopCount1_ValueChanged(sender As Object, e As EventArgs) Handles nudLoopCount1.Click
        Type.Animation(EditorIndex).LoopCount(1) = nudLoopCount1.Value
    End Sub

    Private Sub NudFrameCount0_ValueChanged(sender As Object, e As EventArgs) Handles nudFrameCount0.Click
        Type.Animation(EditorIndex).Frames(0) = nudFrameCount0.Value
    End Sub

    Private Sub NudFrameCount1_ValueChanged(sender As Object, e As EventArgs) Handles nudFrameCount1.Click
        Type.Animation(EditorIndex).Frames(1) = nudFrameCount1.Value
    End Sub

    Private Sub NudLoopTime0_ValueChanged(sender As Object, e As EventArgs) Handles nudLoopTime0.Click
        Type.Animation(EditorIndex).LoopTime(0) = nudLoopTime0.Value
    End Sub

    Private Sub NudLoopTime1_ValueChanged(sender As Object, e As EventArgs) Handles nudLoopTime1.Click
        Type.Animation(EditorIndex).LoopTime(1) = nudLoopTime1.Value
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        AnimationEditorOk()
        Dispose()
    End Sub

    Private Sub TxtName_TextChanged(sender As Object, e As EventArgs) Handles txtName.TextChanged
        Dim tmpindex As Integer
        tmpindex = lstIndex.SelectedIndex
        Type.Animation(EditorIndex).Name = txtName.Text
        lstIndex.Items.RemoveAt(EditorIndex - 1)
        lstIndex.Items.Insert(EditorIndex - 1, EditorIndex & ": " & Type.Animation(EditorIndex).Name)
        lstIndex.SelectedIndex = tmpindex
    End Sub

    Private Sub lstIndex_Click(sender As Object, e As MouseEventArgs)
        AnimationEditorInit()
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim tmpindex As Integer

        ClearAnimation(EditorIndex)

        tmpindex = lstIndex.SelectedIndex
        lstIndex.Items.RemoveAt(EditorIndex - 1)
        lstIndex.Items.Insert(EditorIndex - 1, EditorIndex & ": " & Type.Animation(EditorIndex).Name)
        lstIndex.SelectedIndex = tmpindex

        AnimationEditorInit()
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        AnimationEditorCancel()
        Dispose()
    End Sub

    Private Sub frmEditor_Animation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lstIndex.Items.Clear()

        ' Add the names
        For i = 1 To MAX_ANIMATIONS
            lstIndex.Items.Add(i & ": " & Type.Animation(i).Name)
        Next

        ' find the music we have set
        cmbSound.Items.Clear()

        CacheSound()

        For i = 1 To UBound(SoundCache)
            cmbSound.Items.Add(SoundCache(i))
        Next

        nudSprite0.Maximum = NumAnimations
        nudSprite1.Maximum = NumAnimations
    End Sub

    Private Sub CmbSound_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSound.SelectedIndexChanged
        Type.Animation(EditorIndex).Sound = cmbSound.SelectedItem.ToString
    End Sub

    Private Sub frmEditor_Animation_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        AnimationEditorCancel()
    End Sub
End Class