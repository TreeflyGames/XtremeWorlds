Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Threading
Imports System.Windows.Forms
Imports Mirage.Basic.Engine

Module C_General
    Friend Started As Boolean

    Friend Function GetTickCount() As Integer
        Return Environment.TickCount
    End Function

    Sub Startup()
        ClearGameData()
        LoadGame()
        GameLoop()
    End Sub

    Friend Sub LoadGame()
        LoadSettings()
        LoadLanguage()
        LoadInputs()
        LoadGraphics()
        InitNetwork()
        FrmMenu.Text = Settings.GameName
        FrmMenu.Visible = True
        Frmmenuvisible = True
        Ping = -1
    End Sub

    Friend Sub LoadGraphics()
        Started = True
        CheckPaths()
        InitGraphics()
    End Sub

    Friend Sub ClearGameData()
        ClearMap()
        ClearMapNpcs()
        ClearMapItems()
        ClearNpcs()
        ClearResources()
        ClearItems()
        ClearShops()
        ClearSkills()
        ClearAnimations()
        ClearProjectile()
        ClearPets()
        ClearJobs()
        ClearBank()
        ClearParty()

        For i = 1 To MAX_PLAYERS
            ClearPlayer(i)
        Next

        ReDim CharSelection(3)

        ClearAnimInstances()
        ClearAutotiles()
    End Sub

    Friend Sub CheckPaths()
        CacheMusic()
        CacheSound()

        If Settings.Music = 1 AndAlso Len(Trim$(Settings.MenuMusic)) > 0 Then
            PlayMusic(Trim$(Settings.MenuMusic))
            MusicPlayer.Volume() = Settings.Volume
        End If

        CheckAnimations()
        CheckCharacters()
        CheckEmotes()
        CheckFaces()
        CheckFog()
        CheckItems()
        CheckPanoramas()
        CheckPaperdolls()
        CheckParallax()
        CheckPictures()
        CheckProjectile()
        CheckResources()
        CheckSkillIcons()
        CheckTilesets()
    End Sub

    Friend Function IsLoginLegal(username As String, password As String) As Boolean
        Return Len(Trim$(username)) >= MIN_STRING_LENGTH AndAlso Len(Trim$(password)) >= MIN_STRING_LENGTH And Len(Trim$(username)) <= MAX_STRING_LENGTH And Len(Trim$(password)) <= MAX_STRING_LENGTH
    End Function

    Friend Function IsStringLegal(sInput As String) As Boolean
        Dim i As Integer

        ' Prevent high ascii chars
        For i = 1 To Len(sInput)

            If (Asc(Mid$(sInput, i, 1))) < 32 OrElse Asc(Mid$(sInput, i, 1)) > 126 Then
                MsgBox(Language.MainMenu.StringLegal, vbOKOnly, Settings.GameName)
                IsStringLegal = False
                Exit Function
            End If

        Next

        IsStringLegal = True
    End Function

    Sub GameInit()
        ' Set the focus
        FrmGame.picscreen.Focus()

        ' Send a request to the server to open the admin menu if the user wants it.
        If Settings.OpenAdminPanelOnLogin = 1 Then
            If GetPlayerAccess(Myindex) > 0 Then
                SendRequestAdmin()
            End If
        End If

        'stop the song playing
        StopMusic()
    End Sub

    Friend Sub MenuState(state As Integer)
        Frmmenuvisible = False
        Select Case state
            Case MenuStateAddchar
                PnlCharCreateVisible = False
                PnlLoginVisible = False
                PnlRegisterVisible = False
                PnlCreditsVisible = False

                If ConnectToServer(1) Then
                    If FrmMenu.rdoMale.Checked = True Then
                        SendAddChar(SelectedChar, FrmMenu.txtCharName.Text, SexType.Male, FrmMenu.cmbJob.SelectedIndex)
                    Else
                        SendAddChar(SelectedChar, FrmMenu.txtCharName.Text, SexType.Female, FrmMenu.cmbJob.SelectedIndex)
                    End If
                End If

            Case MenuStateNewAccount
                PnlLoginVisible = False
                PnlCharCreateVisible = False
                PnlRegisterVisible = False
                PnlCreditsVisible = False
                PnlLoginVisible = True

                If ConnectToServer(1) Then
                    SendNewAccount(FrmMenu.txtRuser.Text, FrmMenu.txtRPass.Text)
                End If

            Case MenuStateLogin
                PnlLoginVisible = False
                PnlCharCreateVisible = False
                PnlRegisterVisible = False
                PnlCreditsVisible = False
                TempUserName = FrmMenu.txtLogin.Text
                TempPassword = FrmMenu.txtPassword.Text

                If ConnectToServer(1) Then
                    SendLogin(FrmMenu.txtLogin.Text, FrmMenu.txtPassword.Text)
                    Exit Sub
                End If
        End Select

    End Sub

    Friend Function ConnectToServer(i As Integer) As Boolean
        Dim until As Integer
        ConnectToServer = False

        ' Check to see if we are already connected, if so just exit
        If Socket.IsConnected() Then
            ConnectToServer = True
            Exit Function
        End If

        If i = 4 Then Exit Function
        until = GetTickCount() + 3500

        Connect()

        ' Wait until connected or a few seconds have passed and report the server being down
        Do While (Not Socket.IsConnected()) AndAlso (GetTickCount() <= until)
            Application.DoEvents()
        Loop

        ' return value
        If Socket.IsConnected() Then
            ConnectToServer = True
        End If

        If Not ConnectToServer Then
            ConnectToServer(i + 1)
        End If

    End Function

    Friend Sub DestroyGame()
        ' break out of GameLoop
        InGame = False
        Application.Exit()
        End
    End Sub

    Friend Sub CheckDir(dirPath As String)
        If Not IO.Directory.Exists(dirPath) Then
            IO.Directory.CreateDirectory(dirPath)
        End If
    End Sub

    Friend Function GetExceptionInfo(ex As Exception) As String
        Dim result As String
        Dim hr As Integer = Runtime.InteropServices.Marshal.GetHRForException(ex)
        result = ex.GetType.ToString & "(0x" & hr.ToString("X8") & "): " & ex.Message & Environment.NewLine & ex.StackTrace & Environment.NewLine
        Dim st As StackTrace = New StackTrace(ex, True)
        For Each sf As StackFrame In st.GetFrames
            If sf.GetFileLineNumber() > 0 Then
                result &= "Line:" & sf.GetFileLineNumber() & " Filename: " & IO.Path.GetFileName(sf.GetFileName) & Environment.NewLine
            End If
        Next
        Return result
    End Function

End Module