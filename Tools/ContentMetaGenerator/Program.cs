using ContentMetaGenerator.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Reoria.Engine.Container;
using Reoria.Engine.Container.Configuration;
using Reoria.Engine.Container.Configuration.Interfaces;
using Reoria.Engine.Container.Interfaces;
using Reoria.Engine.Container.Logging;
using Reoria.Engine.Container.Logging.Interfaces;

IServiceCollection services = new ServiceCollection()
    .AddTransient<IEngineConfigurationSources, EngineConfigurationSources>()
    .AddTransient<IEngineConfigurationProvider, EngineConfigurationProvider>()
    .AddTransient<IEngineLoggerFactory, SerilogLoggerFactory>();

IEngineContainer container = new EngineContainer(services);

container.Provider.GetService<IMetaGenerator>()?.Run();
