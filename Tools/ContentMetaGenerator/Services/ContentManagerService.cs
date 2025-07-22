using ContentMetaGenerator.Services.Interfaces;
using Microsoft.Xna.Framework.Content;

namespace ContentMetaGenerator.Services;

public class ContentManagerService(HeadlessGame game) : IContentManagerService
{
    public ContentManager ContentManager
    {
        get
        {
            if (!game.IsInitialized)
            {
                game.RunOneFrame();
            }

            return game.Content;
        }
    }
}
