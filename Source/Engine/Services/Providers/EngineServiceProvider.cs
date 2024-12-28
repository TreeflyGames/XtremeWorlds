using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Configuration.Interfaces;
using EngineServices.Providers.Interfaces;

namespace EngineServices.Providers
{
    public class EngineServiceProvider : IEngineServiceProvider
    {

        public ServiceCollection Services { get; private set; }
        private IServiceProvider _serviceProvider;
        private bool _isDisposed;

        public EngineServiceProvider(ref IEngineConfigurationBuilder configurationBuilder)
        {
            Services = new ServiceCollection();

            IEngineConfiguration configuration = (IEngineConfiguration)configurationBuilder.Build();
            var unused1 = Services.AddSingleton(configuration);
            var unused2 = Services.AddSingleton<IConfiguration>(configuration);
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

        public IServiceProvider ServiceProvider
        {
            get
            {
                if (_serviceProvider is null)
                {
                    if (Services is not null)
                    {
                        _serviceProvider = Services.BuildServiceProvider();
                    }
                    else
                    {
                        throw new NullReferenceException(nameof(Services));
                    }
                }

                return _serviceProvider;
            }
        }
    }

}