using System;
using Microsoft.Extensions.DependencyInjection;

namespace EngineServices.Providers.Interfaces
{
    
    public interface IEngineServiceProvider : IDisposable
    {

        IServiceProvider ServiceProvider { get; }
        ServiceCollection Services { get; }
    }
}