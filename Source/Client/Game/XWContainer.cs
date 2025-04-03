using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reoria.Engine.Container;
using Reoria.Engine.Container.Logging;
using Reoria.Engine.Events;
using Reoria.Engine.Events.Interfaces;
using Reoria.Engine.Security.Cryptography;
using Reoria.Engine.Security.Cryptography.Interfaces;

namespace Client;

public class XWContainer : EngineContainer<SerilogLoggingInitializer>
{
    private const string AppSettingsFile = "appsettings.json";
    private const string ClientSettingsFile = "appsettings.client.json";
    private const string ClientSecretSettingsFile = "appsettings.client.secret.json";

    protected override IConfigurationBuilder CreateEarlyConfigurationBuilder(IConfigurationBuilder builder)
    {
        return builder.AddJsonFile(AppSettingsFile, optional: false, reloadOnChange: true);
    }

    protected override void OnCreateConfiguration(IConfigurationBuilder builder)
    {
        builder.AddJsonFile(AppSettingsFile, optional: false, reloadOnChange: true)
               .AddJsonFile(ClientSettingsFile, optional: true, reloadOnChange: true)
               .AddJsonFile(ClientSecretSettingsFile, optional: true, reloadOnChange: true);
    }

    protected override void OnCreateServiceCollection(IServiceCollection services)
    {
        services.AddScoped<IHashGenerator, HashGenerator>()
                .AddScoped<ISaltGenerator, SaltGenerator>()
                .AddSingleton<IEventBus, EventBus>();
    }
}
protected override void OnCreateConfiguration(IConfigurationBuilder builder)
{
    builder.AddJsonFile(AppSettingsFile, optional: false, reloadOnChange: true)
           .AddJsonFile(ClientSettingsFile, optional: true, reloadOnChange: true)
           .AddJsonFile(ClientSecretSettingsFile, optional: true, reloadOnChange: true);

    var config = builder.Build();
    if (string.IsNullOrEmpty(config.GetSection("RequiredSection")?.Value))
    {
        throw new InvalidOperationException("Required configuration section is missing");
    }
}
protected override IConfigurationBuilder CreateEarlyConfigurationBuilder(IConfigurationBuilder builder)
{
    try
    {
        return builder.AddJsonFile(AppSettingsFile, optional: false, reloadOnChange: true);
    }
    catch (FileNotFoundException ex)
    {
        throw new InvalidOperationException($"Required configuration file '{AppSettingsFile}' not found", ex);
    }
}protected override void OnCreateServiceCollection(IServiceCollection services)
{
    services.AddScoped<IHashGenerator, HashGenerator>()
            .AddScoped<ISaltGenerator, SaltGenerator>()
            .AddSingleton<IEventBus>(sp => 
                new EventBus(sp.GetRequiredService<ILogger<EventBus>>()));
}
