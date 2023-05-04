Imports Mirage.Sharp.Asfw
Imports Mirage.Basic.Engine

Friend Module Time

    Sub InitTime()
        ' Add handlers to time events
        AddHandler Engine.Time.Instance.OnTimeChanged, AddressOf HandleTimeChanged
        AddHandler Engine.Time.Instance.OnTimeOfDayChanged, AddressOf HandleTimeOfDayChanged
        AddHandler Engine.Time.Instance.OnTimeSync, AddressOf HandleTimeSync

        ' Prepare the time instance
        Engine.Time.Instance.Time = Date.Now
        Engine.Time.Instance.GameSpeed = 1
    End Sub

    Sub HandleTimeChanged(ByRef source As Engine.Time)
        UpdateCaption()
    End Sub

    Sub HandleTimeOfDayChanged(ByRef source As Engine.Time)
        SendTimeToAll()
    End Sub

    Sub HandleTimeSync(ByRef source As Engine.Time)
        SendGameClockToAll()
    End Sub

    Sub SendGameClockTo(index As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ServerPackets.SClock)
        buffer.WriteInt32(Engine.Time.Instance.GameSpeed)
        buffer.WriteBytes(BitConverter.GetBytes(Engine.Time.Instance.Time.Ticks))
        Socket.SendDataTo(index, buffer.Data, buffer.Head)

        AddDebug("Sent SMSG: SClock")

        AddDebug(" Player: " & GetPlayerName(index) & " : " & " GameSpeed: " & Engine.Time.Instance.GameSpeed & " Instance Time Ticks: " & Engine.Time.Instance.Time.Ticks)

        buffer.Dispose()
    End Sub

    Sub SendGameClockToAll()
        Dim i As Integer

        For i = 1 To GetPlayersOnline()
            If IsPlaying(i) Then
                SendGameClockTo(i)
            End If
        Next
    End Sub

    Sub SendTimeTo(index As Integer)
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ServerPackets.STime)
        buffer.WriteByte(Engine.Time.Instance.TimeOfDay)
        Socket.SendDataTo(index, buffer.Data, buffer.Head)

        AddDebug("Sent SMSG: STime")

        AddDebug(" Player: " & GetPlayerName(index) & " : " & " Time Of Day: " & Engine.Time.Instance.TimeOfDay)

        buffer.Dispose()
    End Sub

    Sub SendTimeToAll()
        Dim i As Integer

        For i = 1 To GetPlayersOnline()
            If IsPlaying(i) Then
                SendTimeTo(i)
            End If
        Next

    End Sub

End Module