using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options; // Added for IOptions
using NLua;
using NLua.Exceptions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection; // Added for GetMethod
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core
{
    /// <summary>
    /// Configuration options for the Lua scripting engine.
    /// </summary>
    public class LuaScriptingOptions
    {
        /// <summary>
        /// Gets or sets a value indicating whether scripts should be automatically reloaded when changed on disk.
        /// Defaults to true.
        /// </summary>
        public bool AutoReloadScripts { get; set; } = true;

        /// <summary>
        /// Gets or sets the maximum execution time in milliseconds for a single Lua function call.
        /// Defaults to 5000ms.
        /// </summary>
        public int MaxExecutionTimeMs { get; set; } = 5000;

        /// <summary>
        /// Gets or sets the directory containing the Lua scripts.
        /// Defaults to a "Scripts" subdirectory relative to the application base directory.
        /// </summary>
        public string ScriptsDirectory { get; set; } = System.IO.Path.Combine(AppContext.BaseDirectory, "Scripts");

        /// <summary>
        /// Gets or sets a value indicating whether retrieved Lua functions should be cached for faster subsequent calls.
        /// Defaults to true.
        /// </summary>
        public bool EnableFunctionCaching { get; set; } = true;
    }

    /// <summary>
    /// Manages Lua scripting integration within a .NET application using NLua.
    /// Designed to be registered as a singleton service in a DI container.
    /// </summary>
    public class LuaScripting : IDisposable
    {
        private readonly ILogger<LuaScripting> _logger;
        private readonly LuaScriptingOptions _config;
        private readonly Lua _lua;
        private readonly ConcurrentDictionary<string, DateTime> _scriptLastModified;
        private readonly ConcurrentDictionary<string, LuaFunction> _cachedFunctions;
        private readonly ConcurrentDictionary<string, bool> _requiredModules; // Tracks loaded modules for RequireScript
        private readonly object _luaStateLock = new object(); // Lock for accessing the Lua state
        private FileSystemWatcher _fileWatcher;
        private bool _isInitialized = false;
        private bool _disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="LuaScripting"/> class.
        /// </summary>
        /// <param name="logger">Logger instance.</param>
        /// <param name="configAccessor">Configuration options accessor.</param>
        public LuaScripting(ILogger<LuaScripting> logger, IOptions<LuaScriptingOptions> configAccessor)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _config = configAccessor?.Value ?? throw new ArgumentNullException(nameof(configAccessor));
            _scriptLastModified = new ConcurrentDictionary<string, DateTime>(StringComparer.OrdinalIgnoreCase);
            _cachedFunctions = new ConcurrentDictionary<string, LuaFunction>();
            _requiredModules = new ConcurrentDictionary<string, bool>(StringComparer.OrdinalIgnoreCase);

            // Ensure the scripts directory exists
            try
            {
                if (!Directory.Exists(_config.ScriptsDirectory))
                {
                    Directory.CreateDirectory(_config.ScriptsDirectory);
                    _logger.LogInformation("Created scripts directory: {Directory}", _config.ScriptsDirectory);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create or access scripts directory: {Directory}", _config.ScriptsDirectory);
                // Depending on requirements, you might want to throw here or continue without script loading/watching
            }

            _logger.LogInformation("Initializing Lua state...");
            _lua = new Lua
            {
                UseTraceback = true // Enable detailed stack traces for better debugging
            };
            _lua.State.Encoding = Encoding.UTF8;

            InitializeStandardLibraries();
            RegisterCoreFunctions();
            _logger.LogInformation("Lua state initialized.");
        }

        /// <summary>
        /// Asynchronously initializes the Lua scripting environment by loading initial scripts
        /// and setting up the file watcher if configured.
        /// This should be called once after the service is created.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        public async Task InitializeAsync(CancellationToken cancellationToken = default)
        {
            if (_isInitialized)
            {
                _logger.LogWarning("LuaScripting is already initialized.");
                return;
            }
            if (_disposed) ThrowDisposed();

            _logger.LogInformation("Starting LuaScripting initialization...");

            await LoadAllScriptsAsync(cancellationToken); // Initial load

            if (_config.AutoReloadScripts)
            {
                SetupFileWatcher();
            }

            _isInitialized = true;
            _logger.LogInformation("LuaScripting initialization complete.");
        }

        #region Core Functionality

        private void InitializeStandardLibraries()
        {
            lock (_luaStateLock) // Protect Lua state access
            {
                _lua.LoadCLRPackage(); // Needed for import()
                _lua.DoString(@"
                    -- Optional: Define a custom import function if needed, or rely on LoadCLRPackage
                    -- function import(namespace) clr.System.Reflection.Assembly.Load(namespace) end

                    -- Import commonly used namespaces
                    import('System')
                    import('System.Collections.Generic')
                    -- Add other necessary .NET namespaces here
                    -- import('Core') -- If 'Core' is the assembly name containing relevant types
                ");
                // Consider customizing Lua's package.path if scripts need to 'require' other Lua modules
                // string currentPath = _lua.GetString("package.path");
                // _lua.DoString($"package.path = package.path .. ';{_config.ScriptsDirectory.Replace("\\", "/")}/?.lua'");
            }
            _logger.LogDebug("Standard libraries and CLR package loaded.");
        }

        private void RegisterCoreFunctions()
        {
            // Use delegates for slightly better performance and type safety than GetMethod reflection
            RegisterFunction("print", (Action<string>)LogMessage);
            RegisterFunction("getTimestamp", (Func<long>)GetTimestamp);
            RegisterFunction("require", (Func<string, object[]>)RequireScript);
            RegisterFunction("setGlobal", (Action<string, object>)SetGlobalVariable);
            RegisterFunction("getGlobal", (Func<string, object>)GetGlobalVariable);
            // Add other core C# functions accessible from Lua here
            _logger.LogDebug("Core C# functions registered.");
        }

        /// <summary>
        /// Executes a Lua script provided as a string.
        /// </summary>
        /// <param name="scriptContent">The Lua code to execute.</param>
        /// <param name="chunkName">An optional name for the script chunk (used in error reporting).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>An array of objects returned by the script.</returns>
        public async Task<object[]> ExecuteCodeAsync(string scriptContent, string chunkName = "dynamic_code", CancellationToken cancellationToken = default)
        {
             if (_disposed) ThrowDisposed();
             if (!_isInitialized) _logger.LogWarning("Executing code before initialization is complete.");

            try
            {
                // NLua DoString is blocking, run it in a background thread.
                return await Task.Run(() =>
                {
                    lock (_luaStateLock) // Protect Lua state access
                    {
                         cancellationToken.ThrowIfCancellationRequested();
                        // Consider clearing relevant parts of the cache if dynamic code affects existing functions/globals
                        // ClearFunctionCache();
                        return _lua.DoString(scriptContent, chunkName);
                    }
                }, cancellationToken).TimeoutAfter(_config.MaxExecutionTimeMs, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                 _logger.LogWarning("Lua code execution was cancelled: {ChunkName}", chunkName);
                 throw;
            }
            catch (TimeoutException)
            {
                _logger.LogError("Lua code execution timed out: {ChunkName}", chunkName);
                throw new LuaScriptException($"Execution of Lua code timed out ({_config.MaxExecutionTimeMs}ms).", chunkName);
            }
            catch (LuaException ex)
            {
                _logger.LogError(ex, "Error executing Lua code [{ChunkName}]: {ErrorMessage}\nLua Stack Trace:\n{LuaStackTrace}", chunkName, ex.Message, ex.StackTrace);
                throw new LuaScriptException($"Error executing Lua code [{chunkName}]: {ex.Message}", chunkName, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error executing Lua code: {ChunkName}", chunkName);
                throw new LuaScriptException($"An unexpected error occurred while executing Lua code [{chunkName}].", chunkName, ex);
            }
        }

        /// <summary>
        /// Loads and executes a Lua script from the specified file path.
        /// </summary>
        /// <param name="filePath">The absolute path to the Lua script file.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>An array of objects returned by the script execution.</returns>
        public async Task<object[]> LoadScriptFromFileAsync(string filePath, CancellationToken cancellationToken = default)
        {
            if (_disposed) ThrowDisposed();
            ValidateFilePath(filePath);

            string scriptContent;
            try
            {
                scriptContent = await File.ReadAllTextAsync(filePath, Encoding.UTF8, cancellationToken);
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                _logger.LogError(ex, "Failed to read Lua script file: {FilePath}", filePath);
                throw new LuaScriptException($"Failed to read script file '{filePath}'.", System.IO.Path.GetFileName(filePath), ex);
            }

            _logger.LogDebug("Executing script file: {FilePath}", filePath);
            var result = await ExecuteCodeAsync(scriptContent, System.IO.Path.GetFileName(filePath), cancellationToken);

            // Update last modified time and clear relevant caches
            _scriptLastModified[filePath] = File.GetLastWriteTimeUtc(filePath);
            _requiredModules.TryRemove(filePath, out _); // Allow re-requiring after manual load/reload
            if (_config.EnableFunctionCaching)
            {
                 ClearFunctionCache(); // Simplest approach; could be more granular
            }
            _logger.LogInformation("Successfully loaded and executed Lua script: {FilePath}", filePath);
            return result;
        }

        /// <summary>
        /// Executes a previously loaded Lua function by its global name.
        /// </summary>
        /// <param name="functionName">The global name of the Lua function to execute.</param>
        /// <param name="args">Arguments to pass to the Lua function.</param>
        /// <returns>An array of objects returned by the Lua function.</returns>
        public Task<object[]> ExecuteFunctionAsync(string functionName, params object[] args)
            => ExecuteFunctionAsync(functionName, CancellationToken.None, args);


        /// <summary>
        /// Executes a previously loaded Lua function by its global name with cancellation support.
        /// </summary>
        /// <param name="functionName">The global name of the Lua function to execute.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <param name="args">Arguments to pass to the Lua function.</param>
        /// <returns>An array of objects returned by the Lua function.</returns>
        public async Task<object[]> ExecuteFunctionAsync(string functionName, CancellationToken cancellationToken, params object[] args)
        {
            if (string.IsNullOrWhiteSpace(functionName))
                throw new ArgumentException("Function name cannot be empty.", nameof(functionName));
            if (_disposed) ThrowDisposed();
             if (!_isInitialized) _logger.LogWarning("Executing function '{FunctionName}' before initialization is complete.", functionName);


            LuaFunction function = null;

            try
            {
                // Get the function (potentially cached) - Protected by lock inside GetOrAdd
                function = GetOrAddCachedFunction(functionName);

                if (function == null)
                {
                    throw new LuaScriptException($"Function '{functionName}' not found in the Lua state.", functionName);
                }

                 _logger.LogDebug("Executing Lua function: {FunctionName}", functionName);
                // NLua function.Call is blocking, run it in a background thread.
                return await Task.Run(() =>
                {
                    lock (_luaStateLock) // Protect Lua state access during call
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        return function.Call(args);
                    }
                }, cancellationToken).TimeoutAfter(_config.MaxExecutionTimeMs, cancellationToken);
            }
             catch (OperationCanceledException)
            {
                 _logger.LogWarning("Lua function execution was cancelled: {FunctionName}", functionName);
                 throw;
            }
            catch (TimeoutException)
            {
                _logger.LogError("Lua function execution timed out: {FunctionName}", functionName);
                // Optionally try to interrupt the Lua state here if NLua supports it, though it's often difficult.
                throw new LuaScriptException($"Execution of Lua function '{functionName}' timed out ({_config.MaxExecutionTimeMs}ms).", functionName);
            }
            catch (LuaException ex) // Catch NLua specific exceptions
            {
                // Log with detailed info if available
                _logger.LogError(ex, "Error executing Lua function '{FunctionName}': {ErrorMessage}\nLua Stack Trace:\n{LuaStackTrace}", functionName, ex.Message, ex.StackTrace);
                // Wrap in a custom exception
                throw new LuaScriptException($"Error executing Lua function '{functionName}': {ex.Message}", functionName, ex);
            }
            catch (Exception ex) // Catch other potential exceptions
            {
                 _logger.LogError(ex, "Unexpected error executing Lua function: {FunctionName}", functionName);
                throw new LuaScriptException($"An unexpected error occurred while executing function '{functionName}'.", functionName, ex);
            }
        }

        #endregion

        #region Script Management & Caching

        /// <summary>
        /// Loads all Lua scripts (*.lua) found in the configured scripts directory and its subdirectories.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        private async Task LoadAllScriptsAsync(CancellationToken cancellationToken = default)
        {
            if (!Directory.Exists(_config.ScriptsDirectory))
            {
                _logger.LogWarning("Scripts directory not found, skipping initial load: {Directory}", _config.ScriptsDirectory);
                return;
            }

            _logger.LogInformation("Loading all scripts from: {Directory}", _config.ScriptsDirectory);
            var scriptFiles = Directory.EnumerateFiles(_config.ScriptsDirectory, "*.lua", SearchOption.AllDirectories);
            var loadTasks = new List<Task>();

            foreach (var file in scriptFiles)
            {
                cancellationToken.ThrowIfCancellationRequested();
                // Execute LoadScriptFromFileAsync for each file, but don't await immediately
                 loadTasks.Add(LoadScriptFromFileAsync(file, cancellationToken).ContinueWith(t =>
                 {
                     if (t.IsFaulted)
                     {
                        // Log errors occurred during individual script loading
                        _logger.LogError(t.Exception?.InnerException ?? t.Exception, "Failed to load script during bulk load: {File}", file);
                     }
                     // Don't rethrow here, allow other scripts to load. AggregateException will be thrown by Task.WhenAll if needed.
                 }, cancellationToken));
            }

            try
            {
                await Task.WhenAll(loadTasks);
                _logger.LogInformation("Finished loading all scripts.");
            }
            catch (Exception ex) // Catches AggregateException from WhenAll
            {
                 _logger.LogError(ex, "One or more scripts failed to load during bulk load.");
                 // Decide if this is a fatal error for initialization
                 throw; // Rethrow if initialization should fail
            }
        }

        /// <summary>
        /// Manually reloads a specific Lua script from its file path.
        /// </summary>
        /// <param name="filePath">The absolute path to the Lua script file.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public async Task ReloadScriptAsync(string filePath, CancellationToken cancellationToken = default)
        {
             if (_disposed) ThrowDisposed();
            _logger.LogInformation("Attempting manual reload of script: {FilePath}", filePath);
            // Essentially the same as loading it, which replaces existing definitions
            // and updates the timestamp/cache state.
            await LoadScriptFromFileAsync(filePath, cancellationToken);
        }

        /// <summary>
        /// Manually reloads all Lua scripts from the configured scripts directory.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        public async Task ReloadAllScriptsAsync(CancellationToken cancellationToken = default)
        {
             if (_disposed) ThrowDisposed();
             if (!_isInitialized)
             {
                 _logger.LogWarning("Attempting to reload all scripts before initial initialization.");
                 // Optionally, just run InitializeAsync again or throw
                 await InitializeAsync(cancellationToken);
                 return;
             }

            _logger.LogInformation("Attempting manual reload of all scripts...");
             // Clear existing state before reloading all
            ClearFunctionCache();
            _requiredModules.Clear();
            _scriptLastModified.Clear();
            // Consider resetting specific global variables if necessary

            await LoadAllScriptsAsync(cancellationToken);
        }

        /// <summary>
        /// Clears the cached Lua functions. Functions will be re-fetched from the Lua state on next execution.
        /// </summary>
        public void ClearFunctionCache()
        {
            if (_disposed) return; // Don't throw if disposing
            _cachedFunctions.Clear();
            _logger.LogInformation("Cleared the Lua function cache.");
        }

        private LuaFunction GetOrAddCachedFunction(string functionName)
        {
            if (!_config.EnableFunctionCaching)
            {
                lock (_luaStateLock) // Protect Lua state access
                {
                    return _lua.GetFunction(functionName);
                }
            }

            // ConcurrentDictionary's GetOrAdd ensures the factory runs only once per key if needed.
            return _cachedFunctions.GetOrAdd(functionName, (name) =>
            {
                 _logger.LogDebug("Cache miss. Fetching Lua function from state: {FunctionName}", name);
                lock (_luaStateLock) // Protect Lua state access during fetch
                {
                    var func = _lua.GetFunction(name);
                    if(func == null)
                    {
                        _logger.LogWarning("Attempted to cache function '{FunctionName}', but it was not found in the Lua state.", name);
                        // Remove the key if the factory failed to produce a valid function to avoid caching nulls permanently.
                         _cachedFunctions.TryRemove(name, out _);
                    }
                    return func;
                }
            });
        }
        #endregion

        #region Interop and Globals

        /// <summary>
        /// Implements Lua's 'require' logic. Loads and executes a script if not already loaded.
        /// Tries to prevent re-execution of the same script path during the lifetime of the Lua state.
        /// </summary>
        /// <param name="modulePath">The path of the script/module to load, relative to the ScriptsDirectory or absolute.</param>
        /// <returns>The value returned by the required script, or null/empty array.</returns>
        public object[] RequireScript(string modulePath)
        {
             if (_disposed) ThrowDisposed();
            string fullPath = ResolveScriptPath(modulePath);
            ValidateFilePath(fullPath); // Ensure it exists after resolving

            // Check if already required
            if (_requiredModules.ContainsKey(fullPath))
            {
                _logger.LogDebug("Module already required: {ModulePath} ({FullPath})", modulePath, fullPath);
                return null; // Lua's require typically returns the cached result, here we just prevent re-execution
            }

            try
            {
                _logger.LogInformation("Requiring Lua module: {ModulePath} ({FullPath})", modulePath, fullPath);
                string scriptContent = File.ReadAllText(fullPath, Encoding.UTF8);

                object[] result;
                 // Execute the script's content. Protect Lua state access.
                lock (_luaStateLock)
                {
                    result = _lua.DoString(scriptContent, System.IO.Path.GetFileName(fullPath));
                }

                // Mark as required *after* successful execution
                _requiredModules.TryAdd(fullPath, true);
                _scriptLastModified[fullPath] = File.GetLastWriteTimeUtc(fullPath); // Track modification time

                 // Lua 'require' typically returns the value returned by the module chunk.
                _logger.LogDebug("Module required successfully: {FullPath}", fullPath);
                return result;
            }
            catch (LuaException ex)
            {
                _logger.LogError(ex, "Error requiring Lua module '{ModulePath}': {ErrorMessage}\nLua Stack Trace:\n{LuaStackTrace}", modulePath, ex.Message, ex.StackTrace);
                throw new LuaScriptException($"Error requiring module '{modulePath}' ({fullPath}): {ex.Message}", modulePath, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error requiring Lua module: {ModulePath} ({FullPath})", modulePath, fullPath);
                throw new LuaScriptException($"An unexpected error occurred while requiring module '{modulePath}' ({fullPath}).", modulePath, ex);
            }
        }


        /// <summary>
        /// Sets a global variable in the Lua state.
        /// </summary>
        /// <param name="name">The name of the global variable.</param>
        /// <param name="value">The value to set. Can be primitive types, CLR objects, LuaTables, etc.</param>
        public void SetGlobalVariable(string name, object value)
        {
             if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Global variable name cannot be empty.", nameof(name));
             if (_disposed) ThrowDisposed();

            lock (_luaStateLock) // Protect Lua state access
            {
                _lua[name] = value;
            }
            _logger.LogTrace("Set global Lua variable '{Name}'", name); // Avoid logging potentially sensitive values by default
        }

        /// <summary>
        /// Gets the value of a global variable from the Lua state.
        /// </summary>
        /// <param name="name">The name of the global variable.</param>
        /// <returns>The value of the global variable, or null if not found.</returns>
        public object GetGlobalVariable(string name)
        {
             if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Global variable name cannot be empty.", nameof(name));
             if (_disposed) ThrowDisposed();

            object value;
            lock (_luaStateLock) // Protect Lua state access
            {
                value = _lua[name];
            }
             _logger.LogTrace("Retrieved global Lua variable '{Name}'", name);
            return value;
        }

        /// <summary>
        /// Registers a C# delegate (Action or Func) as a global Lua function.
        /// </summary>
        /// <param name="luaFunctionName">The name the function will have in Lua.</param>
        /// <param name="delegateToRegister">The C# delegate (method group, lambda, Action, Func) to register.</param>
        public void RegisterFunction(string luaFunctionName, Delegate delegateToRegister)
        {
            if (string.IsNullOrWhiteSpace(luaFunctionName))
                throw new ArgumentException("Lua function name cannot be empty.", nameof(luaFunctionName));
            ArgumentNullException.ThrowIfNull(delegateToRegister);
             if (_disposed) ThrowDisposed();


            try
            {
                 lock (_luaStateLock) // Protect Lua state access
                 {
                     // Use the specific overload accepting Delegate directly
                     _lua.RegisterFunction(luaFunctionName, delegateToRegister.Target, delegateToRegister.Method);
                 }
                _logger.LogInformation("Registered C# function '{MethodName}' as '{LuaFunctionName}' in Lua.", delegateToRegister.Method.Name, luaFunctionName);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Failed to register C# function {LuaFunctionName}", luaFunctionName);
                throw; // Rethrow exception after logging
            }
        }


        /// <summary>
        /// Registers a C# object instance, making its public methods and properties accessible globally in Lua.
        /// </summary>
        /// <param name="nameInLua">The global name the object will have in Lua.</param>
        /// <param name="objInstance">The C# object instance.</param>
        /// <remarks>
        /// Be cautious about exposing complex objects. Only public members are typically accessible.
        /// Consider creating dedicated interface objects for Lua interaction.
        /// </remarks>
        public void RegisterObject(string nameInLua, object objInstance)
        {
             if (string.IsNullOrWhiteSpace(nameInLua))
                throw new ArgumentException("Name in Lua cannot be empty.", nameof(nameInLua));
            ArgumentNullException.ThrowIfNull(objInstance);
             if (_disposed) ThrowDisposed();


            lock (_luaStateLock) // Protect Lua state access
            {
                _lua[nameInLua] = objInstance; // Directly assign the object
            }
            _logger.LogInformation("Registered .NET object of type '{TypeName}' as '{NameInLua}' in Lua.", objInstance.GetType().Name, nameInLua);
        }

        /// <summary>
        /// Gets the underlying NLua Lua state object. Use with caution.
        /// Access should be synchronized externally if used outside this class instance methods.
        /// </summary>
        public Lua GetLuaStateUnsafe() => _lua;

        /// <summary>
        /// Provides direct, synchronized access to the underlying Lua state for advanced operations.
        /// </summary>
        /// <param name="action">The action to perform with the Lua state. The Lua state is locked during execution.</param>
        public void AccessLuaStateSafe(Action<Lua> action)
        {
            ArgumentNullException.ThrowIfNull(action);
            if (_disposed) ThrowDisposed();

            lock (_luaStateLock)
            {
                action(_lua);
            }
        }

        #endregion

        #region Utility Methods

        /// <summary>
        /// Logs a message using the configured logger. Intended to be called from Lua via the registered 'print' function.
        /// </summary>
        public void LogMessage(string message)
        {
            // Called from Lua, already within a lock if called during script execution/function call
            _logger.LogInformation("[LUA] {Message}", message);
             // Optionally write to Console as well, depending on application needs
             // Console.WriteLine($"[LUA] {message}");
        }

        /// <summary>
        /// Gets the current UTC timestamp as Unix seconds. Intended to be called from Lua.
        /// </summary>
        public long GetTimestamp()
        {
            // Called from Lua, already within a lock if called during script execution/function call
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }

        private void ValidateFilePath(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path cannot be null or whitespace.", nameof(filePath));
            if (!System.IO.Path.IsPathRooted(filePath))
                 throw new ArgumentException($"File path must be absolute: '{filePath}'", nameof(filePath));
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Script file not found: {filePath}", filePath);
        }

        private string ResolveScriptPath(string scriptPath)
        {
            if (string.IsNullOrWhiteSpace(scriptPath)) return scriptPath;
            // If it's already rooted, use it directly. Otherwise, combine with ScriptsDirectory.
            if (System.IO.Path.IsPathRooted(scriptPath))
            {
                return scriptPath;
            }
            else
            {
                // Normalize potential relative paths like '../' etc.
                return System.IO.Path.GetFullPath(System.IO.Path.Combine(_config.ScriptsDirectory, scriptPath));
            }
        }

         private void ThrowDisposed()
         {
              throw new ObjectDisposedException(nameof(LuaScripting));
         }
        #endregion

        #region File Watching (Auto-Reload)

        private void SetupFileWatcher()
        {
            if (!Directory.Exists(_config.ScriptsDirectory))
            {
                _logger.LogWarning("Cannot set up FileSystemWatcher: Scripts directory does not exist ({Directory}). Auto-reload disabled.", _config.ScriptsDirectory);
                return;
            }
             if (_disposed) return; // Don't setup if already disposed

            try
            {
                _fileWatcher = new FileSystemWatcher(_config.ScriptsDirectory)
                {
                    NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName, // Watch for writes and renames
                    IncludeSubdirectories = true, // Watch subdirectories
                    Filter = "*.lua", // Only watch Lua files
                    EnableRaisingEvents = true // Start watching
                };

                // Use a debounce mechanism if necessary, but basic handling first
                _fileWatcher.Changed += OnScriptFileChanged;
                _fileWatcher.Created += OnScriptFileChanged; // Treat created same as changed
                _fileWatcher.Renamed += OnScriptFileRenamed;
                _fileWatcher.Error += OnWatcherError; // Handle watcher errors

                _logger.LogInformation("FileSystemWatcher started for: {Directory}", _config.ScriptsDirectory);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Failed to initialize FileSystemWatcher for {Directory}. Auto-reload disabled.", _config.ScriptsDirectory);
                 _fileWatcher?.Dispose(); // Clean up partial setup
                 _fileWatcher = null;
            }
        }

        private void OnWatcherError(object sender, ErrorEventArgs e)
        {
            // Log errors from the FileSystemWatcher itself
             _logger.LogError(e.GetException(), "FileSystemWatcher encountered an error.");
             // Consider attempting to recreate the watcher if it's a recoverable error
        }

        // Shared handler for Created and Changed events
        private void OnScriptFileChanged(object sender, FileSystemEventArgs e)
        {
            _logger.LogDebug("FileSystemWatcher event: {ChangeType} for {FullPath}", e.ChangeType, e.FullPath);
            // Basic debounce: Check if the file's write time actually changed recently.
            // This helps filter out duplicate events or events from saving without changes.
             if (!ShouldReload(e.FullPath)) return;

            _logger.LogInformation("Detected change in script file: {FilePath}. Reloading...", e.FullPath);

            // Reload asynchronously but don't wait for it here (fire and forget in event handler)
            // Capture context for async operation
             var filePath = e.FullPath;
             Task.Run(async () => {
                try
                {
                    // Wait a very brief moment in case of rapid save operations
                    await Task.Delay(100);
                    if (!ShouldReload(filePath)) return; // Double check after delay

                    // LoadScriptFromFileAsync handles locking, cache clearing, and updates _scriptLastModified
                    await LoadScriptFromFileAsync(filePath);
                }
                 catch (FileNotFoundException)
                 {
                     _logger.LogWarning("Script file {FilePath} not found during reload (possibly deleted quickly after change event).", filePath);
                     // Clean up state if file is gone
                     _scriptLastModified.TryRemove(filePath, out _);
                     _requiredModules.TryRemove(filePath, out _);
                 }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error auto-reloading script: {FilePath}", filePath);
                    // Optionally, update state to reflect failed reload
                }
             });
        }

        private void OnScriptFileRenamed(object sender, RenamedEventArgs e)
        {
             _logger.LogDebug("FileSystemWatcher event: Renamed from {OldFullPath} to {FullPath}", e.OldFullPath, e.FullPath);

             // Remove state associated with the old path
             _scriptLastModified.TryRemove(e.OldFullPath, out _);
             _requiredModules.TryRemove(e.OldFullPath, out _);
             // Potentially clear specific cached functions related to the old file if identifiable

             // Treat the new path as a changed/created file
             OnScriptFileChanged(sender, new FileSystemEventArgs(WatcherChangeTypes.Changed, System.IO.Path.GetDirectoryName(e.FullPath), System.IO.Path.GetFileName(e.FullPath)));
        }

        private bool ShouldReload(string filePath)
        {
            // Suppress reload if disposed or file doesn't exist
            if (_disposed || !File.Exists(filePath)) return false;

            try
            {
                var currentWriteTime = File.GetLastWriteTimeUtc(filePath);
                // Check if we have a previous time and if the current time is newer
                if (_scriptLastModified.TryGetValue(filePath, out var lastWriteTime))
                {
                    // Add a small tolerance (e.g., 1 second) to avoid issues with file system timestamp precision
                    return (currentWriteTime - lastWriteTime) > TimeSpan.FromSeconds(1);
                }
                return true; // No previous time recorded, so reload
            }
            catch(Exception ex)
            {
                _logger.LogWarning(ex, "Could not get LastWriteTime for {FilePath} during reload check.", filePath);
                return false; // Don't reload if we can't check the time
            }
        }


        #endregion

        #region Resource Management

        /// <summary>
        /// Disposes the Lua state and stops the file watcher.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _logger.LogInformation("Disposing LuaScripting instance...");

                 // Stop file watcher first
                if (_fileWatcher != null)
                {
                    _fileWatcher.EnableRaisingEvents = false;
                    _fileWatcher.Changed -= OnScriptFileChanged;
                    _fileWatcher.Created -= OnScriptFileChanged;
                    _fileWatcher.Renamed -= OnScriptFileRenamed;
                    _fileWatcher.Error -= OnWatcherError;
                    _fileWatcher.Dispose();
                    _fileWatcher = null;
                     _logger.LogDebug("FileSystemWatcher disposed.");
                }

                // Dispose Lua state (acquires lock internally in NLua)
                // Use lock here to ensure no other operation tries to use _lua while disposing
                lock(_luaStateLock)
                {
                    _lua?.Dispose();
                     _logger.LogDebug("Lua state disposed.");
                }

                 // Clear managed resources
                _cachedFunctions.Clear();
                _scriptLastModified.Clear();
                _requiredModules.Clear();
            }

            _disposed = true;
            _logger.LogInformation("LuaScripting instance disposed.");
        }

        // Finalizer as a safeguard in case Dispose is not called
         ~LuaScripting()
         {
             Dispose(false);
         }
        #endregion
    }

    /// <summary>
    /// Represents an exception that occurred during Lua script execution or management.
    /// </summary>
    public class LuaScriptException : Exception
    {
        /// <summary>
        /// The name of the script file, function, or code chunk where the error occurred, if available.
        /// </summary>
        public string Source { get; }

        public LuaScriptException(string message, string source = null, Exception innerException = null)
            : base(message, innerException)
        {
            Source = source;
        }
    }

    /// <summary>
    /// Provides extension methods for Task operations.
    /// </summary>
    public static class TaskExtensions
    {
        /// <summary>
        /// Applies a timeout to a task.
        /// </summary>
        /// <typeparam name="T">The result type of the task.</typeparam>
        /// <param name="task">The task to which the timeout is applied.</param>
        /// <param name="milliseconds">The timeout duration in milliseconds.</param>
        /// <param name="cancellationToken">Optional external cancellation token.</param>
        /// <returns>The result of the task.</returns>
        /// <exception cref="TimeoutException">Thrown if the task does not complete within the specified duration.</exception>
         /// <exception cref="OperationCanceledException">Thrown if the external cancellation token is signaled.</exception>
        public static async Task<T> TimeoutAfter<T>(this Task<T> task, int milliseconds, CancellationToken cancellationToken = default)
        {
            if (milliseconds <= 0) return await task; // No timeout

            using var timeoutCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken); // Link external token
            var timeoutTask = Task.Delay(milliseconds, timeoutCts.Token);
            var completedTask = await Task.WhenAny(task, timeoutTask);

            if (completedTask == task)
            {
                // Task completed before timeout. Cancel the timeout delay task.
                timeoutCts.Cancel();
                 // Propagate exceptions from the original task
                return await task;
            }
            else // Timeout task completed first
            {
                 // Check if the timeout occurred because the external token was cancelled
                 cancellationToken.ThrowIfCancellationRequested();
                 // Otherwise, it was a genuine timeout
                throw new TimeoutException($"The operation timed out after {milliseconds}ms.");
            }
        }

         /// <summary>
        /// Applies a timeout to a non-generic task.
        /// </summary>
        public static async Task TimeoutAfter(this Task task, int milliseconds, CancellationToken cancellationToken = default)
        {
             if (milliseconds <= 0)
             {
                 await task; // No timeout, just await original task
                 return;
             }

            using var timeoutCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken); // Link external token
            var timeoutTask = Task.Delay(milliseconds, timeoutCts.Token);
            var completedTask = await Task.WhenAny(task, timeoutTask);

            if (completedTask == task)
            {
                timeoutCts.Cancel();
                await task; // Propagate exceptions
            }
            else
            {
                 cancellationToken.ThrowIfCancellationRequested();
                throw new TimeoutException($"The operation timed out after {milliseconds}ms.");
            }
        }
    }
}
