using System;
using System.IO;
using System.Reflection;
using System.Text;
using NLua;

namespace Core
{

    public class LuaScripting
    {
        public static event EventHandler InstanceCreated;
        private static LuaScripting _instance;

        public static LuaScripting Instance()
        {
            if (_instance is null)
            {
                _instance = new LuaScripting();
            }
            return _instance;
        }

        public static LuaScripting ResetInstance()
        {
            if (_instance is not null)
            {
                _instance = null;
            }
            return Instance();
        }

        private readonly Lua lua;

        private LuaScripting()
        {
            // Create the Lua instance.
            lua = new Lua();
            lua.State.Encoding = Encoding.UTF8;

            // Register scripts during initialization
            foreach (var script in Directory.GetFiles(Path.Scripts))
            {
                Console.WriteLine($"Registering Lua script '{script}'");
                AddScriptFromFile(script);
            }

            // Raise the shared event when a new instance is created
            InstanceCreated?.Invoke(this, EventArgs.Empty);
        }

        public object[] AddScript(string script)
        {
            try
            {
                return lua.DoString(script);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding Lua script: {ex.Message}");
            }
        }

        public object[] AddScriptFromFile(string filepath)
        {
            if (string.IsNullOrWhiteSpace(filepath))
            {
                throw new Exception($"Error adding Lua script from file, no file path was provided.");
            }

            if (!File.Exists(filepath))
            {
                throw new Exception($"Error adding Lua script from file, the file '{filepath}' does not exist.");
            }

            using (var fs = new StreamReader(filepath))
            {
                string script = fs.ReadToEnd();
                fs.Close();

                return AddScript(script);
            }
        }

        public object[] ExecuteScript(string functionName, params object[] args)
        {
            try
            {
                return lua.GetFunction(functionName).Call(args);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error calling Lua function: {ex.Message}");
            }
        }

        public LuaFunction RegisterFunction(string path, MethodBase function)
        {
            return RegisterFunction(path, null, function);
        }

        public LuaFunction RegisterFunction(string path, object target, MethodBase function)
        {
            try
            {
                return lua.RegisterFunction(path, target, function);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error registering Lua function: {ex.Message}");
            }
        }
    }
}