using Mirage.Sharp.Asfw.IO.Encryption;

namespace Server
{

    public class Global
    {
        public static bool DebugTxt;
        public static int ErrorCount;
        public static int UniqueIdCounter;

        public static KeyPair EKeyPair = new KeyPair();
    }
}