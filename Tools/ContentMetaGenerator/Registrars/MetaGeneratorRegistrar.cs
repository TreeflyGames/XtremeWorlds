using Autofac;
using ContentMetaGenerator.Content.Interfaces;
using ContentMetaGenerator.Services;
using ContentMetaGenerator.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Reoria.Engine.Container.Configuration.Interfaces;
using Reoria.Engine.Container.Registrars;

namespace ContentMetaGenerator.Registrars;

public class MetaGeneratorRegistrar : IServiceRegistrar, IConfigurationRegistrar
{
    public void RegisterServices(ContainerBuilder builder, IConfiguration configuration, ILoggerFactory loggerFactory)
    {
        List<IContentParser> parsers = [];
        IEnumerable<Type> parserTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => typeof(IContentParser).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .ToArray();

        foreach (Type type in parserTypes)
        {
            _ = builder.RegisterType(type).As<IContentParser>().InstancePerDependency();
        }

        _ = builder.RegisterType<ContentManagerService>().As<IContentManagerService>().SingleInstance();
        _ = builder.RegisterType<MetaGenerator>().As<IMetaGenerator>().SingleInstance();
        _ = builder.RegisterType<HeadlessGame>().As<HeadlessGame>().SingleInstance();
    }

    public void RegisterSources(IEngineConfigurationSources sources)
        => sources.AddSource("appsettings.content.json", optional: false, reloadOnChange: false);
}
