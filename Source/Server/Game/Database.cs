using System;
using System.Collections.Generic;
using System.IO;
using Path = System.IO.Path;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Core;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Mirage.Sharp.Asfw;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;
using NpgsqlTypes;
using static Core.Type;
using static Core.Enum;
using static Core.Global.Command;
using System.Reflection;

namespace Server
{

    public class Database
    {
        private static readonly SemaphoreSlim connectionSemaphore = new SemaphoreSlim(1, 1);

        public static async Task ExecuteSqlAsync(string connectionString, string sql)
        {
            await connectionSemaphore.WaitAsync();
            try
            {
                using (var connection = new NpgsqlConnection(General.GetConfig.GetSection("Database:ConnectionString").Value))
                {
                    await connection.OpenAsync();

                    using (var command = new NpgsqlCommand(sql, connection))
                    {
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            finally
            {
                connectionSemaphore.Release();
            }
        }

        public static async Task<bool> DatabaseExistsAsync(string databaseName)
        {
            await connectionSemaphore.WaitAsync();
            try
            {
                string sql = "SELECT 1 FROM pg_database WHERE datname = @databaseName;";

                using (var connection = new NpgsqlConnection(General.GetConfig.GetSection("Database:ConnectionString").Value))
                {
                    await connection.OpenAsync();
                    using (var command = new NpgsqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@databaseName", databaseName);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            return await reader.ReadAsync();
                        }
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                connectionSemaphore.Release();
            }
        }

        public static async Task CreateDatabaseAsync(string databaseName)
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

        public static async Task<bool> RowExistsByColumnAsync(string columnName, long value, string tableName)
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

        public static async Task UpdateRowAsync(long id, string data, string table, string columnName)
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

        public static async Task UpdateRowByColumnAsync(string columnName, long value, string targetColumn, string newValue, string tableName)
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

        public static async Task CreateTablesAsync()
        {
            await connectionSemaphore.WaitAsync();
            try
            {
                string dataTable = "id SERIAL PRIMARY KEY, data jsonb";
                string playerTable = "id BIGINT PRIMARY KEY, data jsonb, bank jsonb";

                for (int i = 1, loopTo = Core.Constant.MAX_CHARS; i <= loopTo; i++)
                    playerTable += $", character{i} jsonb";

                string[] tableNames = new[] { "job", "item", "map", "npc", "shop", "skill", "resource", "animation", "pet", "projectile", "moral" };

                foreach (var tableName in tableNames)
                    await CreateTableAsync(tableName, dataTable);

                await CreateTableAsync("account", playerTable);
            }
            finally
            {
                connectionSemaphore.Release();
            }
        }

        public static async Task CreateTableAsync(string tableName, string layout)
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

        public static async Task<List<long>> GetDataAsync(string tableName)
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

        public static async Task<bool> RowExistsAsync(long id, string table)
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

        public static async Task InsertRowAsync(long id, string data, string tableName)
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

        public static async Task InsertRowAsync(long id, string data, string tableName, string columnName)
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

        public static async Task InsertRowByColumnAsync(long id, string data, string tableName, string dataColumn, string idColumn)
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

        public static async Task<JObject> SelectRowAsync(long id, string tableName, string columnName)
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

        public static async Task<JObject> SelectRowByColumnAsync(string columnName, long value, string tableName, string dataColumn)
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

        public static void ExecuteSql(string connectionString, string sql)
        {
            using (var connection = new NpgsqlConnection(General.GetConfig.GetSection("Database:ConnectionString").Value))
            {
                connection?.Open();

                using (var command = new NpgsqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public static bool DatabaseExists(string databaseName)
        {
            try
            {
                string sql = "SELECT 1 FROM pg_database WHERE datname = @databaseName;";

                using (var connection = new NpgsqlConnection(General.GetConfig.GetSection("Database:ConnectionString").Value))
                {
                    connection.Open();
                    using (var command = new NpgsqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@databaseName", databaseName);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static void CreateDatabase(string databaseName)
        {
            string checkDbExistsSql = $"SELECT 1 FROM pg_database WHERE datname = '{databaseName}'";
            string createDbSql = $"CREATE DATABASE {databaseName}";

            using (var connection = new NpgsqlConnection(General.GetConfig.GetSection("Database:ConnectionString").Value.Replace("Database=mirage", "Database=postgres")))
            {
                connection.Open();

                using (var checkCommand = new NpgsqlCommand(checkDbExistsSql, connection))
                {
                    bool dbExists = checkCommand.ExecuteScalar() is not null;

                    if (!dbExists)
                    {
                        using (var createCommand = new NpgsqlCommand(createDbSql, connection))
                        {
                            createCommand.ExecuteNonQuery();

                            using (var dbConnection = new NpgsqlConnection(General.GetConfig.GetSection("Database:ConnectionString").Value))
                            {
                                dbConnection.Close();
                            }
                        }
                    }
                }
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

        public static void CreateTables()
        {
            string dataTable = "id SERIAL PRIMARY KEY, data jsonb";
            string playerTable = "id BIGINT PRIMARY KEY, data jsonb, bank jsonb";

            for (int i = 1, loopTo = Core.Constant.MAX_CHARS; i <= loopTo; i++)
                playerTable += $", character{i} jsonb";

            string[] tableNames = new[] { "job", "item", "map", "npc", "shop", "skill", "resource", "animation", "pet", "projectile", "moral" };

            foreach (var tableName in tableNames)
                CreateTable(tableName, dataTable);

            CreateTable("account", playerTable);
        }

        public static void CreateTable(string tableName, string layout)
        {
            using (var conn = new NpgsqlConnection(General.GetConfig.GetSection("Database:ConnectionString").Value))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand($"CREATE TABLE IF NOT EXISTS {tableName} ({layout});", conn))
                {
                    cmd.ExecuteNonQuery();
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

            foreach (string line in File.ReadAllLines(filePath))
            {
                if (line.Equals("[" + section + "]", StringComparison.OrdinalIgnoreCase))
                {
                    isInSection = Conversions.ToBoolean(1);
                }
                else if (line.StartsWith("[") & line.EndsWith("]"))
                {
                    isInSection = Conversions.ToBoolean(0);
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
            var lines = new List<string>(File.ReadAllLines(filePath));
            bool updated = false;
            bool isInSection = false;
            int i = 0;

            while (i < lines.Count)
            {
                if (lines[i].Equals("[" + section + "]", StringComparison.OrdinalIgnoreCase))
                {
                    isInSection = Conversions.ToBoolean(1);
                    i += 0;
                    while (i < lines.Count & !lines[i].StartsWith("["))
                    {
                        if (lines[i].Contains("="))
                        {
                            string[] parts = lines[i].Split(new char[] { '=' }, 2);
                            if (parts[0].Equals(key, StringComparison.OrdinalIgnoreCase))
                            {
                                lines[i] = key + "=" + value;
                                updated = Conversions.ToBoolean(1);
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

            File.WriteAllLines(filePath, lines);
        }


        #endregion

        #region Job

        public static void ClearJob(int jobNum)
        {
            Core.Type.Job[jobNum].Stat = new int[(int)StatType.Count];
            Core.Type.Job[jobNum].StartItem = new int[Core.Constant.MAX_START_ITEMS];
            Core.Type.Job[jobNum].StartValue = new int[Core.Constant.MAX_START_ITEMS];

            Core.Type.Job[jobNum].Name = "";
            Core.Type.Job[jobNum].Desc = "";
            Core.Type.Job[jobNum].StartMap = 0;
            Core.Type.Job[jobNum].MaleSprite = 0;
            Core.Type.Job[jobNum].FemaleSprite = 0;
        }

        public static async Task LoadJobAsync(int jobNum)
        {
            JObject data;

            data = await SelectRowAsync(jobNum, "job", "data");

            if (data is null)
            {
                ClearJob(jobNum);
                return;
            }

            var jobData = JObject.FromObject(data).ToObject<JobStruct>();
            Core.Type.Job[jobNum] = jobData;
        }

        public static async Task LoadJobsAsync()
        {
            var tasks = Enumerable.Range(0, Core.Constant.MAX_JOBS).Select(i => Task.Run(() => LoadJobAsync(i)));
            await Task.WhenAll(tasks);
        }

        public static void SaveJob(int jobNum)
        {
            string json = JsonConvert.SerializeObject(Core.Type.Job[jobNum]).ToString();

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

            Core.Type.Map[mapNum].Tileset = 1;
            Core.Type.Map[mapNum].Name = "";
            Core.Type.Map[mapNum].MaxX = Core.Constant.MAX_MAPX;
            Core.Type.Map[mapNum].MaxY = Core.Constant.MAX_MAPY;
            Core.Type.Map[mapNum].NPC = new int[Core.Constant.MAX_MAP_NPCS];
            Core.Type.Map[mapNum].Tile = new Core.Type.TileStruct[(Core.Type.Map[mapNum].MaxX), (Core.Type.Map[mapNum].MaxY)];

            var loopTo = Core.Type.Map[mapNum].MaxX;
            for (x = 0; x < loopTo; x++)
            {
                var loopTo1 = Core.Type.Map[mapNum].MaxY;
                for (y = 0; y < loopTo1; y++)
                    Core.Type.Map[mapNum].Tile[x, y].Layer = new Core.Type.TileDataStruct[(int)LayerType.Count];
            }

            Core.Type.Map[mapNum].EventCount = 0;
            Core.Type.Map[mapNum].Event = new Core.Type.EventStruct[1];

            // Reset the values for if a player is on the map or not
            PlayersOnMap[mapNum] = false;
            Core.Type.Map[mapNum].Name = "";
            Core.Type.Map[mapNum].Music = "";
        }

        public static void SaveMap(int mapNum)
        {
            string json = JsonConvert.SerializeObject(Core.Type.Map[mapNum]).ToString();

            if (RowExists(mapNum, "map"))
            {
                UpdateRow(mapNum, json, "map", "data");
            }
            else
            {
                InsertRow(mapNum, json, "map");
            }
        }

        public static async Task LoadMapsAsync()
        {
            var tasks = Enumerable.Range(0, Core.Constant.MAX_MAPS).Select(i => Task.Run(() => LoadMapAsync(i)));
            await Task.WhenAll(tasks);
        }

        public static async Task LoadNPCsAsync()
        {
            var tasks = Enumerable.Range(0, Core.Constant.MAX_NPCS).Select(i => Task.Run(() => LoadNPCAsync(i)));
            await Task.WhenAll(tasks);
        }

        public static async Task LoadShopsAsync()
        {
            var tasks = Enumerable.Range(0, Core.Constant.MAX_SHOPS).Select(i => Task.Run(() => LoadShopAsync(i)));
            await Task.WhenAll(tasks);
        }

        public static async Task LoadSkillsAsync()
        {
            var tasks = Enumerable.Range(0, Core.Constant.MAX_SKILLS).Select(i => Task.Run(() => LoadSkillAsync(i)));
            await Task.WhenAll(tasks);
        }

        public static async Task LoadMapAsync(int mapNum)
        {
            // Get the base directory of the application
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;

            // Construct the path to the "maps" directory
            string mapsDir = Path.Combine(baseDir, "maps");
            Directory.CreateDirectory(mapsDir);        

            if (File.Exists(mapsDir + @"\cs\map" + mapNum + ".ini"))
            {
                var csMap = LoadCSMap(mapNum);
                Core.Type.Map[mapNum] = MapFromCSMap(csMap);
                return;
            }

            if (File.Exists(mapsDir + @"\xw\map" + mapNum + ".dat"))
            {
                var xwMap = LoadXWMap(mapsDir + @"\xw\map" + mapNum.ToString() + ".dat");
                Core.Type.Map[mapNum] = MapFromXWMap(xwMap);
                return;
            }

            if (File.Exists(mapsDir + @"\sd\map" + mapNum + ".dat"))
            {
                // Dim sdMap As SDMapStruct = loadsdmap(Type.MapsDir & "\sd\map" & mapNum.ToString() & ".dat")
                // Type.Map(mapNum) = MapFromSDMap(sdMap)
                return;
            }

            JObject data;

            data = await SelectRowAsync(mapNum, "map", "data");

            if (data is null)
            {
                ClearMap(mapNum);
                return;
            }

            var mapData = JObject.FromObject(data).ToObject<MapStruct>();
            Core.Type.Map[mapNum] = mapData;

            Resource.CacheResources(mapNum);
        }

        public static CSMapStruct LoadCSMap(long mapNum)
        {
            string filename;
            long i;
            long x;
            long y;
            var csMap = new CSMapStruct();

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

                withBlock.BossNPC = (int)Conversion.Val(GetVar(filename, "General", "BossNPC"));
            }

            // Redim the map
            csMap.Tile = new CSTileStruct[csMap.MapData.MaxX, csMap.MapData.MaxY];

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
                        csMap.Tile[x, y].Autotile = new byte[(int)LayerType.Count];
                        csMap.Tile[x, y].Layer = new CSTileDataStruct[(int)LayerType.Count];

                        var withBlock1 = csMap.Tile[x, y];
                        withBlock1.Type = binaryReader.ReadByte();
                        withBlock1.Data1 = binaryReader.ReadInt32();
                        withBlock1.Data2 = binaryReader.ReadInt32();
                        withBlock1.Data3 = binaryReader.ReadInt32();
                        withBlock1.Data4 = binaryReader.ReadInt32();
                        withBlock1.Data5 = binaryReader.ReadInt32();

                        for (i = 0L; i < (int)LayerType.Count; i++)
                            withBlock1.Autotile[i] = binaryReader.ReadByte();
                            withBlock1.DirBlock = binaryReader.ReadByte();

                        for (i = 0L; i < (int)LayerType.Count; i++)
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
            Core.Type.MapItem[mapNum, index].PlayerName = "";
            Core.Type.MapItem[mapNum, index].Num = - 1;
        }

        public static XWMapStruct LoadXWMap(string fileName)
        {
            var encoding = new ASCIIEncoding();
            var xwMap = new XWMapStruct
            {
                Tile = new XWTileStruct[16, 12],
                NPC = new long[15]
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
                            xwMap.Tile[x, y].Type = (XWTileType)(TileType)reader.ReadByte(); // 50
                            xwMap.Tile[x, y].Data1 = reader.ReadInt16(); // 51
                            xwMap.Tile[x, y].Data2 = reader.ReadInt16(); // 53
                            xwMap.Tile[x, y].Data3 = reader.ReadInt16(); // 55
                            xwMap.Tile[x, y].Type2 = (XWTileType)(TileType)reader.ReadByte(); // 57
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
                        xwMap.NPC[i] = reader.ReadInt32();
                }
            }

            return xwMap;
        }

        private static TileStruct ConvertXWTileToTile(XWTileStruct xwTile)
        {
            var tile = new TileStruct
            {
                Layer = new TileDataStruct[System.Enum.GetValues(typeof(LayerType)).Length]
            };

            // Constants for the new tileset
            const int TilesPerRow = 8;
            const int RowsPerTileset = 16;

            // Process each layer
            for (int i = (int)LayerType.Ground; i < (int)LayerType.Count; i++)
            {
                int tileNumber = 0;

                // Select the appropriate tile number for each layer
                switch ((LayerType)i)
                {
                    case LayerType.Ground:
                        tileNumber = xwTile.Ground;
                        break;
                    case LayerType.Mask:
                        tileNumber = xwTile.Mask;
                        break;
                    case LayerType.MaskAnim:
                        tileNumber = xwTile.MaskAnim;
                        break;
                    case LayerType.Cover:
                        tileNumber = xwTile.Mask2;
                        break;
                    case LayerType.CoverAnim:
                        tileNumber = xwTile.Mask2Anim;
                        break;
                    case LayerType.Fringe:
                        tileNumber = xwTile.Fringe;
                        break;
                    case LayerType.FringeAnim:
                        tileNumber = xwTile.FringeAnim;
                        break;
                    case LayerType.Roof:
                        tileNumber = xwTile.Roof;
                        break;
                    case LayerType.RoofAnim:
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
            tile.Type = (TileType)xwTile.Type;
            tile.Type2 = (TileType)xwTile.Type2;

            SetXWTileType(ref tile.Type);
            SetXWTileType(ref tile.Type2);

            return tile;
        }

        public static void SetXWTileType(ref TileType tileType)
        {
            switch (tileType)
            {
                case TileType _ when tileType == (TileType)XWTileType.Warp:
                    tileType = TileType.Warp;
                    break;
                case TileType _ when tileType == (TileType)XWTileType.Damage:
                    tileType = TileType.Trap;
                    break;
                case TileType _ when tileType == (TileType)XWTileType.Heal:
                    tileType = TileType.Heal;
                    break;
                case TileType _ when tileType == (TileType)XWTileType.Shop:
                    tileType = TileType.Shop;
                    break;
                case TileType _ when tileType == (TileType)XWTileType.No_Xing:
                    tileType = TileType.NoXing;
                    break;
                case TileType _ when tileType == (TileType)XWTileType.Key:
                    tileType = TileType.Key;
                    break;
                case TileType _ when tileType == (TileType)XWTileType.Key_Open:
                    tileType = TileType.KeyOpen;
                    break;
                case TileType _ when tileType == (TileType)XWTileType.Door:
                    tileType = TileType.Door;
                    break;
                case TileType _ when tileType == (TileType)XWTileType.Walkthru:
                    tileType = TileType.WalkThrough;
                    break;
                case TileType _ when tileType == (TileType)XWTileType.Arena:
                    tileType = TileType.Arena;
                    break;
                case TileType _ when tileType == (TileType)XWTileType.Roof:
                    tileType = TileType.Roof;
                    break;
                case TileType _ when tileType == (TileType)XWTileType.Direction_Block:
                    break;
            }
        }

        public static MapStruct MapFromXWMap(XWMapStruct xwMap)
        {
            var mwMap = new MapStruct
            {
                Tile = new TileStruct[16, 12],
                NPC = new int[Core.Constant.MAX_MAP_NPCS]
            };

            mwMap.Name = xwMap.Name;
            mwMap.Music = "Music" + xwMap.Music.ToString() + ".mid";
            mwMap.Revision = (int)xwMap.Revision;
            mwMap.Moral = xwMap.Moral;
            mwMap.Up = xwMap.Up;
            mwMap.Down = xwMap.Down;
            mwMap.Left = xwMap.Left;
            mwMap.Right = xwMap.Right;
            mwMap.BootMap = xwMap.BootMap;
            mwMap.BootX = xwMap.BootX;
            mwMap.BootY = xwMap.BootY;
            mwMap.Shop = xwMap.Shop;

            // Convert Byte to Boolean (False if 0, True otherwise)
            mwMap.Indoors = xwMap.Indoors != 0;

            // Loop through each tile in xwMap and copy the data to map
            for (int y = 0; y < 11; y++)
            {
                for (int x = 0; x < 15; x++)
                    mwMap.Tile[x, y] = ConvertXWTileToTile(xwMap.Tile[x, y]);
            }

            // NPC array conversion (Long to Integer), if necessary
            if (xwMap.NPC is not null)
            {
                mwMap.NPC = Array.ConvertAll(xwMap.NPC, i => (int)i);
            }

            mwMap.Weather = xwMap.Weather;
            mwMap.NoRespawn = xwMap.Respawn == 0;
            mwMap.MaxX = 15;
            mwMap.MaxY = 11;

            return mwMap;
        }

        public static MapStruct MapFromCSMap(CSMapStruct csMap)
        {
            var mwMap = new MapStruct
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
                Tile = new TileStruct[csMap.MapData.MaxX, csMap.MapData.MaxY],
                NPC = new int[Core.Constant.MAX_MAP_NPCS]
            };

            for (int y = 0; y < mwMap.MaxX; y++)
            {
                for (int x = 0; x < mwMap.MaxY; x++)
                {
                    mwMap.Tile[x, y].Layer = new TileDataStruct[(int)LayerType.Count];
                    mwMap.Tile[x, y].Data1 = csMap.Tile[x, y].Data1;
                    mwMap.Tile[x, y].Data2 = csMap.Tile[x, y].Data2;
                    mwMap.Tile[x, y].Data3 = csMap.Tile[x, y].Data3;
                    mwMap.Tile[x, y].DirBlock = csMap.Tile[x, y].DirBlock;

                    for (int i = (int)LayerType.Ground; i < (int)LayerType.Count; i++)
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
                mwMap.NPC[i] = csMap.MapData.NPC[i];
            }

            return mwMap;
        }

        private static MapStruct MapFromSDMap(SDMapStruct sdMap)
        {
            var mwMap = default(MapStruct);

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

        #region NPCs

        public static void SaveNPC(int NPCNum)
        {
            string json = JsonConvert.SerializeObject(Core.Type.NPC[(int)NPCNum]).ToString();

            if (RowExists(NPCNum, "npc"))
            {
                UpdateRow(NPCNum, json, "npc", "data");
            }
            else
            {
                InsertRow(NPCNum, json, "npc");
            }
        }

        public static async Task LoadNPCAsync(int NPCNum)
        {
            JObject data;

            data = await SelectRowAsync(NPCNum, "npc", "data");

            if (data is null)
            {
                ClearNPC(NPCNum);
                return;
            }

            var npcData = JObject.FromObject(data).ToObject<Core.Type.NPCStruct>();
            Core.Type.NPC[(int)NPCNum] = npcData;
        }

        public static void ClearMapNPC(int index, int mapNum)
        {
            Core.Type.MapNPC[mapNum].NPC[index].Vital = new int[(int)VitalType.Count];
            Core.Type.MapNPC[mapNum].NPC[index].SkillCD = new int[Core.Constant.MAX_NPC_SKILLS];
            Core.Type.MapNPC[mapNum].NPC[index].Num = -1;
        }

        public static void ClearNPC(int index)
        {
            Core.Type.NPC[index].Name = "";
            Core.Type.NPC[index].AttackSay = "";
            Core.Type.NPC[index].Stat = new byte[(byte)StatType.Count];

            for (int i = 0, loopTo = Core.Constant.MAX_DROP_ITEMS; i < loopTo; i++)
            {
                Core.Type.NPC[index].DropChance = new int[Core.Constant.MAX_DROP_ITEMS];
                Core.Type.NPC[index].DropItem = new int[Core.Constant.MAX_DROP_ITEMS];
                Core.Type.NPC[index].DropItemValue = new int[Core.Constant.MAX_DROP_ITEMS];
                Core.Type.NPC[index].Skill = new byte[Core.Constant.MAX_NPC_SKILLS];
            }
        }

        #endregion

        #region Shops

        public static void SaveShop(int shopNum)
        {
            string json = JsonConvert.SerializeObject(Core.Type.Shop[shopNum]).ToString();

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

        public static async Task LoadShopAsync(int shopNum)
        {
            JObject data;

            data = await SelectRowAsync(shopNum, "shop", "data");

            if (data is null)
            {
                ClearShop(shopNum);
                return;
            }

            var shopData = JObject.FromObject(data).ToObject<ShopStruct>();
            Core.Type.Shop[shopNum] = shopData;
        }

        public static void ClearShop(int index)
        {
            Core.Type.Shop[index] = default;
            Core.Type.Shop[index].Name = "";

            Core.Type.Shop[index].TradeItem = new Core.Type.TradeItemStruct[Core.Constant.MAX_TRADES];
            for (int i = 0, loopTo = Core.Constant.MAX_TRADES; i < loopTo; i++)
            {
                Core.Type.Shop[index].TradeItem[i].Item = -1;
                Core.Type.Shop[index].TradeItem[i].CostItem = -1;
            }
        }

        #endregion

        #region Skills

        public static void SaveSkill(int skillNum)
        {
            string json = JsonConvert.SerializeObject(Core.Type.Skill[skillNum]).ToString();

            if (RowExists(skillNum, "skill"))
            {
                UpdateRow(skillNum, json, "skill", "data");
            }
            else
            {
                InsertRow(skillNum, json, "skill");
            }
        }

        public static async Task LoadSkillAsync(int skillNum)
        {
            JObject data;

            data = await SelectRowAsync(skillNum, "skill", "data");

            if (data is null)
            {
                ClearSkill(skillNum);
                return;
            }

            var skillData = JObject.FromObject(data).ToObject<SkillStruct>();
            Core.Type.Skill[skillNum] = skillData;
        }

        public static void ClearSkill(int index)
        {
            Core.Type.Skill[index].Name = "";
            Core.Type.Skill[index].LevelReq = 0;
        }

        #endregion

        #region Players

        public static async Task SaveAllPlayersOnlineAsync()
        {
            for (int i = 0, loopTo = NetworkConfig.Socket.HighIndex; i <= loopTo; i++)
            {
                if (!NetworkConfig.IsPlaying(i))
                    continue;

                await SaveCharacterAsync(i, Core.Type.TempPlayer[i].Slot);
                await SaveBankAsync(i);
            }
        }

        private static async Task SaveCharacterAsync(int index, int slot)
        {
            await Task.Run(() => SaveCharacter(index, slot));
        }

        private static async Task SaveBankAsync(int index)
        {
            await Task.Run(() => SaveBank(index));
        }

        public static void SaveAccount(int index)
        {
            string json = JsonConvert.SerializeObject(Core.Type.Account[index]).ToString();
            string username = GetPlayerLogin(index);
            long id = GetStringHash(username);

            if (RowExists(id, "account"))
            {
                UpdateRowByColumn("id", id, "data", json, "account");
            }
            else
            {
                InsertRowByColumn(id, json, "account", "data", "id");
            }
        }

        public static void RegisterAccount(int index, string username, string password)
        {
            SetPlayerLogin(index, username);
            SetPlayerPassword(index, password);

            string json = JsonConvert.SerializeObject(Core.Type.Account[index]).ToString();

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

            var accountData = JObject.FromObject(data).ToObject<AccountStruct>();
            Core.Type.Account[index] = accountData;
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

            Core.Type.TempPlayer[index].SkillCD = new int[Core.Constant.MAX_PLAYER_SKILLS];
            Core.Type.TempPlayer[index].PetSkillCD = new int[Core.Constant.MAX_PET_SKILLS];
            Core.Type.TempPlayer[index].TradeOffer = new PlayerInvStruct[Core.Constant.MAX_INV];

            Core.Type.TempPlayer[index].SkillCD = new int[Core.Constant.MAX_PLAYER_SKILLS];
            Core.Type.TempPlayer[index].PetSkillCD = new int[Core.Constant.MAX_PET_SKILLS];
            Core.Type.TempPlayer[index].Editor = -1;
            Core.Type.TempPlayer[index].SkillBuffer = -1;
            Core.Type.TempPlayer[index].InShop = -1;
            Core.Type.TempPlayer[index].InTrade = -1;

            for (int i = 0, loopTo = Core.Type.TempPlayer[index].EventProcessingCount; i < loopTo; i++)
                Core.Type.TempPlayer[index].EventProcessing[i].EventId = -1;

            ClearCharacter(index);
        }

        #endregion

        #region Bank
        public static void LoadBank(int index)
        {
            JObject data;
            data = SelectRowByColumn("id", GetStringHash(GetPlayerLogin(index)), "account", "bank");

            if (data is null)
            {
                ClearBank(index);
                return;
            }

            var bankData = JObject.FromObject(data).ToObject<BankStruct>();
            Bank[index] = bankData;
        }

        public static void SaveBank(int index)
        {
            string json = JsonConvert.SerializeObject(Bank[index]);
            string username = GetPlayerLogin(index);
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
            Bank[index].Item = new PlayerInvStruct[Core.Constant.MAX_BANK + 1];
            for (int i = 0; i < Core.Constant.MAX_BANK; i++)
            {
                Bank[index].Item[i].Num = -1;
                Bank[index].Item[i].Value = 0;
            }
        }

        #endregion

        #region Characters
        public static void ClearCharacter(int index)
        {
            Core.Type.Player[index].Name = "";
            Core.Type.Player[index].Job = 0;
            Core.Type.Player[index].Dir = 0;
            Core.Type.Player[index].Access = (byte)AccessType.Player;

            Core.Type.Player[index].Equipment = new int[(byte)EquipmentType.Count];
            for (int i = 0, loopTo = (byte)EquipmentType.Count; i < loopTo; i++)
                Core.Type.Player[index].Equipment[i] = -1;

            Core.Type.Player[index].Inv = new Core.Type.PlayerInvStruct[Core.Constant.MAX_INV];
            for (int i = 0, loopTo1 = Core.Constant.MAX_INV; i < loopTo1; i++)
            {
                Core.Type.Player[index].Inv[i].Num = -1;
                Core.Type.Player[index].Inv[i].Value = 0;
            }

            Core.Type.Player[index].Exp = 0;
            Core.Type.Player[index].Level = 0;
            Core.Type.Player[index].Map = 0;
            Core.Type.Player[index].Name = "";
            Core.Type.Player[index].Pk = 0;
            Core.Type.Player[index].Points = 0;
            Core.Type.Player[index].Sex = 0;

            Core.Type.Player[index].Skill = new Core.Type.PlayerSkillStruct[Core.Constant.MAX_PLAYER_SKILLS];
            for (int i = 0, loopTo2 = Core.Constant.MAX_PLAYER_SKILLS; i < loopTo2; i++)
            {
                Core.Type.Player[index].Skill[i].Num = -1;
                Core.Type.Player[index].Skill[i].CD = 0;
            }

            Core.Type.Player[index].Sprite = 0;

            Core.Type.Player[index].Stat = new byte[(byte)StatType.Count];
            for (int i = 0, loopTo3 = (byte)StatType.Count; i < loopTo3; i++)
                Core.Type.Player[index].Stat[i] = 0;

            Core.Type.Player[index].Vital = new int[(byte) VitalType.Count];
            for (int i = 0, loopTo4 = (byte) VitalType.Count; i < loopTo4; i++)
                Core.Type.Player[index].Vital[i] = 0;

            Core.Type.Player[index].X = 0;
            Core.Type.Player[index].Y = 0;

            Core.Type.Player[index].Hotbar = new Core.Type.HotbarStruct[Core.Constant.MAX_HOTBAR];
            for (int i = 0, loopTo5 = Core.Constant.MAX_HOTBAR; i < loopTo5; i++)
            {
                Core.Type.Player[index].Hotbar[i].Slot = -1;
                Core.Type.Player[index].Hotbar[i].SlotType = 0;
            }

            Core.Type.Player[index].Switches = new byte[Core.Constant.MAX_SWITCHES];
            for (int i = 0, loopTo6 = Core.Constant.MAX_SWITCHES; i < loopTo6; i++)
                Core.Type.Player[index].Switches[i] = 0;

            Core.Type.Player[index].Variables = new int[Core.Constant.NAX_VARIABLES];
            for (int i = 0, loopTo7 = Core.Constant.NAX_VARIABLES; i < loopTo7; i++)
                Core.Type.Player[index].Variables[i] = 0;

            Core.Type.Player[index].GatherSkills = new Core.Type.ResourceTypetruct[(byte)ResourceType.Count];
            for (int i = 0, loopTo8 = (byte)ResourceType.Count; i < loopTo8; i++)
            {
                Core.Type.Player[index].GatherSkills[i].SkillLevel = 0;
                Core.Type.Player[index].GatherSkills[i].SkillCurExp = 0;
                SetPlayerGatherSkillMaxExp(index, i, GetSkillNextLevel(index, i));
            }

            for (int i = 0, loopTo9 = (byte)EquipmentType.Count; i < loopTo9; i++)
                Core.Type.Player[index].Equipment[i] = -1;

            Core.Type.Player[index].Pet.Num = 0;
            Core.Type.Player[index].Pet.Health = 0;
            Core.Type.Player[index].Pet.Mana = 0;
            Core.Type.Player[index].Pet.Level = 0;

            Core.Type.Player[index].Pet.Stat = new byte[(byte)StatType.Count];

            for (int i = 0, loopTo10 = (byte)StatType.Count; i < loopTo10; i++)
                Core.Type.Player[index].Pet.Stat[i] = 0;

            Core.Type.Player[index].Pet.Skill = new int[Core.Constant.MAX_PET_SKILLS];
            for (int i = 0; i < Core.Constant.MAX_PET_SKILLS; i++)
                Core.Type.Player[index].Pet.Skill[i] = -1;

            Core.Type.Player[index].Pet.Num = -1;
            Core.Type.Player[index].Pet.X = 0;
            Core.Type.Player[index].Pet.Y = 0;
            Core.Type.Player[index].Pet.Dir = 0;
            Core.Type.Player[index].Pet.Alive = 0;
            Core.Type.Player[index].Pet.AttackBehaviour = 0;
            Core.Type.Player[index].Pet.AdoptiveStats = 0;
            Core.Type.Player[index].Pet.Points = 0;
            Core.Type.Player[index].Pet.Exp = 0;
        }

        public static bool LoadCharacter(int index, int charNum)
        {
            JObject data;
            data = SelectRowByColumn("id", GetStringHash(GetPlayerLogin(index)), "account", "character" + charNum.ToString());

            if (data is null)
            {
                return false;
            }

            var characterData = JObject.FromObject(data).ToObject<PlayerStruct>();

            if (characterData.Name == "")
            {
                return false;
            }

            Core.Type.Player[index] = characterData;
            return true;
        }

        public static void SaveCharacter(int index, int slot)
        {
            string json = JsonConvert.SerializeObject(Core.Type.Player[index]).ToString();
            long id = GetStringHash(GetPlayerLogin(index));

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

            if (Strings.Len(Core.Type.Player[index].Name) == 0)
            {
                Core.Type.Player[index].Name = name;
                Core.Type.Player[index].Sex = Sex;
                Core.Type.Player[index].Job = jobNum;
                Core.Type.Player[index].Sprite = sprite;
                Core.Type.Player[index].Level = 1;

                var loopTo = (byte)StatType.Count;
                for (n = 0; n < loopTo; n++)
                    Core.Type.Player[index].Stat[n] = (byte)Core.Type.Job[jobNum].Stat[n];

                Core.Type.Player[index].Dir = (byte) DirectionType.Down;
                Core.Type.Player[index].Map = Core.Type.Job[jobNum].StartMap;

                if (Core.Type.Player[index].Map == 0)
                    Core.Type.Player[index].Map = 1;

                Core.Type.Player[index].X = Core.Type.Job[jobNum].StartX;
                Core.Type.Player[index].Y = Core.Type.Job[jobNum].StartY;
                Core.Type.Player[index].Dir = (byte) DirectionType.Down;

                var loopTo1 = VitalType.Count;
                for (i = 0; i < (int)loopTo1; i++)
                    SetPlayerVital(index, (VitalType)i, GetPlayerMaxVital(index, (VitalType)i));

                // set starter equipment
                for (n = 0; n < Core.Constant.MAX_START_ITEMS; n++)
                {
                    if (Core.Type.Job[jobNum].StartItem[n] > 0)
                    {
                        Core.Type.Player[index].Inv[n].Num = Core.Type.Job[jobNum].StartItem[n];
                        Core.Type.Player[index].Inv[n].Value = Core.Type.Job[jobNum].StartValue[n];
                    }
                }

                // set skills
                var loopTo2 = ResourceType.Count;
                for (i = 0; i < (int)loopTo2; i++)
                {
                    Core.Type.Player[index].GatherSkills[i].SkillLevel = 0;
                    Core.Type.Player[index].GatherSkills[i].SkillCurExp = 0;
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
            if (!File.Exists(filename))
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

            Core.Type.Account[BanPlayerindex].Banned = Conversions.ToBoolean(1);

            IP = Strings.Mid(IP, 1, i);
            Core.Log.AddTextToFile(IP, "banlist.txt");
            NetworkSend.GlobalMsg(GetPlayerName(BanPlayerindex) + " has been banned from " + Settings.Instance.GameName + " by " + "the Server" + "!");
            Core.Log.Add("The Server" + " has banned " + GetPlayerName(BanPlayerindex) + ".", Constant.ADMIN_LOG);
            NetworkSend.AlertMsg(BanPlayerindex, (int)DialogueMsg.Banned);
        }

        public static bool IsBanned(int index, string IP)
        {
            bool IsBannedRet = default;
            string filename;
            string line;

            filename = Path.Combine(Core.Path.Database, "banlist.txt");

            // Check if file exists
            if (!File.Exists(filename))
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
                    IsBannedRet = Conversions.ToBoolean(1);
                }
            }

            sr.Close();

            if (Core.Type.Account[index].Banned)
            {
                IsBannedRet = Conversions.ToBoolean(1);
            }

            return IsBannedRet;

        }

        public static void BanPlayer(int BanPlayerindex, int BannedByindex)
        {
            string filename = Path.Combine(Core.Path.Database, "banlist.txt");
            string IP;
            int i;

            // Make sure the file exists
            if (!File.Exists(filename))
                File.Create(filename).Dispose();

            // Cut off last portion of ip
            IP = NetworkConfig.Socket.ClientIP(BanPlayerindex);

            for (i = Strings.Len(IP); i >= 0; i -= 1)
            {

                if (Strings.Mid(IP, i, 1) == ".")
                {
                    break;
                }

            }

            Core.Type.Account[BanPlayerindex].Banned = true;

            IP = Strings.Mid(IP, 1, i);
            Core.Log.AddTextToFile(IP, "banlist.txt");
            NetworkSend.GlobalMsg(GetPlayerName(BanPlayerindex) + " has been banned from " + Settings.Instance.GameName + " by " + GetPlayerName(BannedByindex) + "!");
            Core.Log.Add(GetPlayerName(BannedByindex) + " has banned " + GetPlayerName(BanPlayerindex) + ".", Constant.ADMIN_LOG);
            NetworkSend.AlertMsg(BanPlayerindex, (int)DialogueMsg.Banned);
        }

        #endregion

        #region Data Functions
        public static byte[] JobData(int jobNum)
        {
            int n;
            int q;
            var buffer = new ByteStream(4);

            buffer.WriteString(Core.Type.Job[jobNum].Name);
            buffer.WriteString(Core.Type.Job[jobNum].Desc);

            buffer.WriteInt32(Core.Type.Job[jobNum].MaleSprite);
            buffer.WriteInt32(Core.Type.Job[jobNum].FemaleSprite);

            for (int i = 0, loopTo = (byte)StatType.Count; i < loopTo; i++)
                buffer.WriteInt32(Core.Type.Job[jobNum].Stat[i]);

            for (q = 0; q < Core.Constant.MAX_START_ITEMS; q++)
            {
                buffer.WriteInt32(Core.Type.Job[jobNum].StartItem[q]);
                buffer.WriteInt32(Core.Type.Job[jobNum].StartValue[q]);
            }

            buffer.WriteInt32(Core.Type.Job[jobNum].StartMap);
            buffer.WriteByte(Core.Type.Job[jobNum].StartX);
            buffer.WriteByte(Core.Type.Job[jobNum].StartY);
            buffer.WriteInt32(Core.Type.Job[jobNum].BaseExp);

            return buffer.ToArray();
        }

        public static byte[] NPCsData()
        {
            var buffer = new ByteStream(4);

            for (int i = 0, loopTo = Core.Constant.MAX_NPCS; i < loopTo; i++)
            {
                if (!(Strings.Len(Core.Type.NPC[i].Name) > 0))
                    continue;
                buffer.WriteBlock(NPCData(i));
            }
            return buffer.ToArray();
        }

        public static byte[] NPCData(int NPCNum)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32(NPCNum);
            buffer.WriteInt32(Core.Type.NPC[(int)NPCNum].Animation);
            buffer.WriteString(Core.Type.NPC[(int)NPCNum].AttackSay);
            buffer.WriteByte(Core.Type.NPC[(int)NPCNum].Behaviour);

            for (int i = 0, loopTo = Core.Constant.MAX_DROP_ITEMS; i < loopTo; i++)
            {
                buffer.WriteInt32(Core.Type.NPC[(int)NPCNum].DropChance[i]);
                buffer.WriteInt32(Core.Type.NPC[(int)NPCNum].DropItem[i]);
                buffer.WriteInt32(Core.Type.NPC[(int)NPCNum].DropItemValue[i]);
            }

            buffer.WriteInt32(Core.Type.NPC[(int)NPCNum].Exp);
            buffer.WriteByte(Core.Type.NPC[(int)NPCNum].Faction);
            buffer.WriteInt32(Core.Type.NPC[(int)NPCNum].HP);
            buffer.WriteString(Core.Type.NPC[(int)NPCNum].Name);
            buffer.WriteByte(Core.Type.NPC[(int)NPCNum].Range);
            buffer.WriteByte(Core.Type.NPC[(int)NPCNum].SpawnTime);
            buffer.WriteInt32(Core.Type.NPC[(int)NPCNum].SpawnSecs);
            buffer.WriteInt32(Core.Type.NPC[(int)NPCNum].Sprite);

            for (int i = 0, loopTo1 = (byte)StatType.Count; i < loopTo1; i++)
                buffer.WriteByte(Core.Type.NPC[(int)NPCNum].Stat[i]);

            for (int i = 0, loopTo2 = Core.Constant.MAX_NPC_SKILLS; i < loopTo2; i++)
                buffer.WriteByte(Core.Type.NPC[(int)NPCNum].Skill[i]);

            buffer.WriteInt32(Core.Type.NPC[(int)NPCNum].Level);
            buffer.WriteInt32(Core.Type.NPC[(int)NPCNum].Damage);
            return buffer.ToArray();
        }

        public static byte[] ShopsData()
        {
            var buffer = new ByteStream(4);

            for (int i = 0, loopTo = Core.Constant.MAX_SHOPS; i < loopTo; i++)
            {
                if (!(Strings.Len(Core.Type.Shop[i].Name) > 0))
                    continue;
                buffer.WriteBlock(ShopData(i));
            }
            return buffer.ToArray();
        }

        public static byte[] ShopData(int shopNum)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32(shopNum);
            buffer.WriteInt32(Core.Type.Shop[shopNum].BuyRate);
            buffer.WriteString(Core.Type.Shop[shopNum].Name);

            for (int i = 0, loopTo = Core.Constant.MAX_TRADES; i < loopTo; i++)
            {
                buffer.WriteInt32(Core.Type.Shop[shopNum].TradeItem[i].CostItem);
                buffer.WriteInt32(Core.Type.Shop[shopNum].TradeItem[i].CostValue);
                buffer.WriteInt32(Core.Type.Shop[shopNum].TradeItem[i].Item);
                buffer.WriteInt32(Core.Type.Shop[shopNum].TradeItem[i].ItemValue);
            }
            return buffer.ToArray();
        }

        public static byte[] SkillsData()
        {
            var buffer = new ByteStream(4);

            for (int i = 0, loopTo = Core.Constant.MAX_SKILLS; i < loopTo; i++)
            {
                if (!(Strings.Len(Core.Type.Skill[i].Name) > 0))
                    continue;
                buffer.WriteBlock(SkillData(i));
            }
            return buffer.ToArray();
        }

        public static byte[] SkillData(int skillNum)
        {
            var buffer = new ByteStream(4);

            buffer.WriteInt32(skillNum);
            buffer.WriteInt32(Core.Type.Skill[skillNum].AccessReq);
            buffer.WriteInt32(Core.Type.Skill[skillNum].AoE);
            buffer.WriteInt32(Core.Type.Skill[skillNum].CastAnim);
            buffer.WriteInt32(Core.Type.Skill[skillNum].CastTime);
            buffer.WriteInt32(Core.Type.Skill[skillNum].CdTime);
            buffer.WriteInt32(Core.Type.Skill[skillNum].JobReq);
            buffer.WriteInt32(Core.Type.Skill[skillNum].Dir);
            buffer.WriteInt32(Core.Type.Skill[skillNum].Duration);
            buffer.WriteInt32(Core.Type.Skill[skillNum].Icon);
            buffer.WriteInt32(Core.Type.Skill[skillNum].Interval);
            buffer.WriteInt32(Conversions.ToInteger(Core.Type.Skill[skillNum].IsAoE));
            buffer.WriteInt32(Core.Type.Skill[skillNum].LevelReq);
            buffer.WriteInt32(Core.Type.Skill[skillNum].Map);
            buffer.WriteInt32(Core.Type.Skill[skillNum].MpCost);
            buffer.WriteString(Core.Type.Skill[skillNum].Name);
            buffer.WriteInt32(Core.Type.Skill[skillNum].Range);
            buffer.WriteInt32(Core.Type.Skill[skillNum].SkillAnim);
            buffer.WriteInt32(Core.Type.Skill[skillNum].StunDuration);
            buffer.WriteInt32(Core.Type.Skill[skillNum].Type);
            buffer.WriteInt32(Core.Type.Skill[skillNum].Vital);
            buffer.WriteInt32(Core.Type.Skill[skillNum].X);
            buffer.WriteInt32(Core.Type.Skill[skillNum].Y);
            buffer.WriteInt32(Core.Type.Skill[skillNum].IsProjectile);
            buffer.WriteInt32(Core.Type.Skill[skillNum].Projectile);
            buffer.WriteInt32(Core.Type.Skill[skillNum].KnockBack);
            buffer.WriteInt32(Core.Type.Skill[skillNum].KnockBackTiles);
            return buffer.ToArray();
        }

        #endregion

    }
}