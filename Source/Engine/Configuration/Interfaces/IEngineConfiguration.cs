using Microsoft.Extensions.Configuration;

namespace Configuration.Interfaces
{
    public interface IEngineConfiguration : IConfigurationRoot, IConfiguration
    {

        ValueType GetValue<ValueType>(string key, ValueType defaultValue);
    }
}