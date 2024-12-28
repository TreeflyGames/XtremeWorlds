using System;
using Microsoft.Extensions.Configuration;

namespace Configuration.Interfaces
{
    public interface IEngineConfigurationBuilder : IConfigurationBuilder, IDisposable
    {

        void LoadEnvironmentSettingsFiles();
        void LoadEnvironmentVariables(string prefix = "Mirage");
        void LoadSettingsFiles();
    }
}