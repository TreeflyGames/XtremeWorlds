using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Mirage.Sharp.Asfw;
using static Core.Enum;
using static Core.Packets;
using static Core.Type;
using static Core.Global.Command;

namespace Server
{
    public class MoralService : IDisposable
    {
        private readonly IMemoryCache _cache;
        private readonly ILogger<MoralService> _logger;
        private readonly IDatabaseProvider _database;
        private readonly INetworkService _network;
        private readonly ConcurrentDictionary<int, MoralStruct> _moralCache;
        private readonly object _syncLock = new object();
        private bool _disposed;

        #region Nested Types
        public class MoralStruct
        {
            public string Name { get; set; }
            public byte Color { get; set; }
            public bool CanCast { get; set; }
            public bool CanDropItem { get; set; }
            public bool CanPK { get; set; }
            public bool CanPickupItem { get; set; }
            public bool CanUseItem { get; set; }
            public bool DropItems { get; set; }
            public bool LoseExp { get; set; }
            public bool NPCBlock { get; set; }
            public bool PlayerBlock { get; set; }
            public Dictionary<string, int> CustomAttributes { get; set; } = new();
            public DateTime LastModified { get; set; }
            public string Description { get; set; }
            public List<MoralPermission> Permissions { get; set; } = new();
        }

        public class MoralPermission
        {
            public string PermissionType { get; set; }
            public bool IsAllowed { get; set; }
            public Dictionary<string, string> Conditions { get; set; } = new();
        }

        public class MoralValidationResult
        {
            public bool IsValid { get; set; }
            public List<string> ValidationErrors { get; set; } = new();
        }
        #endregion

        #region Constructor and Initialization
        public MoralService(
            IMemoryCache cache,
            ILogger<MoralService> logger,
            IDatabaseProvider database,
            INetworkService network)
        {
            _cache = cache;
            _logger = logger;
            _database = database;
            _network = network;
            _moralCache = new ConcurrentDictionary<int, MoralStruct>();
        }

        public async Task InitializeAsync()
        {
            try
            {
                await LoadAllMoralsAsync();
                _logger.LogInformation("Moral Service initialized successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to initialize Moral Service");
                throw;
            }
        }
        #endregion

        #region Database Operations
        public void ClearMoral(int moralNum)
        {
            var defaultMoral = new MoralStruct
            {
                Name = string.Empty,
                LastModified = DateTime.UtcNow
            };
            
            _moralCache[moralNum] = defaultMoral;
            _cache.Set($"moral_{moralNum}", defaultMoral, TimeSpan.FromHours(1));
        }

        public async Task<MoralStruct> LoadMoralAsync(int moralNum)
        {
            if (_moralCache.TryGetValue(moralNum, out var cachedMoral))
            {
                return cachedMoral;
            }

            try
            {
                var data = await _database.SelectRowAsync(moralNum, "moral", "data");
                if (data == null)
                {
                    ClearMoral(moralNum);
                    return _moralCache[moralNum];
                }

                var moralData = JsonConvert.DeserializeObject<MoralStruct>(data.ToString());
                moralData.LastModified = DateTime.UtcNow;
                
                _moralCache[moralNum] = moralData;
                _cache.Set($"moral_{moralNum}", moralData, GetCacheOptions());
                
                return moralData;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to load moral {moralNum}");
                throw;
            }
        }

        public async Task LoadAllMoralsAsync()
        {
            var tasks = Enumerable.Range(0, Core.Constant.MAX_MORALS)
                .Select(i => LoadMoralAsync(i));
            await Task.WhenAll(tasks);
        }

        public async Task SaveMoralAsync(int moralNum)
        {
            if (!_moralCache.TryGetValue(moralNum, out var moral))
            {
                return;
            }

            try
            {
                moral.LastModified = DateTime.UtcNow;
                string json = JsonConvert.SerializeObject(moral, Formatting.Indented);
                
                if (await _database.RowExistsAsync(moralNum, "moral"))
                {
                    await _database.UpdateRowAsync(moralNum, json, "moral", "data");
                }
                else
                {
                    await _database.InsertRowAsync(moralNum, json, "moral");
                }

                _cache.Set($"moral_{moralNum}", moral, GetCacheOptions());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to save moral {moralNum}");
                throw;
            }
        }
        #endregion

        #region Packet Operations
        public byte[] SerializeMoralData(int moralNum)
        {
            if (!_moralCache.TryGetValue(moralNum, out var moral))
            {
                return Array.Empty<byte>();
            }

            using var buffer = new ByteStream();
            buffer.WriteInt32(moralNum);
            buffer.WriteString(moral.Name);
            buffer.WriteByte(moral.Color);
            buffer.WriteBoolean(moral.NPCBlock);
            buffer.WriteBoolean(moral.PlayerBlock);
            buffer.WriteBoolean(moral.DropItems);
            buffer.WriteBoolean(moral.CanCast);
            buffer.WriteBoolean(moral.CanDropItem);
            buffer.WriteBoolean(moral.CanPickupItem);
            buffer.WriteBoolean(moral.CanPK);
            buffer.WriteBoolean(moral.LoseExp);
            buffer.WriteString(moral.Description);
            buffer.WriteInt32(moral.CustomAttributes.Count);
            
            foreach (var attr in moral.CustomAttributes)
            {
                buffer.WriteString(attr.Key);
                buffer.WriteInt32(attr.Value);
            }

            return buffer.ToArray();
        }

        public async Task SendMoralsToPlayerAsync(int playerIndex)
        {
            var moralsToSend = _moralCache
                .Where(m => !string.IsNullOrEmpty(m.Value.Name))
                .Select(m => m.Key);

            foreach (var moralNum in moralsToSend)
            {
                await SendUpdateMoralToAsync(playerIndex, moralNum);
            }
        }

        public async Task SendUpdateMoralToAsync(int playerIndex, int moralNum)
        {
            var data = SerializeMoralData(moralNum);
            await _network.SendDataToAsync(playerIndex, 
                ServerPackets.SUpdateMoral, 
                data);
        }

        public async Task BroadcastMoralUpdateAsync(int moralNum)
        {
            var data = SerializeMoralData(moralNum);
            await _network.BroadcastAsync(
                ServerPackets.SUpdateMoral,
                data);
        }
        #endregion

        #region Packet Handlers
        public async Task HandleEditMoralRequestAsync(int playerIndex, byte[] data)
        {
            if (!await ValidatePlayerAccessAsync(playerIndex, AccessType.Developer))
            {
                return;
            }

            if (Core.Type.TempPlayer[playerIndex].Editor > 0)
            {
                await _network.SendPlayerMessageAsync(playerIndex,
                    "Editor already in use",
                    ColorType.BrightRed);
                return;
            }

            var editorLock = await CheckEditorLockAsync(playerIndex, EditorType.Moral);
            if (!string.IsNullOrEmpty(editorLock))
            {
                await _network.SendPlayerMessageAsync(playerIndex,
                    $"Editor locked by {editorLock}",
                    ColorType.BrightRed);
                return;
            }

            await SendMoralsToPlayerAsync(playerIndex);
            Core.Type.TempPlayer[playerIndex].Editor = (byte)EditorType.Moral;
            
            await _network.SendDataToAsync(playerIndex,
                ServerPackets.SMoralEditor,
                Array.Empty<byte>());
        }

        public async Task HandleSaveMoralAsync(int playerIndex, byte[] data)
        {
            if (!await ValidatePlayerAccessAsync(playerIndex, AccessType.Developer))
            {
                return;
            }

            using var buffer = new ByteStream(data);
            var moralNum = buffer.ReadInt32();

            if (!IsValidMoralNumber(moralNum))
            {
                return;
            }

            var moral = DeserializeMoral(buffer);
            var validation = ValidateMoral(moral);
            
            if (!validation.IsValid)
            {
                await SendValidationErrorsAsync(playerIndex, validation);
                return;
            }

            _moralCache[moralNum] = moral;
            await Task.WhenAll(
                BroadcastMoralUpdateAsync(moralNum),
                SaveMoralAsync(moralNum),
                LogMoralUpdateAsync(playerIndex, moralNum)
            );

            await SendMoralsToPlayerAsync(playerIndex);
        }
        #endregion

        #region Validation and Utilities
        private MoralValidationResult ValidateMoral(MoralStruct moral)
        {
            var result = new MoralValidationResult { IsValid = true };
            
            if (string.IsNullOrWhiteSpace(moral.Name))
            {
                result.IsValid = false;
                result.ValidationErrors.Add("Name cannot be empty");
            }

            if (moral.Name?.Length > 50)
            {
                result.IsValid = false;
                result.ValidationErrors.Add("Name too long (max 50 chars)");
            }

            return result;
        }

        private bool IsValidMoralNumber(int moralNum) =>
            moralNum >= 0 && moralNum <= Core.Constant.MAX_MORALS;

        private MemoryCacheEntryOptions GetCacheOptions() =>
            new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromHours(1))
                .SetPriority(CacheItemPriority.High);

        private MoralStruct DeserializeMoral(ByteStream buffer)
        {
            return new MoralStruct
            {
                Name = buffer.ReadString(),
                Color = buffer.ReadByte(),
                CanCast = buffer.ReadBoolean(),
                CanPK = buffer.ReadBoolean(),
                CanDropItem = buffer.ReadBoolean(),
                CanPickupItem = buffer.ReadBoolean(),
                CanUseItem = buffer.ReadBoolean(),
                DropItems = buffer.ReadBoolean(),
                LoseExp = buffer.ReadBoolean(),
                PlayerBlock = buffer.ReadBoolean(),
                NPCBlock = buffer.ReadBoolean(),
                Description = buffer.ReadString()
            };
        }
        #endregion

        #region Cleanup
        public void Dispose()
        {
            if (!_disposed)
            {
                _moralCache.Clear();
                _disposed = true;
            }
        }
        #endregion
    }
}
