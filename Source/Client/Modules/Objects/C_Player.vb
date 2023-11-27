Imports Mirage.Sharp.Asfw
Imports Core
Imports SFML.Graphics
Imports SFML.System

Module C_Player
#Region "Database"
    Sub ClearPlayers()
        Dim i As Integer

        ReDim Account(MAX_PLAYERS)
        ReDim Player(MAX_PLAYERS)

        For i = 1 To MAX_PLAYERS
            ClearAccount(i)
            ClearPlayer(i)
        Next
    End Sub

    Sub ClearAccount(index As Integer)
        Account(index).Login = ""
    End Sub

    Sub ClearPlayer(index As Integer)
        ClearAccount(index)

        Player(index).Name = ""
        Player(index).Attacking = 0
        Player(index).AttackTimer = 0
        Player(index).Job = 1
        Player(index).Dir = 0
        Player(index).Access = AdminType.Player

        ReDim Player(index).Equipment(EquipmentType.Count - 1)
        For y = 0 To EquipmentType.Count - 1
            Player(index).Equipment(y) = -1
        Next

        Player(index).Exp = 0
        Player(index).Level = 0
        Player(index).Map = 1
        Player(index).MapGetTimer = 0
        Player(index).Moving = 0
        Player(index).Pk = 0
        Player(index).Points = 0
        Player(index).Sprite = 0

        ReDim Player(index).Inv(MAX_INV)
        For x = 1 To MAX_INV
            Player(index).Inv(x).Num = 0
            Player(index).Inv(x).Value = 0
        Next

        ReDim Player(index).Skill(MAX_SKILLS)
        For x = 1 To MAX_SKILLS
            Player(index).Skill(x).Num = 0
            Player(index).Skill(x).CD = 0
        Next

        ReDim Player(index).Stat(StatType.Count - 1)
        For x = 0 To StatType.Count - 1
            Player(index).Stat(x) = 0
        Next

        Player(index).Steps = 0

        ReDim Player(index).Vital(VitalType.Count - 1)
        For x = 0 To VitalType.Count - 1
            Player(index).Vital(x) = 0
        Next

        Player(index).X = 0
        Player(index).XOffset = 0
        Player(index).Y = 0
        Player(index).YOffset = 0

        ReDim Player(index).Hotbar(MAX_HOTBAR)
        ReDim Player(index).GatherSkills(ResourceType.Count - 1)
        ReDim Player(index).GatherSkills(ResourceType.Count - 1)

        'pets
        Player(index).Pet.Num = 0
        Player(index).Pet.Health = 0
        Player(index).Pet.Mana = 0
        Player(index).Pet.Level = 0

        ReDim Player(index).Pet.Stat(StatType.Count - 1)
        For x = 0 To StatType.Count - 1
            Player(index).Pet.Stat(x) = 0
        Next

        ReDim Player(index).Pet.Skill(4)
        For x = 0 To 4
            Player(index).Pet.Skill(x) = 0
        Next

        Player(index).Pet.X = 0
        Player(index).Pet.Y = 0
        Player(index).Pet.Dir = 0
        Player(index).Pet.MaxHp = 0
        Player(index).Pet.MaxMp = 0
        Player(index).Pet.Alive = 0
        Player(index).Pet.AttackBehaviour = 0
        Player(index).Pet.Exp = 0
        Player(index).Pet.Tnl = 0
    End Sub
#End Region

#Region "Movement"
    Sub CheckMovement()
        If IsTryingToMove() AndAlso CanMove() Then
            ' Check if player has the shift key down for running
            If VbKeyShift Then
                Player(Myindex).Moving = MovementType.Walking
            Else
                Player(Myindex).Moving = MovementType.Running               
            End If

            Select Case GetPlayerDir(Myindex)
                Case DirectionType.Up
                    SendPlayerMove()
                    Player(Myindex).YOffset = PicY
                    SetPlayerY(Myindex, GetPlayerY(Myindex) - 1)
                Case DirectionType.Down
                    SendPlayerMove()
                    Player(Myindex).YOffset = PicY * -1
                    SetPlayerY(Myindex, GetPlayerY(Myindex) + 1)
                Case DirectionType.Left
                    SendPlayerMove()
                    Player(Myindex).XOffset = PicX
                    SetPlayerX(Myindex, GetPlayerX(Myindex) - 1)
                Case DirectionType.Right
                    SendPlayerMove()
                    Player(Myindex).XOffset = PicX * -1
                    SetPlayerX(Myindex, GetPlayerX(Myindex) + 1)
            End Select

            If Player(Myindex).XOffset = 0 AndAlso Player(Myindex).YOffset = 0 Then
                If Map.Tile(GetPlayerX(Myindex), GetPlayerY(Myindex)).Type = TileType.Warp Then
                    GettingMap = True
                End If
            End If

        End If
    End Sub

    Function IsTryingToMove() As Boolean

        If DirUp OrElse DirDown OrElse DirLeft OrElse DirRight Then
            IsTryingToMove = True
        End If

    End Function

    Function CanMove() As Boolean
        Dim d As Integer
        CanMove = True

        If HoldPlayer = True Then
            CanMove = False
            Exit Function
        End If

        If GettingMap = True Then
            CanMove = False
            Exit Function
        End If

        ' Make sure they aren't trying to move when they are already moving
        If Player(Myindex).Moving <> 0 Then
            CanMove = False
            Exit Function
        End If

        ' Make sure they haven't just casted a skill
        If SkillBuffer > 0 Then
            CanMove = False
            Exit Function
        End If

        ' make sure they're not stunned
        If StunDuration > 0 Then
            CanMove = False
            Exit Function
        End If

        If InEvent Then
            CanMove = False
            Exit Function
        End If

        ' make sure they're not in a shop
        If InShop > 0 Then
            CanMove = False
            Exit Function
        End If

        If InTrade Then
            CanMove = False
            Exit Function
        End If

        If InBank Then
            CloseBank()
        End If

        If Not inSmallChat Then
            CanMove = False
            Exit Function
        End If

        d = GetPlayerDir(Myindex)

        If DirUp Then
            SetPlayerDir(Myindex, DirectionType.Up)

            ' Check to see if they are trying to go out of bounds
            If GetPlayerY(Myindex) > 0 Then
                If CheckDirection(DirectionType.Up) Then
                    CanMove = False

                    ' Set the new direction if they weren't facing that direction
                    If d <> DirectionType.Up Then
                        SendPlayerDir()
                    End If

                    Exit Function
                End If
            Else

                ' Check if they can warp to a new map
                If Map.Up > 0 Then
                    SendPlayerRequestNewMap()
                    GettingMap = True
                    CanMoveNow = False
                End If

                CanMove = False
                Exit Function
            End If
        End If

        If DirDown Then
            SetPlayerDir(Myindex, DirectionType.Down)

            ' Check to see if they are trying to go out of bounds
            If GetPlayerY(Myindex) < Map.MaxY Then
                If CheckDirection(DirectionType.Down) Then
                    CanMove = False

                    ' Set the new direction if they weren't facing that direction
                    If d <> DirectionType.Down Then
                        SendPlayerDir()
                    End If

                    Exit Function
                End If
            Else

                ' Check if they can warp to a new map
                If Map.Down > 0 Then
                    SendPlayerRequestNewMap()
                    GettingMap = True
                    CanMoveNow = False
                End If

                CanMove = False
                Exit Function
            End If
        End If

        If DirLeft Then
            SetPlayerDir(Myindex, DirectionType.Left)

            ' Check to see if they are trying to go out of bounds
            If GetPlayerX(Myindex) > 0 Then
                If CheckDirection(DirectionType.Left) Then
                    CanMove = False

                    ' Set the new direction if they weren't facing that direction
                    If d <> DirectionType.Left Then
                        SendPlayerDir()
                    End If

                    Exit Function
                End If
            Else

                ' Check if they can warp to a new map
                If Map.Left > 0 Then
                    SendPlayerRequestNewMap()
                    GettingMap = True
                    CanMoveNow = False
                End If

                CanMove = False
                Exit Function
            End If
        End If

        If DirRight Then
            SetPlayerDir(Myindex, DirectionType.Right)

            ' Check to see if they are trying to go out of bounds
            If GetPlayerX(Myindex) < Map.MaxX Then
                If CheckDirection(DirectionType.Right) Then
                    CanMove = False

                    ' Set the new direction if they weren't facing that direction
                    If d <> DirectionType.Right Then
                        SendPlayerDir()
                    End If

                    Exit Function
                End If
            Else

                ' Check if they can warp to a new map
                If Map.Right > 0 Then
                    SendPlayerRequestNewMap()
                    GettingMap = True
                    CanMoveNow = False
                End If

                CanMove = False
                Exit Function
            End If
        End If

    End Function

    Function CheckDirection(direction As Byte) As Boolean
        Dim x As Integer, y As Integer
        Dim i As Integer

        CheckDirection = False

        ' check directional blocking
        If IsDirBlocked(Map.Tile(GetPlayerX(Myindex), GetPlayerY(Myindex)).DirBlock, direction) Then
            CheckDirection = True
            Exit Function
        End If

        Select Case direction
            Case DirectionType.Up
                x = GetPlayerX(Myindex)
                y = GetPlayerY(Myindex) - 1
            Case DirectionType.Down
                x = GetPlayerX(Myindex)
                y = GetPlayerY(Myindex) + 1
            Case DirectionType.Left
                x = GetPlayerX(Myindex) - 1
                y = GetPlayerY(Myindex)
            Case DirectionType.Right
                x = GetPlayerX(Myindex) + 1
                y = GetPlayerY(Myindex)
        End Select

        ' Check to see if the map tile is blocked or not
        If Map.Tile(x, y).Type = TileType.Blocked Then
            CheckDirection = True
            Exit Function
        End If

        ' Check to see if the map tile is tree or not
        If Map.Tile(x, y).Type = TileType.Resource Then
            CheckDirection = True
            Exit Function
        End If

        ' Check to see if a npc is already on that tile
        For i = 1 To MAX_MAP_NPCS
            If MapNpc(i).Num > 0 AndAlso MapNpc(i).X = x AndAlso MapNpc(i).Y = y Then
                CheckDirection = True
                Exit Function
            End If
        Next

       For i = 0 To Map.CurrentEvents
            If Map.MapEvents(i).Visible = 1 Then
                If Map.MapEvents(i).X = x AndAlso Map.MapEvents(i).Y = y Then
                    If Map.MapEvents(i).WalkThrough = 0 Then
                        CheckDirection = True
                        Exit Function
                    End If
                End If
            End If
        Next

    End Function

    Sub ProcessMovement(index As Integer)
        Dim movementSpeed As Integer

        ' Check if player is walking, and if so process moving them over
        Select Case Player(index).Moving
            Case MovementType.Walking : movementSpeed = ((ElapsedTime / 1000) * (WalkSpeed * SizeX))
            Case MovementType.Running : movementSpeed = ((ElapsedTime / 1000) * (RunSpeed * SizeX))
            Case Else : Exit Sub
        End Select

        Select Case GetPlayerDir(index)
            Case DirectionType.Up
                Player(Index).YOffset = Player(Index).YOffset - movementSpeed
                If Player(Index).YOffset < 0 Then Player(Index).YOffset = 0
            Case DirectionType.Down
                Player(Index).YOffset = Player(Index).YOffset + movementSpeed
                If Player(Index).YOffset > 0 Then Player(Index).YOffset = 0
            Case DirectionType.Left
                Player(Index).XOffset = Player(Index).XOffset - movementSpeed
                If Player(Index).XOffset < 0 Then Player(Index).XOffset = 0
            Case DirectionType.Right
                Player(Index).XOffset = Player(Index).XOffset + movementSpeed
                If Player(Index).XOffset > 0 Then Player(Index).XOffset = 0
        End Select

        ' Check if completed walking over to the next tile
        If Player(Index).Moving > 0 Then
            If GetPlayerDir(Index) = DirectionType.Right OrElse GetPlayerDir(Index) = DirectionType.Down Then
                If (Player(Index).XOffset >= 0) AndAlso (Player(Index).YOffset >= 0) Then
                    Player(Index).Moving = 0
                    If Player(Index).Steps = 1 Then
                        Player(Index).Steps = 3
                    Else
                        Player(Index).Steps = 1
                    End If
                End If
            Else
                If (Player(Index).XOffset <= 0) AndAlso (Player(Index).YOffset <= 0) Then
                    Player(Index).Moving = 0
                    If Player(Index).Steps = 1 Then
                        Player(Index).Steps = 3
                    Else
                        Player(Index).Steps = 1
                    End If
                End If
            End If
        End If

    End Sub

#End Region

#Region "Attacking"
    Sub CheckAttack()
        Dim attackspeed As Integer, x As Integer, y As Integer
        Dim buffer As New ByteStream(4)

        If VbKeyControl Then
            If InEvent = True Then Exit Sub
            If SkillBuffer > 0 Then Exit Sub ' currently casting a skill, can't attack
            If StunDuration > 0 Then Exit Sub ' stunned, can't attack

            ' speed from weapon
            If GetPlayerEquipment(Myindex, EquipmentType.Weapon) > 0 Then
                attackspeed = Item(GetPlayerEquipment(Myindex, EquipmentType.Weapon)).Speed * 1000
            Else
                attackspeed = 1000
            End If

            If Player(Myindex).AttackTimer + attackspeed < GetTickCount() Then
                If Player(Myindex).Attacking = 0 Then

                    With Player(Myindex)
                        .Attacking = 1
                        .AttackTimer = GetTickCount()
                    End With

                    SendAttack()
                End If
            End If

            Select Case Player(Myindex).Dir
                Case DirectionType.Up
                    x = GetPlayerX(Myindex)
                    y = GetPlayerY(Myindex) - 1
                Case DirectionType.Down
                    x = GetPlayerX(Myindex)
                    y = GetPlayerY(Myindex) + 1
                Case DirectionType.Left
                    x = GetPlayerX(Myindex) - 1
                    y = GetPlayerY(Myindex)
                Case DirectionType.Right
                    x = GetPlayerX(Myindex) + 1
                    y = GetPlayerY(Myindex)
            End Select

            If GetTickCount() > Player(Myindex).EventTimer Then
                For i = 1 To Map.CurrentEvents
                    If Map.MapEvents(i).Visible = 1 Then
                        If Map.MapEvents(i).X = x AndAlso Map.MapEvents(i).Y = y Then
                            buffer = New ByteStream(4)
                            buffer.WriteInt32(ClientPackets.CEvent)
                            buffer.WriteInt32(i)
                            Socket.SendData(buffer.Data, buffer.Head)
                            buffer.Dispose()
                            Player(Myindex).EventTimer = GetTickCount() + 200
                        End If
                    End If
                Next
            End If
        End If

    End Sub

    Friend Sub PlayerCastSkill(skillslot As Integer)
        Dim buffer As New ByteStream(4)

        ' Check for subscript out of range
        If skillslot < 0 OrElse skillslot > MAX_PLAYER_SKILLS Then Exit Sub

        If Player(Myindex).Skill(skillslot).CD > 0 Then
            AddText("Skill has not cooled down yet!", QColorType.AlertColor)
            Exit Sub
        End If

        ' Check if player has enough MP
        If GetPlayerVital(Myindex, VitalType.MP) < Skill(Player(Myindex).Skill(skillslot).Num).MpCost Then
            AddText("Not enough MP to cast " & Trim$(Skill(Player(Myindex).Skill(skillslot).Num).Name) & ".", QColorType.AlertColor)
            Exit Sub
        End If

        If Player(Myindex).Skill(skillslot).Num > 0 Then
            If GetTickCount() > Player(Myindex).AttackTimer + 1000 Then
                If Player(Myindex).Moving = 0 Then
                    buffer.WriteInt32(ClientPackets.CCast)
                    buffer.WriteInt32(skillslot)

                    Socket.SendData(buffer.Data, buffer.Head)
                    buffer.Dispose()

                    SkillBuffer = skillslot
                    SkillBufferTimer = GetTickCount()
                Else
                    AddText("Cannot cast while walking!", QColorType.AlertColor)
                End If
            End If
        Else
            AddText("No skill here.", QColorType.AlertColor)
        End If

    End Sub
#End Region

#Region "Drawing"
    Friend Sub DrawPlayer(index As Integer)
        Dim anim As Byte, x As Integer, y As Integer
        Dim spritenum As Integer, spriteleft As Integer
        Dim attackspeed As Integer, attackSprite As Byte
        Dim srcrec As Rectangle

        spritenum = GetPlayerSprite(index)

        attackSprite = 0

        If spritenum <= 0 OrElse spritenum > NumCharacters Then Exit Sub

        ' speed from weapon
        If GetPlayerEquipment(index, EquipmentType.Weapon) > 0 Then
            attackspeed = Item(GetPlayerEquipment(index, EquipmentType.Weapon)).Speed
        Else
            attackspeed = 1000
        End If

        ' Reset frame
        anim = 0

        ' Check for attacking animation
        If Player(index).AttackTimer + (attackspeed / 2) > GetTickCount() Then
            If Player(index).Attacking = 1 Then
                If attackSprite = 1 Then
                    anim = 4
                Else
                    anim = 3
                End If
            End If
        Else
            ' If not attacking, walk normally
            Select Case GetPlayerDir(index)
                Case DirectionType.Up

                    If (Player(index).YOffset > 8) Then anim = Player(index).Steps
                Case DirectionType.Down

                    If (Player(index).YOffset < -8) Then anim = Player(index).Steps
                Case DirectionType.Left

                    If (Player(index).XOffset > 8) Then anim = Player(index).Steps
                Case DirectionType.Right

                    If (Player(index).XOffset < -8) Then anim = Player(index).Steps
            End Select

        End If

        ' Check to see if we want to stop making him attack
        With Player(index)
            If .AttackTimer + attackspeed < GetTickCount() Then
                .Attacking = 0
                .AttackTimer = 0
            End If

        End With

        ' Set the left
        Select Case GetPlayerDir(index)
            Case DirectionType.Up
                spriteleft = 3
            Case DirectionType.Right
                spriteleft = 2
            Case DirectionType.Down
                spriteleft = 0
            Case DirectionType.Left
                spriteleft = 1
        End Select

        If attackSprite = 1 Then
            srcrec = New Rectangle((anim) * (CharacterGfxInfo(spritenum).Width / 5), spriteleft * (CharacterGfxInfo(spritenum).Height / 4), (CharacterGfxInfo(spritenum).Width / 5), (CharacterGfxInfo(spritenum).Height / 4))
        Else
            srcrec = New Rectangle((anim) * (CharacterGfxInfo(spritenum).Width / 4), spriteleft * (CharacterGfxInfo(spritenum).Height / 4), (CharacterGfxInfo(spritenum).Width / 4), (CharacterGfxInfo(spritenum).Height / 4))
        End If

        ' Calculate the X
        If attackSprite = 1 Then
            x = GetPlayerX(index) * PicX + Player(index).XOffset - ((CharacterGfxInfo(spritenum).Width / 5 - 32) / 2)
        Else
            x = GetPlayerX(index) * PicX + Player(index).XOffset - ((CharacterGfxInfo(spritenum).Width / 4 - 32) / 2)
        End If

        ' Is the player's height more than 32..?
        If (CharacterGfxInfo(spritenum).Height) > 32 Then
            ' Create a 32 pixel offset for larger sprites
            y = GetPlayerY(index) * PicY + Player(index).YOffset - ((CharacterGfxInfo(spritenum).Height / 4) - 32)
        Else
            ' Proceed as normal
            y = GetPlayerY(index) * PicY + Player(index).YOffset
        End If

        ' render the actual sprite
        DrawCharacter(spritenum, x, y, srcrec)

        'check for paperdolling
        For i = 1 To EquipmentType.Count - 1
            If GetPlayerEquipment(index, i) > 0 Then
                If Item(GetPlayerEquipment(index, i)).Paperdoll > 0 Then
                    DrawPaperdoll(x, y, Item(GetPlayerEquipment(index, i)).Paperdoll, anim, spriteleft)
                End If
            End If
        Next

        ' Check to see if we want to stop showing emote
        With Player(index)
            If .EmoteTimer < GetTickCount() Then
                .Emote = 0
                .EmoteTimer = 0
            End If
        End With

        'check for emotes
        If Player(Myindex).Emote > 0 Then
            DrawEmotes(x, y, Player(Myindex).Emote)
        End If
    End Sub

    Friend Sub DrawPlayerName(index As Integer)
        Dim textX As Integer
        Dim textY As Integer
        Dim color As SFML.Graphics.Color, backcolor As SFML.Graphics.Color
        Dim name As String

        ' Check access level
        If GetPlayerPK(index) = False Then
            Select Case GetPlayerAccess(index)
                Case AdminType.Player
                    color = SFML.Graphics.Color.Red
                    backcolor = SFML.Graphics.Color.Black
                Case AdminType.Moderator
                    color = SFML.Graphics.Color.Black
                    backcolor = SFML.Graphics.Color.White
                Case AdminType.Mapper
                    color = SFML.Graphics.Color.Cyan
                    backcolor = SFML.Graphics.Color.Black
                Case AdminType.Developer
                    color = SFML.Graphics.Color.Green
                    backcolor = SFML.Graphics.Color.Black
                Case AdminType.Creator
                    color = SFML.Graphics.Color.Yellow
                    backcolor = SFML.Graphics.Color.Black
            End Select
        Else
            color = SFML.Graphics.Color.Red
        End If

        name = Player(index).Name

        ' calc pos
        textX = ConvertMapX(GetPlayerX(index) * PicX) + Player(index).XOffset + (PicX \ 2) - 2
        textX = textX - (GetTextWidth((name)) / 2)

        If GetPlayerSprite(index) < 0 OrElse GetPlayerSprite(index) > NumCharacters Then
            textY = ConvertMapY(GetPlayerY(index) * PicY) + Player(Myindex).YOffset - 16
        Else
            ' Determine location for text
            textY = ConvertMapY(GetPlayerY(index) * PicY) + Player(index).YOffset - (CharacterGfxInfo(GetPlayerSprite(index)).Height / 4) + 16
        End If

        ' Draw name
        RenderText(name, GameWindow, textX, textY, color, backcolor)
    End Sub

    Sub DrawEquipment()
        Dim i As Integer, itemnum As Integer, itempic As Integer, tmprarity As Byte
        Dim rec As Rectangle, recPos As Rectangle, playersprite As Integer
        Dim tmpSprite2 As Sprite = New Sprite(CharPanelGfx)
        Dim tempRarityColor As SFML.Graphics.Color

        If NumItems = 0 Then Exit Sub

        'first render panel
        RenderTexture(CharPanelSprite, GameWindow, CharWindowX, CharWindowY, 0, 0, CharPanelGfxInfo.Width, CharPanelGfxInfo.Height)

        'lets get player sprite to render
        playersprite = GetPlayerSprite(Myindex)

        With rec
            .Y = 0
            .Height = CharacterGfxInfo(playersprite).Height / 4
            .X = 0
            .Width = CharacterGfxInfo(playersprite).Width / 4
        End With

        RenderTexture(CharacterSprite(playersprite), GameWindow, CharWindowX + CharPanelGfxInfo.Width / 4 - rec.Width / 2, CharWindowY + CharPanelGfxInfo.Height / 2 - rec.Height / 2, rec.X, rec.Y, rec.Width, rec.Height)

        For i = 1 To EquipmentType.Count - 1
            itemnum = GetPlayerEquipment(Myindex, i)

            If itemnum > 0 Then
                StreamItem(itemnum)

                itempic = Item(itemnum).Pic

                If ItemsGfxInfo(itempic).IsLoaded = False Then
                    LoadTexture(itempic, 4)
                End If

                'seeying we still use it, lets update timer
                With ItemsGfxInfo(itempic)
                    .TextureTimer = GetTickCount() + 100000
                End With

                With rec
                    .Y = 0
                    .Height = 32
                    .X = 0
                    .Width = 32
                End With

                With recPos
                    .Y = CharWindowY + EqTop + ((EqOffsetY + 32) * ((i - 1) \ EqColumns))
                    .Height = PicY
                    .X = CharWindowX + EqLeft + 1 + ((EqOffsetX + 32 - 2) * (((i - 1) Mod EqColumns)))
                    .Width = PicX
                End With

                If itempic > 0 Then
                    ItemsSprite(itempic).TextureRect = New IntRect(rec.X, rec.Y, rec.Width, rec.Height)
                    ItemsSprite(itempic).Position = New Vector2f(recPos.X, recPos.Y)
                    GameWindow.Draw(ItemsSprite(itempic))
                End If

                ' set the name
                tmprarity = Item(itemnum).Rarity

                Select Case tmprarity
                    Case 0 ' White
                        tempRarityColor = ItemRarityColor0
                    Case 1 ' green
                        tempRarityColor = ItemRarityColor1
                    Case 2 ' blue
                        tempRarityColor = ItemRarityColor2
                    Case 3 ' maroon
                        tempRarityColor = ItemRarityColor3
                    Case 4 ' purple
                        tempRarityColor = ItemRarityColor4
                    Case 5 'gold
                        tempRarityColor = ItemRarityColor5
                End Select

                Dim rec2 As New RectangleShape With {
                    .OutlineColor = New SFML.Graphics.Color(tempRarityColor),
                    .OutlineThickness = 2,
                    .FillColor = New SFML.Graphics.Color(SFML.Graphics.Color.Transparent),
                    .Size = New Vector2f(30, 30),
                    .Position = New Vector2f(recPos.X, recPos.Y)
                }
                GameWindow.Draw(rec2)
            End If

        Next

        ' Set the character windows
        'name
        RenderText(Language.Character.PName & GetPlayerName(Myindex), GameWindow, CharWindowX + 10, CharWindowY + 14, SFML.Graphics.Color.White, SFML.Graphics.Color.Black)
        'class
        RenderText(Language.Character.JobType & Trim(Job(GetPlayerJob(Myindex)).Name), GameWindow, CharWindowX + 10, CharWindowY + 33, SFML.Graphics.Color.White, SFML.Graphics.Color.Black)
        'level
        RenderText(Language.Character.Level & GetPlayerLevel(Myindex), GameWindow, CharWindowX + 150, CharWindowY + 14, SFML.Graphics.Color.White, SFML.Graphics.Color.Black)
        'points
        RenderText(Language.Character.Points & GetPlayerPoints(Myindex), GameWindow, CharWindowX + 6, CharWindowY + 200, SFML.Graphics.Color.White, SFML.Graphics.Color.Black)

        'Header
        RenderText(Language.Character.StatsLabel, GameWindow, CharWindowX + 250, CharWindowY + 14, SFML.Graphics.Color.White, SFML.Graphics.Color.Black)

        'strength stat
        RenderText(Language.Character.Strength & GetPlayerStat(Myindex, StatType.Strength), GameWindow, CharWindowX + 210, CharWindowY + 30, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, 11)
        'endurance stat
        RenderText(Language.Character.Endurance & GetPlayerStat(Myindex, StatType.Endurance), GameWindow, CharWindowX + 210, CharWindowY + 50, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, 11)
        'vitality stat
        RenderText(Language.Character.Vitality & GetPlayerStat(Myindex, StatType.Vitality), GameWindow, CharWindowX + 210, CharWindowY + 70, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, 11)
        'intelligence stat
        RenderText(Language.Character.Intelligence & GetPlayerStat(Myindex, StatType.Intelligence), GameWindow, CharWindowX + 210, CharWindowY + 90, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, 11)
        'luck stat
        RenderText(Language.Character.Luck & GetPlayerStat(Myindex, StatType.Luck), GameWindow, CharWindowX + 210, CharWindowY + 110, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, 11)
        'spirit stat
        RenderText(Language.Character.Spirit & GetPlayerStat(Myindex, StatType.Spirit), GameWindow, CharWindowX + 210, CharWindowY + 130, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, 11)

        If GetPlayerPoints(Myindex) > 0 Then
            'strength upgrade
            RenderTexture(CharPanelPlusSprite, GameWindow, CharWindowX + StrengthUpgradeX, CharWindowY + StrengthUpgradeY + 4, 0, 0, CharPanelPlusGfxInfo.Width, CharPanelPlusGfxInfo.Height)
            'endurance upgrade
            RenderTexture(CharPanelPlusSprite, GameWindow, CharWindowX + EnduranceUpgradeX, CharWindowY + EnduranceUpgradeY + 4, 0, 0, CharPanelPlusGfxInfo.Width, CharPanelPlusGfxInfo.Height)
            'vitality upgrade
            RenderTexture(CharPanelPlusSprite, GameWindow, CharWindowX + VitalityUpgradeX, CharWindowY + VitalityUpgradeY + 4, 0, 0, CharPanelPlusGfxInfo.Width, CharPanelPlusGfxInfo.Height)
            'intelligence upgrade
            RenderTexture(CharPanelPlusSprite, GameWindow, CharWindowX + IntellectUpgradeX, CharWindowY + IntellectUpgradeY + 4, 0, 0, CharPanelPlusGfxInfo.Width, CharPanelPlusGfxInfo.Height)
            'willpower upgrade
            RenderTexture(CharPanelPlusSprite, GameWindow, CharWindowX + LuckUpgradeX, CharWindowY + LuckUpgradeY + 4, 0, 0, CharPanelPlusGfxInfo.Width, CharPanelPlusGfxInfo.Height)
            'spirit upgrade
            RenderTexture(CharPanelPlusSprite, GameWindow, CharWindowX + SpiritUpgradeX, CharWindowY + SpiritUpgradeY + 4, 0, 0, CharPanelPlusGfxInfo.Width, CharPanelPlusGfxInfo.Height)
        End If

        'gather skills
        'Header
        RenderText(Language.Character.SkillLabel, GameWindow, CharWindowX + 250, CharWindowY + 145, SFML.Graphics.Color.White, SFML.Graphics.Color.Black)
        'herbalist skill
        RenderText(String.Format(GetResourceSkillName(0) & ": " & GetPlayerGatherSkillLvl(Myindex, ResourceType.Herbing)) & " " & Language.Character.Exp & GetPlayerGatherSkillExp(Myindex, ResourceType.Herbing) & "/" & GetPlayerGatherSkillMaxExp(Myindex, ResourceType.Herbing), GameWindow, CharWindowX + 210, CharWindowY + 164, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, 11)
        'woodcutter
        RenderText( String.Format(GetResourceSkillName(1) & ": " & GetPlayerGatherSkillLvl(Myindex, ResourceType.Woodcutting)) & " " & Language.Character.Exp & GetPlayerGatherSkillExp(Myindex, ResourceType.Woodcutting) & "/" & GetPlayerGatherSkillMaxExp(Myindex, ResourceType.Woodcutting), GameWindow, CharWindowX + 210, CharWindowY + 184, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, 11)
        'miner
        RenderText(String.Format(GetResourceSkillName(2) & ": " & GetPlayerGatherSkillLvl(Myindex, ResourceType.Mining)) & " " & Language.Character.Exp & GetPlayerGatherSkillExp(Myindex, ResourceType.Mining) & "/" & GetPlayerGatherSkillMaxExp(Myindex, ResourceType.Mining),  GameWindow, CharWindowX + 210, CharWindowY + 204, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, 11)
        'fisherman
        RenderText(String.Format(GetResourceSkillName(3) & ": " & GetPlayerGatherSkillLvl(Myindex, ResourceType.Fishing)) & " " & Language.Character.Exp & GetPlayerGatherSkillExp(Myindex, ResourceType.Fishing) & "/" & GetPlayerGatherSkillMaxExp(Myindex, ResourceType.Fishing), GameWindow, CharWindowX + 210, CharWindowY + 224, SFML.Graphics.Color.White, SFML.Graphics.Color.Black, 11)
    End Sub
#End Region

#Region "Outgoing Traffic"

#End Region

#Region "Incoming Traffic"
    Sub Packet_PlayerHP(ByRef data() As Byte)
        Dim buffer As New ByteStream(data)

        SetPlayerVital(Myindex, VitalType.HP, buffer.ReadInt32)

        LblHpText = GetPlayerVital(Myindex, VitalType.HP) & "/" & GetPlayerMaxVital(Myindex, VitalType.HP)
        ' hp bar
        PicHpWidth = Int(((GetPlayerVital(Myindex, VitalType.HP) / 169) / (GetPlayerMaxVital(Myindex, VitalType.HP) / 169)) * 169)

        buffer.Dispose()
    End Sub

    Sub Packet_PlayerMP(ByRef data() As Byte)
        Dim buffer As New ByteStream(data)

        SetPlayerVital(Myindex, VitalType.MP, buffer.ReadInt32)

        LblManaText = GetPlayerVital(Myindex, VitalType.MP) & "/" & GetPlayerMaxVital(Myindex, VitalType.MP)
        ' mp bar
        PicManaWidth = Int(((GetPlayerVital(Myindex, VitalType.MP) / 169) / (GetPlayerMaxVital(Myindex, VitalType.MP) / 169)) * 169)

        buffer.Dispose()
    End Sub

    Sub Packet_PlayerSP(ByRef data() As Byte)
        Dim buffer As New ByteStream(data)

        SetPlayerVital(Myindex, VitalType.SP, buffer.ReadInt32)

        buffer.Dispose()
    End Sub

    Sub Packet_PlayerStats(ByRef data() As Byte)
        Dim i As Integer, index As Integer
        Dim buffer As New ByteStream(data)
        index = buffer.ReadInt32
        For i = 1 To StatType.Count - 1
            SetPlayerStat(index, i, buffer.ReadInt32)
        Next
        UpdateCharacterPanel = True

        buffer.Dispose()
    End Sub

    Sub Packet_PlayerData(ByRef data() As Byte)
        Dim i As Integer, x As Integer
        Dim buffer As New ByteStream(data)

        i = buffer.ReadInt32
        SetPlayerName(i, buffer.ReadString)
        SetPlayerJob(i, buffer.ReadInt32)
        SetPlayerLevel(i, buffer.ReadInt32)
        SetPlayerPoints(i, buffer.ReadInt32)
        SetPlayerSprite(i, buffer.ReadInt32)
        SetPlayerMap(i, buffer.ReadInt32)
        SetPlayerX(i, buffer.ReadInt32)
        SetPlayerY(i, buffer.ReadInt32)
        SetPlayerDir(i, buffer.ReadInt32)
        SetPlayerAccess(i, buffer.ReadInt32)
        SetPlayerPk(i, buffer.ReadInt32)

        For x = 1 To StatType.Count - 1
            SetPlayerStat(i, x, buffer.ReadInt32)
        Next

        For x = 1 To ResourceType.Count - 1
            Player(i).GatherSkills(x).SkillLevel = buffer.ReadInt32
            Player(i).GatherSkills(x).SkillCurExp = buffer.ReadInt32
            Player(i).GatherSkills(x).SkillNextLvlExp = buffer.ReadInt32
        Next

        ' Make sure they aren't walking
        Player(i).Moving = 0
        Player(i).XOffset = 0
        Player(i).YOffset = 0

        ' Check if the player is the client player
        If i = Myindex Then
            ' Reset directions
            DirUp = False
            DirDown = False
            DirLeft = False
            DirRight = False

            UpdateCharacterPanel = True

            PlayerData = True
        End If

        buffer.Dispose()
    End Sub

    Sub Packet_PlayerMove(ByRef data() As Byte)
        Dim i As Integer, x As Integer, y As Integer
        Dim dir As Integer, n As Byte
        Dim buffer As New ByteStream(data)
        i = buffer.ReadInt32
        x = buffer.ReadInt32
        y = buffer.ReadInt32
        dir = buffer.ReadInt32
        n = buffer.ReadInt32

        SetPlayerX(i, x)
        SetPlayerY(i, y)
        SetPlayerDir(i, dir)
        Player(i).XOffset = 0
        Player(i).YOffset = 0
        Player(i).Moving = n

        Select Case GetPlayerDir(i)
            Case DirectionType.Up
                Player(i).YOffset = PicY
            Case DirectionType.Down
                Player(i).YOffset = PicY * -1
            Case DirectionType.Left
                Player(i).XOffset = PicX
            Case DirectionType.Right
                Player(i).XOffset = PicX * -1
        End Select

        buffer.Dispose()
    End Sub

    Sub Packet_PlayerDir(ByRef data() As Byte)
        Dim dir As Integer, i As Integer
        Dim buffer As New ByteStream(data)
        i = buffer.ReadInt32
        dir = buffer.ReadInt32

        SetPlayerDir(i, dir)

        With Player(i)
            .XOffset = 0
            .YOffset = 0
            .Moving = 0
        End With

        buffer.Dispose()
    End Sub

    Sub Packet_PlayerExp(ByRef data() As Byte)
        Dim index As Integer, tnl As Integer
        Dim buffer As New ByteStream(data)
        index = buffer.ReadInt32
        SetPlayerExp(index, buffer.ReadInt32)
        tnl = buffer.ReadInt32

        If tnl = 0 Then tnl = 1
        NextlevelExp = tnl

        buffer.Dispose()
    End Sub

    Sub Packet_PlayerXY(ByRef data() As Byte)
        Dim x As Integer, y As Integer, dir As Integer
        Dim buffer As New ByteStream(data)
        x = buffer.ReadInt32
        y = buffer.ReadInt32
        dir = buffer.ReadInt32

        SetPlayerX(Myindex, x)
        SetPlayerY(Myindex, y)
        SetPlayerDir(Myindex, dir)

        ' Make sure they aren't walking
        Player(Myindex).Moving = 0
        Player(Myindex).XOffset = 0
        Player(Myindex).YOffset = 0

        buffer.Dispose()
    End Sub
#End Region

End Module