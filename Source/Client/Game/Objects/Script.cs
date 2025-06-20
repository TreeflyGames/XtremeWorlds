using Assimp;
using Core;
using Mirage.Sharp.Asfw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Script
    {
        public static string TempFile = System.IO.Path.GetTempFileName() + ".cs";

        public static void Packet_EditScript(ref byte[] data)
        {
            ByteStream buffer;

            buffer = new ByteStream(data);

            // Read the number of lines first
            int lineCount = buffer.ReadInt32();
            var lines = new string[lineCount];
            for (int i = 0; i < lineCount; i++)
            {
                lines[i] = buffer.ReadString();
            }
            Core.Type.Script.Code = lines;

            buffer.Dispose();

            GameState.InitScriptEditor = true;
        }

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
            buffer.WriteString(string.Join(Environment.NewLine, Core.Type.Script.Code));

            NetworkConfig.Socket.SendData(buffer.UnreadData, buffer.WritePosition);
            buffer.Dispose();

        }

        public static void ScriptEditorInit()
        {

        }
    }
}
