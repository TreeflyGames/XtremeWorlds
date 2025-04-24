using Microsoft.Extensions.Configuration;
using Reoria.Engine.Container.Configuration;

namespace Server;

public class XWConfigurationProvider : EngineConfigurationProvider
{
    protected override void OnCreateEarlyConfigurationBuilder(IConfigurationBuilder builder)
    {
        _ = builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        _ = builder.AddJsonFile($"appsettings.{this.Environment.ToLower()}.json", optional: true, reloadOnChange: true);
    }

    protected override void OnCreateConfigurationBuilder(IConfigurationBuilder builder)
    {
        builder.SetBasePath(AppContext.BaseDirectory);
        _ = builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        _ = builder.AddJsonFile("appsettings.server.json", optional: true, reloadOnChange: true);
        _ = builder.AddJsonFile("appsettings.server.secret.json", optional: true, reloadOnChange: true);
        _ = builder.AddJsonFile($"appsettings.{this.Environment.ToLower()}.json", optional: true, reloadOnChange: true);
        _ = builder.AddJsonFile($"appsettings.server.{this.Environment.ToLower()}.json", optional: true, reloadOnChange: true);
        _ = builder.AddJsonFile($"appsettings.server.secret.{this.Environment.ToLower()}.json", optional: true, reloadOnChange: true);
    }
}
