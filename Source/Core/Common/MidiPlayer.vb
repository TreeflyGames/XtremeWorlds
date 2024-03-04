Imports Sanford.Multimedia.Midi

Public Module MidiPlayer
    Private OutputDevice As OutputDevice
    Private midiSequence As Sequence
    Private midiSequencer As Sequencer

    ' Initialize the MIDI sequence and sequencer
    Public Sub Initialize()
        midiSequence = New Sequence()
        midiSequencer = New Sequencer()

        AddHandler midiSequencer.PlayingCompleted, AddressOf OnPlayingCompleted
        AddHandler midiSequencer.ChannelMessagePlayed, AddressOf OnChannelMessagePlayed

        midiSequencer.Sequence = midiSequence
        OutputDevice?.Dispose

        ' Initialize the OutputDevice
        Try
            OutputDevice = New OutputDevice(0) ' Assumes device ID 0 is available
        Catch ex As Exception
            ' Handle error (e.g., no MIDI devices available)
            Throw New InvalidOperationException("Could not initialize MIDI output device.", ex)
        End Try
    End Sub

    ' Load a MIDI file
    Public Sub LoadMidiFile(filePath As String)
        midiSequence.Load(filePath)
    End Sub

    ' Play the loaded MIDI file
    Public Sub Play()
        If midiSequencer IsNot Nothing AndAlso midiSequence IsNot Nothing Then
            midiSequencer.Start()
        End If
    End Sub

    ' Stop playback
    Public Sub Dispose()
        midiSequence.Dispose
        midiSequencer.Dispose
        Initialize()
    End Sub

    ' Handle channel messages
    Private Sub OnChannelMessagePlayed(sender As Object, e As ChannelMessageEventArgs)
        OutputDevice.Send(e.Message)
    End Sub

    ' Handle playback completion
    Private Sub OnPlayingCompleted(sender As Object, e As EventArgs)
        ' Handle playback completion here (e.g., clean up or reset the UI)
    End Sub
End Module
