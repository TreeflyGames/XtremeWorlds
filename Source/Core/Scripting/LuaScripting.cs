using Microsoft.Extensions.Logging;
using NLua;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core
{
    /// <summary>
    /// A singleton class that manages Lua scripting within a .NET application using NLua.
    /// </summary>
    public class LuaScripting : IDisposable
    {
        public static event EventHandler InstanceCreated;
        private static readonly Lazy<LuaScripting> _instance = new Lazy<LuaScripting>(() => new LuaScripting());
        private readonly Lua _lua;
        private readonly ILogger<LuaScripting> _logger;
        private readonly ConcurrentDictionary<string, DateTime> _scriptLastModified;
        private readonly ConcurrentDictionary<string, LuaFunction> _cachedFunctions;
        private bool _disposed = false;
        private readonly object _lock = new object();

        /// <summary>
        /// Configuration options for the Lua scripting engine.
        /// </summary>
        public class Config
        {
            public bool AutoReloadScripts { get; set; } = true;
            public int MaxExecutionTimeMs { get; set; } = 5000;
            public string ScriptsDirectory { get; set; } = Path.Scripts;
            public bool EnableScriptCaching { get; set; } = true;
        }

        private readonly Config _config;

        #region Singleton Pattern
        /// <summary>
        /// Gets the singleton instance of the LuaScripting class.
        /// </summary>
        public static LuaScripting Instance => _instance.Value;

        /// <summary>
        /// Resets the singleton instance by disposing the current one and creating a new one.
        /// </summary>
        public static LuaScripting ResetInstance()
        {
            if (_instance.IsValueCreated)
            {
                _instance.Value.Dispose();
            }
            return Instance;
        }
        #endregion

        #region Constructor and Initialization
        private LuaScripting(ILogger<LuaScripting> logger = null, Config config = null)
        {
            _logger = logger;
            _config = config ?? new Config();
            _scriptLastModified = new ConcurrentDictionary<string, DateTime>();
            _cachedFunctions = new ConcurrentDictionary<string, LuaFunction>();

            _lua = new Lua
            {
                UseTraceback = true // Enable stack traces for better debugging
            };
            _lua.State.Encoding = Encoding.UTF8;

            InitializeStandardLibraries();
            RegisterCoreFunctions();
            LoadScriptsAsync().GetAwaiter().GetResult();

            InstanceCreated?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region Core Functionality
        private void InitializeStandardLibraries()
        {
            _lua.LoadCLRPackage();
            _lua.DoString("import('System')");
            _lua.DoString("import('Core')");
        }

        private void RegisterCoreFunctions()
        {
            _lua.RegisterFunction("print", this, typeof(LuaScripting).GetMethod(nameof(LogMessage)));
            _lua.RegisterFunction("getTimestamp", this, typeof(LuaScripting).GetMethod(nameof(GetTimestamp)));
            _lua.RegisterFunction("require", this, typeof(LuaScripting).GetMethod(nameof(RequireScript)));
            _lua.RegisterFunction("setGlobal", this, typeof(LuaScripting).GetMethod(nameof(SetGlobalVariable)));
            _lua.RegisterFunction("getGlobal", this, typeof(LuaScripting).GetMethod(nameof(GetGlobalVariable)));
        }

        private async Task LoadScriptsAsync()
        {
            if (!Directory.Exists(_config.ScriptsDirectory)) return;

            var scriptFiles = Directory.GetFiles(_config.ScriptsDirectory, "*.lua", SearchOption.AllDirectories);
            var loadTasks = scriptFiles.Select(async file =>
            {
                try
                {
                    await AddScriptFromFileAsync(file, false);
                    _scriptLastModified.TryAdd(file, File.GetLastWriteTime(file));
                    _logger?.LogInformation("Loaded Lua script: {File}", file);
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex, "Failed to load Lua script: {File}", file);
                }
            });

            await Task.WhenAll(loadTasks);
        }

        /// <summary>
        /// Adds and executes a Lua script from a string.
        /// </summary>
        public async Task<object[]> AddScriptAsync(string script, bool clearFunctionCache = true)
        {
            return await Task.Run(() =>
            {
                lock (_lock)
                {
                    try
                    {
                        var results = _lua.DoString(script);
                        if (clearFunctionCache && _config.EnableScriptCaching)
                        {
                            ClearFunctionCache();
                        }
                        return results;
                    }
                    catch (Exception ex)
                    {
                        _logger?.LogError(ex, "Error executing Lua script");
                        throw new LuaScriptException($"Error adding Lua script: {ex.Message}", ex);
                    }
                }
            });
        }

        /// <summary>
        /// Adds and executes a Lua script from a file.
        /// </summary>
        public async Task<object[]> AddScriptFromFileAsync(string filepath, bool clearFunctionCache = true)
        {
            ValidateFilePath(filepath);

            try
            {
                string script = await File.ReadAllTextAsync(filepath);
                return await AddScriptAsync(script, clearFunctionCache);
            }
            catch (Exception ex)
            {
                throw new LuaScriptException($"Error loading script from file '{filepath}': {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Executes a Lua function with the specified arguments.
        /// </summary>
        public async Task<object[]> ExecuteScriptAsync(string functionName, params object[] args)
        {
            CheckScriptTimeout();

            try
            {
                LuaFunction function = null;
                if (_config.EnableScriptCaching && _cachedFunctions.TryGetValue(functionName, out function))
                {
                    // Use cached function
                }
                else
                {
                    function = _lua.GetFunction(functionName);
                    if (function == null)
                    {
                        throw new LuaScriptException($"Function '{functionName}' not found");
                    }
                    if (_config.EnableScriptCaching)
                    {
                        _cachedFunctions.TryAdd(functionName, function);
                    }
                }

                return await Task.Run(() => function.Call(args))
                    .TimeoutAfter(_config.MaxExecutionTimeMs);
            }
            catch (LuaException lex)
            {
                string stackTrace = lex.StackTrace;
                throw new LuaScriptException($"Error executing Lua function '{functionName}': {lex.Message}\nStack Trace:\n{stackTrace}", lex);
            }
            catch (TimeoutException)
            {
                _logger?.LogError("Lua script execution timed out for function: {FunctionName}", functionName);
                throw;
            }
            catch (Exception ex)
            {
                throw new LuaScriptException($"Error executing Lua function '{functionName}': {ex.Message}", ex);
            }
        }
        #endregion

        #region New Lua Scripting Options and Features
        public object[] RequireScript(string scriptPath)
        {
            string fullPath = Path.IsPathRooted(scriptPath) ? scriptPath : Path.Combine(_config.ScriptsDirectory, scriptPath);
            ValidateFilePath(fullPath);

            try
            {
                var scriptContent = File.ReadAllText(fullPath);
                _logger?.LogInformation("Required and executed Lua script: {Path}", fullPath);
                return _lua.DoString(scriptContent);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error requiring Lua script: {Path}", fullPath);
                throw new LuaScriptException($"Error requiring script '{scriptPath}': {ex.Message}", ex);
            }
        }

        public void SetGlobalVariable(string name, object value)
        {
            _lua[name] = value;
            _logger?.LogDebug("Set global Lua variable '{Name}' to '{Value}'", name, value);
        }

        public object GetGlobalVariable(string name)
        {
            var value = _lua[name];
            _logger?.LogDebug("Retrieved global Lua variable '{Name}' with value '{Value}'", name, value);
            return value;
        }

        public void ClearFunctionCache()
        {
            _cachedFunctions.Clear();
            _logger?.LogInformation("Cleared the Lua function cache.");
        }

        public async Task<object[]> ExecuteCodeAsync(string luaCode)
        {
            return await AddScriptAsync(luaCode);
        }

        public void RegisterObject(string name, object obj)
        {
            _lua[name] = obj;
            _logger?.LogInformation("Registered .NET object '{Type}' as '{Name}' in Lua.", obj.GetType().Name, name);
        }

        public void RegisterStaticClass(string name, Type type)
        {
            _lua.RegisterFunction(name, type, null);
            _logger?.LogInformation("Registered static .NET class '{Type}' as '{Name}' in Lua.", type.Name, name);
        }
        #endregion

        #region Utility Methods
        public void LogMessage(string message)
        {
            _logger?.LogInformation("[LUA] {Message}", message);
            Console.WriteLine($"[LUA] {message}");
        }

        public long GetTimestamp()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }

        private void ValidateFilePath(string filepath)
        {
            if (string.IsNullOrWhiteSpace(filepath))
                throw new ArgumentException("File path cannot be empty", nameof(filepath));
            if (!File.Exists(filepath))
                throw new FileNotFoundException($"Script file not found: {filepath}", filepath);
        }

        private void CheckScriptTimeout()
        {
            if (_config.AutoReloadScripts)
            {
                lock (_lock)
                {
                    foreach (var script in _scriptLastModified)
                    {
                        var currentModified = File.GetLastWriteTime(script.Key);
                        if (currentModified > script.Value)
                        {
                            AddScriptFromFileAsync(script.Key).GetAwaiter().GetResult();
                            _scriptLastModified[script.Key] = currentModified;
                            _logger?.LogInformation("Reloaded Lua script: {File}", script.Key);
                        }
                    }
                }
            }
        }
        #endregion

        #region Manual Script Reloading
        /// <summary>
        /// Manually reloads a specific Lua script from the given file path.
        /// </summary>
        public async Task ReloadScriptAsync(string filepath)
        {
            ValidateFilePath(filepath);
            try
            {
                await AddScriptFromFileAsync(filepath, true);
                _scriptLastModified[filepath] = File.GetLastWriteTime(filepath);
                _logger?.LogInformation("Manually reloaded Lua script: {File}", filepath);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Failed to reload Lua script: {File}", filepath);
                throw new LuaScriptException($"Error reloading Lua script '{filepath}': {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Manually reloads all Lua scripts from the scripts directory.
        /// </summary>
        public async Task ReloadAllScriptsAsync()
        {
            var scriptFiles = Directory.GetFiles(_config.ScriptsDirectory, "*.lua", SearchOption.AllDirectories);
            var reloadTasks = scriptFiles.Select(async file =>
            {
                try
                {
                    await AddScriptFromFileAsync(file, true);
                    _scriptLastModified[file] = File.GetLastWriteTime(file);
                    _logger?.LogInformation("Reloaded Lua script: {File}", file);
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex, "Failed to reload Lua script: {File}", file);
                }
            });

            await Task.WhenAll(reloadTasks);
        }
        #endregion

        #region Resource Management
        public void Dispose()
        {
            if (_disposed) return;

            lock (_lock)
            {
                _lua?.Dispose();
                _disposed = true;
            }
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    public class LuaScriptException : Exception
    {
        public LuaScriptException(string message, Exception innerException = null)
            : base(message, innerException) { }
    }

    public static class TaskExtensions
    {
        public static async Task<T> TimeoutAfter<T>(this Task<T> task, int milliseconds)
        {
            using var cts = new CancellationTokenSource();
            var completedTask = await Task.WhenAny(task, Task.Delay(milliseconds, cts.Token));
            if (completedTask == task)
            {
                cts.Cancel();
                return await task;
            }
            throw new TimeoutException("The operation has timed out.");
        }
    }
}
