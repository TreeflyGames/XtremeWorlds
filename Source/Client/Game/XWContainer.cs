using Microsoft.Extensions.DependencyInjection;
using Reoria.Engine.Container;

namespace Client;

public class XWContainer(IServiceCollection services) : EngineContainer(services)
{

}