Imports Core

Friend Class FrmGame

#Region "Frm Code"
    Private Sub FrmMainGame_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim screenWidth As String = Screen.PrimaryScreen.Bounds.Width.ToString()
        Dim screenHeight As String = Screen.PrimaryScreen.Bounds.Height.ToString()
        Dim resolution As String()

        Startup()

        If Types.Settings.Fullscreen = 0 Then
            screenWidth = Types.Settings.ScreenWidth
            screenHeight = Types.Settings.ScreenHeight
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

    End Sub

    Private Sub Picscreen_MouseUp(sender As Object, e As MouseEventArgs) Handles picscreen.MouseUp
 
    End Sub

    Private Sub Picscreen_KeyDown(sender As Object, e As KeyEventArgs) Handles picscreen.KeyDown
   
    End Sub

    Private Sub Picscreen_KeyUp(sender As Object, e As KeyEventArgs) Handles picscreen.KeyUp

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