using Mirage.Sharp.Asfw.IO.Encryption;

namespace Server
{

    static class Global
    {
        public static bool DebugTxt;
        public static int ErrorCount;

        // Used for closing key doors again
        public static int KeyTimer;

        // Used for gradually giving back npcs hp
        public static int GiveNPCHPTimer;

        public static int GiveNPCMPTimer;

        public static KeyPair EKeyPair = new KeyPair();
    }
}