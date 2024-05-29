Public Module Commands
    Public Function GetPlayerLogin(index As Integer) As String
        GetPlayerLogin = Trim$(Account(index).Login)
    End Function

    Public Sub SetPlayerLogin(index As Integer, login As String)
        Account(index).Login = login.Trim
    End Sub

    Public Function GetPlayerPassword(index As Integer) As String
        GetPlayerPassword = Trim$(Account(index).Password)
    End Function

    Public Sub SetPlayerPassword(index As Integer, password As String)
        Account(index).Password = password.Trim
    End Sub

    Public Function GetPlayerMaxVital(index As Integer, Vital As VitalType) As Integer
        Select Case Vital
            Case VitalType.HP
                GetPlayerMaxVital = 100 + (Player(index).Level + (GetPlayerStat(index, StatType.Vitality) / 2)) * 2

            Case VitalType.SP
                GetPlayerMaxVital = 50 + (Player(index).Level + (GetPlayerStat(index, StatType.Spirit) / 2)) * 2
        End Select
    End Function

    Public Function GetPlayerStat(index As Integer, Stat As StatType) As Integer
        Dim x As Integer, i As Integer

        x = Player(index).Stat(Stat)

        For i = 1 To EquipmentType.Count - 1
            If Player(index).Equipment(i) > 0 Then
                If Item(Player(index).Equipment(i)).Add_Stat(Stat) > 0 Then
                    x += Item(Player(index).Equipment(i)).Add_Stat(Stat)
                End If
            End If
        Next

        GetPlayerStat = x
    End Function

    Public Function GetPlayerAccess(index As Integer) As Integer
        GetPlayerAccess = Player(index).Access
    End Function

    Public Function GetPlayerX(index As Integer) As Integer
        GetPlayerX = Player(index).X
    End Function

    Public Function GetPlayerY(index As Integer) As Integer
        GetPlayerY = Player(index).Y
    End Function

    Public Function GetPlayerDir(index As Integer) As Integer
        GetPlayerDir = Player(index).Dir
    End Function

    Public Function GetPlayerPK(index As Integer) As Integer
        GetPlayerPK = Player(index).Pk
    End Function

    Public Sub SetPlayerVital(index As Integer, Vital As VitalType, Value As Integer)
        Player(index).Vital(Vital) = Value

        If GetPlayerVital(index, Vital) > GetPlayerMaxVital(index, Vital) Then
            Player(index).Vital(Vital) = GetPlayerMaxVital(index, Vital)
        End If

        If GetPlayerVital(index, Vital) < 0 Then
            Player(index).Vital(Vital) = 0
        End If
    End Sub

    Public Function IsDirBlocked(ByRef Blockvar As Byte, ByRef Dir As Byte) As Boolean
        If Dir = DirectionType.UpRight Then
            Return (Blockvar And (2 ^ DirectionType.Up)) Or (Blockvar And (2 ^ DirectionType.Right))
        ElseIf Dir = DirectionType.UpLeft Then
            Return (Blockvar And (2 ^ DirectionType.Up)) Or (Blockvar And (2 ^ DirectionType.Left))
        ElseIf Dir = DirectionType.DownRight Then
            Return (Blockvar And (2 ^ DirectionType.Down)) Or (Blockvar And (2 ^ DirectionType.Right))
        ElseIf Dir = DirectionType.DownLeft Then
            Return (Blockvar And (2 ^ DirectionType.Down)) Or (Blockvar And (2 ^ DirectionType.Left))
        Else
            Return Blockvar And (2 ^ Dir)
        End If
    End Function


    Public Function GetPlayerNextLevel(index As Integer) As Integer
        GetPlayerNextLevel = (50 / 3) * ((GetPlayerLevel(index) + 1) ^ 3 - (6 * (GetPlayerLevel(index) + 1) ^ 2) + 17 * (GetPlayerLevel(index) + 1) - 12)
    End Function

    Public Function GetPlayerExp(index As Integer) As Integer
        GetPlayerExp = Player(index).Exp
    End Function

    Public Function GetPlayerRawStat(index As Integer, Stat As StatType) As Integer
        GetPlayerRawStat = Player(index).Stat(Stat)
    End Function

    Public Sub SetPlayerGatherSkillLvl(index As Integer, SkillSlot As Integer, lvl As Integer)
        Player(index).GatherSkills(SkillSlot).SkillLevel = lvl
    End Sub

    Public Sub SetPlayerGatherSkillExp(index As Integer, SkillSlot As Integer, Exp As Integer)
        Player(index).GatherSkills(SkillSlot).SkillCurExp = Exp
    End Sub

    Public Sub SetPlayerGatherSkillMaxExp(index As Integer, SkillSlot As Integer, MaxExp As Integer)
        Player(index).GatherSkills(SkillSlot).SkillNextLvlExp = MaxExp
    End Sub

    Public Function GetResourceSkillName(skillNum As ResourceType) As String
        Select Case skillNum
            Case ResourceType.Herb
                Return "Herbalism"
            Case ResourceType.Woodcut
                Return "Woodcutting"
            Case ResourceType.Mine
                Return "Mining"
            Case ResourceType.Fish
                Return "Fishing"
        End Select
    End Function

    Public Function GetSkillNextLevel(index As Integer, SkillSlot As Integer) As Integer
        Return (50 / 3) * ((GetPlayerGatherSkillLvl(index, SkillSlot) + 1) ^ 3 - (6 * (GetPlayerGatherSkillLvl(index, SkillSlot) + 1) ^ 2) + 17 * (GetPlayerGatherSkillLvl(index, SkillSlot) + 1) - 12)
    End Function

    Public Function IsPlaying(index As Integer) As Boolean
        ' if the player doesn't exist, the name will equal 0
        If Len(GetPlayerName(index)) > 0 Then
            Return True
        End If

        Return False
    End Function

    Public Function GetPlayerName(index As Integer) As String
        Return Trim$(Player(index).Name)
    End Function

    Public Function GetPlayerGatherSkillLvl(index As Integer, skillSlot As Integer) As Integer
        Return Player(index).GatherSkills(skillSlot).SkillLevel
    End Function

    Public Function GetPlayerGatherSkillExp(index As Integer, skillSlot As Integer) As Integer
        Return Player(index).GatherSkills(skillSlot).SkillCurExp
    End Function

    Public Function GetPlayerGatherSkillMaxExp(index As Integer, skillSlot As Integer) As Integer
        Return Player(index).GatherSkills(skillSlot).SkillNextLvlExp
    End Function

    Public Sub SetPlayerMap(index As Integer, mapNum As Integer)
        Player(index).Map = mapNum
    End Sub

    Public Function GetPlayerInv(index As Integer, invslot As Integer) As Integer
        Return Player(index).Inv(invslot).Num
    End Function

    Public Sub SetPlayerName(index As Integer, name As String)
        Player(index).Name = name
    End Sub

    Public Sub SetPlayerJob(index As Integer, jobNum As Integer)
        Player(index).Job = jobNum
    End Sub

    Public Sub SetPlayerPoints(index As Integer, points As Integer)
        Player(index).Points = points
    End Sub

    Public Sub SetPlayerStat(index As Integer, stat As StatType, value As Integer)
        If value > MAX_POINTS Then value = MAX_POINTS
        Player(index).Stat(stat) = value
    End Sub

    Public Sub SetPlayerInv(index As Integer, invSlot As Integer, itemNum As Integer)
        Player(index).Inv(invSlot).Num = itemNum
    End Sub

    Public Function GetPlayerInvValue(index As Integer, invslot As Integer) As Integer
        Return Player(index).Inv(invslot).Value
    End Function

    Public Sub SetPlayerInvValue(index As Integer, invslot As Integer, itemValue As Integer)
        Player(index).Inv(invslot).Value = itemValue
    End Sub

    Public Function GetPlayerPoints(index As Integer) As Integer
        Return Player(index).Points
    End Function

    Public Sub SetPlayerAccess(index As Integer, access As Integer)
        Player(index).Access = access
    End Sub

    Public Sub SetPlayerPk(index As Integer, pk As Integer)
        Player(index).Pk = pk
    End Sub

    Public Sub SetPlayerX(index As Integer, x As Integer)
        Player(index).X = x
    End Sub

    Public Sub SetPlayerY(index As Integer, y As Integer)
        Player(index).Y = y
    End Sub

    Public Sub SetPlayerSprite(index As Integer, sprite As Integer)
        Player(index).Sprite = sprite
    End Sub

    Public Sub SetPlayerExp(index As Integer, exp As Integer)
        Player(index).Exp = exp
    End Sub

    Public Sub SetPlayerLevel(index As Integer, level As Integer)
        Player(index).Level = level
    End Sub

    Public Sub SetPlayerDir(index As Integer, dir As Integer)
        Player(index).Dir = dir
    End Sub

    Public Function GetPlayerVital(index As Integer, vital As VitalType) As Integer
        Return Player(index).Vital(vital)
    End Function

    Function GetPlayerSprite(index As Integer) As Integer
        Return Player(index).Sprite
    End Function

    Public Function GetPlayerJob(index As Integer) As Integer
        Return Player(index).Job
    End Function

    Public Function GetPlayerMap(index As Integer) As Integer
        Return Player(index).Map
    End Function

    Function GetPlayerLevel(index As Integer) As Integer
        Return Player(index).Level
    End Function

    Public  Function GetPlayerEquipment(index As Integer, equipmentSlot As EquipmentType) As Integer
        Return Player(index).Equipment(equipmentSlot)
    End Function

    Public Sub SetPlayerEquipment(index As Integer, invNum As Integer, equipmentSlot As EquipmentType)
        Player(index).Equipment(equipmentSlot) = invNum
    End Sub

    Public Function IsEditorLocked(index As Integer, id As Integer) As String
        For i = 1 To MAX_PLAYERS
            If IsPlaying(i) Then
                If i <> index Then
                    if TempPlayer(i).Editor = id Then
                        Return GetPlayerName(i)
                        Exit Function
                    End if
                End If
            End If 
        Next

        Return ""
    End Function

    Public Function FindOpenSkill(index As Integer) As Integer
        Dim i As Integer

        For i = 1 To MAX_PLAYER_SKILLS

            If GetPlayerSkill(index, i) = 0 Then
                Return i
                Exit Function
            End If

        Next

        Return 0
    End Function

    Public Function GetPlayerSkill(index As Integer, skillSlot As Integer) As Integer
        Return Player(index).Skill(skillSlot).Num
    End Function

    Public Function GetPlayerSkillCD(index As Integer, skillSlot As Integer) As Integer
        Return Player(index).Skill(skillSlot).CD
    End Function

    Public Sub SetPlayerSkillCD(index As Integer, skillSlot As Integer, Value As Integer)
        Player(index).Skill(skillSlot).CD = Value
    End Sub

    Public Function HasSkill(index As Integer, skillNum As Integer) As Boolean
        Dim i As Integer

        For i = 1 To MAX_PLAYER_SKILLS

            If GetPlayerSkill(index, i) = skillNum Then
                Return True
                Exit Function
            End If

        Next

        Return False
    End Function

    Public Sub SetPlayerSkill(index As Integer, Skillslot As Integer, Skillnum As Integer)
        Player(index).Skill(Skillslot).Num = Skillnum
    End Sub

    Public Function GetBank(index As Integer, bankslot As Byte) As Integer
        GetBank = Bank(index).Item(bankslot).Num
    End Function

    Public Sub SetBank(index As Integer, bankSlot As Byte, itemNum As Integer)
        Bank(index).Item(bankSlot).Num = itemNum
    End Sub

    Public Function GetBankValue(index As Integer, bankSlot As Byte) As Integer
        GetBankValue = Bank(index).Item(bankSlot).Value
    End Function

    Public Sub SetBankValue(index As Integer, bankSlot As Byte, itemValue As Integer)
        Bank(index).Item(bankSlot).Value = itemValue
    End Sub

End Module
