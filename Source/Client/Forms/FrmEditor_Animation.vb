Imports System.IO
Imports System.Windows.Forms
Imports Core
Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics

Friend Class frmEditor_Animation
    Private Sub NudSprite0_ValueChanged(sender As Object, e As EventArgs) Handles nudSprite0.Click
        Type.Animation(GameState.EditorIndex).Sprite(0) = nudSprite0.Value
    End Sub

    Private Sub NudSprite1_ValueChanged(sender As Object, e As EventArgs) Handles nudSprite1.Click
        Type.Animation(GameState.EditorIndex).Sprite(1) = nudSprite1.Value
    End Sub

    Private Sub NudLoopCount0_ValueChanged(sender As Object, e As EventArgs) Handles nudLoopCount0.Click
        Type.Animation(GameState.EditorIndex).LoopCount(0) = nudLoopCount0.Value
    End Sub

    Private Sub NudLoopCount1_ValueChanged(sender As Object, e As EventArgs) Handles nudLoopCount1.Click
        Type.Animation(GameState.EditorIndex).LoopCount(1) = nudLoopCount1.Value
    End Sub

    Private Sub NudFrameCount0_ValueChanged(sender As Object, e As EventArgs) Handles nudFrameCount0.Click
        Type.Animation(GameState.EditorIndex).Frames(0) = nudFrameCount0.Value
    End Sub

    Private Sub NudFrameCount1_ValueChanged(sender As Object, e As EventArgs) Handles nudFrameCount1.Click
        Type.Animation(GameState.EditorIndex).Frames(1) = nudFrameCount1.Value
    End Sub

    Private Sub NudLoopTime0_ValueChanged(sender As Object, e As EventArgs) Handles nudLoopTime0.Click
        Type.Animation(GameState.EditorIndex).LoopTime(0) = nudLoopTime0.Value
    End Sub

    Private Sub NudLoopTime1_ValueChanged(sender As Object, e As EventArgs) Handles nudLoopTime1.Click
        Type.Animation(GameState.EditorIndex).LoopTime(1) = nudLoopTime1.Value
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        AnimationEditorOk()
        Dispose()
    End Sub

    Private Sub TxtName_TextChanged(sender As Object, e As EventArgs) Handles txtName.TextChanged
        Dim tmpindex As Integer
        tmpindex = lstIndex.SelectedIndex
        Type.Animation(GameState.EditorIndex).Name = Trim$(txtName.Text)
        lstIndex.Items.RemoveAt(GameState.EditorIndex - 1)
        lstIndex.Items.Insert(GameState.EditorIndex - 1, GameState.EditorIndex & ": " & Type.Animation(GameState.EditorIndex).Name)
        lstIndex.SelectedIndex = tmpindex
    End Sub

    Private Sub lstIndex_Click(sender As Object, e As MouseEventArgs)
        AnimationEditorInit()
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim tmpindex As Integer

        ClearAnimation(GameState.EditorIndex)

        tmpindex = lstIndex.SelectedIndex
        lstIndex.Items.RemoveAt(GameState.EditorIndex - 1)
        lstIndex.Items.Insert(GameState.EditorIndex - 1, GameState.EditorIndex & ": " & Type.Animation(GameState.EditorIndex).Name)
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

        nudSprite0.Maximum = GameState.NumAnimations
        nudSprite1.Maximum = GameState.NumAnimations
    End Sub

    Private Sub CmbSound_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSound.SelectedIndexChanged
        Type.Animation(GameState.EditorIndex).Sound = cmbSound.SelectedItem.ToString
    End Sub

    Private Sub frmEditor_Animation_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        AnimationEditorCancel()
    End Sub

    Public Sub ProcessAnimation(animationControl As System.Windows.Forms.NumericUpDown,
                            frameCountControl As System.Windows.Forms.NumericUpDown,
                            loopCountControl As System.Windows.Forms.NumericUpDown,
                            animationTimerIndex As Integer,
                            renderTarget As RenderTarget2D,
                            backgroundColorControl As System.Windows.Forms.PictureBox,
                            spriteBatch As Graphics.SpriteBatch)

        ' Retrieve the animation number and check its validity
        Dim animationNum As Integer = animationControl.Value
        If animationNum <= 0 Or animationNum > GameState.NumAnimations Then
            spriteBatch.GraphicsDevice.Clear(GameClient.ToMonoGameColor(backgroundColorControl.BackColor))
            Exit Sub
        End If

        ' Check whether animationDisplay is Texture2D or System.Drawing.Image
        Dim texture As Graphics.Texture2D
        texture = GameClient.GetTexture(System.IO.Path.Combine(Core.Path.Animations, animationNum & GameState.GfxExt))
        If texture Is Nothing Then
            Exit Sub
        End If

        ' Get dimensions and column count from controls and graphic info
        Dim totalWidth As Integer
        Dim totalHeight As Integer

        Dim gfxInfo = GameClient.GetGfxInfo(System.IO.Path.Combine(Core.Path.Animations, animationNum & GameState.GfxExt))
        totalWidth = gfxInfo.Width
        totalHeight = gfxInfo.Height

        Dim columns As Integer = frameCountControl.Value

        ' Validate columns to avoid division by zero
        If columns <= 0 Then Exit Sub

        ' Calculate frame dimensions
        Dim frameWidth As Integer = totalWidth / columns

        ' Assuming square frames for simplicity (adjust if frames are not square)
        Dim frameHeight As Integer = frameWidth

        Dim rows As Integer
        If frameHeight > 0 Then
            rows = totalHeight / frameHeight
        End If

        Dim frameCount As Integer = rows * columns

        ' Retrieve loop timing and check frame rendering necessity
        Dim looptime As Integer = loopCountControl.Value
        If GameState.AnimEditorTimer(animationTimerIndex) + looptime <= Environment.TickCount Then
            If GameState.AnimEditorFrame(animationTimerIndex) >= frameCount Then
                GameState.AnimEditorFrame(animationTimerIndex) = 1 ' Reset to the first frame if it exceeds the count
            Else
                GameState.AnimEditorFrame(animationTimerIndex) += 1
            End If
            GameState.AnimEditorTimer(animationTimerIndex) = Environment.TickCount

            ' Render the frame if necessary
            If frameCountControl.Value > 0 Then
                Dim frameIndex As Integer = GameState.AnimEditorFrame(animationTimerIndex) - 1
                Dim column As Integer = frameIndex Mod columns
                Dim row As Integer = frameIndex \ columns

                ' Calculate the source rectangle for the texture or image
                Dim sRECT As New Rectangle(column * frameWidth, row * frameHeight, frameWidth, frameHeight)

                ' Clear the background to the specified color
                spriteBatch.GraphicsDevice.Clear(GameClient.ToMonoGameColor(backgroundColorControl.BackColor))

                GameClient.Graphics.GraphicsDevice.SetRenderTarget(renderTarget)

                ' Draw MonoGame texture
                spriteBatch.Begin()
                spriteBatch.Draw(texture, New Rectangle(0, 0, frameWidth, frameHeight), sRECT, Color.White)
                spriteBatch.End()

                GameClient.Graphics.GraphicsDevice.SetRenderTarget(Nothing)

                ' Convert the render target to a Texture2D and set it as the PictureBox background
                Using stream As New System.IO.MemoryStream()
                    renderTarget.SaveAsPng(stream, renderTarget.Width, renderTarget.Height)
                    stream.Position = 0
                    backgroundColorControl.Image = Drawing.Image.FromStream(stream)
                End Using
            End If
        End If
    End Sub

    Private Sub picSprite0_Paint(sender As Object, e As PaintEventArgs) Handles picSprite0.Paint
        DrawAnimationSprite0()
    End Sub

    Private Sub picSprite1_Paint(sender As Object, e As PaintEventArgs) Handles picSprite1.Paint
        DrawAnimationSprite1()
    End Sub

    Private Sub DrawAnimationSprite0()
        With frmEditor_Animation.Instance
            ' Ensure spriteBatch is created and disposed properly
            Dim spriteBatch As New Graphics.SpriteBatch(GameClient.Graphics.GraphicsDevice)
            Dim renderTarget As New RenderTarget2D(GameClient.Graphics.GraphicsDevice, .picSprite0.Width, .picSprite0.Height)

            ' Call ProcessAnimation for each animation panel
            ProcessAnimation(.nudSprite0, .nudFrameCount0, .nudLoopTime0, 0, renderTarget, .picSprite0, spriteBatch)
        End With
    End Sub

    Private Sub DrawAnimationSprite1()
        With frmEditor_Animation.Instance
            ' Ensure spriteBatch is created and disposed properly
            Dim spriteBatch As New Graphics.SpriteBatch(GameClient.Graphics.GraphicsDevice)
            Dim renderTarget As New RenderTarget2D(GameClient.Graphics.GraphicsDevice, .picSprite1.Width, .picSprite1.Height)

            ' Call ProcessAnimation for each animation panel
            ProcessAnimation(.nudSprite1, .nudFrameCount1, .nudLoopTime1, 1, renderTarget, .picSprite1, spriteBatch)
        End With
    End Sub
End Class