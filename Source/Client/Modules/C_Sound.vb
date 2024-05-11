Imports System.IO
Imports SFML.Audio
Imports Core
Imports System.Runtime.Intrinsics.X86
Imports Core.Paths

Module C_Sound

    'Music + Sound Players
    Friend SoundPlayer As Sound
    Friend ExtraSoundPlayer As Sound
    Friend MusicPlayer As Music
    Friend PreviewPlayer As Music

    Friend MusicCache() As String
    Friend SoundCache() As String

    Friend FadeInSwitch As Boolean
    Friend FadeOutSwitch As Boolean
    Friend CurrentMusic As String

    Sub PlayMusic(fileName As String)
        if fileName = "None" Then
            Exit Sub
        End If

        If Types.Settings.Music = False Or Not File.Exists(Paths.Music & fileName) Then
            StopMusic()
            Exit Sub
        End If

        if fileName = CurrentMusic Then
            Exit Sub
        End If

        If Types.Settings.MusicExt = ".mid" Then
            MidiPlayer.Dispose()
            MidiPlayer.Load(Paths.Music & fileName)
            MidiPlayer.Play()
            CurrentMusic = fileName
            Exit Sub
        End If

        Try
            If MusicPlayer Is Nothing Then
                MusicPlayer = New Music(Paths.Music & fileName)
                MusicPlayer.Loop() = True
                MusicPlayer.Volume() = Types.Settings.MusicVolume
                MusicPlayer.Play()
                CurrentMusic = fileName
                FadeInSwitch = True
            Else
                CurrentMusic = fileName
                FadeOutSwitch = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Sub StopMusic()
        If Not MusicPlayer Is Nothing Then
            MusicPlayer.Stop()
            MusicPlayer.Dispose()
            MusicPlayer = Nothing
            CurrentMusic = ""
        End If

        If MidiPlayer.midiSequence Is Nothing Or MidiPlayer.midiSequencer Is Nothing Then
            Exit Sub
        Else
            MidiPlayer.Dispose()
            CurrentMusic = ""
        End If
    End Sub

    Sub PlayPreview(fileName As String)
        If Types.Settings.Music = 0 Or Not File.Exists(Paths.Music & fileName) Then Exit Sub

        If PreviewPlayer Is Nothing Then
            Try
                PreviewPlayer = New Music(Paths.Music & fileName)
                PreviewPlayer.Loop() = True
                PreviewPlayer.Volume() = Types.Settings.MusicVolume
                PreviewPlayer.Play()
            Catch ex As Exception

            End Try
        Else
            Try
                StopPreview()
                PreviewPlayer = New Music(Paths.Music & fileName)
                PreviewPlayer.Loop() = True
                PreviewPlayer.Volume() = Types.Settings.MusicVolume
                PreviewPlayer.Play()
            Catch ex As Exception

            End Try
        End If
    End Sub

    Sub StopPreview()
        If PreviewPlayer Is Nothing Then Exit Sub
        PreviewPlayer.Stop()
        PreviewPlayer.Dispose()
        PreviewPlayer = Nothing
    End Sub

    Sub PlaySound(fileName As String, x As Integer, y As Integer, Optional looped As Boolean = False)
        ' Exit if sound is disabled or file does not exist
        If Types.Settings.Sound = 0 Or Not File.Exists(Paths.Sounds & fileName) Then Exit Sub

        ' Attempt to create a new sound buffer from the file
        Dim buffer As SoundBuffer
        Try
            buffer = New SoundBuffer(Paths.Sounds & fileName)
        Catch ex As Exception
            MessageBox.Show($"Failed to load sound file: {ex.Message}")
            Exit Sub
        End Try

        ' Check if the sound player needs to be initialized
        If SoundPlayer Is Nothing Then
            SoundPlayer = New Sound()
        Else
            ' Stop the current sound if the player is already initialized
            SoundPlayer.Stop()
        End If

        ' Set the sound buffer to the player
        SoundPlayer.SoundBuffer = buffer

        ' Set looping based on the parameter
        SoundPlayer.Loop = looped

        ' Calculate and set the volume based on the position
        Dim calculatedVolume As Double = CalculateSoundVolume(x, y)
        SoundPlayer.Volume = calculatedVolume

        ' Play the sound
        SoundPlayer.Play()
    End Sub

    Sub StopSound()
        If SoundPlayer Is Nothing Then Exit Sub
        SoundPlayer.Dispose()
        SoundPlayer = Nothing
    End Sub

    Sub PlayExtraSound(fileName As String, Optional looped As Boolean = False)
        If Types.Settings.Sound = 0 Or Not File.Exists(Paths.Sounds & fileName) Then Exit Sub

        Dim buffer As SoundBuffer
        If ExtraSoundPlayer Is Nothing Then
            ExtraSoundPlayer = New Sound()
            buffer = New SoundBuffer(Paths.Sounds & fileName)
            ExtraSoundPlayer.SoundBuffer = buffer
            If looped = True Then
                ExtraSoundPlayer.Loop() = True
            Else
                ExtraSoundPlayer.Loop() = False
            End If
            ExtraSoundPlayer.Volume() = Types.Settings.SoundVolume
            ExtraSoundPlayer.Play()
        Else
            ExtraSoundPlayer.Stop()
            buffer = New SoundBuffer(Paths.Sounds & fileName)
            ExtraSoundPlayer.SoundBuffer = buffer
            If looped = True Then
                ExtraSoundPlayer.Loop() = True
            Else
                ExtraSoundPlayer.Loop() = False
            End If
            ExtraSoundPlayer.Volume() = Types.Settings.SoundVolume
            ExtraSoundPlayer.Play()
        End If
    End Sub

    Sub StopExtraSound()
        If ExtraSoundPlayer Is Nothing Then Exit Sub
        ExtraSoundPlayer.Dispose()
        ExtraSoundPlayer = Nothing
    End Sub

    Sub FadeIn()

        If MusicPlayer Is Nothing Then Exit Sub

        If MusicPlayer.Volume() >= Types.Settings.MusicVolume Then FadeInSwitch = False
        MusicPlayer.Volume() = MusicPlayer.Volume() + 3

    End Sub

    Sub FadeOut()
        Dim tmpmusic As String
        If MusicPlayer Is Nothing Then Exit Sub

        If MusicPlayer.Volume() = 0 Or MusicPlayer.Volume() < 3 Then
            FadeOutSwitch = False
            If CurrentMusic = "" Then
                StopMusic()
            Else
                tmpmusic = CurrentMusic
                StopMusic()
                PlayMusic(tmpmusic)
            End If
        End If
        If MusicPlayer Is Nothing Then Exit Sub

        MusicPlayer.Volume() = MusicPlayer.Volume() - 3

    End Sub

    Public Function CalculateSoundVolume(ByRef x As Integer, ByRef y As Integer) As Double
        Dim X1, X2, Y1, Y2 As Integer
        Dim Distance As Double

        If Not InGame Then
            CalculateSoundVolume = 1
            Return CalculateSoundVolume
        End If

        If InGame AndAlso x = GetPlayerX(MyIndex) AndAlso y = GetPlayerY(MyIndex) Then
            CalculateSoundVolume = 1
            CalculateSoundVolume *= Types.Settings.SoundVolume
            Return CalculateSoundVolume
        End If

        If x > -1 OrElse y > -1 Then
            If x = -1 Then x = 0
            If y = -1 Then y = 0
            X1 = CInt((Player(MyIndex).X * 32) + Player(MyIndex).xOffset)
            Y1 = CInt((Player(MyIndex).Y * 32) + Player(MyIndex).yOffset)
            X2 = x * 32
            Y2 = y * 32

            If CInt(Math.Pow((X2 - X1), 2)) + CInt(Math.Pow((Y2 - Y1), 2)) < 0 Then
                Distance = Math.Sqrt(CInt(Math.Pow((X2 - X1), 2)) + CInt(Math.Pow((Y2 - Y1), 2)) * -1)
            Else
                Distance = Math.Sqrt(CInt(Math.Pow((X2 - X1), 2)) + CInt(Math.Pow((Y2 - Y1), 2)))
            End If

            ' If the range is greater than 32, do not send a sound
            If (Distance / 32) > 32 Then
                CalculateSoundVolume = 0
            Else
                CalculateSoundVolume = 1 / (Distance / 32)

                If CalculateSoundVolume > 1 Then
                    CalculateSoundVolume = 1
                ElseIf CalculateSoundVolume < 0 Then
                    CalculateSoundVolume *= -1
                End If
            End If
        Else
            CalculateSoundVolume = 1
        End If

        CalculateSoundVolume *= Types.Settings.SoundVolume

        Return CalculateSoundVolume
    End Function


End Module