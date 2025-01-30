using System.Reflection.Metadata;
using static Core.Enum;

namespace Client
{

    static class Program
    {
        private static Timer updateFormsTimer;

        public static void Main()
        {
            RunGame();
        }

        public static void RunGame()
        {
            General.Client.Run();
        }

    }
}