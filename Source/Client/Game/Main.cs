using Avalonia;
using AvaloniaAppTemplate;
using System.Reflection.Metadata;
using Avalonia.Threading;
using static Core.Enum;

namespace Client
{

    public class Program
    {
        public partial class App : Application
        {
            public override void OnFrameworkInitializationCompleted()
            {
                Client.Program.StartGameThread();
                base.OnFrameworkInitializationCompleted();
            }
        }
        
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args)
        {
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }

        public static AppBuilder BuildAvaloniaApp() =>
            AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .WithInterFont()
                .LogToTrace();

        // If you want to run the game in a separate thread, call this from somewhere else in your app.
        public static void StartGameThread()
        {
            var gameThread = new System.Threading.Thread(RunGame);
            gameThread.IsBackground = false;
            gameThread.Start();
        }

        public static void RunGame()
        {
            General.Client.Run();
        }
    }
}