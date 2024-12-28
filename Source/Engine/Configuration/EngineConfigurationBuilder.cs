using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Configuration.Interfaces;

namespace Configuration
{
    public class EngineConfigurationBuilder : IEngineConfigurationBuilder
    {

        private bool _isDisposed;
        private readonly Dictionary<string, object> _properties = new Dictionary<string, object>();
        private readonly List<IConfigurationSource> _sources = new List<IConfigurationSource>();

        public IDictionary<string, object> Properties
        {
            get
            {
                return _properties;
            }
        }

        public IList<IConfigurationSource> Sources
        {
            get
            {
                return _sources;
            }
        }

        public IConfigurationBuilder Add(IConfigurationSource source)
        {
            _sources.Add(source);
            return this;
        }

        public IConfigurationRoot Build()
        {
            var engineConfig = new EngineConfiguration();

            foreach (IConfigurationSource source in _sources)
            {
                var provider = source.Build(this);

                IConfiguration configuration = new ConfigurationRoot(new List<IConfigurationProvider>() { provider });

                foreach (KeyValuePair<string, string> kvp in configuration.AsEnumerable())
                    engineConfig[kvp.Key] = kvp.Value;
            }

            return engineConfig;
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    // Dispose of managed resources here if needed.
                }
                _isDisposed = true;
            }
        }

        private void LoadJsonFiles(IEnumerable<string> filePaths)
        {
            foreach (string @file in filePaths)
            {
                var unused = this.AddJsonFile(@file, optional: true, reloadOnChange: true);
            }
        }

        public void LoadSettingsFiles()
        {
            var files = Directory.GetFiles(AppContext.BaseDirectory).Where(name => name.Contains("appsettings") && name.EndsWith(".json")).Where(name => !name.Contains(".development") && !name.Contains(".production"));


            LoadJsonFiles(files);
        }

        public void LoadEnvironmentSettingsFiles()
        {
            string currentEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")?.ToLowerInvariant();

            if (string.IsNullOrWhiteSpace(currentEnvironment))
                currentEnvironment = "production";

            var files = Directory.GetFiles(AppContext.BaseDirectory).Where(name => name.Contains("appsettings") && name.EndsWith($".{currentEnvironment}.json"));

            LoadJsonFiles(files);
        }

        public void LoadEnvironmentVariables(string prefix = "XW")
        {
            var unused = this.AddEnvironmentVariables(prefix);
        }
    }
}