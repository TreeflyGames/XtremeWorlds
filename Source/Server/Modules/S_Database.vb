Imports System
Imports System.Data
Imports System.Globalization
Imports System.IO
Imports System.Numerics
Imports System.Runtime.InteropServices.JavaScript
Imports System.Security.Cryptography
Imports System.Text
Imports System.Text.Json.Nodes
Imports System.Text.RegularExpressions
Imports Core
Imports Mirage.Sharp.Asfw
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports Npgsql
Imports NpgsqlTypes

Module S_Database
    Dim connectionString As String = "Host=localhost;Port=5432;Username=postgres;Password=mirage;Database=mirage"

    Public Sub ExecuteSql(connectionString As String, sql As String)
        Using connection As New NpgsqlConnection(connectionString)
            connection.Open()

            Using command As New NpgsqlCommand(sql, connection)
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub
    
    Function DatabaseExists(databaseName As String) As Boolean
        Dim sql As String = "SELECT 1 FROM pg_database WHERE datname = @databaseName;"

        Using connection As New NpgsqlConnection(connectionString)
            connection.Open()
            Using command As New NpgsqlCommand(sql, connection)
                command.Parameters.AddWithValue("@databaseName", databaseName)

                Using reader As NpgsqlDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        Return True
                    Else
                        Return False
                    End If
                End Using
            End Using
        End Using
    End Function

    Public Sub CreateDatabase(databaseName As String)
        If Not DatabaseExists("mirage") Then
            Dim sql As String = $"CREATE DATABASE {databaseName};"

            ' Connect to the "postgres" maintenance database
            Dim builder As New NpgsqlConnectionStringBuilder(connectionString)
            builder.Database = "postgres"

            Dim maintenanceConnectionString As String = builder.ConnectionString

            ExecuteSql(maintenanceConnectionString, sql)
        End If
    End Sub

    Public Function RowExistsByColumn(columnName As String, value As BigInteger, tableName As String) As Boolean
        Dim sql As String = $"SELECT EXISTS (SELECT 1 FROM {tableName} WHERE {columnName} = @value);"

        Using connection As New NpgsqlConnection(connectionString)
            connection.Open()

            Using command As New NpgsqlCommand(sql, connection)
                command.Parameters.AddWithValue("@value", value)

                Dim exists As Boolean = command.ExecuteScalar()
                Return exists
            End Using
        End Using
    End Function

    Sub UpdateRow(id As BigInteger, data As String, table As String, columnName As String)
        Dim sqlCheck As String = $"SELECT column_name FROM information_schema.columns WHERE table_name='{table}' AND column_name='{columnName}';"

        Using connection As New NpgsqlConnection(connectionString)
            connection.Open()

            ' Check if column exists
            Using commandCheck As New NpgsqlCommand(sqlCheck, connection)
                Dim result As Object = commandCheck.ExecuteScalar()

                ' If column exists, then proceed with update
                If result IsNot Nothing Then
                    Dim sqlUpdate As String = $"UPDATE {table} SET {columnName} = @data WHERE id = @id;"

                    Using commandUpdate As New NpgsqlCommand(sqlUpdate, connection)
                        Dim jsonString As String = data.ToString()
                        commandUpdate.Parameters.AddWithValue("@data", NpgsqlDbType.Jsonb, jsonString)
                        commandUpdate.Parameters.AddWithValue("@id", id)

                        commandUpdate.ExecuteNonQuery()
                    End Using
                Else
                    Console.WriteLine($"Column '{columnName}' does not exist in table {table}.")
                End If
            End Using
        End Using
    End Sub

    Public Function StringToHex(input As String) As String
        Dim byteArray() As Byte = Encoding.UTF8.GetBytes(input)
        Dim hex As New StringBuilder(byteArray.Length * 2)

        For Each b As Byte In byteArray
            hex.AppendFormat("{0:x2}", b)
        Next

        Return hex.ToString()
    End Function

    Public Function HexToNumber(hexString As String) As BigInteger
        If String.IsNullOrWhiteSpace(hexString) OrElse Not Regex.IsMatch(hexString, "^[0-9a-fA-F]+$") Then
            Return 0
        End If

        Return BigInteger.Parse(hexString)
    End Function

    Public Function GenerateIdFromString(input As String) As BigInteger
        Return HexToNumber(StringToHex(input))
    End Function

    Public Sub UpdateRowByColumn(columnName As String, value As BigInteger, targetColumn As String, newValue As String, tableName As String)
        Dim sql As String = $"UPDATE {tableName} SET {targetColumn} = @newValue::jsonb WHERE {columnName} = @value;"

        Dim jsonData As JObject = JObject.Parse(newValue) ' Parse the string into a JObject

        Using connection As New NpgsqlConnection(connectionString)
            connection.Open()

            Using command As New NpgsqlCommand(sql, connection)
                command.Parameters.AddWithValue("@value", value)
                command.Parameters.AddWithValue("@newValue", jsonData.ToString()) ' Convert JObject back to string

                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub


    Public Sub CreateTables()
        Dim dataTable As String = "id SERIAL PRIMARY KEY, data jsonb"
        Dim playerTable As String = "id BIGINT PRIMARY KEY, data jsonb, bank jsonb"
        
        For i = 1 To MAX_CHARACTERS
            playerTable += $", character{i} jsonb"
        Next
        
        Dim tableNames As String() = {"job", "item", "map", "npc", "shop", "skill", "resource", "animation", "pet", "projectile"}

        For Each tableName In tableNames
            CreateTable(tableName, dataTable)
        Next

        CreateTable("account", playerTable)
    End Sub

    Public Sub CreateTable(tableName As String, layout As String)
        Using conn As New NpgsqlConnection(connectionString)
            conn.Open()

            Using cmd As New NpgsqlCommand()
                cmd.Connection = conn
                cmd.CommandText = $"CREATE TABLE IF NOT EXISTS {tableName} ({layout});"
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Async Function GetData(tableName As String) As Task(Of List(Of BigInteger))
        Dim ids As New List(Of BigInteger)

        Using conn As New NpgsqlConnection(connectionString)
            Await conn.OpenAsync()

            ' Define a query
            Dim cmd As New NpgsqlCommand($"SELECT id FROM {tableName}", conn)

            ' Execute a query
            Using reader As NpgsqlDataReader = Await cmd.ExecuteReaderAsync()
                ' Read all rows and output the first column in each row
                While Await reader.ReadAsync()
                    Dim id = Await reader.GetFieldValueAsync(Of Int32)(0) ' Get the value as a long
                    ids.Add(new BigInteger(id)) ' Convert the long to a BigInteger and add it to the list
                End While
            End Using
        End Using

        Return ids
    End Function

    Public Function RowExists(id As BigInteger, table As String) As Boolean
        Dim sql As String = $"SELECT EXISTS (SELECT 1 FROM {table} WHERE id = @id);"

        Using connection As New NpgsqlConnection(connectionString)
            connection.Open()

            Using command As New NpgsqlCommand(sql, connection)
                command.Parameters.AddWithValue("@id", id)

                Using reader As NpgsqlDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        Return reader.GetBoolean(0)
                    Else
                        Return False
                    End If
                End Using
            End Using
        End Using
    End Function

    Public Sub InsertRow(id As BigInteger, data As String, tableName As String)
        Dim jsonData As JObject = JObject.Parse(data) ' Parse the string into a JObject

        Using conn As New NpgsqlConnection(connectionString)
            conn.Open()

            Using cmd As New NpgsqlCommand()
                cmd.Connection = conn
                cmd.CommandText = $"INSERT INTO {tableName} (id, data) VALUES (@id, @data::jsonb);"
                cmd.Parameters.AddWithValue("@id", id)
                cmd.Parameters.AddWithValue("@data", jsonData.ToString()) ' Convert JObject back to string

                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub InsertRow(id As BigInteger, data As String, tableName As String, columnName As String)
        Dim jsonData As JObject = JObject.Parse(data) ' Parse the string into a JObject

        Using conn As New NpgsqlConnection(connectionString)
            conn.Open()

            Using cmd As New NpgsqlCommand()
                cmd.Connection = conn
                cmd.CommandText = $"INSERT INTO {tableName} (id, data) VALUES (@id, @data::jsonb);"
                cmd.Parameters.AddWithValue("@id", id)
                cmd.Parameters.AddWithValue("@" + columnName, jsonData.ToString()) ' Convert JObject back to string

                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub InsertRowByColumn(id As BigInteger, data As String, tableName As String, dataColumn As String, idColumn As String)
        Dim jsonData As JObject = JObject.Parse(data) ' Parse the string into a JObject

        Dim sql As String = $"INSERT INTO {tableName} ({idColumn}, {dataColumn}) VALUES (@id, @data::jsonb);"

        Using connection As New NpgsqlConnection(connectionString)
            connection.Open()

            Using command As New NpgsqlCommand(sql, connection)
                command.Parameters.AddWithValue("@id", id)
                command.Parameters.AddWithValue("@data", jsonData.ToString()) ' Convert JObject back to string

                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Function SelectRow(id As BigInteger, tableName As String, columnName As String) As JObject
        Dim sql As String = $"SELECT {columnName} FROM {tableName} WHERE id = @id;"

        Using connection As New NpgsqlConnection(connectionString)
            connection.Open()

            Using command As New NpgsqlCommand(sql, connection)
                command.Parameters.AddWithValue("@id", id)

                Using reader As NpgsqlDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        Dim jsonbData As String = reader.GetString(0)
                        Dim jsonObject As JObject = JObject.Parse(jsonbData)
                        Return jsonObject
                    Else
                        Return Nothing
                    End If
                End Using
            End Using
        End Using
    End Function

    Public Function SelectRowByColumn(columnName As String, value As BigInteger, tableName As String, dataColumn As String) As JObject
        Dim sql As String = $"SELECT {dataColumn} FROM {tableName} WHERE {columnName} = @value;"

        Using connection As New NpgsqlConnection(connectionString)
            connection.Open()

            Using command As New NpgsqlCommand(sql, connection)
                command.Parameters.AddWithValue("@value", value)

                Using reader As NpgsqlDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        ' Check if the first column is not null
                        If Not reader.IsDBNull(0) Then
                            Dim jsonbData As String = reader.GetString(0)
                            Dim jsonObject As JObject = JObject.Parse(jsonbData)
                            Return jsonObject
                        Else
                            ' Handle null value or return null JObject...
                            Return Nothing
                        End If
                    Else
                        Return Nothing
                    End If
                End Using
            End Using
        End Using
    End Function

#Region "Job"

    Sub ClearJobs()
        Dim i As Integer

        ReDim Job(MAX_JOBS)
        For i = 1 To MAX_JOBS
            ReDim Job(i).Stat(Core.StatType.Count - 1)
            ReDim Job(i).StartItem(5)
            ReDim Job(i).StartValue(5)
        Next

        For i = 1 To MAX_JOBS
            ClearJob(i)
        Next
    End Sub

    Sub ClearJob(jobNum As Integer)
        Job(jobNum).Name = ""
        Job(jobNum).Desc = ""
        Job(jobNum).StartMap = 1
        ReDim Job(jobNum).Stat(StatType.Count - 1)
        ReDim Job(jobNum).StartItem(5)
        ReDim Job(jobNum).StartValue(5)
        Job(jobNum).MaleSprite = 1
        Job(jobNum).FemaleSprite = 1
    End Sub

    Sub LoadJob(jobNum As Integer)
        Dim data As JObject
            
        data = SelectRow(jobNum, "job", "data")

        If data Is Nothing Then
            ClearJob(jobNum)
            Exit Sub
        End If

        Dim jobData = JObject.FromObject(data).toObject(Of JobStruct)()
        Job(jobNum) = jobData
    End Sub

    Sub LoadJobs()
        Dim i As Integer

        For i = 1 To MAX_JOBS
            LoadJob(i)
        Next
    End Sub

    Sub SaveJob(jobNum As Integer)
        Dim json As String = JsonConvert.SerializeObject(Job(jobNum)).ToString()

        If RowExists(jobNum, "job")
            UpdateRow(jobNum, json, "job", "data")
        Else
            InsertRow(jobNum, "data", "job")
        End If
    End Sub

    Sub SaveJobs()
        Dim i As Integer

        For i = 1 To MAX_JOBS
            SaveJob(i)
        Next
    End Sub
#End Region

#Region "Maps"

    Sub ClearMaps()
        ReDim Map(MAX_CACHED_MAPS)
        ReDim MapNPC(MAX_CACHED_MAPS)

        For i = 1 To MAX_CACHED_MAPS
            ReDim MapNPC(i).Npc(MAX_MAP_NPCS)
            ReDim Map(i).Npc(MAX_MAP_NPCS)
        Next

        ReDim Switches(MAX_SWITCHES)
        ReDim Variables(NAX_VARIABLES)
        ReDim TempEventMap(MAX_CACHED_MAPS)

        For i = 1 To MAX_CACHED_MAPS
            ClearMap(i)
        Next
    End Sub

    Sub ClearMap(mapNum As Integer)
        Dim x As Integer
        Dim y As Integer

        Map(mapNum).Tileset = 1
        Map(mapNum).Name = ""
        Map(mapNum).MaxX = MAX_MAPX
        Map(mapNum).MaxY = MAX_MAPY
        ReDim Map(mapNum).Npc(MAX_MAP_NPCS)
        ReDim Map(mapNum).Tile(Map(mapNum).MaxX, Map(mapNum).MaxY)

        For x = 0 To MAX_MAPX
            For y = 0 To MAX_MAPY
                ReDim Map(mapNum).Tile(x, y).Layer(LayerType.Count - 1)
            Next
        Next

        Map(mapNum).EventCount = 0
        ReDim Map(mapNum).Events(0)

        ' Reset the values for if a player is on the map or not
        PlayersOnMap(mapNum) = False
        Map(mapNum).Tileset = 1
        Map(mapNum).Name = ""
        Map(mapNum).Music = ""
        Map(mapNum).MaxX = MAX_MAPX
        Map(mapNum).MaxY = MAX_MAPY
    End Sub

    Public Sub SaveMap(mapNum As Integer)
        Dim json As String = JsonConvert.SerializeObject(Map(mapNum)).ToString()

        If RowExists(mapNum, "map")
            UpdateRow(mapNum, json, "map", "data")
        Else
            InsertRow(mapNum,  json, "map")
        End If
    End Sub

    Sub LoadMaps()
        Dim i As Integer

        For i = 1 To MAX_MAPS
            LoadMap(i)
        Next
    End Sub

    Sub LoadMap(mapNum As Integer)
        
        Dim data As JObject

        data = SelectRow(mapNum, "map", "data")

        if data Is Nothing then
            ClearMap(mapNum)
            Exit Sub
        End If

        Dim mapData = JObject.FromObject(data).toObject(Of MapStruct)()
        Map(mapNum) = mapData

        CacheResources(mapNum)
    End Sub

    Sub ClearMapItem(index As Integer, mapNum As Integer)
        MapItem(mapNum, index).PlayerName = ""
    End Sub

    Sub ClearMapItems()
        Dim x As Integer
        Dim y As Integer

        For y = 1 To MAX_CACHED_MAPS
            For x = 1 To MAX_MAP_ITEMS
                ClearMapItem(x, y)
            Next
        Next

    End Sub

#End Region

#Region "Npc's"

    Sub SaveNpc(npcNum As Integer)
        Dim json As String = JsonConvert.SerializeObject(NPC(npcNum)).ToString()

        If RowExists(npcNum, "npc")
            UpdateRow(npcNum, json, "npc", "data")
        Else
            InsertRow(npcNum, json, "npc")
        End If
    End Sub

    Sub LoadNpcs()
        Dim i As Integer

        For i = 1 To MAX_NPCS
            LoadNpc(i)
        Next
    End Sub

    Sub LoadNpc(npcNum As Integer)
        Dim data As JObject

        data = SelectRow(npcNum, "npc", "data")

        If data Is Nothing Then
            ClearNpc(npcNum)
            Exit Sub
        End If

        Dim npcData = JObject.FromObject(data).toObject(Of NpcStruct)()
        NPC(npcNum) = npcData
    End Sub

    Sub ClearMapNpc(index As Integer, mapNum As Integer)
        MapNPC(mapNum).Npc(index).Num = 0
        ReDim MapNPC(mapNum).Npc(index).Vital(VitalType.Count - 1)
        ReDim MapNPC(mapNum).Npc(index).SkillCd(MAX_NPC_SKILLS)
    End Sub
    
    Sub ClearAllMapNpcs()
        Dim i As Integer

        For i = 1 To MAX_CACHED_MAPS
            ClearMapNpcs(i)
        Next

    End Sub

    Sub ClearMapNpcs(mapNum As Integer)
        Dim x As Integer

        For x = 1 To MAX_MAP_NPCS
            ClearMapNpc(x, mapNum)
        Next

    End Sub

    Sub ClearNpc(index As Integer)
        NPC(index).Name = ""
        NPC(index).AttackSay = ""

        ReDim NPC(index).Stat(StatType.Count - 1)
        
        For i = 1 To MAX_DROP_ITEMS
            ReDim NPC(index).DropChance(5)
            ReDim NPC(index).DropItem(5)
            ReDim NPC(index).DropItemValue(5)
            ReDim NPC(index).Skill(MAX_NPC_SKILLS)
        Next
    End Sub

    Sub ClearNpcs()
        For i = 1 To MAX_NPCS
            ClearNpc(i)
        Next

    End Sub

#End Region

#Region "Shops"

    Sub SaveShop(shopNum As Integer)
        Dim json As String = JsonConvert.SerializeObject(Shop(shopNum)).ToString()

        If RowExists(shopNum, "shop")
            UpdateRow(shopNum, json, "shop", "data")
        Else
            InsertRow(shopNum, json, "shop")
        End If
    End Sub

    Sub LoadShops()
        Dim i As Integer

        For i = 1 To MAX_SHOPS
            LoadShop(i)
        Next

    End Sub

    Sub LoadShop(shopNum As Integer)
        Dim data As JObject

        data = SelectRow(shopNum, "shop", "data")

        If data Is Nothing Then
            ClearShop(shopNum)
            Exit Sub
        End If

        Dim shopData = JObject.FromObject(data).toObject(Of ShopStruct)()
        Shop(shopNum) = shopData
    End Sub

    Sub ClearShop(index As Integer)
        Dim i As Integer

        Shop(index).Name = ""

        ReDim Shop(index).TradeItem(MAX_TRADES)
        For i = 1 To MAX_SHOPS
            ReDim Shop(i).TradeItem(MAX_TRADES)
        Next

    End Sub

    Sub ClearShops()
        ReDim Shop(MAX_SHOPS).TradeItem(MAX_TRADES)

        For i = 1 To MAX_SHOPS
            Call ClearShop(i)
        Next
    End Sub

#End Region

#Region "Skills"

    Sub SaveSkill(skillNum As Integer)
        Dim json As String = JsonConvert.SerializeObject(Skill(skillNum)).ToString()

        If RowExists(skillNum, "skill")
            UpdateRow(skillNum, json, "skill", "data")
        Else
            InsertRow(skillNum, json, "skill")
        End If
    End Sub

    Sub LoadSkills()
        Dim i As Integer

        For i = 1 To MAX_SKILLS
            LoadSkill(i)
        Next

    End Sub

    Sub LoadSkill(skillNum As Integer)
        Dim data As JObject

        data = SelectRow(skillNum, "skill", "data")

        If data Is Nothing Then
            ClearSkill(skillNum)
            Exit Sub
        End If

        Dim skillData = JObject.FromObject(data).toObject(Of SkillStruct)()
        Skill(skillNum) = skillData
    End Sub

    Sub ClearSkill(index As Integer)
        Skill(index).Name = ""
        Skill(index).LevelReq = 1
    End Sub

    Sub ClearSkills()
        Dim i As Integer

        For i = 1 To MAX_SKILLS
            ClearSkill(i)
        Next

    End Sub

#End Region

#Region "Players"

    Sub SaveAllPlayersOnline()
        For i = 1 To GetPlayersOnline()
            If Not IsPlaying(i) Then Continue For
            SavePlayer(i)
        Next
    End Sub

    Sub SavePlayer(index As Integer)
        Dim json As String = JsonConvert.SerializeObject(Account(index)).ToString()
        Dim username As String = GetPlayerLogin(index)
        Dim id As BigInteger = GenerateIdFromString(username)

        If RowExists(id, "account")
            UpdateRowByColumn("id", id, "data", json, "account")
        Else
            InsertRowByColumn(id, json, "account", "data", "id")
        End If

        SaveCharacter(index, Account(index).Index)
        SaveBank(index)
    End Sub

    Sub LoadAccount(index As Integer, username As String)
        Dim data As JObject
        SetPlayerLogin(index, username)

        data = SelectRowByColumn("id", GenerateIdFromString(username), "account", "data")

        If data Is Nothing Then
            ClearAccount(index)
            SetPlayerLogin(index, username)
            Exit Sub
        End If

        Dim accountData = JObject.FromObject(data).toObject(Of AccountStruct)()
        Account(index) = accountData

        LoadBank(index)
    End Sub

    Sub ClearAccount(index As Integer)
        Player(index).Access = AdminType.Player
        SetPlayerLogin(index, "")
        ReDim Account(index).Character(MAX_CHARACTERS)

        For i = 1 To MAX_CHARACTERS
            SetPlayerCharName(index, i, "")
        Next
    End Sub

    Sub ClearPlayer(index As Integer)
        ClearAccount(index)

        ReDim TempPlayer(MAX_PLAYERS)

        For i = 1 To MAX_PLAYERS
            ReDim TempPlayer(i).SkillCd(MAX_PLAYER_SKILLS)
            ReDim TempPlayer(i).PetSkillCd(4)
            ReDim TempPlayer(i).TradeOffer(MAX_INV)
        Next

        ReDim TempPlayer(index).SkillCd(MAX_PLAYER_SKILLS)
        ReDim TempPlayer(index).PetSkillCd(4)
        TempPlayer(index).Editor = -1

        ClearBank(index)
        ClearCharacter(index)
    End Sub

#End Region

#Region "Bank"
    Friend Sub LoadBank(index As Integer)
        Dim data As JObject
        data = SelectRowByColumn("id", GenerateIdFromString(GetPlayerLogin(index)), "account", "bank")

        If data Is Nothing Then
            ClearBank(index)
            Exit Sub
        End If

        Dim bankData = JObject.FromObject(data).toObject(Of BankStruct)()
        Bank(index) = bankData
    End Sub

    Sub SaveBank(index As Integer)
        Dim json As String = JsonConvert.SerializeObject(Bank(index)).ToString()
        Dim username As String = GetPlayerLogin(index)
        Dim id As BigInteger = GenerateIdFromString(username)

        If RowExistsByColumn("id", id, "account")
            UpdateRowByColumn("id", id, "bank", json, "account")
        Else
            InsertRowByColumn(id, json, "account", "bank", "id")
        End If
    End Sub

    Sub ClearBank(index As Integer)
        ReDim Bank(index).Item(MAX_BANK)

        For i = 1 To MAX_BANK
            Bank(index).Item(i).Num = 0
            Bank(index).Item(i).Value = 0
        Next
    End Sub

#End Region

#Region "Characters"
    Sub ClearCharacter(index As Integer)
        Player(index).Name = ""
        Player(index).Job = 0
        Player(index).Dir = 0

        ReDim Player(index).Equipment(EquipmentType.Count - 1)
        For i = 1 To EquipmentType.Count - 1
            Player(index).Equipment(i) = 0
        Next

        ReDim Player(index).Inv(MAX_INV)
        For i = 1 To MAX_INV
            Player(index).Inv(i).Num = 0
            Player(index).Inv(i).Value = 0
        Next

        Player(index).Exp = 0
        Player(index).Level = 0
        Player(index).Map = 1
        Player(index).Name = ""
        Player(index).Pk = 0
        Player(index).Points = 0
        Player(index).Sex = 0

        ReDim Player(index).Skill(MAX_PLAYER_SKILLS)
        For i = 1 To MAX_PLAYER_SKILLS
            Player(index).Skill(i).Num = 0
            Player(index).Skill(i).CD = 0
        Next

        Player(index).Sprite = 0

        ReDim Player(index).Stat(StatType.Count - 1)
        For i = 1 To StatType.Count - 1
            Player(index).Stat(i) = 0
        Next

        ReDim Player(index).Vital(VitalType.Count - 1)
        For i = 1 To VitalType.Count - 1
            Player(index).Vital(i) = 0
        Next

        Player(index).X = 0
        Player(index).Y = 0

        ReDim Player(index).Hotbar(MAX_HOTBAR)
        For i = 1 To MAX_HOTBAR
            Player(index).Hotbar(i).Slot = 0
            Player(index).Hotbar(i).SlotType = 0
        Next

        ReDim Player(index).Switches(MAX_SWITCHES)
        For i = 1 To MAX_SWITCHES
            Player(index).Switches(i) = 0
        Next
        ReDim Player(index).Variables(NAX_VARIABLES)
        For i = 1 To NAX_VARIABLES
            Player(index).Variables(i) = 0
        Next

        ReDim Player(index).GatherSkills(ResourceType.Count - 1)
        For i = 1 To ResourceType.Count - 1
            Player(index).GatherSkills(i).SkillLevel = 1
            Player(index).GatherSkills(i).SkillCurExp = 0
            SetPlayerGatherSkillMaxExp(index, i, GetSkillNextLevel(index, i))
        Next

        For i = 1 To EquipmentType.Count - 1
            Player(index).Equipment(i) = 0
        Next

        Player(index).Pet.Num = 0
        Player(index).Pet.Health = 0
        Player(index).Pet.Mana = 0
        Player(index).Pet.Level = 0

        ReDim Player(index).Pet.Stat(StatType.Count - 1)

        For i = 1 To StatType.Count - 1
            Player(index).Pet.Stat(i) = 0
        Next

        ReDim Player(index).Pet.Skill(4)
        For i = 1 To 4
            Player(index).Pet.Skill(i) = 0
        Next

        Player(index).Pet.X = 0
        Player(index).Pet.Y = 0
        Player(index).Pet.Dir = 0
        Player(index).Pet.Alive = 0
        Player(index).Pet.AttackBehaviour = 0
        Player(index).Pet.AdoptiveStats = 0
        Player(index).Pet.Points = 0
        Player(index).Pet.Exp = 0
    End Sub

    Sub LoadCharacter(index As Integer, charNum As Integer)
        Dim data As JObject

        data = SelectRowByColumn("id", GenerateIdFromString(GetPlayerLogin(index)), "account", "character" & charNum.ToString())

        If data Is Nothing Then
            ClearCharacter(index)
            Exit Sub
        End If

        Dim characterData = JObject.FromObject(data).toObject(Of PlayerStruct)()
        Player(index) = characterData
    End Sub

    Sub SaveCharacter(index As Integer, charNum As Integer)
        Dim json As String = JsonConvert.SerializeObject(Player(index)).ToString()
        Dim id As BigInteger = GenerateIdFromString(GetPlayerLogin(index))

        If RowExistsByColumn("id", id, "account")
            UpdateRowByColumn("id", id, "character" & charNum.ToString(), json, "account")
        Else
            InsertRowByColumn(id, json, "account", "character" & charNum.ToString(), "id")
        End If
    End Sub

    Function CharExist(index As Integer, charnUm As Integer) As Boolean
        Return Account(index).Character(charnUm).Trim.Length > 0
    End Function

    Sub AddChar(index As Integer, charNum As Integer, name As String, Sex As Byte, jobNum As Byte, sprite As Integer)
        Dim n As Integer, i As Integer

        If Len(Trim$(Player(index).Name)) = 0 Then
            Player(index).Name = name
            Player(index).Sex = Sex
            Player(index).Job = jobNum
            Player(index).Sprite = sprite

            Player(index).Level = 1

            For n = 1 To StatType.Count - 1
                Player(index).Stat(n) = Job(jobNum).Stat(n)
            Next

            Player(index).Dir = DirectionType.Down
            Player(index).Map = Job(jobNum).StartMap
            Player(index).X = Job(jobNum).StartX
            Player(index).Y = Job(jobNum).StartY
            Player(index).Dir = DirectionType.Down

            For i = 1 To VitalType.Count - 1
                Call SetPlayerVital(index, i, GetPlayerMaxVital(index, i))
            Next

            ' set starter equipment
            For n = 1 To 5
                If Job(jobNum).StartItem(n) > 0 Then
                    Player(index).Inv(n).Num = Job(jobNum).StartItem(n)
                    Player(index).Inv(n).Value = Job(jobNum).StartValue(n)
                End If
            Next

            ' set skills
            For i = 1 To ResourceType.Count - 1
                Player(index).GatherSkills(i).SkillLevel = 1
                Player(index).GatherSkills(i).SkillCurExp = 0
                SetPlayerGatherSkillMaxExp(index, i, GetSkillNextLevel(index, i))
            Next

            ' Add name to character list.
            CharactersList.Add(name)

            Account(index).Index = charNum
            SetPlayerCharName(index, Account(index).Index, name)
            SavePlayer(index)
        End If
        
    End Sub

#End Region

#Region "Logs"

    Friend Function GetFileContents(fullPath As String) As String
        Dim strContents = ""
        Dim objReader As StreamReader
        If Not File.Exists(fullPath) Then File.Create(fullPath).Dispose()
        Try
            objReader = New StreamReader(fullPath)
            strContents = objReader.ReadToEnd()
            objReader.Close()
        Catch
        End Try
        Return strContents
    End Function

    Friend Function Addlog(strData As String, FN As String) As Boolean
        Dim fullpath As String
        Dim contents As String
        Dim bAns = False
        Dim objReader As StreamWriter
        fullpath = Paths.Logs & FN
        contents = GetFileContents(fullpath)
        contents = contents & vbNewLine & strData
        Try
            objReader = New StreamWriter(fullpath)
            objReader.Write(contents)
            objReader.Close()
            bAns = True
        Catch
        End Try
        Return bAns
    End Function

    Friend Function AddTextToFile(strData As String, fn As String) As Boolean
        Dim fullpath As String
        Dim contents As String
        Dim bAns = False
        Dim objReader As StreamWriter
        fullpath = Paths.Database & fn
        contents = GetFileContents(fullpath)
        contents = contents & vbNewLine & strData
        Try
            objReader = New StreamWriter(fullpath)
            objReader.Write(contents)
            objReader.Close()
            bAns = True
        Catch
        End Try
        Return bAns
    End Function

#End Region

#Region "Banning"

    Sub ServerBanIndex(BanPlayerindex As Integer)
        Dim filename As String
        Dim IP As String
        Dim F As Integer
        Dim i As Integer
        filename = Paths.Database & "banlist.txt"

        ' Make sure the file exists
        If Not File.Exists("data\banlist.txt") Then
            F = FreeFile()
            'COME HERE!!!
        End If

        ' Cut off last portion of ip
        IP = Socket.ClientIp(BanPlayerindex)

        For i = Len(IP) To 1 Step -1

            If Mid$(IP, i, 1) = "." Then
                Exit For
            End If

        Next

        IP = Mid$(IP, 1, i)
        AddTextToFile(IP, "banlist.txt")
        GlobalMsg(GetPlayerName(BanPlayerindex) & " has been banned from " & Settings.Data.GameName & " by " & "the Server" & "!")
        Addlog("The Server" & " has banned " & GetPlayerName(BanPlayerindex) & ".", ADMIN_LOG)
        AlertMsg(BanPlayerindex, "You have been banned by " & "The Server" & "!")
    End Sub

    Function IsBanned(IP As String) As Boolean
        Dim filename As String, line As String

        filename = Paths.Database & "banlist.txt"

        ' Check if file exists
        If Not File.Exists("data\banlist.txt") Then
            Return False
        End If

        Dim sr As StreamReader = New StreamReader(filename)

        Do While sr.Peek() >= 0
            'Console.WriteLine(sr.ReadLine())
            ' Is banned?
            line = sr.ReadLine()
            If Trim$(LCase$(line)) = Trim$(LCase$(Mid$(IP, 1, Len(line)))) Then
                IsBanned = True
            End If
        Loop
        sr.Close()

    End Function

    Sub BanIndex(BanPlayerindex As Integer, BannedByindex As Integer)
        Dim filename As String = Paths.Database & "banlist.txt"
        Dim IP As String, i As Integer

        ' Make sure the file exists
        If Not File.Exists(filename) Then File.Create(filename).Dispose()

        ' Cut off last portion of ip
        IP = Socket.ClientIp(BanPlayerindex)

        For i = Len(IP) To 1 Step -1

            If Mid$(IP, i, 1) = "." Then
                Exit For
            End If

        Next

        IP = Mid$(IP, 1, i)
        AddTextToFile(IP, "banlist.txt")
        GlobalMsg(GetPlayerName(BanPlayerindex) & " has been banned from " & Settings.Data.GameName & " by " & GetPlayerName(BannedByindex) & "!")
        Addlog(GetPlayerName(BannedByindex) & " has banned " & GetPlayerName(BanPlayerindex) & ".", ADMIN_LOG)
        AlertMsg(BanPlayerindex, "You have been banned by " & GetPlayerName(BannedByindex) & "!")
    End Sub

#End Region

#Region "Data Functions"
    Function JobData(jobNum As Integer) As Byte()
        Dim n As Integer, q As Integer
        Dim buffer As New ByteStream(4)

        buffer.WriteString((Job(jobNum).Name))
        buffer.WriteString((Job(jobNum).Desc))

        buffer.WriteInt32(Job(jobNum).MaleSprite)
        buffer.WriteInt32(Job(jobNum).FemaleSprite)

        For i = 1 To StatType.Count - 1
            buffer.WriteInt32(Job(jobNum).Stat(i))
        Next

        For q = 1 To 5
            buffer.WriteInt32(Job(jobNum).StartItem(q))
            buffer.WriteInt32(Job(jobNum).StartValue(q))
        Next

        buffer.WriteInt32(Job(jobNum).StartMap)
        buffer.WriteByte(Job(jobNum).StartX)
        buffer.WriteByte(Job(jobNum).StartY)
        buffer.WriteInt32(Job(jobNum).BaseExp)

        Return buffer.ToArray()
    End Function

    Function NpcsData() As Byte()
        Dim buffer As New ByteStream(4)

        For i = 1 To MAX_NPCS
            If Not Len(Trim$(NPC(i).Name)) > 0 Then Continue For
            buffer.WriteBlock(NpcData(i))
        Next
        Return buffer.ToArray
    End Function

    Function NpcData(NpcNum As Integer) As Byte()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(NpcNum)
        buffer.WriteInt32(NPC(NpcNum).Animation)
        buffer.WriteString(NPC(NpcNum).AttackSay)
        buffer.WriteByte(NPC(NpcNum).Behaviour)
        For i = 1 To MAX_DROP_ITEMS
            buffer.WriteInt32(NPC(NpcNum).DropChance(i))
            buffer.WriteInt32(NPC(NpcNum).DropItem(i))
            buffer.WriteInt32(NPC(NpcNum).DropItemValue(i))
        Next
        buffer.WriteInt32(NPC(NpcNum).Exp)
        buffer.WriteByte(NPC(NpcNum).Faction)
        buffer.WriteInt32(NPC(NpcNum).HP)
        buffer.WriteString((NPC(NpcNum).Name))
        buffer.WriteByte(NPC(NpcNum).Range)
        buffer.WriteByte(NPC(NpcNum).SpawnTime)
        buffer.WriteInt32(NPC(NpcNum).SpawnSecs)
        buffer.WriteInt32(NPC(NpcNum).Sprite)
        For i = 1 To StatType.Count - 1
            buffer.WriteByte(NPC(NpcNum).Stat(i))
        Next
        For i = 1 To MAX_NPC_SKILLS
            buffer.WriteByte(NPC(NpcNum).Skill(i))
        Next
        buffer.WriteInt32(NPC(NpcNum).Level)
        buffer.WriteInt32(NPC(NpcNum).Damage)
        Return buffer.ToArray
    End Function

    Function ShopsData() As Byte()
        Dim buffer As New ByteStream(4)
        For i = 1 To MAX_SHOPS
            If Not Len(Trim$(Shop(i).Name)) > 0 Then Continue For
            buffer.WriteBlock(ShopData(i))
        Next
        Return buffer.ToArray
    End Function

    Function ShopData(shopNum As Integer) As Byte()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(shopNum)
        buffer.WriteInt32(Shop(shopNum).BuyRate)
        buffer.WriteString((Shop(shopNum).Name))
        buffer.WriteInt32(Shop(shopNum).Face)
        For i = 1 To MAX_TRADES
            buffer.WriteInt32(Shop(shopNum).TradeItem(i).CostItem)
            buffer.WriteInt32(Shop(shopNum).TradeItem(i).CostValue)
            buffer.WriteInt32(Shop(shopNum).TradeItem(i).Item)
            buffer.WriteInt32(Shop(shopNum).TradeItem(i).ItemValue)
        Next
        Return buffer.ToArray
    End Function

    Function SkillsData() As Byte()
        Dim buffer As New ByteStream(4)

        For i = 1 To MAX_SKILLS
            If Not Len(Trim$(Skill(i).Name)) > 0 Then Continue For
            buffer.WriteBlock(SkillData(i))
        Next
        Return buffer.ToArray
    End Function

    Function SkillData(skillnum As Integer) As Byte()
        Dim buffer As New ByteStream(4)

        buffer.WriteInt32(skillnum)
        buffer.WriteInt32(Skill(skillnum).AccessReq)
        buffer.WriteInt32(Skill(skillnum).AoE)
        buffer.WriteInt32(Skill(skillnum).CastAnim)
        buffer.WriteInt32(Skill(skillnum).CastTime)
        buffer.WriteInt32(Skill(skillnum).CdTime)
        buffer.WriteInt32(Skill(skillnum).JobReq)
        buffer.WriteInt32(Skill(skillnum).Dir)
        buffer.WriteInt32(Skill(skillnum).Duration)
        buffer.WriteInt32(Skill(skillnum).Icon)
        buffer.WriteInt32(Skill(skillnum).Interval)
        buffer.WriteInt32(Skill(skillnum).IsAoE)
        buffer.WriteInt32(Skill(skillnum).LevelReq)
        buffer.WriteInt32(Skill(skillnum).Map)
        buffer.WriteInt32(Skill(skillnum).MpCost)
        buffer.WriteString((Skill(skillnum).Name))
        buffer.WriteInt32(Skill(skillnum).Range)
        buffer.WriteInt32(Skill(skillnum).SkillAnim)
        buffer.WriteInt32(Skill(skillnum).StunDuration)
        buffer.WriteInt32(Skill(skillnum).Type)
        buffer.WriteInt32(Skill(skillnum).Vital)
        buffer.WriteInt32(Skill(skillnum).X)
        buffer.WriteInt32(Skill(skillnum).Y)
        buffer.WriteInt32(Skill(skillnum).IsProjectile)
        buffer.WriteInt32(Skill(skillnum).Projectile)
        buffer.WriteInt32(Skill(skillnum).KnockBack)
        buffer.WriteInt32(Skill(skillnum).KnockBackTiles)
        Return buffer.ToArray
    End Function

#End Region

End Module