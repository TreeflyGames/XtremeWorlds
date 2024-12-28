using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Configuration.Interfaces;

namespace Configuration
{
    public class EngineConfigurationSection : IEngineConfigurationSection
    {

        private readonly Dictionary<string, string> _children = new Dictionary<string, string>();
        private IChangeToken _reloadToken;
        private CancellationTokenSource _cancellationTokenSource;

        public EngineConfigurationSection(string key, string path, string value, Dictionary<string, string> children = null)
        {
            Key = key;
            Path = path;
            Value = value;

            if (children is not null)
            {
                _children = children;
            }

            _cancellationTokenSource = new CancellationTokenSource();
            _reloadToken = new CancellationChangeToken(_cancellationTokenSource.Token);
        }

        public string Key { get; private set; }

        public string Path { get; private set; }

        public string Value { get; set; }

        public string this[string key]
        {
            get
            {
                string value = null;
                return _children.TryGetValue(key, out value) ? value : null;
            }
            set
            {
                _children[key] = value;
                TriggerReload();
            }
        }

        public IConfigurationSection GetSection(string key)
        {
            string value = null;
            return _children.TryGetValue(key, out value) ? new EngineConfigurationSection(key, Path + "." + key, value) : new EngineConfigurationSection(key, Path + "." + key, null);
        }

        public IEnumerable<IConfigurationSection> GetChildren()
        {
            return _children.Select(pair => new EngineConfigurationSection(pair.Key, Path + "." + pair.Key, pair.Value));
        }

        public IChangeToken GetReloadToken()
        {
            return _reloadToken;
        }

        private void TriggerReload()
        {
            if (_cancellationTokenSource is not null)
            {
                _cancellationTokenSource.Cancel();

                _cancellationTokenSource = new CancellationTokenSource();
                _reloadToken = new CancellationChangeToken(_cancellationTokenSource.Token);
            }
        }
    }
}