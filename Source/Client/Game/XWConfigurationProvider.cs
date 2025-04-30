﻿using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using Reoria.Engine.Container.Configuration;

namespace Client;

public class XWConfigurationProvider : EngineConfigurationProvider
{
    protected override void OnSetEarlyConfigurationBuilderBasePath(IConfigurationBuilder builder)
        => builder.SetBasePath(AppContext.BaseDirectory);

    protected override void OnCreateEarlyConfigurationBuilder(IConfigurationBuilder builder)
    {
        builder.SetBasePath(AppContext.BaseDirectory);
        _ = builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        _ = builder.AddJsonFile($"appsettings.{this.Environment.ToLower()}.json", optional: true, reloadOnChange: true);
    }

    protected override void OnSetConfigurationBuilderBasePath(IConfigurationBuilder builder)
        => builder.SetBasePath(AppContext.BaseDirectory);

    protected override void OnCreateConfigurationBuilder(IConfigurationBuilder builder)
    {
        _ = builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        _ = builder.AddJsonFile("appsettings.client.json", optional: true, reloadOnChange: true);
        _ = builder.AddJsonFile("appsettings.client.secret.json", optional: true, reloadOnChange: true);
        _ = builder.AddJsonFile($"appsettings.{this.Environment.ToLower()}.json", optional: true, reloadOnChange: true);
        _ = builder.AddJsonFile($"appsettings.client.{this.Environment.ToLower()}.json", optional: true, reloadOnChange: true);
        _ = builder.AddJsonFile($"appsettings.client.secret.{this.Environment.ToLower()}.json", optional: true, reloadOnChange: true);
    }
}
