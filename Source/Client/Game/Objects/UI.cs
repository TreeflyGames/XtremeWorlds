using Client.Content.Skins;
using Core;
using CSScriptLib;
using Mirage.Sharp.Asfw;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Packets;

namespace Client.Game.Objects
{
    public class UI
    {
        public static dynamic? Instance { get; private set; }

        public static void Load()
        {
            // Load the script file
            var scriptPath = System.IO.Path.Combine(Core.Path.Skins, SettingsManager.Instance.Skin + ".cs");
            if (File.Exists(scriptPath))
            {
                var lines = File.ReadLines(scriptPath, Encoding.UTF8).ToArray();
                if (lines.Length > 0)
                {
                    Core.Data.UI.Code = lines;
                }
                else
                {
                    Core.Data.UI.Code = Array.Empty<string>();
                }
            }
            else
            {
                Core.Data.UI.Code = Array.Empty<string>();
            }

            string code = (Core.Data.UI.Code != null && Core.Data.UI.Code.Length > 0) ? string.Join(Environment.NewLine, Core.Data.UI.Code) : string.Empty;

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
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
