using Core;
using Mirage.Sharp.Asfw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Game.Objects
{
    public class Script
    {
        public static void SendRequestEditScript()
        {
            ByteStream buffer;

            buffer = new ByteStream(4);
            buffer.WriteInt32((int)Packets.ClientPackets.CRequestEditScript);
            NetworkConfig.Socket.SendData(buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();

        }

        public static void SendSaveScript()
        {
            ByteStream buffer;

            buffer = new ByteStream(4);

            buffer.WriteInt32((int)Packets.ClientPackets.CSaveScript);
            buffer.WriteBoolean(Core.Type.Script.Type);
            buffer.WriteString(Core.Type.Script.Code);

            NetworkConfig.Socket.SendData(buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();

        }
    }
}
