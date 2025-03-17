using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Database
{
    /// <summary>
    /// A case-insensitive collection of character names with enhanced functionality
    /// </summary>
    public class CharList
    {
        private readonly HashSet<string> _names;

        /// <summary>
        /// Initializes a new instance of CharList
        /// </summary>
        public CharList()
        {
            _names = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Initializes a new instance of CharList with initial names
        /// </summary>
        /// <param name="initialNames">Initial collection of names</param>
        public CharList(IEnumerable<string> initialNames) : this()
        {
            if (initialNames != null)
            {
                AddRange(initialNames);
            }
        }

        /// <summary>
        /// Gets the count of names in the collection
        /// </summary>
        public int Count => _names.Count;

        /// <summary>
        /// Gets all names as a read-only collection
        /// </summary>
        public IReadOnlyCollection<string> Names => _names.ToArray();

        /// <summary>
        /// Checks if a name exists in the collection
        /// </summary>
        /// <param name="name">Name to find</param>
        /// <returns>True if the name exists, false otherwise</returns>
        public bool Contains(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            return _names.Contains(name);
        }

        /// <summary>
        /// Adds a single name to the collection
        /// </summary>
        /// <param name="name">Name to add</param>
        /// <returns>Current instance for method chaining</returns>
        /// <exception cref="ArgumentNullException">Thrown when name is null</exception>
        public CharList Add(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name), "Name cannot be null or whitespace");

            _names.Add(name);
            return this;
        }

        /// <summary>
        /// Adds multiple names to the collection
        /// </summary>
        /// <param name="names">Collection of names to add</param>
        /// <returns>Current instance for method chaining</returns>
        public CharList AddRange(IEnumerable<string> names)
        {
            if (names == null)
                throw new ArgumentNullException(nameof(names), "Names collection cannot be null");

            foreach (var name in names.Where(n => !string.IsNullOrWhiteSpace(n)))
            {
                _names.Add(name);
            }
            return this;
        }

        /// <summary>
        /// Removes a name from the collection
        /// </summary>
        /// <param name="name">Name to remove</param>
        /// <returns>Current instance for method chaining</returns>
        public CharList Remove(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                _names.Remove(name);
            }
            return this;
        }

        /// <summary>
        /// Clears all names from the collection
        /// </summary>
        /// <returns>Current instance for method chaining</returns>
        public CharList Clear()
        {
            _names.Clear();
            return this;
        }

        /// <summary>
        /// Returns an array of all names in the collection
        /// </summary>
        /// <returns>Array of names</returns>
        public string[] ToArray()
        {
            return _names.ToArray();
        }

        /// <summary>
        /// Merges another CharList into this one
        /// </summary>
        /// <param name="other">CharList to merge from</param>
        /// <returns>Current instance for method chaining</returns>
        public CharList Merge(CharList other)
        {
            if (other != null)
            {
                _names.UnionWith(other._names);
            }
            return this;
        }

        /// <summary>
        /// Checks if the collection is empty
        /// </summary>
        public bool IsEmpty => _names.Count == 0;
    }
}
