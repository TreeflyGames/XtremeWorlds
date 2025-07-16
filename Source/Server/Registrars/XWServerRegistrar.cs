using Reoria.Engine.Container.Configuration.Interfaces;
using Reoria.Engine.Container.Registrars;

namespace Server.Registrars;

public class XWServerRegistrar : IConfigurationRegistrar
{
    public void RegisterSources(IEngineConfigurationSources sources)
    {
        sources.AddSource("appsettings.json", optional: false, reloadOnChange: true);
        sources.AddSource("appsettings.server.json", optional: true, reloadOnChange: true);
        sources.AddSource("appsettings.server.secret.json", optional: true, reloadOnChange: true);
    }
}
