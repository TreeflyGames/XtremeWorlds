using Reoria.Engine.Base.Container.Attributes;
using Reoria.Engine.Base.Container.Configuration;
using Reoria.Engine.Base.Container.Interfaces;

namespace Server.Game;

[Container]
public class XWContainer
{
    #region XWContainer: Service Definitions
    /// <summary>
    /// This method is called by <see cref="IEngineContainer"/> during the configuration setup phase. It adds 
    /// the specified configuration files (e.g., "appsettings.json") as configuration sources to the container.
    /// </summary>
    /// <param name="sources">The <see cref="ContainerConfigurationSources"/> instance that is used to add configuration sources to the container.</param>
    [ContainerAttribute.DiscoverConfigurationSources]
    public static void DiscoverConfigurationSources(ContainerConfigurationSources sources) {
        // Register the configuration sources, indicating if they are optional and should reload on change
        sources.Add("appsettings.json", optional: false, reloadOnChange: true);
        sources.Add("appsettings.server.json", optional: true, reloadOnChange: true);
        sources.Add("appsettings.server.secret.json", optional: true, reloadOnChange: true);
    }        
    #endregion
}
