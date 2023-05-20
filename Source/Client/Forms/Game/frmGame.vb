Imports Core

Friend Class FrmGame

#Region "Frm Code"
    Private Sub FrmMainGame_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim screenWidth As String = Screen.PrimaryScreen.Bounds.Width.ToString()
        Dim screenHeight As String = Screen.PrimaryScreen.Bounds.Height.ToString()
        Dim resolution As String()

        If Settings.Data.Fullscreen = 0 Then
            screenWidth = Settings.Data.Width
            screenHeight = Settings.Data.Height
        Else
            FormBorderStyle = 0
        End If
       
        RePositionGui(screenWidth, screenHeight)
    End Sub

    Private Sub FrmMainGame_Closing(sender As Object, e As EventArgs) Handles MyBase.Closing
        Application.Exit()
    End Sub

    Private Sub FrmMainGame_KeyPress(sender As Object, e As KeyPressEventArgs) Handles MyBase.KeyPress
        ChatInput.ProcessCharacter(e)
    End Sub

    Private Sub FrmGame_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        CenterToScreen()
    End Sub

    Private Sub FrmMainGame_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

    End Sub

    Private Sub FrmMainGame_KeyUp(sender As Object, e As KeyEventArgs) Handles MyBase.KeyUp

    End Sub

#End Region

#Region "PicScreen Code"

    Private Sub Picscreen_MouseDown(sender As Object, e As MouseEventArgs) Handles picscreen.MouseDown
        

    End Sub

    Private Sub PicScreen_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picscreen.MouseWheel

    End Sub

    Private Sub PicScreen_DoubleClick(sender As Object, e As MouseEventArgs) Handles picscreen.DoubleClick

    End Sub

    Private Overloads Sub Picscreen_Paint(sender As Object, e As PaintEventArgs) Handles picscreen.Paint
        'This is here to make sure that the box dosen't try to re-paint itself... saves time and w/e else
        Exit Sub
    End Sub

    Private Sub Picscreen_MouseMove(sender As Object, e As MouseEventArgs) Handles picscreen.MouseMove
        CurX = TileView.Left + ((e.Location.X + Camera.Left) \ PicX)
        CurY = TileView.Top + ((e.Location.Y + Camera.Top) \ PicY)
        CurMouseX = e.Location.X
        CurMouseY = e.Location.Y
        CheckGuiMove(e.X, e.Y)
        If e.Button = MouseButtons.Left Then HandleInterfaceEvents(EntState.MouseMove)

        If Editor = EditorType.Map Then
            If e.Button = MouseButtons.Left OrElse e.Button = MouseButtons.Right Then
                FrmEditor_Map.MapEditorMouseDown(e.Button, e.X, e.Y)
            End If
        End If
    End Sub

    Private Sub Picscreen_MouseUp(sender As Object, e As MouseEventArgs) Handles picscreen.MouseUp
        CurX = TileView.Left + ((e.Location.X + Camera.Left) \ PicX)
        CurY = TileView.Top + ((e.Location.Y + Camera.Top) \ PicY)
        CheckGuiMouseUp(e.X, e.Y, e)
        If e.Button = MouseButtons.Left Then HandleInterfaceEvents(EntState.MouseUp)
    End Sub

    Private Sub Picscreen_KeyDown(sender As Object, e As KeyEventArgs) Handles picscreen.KeyDown
        If Inputs.MoveUp(e.KeyCode) Then VbKeyUp = True
        If Inputs.MoveDown(e.KeyCode) Then VbKeyDown = True
        If Inputs.MoveDown(e.KeyCode) Then VbKeyLeft = True
        If Inputs.MoveDown(e.KeyCode) Then VbKeyRight = True
        If Inputs.Attack(e.KeyCode) Then VbKeyControl = True
        If Inputs.Run(e.KeyCode) Then VbKeyShift = True

        'hotbar
        If Inputs.HotBar1(e.KeyCode) AndAlso Player(Myindex).Hotbar(1).Slot <> 0 Then SendUseHotbarSlot(1)
        If Inputs.HotBar2(e.KeyCode) AndAlso Player(Myindex).Hotbar(2).Slot <> 0 Then SendUseHotbarSlot(2)
        If Inputs.HotBar3(e.KeyCode) AndAlso Player(Myindex).Hotbar(3).Slot <> 0 Then SendUseHotbarSlot(3)
        If Inputs.HotBar4(e.KeyCode) AndAlso Player(Myindex).Hotbar(4).Slot <> 0 Then SendUseHotbarSlot(4)
        If Inputs.HotBar5(e.KeyCode) AndAlso Player(Myindex).Hotbar(5).Slot <> 0 Then SendUseHotbarSlot(5)
        If Inputs.HotBar6(e.KeyCode) AndAlso Player(Myindex).Hotbar(6).Slot <> 0 Then SendUseHotbarSlot(6)
        If Inputs.HotBar7(e.KeyCode) AndAlso Player(Myindex).Hotbar(7).Slot <> 0 Then SendUseHotbarSlot(7)

        'admin
        If Inputs.Admin(e.KeyCode) Then
            If GetPlayerAccess(Myindex) > 0 Then
                SendRequestAdmin()
            End If
        End If

        'hide gui
        If Inputs.HudToggle(e.KeyCode) Then
            HideGui = Not HideGui
        End If
    End Sub

    Private Sub Picscreen_KeyUp(sender As Object, e As KeyEventArgs) Handles picscreen.KeyUp
        If Inputs.MoveUp(e.KeyCode) Then VbKeyUp = False
        If Inputs.MoveDown(e.KeyCode) Then VbKeyDown = False
        If Inputs.MoveDown(e.KeyCode) Then VbKeyLeft = False
        If Inputs.MoveDown(e.KeyCode) Then VbKeyRight = False
        If Inputs.Attack(e.KeyCode) Then VbKeyControl = False
        If Inputs.Run(e.KeyCode) Then VbKeyShift = False

        Dim keyData As Keys = e.KeyData

        If IsAcceptable(keyData) Then
            e.Handled = True
            e.SuppressKeyPress = True
        End If
    End Sub

#End Region

#Region "Quest Code"

    Private Sub LblAbandonQuest_Click(sender As Object, e As EventArgs)
        'Dim QuestNum As Integer = GetQuestNum(Trim$(lstQuestLog.Text))
        'If Trim$(lstQuestLog.Text) = "" Then Exit Sub

        'PlayerHandleQuest(QuestNum, 2)
        'ResetQuestLog()
        'pnlQuestLog.Visible = False
    End Sub

#End Region

#Region "Misc"

    Private ReadOnly _nonAcceptableKeys() As Keys = {Keys.NumPad0, Keys.NumPad1, Keys.NumPad2, Keys.NumPad3, Keys.NumPad4, Keys.NumPad5, Keys.NumPad6, Keys.NumPad7, Keys.NumPad8, Keys.NumPad9}

    Friend Function IsAcceptable(keyData As Keys) As Boolean
        Dim index As Integer = Array.IndexOf(_nonAcceptableKeys, keyData)
        Return index >= 0
    End Function

#End Region

End Class