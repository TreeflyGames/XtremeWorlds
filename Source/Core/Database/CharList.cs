using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

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
        private readonly object _lock = new object();

        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="CharList"/> with default settings.
        /// </summary>
        public CharList()
        {
            _names = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            _orderedNames = new List<string>();
            _characterData = new Dictionary<string, CharacterData>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="CharList"/> with a collection of initial names.
        /// Each name is added with default character data.
        /// </summary>
        /// <param name="initialNames">Initial collection of names.</param>
        public CharList(IEnumerable<string> initialNames) : this()
        {
            if (initialNames != null)
            {
                AddRange(initialNames);
            }
        }

        /// <summary>
        /// Initializes a new instance of <see cref="CharList"/> with a collection of names and their associated data.
        /// </summary>
        /// <param name="initialData">Collection of tuples containing names and their character data.</param>
        public CharList(IEnumerable<(string Name, CharacterData Data)> initialData) : this()
        {
            if (initialData == null)
                throw new ArgumentNullException(nameof(initialData), "Initial data collection cannot be null");

            foreach (var (name, data) in initialData)
            {
                if (IsValidName(name))
                {
                    if (_names.Add(name))
                    {
                        _orderedNames.Add(name);
                        _characterData[name] = data ?? new CharacterData();
                    }
                }
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of names in the collection.
        /// </summary>
        public int Count
        {
            get
            {
                lock (_lock)
                {
                    return _names.Count;
                }
            }
        }

        /// <summary>
        /// Gets all names as a read-only collection in insertion order.
        /// </summary>
        public IReadOnlyList<string> OrderedNames
        {
            get
            {
                lock (_lock)
                {
                    return _orderedNames.AsReadOnly();
                }
            }
        }

        /// <summary>
        /// Gets all names as a read-only collection sorted alphabetically (case-insensitive).
        /// </summary>
        public IReadOnlyList<string> SortedNames
        {
            get
            {
                lock (_lock)
                {
                    return _names.OrderBy(n => n, StringComparer.OrdinalIgnoreCase).ToList().AsReadOnly();
                }
            }
        }

        /// <summary>
        /// Gets all names as a read-only collection (unordered, as stored in the set).
        /// </summary>
        public IReadOnlyCollection<string> Names
        {
            get
            {
                lock (_lock)
                {
                    return _names.ToArray();
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether the collection is empty.
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                lock (_lock)
                {
                    return _names.Count == 0;
                }
            }
        }

        /// <summary>
        /// Gets or sets the character data for a specific name using indexer syntax.
        /// </summary>
        /// <param name="name">The name to access.</param>
        /// <exception cref="KeyNotFoundException">Thrown if the name does not exist when setting data.</exception>
        public CharacterData this[string name]
        {
            get
            {
                lock (_lock)
                {
                    if (_names.Contains(name))
                        return _characterData[name];
                    throw new KeyNotFoundException($"Name '{name}' not found in the collection.");
                }
            }
            set
            {
                lock (_lock)
                {
                    if (_names.Contains(name))
                        _characterData[name] = value ?? throw new ArgumentNullException(nameof(value), "Character data cannot be null");
                    else
                        throw new KeyNotFoundException($"Name '{name}' not found in the collection.");
                }
            }
        }

        #endregion

        #region Core Methods

        /// <summary>
        /// Checks if a name exists in the collection.
        /// </summary>
        /// <param name="name">The name to check.</param>
        /// <returns><c>true</c> if the name exists; otherwise, <c>false</c>.</returns>
        public bool Contains(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            lock (_lock)
            {
                return _names.Contains(name);
            }
        }

        /// <summary>
        /// Adds a single name to the collection with default character data.
        /// </summary>
        /// <param name="name">The name to add.</param>
        /// <returns>An <see cref="OperationResult"/> indicating the outcome.</returns>
        public OperationResult Add(string name)
        {
            if (!IsValidName(name))
                return new OperationResult { Success = false, Message = "Invalid name. Name must be 3-20 characters and not null or whitespace." };

            lock (_lock)
            {
                if (_names.Add(name))
                {
                    _orderedNames.Add(name);
                    _characterData[name] = new CharacterData();
                    return new OperationResult { Success = true, Message = $"Added '{name}' successfully." };
                }
                return new OperationResult { Success = false, Message = $"Name '{name}' already exists." };
            }
        }

        /// <summary>
        /// Adds a single name to the collection with specified character data.
        /// </summary>
        /// <param name="name">The name to add.</param>
        /// <param name="data">The character data to associate with the name.</param>
        /// <returns>An <see cref="OperationResult"/> indicating the outcome.</returns>
        public OperationResult Add(string name, CharacterData data)
        {
            if (!IsValidName(name))
                return new OperationResult { Success = false, Message = "Invalid name. Name must be 3-20 characters and not null or whitespace." };

            if (data == null)
                return new OperationResult { Success = false, Message = "Character data cannot be null." };

            lock (_lock)
            {
                if (_names.Add(name))
                {
                    _orderedNames.Add(name);
                    _characterData[name] = data;
                    return new OperationResult { Success = true, Message = $"Added '{name}' with data successfully." };
                }
                return new OperationResult { Success = false, Message = $"Name '{name}' already exists." };
            }
        }

        /// <summary>
        /// Adds multiple names to the collection with default character data.
        /// </summary>
        /// <param name="names">The collection of names to add.</param>
        /// <returns>A list of names that failed to be added (invalid or duplicates).</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="names"/> is null.</exception>
        public List<string> AddRange(IEnumerable<string> names)
        {
            if (names == null)
                throw new ArgumentNullException(nameof(names), "Names collection cannot be null.");

            var failed = new List<string>();
            lock (_lock)
            {
                foreach (var name in names)
                {
                    if (!IsValidName(name))
                    {
                        failed.Add(name);
                        continue;
                    }
                    if (!_names.Add(name))
                    {
                        failed.Add(name);
                        continue;
                    }
                    _orderedNames.Add(name);
                    _characterData[name] = new CharacterData();
                }
            }
            return failed;
        }

        /// <summary>
        /// Removes a name and its associated data from the collection.
        /// </summary>
        /// <param name="name">The name to remove.</param>
        /// <returns>An <see cref="OperationResult"/> indicating the outcome.</returns>
        public OperationResult Remove(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return new OperationResult { Success = false, Message = "Name cannot be null or whitespace." };

            lock (_lock)
            {
                if (_names.Remove(name))
                {
                    _orderedNames.Remove(name);
                    _characterData.Remove(name);
                    return new OperationResult { Success = true, Message = $"Removed '{name}' successfully." };
                }
                return new OperationResult { Success = false, Message = $"Name '{name}' not found." };
            }
        }

        /// <summary>
        /// Clears all names and associated data from the collection.
        /// </summary>
        /// <returns>The current instance for method chaining.</returns>
        public CharList Clear()
        {
            lock (_lock)
            {
                _names.Clear();
                _orderedNames.Clear();
                _characterData.Clear();
            }
            return this;
        }

        /// <summary>
        /// Merges another <see cref="CharList"/> into this one. Existing names retain their current data.
        /// </summary>
        /// <param name="other">The <see cref="CharList"/> to merge from.</param>
        /// <returns>The current instance for method chaining.</returns>
        public CharList Merge(CharList other)
        {
            if (other == null)
                return this;

            lock (_lock)
            {
                lock (other._lock)
                {
                    foreach (var name in other._names)
                    {
                        if (_names.Add(name))
                        {
                            _orderedNames.Add(name);
                            _characterData[name] = other._characterData[name];
                        }
                    }
                }
            }
            return this;
        }

        #endregion

        #region Character Data Management

        /// <summary>
        /// Retrieves the character data associated with a name.
        /// </summary>
        /// <param name="name">The name to look up.</param>
        /// <returns>The <see cref="CharacterData"/> if found; otherwise, <c>null</c>.</returns>
        public CharacterData GetCharacterData(string name)
        {
            lock (_lock)
            {
                return _characterData.TryGetValue(name, out var data) ? data : null;
            }
        }

        /// <summary>
        /// Sets the character data for an existing name.
        /// </summary>
        /// <param name="name">The name to update.</param>
        /// <param name="data">The new character data.</param>
        /// <returns>An <see cref="OperationResult"/> indicating the outcome.</returns>
        public OperationResult SetCharacterData(string name, CharacterData data)
        {
            if (string.IsNullOrWhiteSpace(name))
                return new OperationResult { Success = false, Message = "Name cannot be null or whitespace." };

            if (data == null)
                return new OperationResult { Success = false, Message = "Character data cannot be null." };

            lock (_lock)
            {
                if (_names.Contains(name))
                {
                    _characterData[name] = data;
                    return new OperationResult { Success = true, Message = $"Updated data for '{name}' successfully." };
                }
                return new OperationResult { Success = false, Message = $"Name '{name}' not found." };
            }
        }

        /// <summary>
        /// Attempts to retrieve the character data for a name.
        /// </summary>
        /// <param name="name">The name to look up.</param>
        /// <param name="data">The character data if found.</param>
        /// <returns><c>true</c> if the name exists; otherwise, <c>false</c>.</returns>
        public bool TryGetCharacterData(string name, out CharacterData data)
        {
            lock (_lock)
            {
                return _characterData.TryGetValue(name, out data);
            }
        }

        /// <summary>
        /// Gets all character data as an enumerable collection of key-value pairs.
        /// </summary>
        /// <returns>An enumerable of name and <see cref="CharacterData"/> pairs.</returns>
        public IEnumerable<KeyValuePair<string, CharacterData>> GetAllCharacterData()
        {
            lock (_lock)
            {
                return _characterData.ToList();
            }
        }

        #endregion

        #region Querying Methods

        /// <summary>
        /// Finds all names that start with the specified prefix (case-insensitive).
        /// </summary>
        /// <param name="prefix">The prefix to search for.</param>
        /// <returns>A read-only list of matching names.</returns>
        public IReadOnlyList<string> FindNamesStartingWith(string prefix)
        {
            if (string.IsNullOrEmpty(prefix))
                return Array.Empty<string>().AsReadOnly();

            lock (_lock)
            {
                return _orderedNames
                    .Where(n => n.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    .ToList()
                    .AsReadOnly();
            }
        }

        /// <summary>
        /// Finds all names that contain the specified substring (case-insensitive).
        /// </summary>
        /// <param name="substring">The substring to search for.</param>
        /// <returns>A read-only list of matching names.</returns>
        public IReadOnlyList<string> FindNamesContaining(string substring)
        {
            if (string.IsNullOrEmpty(substring))
                return Array.Empty<string>().AsReadOnly();

            lock (_lock)
            {
                return _orderedNames
                    .Where(n => n.IndexOf(substring, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList()
                    .AsReadOnly();
            }
        }

        /// <summary>
        /// Finds all names whose character data satisfies the specified predicate.
        /// </summary>
        /// <param name="predicate">The condition to filter character data.</param>
        /// <returns>A read-only list of matching names.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="predicate"/> is null.</exception>
        public IReadOnlyList<string> FindNames(Func<CharacterData, bool> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            lock (_lock)
            {
                return _characterData
                    .Where(kvp => predicate(kvp.Value))
                    .Select(kvp => kvp.Key)
                    .ToList()
                    .AsReadOnly();
            }
        }

        #endregion

        #region Serialization

        /// <summary>
        /// Serializes the <see cref="CharList"/> to a JSON string.
        /// </summary>
        /// <returns>A JSON string representing the collection.</returns>
        public string ToJson()
        {
            lock (_lock)
            {
                var data = new SerializableData
                {
                    Names = _orderedNames.ToList(),
                    CharacterData = _characterData.ToDictionary(kvp => kvp.Key, kvp => kvp.Value)
                };
                return JsonSerializer.Serialize(data);
            }
        }

        /// <summary>
        /// Deserializes a <see cref="CharList"/> from a JSON string.
        /// </summary>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>A new <see cref="CharList"/> instance.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="json"/> is null.</exception>
        /// <exception cref="JsonException">Thrown if deserialization fails.</exception>
        public static CharList FromJson(string json)
        {
            if (json == null)
                throw new ArgumentNullException(nameof(json), "JSON string cannot be null.");

            var data = JsonSerializer.Deserialize<SerializableData>(json);
            var charList = new CharList();
            foreach (var name in data.Names)
            {
                if (charList.IsValidName(name) && charList._names.Add(name))
                {
                    charList._orderedNames.Add(name);
                    charList._characterData[name] = data.CharacterData.TryGetValue(name, out var charData) ? charData : new CharacterData();
                }
            }
            return charList;
        }

        #endregion

        #region Validation

        /// <summary>
        /// Determines whether a name is valid. Can be overridden in derived classes.
        /// </summary>
        /// <param name="name">The name to validate.</param>
        /// <returns><c>true</c> if the name is valid; otherwise, <c>false</c>.</returns>
        protected virtual bool IsValidName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && name.Length >= 3 && name.Length <= 20;
        }

        #endregion

        #region IEnumerable Implementation

        /// <summary>
        /// Gets an enumerator for the names in insertion order.
        /// </summary>
        /// <returns>An enumerator for the names.</returns>
        public IEnumerator<string> GetEnumerator()
        {
            lock (_lock)
            {
                return _orderedNames.ToList().GetEnumerator(); // Return a copy to avoid enumeration issues during modification
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Nested Classes

        private class SerializableData
        {
            public List<string> Names { get; set; }
            public Dictionary<string, CharacterData> CharacterData { get; set; }
        }

        #endregion
    }

    /// <summary>
    /// Represents data associated with a character, such as level and class.
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
        public string Class { get; set; }

        // Additional properties can be added as needed
    }

    /// <summary>
    /// Represents the result of an operation, including success status and a message.
    /// </summary>
    public class OperationResult
    {
        /// <summary>
        /// Gets or sets a value indicating whether the operation was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets a message describing the outcome of the operation.
        /// </summary>
        public string Message { get; set; }
    }
}
