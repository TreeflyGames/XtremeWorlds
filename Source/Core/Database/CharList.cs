using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;

namespace Core.Database
{
    /// <summary>
    /// A thread-safe, case-insensitive collection of character names with associated data.
    /// Supports insertion order preservation, advanced querying, extensible character data,
    /// bulk operations, indexing, and serialization.
    /// </summary>
    public class CharList : IEnumerable<string>
    {
        private readonly HashSet<string> _names;
        private readonly List<string> _orderedNames;
        private readonly Dictionary<string, CharacterData> _characterData;
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        #region Constructors

        /// <summary>
        /// Initializes an empty CharList.
        /// </summary>
        public CharList()
        {
            _names = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            _orderedNames = new List<string>();
            _characterData = new Dictionary<string, CharacterData>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Initializes a CharList with a collection of names, each with default character data.
        /// </summary>
        public CharList(IEnumerable<string> initialNames) : this()
        {
            ArgumentNullException.ThrowIfNull(initialNames, nameof(initialNames));
            AddRange(initialNames);
        }

        /// <summary>
        /// Initializes a CharList with a collection of name and character data pairs.
        /// </summary>
        public CharList(IEnumerable<(string Name, CharacterData Data)> initialData) : this()
        {
            ArgumentNullException.ThrowIfNull(initialData, nameof(initialData));
            AddRange(initialData);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of characters in the list.
        /// </summary>
        public int Count => ExecuteRead(() => _names.Count);

        /// <summary>
        /// Gets the names in their insertion order.
        /// </summary>
        public IReadOnlyList<string> OrderedNames => ExecuteRead(() => _orderedNames.AsReadOnly());

        /// <summary>
        /// Gets the names sorted alphabetically (case-insensitive).
        /// </summary>
        public IReadOnlyList<string> SortedNames => ExecuteRead(() =>
            _names.Order(StringComparer.OrdinalIgnoreCase).ToList().AsReadOnly());

        /// <summary>
        /// Gets a read-only collection of all names.
        /// </summary>
        public IReadOnlyCollection<string> Names => ExecuteRead(() => _names.ToArray());

        /// <summary>
        /// Indicates whether the list is empty.
        /// </summary>
        public bool IsEmpty => ExecuteRead(() => _names.Count == 0);

        /// <summary>
        /// Gets or sets the character data for a given name.
        /// </summary>
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

        /// <summary>
        /// Checks if a name exists in the list.
        /// </summary>
        public bool Contains(string name) => !string.IsNullOrWhiteSpace(name) && ExecuteRead(() => _names.Contains(name));

        /// <summary>
        /// Adds a name with default character data.
        /// </summary>
        public OperationResult Add(string name) => Add(name, new CharacterData());

        /// <summary>
        /// Adds a name with specified character data.
        /// </summary>
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

        /// <summary>
        /// Adds multiple names with default character data, returning failed additions.
        /// </summary>
        public List<string> AddRange(IEnumerable<string> names)
        {
            ArgumentNullException.ThrowIfNull(names, nameof(names));
            return AddRange(names.Select(n => (n, new CharacterData())));
        }

        /// <summary>
        /// Adds multiple name-data pairs, returning failed additions.
        /// </summary>
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

        /// <summary>
        /// Removes a name and its associated data.
        /// </summary>
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

        /// <summary>
        /// Removes multiple names and returns the list of successfully removed names.
        /// </summary>
        public List<string> RemoveRange(IEnumerable<string> names)
        {
            ArgumentNullException.ThrowIfNull(names, nameof(names));
            var removed = new List<string>();
            ExecuteWrite(() =>
            {
                foreach (var name in names)
                {
                    if (_names.Remove(name))
                    {
                        _orderedNames.Remove(name);
                        _characterData.Remove(name);
                        removed.Add(name);
                    }
                }
            });
            return removed;
        }

        /// <summary>
        /// Clears all names and data from the list.
        /// </summary>
        public CharList Clear() => ExecuteWrite(() =>
        {
            _names.Clear();
            _orderedNames.Clear();
            _characterData.Clear();
            return this;
        });

        /// <summary>
        /// Merges another CharList into this one, optionally updating existing entries.
        /// </summary>
        public CharList Merge(CharList other, bool updateExisting = false)
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
                        else if (updateExisting)
                        {
                            _characterData[name] = other._characterData[name];
                        }
                    }
                });
            });
            return this;
        }

        #endregion

        #region Character Data Management

        /// <summary>
        /// Retrieves the character data for a name, or null if not found.
        /// </summary>
        public CharacterData? GetCharacterData(string name) =>
            ExecuteRead(() => _characterData.TryGetValue(name, out var data) ? data : null);

        /// <summary>
        /// Sets the character data for an existing name.
        /// </summary>
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

        /// <summary>
        /// Updates specific fields of character data for a name.
        /// </summary>
        public OperationResult UpdateCharacterData(string name, Action<CharacterData> updateAction)
        {
            if (string.IsNullOrWhiteSpace(name))
                return new OperationResult { Success = false, Message = "Name cannot be null or whitespace." };
            if (updateAction == null)
                return new OperationResult { Success = false, Message = "Update action cannot be null." };

            return ExecuteWrite(() =>
            {
                if (_characterData.TryGetValue(name, out var data))
                {
                    updateAction(data);
                    return new OperationResult { Success = true, Message = $"Updated data for '{name}'." };
                }
                return new OperationResult { Success = false, Message = $"Name '{name}' not found." };
            });
        }

        /// <summary>
        /// Tries to get the character data for a name, returning success status.
        /// </summary>
        public bool TryGetCharacterData(string name, out CharacterData? data)
        {
            var result = ExecuteRead(() => _characterData.TryGetValue(name, out data));
            data ??= null;
            return result;
        }

        /// <summary>
        /// Gets all name-data pairs in the list.
        /// </summary>
        public IReadOnlyList<KeyValuePair<string, CharacterData>> GetAllCharacterData() =>
            ExecuteRead(() => _characterData.ToList().AsReadOnly());

        #endregion

        #region Querying Methods

        /// <summary>
        /// Finds names based on a filter applied to character data, with optional sorting.
        /// </summary>
        public IReadOnlyList<string> FindNames(Func<CharacterData, bool> filter, Func<CharacterData, object>? sortKeySelector = null)
        {
            ArgumentNullException.ThrowIfNull(filter, nameof(filter));
            return ExecuteRead(() =>
            {
                var filtered = _characterData.Where(kvp => filter(kvp.Value));
                if (sortKeySelector != null)
                {
                    filtered = filtered.OrderBy(kvp => sortKeySelector(kvp.Value));
                }
                return filtered.Select(kvp => kvp.Key).ToList().AsReadOnly();
            });
        }

        /// <summary>
        /// Finds names that start with a given prefix.
        /// </summary>
        public IReadOnlyList<string> FindNamesStartingWith(string prefix) =>
            string.IsNullOrEmpty(prefix)
                ? Array.Empty<string>().AsReadOnly()
                : ExecuteRead(() => _orderedNames
                    .Where(n => n.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    .ToList()
                    .AsReadOnly());

        /// <summary>
        /// Finds names that contain a given substring.
        /// </summary>
        public IReadOnlyList<string> FindNamesContaining(string substring) =>
            string.IsNullOrEmpty(substring)
                ? Array.Empty<string>().AsReadOnly()
                : ExecuteRead(() => _orderedNames
                    .Where(n => n.Contains(substring, StringComparison.OrdinalIgnoreCase))
                    .ToList()
                    .AsReadOnly());

        #endregion

        #region Indexing by Position

        /// <summary>
        /// Gets the name at the specified insertion order index.
        /// </summary>
        public string GetNameAt(int index) => ExecuteRead(() =>
        {
            if (index < 0 || index >= _orderedNames.Count)
                throw new IndexOutOfRangeException("Index is out of range.");
            return _orderedNames[index];
        });

        /// <summary>
        /// Gets the character data at the specified insertion order index.
        /// </summary>
        public CharacterData GetCharacterDataAt(int index) => ExecuteRead(() =>
        {
            if (index < 0 || index >= _orderedNames.Count)
                throw new IndexOutOfRangeException("Index is out of range.");
            return _characterData[_orderedNames[index]];
        });

        /// <summary>
        /// Gets the name and character data at the specified insertion order index.
        /// </summary>
        public (string Name, CharacterData Data) GetAt(int index) => ExecuteRead(() =>
        {
            if (index < 0 || index >= _orderedNames.Count)
                throw new IndexOutOfRangeException("Index is out of range.");
            var name = _orderedNames[index];
            return (name, _characterData[name]);
        });

        #endregion

        #region Enumeration over Characters

        /// <summary>
        /// Gets an enumerable of name and character data pairs in insertion order.
        /// </summary>
        public IEnumerable<(string Name, CharacterData Data)> GetCharacters() => ExecuteRead(() =>
            _orderedNames.Select(name => (name, _characterData[name]))
        );

        #endregion

        #region Serialization

        /// <summary>
        /// Serializes the CharList to a JSON string.
        /// </summary>
        public string ToJson() => ExecuteRead(() => JsonSerializer.Serialize(new SerializableData
        {
            Names = _orderedNames.ToList(),
            CharacterData = _characterData.ToDictionary(kvp => kvp.Key, kvp => kvp.Value)
        }));

        /// <summary>
        /// Deserializes a CharList from a JSON string.
        /// </summary>
        public static CharList FromJson(string json)
        {
            ArgumentNullException.ThrowIfNull(json, nameof(json));
            var data = JsonSerializer.Deserialize<SerializableData>(json)
                ?? throw new JsonException("Deserialized data is null.");
            return new CharList(data.Names.Select(n => (n, data.CharacterData.GetValueOrDefault(n, new CharacterData()))));
        }

        #endregion

        #region Validation

        /// <summary>
        /// Validates a name (3-20 characters, not null or whitespace).
        /// </summary>
        protected virtual bool IsValidName(string name) =>
            !string.IsNullOrWhiteSpace(name) && name.Length >= 3 && name.Length <= 20;

        #endregion

        #region IEnumerable Implementation

        /// <summary>
        /// Gets an enumerator over the names in insertion order.
        /// </summary>
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

    /// <summary>
    /// Represents character data with extensible properties.
    /// </summary>
    public class CharacterData
    {
        /// <summary>
        /// Gets or sets the character's level.
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Gets or sets the character's class.
        /// </summary>
        public string? Class { get; set; }

        /// <summary>
        /// Gets a dictionary for storing custom properties.
        /// </summary>
        public Dictionary<string, object> CustomProperties { get; } = new Dictionary<string, object>();
    }

    /// <summary>
    /// Represents the result of an operation with success status and message.
    /// </summary>
    public class OperationResult
    {
        /// <summary>
        /// Indicates whether the operation succeeded.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Provides a message describing the operation's outcome.
        /// </summary>
        public string Message { get; set; } = string.Empty;
    }
}
