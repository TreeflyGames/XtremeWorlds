using ContentMetaGenerator.Content.Interfaces;
using ContentMetaGenerator.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ContentMetaGenerator.Content;

public class TestParser(ILogger<IContentParser> logger, IContentManagerService contentManagerService) : IContentParser
{
    protected readonly ContentManager ContentManager = contentManagerService.ContentManager;

    public void Parse()
    {
        string texturePath = "Graphics/Characters/1.png";
        Texture2D texture = this.ContentManager.Load<Texture2D>(texturePath);
        logger.LogInformation("The dimensions of '{texturePath}' are '{width}' by '{height}'.", texturePath, texture.Width, texture.Height);
    }
}
