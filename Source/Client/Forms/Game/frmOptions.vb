
Imports Core

Friend Class FrmOptions

#Region "Options"

    Private Sub scrlVolume_ValueChanged(sender As Object, e As EventArgs) Handles scrlVolume.ValueChanged
        Types.Settings.Volume = scrlVolume.Value

        MaxVolume = Types.Settings.Volume

        lblVolume.Text = "Volume: " & Types.Settings.Volume

        If Not MusicPlayer Is Nothing Then MusicPlayer.Volume() = MaxVolume

    End Sub

    Private Sub btnSaveSettings_Click(sender As Object, e As EventArgs) Handles btnSaveSettings.Click
        Dim resolution As String(), width As String, height As String

        'music
        If optMOn.Checked = True Then
            Types.Settings.Music = True
            ' start music playing
            PlayMusic(Trim$(Map.Music))
        Else
            Types.Settings.Music = False
            ' stop music playing
            StopMusic()
            CurMusic = ""
        End If

        'sound
        If optSOn.Checked = True Then
            Types.Settings.Sound = True
        Else
            Types.Settings.Sound = False
            StopSound()
        End If

        'screensize
        If Types.Settings.Width & "x" & Types.Settings.Height <> cmbScreenSize.SelectedItem Then
            resolution = cmbScreenSize.SelectedItem.ToString.ToLower.Split("x")
            Types.Settings.Width = resolution(0)
            Types.Settings.Height = resolution(1)
        End If

        If chkVsync.Checked Then
            Types.Settings.Vsync = 1
        Else
            Types.Settings.Vsync = 0
        End If

        If chkNpcBars.Checked Then
            Types.Settings.ShowNpcBar = 1
        Else
            Types.Settings.ShowNpcBar = 0
        End If

        If chkFullscreen.Checked Then
            If Types.Settings.Fullscreen = 0 Then
                MsgBox(Language.Game.Fullscreen, vbOKOnly, Types.Settings.GameName)
            End If
            Types.Settings.Fullscreen = 1
        Else
            If Types.Settings.Fullscreen = 1 Then
                cmbScreenSize.Enabled = False
            Else
                resolution = cmbScreenSize.SelectedItem.ToString.ToLower.Split("x")
                width = resolution(0)
                height = resolution(1)

                RePositionGui(Width, Height)
            End If

            Types.Settings.Fullscreen = 0
        End If

        If chkDynamicLighting.Checked Then
            Types.Settings.DynamicLightRendering = 1
        Else
            Types.Settings.DynamicLightRendering = 0
        End If

        If chkOpenAdminPanelOnLogin.Checked Then
            Types.Settings.OpenAdminPanelOnLogin = 1
        Else
            Types.Settings.OpenAdminPanelOnLogin = 0
        End If

        ' save to config.ini
        Save()
        Me.Visible = False
    End Sub

    Private Sub FrmOptions_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        optMOn.Checked = Types.Settings.Music
        optSOn.Checked = Types.Settings.Sound
        lblVolume.Text = "Volume: " & Types.Settings.Volume
        scrlVolume.Value = Types.Settings.Volume
        If GetPlayerAccess(Myindex) > 0 Then
            chkOpenAdminPanelOnLogin.Visible = True
        Else
            chkOpenAdminPanelOnLogin.Visible = False
        End If
        TopMost = True
    End Sub

#End Region

End Class