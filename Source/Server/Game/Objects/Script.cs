using CSScriptLib;
using Mirage.Sharp.Asfw;
using Mirage.Sharp.Asfw.Network;
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
        public static Task ScriptLoopTask;
        public static CancellationTokenSource ScriptLoopCts;

        public interface Main
        {
            int Loop();
        }

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

            buffer.WriteInt32((int)ServerPackets.SScriptEditor);
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

            // Save ith the new script code
            File.WriteAllText(Core.Path.Scripts, buffer.ReadString(), Encoding.UTF8);

            Task.Run(async () =>
            {
                await LoadScriptAsync(index);
            });
        }

        public static async Task LoadScriptAsync(int index)
        {
            try
            {
                // Load the script file
                if (File.Exists(Core.Path.Scripts))
                {
                    var text = File.ReadAllText(Core.Path.Scripts, Encoding.UTF8);
                    if (!string.IsNullOrEmpty(text))
                    {
                        Core.Type.Script.Code = text;
                    }
                    else
                    {
                        Core.Type.Script.Code = string.Empty;
                    }
                }
                else
                {
                    Core.Type.Script.Code = string.Empty;
                }

                // Cancel previous loop if running
                ScriptLoopCts?.Cancel();
                if (ScriptLoopTask != null)
                {
                    try { await ScriptLoopTask; } catch { /* ignore */ }
                }

                // Compile the script
                var script = CSScript.Evaluator.CompileMethod(Core.Type.Script.Code);

                if (script != null)
                {
                    Main main = CSScript.Evaluator
                                         .LoadCode<Main>(Core.Type.Script.Code);
                    // Call the Main loop if available
                    if (main != null)
                    {
                        ScriptLoopCts = new CancellationTokenSource();
                        var token = ScriptLoopCts.Token;
                        ScriptLoopTask = Task.Run(async () =>
                        {
                            while (!token.IsCancellationRequested)
                            {
                                // Assuming Main has a method called Loop to be called repeatedly
                                main.Loop();
                                await Task.Delay(1, token); // Prevents tight infinite loop, adjust as needed
                            }
                        }, token);
                    }
                }
                else
                {
                    NetworkSend.PlayerMsg(index, "Script compilation failed.", (int)ColorType.BrightRed);
                    Debug.WriteLine("Script compilation failed.");
                }

            }
            catch (Exception ex)
            {
                NetworkSend.PlayerMsg(index, "Error loading scripts: " + ex.Message, (int)ColorType.BrightRed);
                Debug.WriteLine("Error loading scripts: " + ex.Message);
            }
        }
    }
}
