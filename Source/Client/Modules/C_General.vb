Imports Core

Module C_General
    Friend Started As Boolean

    Friend Function GetTickCount() As Integer
        Return Environment.TickCount
    End Function

    Sub Startup()
        InMenu = True
        ClearGameData()
        LoadGame()
        GameLoop()
    End Sub

    Friend Sub LoadGame()
        SettingsManager.Load()
        LoadLanguage()
        InputManager.Load()
        LoadGraphics()
        InitNetwork()
        InitInterface()
        ResizeGUI()
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

        ClearAnimInstances()
        ClearAutotiles()
    End Sub

    Friend Sub CheckPaths()
        CacheMusic()
        CacheSound()
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
        CheckSkills()
        CheckTilesets()
        ChecKInterface()
        CheckGradients()
        CheckDesigns()
    End Sub

    Sub GameInit()
        ' Send a request to the server to open the admin menu if the user wants it.
        If Types.Settings.OpenAdminPanelOnLogin = 1 Then
            If GetPlayerAccess(Myindex) > 0 Then
                SendRequestAdmin()
            End If
        End If
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