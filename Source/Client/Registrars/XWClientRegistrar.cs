using Reoria.Engine.Container.Configuration.Interfaces;
using Reoria.Engine.Container.Registrars;

namespace Client.Registrars;

public class XWClientRegistrar : IConfigurationRegistrar
{
    public void RegisterSources(IEngineConfigurationSources sources)
    {
        sources.AddSource("appsettings.json", optional: false, reloadOnChange: true);
        sources.AddSource("appsettings.client.json", optional: true, reloadOnChange: true);
        sources.AddSource("appsettings.client.secret.json", optional: true, reloadOnChange: true);
    }
}
