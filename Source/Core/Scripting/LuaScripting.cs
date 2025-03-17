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
    public class LuaScripting : IDisposable
    {
        public static event EventHandler InstanceCreated;
        private static readonly Lazy<LuaScripting> _instance = new Lazy<LuaScripting>(() => new LuaScripting());
        private readonly Lua _lua;
        private readonly ILogger<LuaScripting> _logger;
        private readonly ConcurrentDictionary<string, DateTime> _scriptLastModified;
        private bool _disposed = false;
        private readonly object _lock = new object();

        // Configuration options
        public class Config
        {
            public bool AutoReloadScripts { get; set; } = true;
            public int MaxExecutionTimeMs { get; set; } = 5000;
            public string ScriptsDirectory { get; set; } = Path.Scripts;
        }

        private readonly Config _config;

        #region Singleton Pattern
        public static LuaScripting Instance => _instance.Value;

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
        }

        private async Task LoadScriptsAsync()
        {
            if (!Directory.Exists(_config.ScriptsDirectory)) return;

            var scriptFiles = Directory.GetFiles(_config.ScriptsDirectory, "*.lua", SearchOption.AllDirectories);
            var loadTasks = scriptFiles.Select(async file =>
            {
                try
                {
                    await AddScriptFromFileAsync(file);
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

        public async Task<object[]> AddScriptAsync(string script)
        {
            return await Task.Run(() =>
            {
                try
                {
                    return _lua.DoString(script);
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex, "Error executing Lua script");
                    throw new LuaScriptException($"Error adding Lua script: {ex.Message}", ex);
                }
            });
        }

        public async Task<object[]> AddScriptFromFileAsync(string filepath)
        {
            ValidateFilePath(filepath);
            
            try
            {
                string script = await File.ReadAllTextAsync(filepath);
                return await AddScriptAsync(script);
            }
            catch (Exception ex)
            {
                throw new LuaScriptException($"Error loading script from file '{filepath}': {ex.Message}", ex);
            }
        }

        public async Task<object[]> ExecuteScriptAsync(string functionName, params object[] args)
        {
            CheckScriptTimeout();
            
            try
            {
                var function = _lua.GetFunction(functionName);
                if (function == null)
                    throw new LuaScriptException($"Function '{functionName}' not found");

                return await Task.Run(() => function.Call(args))
                    .TimeoutAfter(_config.MaxExecutionTimeMs);
            }
            catch (Exception ex)
            {
                throw new LuaScriptException($"Error executing Lua function '{functionName}': {ex.Message}", ex);
            }
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
                        }
                    }
                }
            }
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

    #region Custom Exception
    public class LuaScriptException : Exception
    {
        public LuaScriptException(string message, Exception innerException = null) 
            : base(message, innerException) { }
    }
    #endregion

    #region Extensions
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
    #endregion
}
