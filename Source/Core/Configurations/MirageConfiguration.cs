using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.CompilerServices;

namespace Core
{

    public class MirageConfiguration : IDisposable
    {

        private bool isDisposed;
        private readonly IConfigurationRoot configuration;

        public MirageConfiguration(string envPrefix = "MIRAGE")
        {
            string currentEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var files = Directory.GetFiles(AppContext.BaseDirectory).Where(name => name.Contains("appsettings")).Where(name => !name.Contains(".development")).Where(name => !name.Contains(".production")).Where(name => name.EndsWith(".json"));




            var envfiles = Directory.GetFiles(AppContext.BaseDirectory).Where(name => name.Contains("appsettings")).Where(name => name.EndsWith($".{currentEnvironment}.json"));


            IConfigurationBuilder builder = new ConfigurationBuilder();

            foreach (string @file in files)
            {
                Console.WriteLine($"Reading configuration file '{@file}'...");
                builder = builder.AddJsonFile(@file, optional: true, reloadOnChange: true);
            }

            foreach (string @file in envfiles)
            {
                Console.WriteLine($"Reading configuration file '{@file}'...");
                builder = builder.AddJsonFile(@file, optional: true, reloadOnChange: true);
            }

            Console.WriteLine($"Reading configuration environment variables with prefix '{envPrefix}'...");
            builder = builder.AddEnvironmentVariables(envPrefix);

            configuration = builder.Build();
        }


        ~MirageConfiguration()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {

                }

                isDisposed = Conversions.ToBoolean(1);
            }
        }

        public ValueType GetValue<ValueType>(string key, ValueType defaultValue)
        {
            string value = configuration[key];

            if (!string.IsNullOrWhiteSpace(value))
            {
                try
                {
                    return Conversions.ToGenericParameter<ValueType>(Convert.ChangeType(value, typeof(ValueType)));
                }
                catch (InvalidCastException ex)
                {
                    Console.WriteLine($"[Error] Unable to read configuration value '{key}' as type of '{nameof(ValueType)}'.");
                    return defaultValue;
                }
            }
            else
            {
                Console.WriteLine($"[Error] Unable to read configuration value '{key}' as it does not exist'.");
                return defaultValue;
            }
        }

    }
}