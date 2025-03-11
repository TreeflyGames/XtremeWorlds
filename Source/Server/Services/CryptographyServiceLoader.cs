using Microsoft.Extensions.DependencyInjection;
using Reoria.Engine.Security.Cryptography;
using Reoria.Engine.Security.Cryptography.Factories;
using Reoria.Engine.Security.Cryptography.Factories.Interfaces;
using Reoria.Engine.Security.Cryptography.Interfaces;
using Service = Reoria.Engine.Services.ServiceAttribute;

namespace Server.Services;

/// <summary>
/// A service loader responsible for adding cryptographic services to the dependency injection container.
/// This class configures services related to salt and hash generation, allowing them to be used across the application.
/// </summary>
[@Service]
public static class CryptographyServiceLoader
{
    /// <summary>
    /// Registers the cryptographic services (such as salt and hash generators) into the provided <see cref="IServiceCollection"/>.
    /// These services are essential for cryptographic operations such as password hashing and salt generation.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> instance to which services will be added.</param>
    [@Service.RegisterServices]
    public static void RegisterServices(IServiceCollection services)
    {
        // Registering ISaltGenerator with a scoped lifetime (new instance per request)
        _ = services.AddScoped<ISaltGenerator, SaltGenerator>();

        // Registering IHashGenerator with a scoped lifetime (new instance per request)
        _ = services.AddScoped<IHashGenerator, HashGenerator>();

        // Registering ISaltGeneratorFactory with a singleton lifetime (shared across all requests)
        _ = services.AddSingleton<ISaltGeneratorFactory, SaltGeneratorFactory>();

        // Registering IHashGeneratorFactory with a singleton lifetime (shared across all requests)
        _ = services.AddSingleton<IHashGeneratorFactory, HashGeneratorFactory>();
    }

    /// <summary>
    /// Configures services that require an <see cref="IServiceProvider"/> to resolve dependencies at runtime.
    /// This method allows for services such as factories to have access to the provider after they have been added to the container.
    /// </summary>
    /// <param name="provider">The <see cref="IServiceProvider"/> instance used to resolve and configure services.</param>
    [@Service.ConfigureServices]
    public static void ConfigureServices(IServiceProvider provider)
    {
        // Resolving the IHashGeneratorFactory and assigning the service provider to it
        IHashGeneratorFactory? hashGeneratorFactory = provider.GetService<IHashGeneratorFactory>();
        hashGeneratorFactory?.AssignServiceProvider(provider);

        // Resolving the ISaltGeneratorFactory and assigning the service provider to it
        ISaltGeneratorFactory? saltGeneratorFactory = provider.GetService<ISaltGeneratorFactory>();
        saltGeneratorFactory?.AssignServiceProvider(provider);
    }
}
