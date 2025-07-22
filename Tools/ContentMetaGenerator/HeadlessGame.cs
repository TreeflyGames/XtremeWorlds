using Microsoft.Extensions.Configuration;
using Microsoft.Xna.Framework;

namespace ContentMetaGenerator;

public class HeadlessGame : Game
{
    public bool IsInitialized { get; protected set; } = false;

    public HeadlessGame(IConfiguration configuration)
    {
        GraphicsDeviceManager gdm = new (this);
        this.Content.RootDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, configuration["Sources:ClientPath"] ?? string.Empty, configuration["Content:RootPath"] ?? string.Empty));
        gdm.PreparingDeviceSettings += (_, e) => e.GraphicsDeviceInformation.PresentationParameters.DeviceWindowHandle = this.Window.Handle;
    }

    protected override void Update(GameTime gameTime)
    {
        this.IsInitialized = true;

        base.Update(gameTime);
    }
}
