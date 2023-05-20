Imports System.IO
Imports Mirage.Sharp.Asfw
Imports Core

Friend Class FrmMenu
    Inherits Form

#Region "Form Functions"

    ''' <summary>
    ''' clean up and close the game.
    ''' </summary>
    Private Sub FrmMenu_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        DestroyGame()
    End Sub

    ''' <summary>
    ''' On load, get GUI ready.
    ''' </summary>
    Private Sub Frmmenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadMenuGraphics()

        If Started = False Then Call Startup()

        Connect()
    End Sub

    ''' <summary>
    ''' Draw Char select when its needed.
    ''' </summary>
    Private Sub PnlCharSelect_VisibleChanged(sender As Object, e As EventArgs) Handles pnlCharSelect.VisibleChanged
        DrawCharacterSelect()
    End Sub

#End Region

#Region "Draw Functions"

    ''' <summary>
    ''' Preload the images in the menu.
    ''' </summary>
    Friend Sub LoadMenuGraphics()
        'main menu
        If File.Exists(Paths.Gui & "Menu/menu" & GfxExt) Then
            BackgroundImage = Image.FromFile(Paths.Gui & "Menu/menu" & GfxExt)
        End If

        'main menu buttons
        If File.Exists(Paths.Gui & "Menu/button" & GfxExt) Then
            btnExit.Image = Image.FromFile(Paths.Gui & "Menu/button_exit" & GfxExt)
            btnLogin.Image = Image.FromFile(Paths.Gui & "Menu/btn_login" & GfxExt)
            btnPlay.Image = Image.FromFile(Paths.Gui & "Menu/button_play" & GfxExt)
            btnRegister.Image = Image.FromFile(Paths.Gui & "Menu/button_register" & GfxExt)
            btnNewChar.Image = Image.FromFile(Paths.Gui & "Menu/btn_newchar" & GfxExt)
            btnUseChar.Image = Image.FromFile(Paths.Gui & "Menu/btn_usechar" & GfxExt)
            btnDelChar.Image = Image.FromFile(Paths.Gui & "Menu/btn_deletechar" & GfxExt)
            btnCreateAccount.Image = Image.FromFile(Paths.Gui & "Menu/btn_createacc" & GfxExt)
        End If

        'main menu panels
        If File.Exists(Paths.Gui & "Menu\panel" & GfxExt) Then
            pnlMainMenu.BackgroundImage = Image.FromFile(Paths.Gui & "Menu\panel" & GfxExt)
            pnlLogin.BackgroundImage = Image.FromFile(Paths.Gui & "Menu\panel" & GfxExt)
            pnlNewChar.BackgroundImage = Image.FromFile(Paths.Gui & "Menu\panel" & GfxExt)
            pnlCharSelect.BackgroundImage = Image.FromFile(Paths.Gui & "Menu\panel" & GfxExt)
            pnlRegister.BackgroundImage = Image.FromFile(Paths.Gui & "Menu\panel" & GfxExt)
            pnlCredits.BackgroundImage = Image.FromFile(Paths.Gui & "Menu\panel" & GfxExt)
        End If

        'logo
        If File.Exists(Paths.Gui & "Menu\logo" & GfxExt) Then
            picLogo.BackgroundImage = Image.FromFile(Paths.Gui & "Menu\logo" & GfxExt)
        End If

        ' Main
        lblStatusHeader.Text = Language.MainMenu.ServerStatus
        lblNewsHeader.Text = Language.MainMenu.NewsHeader
        'lblNews.Text = Language.MainMenu.News

        ' Login
        lblLogin.Text = Language.MainMenu.Login
        lblLoginName.Text = Language.MainMenu.LoginName
        lblLoginPass.Text = Language.MainMenu.LoginPass
        chkSavePass.Text = Language.MainMenu.LoginCheckBox

        ' New Character
        lblNewChar.Text = Language.MainMenu.NewCharacter
        lblNewCharName.Text = Language.MainMenu.NewCharacterName
        lblNewCharJob.Text = Language.MainMenu.NewCharacterClass
        lblNewCharGender.Text = Language.MainMenu.NewCharacterGender
        rdoMale.Text = Language.MainMenu.NewCharacterMale
        rdoFemale.Text = Language.MainMenu.NewCharacterFemale
        lblNewCharSprite.Text = Language.MainMenu.NewCharacterSprite
        btnCreateCharacter.Text = Language.MainMenu.NewCharacterButton

        ' Use Character
        lblCharSelect.Text = Language.MainMenu.UseCharacter

        ' Registration
        lblNewAccount.Text = Language.MainMenu.Register
        lblNewAccName.Text = Language.MainMenu.RegisterName
        lblNewAccPass.Text = Language.MainMenu.RegisterPass1
        lblNewAccPass2.Text = Language.MainMenu.RegisterPass2

        ' Credits
        lblCreditsTop.Text = Language.MainMenu.Credits
    End Sub

    ''' <summary>
    ''' Draw the Character for new char creation.
    ''' </summary>
    Sub DrawCharacter()
        If pnlNewChar.Visible = True Then
            Dim g As Graphics = pnlNewChar.CreateGraphics
            Dim filename As String
            Dim srcRect As Rectangle
            Dim destRect As Rectangle
            Dim charwidth As Integer
            Dim charheight As Integer

            If rdoMale.Checked = True Then
                filename = Paths.Graphics & "characters\" & Job(NewCharJob).MaleSprite & GfxExt
            Else
                filename = Paths.Graphics & "characters\" & Job(NewCharJob).FemaleSprite & GfxExt
            End If

            If File.Exists(filename) = False Then Exit sub
            Dim charsprite As Bitmap = New Bitmap(filename)

            charwidth = charsprite.Width / 4
            charheight = charsprite.Height / 4

            srcRect = New Rectangle(0, 0, charwidth, charheight)
            destRect = New Rectangle(placeholderforsprite.Left, placeholderforsprite.Top, charwidth, charheight)

            charsprite.MakeTransparent(charsprite.GetPixel(0, 0))

            If charwidth > 32 Then
                Lblnextcharleft = (100 - (64 - charwidth))
            Else
                Lblnextcharleft = 100
            End If
            pnlNewChar.Refresh()
            g.DrawImage(charsprite, destRect, srcRect, GraphicsUnit.Pixel)
            g.Dispose()
        End If
    End Sub

    ''' <summary>
    ''' Draw the character for the char select screen.
    ''' </summary>
    Sub DrawCharacterSelect()
        Dim g As Graphics
        Dim srcRect As Rectangle
        Dim destRect As Rectangle

        If pnlCharSelect.Visible = True Then
            Dim filename As String
            Dim charwidth As Integer, charheight As Integer

            'first
            If CharSelection(1).Sprite > 0 And NumCharacters >= CharSelection(1).Sprite Then
                g = picChar1.CreateGraphics

                filename = Paths.Graphics & "characters\" & CharSelection(1).Sprite & GfxExt

                Dim charsprite As Bitmap = New Bitmap(filename)

                charwidth = charsprite.Width / 4
                charheight = charsprite.Height / 4

                srcRect = New Rectangle(0, 0, charwidth, charheight)
                destRect = New Rectangle(0, 0, charwidth, charheight)

                charsprite.MakeTransparent(charsprite.GetPixel(0, 0))

                picChar1.Refresh()
                g.DrawImage(charsprite, destRect, srcRect, GraphicsUnit.Pixel)

                If SelectedChar = 1 Then
                    g.DrawRectangle(Pens.Red, New Rectangle(0, 0, charwidth - 1, charheight))
                End If

                g.Dispose()
            Else
                picChar1.BorderStyle = BorderStyle.FixedSingle
                picChar1.Refresh()
            End If

            'second
            If CharSelection(2).Sprite > 0 And NumCharacters >= CharSelection(2).Sprite Then
                g = picChar2.CreateGraphics

                filename = Paths.Graphics & "characters\" & CharSelection(2).Sprite & GfxExt

                Dim charsprite As Bitmap = New Bitmap(filename)

                charwidth = charsprite.Width / 4
                charheight = charsprite.Height / 4

                srcRect = New Rectangle(0, 0, charwidth, charheight)
                destRect = New Rectangle(0, 0, charwidth, charheight)

                charsprite.MakeTransparent(charsprite.GetPixel(0, 0))

                picChar2.Refresh()
                g.DrawImage(charsprite, destRect, srcRect, GraphicsUnit.Pixel)

                If SelectedChar = 2 Then
                    g.DrawRectangle(Pens.Red, New Rectangle(0, 0, charwidth - 1, charheight))
                End If

                g.Dispose()
            Else
                picChar2.BorderStyle = BorderStyle.FixedSingle
                picChar2.Refresh()
            End If

            'third
            If CharSelection(3).Sprite > 0 And NumCharacters >= CharSelection(3).Sprite Then
                g = picChar3.CreateGraphics

                filename = Paths.Graphics & "characters\" & CharSelection(3).Sprite & GfxExt

                Dim charsprite As Bitmap = New Bitmap(filename)

                charwidth = charsprite.Width / 4
                charheight = charsprite.Height / 4

                srcRect = New Rectangle(0, 0, charwidth, charheight)
                destRect = New Rectangle(0, 0, charwidth, charheight)

                charsprite.MakeTransparent(charsprite.GetPixel(0, 0))

                picChar3.Refresh()
                g.DrawImage(charsprite, destRect, srcRect, GraphicsUnit.Pixel)

                If SelectedChar = 3 Then
                    g.DrawRectangle(Pens.Red, New Rectangle(0, 0, charwidth - 1, charheight))
                End If

                g.Dispose()
            Else
                picChar3.BorderStyle = BorderStyle.FixedSingle
                picChar3.Refresh()
            End If

        End If
    End Sub

    ''' <summary>
    ''' Stop the NewChar panel from repainting itself.
    ''' </summary>
    Private Sub PnlNewChar_Paint(sender As Object, e As PaintEventArgs) Handles pnlNewChar.Paint
        'nada here
    End Sub

#End Region

#Region "Credits"

    ''' <summary>
    ''' This timer handles the scrolling credits.
    ''' </summary>
    Private Sub TmrCredits_Tick(sender As Object, e As EventArgs) Handles tmrCredits.Tick
        Dim credits As String
        Dim filepath As String
        filepath = Paths.Database & "credits.txt"
        lblScrollingCredits.Top = 177
        If PnlCreditsVisible = True Then
            tmrCredits.Enabled = False
            credits = GetFileContents(filepath)
            lblScrollingCredits.Text = "" & vbCrLf & credits
            Do Until PnlCreditsVisible = False
                lblScrollingCredits.Top = lblScrollingCredits.Top - 1
                If lblScrollingCredits.Bottom <= lblCreditsTop.Bottom Then
                    lblScrollingCredits.Top = 177
                End If
                Application.DoEvents()
                Threading.Thread.Sleep(1)
            Loop
        End If
    End Sub

#End Region

#Region "Login"

    ''' <summary>
    ''' Handles press enter on login name txtbox.
    ''' </summary>
    Private Sub TxtLogin_KeyDown(sender As Object, e As KeyEventArgs) Handles txtLogin.KeyDown
        If e.KeyCode = Keys.Enter Then
            BtnLogin_Click(Me, Nothing)
        End If
    End Sub

    ''' <summary>
    ''' Handles press enter on login password txtbox.
    ''' </summary>
    Private Sub TxtPassword_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPassword.KeyDown
        If e.KeyCode = Keys.Enter Then
            BtnLogin_Click(Me, Nothing)
        End If
    End Sub

    ''' <summary>
    ''' Handle the SavePas checkbox.
    ''' </summary>
    Private Sub ChkSavePass_CheckedChanged(sender As Object, e As EventArgs) Handles chkSavePass.CheckedChanged
        ChkSavePassChecked = chkSavePass.Checked
        Types.Settings.RememberPassword = ChkSavePassChecked
        Save()
    End Sub

#End Region

#Region "Char Creation"

    ''' <summary>
    ''' Changes selected class.
    ''' </summary>
    Private Sub CmbJob_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbJob.SelectedIndexChanged
        NewCharJob = cmbJob.SelectedIndex + 1
        txtDescription.Text = Job(NewCharJob).Desc
        DrawCharacter()
    End Sub

    ''' <summary>
    ''' Switches to male gender.
    ''' </summary>
    Private Sub RdoMale_CheckedChanged(sender As Object, e As EventArgs) Handles rdoMale.CheckedChanged
        DrawCharacter()
    End Sub

    ''' <summary>
    ''' Switches to female gender.
    ''' </summary>
    Private Sub RdoFemale_CheckedChanged(sender As Object, e As EventArgs) Handles rdoFemale.CheckedChanged
        DrawCharacter()
    End Sub

    ''' <summary>
    ''' Initial drawing of new char.
    ''' </summary>
    Private Sub PnlNewChar_VisibleChanged(sender As Object, e As EventArgs) Handles pnlNewChar.VisibleChanged
        DrawCharacter()
    End Sub

#End Region

#Region "Buttons"

    ''' <summary>
    ''' Handle Play button press.
    ''' </summary>
    Private Sub BtnPlay_Click(sender As Object, e As EventArgs) Handles btnPlay.Click
        If Socket.IsConnected() = True Then
            PlaySound("Click.ogg")
            PnlRegisterVisible = False
            PnlLoginVisible = True
            PnlCharCreateVisible = False
            PnlCreditsVisible = False
            pnlMainMenu.Visible = False
            txtLogin.Focus()
            If Types.Settings.RememberPassword = True Then
                txtLogin.Text = Types.Settings.Username
                txtPassword.Text = Types.Settings.Password
                chkSavePass.Checked = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' Changes to hover state on button.
    ''' </summary>
    Private Sub BtnPlay_MouseEnter(sender As Object, e As EventArgs) Handles btnPlay.MouseEnter
        btnPlay.Image = Image.FromFile(Paths.Gui & "Menu\button_hover_play" & GfxExt)
    End Sub

    ''' <summary>
    ''' Changes to normal state on button.
    ''' </summary>
    Private Sub BtnPlay_MouseLeave(sender As Object, e As EventArgs) Handles btnPlay.MouseLeave
        btnPlay.Image = Image.FromFile(Paths.Gui & "Menu\button_play" & GfxExt)
    End Sub

    ''' <summary>
    ''' Handle Register button press.
    ''' </summary>
    Private Sub BtnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
        Process.Start("explorer.exe", Types.Settings.Website)
    End Sub

    ''' <summary>
    ''' Changes to hover state on button.
    ''' </summary>
    Private Sub btnRegister_MouseEnter(sender As Object, e As EventArgs) Handles btnRegister.MouseEnter
        btnRegister.Image = Image.FromFile(Paths.Gui & "Menu\button_hover_register" & GfxExt)
    End Sub

    ''' <summary>
    ''' Changes to normal state on button.
    ''' </summary>
    Private Sub BtnRegister_MouseLeave(sender As Object, e As EventArgs) Handles btnRegister.MouseLeave
        btnRegister.Image = Image.FromFile(Paths.Gui & "Menu\button_register" & GfxExt)
    End Sub

    ''' <summary>
    ''' Handles Exit button press.
    ''' </summary>
    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        PlaySound("Click.ogg")
        DestroyGame()
    End Sub

    ''' <summary>
    ''' Changes to hover state on button.
    ''' </summary>
    Private Sub btnExit_MouseEnter(sender As Object, e As EventArgs) Handles btnExit.MouseEnter
        btnExit.Image = Image.FromFile(Paths.Gui & "Menu\button_hover_exit" & GfxExt)
    End Sub

    ''' <summary>
    ''' Changes to normal state on button.
    ''' </summary>
    Private Sub BtnExit_MouseLeave(sender As Object, e As EventArgs) Handles btnExit.MouseLeave
        btnExit.Image = Image.FromFile(Paths.Gui & "Menu\button_exit" & GfxExt)
    End Sub

    ''' <summary>
    ''' Handles Login button press.
    ''' </summary>
    Private Sub BtnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        MenuState(MenuStateLogin)
    End Sub

    ''' <summary>
    ''' Changes to hover state on button.
    ''' </summary>
    Private Sub BtnLogin_MouseEnter(sender As Object, e As EventArgs) Handles btnLogin.MouseEnter
        btnLogin.Image = Image.FromFile(Paths.Gui & "Menu\btn_login_hover" & GfxExt)
    End Sub

    ''' <summary>
    ''' Changes to normal state on button.
    ''' </summary>
    Private Sub BtnLogin_MouseLeave(sender As Object, e As EventArgs) Handles btnLogin.MouseLeave
        btnLogin.Image = Image.FromFile(Paths.Gui & "Menu\btn_login" & GfxExt)
    End Sub

    ''' <summary>
    ''' Changes to hover state on button.
    ''' </summary>
    Private Sub BtnCreateAccount_MouseEnter(sender As Object, e As EventArgs) Handles btnCreateAccount.MouseEnter
        btnCreateAccount.Image = Image.FromFile(Paths.Gui & "Menu\btn_createacc_hover" & GfxExt)
    End Sub

    ''' <summary>
    ''' Changes to normal state on button.
    ''' </summary>
    Private Sub BtnCreateAccount_MouseLeave(sender As Object, e As EventArgs) Handles btnCreateAccount.MouseLeave
        btnCreateAccount.Image = Image.FromFile(Paths.Gui & "Menu\btn_createacc" & GfxExt)
    End Sub

    ''' <summary>
    ''' Handles CreateCharacter button press.
    ''' </summary>
    Private Sub BtnCreateCharacter_Click(sender As Object, e As EventArgs) Handles btnCreateCharacter.Click
        MenuState(MenuStateAddchar)
    End Sub

    ''' <summary>
    ''' Changes to hover state on button.
    ''' </summary>
    Private Sub BtnCreateCharacter_MouseEnter(sender As Object, e As EventArgs) Handles btnCreateCharacter.MouseEnter
        btnCreateCharacter.Image = Image.FromFile(Paths.Gui & "Menu\btn_createchar_hover" & GfxExt)
    End Sub

    ''' <summary>
    ''' Changes to normal state on button.
    ''' </summary>
    Private Sub BtnCreateCharacter_MouseLeave(sender As Object, e As EventArgs) Handles btnCreateCharacter.MouseLeave
        btnCreateCharacter.Image = Image.FromFile(Paths.Gui & "Menu\btn_createchar" & GfxExt)
    End Sub

    ''' <summary>
    ''' Handles selecting character 1.
    ''' </summary>
    Private Sub PicChar1_Click(sender As Object, e As EventArgs) Handles picChar1.Click
        SelectedChar = 1
        DrawCharacterSelect()
    End Sub

    ''' <summary>
    ''' Handles selecting character 2.
    ''' </summary>
    Private Sub PicChar2_Click(sender As Object, e As EventArgs) Handles picChar2.Click
        SelectedChar = 2
        DrawCharacterSelect()
    End Sub

    ''' <summary>
    ''' Handles selecting character 3.
    ''' </summary>
    Private Sub PicChar3_Click(sender As Object, e As EventArgs) Handles picChar3.Click
        SelectedChar = 3
        DrawCharacterSelect()
    End Sub

    ''' <summary>
    ''' Handles NewChar button press.
    ''' </summary>
    Private Sub btnNewChar_Click(sender As Object, e As EventArgs) Handles btnNewChar.Click
        Dim i As Integer, newSelectedChar As Byte

        newSelectedChar = 0

        For i = 1 To MAX_CHARACTERS
            If CharSelection(i).Name = "" Then
                newSelectedChar = i
                Exit For
            End If
        Next

        If newSelectedChar > 0 Then
            SelectedChar = newSelectedChar
        End If

        PnlCharCreateVisible = True
        PnlCharSelectVisible = False
        DrawChar = True
    End Sub

    ''' <summary>
    ''' Changes to hover state on button.
    ''' </summary>
    Private Sub BtnNewChar_MouseEnter(sender As Object, e As EventArgs) Handles btnNewChar.MouseEnter
        btnNewChar.Image = Image.FromFile(Paths.Gui & "Menu\btn_newchar_hover" & GfxExt)
    End Sub

    ''' <summary>
    ''' Changes to normal state on button.
    ''' </summary>
    Private Sub BtnNewChar_MouseLeave(sender As Object, e As EventArgs) Handles btnNewChar.MouseLeave
        btnNewChar.Image = Image.FromFile(Paths.Gui & "Menu\btn_newchar" & GfxExt)
    End Sub

    ''' <summary>
    ''' Handles UseChar button press.
    ''' </summary>
    Private Sub BtnUseChar_Click(sender As Object, e As EventArgs) Handles btnUseChar.Click
        If CharSelection(SelectedChar).Name.Trim = "" Then
            MsgBox("Character slot empty.")
            Exit SUb
        End If

        Frmmenuvisible = False

        Dim buffer As ByteStream
        buffer = New ByteStream(8)
        buffer.WriteInt32(ClientPackets.CUseChar)
        buffer.WriteInt32(SelectedChar)
        Socket.SendData(buffer.Data, buffer.Head)

        buffer.Dispose()
    End Sub

    ''' <summary>
    ''' Handles DelChar button press.
    ''' </summary>

    Private Sub btnDelChar_Click(sender As Object, e As EventArgs) Handles btnDelChar.Click
        Dim buffer As ByteStream
        buffer = New ByteStream(8)
        buffer.WriteInt32(ClientPackets.CDelChar)
        buffer.WriteInt32(SelectedChar)
        Socket.SendData(buffer.Data, buffer.Head)

        buffer.Dispose()
    End Sub

    ''' <summary>
    ''' Changes to hover state on button.
    ''' </summary>
    Private Sub BtnUseChar_MouseEnter(sender As Object, e As EventArgs) Handles btnUseChar.MouseEnter
        btnUseChar.Image = Image.FromFile(Paths.Gui & "Menu\btn_usechar_hover" & GfxExt)
    End Sub

    ''' <summary>
    ''' Changes to normal state on button.
    ''' </summary>
    Private Sub BtnUseChar_MouseLeave(sender As Object, e As EventArgs) Handles btnUseChar.MouseLeave
        btnUseChar.Image = Image.FromFile(Paths.Gui & "Menu\btn_usechar" & GfxExt)
    End Sub

    ''' <summary>
    ''' Changes to hover state on button.
    ''' </summary>
    Private Sub BtnDelChar_MouseEnter(sender As Object, e As EventArgs) Handles btnDelChar.MouseEnter
        btnDelChar.Image = Image.FromFile(Paths.Gui & "Menu\btn_deletechar_hover" & GfxExt)
    End Sub

    ''' <summary>
    ''' Changes to normal state on button.
    ''' </summary>
    Private Sub BtnDelChar_MouseLeave(sender As Object, e As EventArgs) Handles btnDelChar.MouseLeave
        btnDelChar.Image = Image.FromFile(Paths.Gui & "Menu\btn_deletechar" & GfxExt)
    End Sub

#End Region

End Class