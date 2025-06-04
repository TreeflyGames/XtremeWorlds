using Avalonia;
using System.Reflection.Metadata;
using Avalonia.Threading;
using static Core.Enum;

namespace Client
{
    public class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args)
        {
            RunGame();
        }

        public static void RunGame()
        {
            General.Client.Run();
        }
    }
}