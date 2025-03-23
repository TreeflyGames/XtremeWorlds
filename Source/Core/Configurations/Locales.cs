using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Core
{
    public class Locales
    {
        public Dictionary<string, Dictionary<string, string>> Sections { get; set; } = new Dictionary<string, Dictionary<string, string>>();

        /// <summary>
        /// Retrieves a value for a given section and key. Returns null if not found.
        /// </summary>
        public string GetValue(string section, string key)
        {
            if (Sections.TryGetValue(section, out var sectionDict) && sectionDict.TryGetValue(key, out var value))
            {
                return value;
            }
            return null;
        }

        /// <summary>
        /// Retrieves a value and formats it with the provided arguments.
        /// </summary>
        public string GetValue(string section, string key, params object[] args)
        {
            string value = GetValue(section, key);
            if (value == null) return null;
            try
            {
                return string.Format(value, args);
            }
            catch (FormatException)
            {
                return value; // Return unformatted value if formatting fails
            }
        }

        /// <summary>
        /// Adds or updates a key-value pair in the specified section.
        /// </summary>
        public void AddOrUpdate(string section, string key, string value)
        {
            if (!Sections.TryGetValue(section, out var sectionDict))
            {
                sectionDict = new Dictionary<string, string>();
                Sections[section] = sectionDict;
            }
            sectionDict[key] = value;
        }

        /// <summary>
        /// Loads a Locales instance from a JSON file.
        /// </summary>
        public static Locales LoadFromFile(string filePath)
        {
            try
            {
                string json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<Locales>(json);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to load locales from {filePath}", ex);
            }
        }

        /// <summary>
        /// Saves the Locales instance to a JSON file.
        /// </summary>
        public void SaveToFile(string filePath)
        {
            string json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }
}
