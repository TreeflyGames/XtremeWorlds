using Reoria.Engine.Base.Container.Attributes;
using Reoria.Engine.Base.Container.Configuration;
using Reoria.Engine.Base.Container.Services;

namespace Server.Game;

[Container]
public class XWContainer
{
    #region XWContainer: Service Definitions
    [ContainerAttribute.DiscoverConfigurationSources]
    public static void DiscoverConfigurationSources(ContainerConfigurationSources sources) {
        sources.Add("appsettings.json", optional: false, reloadOnChange: true);
        sources.Add("appsettings.server.json", optional: true, reloadOnChange: true);
        sources.Add("appsettings.server.secret.json", optional: true, reloadOnChange: true);
    }

    [ContainerAttribute.DiscoverSerivceDefinitions]
    public static void DiscoverSerivceDefinitions(ContainerServiceDefinitions services)
    {

    }

    [ContainerAttribute.BuildServiceProvider]
    public static void BuildServiceProvider(IServiceProvider serviceProvider)
    {

    }
    #endregion
}
