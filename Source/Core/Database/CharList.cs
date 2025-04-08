using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;

namespace Core.Database
{
    /// <summary>
    /// A thread-safe, case-insensitive collection of character names with associated data and advanced functionality.
    /// Supports insertion order preservation, character data management, serialization, querying, and more.
    /// </summary>
    public class CharList : IEnumerable<string>
    {
        private readonly HashSet<string> _names;
        private readonly List<string> _orderedNames;
        private readonly Dictionary<string, CharacterData> _characterData;
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        #region Constructors

        public CharList()
        {
            _names = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            _orderedNames = new List<string>();
            _characterData = new Dictionary<string, CharacterData>(StringComparer.OrdinalIgnoreCase);
        }

        public CharList(IEnumerable<string> initialNames) : this()
        {
            ArgumentNullException.ThrowIfNull(initialNames, nameof(initialNames));
            AddRange(initialNames);
        }

        public CharList(IEnumerable<(string Name, CharacterData Data)> initialData) : this()
        {
            ArgumentNullException.ThrowIfNull(initialData, nameof(initialData));
            AddRange(initialData);
        }

        #endregion

        #region Properties

        public int Count => ExecuteRead(() => _names.Count);

        public IReadOnlyList<string> OrderedNames => ExecuteRead(() => _orderedNames.AsReadOnly());

        public IReadOnlyList<string> SortedNames => ExecuteRead(() => 
            _names.Order(StringComparer.OrdinalIgnoreCase).ToList().AsReadOnly());

        public IReadOnlyCollection<string> Names => ExecuteRead(() => _names.ToArray());

        public bool IsEmpty => ExecuteRead(() => _names.Count == 0);

        public CharacterData this[string name]
        {
            get => ExecuteRead(() => _names.Contains(name)
                ? _characterData[name]
                : throw new KeyNotFoundException($"Name '{name}' not found."));
            set => ExecuteWrite(() =>
            {
                if (!_names.Contains(name))
                    throw new KeyNotFoundException($"Name '{name}' not found.");
                _characterData[name] = value ?? throw new ArgumentNullException(nameof(value));
            });
        }

        #endregion

        #region Core Methods

        public bool Contains(string name) => !string.IsNullOrWhiteSpace(name) && ExecuteRead(() => _names.Contains(name));

        public OperationResult Add(string name) => Add(name, new CharacterData());

        public OperationResult Add(string name, CharacterData data)
        {
            if (!IsValidName(name))
                return new OperationResult { Success = false, Message = "Invalid name: 3-20 characters, not null/whitespace." };
            if (data == null)
                return new OperationResult { Success = false, Message = "Character data cannot be null." };

            return ExecuteWrite(() =>
            {
                if (_names.Add(name))
                {
                    _orderedNames.Add(name);
                    _characterData[name] = data;
                    return new OperationResult { Success = true, Message = $"Added '{name}' successfully." };
                }
                return new OperationResult { Success = false, Message = $"Name '{name}' already exists." };
            });
        }

        public List<string> AddRange(IEnumerable<string> names)
        {
            ArgumentNullException.ThrowIfNull(names, nameof(names));
            return AddRange(names.Select(n => (n, new CharacterData())));
        }

        public List<string> AddRange(IEnumerable<(string Name, CharacterData Data)> items)
        {
            ArgumentNullException.ThrowIfNull(items, nameof(items));
            var failed = new List<string>();
            ExecuteWrite(() =>
            {
                foreach (var (name, data) in items)
                {
                    if (!IsValidName(name) || data == null || !_names.Add(name))
                    {
                        failed.Add(name);
                        continue;
                    }
                    _orderedNames.Add(name);
                    _characterData[name] = data;
                }
            });
            return failed;
        }

        public OperationResult Remove(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return new OperationResult { Success = false, Message = "Name cannot be null or whitespace." };

            return ExecuteWrite(() =>
            {
                if (_names.Remove(name))
                {
                    _orderedNames.Remove(name);
                    _characterData.Remove(name);
                    return new OperationResult { Success = true, Message = $"Removed '{name}' successfully." };
                }
                return new OperationResult { Success = false, Message = $"Name '{name}' not found." };
            });
        }

        public CharList Clear() => ExecuteWrite(() =>
        {
            _names.Clear();
            _orderedNames.Clear();
            _characterData.Clear();
            return this;
        });

        public CharList Merge(CharList other)
        {
            if (other == null) return this;
            ExecuteWrite(() =>
            {
                other.ExecuteRead(() =>
                {
                    foreach (var name in other._names)
                    {
                        if (_names.Add(name))
                        {
                            _orderedNames.Add(name);
                            _characterData[name] = other._characterData[name];
                        }
                    }
                });
            });
            return this;
        }

        #endregion

        #region Character Data Management

        public CharacterData? GetCharacterData(string name) => 
            ExecuteRead(() => _characterData.TryGetValue(name, out var data) ? data : null);

        public OperationResult SetCharacterData(string name, CharacterData data)
        {
            if (string.IsNullOrWhiteSpace(name))
                return new OperationResult { Success = false, Message = "Name cannot be null or whitespace." };
            if (data == null)
                return new OperationResult { Success = false, Message = "Character data cannot be null." };

            return ExecuteWrite(() =>
            {
                if (_names.Contains(name))
                {
                    _characterData[name] = data;
                    return new OperationResult { Success = true, Message = $"Updated data for '{name}'." };
                }
                return new OperationResult { Success = false, Message = $"Name '{name}' not found." };
            });
        }

        public bool TryGetCharacterData(string name, out CharacterData? data)
        {
            var result = ExecuteRead(() => _characterData.TryGetValue(name, out data));
            data ??= null; // Ensure out parameter is assigned even if TryGetValue fails
            return result;
        }

        public IReadOnlyList<KeyValuePair<string, CharacterData>> GetAllCharacterData() => 
            ExecuteRead(() => _characterData.ToList().AsReadOnly());

        #endregion

        #region Querying Methods

        public IReadOnlyList<string> FindNamesStartingWith(string prefix) => 
            string.IsNullOrEmpty(prefix)
                ? Array.Empty<string>().AsReadOnly()
                : ExecuteRead(() => _orderedNames
                    .Where(n => n.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    .ToList()
                    .AsReadOnly());

        public IReadOnlyList<string> FindNamesContaining(string substring) => 
            string.IsNullOrEmpty(substring)
                ? Array.Empty<string>().AsReadOnly()
                : ExecuteRead(() => _orderedNames
                    .Where(n => n.Contains(substring, StringComparison.OrdinalIgnoreCase))
                    .ToList()
                    .AsReadOnly());

        public IReadOnlyList<string> FindNames(Func<CharacterData, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(predicate, nameof(predicate));
            return ExecuteRead(() => _characterData
                .Where(kvp => predicate(kvp.Value))
                .Select(kvp => kvp.Key)
                .ToList()
                .AsReadOnly());
        }

        #endregion

        #region Serialization

        public string ToJson() => ExecuteRead(() => JsonSerializer.Serialize(new SerializableData
        {
            Names = _orderedNames.ToList(),
            CharacterData = _characterData.ToDictionary(kvp => kvp.Key, kvp => kvp.Value)
        }));

        public static CharList FromJson(string json)
        {
            ArgumentNullException.ThrowIfNull(json, nameof(json));
            var data = JsonSerializer.Deserialize<SerializableData>(json) 
                ?? throw new JsonException("Deserialized data is null.");
            return new CharList(data.Names.Select(n => (n, data.CharacterData.GetValueOrDefault(n, new CharacterData()))));
        }

        #endregion

        #region Validation

        protected virtual bool IsValidName(string name) => 
            !string.IsNullOrWhiteSpace(name) && name.Length >= 3 && name.Length <= 20;

        #endregion

        #region IEnumerable Implementation

        public IEnumerator<string> GetEnumerator() => ExecuteRead(() => _orderedNames.ToList().GetEnumerator());

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion

        #region Synchronization Helpers

        private T ExecuteRead<T>(Func<T> action)
        {
            _lock.EnterReadLock();
            try
            {
                return action();
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        private T ExecuteWrite<T>(Func<T> action)
        {
            _lock.EnterWriteLock();
            try
            {
                return action();
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        private void ExecuteWrite(Action action)
        {
            _lock.EnterWriteLock();
            try
            {
                action();
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        #endregion

        #region Nested Classes

        private class SerializableData
        {
            public List<string> Names { get; set; } = new();
            public Dictionary<string, CharacterData> CharacterData { get; set; } = new(StringComparer.OrdinalIgnoreCase);
        }

        #endregion
    }

    public class CharacterData
    {
        public int Level { get; set; }
        public string? Class { get; set; }
    }

    public class OperationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
