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
using static Core.Packets;

namespace Server
{
    public class Script
    {
        public static dynamic? Instance { get; private set; }

        public static void Packet_RequestEditScript(int index, ref byte[] data)
        {
            var buffer = new ByteStream(4);

            if (Core.Global.Command.GetPlayerAccess(index) < (byte)Core.AccessLevel.Owner)
                return;

            string user;

            user = Core.Global.Command.IsEditorLocked(index, (byte)Core.EditorType.Script);

            if (!string.IsNullOrEmpty(user))
            {
                NetworkSend.PlayerMsg(index, "The game editor is locked and being used by " + user + ".", (int)Core.Color.BrightRed);
                return;
            }

            Core.Data.TempPlayer[index].Editor = (byte)Core.EditorType.Script;

            buffer.WriteInt32((int)ServerPackets.SScriptEditor);

            buffer.WriteInt32(Core.Data.Script.Code != null ? Core.Data.Script.Code.Length : 0);

            if (Core.Data.Script.Code != null)
            {
                foreach (var line in Core.Data.Script.Code)
                {
                    buffer.WriteString(line);
                }
            }
            else
            {
                buffer.WriteString(string.Empty);
            }

            NetworkConfig.Socket.SendDataTo(index, buffer.UnreadData, buffer.WritePosition);

            buffer.Dispose();
        }

        public static void Packet_SaveScript(int index, ref byte[] data)
        {
            var buffer = new ByteStream(data);;

            // Prevent hacking
            if (Core.Global.Command.GetPlayerAccess(index) < (byte)Core.AccessLevel.Owner)
                return;

            // Save with the new script code and ensure the filename is Script.cs
            var scriptPath = Path.Combine(Core.Path.Database, "Script.cs");
            string code = buffer.ReadString();

            // Create file
            if (!File.Exists(scriptPath))
            {
                Directory.CreateDirectory(Core.Path.Database);
                File.Create(scriptPath);
                using (File.Create(scriptPath))
                {
                    File.WriteAllText(scriptPath, buffer.ReadString(), Encoding.UTF8);
                }
            }
            else
            {
                File.WriteAllText(scriptPath, code, Encoding.UTF8);
            }

            Task.Run(async () =>
                {
                    await LoadScriptAsync(index);
                });
        }

        public static async System.Threading.Tasks.Task LoadScriptAsync(int index)
        {
            // Load the script file
            var scriptPath = Path.Combine(Core.Path.Database, "Script.cs");
            if (File.Exists(scriptPath))
            {
                var lines = File.ReadLines(scriptPath, Encoding.UTF8).ToArray();
                if (lines.Length > 0)
                {
                    Core.Data.Script.Code = lines;
                }
                else
                {
                    Core.Data.Script.Code = Array.Empty<string>();
                }
            }
            else
            {
                Core.Data.Script.Code = Array.Empty<string>();
            }

            string code = (Core.Data.Script.Code != null && Core.Data.Script.Code.Length > 0) ? string.Join(Environment.NewLine, Core.Data.Script.Code) : string.Empty;

            if (string.IsNullOrWhiteSpace(code))
            {
                NetworkSend.PlayerMsg(index, "No script code found to compile.", (int)Core.Color.BrightRed);
                Debug.WriteLine("No script code found to compile.");
                return;
            }

            try
            {
                // Use the Roslyn evaluator directly for dynamic code loading
                var evaluator = CSScript.RoslynEvaluator;
                CSScript.EvaluatorConfig.Engine = EvaluatorEngine.Roslyn;

                // Dynamically load and execute the script
                dynamic script = evaluator
                    .ReferenceDomainAssemblies()
                    .LoadCode(code);

                if (script != null)
                {
                    Instance = script;
                    NetworkSend.PlayerMsg(index, "Script saved successfully!", (int)Core.Color.Yellow);
                }
            }
            catch (Exception ex)
            {
                NetworkSend.PlayerMsg(index, $"Script compile error: {ex.Message}", (int)Core.Color.BrightRed);
                Debug.WriteLine($"Script compile error: {ex}");
            }
        }
    }
}
