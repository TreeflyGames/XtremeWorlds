using Microsoft.Extensions.DependencyInjection;
using Reoria.Engine.Container;

namespace Server;

public class XWContainer(IServiceCollection services) : EngineContainer(services)
{

}
