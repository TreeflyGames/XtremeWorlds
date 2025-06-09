using CSScriptLib;
using Mirage.Sharp.Asfw;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Core.Enum;
using static Core.Packets;

namespace Server
{
    public class Script
    {
        public static void Packet_RequestEditScript(int index, ref byte[] data)
        {
            var buffer = new ByteStream(4);

            if (Core.Global.Command.GetPlayerAccess(index) < (byte)AccessType.Owner)
                return;

            if (Core.Type.TempPlayer[index].Editor > 0)
                return;

            string user;

            user = Core.Global.Command.IsEditorLocked(index, (byte)EditorType.Script);

            if (!string.IsNullOrEmpty(user))
            {
                NetworkSend.PlayerMsg(index, "The game editor is locked and being used by " + user + ".", (int)ColorType.BrightRed);
                return;
            }

            Core.Type.TempPlayer[index].Editor = (byte)EditorType.Script;

            buffer.WriteInt32((int)ServerPackets.SScript);
            buffer.WriteBoolean(Core.Type.Script.Type);
            buffer.WriteString(Core.Type.Script.Code);

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();

        }

        public static void Packet_SaveScript(int index, ref byte[] data)
        {
            var buffer = new ByteStream(data);

            // Prevent hacking
            if (Core.Global.Command.GetPlayerAccess(index) < (byte)AccessType.Owner)
                return;

            ref var withBlock = ref Core.Type.Script;
            withBlock.Type = buffer.ReadBoolean();
            withBlock.Code = buffer.ReadString();

            // Save it: encode Type at the top, then Code
            var sb = new StringBuilder();
            sb.AppendLine(withBlock.Type.ToString());
            sb.Append(withBlock.Code);
            File.WriteAllText(Core.Path.Scripts, sb.ToString(), Encoding.UTF8);
        }

        public static async Task LoadScriptAsync()
        {
            try
            {
                // Load the script file
                if (File.Exists(Core.Path.Scripts))
                {
                    var lines = File.ReadAllLines(Core.Path.Scripts, Encoding.UTF8);
                    if (lines.Length > 0)
                    {
                        // First line is the type
                        if (bool.TryParse(lines[0], out bool type))
                        {
                            Core.Type.Script.Type = type;
                        }
                        else
                        {
                            Core.Type.Script.Type = false;
                        }

                        // Remaining lines are the code
                        Core.Type.Script.Code = string.Join(Environment.NewLine, lines.Skip(1));
                    }
                    else
                    {
                        Core.Type.Script.Type = false;
                        Core.Type.Script.Code = string.Empty;
                    }
                }
                else
                {
                    Core.Type.Script.Type = false;
                    Core.Type.Script.Code = string.Empty;
                }

                // Compile the script
                var script = CSScript.Evaluator.CompileMethod(Core.Type.Script.Code);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error loading scripts: " + ex.Message);
            }
        }
    }
}
