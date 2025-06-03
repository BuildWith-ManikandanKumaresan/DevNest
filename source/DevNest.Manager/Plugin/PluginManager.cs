#region using directives
using DevNest.Common.Base.Constants;
using DevNest.Common.Base.Contracts;
using DevNest.Common.Logger;
using DevNest.Manager.FileSystem;
using DevNest.Plugin.Contracts;
using System.Reflection;
using System.Runtime.Loader;
#endregion using directives7

namespace DevNest.Manager.Plugin
{
    /// <summary>
    /// Represents the class instance for managing plugins in the application.
    /// </summary>
    public class PluginManager : IPluginManager
    {
        private readonly IFileSystemManager _fileSystemManager;
        private readonly IApplicationLogger<PluginManager> _logger;
        private readonly IList<IStoragePlugin>? _pluginInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginManager"/> class.
        /// </summary>
        /// <param name="fileSystemManager"></param>
        /// <param name="logger"></param>
        public PluginManager(
            IFileSystemManager fileSystemManager, 
            IApplicationLogger<PluginManager> logger)
        {
            this._pluginInstance = [];
            this._logger = logger;
            this._fileSystemManager = fileSystemManager;
            LoadPlugins();
        }

        /// <summary>
        /// Loads the plugins from the specified directory and initializes the plugin instance.
        /// </summary>
        /// <exception cref="FileNotFoundException"></exception>
        private void LoadPlugins()
        {
            string[] pluginDirectories = Directory.GetDirectories(_fileSystemManager.PluginDirectory ?? string.Empty) ?? [];
            foreach (string pluginDirectory in pluginDirectories)
            {
                var pluginFiles = Directory.GetFiles(pluginDirectory, CommonConstants.Plugin_AssemblySearchPattern) ?? throw new FileNotFoundException("No plugin found.");
                foreach (var pluginFile in pluginFiles)
                {
                    Assembly asm = AssemblyLoadContext.Default.LoadFromAssemblyPath(Path.GetFullPath(pluginFile));
                    var type = asm.GetTypes().FirstOrDefault(t => typeof(IStoragePlugin).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

                    if (type != null)
                    {
                        _pluginInstance?.Add((IStoragePlugin)Activator.CreateInstance(type)!);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the data context for the plugin with the specified connection parameters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionParams"></param>
        /// <returns></returns>
        public IDataContext<T>? GetContext<T>(Dictionary<string, object> connectionParams) where T : class
        {
            var activePlugin = connectionParams["storageId"] != null ?
                _pluginInstance?.FirstOrDefault(p => p.PluginId == Guid.Parse(connectionParams["storageId"].ToString() ?? string.Empty) && p.IsActive == true) :
                _pluginInstance?.FirstOrDefault(p => p.IsActive == true && p.IsPrimary == true);
            if (activePlugin == null)
            {
                this._logger.LogError(message: "No active primary plugin found.", apiCall: null);
                return null;
            }
            return activePlugin?.GetDataContext<T>(connectionParams);
        }
    }
}
