Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms
Imports Mirage.Sharp.Asfw
Imports Mirage.Sharp.Asfw.IO
Imports Mirage.Basic.Engine

Module C_Maps

#Region "Globals"

    Friend Map As MapStruct
    Friend MapItem(MAX_MAP_ITEMS) As MapItemStruct
    Friend MapNpc(MAX_MAP_NPCS) As MapNpcStruct

#End Region

#Region "Structs"
    Public Structure MapStruct
        Dim Name As String
        Dim Music As String

        Dim Revision As Integer
        Dim Moral As Byte
        Dim Tileset As Integer

        Dim Up As Integer
        Dim Down As Integer
        Dim Left As Integer
        Dim Right As Integer

        Dim BootMap As Integer
        Dim BootX As Byte
        Dim BootY As Byte

        Dim MaxX As Byte
        Dim MaxY As Byte

        Dim Tile(,) As TileStruct
        Dim Npc() As Integer
        Dim EventCount As Integer
        Dim Events() As EventStruct

        Dim WeatherType As Byte
        Dim Fogindex As Integer
        Dim WeatherIntensity As Integer
        Dim FogAlpha As Byte
        Dim FogSpeed As Byte

        Dim HasMapTint As Byte
        Dim MapTintR As Byte
        Dim MapTintG As Byte
        Dim MapTintB As Byte
        Dim MapTintA As Byte

        Dim Instanced As Byte

        Dim Panorama As Byte
        Dim Parallax As Byte

        Dim Brightness As Byte

        'Client Side Only -- Temporary
        Dim CurrentEvents As Integer

        Dim MapEvents() As MapEventStruct
    End Structure

    Public Structure MapItemStruct
        Dim Num As Integer
        Dim Value As Integer
        Dim Frame As Byte
        Dim X As Byte
        Dim Y As Byte
    End Structure

    Public Structure MapNpcStruct
        Dim Num As Integer
        Dim Target As Integer
        Dim TargetType As Byte
        Dim Vital() As Integer
        Dim Map As Integer
        Dim X As Byte
        Dim Y As Byte
        Dim Dir As Byte

        ' Client use only
        Dim XOffset As Integer

        Dim YOffset As Integer
        Dim Moving As Byte
        Dim Attacking As Byte
        Dim AttackTimer As Integer
        Dim Steps As Integer
    End Structure

    Public Structure TileHistoryStruct
        Dim Tile(,) As TileStruct
    End Structure
#End Region

#Region "Database"

    Friend Sub CheckTilesets()
        Dim i As Integer
        i = 1

        While File.Exists(Paths.Graphics & "\tilesets\" & i & GfxExt)
            NumTileSets = NumTileSets + 1
            i = i + 1
        End While
    End Sub

    Sub ClearMap()
        Dim i As Integer, x As Integer, y As Integer

        Map.Name = ""
        Map.Tileset = 1
        Map.MaxX = ScreenMapx
        Map.MaxY = ScreenMapy
        Map.BootMap = 0
        Map.BootX = 0
        Map.BootY = 0
        Map.Down = 0
        Map.Left = 0
        Map.Moral = 0
        Map.Music = ""
        Map.Revision = 0
        Map.Right = 0
        Map.Up = 0

        ReDim Map.Npc(MAX_MAP_NPCS)
        ReDim Map.Tile(Map.MaxX, Map.MaxY)
        ReDim TileHistory(MaxTileHistory)
        For i = 1 To MaxTileHistory
            ReDim TileHistory(i).Tile(Map.MaxX, Map.MaxY)
        Next
        HistoryIndex = 0
        TileHistoryHighIndex = 0

        For X = 0 To ScreenMapx
            For Y = 0 To ScreenMapy
                ReDim Map.Tile(x, y).Layer(LayerType.Count - 1)

                For i = 1 To MaxTileHistory
                    ReDim TileHistory(i).Tile(x, y).Layer(LayerType.Count - 1)
                Next

                For l = 1 To LayerType.Count - 1
                    Map.Tile(x, y).Layer(l).Tileset = 0
                    Map.Tile(x, y).Layer(l).X = 0
                    Map.Tile(x, y).Layer(l).Y = 0
                    Map.Tile(x, y).Layer(l).AutoTile = 0
                    Map.Tile(x, y).Data1 = 0
                    Map.Tile(x, y).Data2 = 0
                    Map.Tile(x, y).Data3 = 0
                    Map.Tile(x, y).Type = 0
                    Map.Tile(x, y).DirBlock = 0

                    For i = 1 To MaxTileHistory
                        TileHistory(i).Tile(x, y).Layer(l).Tileset = 0
                        TileHistory(i).Tile(x, y).Layer(l).X = 0
                        TileHistory(i).Tile(x, y).Layer(l).Y = 0
                        TileHistory(i).Tile(x, y).Layer(l).AutoTile = 0
                        TileHistory(i).Tile(x, y).Data1 = 0
                        TileHistory(i).Tile(x, y).Data2 = 0
                        TileHistory(i).Tile(x, y).Data3 = 0
                        TileHistory(i).Tile(x, y).Type = 0
                        TileHistory(i).Tile(x, y).DirBlock = 0
                    Next
                Next

            Next
        Next

        ClearMapEvents()
        tempTileLights = Nothing

    End Sub

    Sub ClearMapItems()
        Dim i As Integer

       For i = 1 To MAX_MAP_ITEMS
            ClearMapItem(i)
        Next

    End Sub

    Sub ClearMapItem(index As Integer)
        MapItem(index).Frame = 0
        MapItem(index).Num = 0
        MapItem(index).Value = 0
        MapItem(index).X = 0
        MapItem(index).Y = 0
    End Sub

    Sub ClearMapNpc(index As Integer)
        MapNpc(index).Attacking = 0
        MapNpc(index).AttackTimer = 0
        MapNpc(index).Dir = 0
        MapNpc(index).Map = 0
        MapNpc(index).Moving = 0
        MapNpc(index).Num = 0
        MapNpc(index).Steps = 0
        MapNpc(index).Target = 0
        MapNpc(index).TargetType = 0
        ReDim MapNpc(index).Vital(VitalType.Count - 1)
        MapNpc(index).Vital(VitalType.HP) = 0
        MapNpc(index).Vital(VitalType.MP) = 0
        MapNpc(index).Vital(VitalType.SP) = 0
        MapNpc(index).X = 0
        MapNpc(index).XOffset = 0
        MapNpc(index).Y = 0
        MapNpc(index).YOffset = 0
    End Sub

    Sub ClearMapNpcs()
       Dim i As Integer

       For i = 1 To MAX_MAP_NPCS
            ClearMapNpc(i)
        Next

    End Sub

#End Region

#Region "Incoming Packets"

    Friend Sub Packet_EditMap(ByRef data() As Byte)
        Dim buffer As New ByteStream(data)
        InitMapEditor = True

        buffer.Dispose()
    End Sub

    Sub Packet_CheckMap(ByRef data() As Byte)
        Dim x As Integer, y As Integer, i As Integer
        Dim needMap As Byte
        Dim buffer As New ByteStream(data)

        GettingMap = True

        ' Erase all players except self
       For i = 1 To MAX_PLAYERS
            If i <> Myindex Then
                SetPlayerMap(i, 0)
            End If
        Next

        ' Erase all temporary tile values
        ClearMapNpcs()
        ClearMapItems()
        ClearBlood()
        ClearMap()
        ClearMapEvents()

        ' Get map num
        x = buffer.ReadInt32

        ' Get revision
        y = buffer.ReadInt32

        needMap = 1

        ' Either the revisions didn't match or we dont have the map, so we need it
        buffer = New ByteStream(4)
        buffer.WriteInt32(ClientPackets.CNeedMap)
        buffer.WriteInt32(needMap)
        Socket.SendData(buffer.Data, buffer.Head)

        buffer.Dispose()
    End Sub

    Sub Packet_MapData(ByRef data() As Byte)
        Dim x As Integer, y As Integer, i As Integer, j As Integer, mapNum As Integer
        Dim buffer As New ByteStream(Compression.DecompressBytes(data))

        MapData = False

        ClearMap()

        If buffer.ReadInt32 = 1 Then
            mapNum = buffer.ReadInt32
            Map.Name = Trim(buffer.ReadString)
            Map.Music = Trim(buffer.ReadString)
            Map.Revision = buffer.ReadInt32
            Map.Moral = buffer.ReadInt32
            Map.Tileset = buffer.ReadInt32
            Map.Up = buffer.ReadInt32
            Map.Down = buffer.ReadInt32
            Map.Left = buffer.ReadInt32
            Map.Right = buffer.ReadInt32
            Map.BootMap = buffer.ReadInt32
            Map.BootX = buffer.ReadInt32
            Map.BootY = buffer.ReadInt32
            Map.MaxX = buffer.ReadInt32
            Map.MaxY = buffer.ReadInt32
            Map.WeatherType = buffer.ReadInt32
            Map.Fogindex = buffer.ReadInt32
            Map.WeatherIntensity = buffer.ReadInt32
            Map.FogAlpha = buffer.ReadInt32
            Map.FogSpeed = buffer.ReadInt32
            Map.HasMapTint = buffer.ReadInt32
            Map.MapTintR = buffer.ReadInt32
            Map.MapTintG = buffer.ReadInt32
            Map.MapTintB = buffer.ReadInt32
            Map.MapTintA = buffer.ReadInt32
            Map.Instanced = buffer.ReadInt32
            Map.Panorama = buffer.ReadByte
            Map.Parallax = buffer.ReadByte
            Map.Brightness = buffer.ReadByte

            ReDim Map.Tile(Map.MaxX, Map.MaxY)
            For i = 1 To MaxTileHistory
                ReDim TileHistory(i).Tile(Map.MaxX, Map.MaxY)
            Next

            For x = 1 To MAX_MAP_NPCS
                Map.Npc(x) = buffer.ReadInt32
            Next

            For X = 0 To Map.MaxX
                For Y = 0 To Map.MaxY
                    Map.Tile(x, y).Data1 = buffer.ReadInt32
                    Map.Tile(x, y).Data2 = buffer.ReadInt32
                    Map.Tile(x, y).Data3 = buffer.ReadInt32
                    Map.Tile(x, y).DirBlock = buffer.ReadInt32

                    For j = 1 To MaxTileHistory
                        TileHistory(j).Tile(x, y).Data1 = Map.Tile(x, y).Data1
                        TileHistory(j).Tile(x, y).Data2 = Map.Tile(x, y).Data2
                        TileHistory(j).Tile(x, y).Data3 = Map.Tile(x, y).Data3
                        TileHistory(j).Tile(x, y).DirBlock = Map.Tile(x, y).DirBlock
                        TileHistory(j).Tile(x, y).Type = Map.Tile(x, y).Type
                    Next

                    ReDim Map.Tile(x, y).Layer(LayerType.Count - 1)
                    For i = 1 To MaxTileHistory
                        ReDim TileHistory(i).Tile(x, y).Layer(LayerType.Count - 1)
                    Next

                    For i = 1 To LayerType.Count - 1
                        Map.Tile(x, y).Layer(i).Tileset = buffer.ReadInt32
                        Map.Tile(x, y).Layer(i).X = buffer.ReadInt32
                        Map.Tile(x, y).Layer(i).Y = buffer.ReadInt32
                        Map.Tile(x, y).Layer(i).AutoTile = buffer.ReadInt32

                        For j = 1 To MaxTileHistory
                            TileHistory(j).Tile(x, y).Layer(i).Tileset = Map.Tile(x, y).Layer(i).Tileset
                            TileHistory(j).Tile(x, y).Layer(i).X = Map.Tile(x, y).Layer(i).X
                            TileHistory(j).Tile(x, y).Layer(i).Y = Map.Tile(x, y).Layer(i).Y
                            TileHistory(j).Tile(x, y).Layer(i).AutoTile = Map.Tile(x, y).Layer(i).AutoTile
                        Next
                    Next
                    Map.Tile(x, y).Type = buffer.ReadInt32
                Next
            Next

            Map.EventCount = buffer.ReadInt32

            If Map.EventCount > 0 Then
                ReDim Map.Events(Map.EventCount)
                For i = 0 To Map.EventCount
                    With Map.Events(i)
                        .Name = Trim(buffer.ReadString)
                        .Globals = buffer.ReadByte
                        .X = buffer.ReadInt32
                        .Y = buffer.ReadInt32
                        .PageCount = buffer.ReadInt32
                    End With

                    If Map.Events(i).PageCount > 0 Then
                        ReDim Map.Events(i).Pages(Map.Events(i).PageCount)
                        For x = 0 To Map.Events(i).PageCount
                            With Map.Events(i).Pages(x)
                                .ChkVariable = buffer.ReadInt32
                                .Variableindex = buffer.ReadInt32
                                .VariableCondition = buffer.ReadInt32
                                .VariableCompare = buffer.ReadInt32

                                .ChkSwitch = buffer.ReadInt32
                                .Switchindex = buffer.ReadInt32
                                .SwitchCompare = buffer.ReadInt32

                                .ChkHasItem = buffer.ReadInt32
                                .HasItemindex = buffer.ReadInt32
                                .HasItemAmount = buffer.ReadInt32

                                .ChkSelfSwitch = buffer.ReadInt32
                                .SelfSwitchindex = buffer.ReadInt32
                                .SelfSwitchCompare = buffer.ReadInt32

                                .GraphicType = buffer.ReadByte
                                .Graphic = buffer.ReadInt32
                                .GraphicX = buffer.ReadInt32
                                .GraphicY = buffer.ReadInt32
                                .GraphicX2 = buffer.ReadInt32
                                .GraphicY2 = buffer.ReadInt32

                                .MoveType = buffer.ReadByte
                                .MoveSpeed = buffer.ReadByte
                                .MoveFreq = buffer.ReadByte
                                .MoveRouteCount = buffer.ReadInt32
                                .IgnoreMoveRoute = buffer.ReadInt32
                                .RepeatMoveRoute = buffer.ReadInt32

                                If .MoveRouteCount > 0 Then
                                    ReDim Map.Events(i).Pages(x).MoveRoute(.MoveRouteCount)
                                    For y = 0 To .MoveRouteCount
                                        .MoveRoute(y).Index = buffer.ReadInt32
                                        .MoveRoute(y).Data1 = buffer.ReadInt32
                                        .MoveRoute(y).Data2 = buffer.ReadInt32
                                        .MoveRoute(y).Data3 = buffer.ReadInt32
                                        .MoveRoute(y).Data4 = buffer.ReadInt32
                                        .MoveRoute(y).Data5 = buffer.ReadInt32
                                        .MoveRoute(y).Data6 = buffer.ReadInt32
                                    Next
                                End If

                                .WalkAnim = buffer.ReadInt32
                                .DirFix = buffer.ReadInt32
                                .WalkThrough = buffer.ReadInt32
                                .ShowName = buffer.ReadInt32
                                .Trigger = buffer.ReadByte
                                .CommandListCount = buffer.ReadInt32
                                .Position = buffer.ReadByte
                                .QuestNum = buffer.ReadInt32
                            End With

                            If Map.Events(i).Pages(x).CommandListCount > 0 Then
                                ReDim Map.Events(i).Pages(x).CommandList(Map.Events(i).Pages(x).CommandListCount)
                                For y = 0 To Map.Events(i).Pages(x).CommandListCount
                                    Map.Events(i).Pages(x).CommandList(y).CommandCount = buffer.ReadInt32
                                    Map.Events(i).Pages(x).CommandList(y).ParentList = buffer.ReadInt32
                                    If Map.Events(i).Pages(x).CommandList(y).CommandCount > 0 Then
                                        ReDim Map.Events(i).Pages(x).CommandList(y).Commands(Map.Events(i).Pages(x).CommandList(y).CommandCount)
                                        For z = 0 To Map.Events(i).Pages(x).CommandList(y).CommandCount
                                            With Map.Events(i).Pages(x).CommandList(y).Commands(z)
                                                .Index = buffer.ReadByte
                                                .Text1 = Trim(buffer.ReadString)
                                                .Text2 = Trim(buffer.ReadString)
                                                .Text3 = Trim(buffer.ReadString)
                                                .Text4 = Trim(buffer.ReadString)
                                                .Text5 = Trim(buffer.ReadString)
                                                .Data1 = buffer.ReadInt32
                                                .Data2 = buffer.ReadInt32
                                                .Data3 = buffer.ReadInt32
                                                .Data4 = buffer.ReadInt32
                                                .Data5 = buffer.ReadInt32
                                                .Data6 = buffer.ReadInt32
                                                .ConditionalBranch.CommandList = buffer.ReadInt32
                                                .ConditionalBranch.Condition = buffer.ReadInt32
                                                .ConditionalBranch.Data1 = buffer.ReadInt32
                                                .ConditionalBranch.Data2 = buffer.ReadInt32
                                                .ConditionalBranch.Data3 = buffer.ReadInt32
                                                .ConditionalBranch.ElseCommandList = buffer.ReadInt32
                                                .MoveRouteCount = buffer.ReadInt32
                                                If .MoveRouteCount > 0 Then
                                                    ReDim Preserve .MoveRoute(.MoveRouteCount)
                                                    For w = 0 To .MoveRouteCount
                                                        .MoveRoute(w).Index = buffer.ReadInt32
                                                        .MoveRoute(w).Data1 = buffer.ReadInt32
                                                        .MoveRoute(w).Data2 = buffer.ReadInt32
                                                        .MoveRoute(w).Data3 = buffer.ReadInt32
                                                        .MoveRoute(w).Data4 = buffer.ReadInt32
                                                        .MoveRoute(w).Data5 = buffer.ReadInt32
                                                        .MoveRoute(w).Data6 = buffer.ReadInt32
                                                    Next
                                                End If
                                            End With
                                        Next
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
            End If
        End If

        For i = 1 To MAX_MAP_ITEMS
            MapItem(i).Num = buffer.ReadInt32
            MapItem(i).Value = buffer.ReadInt32()
            MapItem(i).X = buffer.ReadInt32()
            MapItem(i).Y = buffer.ReadInt32()
        Next

        For i = 1 To MAX_MAP_NPCS
            MapNpc(i).Num = buffer.ReadInt32()
            MapNpc(i).X = buffer.ReadInt32()
            MapNpc(i).Y = buffer.ReadInt32()
            MapNpc(i).Dir = buffer.ReadInt32()
            MapNpc(i).Vital(VitalType.HP) = buffer.ReadInt32()
            MapNpc(i).Vital(VitalType.MP) = buffer.ReadInt32()
        Next

        If buffer.ReadInt32 = 1 Then
            ResourceIndex = buffer.ReadInt32
            ResourcesInit = False
            ReDim MapResource(ResourceIndex)

            If ResourceIndex > 0 Then
                For i = 0 To ResourceIndex
                    MapResource(i).State = buffer.ReadByte
                    MapResource(i).X = buffer.ReadInt32
                    MapResource(i).Y = buffer.ReadInt32
                Next

                ResourcesInit = True
            End If
        End If

        buffer.Dispose()

        InitAutotiles()

        MapData = True

        For i = 0 To Byte.MaxValue
            ClearActionMsg(i)
        Next

        CurrentWeather = Map.WeatherType
        CurrentWeatherIntensity = Map.WeatherIntensity
        CurrentFog = Map.Fogindex
        CurrentFogSpeed = Map.FogSpeed
        CurrentFogOpacity = Map.FogAlpha
        CurrentTintR = Map.MapTintR
        CurrentTintG = Map.MapTintG
        CurrentTintB = Map.MapTintB
        CurrentTintA = Map.MapTintA

        PlayMusic(Trim$(Map.Music))

        UpdateDrawMapName()

        GettingMap = False
        CanMoveNow = True

    End Sub

    Sub Packet_MapNPCData(ByRef data() As Byte)
        Dim i As Integer
        Dim buffer As New ByteStream(data)

        For i = 1 To MAX_MAP_NPCS

            With MapNpc(i)
                .Num = buffer.ReadInt32
                .X = buffer.ReadInt32
                .Y = buffer.ReadInt32
                .Dir = buffer.ReadInt32
                .Vital(VitalType.HP) = buffer.ReadInt32
                .Vital(VitalType.MP) = buffer.ReadInt32
            End With

        Next

        buffer.Dispose()
    End Sub

    Sub Packet_MapNPCUpdate(ByRef data() As Byte)
        Dim npcNum As Integer
        Dim buffer As New ByteStream(data)
        npcNum = buffer.ReadInt32

        With MapNpc(npcNum)
            .Num = buffer.ReadInt32
            .X = buffer.ReadInt32
            .Y = buffer.ReadInt32
            .Dir = buffer.ReadInt32
            .Vital(VitalType.HP) = buffer.ReadInt32
            .Vital(VitalType.MP) = buffer.ReadInt32
        End With

        buffer.Dispose()
    End Sub

    Sub Packet_MapDone(ByRef data() As Byte)
        Dim i As Integer
        Dim musicFile As String

        For i = 0 To Byte.MaxValue
            ClearActionMsg(i)
        Next

        CurrentWeather = Map.WeatherType
        CurrentWeatherIntensity = Map.WeatherIntensity
        CurrentFog = Map.Fogindex
        CurrentFogSpeed = Map.FogSpeed
        CurrentFogOpacity = Map.FogAlpha
        CurrentTintR = Map.MapTintR
        CurrentTintG = Map.MapTintG
        CurrentTintB = Map.MapTintB
        CurrentTintA = Map.MapTintA

        musicFile = Trim$(Map.Music)
        PlayMusic(musicFile)

        UpdateDrawMapName()

        GettingMap = False
        CanMoveNow = True

    End Sub

#End Region

#Region "Outgoing Packets"

    Friend Sub SendPlayerRequestNewMap()
        If GettingMap Then Exit Sub

        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CRequestNewMap)
        buffer.WriteInt32(GetPlayerDir(Myindex))

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()

    End Sub

    Friend Sub SendRequestEditMap()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CRequestEditMap)
        Socket.SendData(buffer.Data, buffer.Head)

        buffer.Dispose()
    End Sub

    Friend Sub SendMap()
        Dim x As Integer, y As Integer, i As Integer
        Dim data() As Byte
        Dim buffer As New ByteStream(4)
        CanMoveNow = False

        buffer.WriteString((Map.Name.Trim))
        buffer.WriteString((Map.Music.Trim))
        buffer.WriteInt32(Map.Moral)
        buffer.WriteInt32(Map.Tileset)
        buffer.WriteInt32(Map.Up)
        buffer.WriteInt32(Map.Down)
        buffer.WriteInt32(Map.Left)
        buffer.WriteInt32(Map.Right)
        buffer.WriteInt32(Map.BootMap)
        buffer.WriteInt32(Map.BootX)
        buffer.WriteInt32(Map.BootY)
        buffer.WriteInt32(Map.MaxX)
        buffer.WriteInt32(Map.MaxY)
        buffer.WriteInt32(Map.WeatherType)
        buffer.WriteInt32(Map.Fogindex)
        buffer.WriteInt32(Map.WeatherIntensity)
        buffer.WriteInt32(Map.FogAlpha)
        buffer.WriteInt32(Map.FogSpeed)
        buffer.WriteInt32(Map.HasMapTint)
        buffer.WriteInt32(Map.MapTintR)
        buffer.WriteInt32(Map.MapTintG)
        buffer.WriteInt32(Map.MapTintB)
        buffer.WriteInt32(Map.MapTintA)
        buffer.WriteInt32(Map.Instanced)
        buffer.WriteByte(Map.Panorama)
        buffer.WriteByte(Map.Parallax)
        buffer.WriteByte(Map.Brightness)

        For i = 1 To MAX_MAP_NPCS
            buffer.WriteInt32(Map.Npc(i))
        Next

        For x = 0 To Map.MaxX
            For y = 0 To Map.MaxY
                buffer.WriteInt32(Map.Tile(x, y).Data1)
                buffer.WriteInt32(Map.Tile(x, y).Data2)
                buffer.WriteInt32(Map.Tile(x, y).Data3)
                buffer.WriteInt32(Map.Tile(x, y).DirBlock)
                For i = 1 To LayerType.Count - 1
                    buffer.WriteInt32(Map.Tile(x, y).Layer(i).Tileset)
                    buffer.WriteInt32(Map.Tile(x, y).Layer(i).X)
                    buffer.WriteInt32(Map.Tile(x, y).Layer(i).Y)
                    buffer.WriteInt32(Map.Tile(x, y).Layer(i).AutoTile)
                Next
                buffer.WriteInt32(Map.Tile(x, y).Type)
            Next
        Next

        buffer.WriteInt32(Map.EventCount)

        If Map.EventCount > 0 Then
           For i = 0 To Map.EventCount
                With Map.Events(i)    
                    If .Name Is Nothing then .Name = ""
                    buffer.WriteString((.Name.Trim))
                    buffer.WriteByte(.Globals)
                    buffer.WriteInt32(.X)
                    buffer.WriteInt32(.Y)
                    buffer.WriteInt32(.PageCount)
                End With
                If Map.Events(i).PageCount > 0 Then
                    For x = 0 To Map.Events(i).PageCount
                        With Map.Events(i).Pages(x)
                            buffer.WriteInt32(.ChkVariable)
                            buffer.WriteInt32(.Variableindex)
                            buffer.WriteInt32(.VariableCondition)
                            buffer.WriteInt32(.VariableCompare)
                            buffer.WriteInt32(.ChkSwitch)
                            buffer.WriteInt32(.Switchindex)
                            buffer.WriteInt32(.SwitchCompare)
                            buffer.WriteInt32(.ChkHasItem)
                            buffer.WriteInt32(.HasItemindex)
                            buffer.WriteInt32(.HasItemAmount)
                            buffer.WriteInt32(.ChkSelfSwitch)
                            buffer.WriteInt32(.SelfSwitchindex)
                            buffer.WriteInt32(.SelfSwitchCompare)
                            buffer.WriteByte(.GraphicType)
                            buffer.WriteInt32(.Graphic)
                            buffer.WriteInt32(.GraphicX)
                            buffer.WriteInt32(.GraphicY)
                            buffer.WriteInt32(.GraphicX2)
                            buffer.WriteInt32(.GraphicY2)
                            buffer.WriteByte(.MoveType)
                            buffer.WriteByte(.MoveSpeed)
                            buffer.WriteByte(.MoveFreq)
                            buffer.WriteInt32(Map.Events(i).Pages(x).MoveRouteCount)
                            buffer.WriteInt32(.IgnoreMoveRoute)
                            buffer.WriteInt32(.RepeatMoveRoute)

                            If .MoveRouteCount > 0 Then
                                For y = 0 To .MoveRouteCount
                                    buffer.WriteInt32(.MoveRoute(y).Index)
                                    buffer.WriteInt32(.MoveRoute(y).Data1)
                                    buffer.WriteInt32(.MoveRoute(y).Data2)
                                    buffer.WriteInt32(.MoveRoute(y).Data3)
                                    buffer.WriteInt32(.MoveRoute(y).Data4)
                                    buffer.WriteInt32(.MoveRoute(y).Data5)
                                    buffer.WriteInt32(.MoveRoute(y).Data6)
                                Next
                            End If

                            buffer.WriteInt32(.WalkAnim)
                            buffer.WriteInt32(.DirFix)
                            buffer.WriteInt32(.WalkThrough)
                            buffer.WriteInt32(.ShowName)
                            buffer.WriteByte(.Trigger)
                            buffer.WriteInt32(.CommandListCount)
                            buffer.WriteByte(.Position)
                            buffer.WriteInt32(.Questnum)
                        End With

                        If Map.Events(i).Pages(x).CommandListCount > 0 Then
                            For y = 0 To Map.Events(i).Pages(x).CommandListCount
                                buffer.WriteInt32(Map.Events(i).Pages(x).CommandList(y).CommandCount)
                                buffer.WriteInt32(Map.Events(i).Pages(x).CommandList(y).ParentList)
                                If Map.Events(i).Pages(x).CommandList(y).CommandCount > 0 Then
                                    For z = 0 To Map.Events(i).Pages(x).CommandList(y).CommandCount
                                        With Map.Events(i).Pages(x).CommandList(y).Commands(z)
                                            buffer.WriteByte(.Index)
                                            buffer.WriteString((.Text1))
                                            buffer.WriteString((.Text2))
                                            buffer.WriteString((.Text3))
                                            buffer.WriteString((.Text4))
                                            buffer.WriteString((.Text5))
                                            buffer.WriteInt32(.Data1)
                                            buffer.WriteInt32(.Data2)
                                            buffer.WriteInt32(.Data3)
                                            buffer.WriteInt32(.Data4)
                                            buffer.WriteInt32(.Data5)
                                            buffer.WriteInt32(.Data6)
                                            buffer.WriteInt32(.ConditionalBranch.CommandList)
                                            buffer.WriteInt32(.ConditionalBranch.Condition)
                                            buffer.WriteInt32(.ConditionalBranch.Data1)
                                            buffer.WriteInt32(.ConditionalBranch.Data2)
                                            buffer.WriteInt32(.ConditionalBranch.Data3)
                                            buffer.WriteInt32(.ConditionalBranch.ElseCommandList)
                                            buffer.WriteInt32(.MoveRouteCount)
                                            If .MoveRouteCount > 0 Then
                                                For w = 0 To .MoveRouteCount
                                                    buffer.WriteInt32(.MoveRoute(w).Index)
                                                    buffer.WriteInt32(.MoveRoute(w).Data1)
                                                    buffer.WriteInt32(.MoveRoute(w).Data2)
                                                    buffer.WriteInt32(.MoveRoute(w).Data3)
                                                    buffer.WriteInt32(.MoveRoute(w).Data4)
                                                    buffer.WriteInt32(.MoveRoute(w).Data5)
                                                    buffer.WriteInt32(.MoveRoute(w).Data6)
                                                Next
                                            End If
                                        End With
                                    Next
                                End If
                            Next
                        End If
                    Next
                End If
            Next
        End If

        data = buffer.ToArray

        buffer = New ByteStream(4)
        buffer.WriteInt32(ClientPackets.CSaveMap)
        buffer.WriteBlock(Compression.CompressBytes(data))

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub SendMapRespawn()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(ClientPackets.CMapRespawn)

        Socket.SendData(buffer.Data, buffer.Head)
        buffer.Dispose()
    End Sub

    Friend Sub ClearMapEvents()
        ReDim Map.MapEvents(Map.EventCount)

        For i = 0 To Map.EventCount
            Map.MapEvents(i).Name = ""
        Next

        Map.CurrentEvents = 0

    End Sub

#End Region

#Region "Drawing"

    Friend Sub DrawMapTile(x As Integer, y As Integer)
        Dim i As Integer, alpha As Byte
        Dim srcrect As New Rectangle(0, 0, 0, 0)

        If GettingMap Then Exit Sub
        If Map.Tile Is Nothing Then Exit Sub
        If MapData = False Then Exit Sub

        For i = LayerType.Ground To LayerType.Cover
            If Map.Tile(x, y).Layer Is Nothing Then Exit Sub

            ' skip tile if tileset isn't set
            If Map.Tile(x, y).Layer(i).Tileset > 0 AndAlso Map.Tile(x, y).Layer(i).Tileset <= NumTileSets Then
                If TileSetTextureInfo(Map.Tile(x, y).Layer(i).Tileset).IsLoaded = False Then
                    LoadTexture(Map.Tile(x, y).Layer(i).Tileset, 1)
                End If

                ' we use it, lets update timer
                With TileSetTextureInfo(Map.Tile(x, y).Layer(i).Tileset)
                    .TextureTimer = GetTickCount() + 100000
                End With

                If Autotile(x, y).Layer(i).RenderState = RenderStateNormal Then
                    With srcrect
                        .X = Map.Tile(x, y).Layer(i).X * 32
                        .Y = Map.Tile(x, y).Layer(i).Y * 32
                        .Width = 32
                        .Height = 32
                    End With

                    If Editor = EditorType.Map and HideLayers Then
                        If i = frmEditor_Map.cmbLayers.SelectedIndex Then
                            alpha = 255
                        Else
                            alpha = 127
                        End If
                    Else
                        alpha = 255
                    End If

                    RenderTexture(TileSetSprite(Map.Tile(x, y).Layer(i).Tileset), GameWindow, ConvertMapX(x * PicX), ConvertMapY(y * PicY), srcrect.X, srcrect.Y, srcrect.Width, srcrect.Height, , , alpha)
                ElseIf Autotile(x, y).Layer(i).RenderState = RenderStateAutotile Then
                    ' Draw autotiles
                    DrawAutoTile(i, ConvertMapX(x * PicX), ConvertMapY(y * PicY), 1, x, y, 0, False)
                    DrawAutoTile(i, ConvertMapX(x * PicX) + 16, ConvertMapY(y * PicY), 2, x, y, 0, False)
                    DrawAutoTile(i, ConvertMapX(x * PicX), ConvertMapY(y * PicY) + 16, 3, x, y, 0, False)
                    DrawAutoTile(i, ConvertMapX(x * PicX) + 16, ConvertMapY(y * PicY) + 16, 4, x, y, 0, False)
                End If
            End If
        Next

    End Sub

    Friend Sub DrawMapFringeTile(x As Integer, y As Integer)
        Dim i As Integer, alpha As Integer
        Dim srcrect As New Rectangle(0, 0, 0, 0)

        If GettingMap Then Exit Sub
        If Map.Tile Is Nothing Then Exit Sub
        If MapData = False Then Exit Sub

        For i = LayerType.Fringe To LayerType.Roof
            If Map.Tile(x, y).Layer Is Nothing Then Exit Sub
            ' skip tile if tileset isn't set
            If Map.Tile(x, y).Layer(i).Tileset > 0 AndAlso Map.Tile(x, y).Layer(i).Tileset <= NumTileSets Then
                If TileSetTextureInfo(Map.Tile(x, y).Layer(i).Tileset).IsLoaded = False Then
                    LoadTexture(Map.Tile(x, y).Layer(i).Tileset, 1)
                End If

                ' we use it, lets update timer
                With TileSetTextureInfo(Map.Tile(x, y).Layer(i).Tileset)
                    .TextureTimer = GetTickCount() + 100000
                End With

                ' render
                If Autotile(x, y).Layer(i).RenderState = RenderStateNormal Then
                    With srcrect
                        .X = Map.Tile(x, y).Layer(i).X * 32
                        .Y = Map.Tile(x, y).Layer(i).Y * 32
                        .Width = 32
                        .Height = 32
                    End With

                    If Editor = EditorType.Map And HideLayers Then
                        If i = frmEditor_Map.cmbLayers.SelectedIndex Then
                            alpha = 255
                        Else
                            alpha = 127
                        End If
                    Else   
                        alpha = 255
                    End If

                    RenderTexture(TileSetSprite(Map.Tile(x, y).Layer(i).Tileset), GameWindow, ConvertMapX(x * PicX), ConvertMapY(y * PicY), srcrect.X, srcrect.Y, srcrect.Width, srcrect.Height, , , alpha)

                ElseIf Autotile(x, y).Layer(i).RenderState = RenderStateAutotile Then
                    ' Draw autotiles
                    DrawAutoTile(i, ConvertMapX(x * PicX), ConvertMapY(y * PicY), 1, x, y, 0, False)
                    DrawAutoTile(i, ConvertMapX(x * PicX) + 16, ConvertMapY(y * PicY), 2, x, y, 0, False)
                    DrawAutoTile(i, ConvertMapX(x * PicX), ConvertMapY(y * PicY) + 16, 3, x, y, 0, False)
                    DrawAutoTile(i, ConvertMapX(x * PicX) + 16, ConvertMapY(y * PicY) + 16, 4, x, y, 0, False)
                End If
            End If
        Next

    End Sub

#End Region

End Module