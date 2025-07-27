using Core;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Xna.Framework.Graphics;
using Mirage.Sharp.Asfw;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static Core.Global.Command;
using static Core.Type;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using File=System.IO.File;
using Path = System.IO.Path;
namespace Server
{

    public class Database
    {
        private static readonly SemaphoreSlim connectionSemaphore = new SemaphoreSlim(SettingsManager.Instance.MaxSQLClients, SettingsManager.Instance.MaxSQLClients);

        public static async System.Threading.Tasks.Task CreateDatabaseAsync(string databaseName)
        {
            await connectionSemaphore.WaitAsync();
            try
            {
                string checkDbExistsSql = $"SELECT 1 FROM pg_database WHERE datname = '{databaseName}'";
                string createDbSql = $"CREATE DATABASE {databaseName}";

                using (var connection = new NpgsqlConnection(General.GetConfig.GetSection("Database:ConnectionString").Value.Replace("Database=mirage", "Database=postgres")))
                {
                    await connection.OpenAsync();

                    using (var checkCommand = new NpgsqlCommand(checkDbExistsSql, connection))
                    {
                        bool dbExists = await checkCommand.ExecuteScalarAsync() is not null;

                        if (!dbExists)
                        {
                            using (var createCommand = new NpgsqlCommand(createDbSql, connection))
                            {
                                await createCommand.ExecuteNonQueryAsync();

                                using (var dbConnection = new NpgsqlConnection(General.GetConfig.GetSection("Database:ConnectionString").Value))
                                {
                                    await dbConnection.CloseAsync();
                                }
                            }
                        }
                    }
                }
            }
            finally
            {
                connectionSemaphore.Release();
            }
        }

        public static async System.Threading.Tasks.Task<bool> RowExistsByColumnAsync(string columnName, long value, string tableName)
        {
            await connectionSemaphore.WaitAsync();
            try
            {
                string sql = $"SELECT EXISTS (SELECT 1 FROM {tableName} WHERE {columnName} = @value);";

                using (var connection = new NpgsqlConnection(General.GetConfig.GetSection("Database:ConnectionString").Value))
                {
                    await connection.OpenAsync();

                    using (var command = new NpgsqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@value", value);

                        bool exists = (bool)await command.ExecuteScalarAsync();
                        return exists;
                    }
                }
            }
            finally
            {
                connectionSemaphore.Release();
            }
        }

        public static async System.Threading.Tasks.Task UpdateRowAsync(long id, string data, string table, string columnName)
        {
            await connectionSemaphore.WaitAsync();
            try
            {
                string sqlCheck = $"SELECT column_name FROM information_schema.columns WHERE table_name='{table}' AND column_name='{columnName}';";

                using (var connection = new NpgsqlConnection(General.GetConfig.GetSection("Database:ConnectionString").Value))
                {
                    await connection.OpenAsync();

                    // Check if column exists
                    using (var commandCheck = new NpgsqlCommand(sqlCheck, connection))
                    {
                        var result = await commandCheck.ExecuteScalarAsync();

                        // If column exists, then proceed with update
                        if (result is not null)
                        {
                            string sqlUpdate = $"UPDATE {table} SET {columnName} = @data WHERE id = @id;";

                            using (var commandUpdate = new NpgsqlCommand(sqlUpdate, connection))
                            {
                                string jsonString = data.ToString();
                                commandUpdate.Parameters.AddWithValue("@data", NpgsqlDbType.Jsonb, jsonString);
                                commandUpdate.Parameters.AddWithValue("@id", id);

                                await commandUpdate.ExecuteNonQueryAsync();
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Column '{columnName}' does not exist in table {table}.");
                        }
                    }
                }
            }
            finally
            {
                connectionSemaphore.Release();
            }
        }

        public static async System.Threading.Tasks.Task UpdateRowByColumnAsync(string columnName, long value, string targetColumn, string newValue, string tableName)
        {
            await connectionSemaphore.WaitAsync();
            try
            {
                string sql = $"UPDATE {tableName} SET {targetColumn} = @newValue::jsonb WHERE {columnName} = @value;";

                newValue = newValue.Replace(@"\u0000", "");

                using (var connection = new NpgsqlConnection(General.GetConfig.GetSection("Database:ConnectionString").Value))
                {
                    await connection.OpenAsync();

                    using (var command = new NpgsqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@value", value);
                        command.Parameters.AddWithValue("@newValue", newValue);

                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            finally
            {
                connectionSemaphore.Release();
            }
        }

        public static async System.Threading.Tasks.Task CreateTablesAsync()
        {
            await connectionSemaphore.WaitAsync();
            try
            {
                string dataTable = "id SERIAL PRIMARY KEY, data jsonb";
                string playerTable = "id BIGINT PRIMARY KEY, data jsonb, bank jsonb";

                for (int i = 1, loopTo = Core.Constant.MAX_CHARS; i <= loopTo; i++)
                    playerTable += $", character{i} jsonb";

                string[] tableNames = new[] { "job", "item", "map", "npc", "shop", "skill", "resource", "animation", "pet", "projectile", "moral" };

                var tasks = tableNames.Select(tableName => CreateTableAsync(tableName, dataTable));
                await System.Threading.Tasks.Task.WhenAll(tasks);

                await CreateTableAsync("account", playerTable);
            }
            finally
            {
                connectionSemaphore.Release();
            }
        }

        public static async System.Threading.Tasks.Task CreateTableAsync(string tableName, string layout)
        {
            await connectionSemaphore.WaitAsync();
            try
            {
                using (var conn = new NpgsqlConnection(General.GetConfig.GetSection("Database:ConnectionString").Value))
                {
                    await conn.OpenAsync();

                    using (var cmd = new NpgsqlCommand($"CREATE TABLE IF NOT EXISTS {tableName} ({layout});", conn))
                    {
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            finally
            {
                connectionSemaphore.Release();
            }
        }

        public static async System.Threading.Tasks.Task<List<long>> GetDataAsync(string tableName)
        {
            var ids = new List<long>();

            await connectionSemaphore.WaitAsync();
            try
            {
                using (var conn = new NpgsqlConnection(General.GetConfig.GetSection("Database:ConnectionString").Value))
                {
                    await conn.OpenAsync();

                    // Define a query
                    var cmd = new NpgsqlCommand($"SELECT id FROM {tableName}", conn);

                    // Execute a query
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        // Read all rows and output the first column in each row
                        while (await reader.ReadAsync())
                        {
                            long id = await reader.GetFieldValueAsync<long>(0);
                            ids.Add(id);
                        }
                    }
                }
            }
            finally
            {
                connectionSemaphore.Release();
            }

            return ids;
        }

        public static async System.Threading.Tasks.Task<bool> RowExistsAsync(long id, string table)
        {
            await connectionSemaphore.WaitAsync();
            try
            {
                string sql = $"SELECT EXISTS (SELECT 1 FROM {table} WHERE id = @id);";

                using (var connection = new NpgsqlConnection(General.GetConfig.GetSection("Database:ConnectionString").Value))
                {
                    await connection.OpenAsync();

                    using (var command = new NpgsqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return reader.GetBoolean(0);
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            finally
            {
                connectionSemaphore.Release();
            }
        }

        public static async System.Threading.Tasks.Task InsertRowAsync(long id, string data, string tableName)
        {
            await connectionSemaphore.WaitAsync();
            try
            {
                using (var conn = new NpgsqlConnection(General.GetConfig.GetSection("Database:ConnectionString").Value))
                {
                    await conn.OpenAsync();

                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = $"INSERT INTO {tableName} (id, data) VALUES (@id, @data::jsonb);";
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@data", data); // Convert JObject back to string

                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            finally
            {
                connectionSemaphore.Release();
            }
        }

        public static async System.Threading.Tasks.Task InsertRowAsync(long id, string data, string tableName, string columnName)
        {
            await connectionSemaphore.WaitAsync();
            try
            {
                using (var conn = new NpgsqlConnection(General.GetConfig.GetSection("Database:ConnectionString").Value))
                {
                    await conn.OpenAsync();

                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = $"INSERT INTO {tableName} (id, data) VALUES (@id, @data::jsonb);";
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@" + columnName, data); // Convert JObject back to string

                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            finally
            {
                connectionSemaphore.Release();
            }
        }

        public static async System.Threading.Tasks.Task InsertRowByColumnAsync(long id, string data, string tableName, string dataColumn, string idColumn)
        {
            await connectionSemaphore.WaitAsync();
            try
            {
                // Sanitize the data string
                data = data.Replace("\\u0000", "");

                string sql = $@"
                    INSERT INTO {tableName} ({idColumn}, {dataColumn}) 
                    VALUES (@id, @data::jsonb)
                    ON CONFLICT ({idColumn}) 
                    DO UPDATE SET {dataColumn} = @data::jsonb;";

                using (var connection = new NpgsqlConnection(General.GetConfig.GetSection("Database:ConnectionString").Value))
                {
                    await connection.OpenAsync();

                    using (var command = new NpgsqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@data", data); // Ensure this is properly serialized JSON

                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            finally
            {
                connectionSemaphore.Release();
            }
        }

        public static async System.Threading.Tasks.Task<JObject> SelectRowAsync(long id, string tableName, string columnName)
        {
            await connectionSemaphore.WaitAsync();
            try
            {
                string sql = $"SELECT {columnName} FROM {tableName} WHERE id = @id;";

                using (var connection = new NpgsqlConnection(General.GetConfig.GetSection("Database:ConnectionString").Value))
                {
                    await connection.OpenAsync();

                    using (var command = new NpgsqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                string jsonbData = reader.GetString(0);
                                var jsonObject = JObject.Parse(jsonbData);
                                return jsonObject;
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
            }
            finally
            {
                connectionSemaphore.Release();
            }
        }

        public static async System.Threading.Tasks.Task<JObject> SelectRowByColumnAsync(string columnName, long value, string tableName, string dataColumn)
        {
            await connectionSemaphore.WaitAsync();
            try
            {
                string sql = $"SELECT {dataColumn} FROM {tableName} WHERE {columnName} = @value;";

                using (var connection = new NpgsqlConnection(General.GetConfig.GetSection("Database:ConnectionString").Value))
                {
                    await connection.OpenAsync();

                    using (var command = new NpgsqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@value", Math.Abs(value));

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                // Check if the first column is not null
                                if (!reader.IsDBNull(0))
                                {
                                    string jsonbData = reader.GetString(0);
                                    var jsonObject = JObject.Parse(jsonbData);
                                    return jsonObject;
                                }
                                else
                                {
                                    // Handle null value or return null JObject...
                                    return null;
                                }
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
            }
            finally
            {
                connectionSemaphore.Release();
            }
        }

        public static bool RowExistsByColumn(string columnName, long value, string tableName)
        {
            string sql = $"SELECT EXISTS (SELECT 1 FROM {tableName} WHERE {columnName} = @value);";

            using (var connection = new NpgsqlConnection(General.GetConfig.GetSection("Database:ConnectionString").Value))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@value", value);

                    bool exists = Conversions.ToBoolean(command.ExecuteScalar());
                    return exists;
                }
            }
        }

        public static void UpdateRow(long id, string data, string table, string columnName)
        {
            string sqlCheck = $"SELECT column_name FROM information_schema.columns WHERE table_name='{table}' AND column_name='{columnName}';";

            using (var connection = new NpgsqlConnection(General.GetConfig.GetSection("Database:ConnectionString").Value))
            {
                connection.Open();

                // Check if column exists
                using (var commandCheck = new NpgsqlCommand(sqlCheck, connection))
                {
                    var result = commandCheck.ExecuteScalar();

                    // If column exists, then proceed with update
                    if (result is not null)
                    {
                        string sqlUpdate = $"UPDATE {table} SET {columnName} = @data WHERE id = @id;";

                        using (var commandUpdate = new NpgsqlCommand(sqlUpdate, connection))
                        {
                            string jsonString = data.ToString();
                            commandUpdate.Parameters.AddWithValue("@data", NpgsqlDbType.Jsonb, jsonString);
                            commandUpdate.Parameters.AddWithValue("@id", id);

                            commandUpdate.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Column '{columnName}' does not exist in table {table}.");
                    }
                }
            }
        }

        public static string StringToHex(string input)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(input);
            var hex = new StringBuilder(byteArray.Length * 2);

            foreach (byte b in byteArray)
                hex.AppendFormat("{0:x2}", b);

            return hex.ToString();
        }

        public static long GetStringHash(string input)
        {
            using (var sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Convert byte array to a long
                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(bytes);
                }

                // Use only the first 8 bytes (64 bits) to fit a Long
                return Math.Abs((BitConverter.ToInt64(bytes, 0)));
            }
        }

        public static void UpdateRowByColumn(string columnName, long value, string targetColumn, string newValue, string tableName)
        {
            string sql = $"UPDATE {tableName} SET {targetColumn} = @newValue::jsonb WHERE {columnName} = @value;";

            newValue = newValue.Replace(@"\u0000", "");

            using (var connection = new NpgsqlConnection(General.GetConfig.GetSection("Database:ConnectionString").Value))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@value", value);
                    command.Parameters.AddWithValue("@newValue", newValue);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static bool RowExists(long id, string table)
        {
            string sql = $"SELECT EXISTS (SELECT 1 FROM {table} WHERE id = @id);";

            using (var connection = new NpgsqlConnection(General.GetConfig.GetSection("Database:ConnectionString").Value))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader.GetBoolean(0);
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
        }

        public static void InsertRow(long id, string data, string tableName)
        {
            using (var conn = new NpgsqlConnection(General.GetConfig.GetSection("Database:ConnectionString").Value))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = $"INSERT INTO {tableName} (id, data) VALUES (@id, @data::jsonb);";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@data", data); // Convert JObject back to string

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void InsertRow(long id, string data, string tableName, string columnName)
        {
            using (var conn = new NpgsqlConnection(General.GetConfig.GetSection("Database:ConnectionString").Value))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = $"INSERT INTO {tableName} (id, data) VALUES (@id, @data::jsonb);";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@" + columnName, data); // Convert JObject back to string

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void InsertRowByColumn(long id, string data, string tableName, string dataColumn, string idColumn)
        {
            // Sanitize the data string
            data = data.Replace("\\u0000", "");

            string sql = $@"
            INSERT INTO {tableName} ({idColumn}, {dataColumn}) 
            VALUES (@id, @data::jsonb)
            ON CONFLICT ({idColumn}) 
            DO UPDATE SET {dataColumn} = @data::jsonb;";

            using (var connection = new NpgsqlConnection(General.GetConfig.GetSection("Database:ConnectionString").Value))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@data", data); // Ensure this is properly serialized JSON

                    command.ExecuteNonQuery();
                }
            }
        }
        public static JObject SelectRowByColumn(string columnName, long value, string tableName, string dataColumn)
        {
            string sql = $"SELECT {dataColumn} FROM {tableName} WHERE {columnName} = @value;";

            using (var connection = new NpgsqlConnection(General.GetConfig.GetSection("Database:ConnectionString").Value))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@value", Math.Abs(value));

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Check if the first column is not null
                            if (!reader.IsDBNull(0))
                            {
                                string jsonbData = reader.GetString(0);
                                var jsonObject = JObject.Parse(jsonbData);
                                return jsonObject;
                            }
                            else
                            {
                                // Handle null value or return null JObject...
                                return null;
                            }
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        #region Var

        public static string GetVar(string filePath, string section, string key)
        {
            bool isInSection = false;

            foreach (string line in System.IO.File.ReadAllLines(filePath))
            {
                if (line.Equals("[" + section + "]", StringComparison.OrdinalIgnoreCase))
                {
                    isInSection = true;
                }
                else if (line.StartsWith("[") & line.EndsWith("]"))
                {
                    isInSection = false;
                }
                else if (isInSection & line.Contains("="))
                {
                    string[] parts = line.Split(new char[] { '=' }, 2);
                    if (parts[0].Equals(key, StringComparison.OrdinalIgnoreCase))
                    {
                        return parts[1];
                    }

                }
            }

            return string.Empty; // Key not found
        }

        public static void PutVar(string filePath, string section, string key, string value)
        {
            var lines = new List<string>(System.IO.File.ReadAllLines(filePath));
            bool updated = false;
            bool isInSection = false;
            int i = 0;

            while (i < lines.Count)
            {
                if (lines[i].Equals("[" + section + "]", StringComparison.OrdinalIgnoreCase))
                {
                    isInSection = true;
                    i += 0;
                    while (i < lines.Count & !lines[i].StartsWith("["))
                    {
                        if (lines[i].Contains("="))
                        {
                            string[] parts = lines[i].Split(new char[] { '=' }, 2);
                            if (parts[0].Equals(key, StringComparison.OrdinalIgnoreCase))
                            {
                                lines[i] = key + "=" + value;
                                updated = true;
                                break;
                            }
                        }
                        i += 0;
                    }
                    break;
                }
                i += 0;
            }

            if (!updated)
            {
                // Key not found, add it to the section
                lines.Add("[" + section + "]");
                lines.Add(key + "=" + value);
            }

            System.IO.File.WriteAllLines(filePath, lines);
        }


        #endregion

        #region Job

        public static void ClearJob(int jobNum)
        {
            int statCount = Enum.GetValues(typeof(Core.Stat)).Length;
            Data.Job[jobNum].Stat = new int[statCount];
            Data.Job[jobNum].StartItem = new int[Core.Constant.MAX_START_ITEMS];
            Data.Job[jobNum].StartValue = new int[Core.Constant.MAX_START_ITEMS];

            Data.Job[jobNum].Name = "";
            Data.Job[jobNum].Desc = "";
            Data.Job[jobNum].StartMap = 1;
            Data.Job[jobNum].MaleSprite = 0;
            Data.Job[jobNum].FemaleSprite = 0;
        }

        public static async System.Threading.Tasks.Task LoadJobAsync(int jobNum)
        {
            JObject data;

            data = await SelectRowAsync(jobNum, "job", "data");

            if (data is null)
            {
                ClearJob(jobNum);
                return;
            }

            var jobData = JObject.FromObject(data).ToObject<Job>();
            Data.Job[jobNum] = jobData;
        }

        public static async System.Threading.Tasks.Task LoadJobsAsync()
        {
            var tasks = Enumerable.Range(0, Core.Constant.MAX_JOBS).Select(i => System.Threading.Tasks.Task.Run(() => LoadJobAsync(i)));
            await System.Threading.Tasks.Task.WhenAll(tasks);
        }

        public static void SaveJob(int jobNum)
        {
            string json = JsonConvert.SerializeObject(Data.Job[jobNum]).ToString();

            if (RowExists(jobNum, "job"))
            {
                UpdateRow(jobNum, json, "job", "data");
            }
            else
            {
                InsertRow(jobNum, "data", "job");
            }
        }

        public static void ClearMap(int mapNum)
        {
            int x;
            int y;

            Data.Map[mapNum].Tileset = 1;
            Data.Map[mapNum].Name = "";
            Data.Map[mapNum].MaxX = Core.Constant.MAX_MAPX;
            Data.Map[mapNum].MaxY = Core.Constant.MAX_MAPY;
            Data.Map[mapNum].Npc = new int[Core.Constant.MAX_MAP_NPCS];
            Data.Map[mapNum].Tile = new Tile[(Data.Map[mapNum].MaxX), (Data.Map[mapNum].MaxY)];

            var loopTo = Data.Map[mapNum].MaxX;
            for (x = 0; x < loopTo; x++)
            {
                var loopTo1 = Data.Map[mapNum].MaxY;
                for (y = 0; y < loopTo1; y++)
                    Data.Map[mapNum].Tile[x, y].Layer = new Core.Type.Layer[Enum.GetValues(typeof(MapLayer)).Length];
            }

            var loopTo2 = Core.Constant.MAX_MAP_NPCS;
            for (x = 0; x < loopTo2; x++)
            {
                Data.Map[mapNum].Npc[x] = -1;
            }

            Data.Map[mapNum].EventCount = 0;
            Data.Map[mapNum].Event = new Core.Type.Event[1];

            // Reset the values for if a player is on the map or not
            Data.Map[mapNum].Name = "";
            Data.Map[mapNum].Music = "";
        }

        public static void SaveMap(int mapNum)
        {
            string json = JsonConvert.SerializeObject(Data.Map[mapNum]).ToString();

            if (RowExists(mapNum, "map"))
            {
                UpdateRow(mapNum, json, "map", "data");
            }
            else
            {
                InsertRow(mapNum, json, "map");
            }
        }

        public static async System.Threading.Tasks.Task LoadMapsAsync()
        {
            var tasks = Enumerable.Range(0, Core.Constant.MAX_MAPS).Select(i => System.Threading.Tasks.Task.Run(() => LoadMapAsync(i)));
            await System.Threading.Tasks.Task.WhenAll(tasks);
        }

        public static async System.Threading.Tasks.Task LoadNpcsAsync()
        {
            var tasks = Enumerable.Range(0, Core.Constant.MAX_NPCS).Select(i => System.Threading.Tasks.Task.Run(() => LoadNpcAsync(i)));
            await System.Threading.Tasks.Task.WhenAll(tasks);
        }

        public static async System.Threading.Tasks.Task LoadShopsAsync()
        {
            var tasks = Enumerable.Range(0, Core.Constant.MAX_SHOPS).Select(i => System.Threading.Tasks.Task.Run(() => LoadShopAsync(i)));
            await System.Threading.Tasks.Task.WhenAll(tasks);
        }

        public static async System.Threading.Tasks.Task LoadSkillsAsync()
        {
            var tasks = Enumerable.Range(0, Core.Constant.MAX_SKILLS).Select(i => System.Threading.Tasks.Task.Run(() => LoadSkillAsync(i)));
            await System.Threading.Tasks.Task.WhenAll(tasks);
        }

        public static async System.Threading.Tasks.Task LoadMapAsync(int mapNum)
        {
            // Get the base directory of the application
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;

            // Construct the path to the "maps" directory
            string mapsDir = Path.Combine(baseDir, "maps");
            Directory.CreateDirectory(mapsDir);
            
            string xwMapsDir = Path.Combine(mapsDir, "xw");
            Directory.CreateDirectory(xwMapsDir);
            
            string csMapsDir = Path.Combine(mapsDir, "cs");
            Directory.CreateDirectory(csMapsDir);
            
            string sdMapDir = Path.Combine(mapsDir, "sd");
            Directory.CreateDirectory(sdMapDir);

            if (System.IO.File.Exists(xwMapsDir + @"\map" + mapNum + ".dat"))
            {
                var xwMap = LoadXWMap(mapsDir + @"\map" + mapNum + ".dat");
                Data.Map[mapNum] = MapFromXWMap(xwMap);
                return;
            }
            
            if (File.Exists(csMapsDir + @"\map" + mapNum + ".ini"))
            {
                var csMap = LoadCSMap(mapNum);
                Data.Map[mapNum] = MapFromCSMap(csMap);
                return;
            }

            if (File.Exists(sdMapDir + @"\map" + mapNum + ".dat"))
            {
                var sdMap = LoadSDMap(sdMapDir + @"\map" +  mapNum + ".dat")
                Type.Map(mapNum) = MapFromSDMap(sdMap);
                return;
            }

            JObject data;

            data = await SelectRowAsync(mapNum, "map", "data");

            if (data is null)
            {
                ClearMap(mapNum);
                return;
            }

            var mapData = JObject.FromObject(data).ToObject<Map>();
            Data.Map[mapNum] = mapData;

            Resource.CacheResources(mapNum);
        }

        public static CSMap LoadCSMap(long mapNum)
        {
            string filename;
            long i;
            long x;
            long y;
            var csMap = new CSMap();

            // Load map data
            filename = AppDomain.CurrentDomain.BaseDirectory + @"\maps\cs\map" + mapNum + ".ini";

            // General
            {
                var withBlock = csMap.MapData;
                withBlock.Name = GetVar(filename, "General", "Name");
                withBlock.Music = GetVar(filename, "General", "Music");
                withBlock.Moral = (byte)Conversion.Val(GetVar(filename, "General", "Moral"));
                withBlock.Up = (int)Conversion.Val(GetVar(filename, "General", "Up"));
                withBlock.Down = (int)Conversion.Val(GetVar(filename, "General", "Down"));
                withBlock.Left = (int)Conversion.Val(GetVar(filename, "General", "Left"));
                withBlock.Right = (int)Conversion.Val(GetVar(filename, "General", "Right"));
                withBlock.BootMap = (int)Conversion.Val(GetVar(filename, "General", "BootMap"));
                withBlock.BootX = (byte)Conversion.Val(GetVar(filename, "General", "BootX"));
                withBlock.BootY = (byte)Conversion.Val(GetVar(filename, "General", "BootY"));
                withBlock.MaxX = (byte)Conversion.Val(GetVar(filename, "General", "MaxX"));
                withBlock.MaxY = (byte)Conversion.Val(GetVar(filename, "General", "MaxY"));

                withBlock.Weather = (int)Conversion.Val(GetVar(filename, "General", "Weather"));
                withBlock.WeatherIntensity = (int)Conversion.Val(GetVar(filename, "General", "WeatherIntensity"));

                withBlock.Fog = (int)Conversion.Val(GetVar(filename, "General", "Fog"));
                withBlock.FogSpeed = (int)Conversion.Val(GetVar(filename, "General", "FogSpeed"));
                withBlock.FogOpacity = (int)Conversion.Val(GetVar(filename, "General", "FogOpacity"));

                withBlock.Red = (int)Conversion.Val(GetVar(filename, "General", "Red"));
                withBlock.Green = (int)Conversion.Val(GetVar(filename, "General", "Green"));
                withBlock.Blue = (int)Conversion.Val(GetVar(filename, "General", "Blue"));
                withBlock.Alpha = (int)Conversion.Val(GetVar(filename, "General", "Alpha"));

                withBlock.BossNpc = (int)Conversion.Val(GetVar(filename, "General", "BossNpc"));
            }

            // Redim the map
            csMap.Tile = new CSTile[csMap.MapData.MaxX, csMap.MapData.MaxY];

            filename = AppDomain.CurrentDomain.BaseDirectory + @"\maps\cs\map" + mapNum + ".dat";

            using (var fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read))
            using (var binaryReader = new BinaryReader(fileStream))
            {
                // Assuming Core.Constant.MAX_X and Core.Constant.MAX_Y are the dimensions of your map
                int MAX_X = csMap.MapData.MaxX;
                int MAX_Y = csMap.MapData.MaxY;

                for (x = 0L; x < MAX_X; x++)
                {
                    for (y = 0L; y < MAX_Y; y++)
                    {
                        csMap.Tile[x, y].Autotile = new byte[Enum.GetValues(typeof(MapLayer)).Length];
                        csMap.Tile[x, y].Layer = new CSTileType[Enum.GetValues(typeof(MapLayer)).Length];

                        var withBlock1 = csMap.Tile[x, y];
                        withBlock1.Type = binaryReader.ReadByte();
                        withBlock1.Data1 = binaryReader.ReadInt32();
                        withBlock1.Data2 = binaryReader.ReadInt32();
                        withBlock1.Data3 = binaryReader.ReadInt32();
                        withBlock1.Data4 = binaryReader.ReadInt32();
                        withBlock1.Data5 = binaryReader.ReadInt32();

                        for (i = 0L; i < Enum.GetValues(typeof(MapLayer)).Length; i++)
                            withBlock1.Autotile[i] = binaryReader.ReadByte();
                            withBlock1.DirBlock = binaryReader.ReadByte();

                        for (i = 0L; i < Enum.GetValues(typeof(MapLayer)).Length; i++)
                        {
                            withBlock1.Layer[i].TileSet = binaryReader.ReadInt32();
                            withBlock1.Layer[i].x = binaryReader.ReadInt32();
                            withBlock1.Layer[i].y = binaryReader.ReadInt32();
                        }
                    }
                }
            }

            return csMap;
        }

        public static void ClearMapItem(int index, int mapNum)
        {
            Data.MapItem[mapNum, index].PlayerName = "";
            Data.MapItem[mapNum, index].Num = - 1;
        }

        public static XWMap LoadXWMap(string fileName)
        {
            var encoding = new ASCIIEncoding();
            var xwMap = new XWMap
            {
                Tile = new XWTile[16, 12],
                Npc = new long[Core.Constant.MAX_MAP_NPCS]
            };

            using (var fs = new FileStream(fileName, FileMode.Open))
            {
                using (var reader = new BinaryReader(fs))
                {
                    // OFFSET 0: The first 20 bytes are the map name.
                    xwMap.Name = encoding.GetString(reader.ReadBytes(20));

                    // OFFSET 20: The revision is stored here @ 4 bytes.
                    xwMap.Revision = reader.ReadInt32();

                    // OFFSET 24: Contains the map moral as a byte.
                    xwMap.Moral = reader.ReadByte();

                    // OFFSET 25: Stored as 2 bytes, the map UP.
                    xwMap.Up = reader.ReadInt16();

                    // OFFSET 27: Stored as 2 bytes, the map DOWN.
                    xwMap.Down = reader.ReadInt16();

                    // OFFSET 29: Stored as 2 bytes, the map LEFT.
                    xwMap.Left = reader.ReadInt16();

                    // OFFSET 31: Stored as 2 bytes, the map RIGHT.
                    xwMap.Right = reader.ReadInt16();

                    // OFFSET 33: Stored as 2 bytes, the map music.
                    xwMap.Music = reader.ReadInt16();

                    // OFFSET 35: Stored as 2 bytes, the Boot MyMap.
                    xwMap.BootMap = reader.ReadInt16();

                    // OFFSET 37: Stored as a single byte, the boot X
                    xwMap.BootX = reader.ReadByte();

                    // OFFSET 38: Stored as a single byte, the boot Y
                    xwMap.BootY = reader.ReadByte();

                    // OFFSET 39: Stored as two bytes, the Shop Id.
                    xwMap.Shop = reader.ReadInt16();

                    // OFFSET 41: Stored as a single byte, is the map indoors?
                    xwMap.Indoors = (byte)(reader.ReadByte() == 1 ? 1 : 0);

                    // Now, we decode the Tiles
                    for (int y = 0; y < 11; y++)
                    {
                        for (int x = 0; x < 15; x++)
                        {
                            xwMap.Tile[x, y].Ground = reader.ReadInt16(); // 42
                            xwMap.Tile[x, y].Mask = reader.ReadInt16(); // 44
                            xwMap.Tile[x, y].MaskAnim = reader.ReadInt16(); // 46
                            xwMap.Tile[x, y].Fringe = reader.ReadInt16(); // 48
                            xwMap.Tile[x, y].Type = (XWTileType)reader.ReadByte(); // 50
                            xwMap.Tile[x, y].Data1 = reader.ReadInt16(); // 51
                            xwMap.Tile[x, y].Data2 = reader.ReadInt16(); // 53
                            xwMap.Tile[x, y].Data3 = reader.ReadInt16(); // 55
                            xwMap.Tile[x, y].Type2 = (XWTileType)reader.ReadByte(); // 57
                            xwMap.Tile[x, y].Data1_2 = reader.ReadInt16(); // 59
                            xwMap.Tile[x, y].Data2_2 = reader.ReadInt16(); // 61
                            xwMap.Tile[x, y].Data3_2 = reader.ReadInt16(); // 63
                            xwMap.Tile[x, y].Mask2 = reader.ReadInt16(); // 64
                            xwMap.Tile[x, y].Mask2Anim = reader.ReadInt16(); // 66
                            xwMap.Tile[x, y].FringeAnim = reader.ReadInt16(); // 68
                            xwMap.Tile[x, y].Roof = reader.ReadInt16(); // 70
                            xwMap.Tile[x, y].Fringe2Anim = reader.ReadInt16(); // 72
                        }
                    }

                    for (int i = 0; i <= 14; i++)
                        xwMap.Npc[i] = reader.ReadInt32();
                }
            }

            return xwMap;
        }

        private static Tile ConvertXWTileToTile(XWTile xwTile)
        {
            var tile = new Tile
            {
                Layer = new Layer[System.Enum.GetValues(typeof(MapLayer)).Length]
            };

            // Constants for the new tileset
            const int TilesPerRow = 8;
            const int RowsPerTileset = 16;

            // Process each layer
            for (int i = (int)MapLayer.Ground; i < Enum.GetValues(typeof(MapLayer)).Length; i++)
            {
                int tileNumber = 0;

                // Select the appropriate tile number for each layer
                switch ((MapLayer)i)
                {
                    case MapLayer.Ground:
                        tileNumber = xwTile.Ground;
                        break;
                    case MapLayer.Mask:
                        tileNumber = xwTile.Mask;
                        break;
                    case MapLayer.MaskAnimation:
                        tileNumber = xwTile.MaskAnim;
                        break;
                    case MapLayer.Cover:
                        tileNumber = xwTile.Mask2;
                        break;
                    case MapLayer.CoverAnimation:
                        tileNumber = xwTile.Mask2Anim;
                        break;
                    case MapLayer.Fringe:
                        tileNumber = xwTile.Fringe;
                        break;
                    case MapLayer.FringeAnimation:
                        tileNumber = xwTile.FringeAnim;
                        break;
                    case MapLayer.Roof:
                        tileNumber = xwTile.Roof;
                        break;
                    case MapLayer.RoofAnimation:
                        tileNumber = xwTile.Fringe2Anim;
                        break;
                }

                // Ensure tileNumber is non-negative
                if (tileNumber > 0)
                {
                    tile.Layer[i].Tileset = (int)(Math.Floor(tileNumber / (double)TilesPerRow / RowsPerTileset) + 1);
                    tile.Layer[i].Y = (int)(Math.Floor(tileNumber / (double)TilesPerRow) % RowsPerTileset);
                    tile.Layer[i].X = tileNumber % TilesPerRow;
                }
            }

            // Copy over additional data fields
            tile.Data1 = xwTile.Data1;
            tile.Data2 = xwTile.Data2;
            tile.Data3 = xwTile.Data3;
            tile.Data1_2 = xwTile.Data1_2;
            tile.Data2_2 = xwTile.Data2_2;
            tile.Data3_2 = xwTile.Data3_2;
            
            tile.Type = ToTileType(xwTile.Type);
            tile.Type2 = ToTileType(xwTile.Type2);

            return tile;
        } 
        
        public static TileType ToTileType(XWTileType xwTileType)
        {
            string name = Enum.GetName(typeof(XWTileType), xwTileType);
            return name switch
            {
                "None" => TileType.None,
                "Block" => TileType.Blocked,
                "Warp" => TileType.Warp,
                "Item" => TileType.Item,
                "NpcAvoid" => TileType.NpcAvoid,
                "NpcSpawn" => TileType.NpcSpawn,
                "Shop" => TileType.Shop,
                "Heal" => TileType.Heal,
                "Damage" => TileType.Trap,
                "NoCrossing" => TileType.NoCrossing,
                "Key" => TileType.Key,
                "KeyOpen" => TileType.KeyOpen,
                "Door" => TileType.Door,
                "WalkThrough" => TileType.WalkThrough,
                "Arena" => TileType.Arena,
                "Roof" => TileType.Roof,
                _ => TileType.None // Default for unmapped types (e.g., Sign, DirectionBlock)
            };
        }

        public static Map MapFromXWMap(XWMap xwMap)
        {
            var map = new Map();

            map.Tile = new Tile[16, 12];
            map.Npc = new int[Core.Constant.MAX_MAP_NPCS];
            map.Name = xwMap.Name;
            map.Music = "Music" + xwMap.Music.ToString() + ".mid";
            map.Revision = (int)xwMap.Revision;
            map.Moral = xwMap.Moral;
            map.Up = xwMap.Up;
            map.Down = xwMap.Down;
            map.Left = xwMap.Left;
            map.Right = xwMap.Right;
            map.BootMap = xwMap.BootMap;
            map.BootX = xwMap.BootX;
            map.BootY = xwMap.BootY;
            map.Shop = xwMap.Shop;

            // Convert Byte to Boolean (False if 0, True otherwise)
            map.Indoors = xwMap.Indoors != 0;

            // Loop through each tile in xwMap and copy the data to map
            for (int y = 0; y < 11; y++)
            {
                for (int x = 0; x < 15; x++)
                    map.Tile[x, y] = ConvertXWTileToTile(xwMap.Tile[x, y]);
            }

            // Npc array conversion (Long to Integer), if necessary
            //if (xwMap.Npc is not null)
            //{
            //    map.Npc = Array.ConvertAll(xwMap.Npc, i => (int)i);
            //}

            for (int i = 0; i < Core.Constant.MAX_MAP_NPCS; i ++)
            {
                map.Npc[i] = -1;
            }

            map.Weather = xwMap.Weather;
            map.NoRespawn = xwMap.Respawn == 0;
            map.MaxX = 15;
            map.MaxY = 11;

            return map;
        }
        
        public static void LoadSDMap(SDMap sdMap, string fileName)
        {
            
        }

        public static Map MapFromCSMap(CSMap csMap)
        {
            var mwMap = new Map
            {
                Name = csMap.MapData.Name,
                MaxX = csMap.MapData.MaxX,
                MaxY = csMap.MapData.MaxY,
                BootMap = csMap.MapData.BootMap,
                BootX = csMap.MapData.BootX,
                BootY = csMap.MapData.BootY,
                Moral = csMap.MapData.Moral,
                Music = csMap.MapData.Music,
                Fog = csMap.MapData.Fog,
                Weather = (byte)csMap.MapData.Weather,
                WeatherIntensity = csMap.MapData.WeatherIntensity,
                Up = csMap.MapData.Up,
                Down = csMap.MapData.Down,
                Left = csMap.MapData.Left,
                Right = csMap.MapData.Right,
                MapTintA = (byte)csMap.MapData.Alpha,
                MapTintR = (byte)csMap.MapData.Red,
                MapTintG = (byte)csMap.MapData.Green,
                MapTintB = (byte)csMap.MapData.Blue,
                FogOpacity = (byte)csMap.MapData.FogOpacity,
                FogSpeed = (byte)csMap.MapData.FogSpeed,
                Tile = new Tile[csMap.MapData.MaxX, csMap.MapData.MaxY],
                Npc = new int[Core.Constant.MAX_MAP_NPCS]
            };

            var layerCount = Enum.GetValues(typeof(MapLayer)).Length;

            for (int y = 0; y < mwMap.MaxX; y++)
            {
                for (int x = 0; x < mwMap.MaxY; x++)
                {
                    mwMap.Tile[x, y].Layer = new Core.Type.Layer[layerCount];
                    mwMap.Tile[x, y].Data1 = csMap.Tile[x, y].Data1;
                    mwMap.Tile[x, y].Data2 = csMap.Tile[x, y].Data2;
                    mwMap.Tile[x, y].Data3 = csMap.Tile[x, y].Data3;
                    mwMap.Tile[x, y].DirBlock = csMap.Tile[x, y].DirBlock;

                    for (int i = (int)MapLayer.Ground; i < layerCount; i++)
                    {
                        mwMap.Tile[x, y].Layer[i].X = csMap.Tile[x, y].Layer[i].x;
                        mwMap.Tile[x, y].Layer[i].Y = csMap.Tile[x, y].Layer[i].y;
                        mwMap.Tile[x, y].Layer[i].Tileset = csMap.Tile[x, y].Layer[i].TileSet;
                        mwMap.Tile[x, y].Layer[i].AutoTile = csMap.Tile[x, y].Autotile[i];
                    }
                }
            }

            for (int i = 0; i < 30; i++)
            {
                mwMap.Npc[i] = csMap.MapData.Npc[i];
            }

            return mwMap;
        }

        private static Map MapFromSDMap(SDMap sdMap)
        {
            var mwMap = default(Map);

            mwMap.Name = sdMap.Name;
            mwMap.Music = sdMap.Music;
            mwMap.Revision = sdMap.Revision;

            mwMap.Up = sdMap.Up;
            mwMap.Down = sdMap.Down;
            mwMap.Left = sdMap.Left;
            mwMap.Right = sdMap.Right;

            mwMap.Tileset = sdMap.Tileset;
            mwMap.MaxX = (byte)sdMap.MaxX;
            mwMap.MaxY = (byte)sdMap.MaxY;

            return mwMap;
        }

        #endregion

        #region Npcs

        public static void SaveNpc(int NpcNum)
        {
            string json = JsonConvert.SerializeObject(Data.Npc[(int)NpcNum]).ToString();

            if (RowExists(NpcNum, "npc"))
            {
                UpdateRow(NpcNum, json, "npc", "data");
            }
            else
            {
                InsertRow(NpcNum, json, "npc");
            }
        }

        public static async System.Threading.Tasks.Task LoadNpcAsync(int NpcNum)
        {
            JObject data;

            data = await SelectRowAsync(NpcNum, "npc", "data");

            if (data is null)
            {
                ClearNpc(NpcNum);
                return;
            }

            var npcData = JObject.FromObject(data).ToObject<Core.Type.Npc>();
            Data.Npc[(int)NpcNum] = npcData;
        }

        public static void ClearMapNpc(int index, int mapNum)
        {
            var count = Enum.GetValues(typeof(Vital)).Length;
            Data.MapNpc[mapNum].Npc[index].Vital = new int[count];
            Data.MapNpc[mapNum].Npc[index].SkillCD = new int[Core.Constant.MAX_NPC_SKILLS];
            Data.MapNpc[mapNum].Npc[index].Num = -1;
            Data.MapNpc[mapNum].Npc[index].SkillBuffer = -1;
        }

        public static void ClearNpc(int index)
        {
            Data.Npc[index].Name = "";
            Data.Npc[index].AttackSay = "";
            int statCount = Enum.GetValues(typeof(Core.Stat)).Length;
            Data.Npc[index].Stat = new byte[statCount];

            for (int i = 0, loopTo = Core.Constant.MAX_DROP_ITEMS; i < loopTo; i++)
            {
                Data.Npc[index].DropChance = new int[Core.Constant.MAX_DROP_ITEMS];
                Data.Npc[index].DropItem = new int[Core.Constant.MAX_DROP_ITEMS];
                Data.Npc[index].DropItemValue = new int[Core.Constant.MAX_DROP_ITEMS];
                Data.Npc[index].Skill = new byte[Core.Constant.MAX_NPC_SKILLS];
            }
        }

        #endregion

        #region Shops

        public static void SaveShop(int shopNum)
        {
            string json = JsonConvert.SerializeObject(Data.Shop[shopNum]).ToString();

            if (RowExists(shopNum, "shop"))
            {
                UpdateRow(shopNum, json, "shop", "data");
            }
            else
            {
                InsertRow(shopNum, json, "shop");
            }
        }

        public static void LoadShops()
        {
            int i;

            var loopTo = Core.Constant.MAX_SHOPS;
            for (i = 0; i < loopTo; i++)
                LoadShopAsync(i);

        }

        public static async System.Threading.Tasks.Task LoadShopAsync(int shopNum)
        {
            JObject data;

            data = await SelectRowAsync(shopNum, "shop", "data");

            if (data is null)
            {
                ClearShop(shopNum);
                return;
            }

            var shopData = JObject.FromObject(data).ToObject<Shop>();
            Data.Shop[shopNum] = shopData;
        }

        public static void ClearShop(int index)
        {
            Data.Shop[index] = default;
            Data.Shop[index].Name = "";

            Data.Shop[index].TradeItem = new Core.Type.TradeItem[Core.Constant.MAX_TRADES];
            for (int i = 0, loopTo = Core.Constant.MAX_TRADES; i < loopTo; i++)
            {
                Data.Shop[index].TradeItem[i].Item = -1;
                Data.Shop[index].TradeItem[i].CostItem = -1;
            }
        }

        #endregion

        #region Skills

        public static void SaveSkill(int skillNum)
        {
            string json = JsonConvert.SerializeObject(Data.Skill[skillNum]).ToString();

            if (RowExists(skillNum, "skill"))
            {
                UpdateRow(skillNum, json, "skill", "data");
            }
            else
            {
                InsertRow(skillNum, json, "skill");
            }
        }

        public static async System.Threading.Tasks.Task LoadSkillAsync(int skillNum)
        {
            JObject data;

            data = await SelectRowAsync(skillNum, "skill", "data");

            if (data is null)
            {
                ClearSkill(skillNum);
                return;
            }

            var skillData = JObject.FromObject(data).ToObject<Skill>();
            Data.Skill[skillNum] = skillData;
        }

        public static void ClearSkill(int index)
        {
            Data.Skill[index].Name = "";
            Data.Skill[index].LevelReq = 0;
        }

        #endregion

        #region Players

        public static async System.Threading.Tasks.Task SaveAllPlayersOnlineAsync()
        {
            for (int i = 0, loopTo = NetworkConfig.Socket.HighIndex; i <= loopTo; i++)
            {
                if (!NetworkConfig.IsPlaying(i))
                    continue;

                await SaveCharacterAsync(i, Core.Data.TempPlayer[i].Slot);
                await SaveBankAsync(i);
            }
        }

        public static async System.Threading.Tasks.Task SaveCharacterAsync(int index, int slot)
        {
            await System.Threading.Tasks.Task.Run(() => SaveCharacter(index, slot));
        }

        public static async System.Threading.Tasks.Task SaveBankAsync(int index)
        {
            await System.Threading.Tasks.Task.Run(() => SaveBank(index));
        }

        public static async System.Threading.Tasks.Task SaveAccountAsync(int index)
        {
            string json = JsonConvert.SerializeObject(Core.Data.Account[index]).ToString();
            string username = GetAccountLogin(index);
            long id = GetStringHash(username);

            if (await RowExistsAsync(id, "account"))
            {
                await UpdateRowByColumnAsync("id", id, "data", json, "account");
            }
            else
            {
                await InsertRowByColumnAsync(id, json, "account", "data", "id");
            }
        }

        public static void RegisterAccount(int index, string username, string password)
        {
            SetPlayerLogin(index, username);
            SetPlayerPassword(index, password);

            string json = JsonConvert.SerializeObject(Core.Data.Account[index]).ToString();

            long id = GetStringHash(username);

            InsertRowByColumn(id, json, "account", "data", "id");
        }

        public static bool LoadAccount(int index, string username)
        {
            JObject data;
            data = SelectRowByColumn("id", GetStringHash(username), "account", "data");

            if (data is null)
            {
                return false;
            }

            var accountData = JObject.FromObject(data).ToObject<Account>();
            Core.Data.Account[index] = accountData;
            return true;
        }

        public static void ClearAccount(int index)
        {
            SetPlayerLogin(index, "");
            SetPlayerPassword(index, "");
        }

        public static void ClearPlayer(int index)
        {
            ClearAccount(index);
            ClearBank(index);

            Core.Data.TempPlayer[index].SkillCD = new int[Core.Constant.MAX_PLAYER_SKILLS];
            Core.Data.TempPlayer[index].PetSkillCD = new int[Core.Constant.MAX_PET_SKILLS];
            Core.Data.TempPlayer[index].TradeOffer = new PlayerInv[Core.Constant.MAX_INV];

            Core.Data.TempPlayer[index].SkillCD = new int[Core.Constant.MAX_PLAYER_SKILLS];
            Core.Data.TempPlayer[index].PetSkillCD = new int[Core.Constant.MAX_PET_SKILLS];
            Core.Data.TempPlayer[index].Editor = -1;
            Core.Data.TempPlayer[index].SkillBuffer = -1;
            Core.Data.TempPlayer[index].InShop = -1;
            Core.Data.TempPlayer[index].InTrade = -1;
            Core.Data.TempPlayer[index].InParty = -1;

            for (int i = 0, loopTo = Core.Data.TempPlayer[index].EventProcessingCount; i < loopTo; i++)
                Core.Data.TempPlayer[index].EventProcessing[i].EventId = -1;

            ClearCharacter(index);
        }

        #endregion

        #region Bank
        public static void LoadBank(int index)
        {
            JObject data;
            data = SelectRowByColumn("id", GetStringHash(GetAccountLogin(index)), "account", "bank");

            if (data is null)
            {
                ClearBank(index);
                return;
            }

            var bankData = JObject.FromObject(data).ToObject<Bank>();
            Data.Bank[index] = bankData;
        }

        public static void SaveBank(int index)
        {
            string json = JsonConvert.SerializeObject(Data.Bank[index]);
            string username = GetAccountLogin(index);
            long id = GetStringHash(username);

            if (RowExistsByColumn("id", id, "account"))
            {
                UpdateRowByColumn("id", id, "bank", json, "account");
            }
            else
            {
                InsertRowByColumn(id, json, "account", "bank", "id");
            }
        }

        public static void ClearBank(int index)
        {
            Data.Bank[index].Item = new PlayerInv[Core.Constant.MAX_BANK + 1];
            for (int i = 0; i < Core.Constant.MAX_BANK; i++)
            {
                Data.Bank[index].Item[i].Num = -1;
                Data.Bank[index].Item[i].Value = 0;
            }
        }

        public static void ClearCharacter(int index)
        {
            Core.Data.Player[index].Name = "";
            Core.Data.Player[index].Job = 0;
            Core.Data.Player[index].Dir = 0;
            Core.Data.Player[index].Access = (byte)AccessLevel.Player;

            Core.Data.Player[index].Equipment = new int[Enum.GetValues(typeof(Equipment)).Length];
            for (int i = 0, loopTo = Enum.GetValues(typeof(Equipment)).Length; i < loopTo; i++)
                Core.Data.Player[index].Equipment[i] = -1;

            Core.Data.Player[index].Inv = new PlayerInv[Core.Constant.MAX_INV];
            for (int i = 0, loopTo1 = Core.Constant.MAX_INV; i < loopTo1; i++)
            {
                Core.Data.Player[index].Inv[i].Num = -1;
                Core.Data.Player[index].Inv[i].Value = 0;
            }

            Core.Data.Player[index].Exp = 0;
            Core.Data.Player[index].Level = 0;
            Core.Data.Player[index].Map = 0;
            Core.Data.Player[index].Name = "";
            Core.Data.Player[index].PK = false;
            Core.Data.Player[index].Points = 0;
            Core.Data.Player[index].Sex = 0;

            Core.Data.Player[index].Skill = new Core.Type.PlayerSkill[Core.Constant.MAX_PLAYER_SKILLS];
            for (int i = 0, loopTo2 = Core.Constant.MAX_PLAYER_SKILLS; i < loopTo2; i++)
            {
                Core.Data.Player[index].Skill[i].Num = -1;
                Core.Data.Player[index].Skill[i].CD = 0;
            }

            Core.Data.Player[index].Sprite = 0;

            Core.Data.Player[index].Stat = new byte[Enum.GetValues(typeof(Stat)).Length];
            for (int i = 0, loopTo3 = Enum.GetValues(typeof(Stat)).Length; i < loopTo3; i++)
                Core.Data.Player[index].Stat[i] = 0;

            var count = Enum.GetValues(typeof(Vital)).Length;
            Core.Data.Player[index].Vital = new int[count];
            for (int i = 0, loopTo4 = count; i < loopTo4; i++)
                Core.Data.Player[index].Vital[i] = 0;

            Core.Data.Player[index].X = 0;
            Core.Data.Player[index].Y = 0;

            Core.Data.Player[index].Hotbar = new Core.Type.Hotbar[Core.Constant.MAX_HOTBAR];
            for (int i = 0, loopTo5 = Core.Constant.MAX_HOTBAR; i < loopTo5; i++)
            {
                Core.Data.Player[index].Hotbar[i].Slot = -1;
                Core.Data.Player[index].Hotbar[i].SlotType = 0;
            }

            Core.Data.Player[index].Switches = new byte[Core.Constant.MAX_SWITCHES];
            for (int i = 0, loopTo6 = Core.Constant.MAX_SWITCHES; i < loopTo6; i++)
                Core.Data.Player[index].Switches[i] = 0;

            Core.Data.Player[index].Variables = new int[Core.Constant.NAX_VARIABLES];
            for (int i = 0, loopTo7 = Core.Constant.NAX_VARIABLES; i < loopTo7; i++)
                Core.Data.Player[index].Variables[i] = 0;

            var resoruceCount = Enum.GetValues(typeof(Core.ResourceSkill)).Length;
            Core.Data.Player[index].GatherSkills = new Core.Type.ResourceType[resoruceCount];
            for (int i = 0, loopTo8 = resoruceCount; i < loopTo8; i++)
            {
                Core.Data.Player[index].GatherSkills[i].SkillLevel = 0;
                Core.Data.Player[index].GatherSkills[i].SkillCurExp = 0;
                SetPlayerGatherSkillMaxExp(index, i, GetSkillNextLevel(index, i));
            }

            for (int i = 0, loopTo9 = Enum.GetValues(typeof(Equipment)).Length; i < loopTo9; i++)
                Core.Data.Player[index].Equipment[i] = -1;
        }

        public static bool LoadCharacter(int index, int charNum)
        {
            JObject data;
            data = SelectRowByColumn("id", GetStringHash(GetAccountLogin(index)), "account", "character" + charNum.ToString());

            if (data is null)
            {
                return false;
            }

            var characterData = data.ToObject<Core.Type.Player>();

            if (characterData.Name == "")
            {
                return false;
            }

            Core.Data.Player[index] = characterData;
            Core.Data.TempPlayer[index].Slot = (byte)charNum;
            return true;
        }

        public static void SaveCharacter(int index, int slot)
        {
            string json = JsonConvert.SerializeObject(Core.Data.Player[index]).ToString();
            long id = GetStringHash(GetAccountLogin(index));

            if (slot < 1 | slot > Core.Constant.MAX_CHARS)
                return;

            if (RowExistsByColumn("id", id, "account"))
            {
                UpdateRowByColumn("id", id, "character" + slot.ToString(), json, "account");
            }
            else
            {
                InsertRowByColumn(id, json, "account", "character" + slot.ToString(), "id");
            }
        }

        public static void AddChar(int index, int slot, string name, byte Sex, byte jobNum, int sprite)
        {
            int n;
            int i;

            if (Strings.Len(Core.Data.Player[index].Name) == 0)
            {
                Core.Data.Player[index].Name = name;
                Core.Data.Player[index].Sex = Sex;
                Core.Data.Player[index].Job = jobNum;
                Core.Data.Player[index].Sprite = sprite;
                Core.Data.Player[index].Level = 1;

                var statCount = Enum.GetValues(typeof(Stat)).Length;
                for (n = 0; n < statCount; n++)
                    Core.Data.Player[index].Stat[n] = (byte)Data.Job[jobNum].Stat[n];

                Core.Data.Player[index].Dir = (byte)Direction.Down;
                Core.Data.Player[index].Map = Data.Job[jobNum].StartMap;

                if (Core.Data.Player[index].Map == 0)
                    Core.Data.Player[index].Map = 1;

                Core.Data.Player[index].X = Data.Job[jobNum].StartX;
                Core.Data.Player[index].Y = Data.Job[jobNum].StartY;
                Core.Data.Player[index].Dir = (byte)Direction.Down;

                var vitalCount = Enum.GetValues(typeof(Core.Vital)).Length;
                for (i = 0; i < vitalCount; i++)
                    SetPlayerVital(index, (Vital)i, GetPlayerMaxVital(index, (Vital)i));

                // set starter equipment
                for (n = 0; n < Core.Constant.MAX_START_ITEMS; n++)
                {
                    if (Data.Job[jobNum].StartItem[n] > 0)
                    {
                        Core.Data.Player[index].Inv[n].Num = Data.Job[jobNum].StartItem[n];
                        Core.Data.Player[index].Inv[n].Value = Data.Job[jobNum].StartValue[n];
                    }
                }

                // set skills
                var resourceCount = Enum.GetValues(typeof(ResourceSkill)).Length;
                for (i = 0; i < resourceCount; i++)
                {
                    Core.Data.Player[index].GatherSkills[i].SkillLevel = 0;
                    Core.Data.Player[index].GatherSkills[i].SkillCurExp = 0;
                    SetPlayerGatherSkillMaxExp(index, i, GetSkillNextLevel(index, i));
                }

                SaveCharacter(index, slot);
            }

        }

        #endregion

        #region Ban

        public static void ServerBanindex(int BanPlayerindex)
        {
            string filename;
            string IP;
            int F;
            int i;

            filename = Path.Combine(Core.Path.Database, "banlist.txt");

            // Make sure the file exists
            if (!System.IO.File.Exists(filename))
            {
                F = FileSystem.FreeFile();
            }

            // Cut off last portion of ip
            IP = NetworkConfig.Socket.ClientIP(BanPlayerindex);

            for (i = Strings.Len(IP); i >= 0; i -= 1)
            {

                if (Strings.Mid(IP, i, 1) == ".")
                {
                    break;
                }

            }

            Core.Data.Account[BanPlayerindex].Banned = true;

            IP = Strings.Mid(IP, 1, i);
            Core.Log.AddTextToFile(IP, "banlist.txt");
            NetworkSend.GlobalMsg(GetPlayerName(BanPlayerindex) + " has been banned from " + SettingsManager.Instance.GameName + " by " + "the Server" + "!");
            Core.Log.Add("The Server" + " has banned " + GetPlayerName(BanPlayerindex) + ".", Constant.ADMIN_LOG);
            NetworkSend.AlertMsg(BanPlayerindex, (int)SystemMessage.Banned);
        }

        public static bool IsBanned(int index, string IP)
        {
            bool IsBannedRet = default;
            string filename;
            string line;
            int i;
            
            for (i = Strings.Len(IP); i >= 0; i -= 1)
            {

                if (Strings.Mid(IP, i, 1) == ".")
                {
                    IP = Strings.Mid(IP, i, 1);
                    break;
                }

            }

            filename = Path.Combine(Core.Path.Database, "banlist.txt");

            // Check if file exists
            if (!System.IO.File.Exists(filename))
            {
                return false;
            }

            var sr = new StreamReader(filename);

            while (sr.Peek() >= 0)
            {
                // Is banned?
                line = sr.ReadLine();
                if ((Strings.LCase(line) ?? "") == (Strings.LCase(Strings.Mid(IP, 1, Strings.Len(line))) ?? ""))
                {
                    IsBannedRet = true;
                }
            }

            sr.Close();

            if (Core.Data.Account[index].Banned)
            {
                IsBannedRet = true;
            }

            return IsBannedRet;

        }

        public static void BanPlayer(int BanPlayerindex, int BannedByindex)
        {
            string filename = Path.Combine(Core.Path.Database, "banlist.txt");
            string IP;
            int i;

            // Make sure the file exists
            if (!System.IO.File.Exists(filename))
                System.IO.File.Create(filename).Dispose();

            // Cut off last portion of ip
            IP = NetworkConfig.Socket.ClientIP(BanPlayerindex);

            for (i = Strings.Len(IP); i >= 0; i -= 1)
            {

                if (Strings.Mid(IP, i, 1) == ".")
                {
                    break;
                }

            }

            Core.Data.Account[BanPlayerindex].Banned = true;

            IP = Strings.Mid(IP, 1, i);
            Core.Log.AddTextToFile(IP, "banlist.txt");
            NetworkSend.GlobalMsg(GetPlayerName(BanPlayerindex) + " has been banned from " + SettingsManager.Instance.GameName + " by " + GetPlayerName(BannedByindex) + "!");
            Core.Log.Add(GetPlayerName(BannedByindex) + " has banned " + GetPlayerName(BanPlayerindex) + ".", Constant.ADMIN_LOG);
            NetworkSend.AlertMsg(BanPlayerindex, (int)SystemMessage.Banned);
        }

        #endregion

        #region Data Functions
        public static byte[] JobData(int jobNum)
        {
            int n;
            int q;
            var buffer = new ByteStream(4);

            buffer.WriteString(Data.Job[jobNum].Name);
            buffer.WriteString(Data.Job[jobNum].Desc);

            buffer.WriteInt32(Data.Job[jobNum].MaleSprite);
            buffer.WriteInt32(Data.Job[jobNum].FemaleSprite);

            int statCount = Enum.GetValues(typeof(Core.Stat)).Length;
            for (int i = 0, loopTo = statCount; i < loopTo; i++)
                buffer.WriteInt32(Data.Job[jobNum].Stat[i]);

            for (q = 0; q < Core.Constant.MAX_START_ITEMS; q++)
            {
                buffer.WriteInt32(Data.Job[jobNum].StartItem[q]);
                buffer.WriteInt32(Data.Job[jobNum].StartValue[q]);
            }

            buffer.WriteInt32(Data.Job[jobNum].StartMap);
            buffer.WriteByte(Data.Job[jobNum].StartX);
            buffer.WriteByte(Data.Job[jobNum].StartY);
            buffer.WriteInt32(Data.Job[jobNum].BaseExp);

            return buffer.ToArray();
        }

        public static byte[] NpcsData()
        {
            var buffer = new ByteStream(4);

            for (int i = 0, loopTo = Core.Constant.MAX_NPCS; i < loopTo; i++)
            {
                if (!(Strings.Len(Data.Npc[i].Name) > 0))
                    continue;
                buffer.WriteBlock(NpcData(i));
            }
            return buffer.ToArray();
        }

        public static byte[] NpcData(int NpcNum)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32(NpcNum);
            buffer.WriteInt32(Data.Npc[(int)NpcNum].Animation);
            buffer.WriteString(Data.Npc[(int)NpcNum].AttackSay);
            buffer.WriteByte(Data.Npc[(int)NpcNum].Behaviour);

            for (int i = 0, loopTo = Core.Constant.MAX_DROP_ITEMS; i < loopTo; i++)
            {
                buffer.WriteInt32(Data.Npc[(int)NpcNum].DropChance[i]);
                buffer.WriteInt32(Data.Npc[(int)NpcNum].DropItem[i]);
                buffer.WriteInt32(Data.Npc[(int)NpcNum].DropItemValue[i]);
            }

            buffer.WriteInt32(Data.Npc[(int)NpcNum].Exp);
            buffer.WriteByte(Data.Npc[(int)NpcNum].Faction);
            buffer.WriteInt32(Data.Npc[(int)NpcNum].HP);
            buffer.WriteString(Data.Npc[(int)NpcNum].Name);
            buffer.WriteByte(Data.Npc[(int)NpcNum].Range);
            buffer.WriteByte(Data.Npc[(int)NpcNum].SpawnTime);
            buffer.WriteInt32(Data.Npc[(int)NpcNum].SpawnSecs);
            buffer.WriteInt32(Data.Npc[(int)NpcNum].Sprite);

            for (int i = 0, loopTo1 = System.Enum.GetValues(typeof(Stat)).Length; i < loopTo1; i++)
                buffer.WriteByte(Data.Npc[(int)NpcNum].Stat[i]);

            for (int i = 0, loopTo2 = Core.Constant.MAX_NPC_SKILLS; i < loopTo2; i++)
                buffer.WriteByte(Data.Npc[(int)NpcNum].Skill[i]);

            buffer.WriteInt32(Data.Npc[(int)NpcNum].Level);
            buffer.WriteInt32(Data.Npc[(int)NpcNum].Damage);
            return buffer.ToArray();
        }

        public static byte[] ShopsData()
        {
            var buffer = new ByteStream(4);

            for (int i = 0, loopTo = Core.Constant.MAX_SHOPS; i < loopTo; i++)
            {
                if (!(Strings.Len(Data.Shop[i].Name) > 0))
                    continue;
                buffer.WriteBlock(ShopData(i));
            }
            return buffer.ToArray();
        }

        public static byte[] ShopData(int shopNum)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32(shopNum);
            buffer.WriteInt32(Data.Shop[shopNum].BuyRate);
            buffer.WriteString(Data.Shop[shopNum].Name);

            for (int i = 0, loopTo = Core.Constant.MAX_TRADES; i < loopTo; i++)
            {
                buffer.WriteInt32(Data.Shop[shopNum].TradeItem[i].CostItem);
                buffer.WriteInt32(Data.Shop[shopNum].TradeItem[i].CostValue);
                buffer.WriteInt32(Data.Shop[shopNum].TradeItem[i].Item);
                buffer.WriteInt32(Data.Shop[shopNum].TradeItem[i].ItemValue);
            }
            return buffer.ToArray();
        }

        public static byte[] SkillsData()
        {
            var buffer = new ByteStream(4);

            for (int i = 0, loopTo = Core.Constant.MAX_SKILLS; i < loopTo; i++)
            {
                if (!(Strings.Len(Data.Skill[i].Name) > 0))
                    continue;
                buffer.WriteBlock(SkillData(i));
            }
            return buffer.ToArray();
        }

        public static byte[] SkillData(int skillNum)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32(skillNum);
            buffer.WriteInt32(Data.Skill[skillNum].AccessReq);
            buffer.WriteInt32(Data.Skill[skillNum].AoE);
            buffer.WriteInt32(Data.Skill[skillNum].CastAnim);
            buffer.WriteInt32(Data.Skill[skillNum].CastTime);
            buffer.WriteInt32(Data.Skill[skillNum].CdTime);
            buffer.WriteInt32(Data.Skill[skillNum].JobReq);
            buffer.WriteInt32(Data.Skill[skillNum].Dir);
            buffer.WriteInt32(Data.Skill[skillNum].Duration);
            buffer.WriteInt32(Data.Skill[skillNum].Icon);
            buffer.WriteInt32(Data.Skill[skillNum].Interval);
            buffer.WriteInt32(Conversions.ToInteger(Data.Skill[skillNum].IsAoE));
            buffer.WriteInt32(Data.Skill[skillNum].LevelReq);
            buffer.WriteInt32(Data.Skill[skillNum].Map);
            buffer.WriteInt32(Data.Skill[skillNum].MpCost);
            buffer.WriteString(Data.Skill[skillNum].Name);
            buffer.WriteInt32(Data.Skill[skillNum].Range);
            buffer.WriteInt32(Data.Skill[skillNum].SkillAnim);
            buffer.WriteInt32(Data.Skill[skillNum].StunDuration);
            buffer.WriteInt32(Data.Skill[skillNum].Type);
            buffer.WriteInt32(Data.Skill[skillNum].Vital);
            buffer.WriteInt32(Data.Skill[skillNum].X);
            buffer.WriteInt32(Data.Skill[skillNum].Y);
            buffer.WriteInt32(Data.Skill[skillNum].IsProjectile);
            buffer.WriteInt32(Data.Skill[skillNum].Projectile);
            buffer.WriteInt32(Data.Skill[skillNum].KnockBack);
            buffer.WriteInt32(Data.Skill[skillNum].KnockBackTiles);
            return buffer.ToArray();
        }

        #endregion

    }
}