
Imports Core

Friend Class FrmOptions

#Region "Options"

    Private Sub scrlVolume_ValueChanged(sender As Object, e As EventArgs) Handles scrlVolume.ValueChanged
        Settings.Data.Volume = scrlVolume.Value

        MaxVolume = Settings.Data.Volume

        lblVolume.Text = "Volume: " & Settings.Data.Volume

        If Not MusicPlayer Is Nothing Then MusicPlayer.Volume() = MaxVolume

    End Sub

    Private Sub btnSaveSettings_Click(sender As Object, e As EventArgs) Handles btnSaveSettings.Click
        Dim resolution As String(), width As String, height As String

        'music
        If optMOn.Checked = True Then
            Settings.Data.Music = True
            ' start music playing
            PlayMusic(Trim$(Map.Music))
        Else
            Settings.Data.Music = False
            ' stop music playing
            StopMusic()
            CurMusic = ""
        End If

        'sound
        If optSOn.Checked = True Then
            Settings.Data.Sound = True
        Else
            Settings.Data.Sound = False
            StopSound()
        End If

        'screensize
        If Settings.Data.Width & "x" & Settings.Data.Height <> cmbScreenSize.SelectedItem Then
            resolution = cmbScreenSize.SelectedItem.ToString.ToLower.Split("x")
            Settings.Data.Width = resolution(0)
            Settings.Data.Height = resolution(1)
        End If

        If chkVsync.Checked Then
            Settings.Data.Vsync = 1
        Else
            Settings.Data.Vsync = 0
        End If

        If chkNpcBars.Checked Then
            Settings.Data.ShowNpcBar = 1
        Else
            Settings.Data.ShowNpcBar = 0
        End If

        If chkFullscreen.Checked Then
            If Settings.Data.Fullscreen = 0 Then
                MsgBox(Language.Game.Fullscreen, vbOKOnly, Settings.Data.GameName)
            End If
            Settings.Data.Fullscreen = 1
        Else
            If Settings.Data.Fullscreen = 1 Then
                cmbScreenSize.Enabled = False
            Else
                resolution = cmbScreenSize.SelectedItem.ToString.ToLower.Split("x")
                width = resolution(0)
                height = resolution(1)

                RePositionGui(Width, Height)
            End If

            Settings.Data.Fullscreen = 0
        End If

        If chkDynamicLighting.Checked Then
            Settings.Data.DynamicLightRendering = 1
        Else
            Settings.Data.DynamicLightRendering = 0
        End If

        If chkOpenAdminPanelOnLogin.Checked Then
            Settings.Data.OpenAdminPanelOnLogin = 1
        Else
            Settings.Data.OpenAdminPanelOnLogin = 0
        End If

        ' save to config.ini
        Settings.Save()
        Me.Visible = False
    End Sub

    Private Sub FrmOptions_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        optMOn.Checked = Settings.Data.Music
        optSOn.Checked = Settings.Data.Sound
        lblVolume.Text = "Volume: " & Settings.Data.Volume
        scrlVolume.Value = Settings.Data.Volume
        If GetPlayerAccess(Myindex) > 0 Then
            chkOpenAdminPanelOnLogin.Visible = True
        Else
            chkOpenAdminPanelOnLogin.Visible = False
        End If
        TopMost = True
    End Sub

#End Region

End Class