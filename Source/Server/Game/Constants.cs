
namespace Server
{
    static class Constant
    {

        // Path constants
        internal const string ADMIN_LOG = "admin.log";
        internal const string PLAYER_LOG = "player.log";
        internal const string PACKET_LOG = "packet.log";

        internal const long ITEM_SPAWN_TIME = 30000L; // 30 seconds
        internal const long ITEM_DESPAWN_TIME = 90000L; // 1:30 seconds

        internal const byte STAT_PER_LEVEL = 5;
    }
}