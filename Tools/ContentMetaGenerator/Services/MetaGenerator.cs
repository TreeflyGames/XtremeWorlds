using ContentMetaGenerator.Content.Interfaces;
using ContentMetaGenerator.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ContentMetaGenerator.Services;

public class MetaGenerator(ILogger<IMetaGenerator> logger, IConfiguration configuration, IEnumerable<IContentParser> contentParsers) : IMetaGenerator
{
    public void Run()
    {
        logger.LogInformation("Starting content metadata generator, please wait...");

        if(!this.VerifyClientPath())
        { return; }

        if(!this.VerifyServerPath())
        { return; }

        foreach (IContentParser contentParser in contentParsers)
        {
            logger.LogInformation("Parsing {parser} content...", contentParser.GetType().Name.Replace("Parser", string.Empty).ToLower());
            contentParser.Parse();
        }

        logger.LogInformation("Content metadata has been generated, please restart your server to load updated data.");
    }

    protected virtual bool VerifyClientPath()
    {
        string path = Path.Combine(configuration["Sources:ClientPath"] ?? string.Empty, "Client.exe");

        if (!File.Exists(path))
        {
            logger.LogError("Unable to verify path to client folder, please check the appsettings.content.json file and ensure the correct path is there.");
            logger.LogError("Expected client path: {path}", path);
            return false;
        }

        logger.LogInformation("Verifed correct path to client directory.");
        return true;
    }

    protected virtual bool VerifyServerPath()
    {
        string path = Path.Combine(configuration["Sources:ServerPath"] ?? string.Empty, "Server.exe");

        if (!File.Exists(path))
        {
            logger.LogError("Unable to verify path to server folder, please check the appsettings.content.json file and ensure the correct path is there.");
            logger.LogError("Expected server path: {path}", path);
            return false;
        }

        logger.LogInformation("Verifed correct path to server directory.");
        return true;
    }
}
