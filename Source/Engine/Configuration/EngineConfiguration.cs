using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualBasic.CompilerServices;
using Configuration.Interfaces;

namespace Configuration
{
    public class EngineConfiguration : IEngineConfiguration
    {

        private readonly Dictionary<string, string> _configDictionary = new Dictionary<string, string>();
        private readonly Dictionary<string, Dictionary<string, string>> _configSections = new Dictionary<string, Dictionary<string, string>>();
        private readonly List<IConfigurationProvider> _providers = new List<IConfigurationProvider>();
        private IChangeToken _reloadToken;

        public string this[string key]
        {
            get
            {
                return _configDictionary.ContainsKey(key) ? _configDictionary[key] : string.Empty;
            }
            set
            {
                _configDictionary[key] = value;
            }
        }

        public IEnumerable<IConfigurationProvider> Providers
        {
            get
            {
                return _providers;
            }
        }

        public void Reload()
        {
            foreach (IConfigurationProvider provider in _providers)
                provider.Load();

            _reloadToken = new CancellationChangeToken(CancellationToken.None);
        }

        public IConfigurationSection GetSection(string key)
        {
            if (_configSections.ContainsKey(key))
            {
                return new EngineConfigurationSection(key, key, null, _configSections[key]);
            }
            else
            {
                return new EngineConfigurationSection(key, key, _configDictionary[key]);
            }
        }

        public IEnumerable<IConfigurationSection> GetChildren()
        {
            return _configSections.Select(pair => new EngineConfigurationSection(pair.Key, pair.Key, null, pair.Value));
        }

        public IChangeToken GetReloadToken()
        {
            return _reloadToken;
        }

        public ValueType GetValue<ValueType>(string key, ValueType defaultValue)
        {
            string value = this[key];

            if (!string.IsNullOrWhiteSpace(value))
            {
                try
                {
                    return Conversions.ToGenericParameter<ValueType>(Convert.ChangeType(value, typeof(ValueType)));
                }
                catch (InvalidCastException ex)
                {
                    return defaultValue;
                }
            }
            else
            {
                return defaultValue;
            }
        }
    }
}