using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reoria.Engine.Container;
using Reoria.Engine.Container.Logging;
using Reoria.Engine.Events;
using Reoria.Engine.Events.Interfaces;
using Reoria.Engine.Security.Cryptography;
using Reoria.Engine.Security.Cryptography.Interfaces;

namespace Server;

public class XWContainer : EngineContainer<SerilogLoggingInitializer>
{
    protected override IConfigurationBuilder CreateEarlyConfigurationBuilder(IConfigurationBuilder builder)
    {
        _ = builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        return base.CreateEarlyConfigurationBuilder(builder);
    }

    protected override void OnCreateConfiguration(IConfigurationBuilder builder)
    {
        _ = builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        _ = builder.AddJsonFile("appsettings.server.json", optional: true, reloadOnChange: true);
        _ = builder.AddJsonFile("appsettings.server.secret.json", optional: true, reloadOnChange: true);

        base.OnCreateConfiguration(builder);
    }

    protected override void OnCreateServiceCollection(IServiceCollection services)
    {
        _ = services.AddScoped<IHashGenerator, HashGenerator>();
        _ = services.AddScoped<ISaltGenerator, SaltGenerator>();

        _ = services.AddSingleton<IEventBus, EventBus>();

        base.OnCreateServiceCollection(services);
    }
}
