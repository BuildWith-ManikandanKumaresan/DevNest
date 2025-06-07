#region using directives
using DevNest.Common.Base.Constants;
using DevNest.Common.Base.Constants.Message;
using DevNest.Common.Base.Contracts;
using DevNest.Common.Logger;
using DevNest.Manager.FileSystem;
using DevNest.Plugin.Contracts;
using DevNest.Plugin.Contracts.Encryption;
using DevNest.Plugin.Contracts.Storage;
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
        private readonly IList<IStoragePlugin>? _pluginStorageInstance;
        private readonly IList<IEncryptionPlugin>? _pluginEncryptionInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginManager"/> class.
        /// </summary>
        /// <param name="fileSystemManager"></param>
        /// <param name="logger"></param>
        public PluginManager(
            IFileSystemManager fileSystemManager, 
            IApplicationLogger<PluginManager> logger)
        {
            this._pluginStorageInstance = [];
            this._pluginEncryptionInstance = [];
            this._logger = logger;
            this._fileSystemManager = fileSystemManager;
            LoadStoragePlugins();
            LoadEncryptionPlugins();
        }

        /// <summary>
        /// Loads the plugins from the specified directory and initializes the plugin instance.
        /// </summary>
        /// <exception cref="FileNotFoundException"></exception>
        private void LoadStoragePlugins()
        {
            string[] pluginDirectories = Directory.GetDirectories(_fileSystemManager.PluginStorageDirectory ?? string.Empty) ?? [];
            foreach (string pluginDirectory in pluginDirectories)
            {
                var pluginFiles = Directory.GetFiles(pluginDirectory, CommonConstants.Plugin_AssemblySearchPattern) ?? throw new FileNotFoundException(Messages.GetError(ErrorConstants.NoStoragePluginFound).Message);
                foreach (var pluginFile in pluginFiles)
                {
                    Assembly asm = AssemblyLoadContext.Default.LoadFromAssemblyPath(Path.GetFullPath(pluginFile));
                    var type = asm.GetTypes().FirstOrDefault(t => typeof(IStoragePlugin).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

                    if (type != null)
                    {
                        _pluginStorageInstance?.Add((IStoragePlugin)Activator.CreateInstance(type)!);
                    }
                }
            }
        }

        /// <summary>
        /// Loads the encryption plugins from the specified directory and initializes the plugin instance.
        /// </summary>
        /// <exception cref="FileNotFoundException"></exception>
        private void LoadEncryptionPlugins()
        {
            string[] pluginDirectories = Directory.GetDirectories(_fileSystemManager.EncryptionPluginStorageDirectory ?? string.Empty) ?? [];
            foreach (string pluginDirectory in pluginDirectories)
            {
                var pluginFiles = Directory.GetFiles(pluginDirectory, CommonConstants.Plugin_AssemblySearchPattern) ?? throw new FileNotFoundException(Messages.GetError(ErrorConstants.NoEncryptionPluginFound).Message);
                foreach (var pluginFile in pluginFiles)
                {
                    Assembly asm = AssemblyLoadContext.Default.LoadFromAssemblyPath(Path.GetFullPath(pluginFile));
                    var type = asm.GetTypes().FirstOrDefault(t => typeof(IEncryptionPlugin).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

                    if (type != null)
                    {
                        _pluginEncryptionInstance?.Add((IEncryptionPlugin)Activator.CreateInstance(type)!);
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
        public IStorageDataContext<T>? GetStorageContext<T>(Dictionary<string, object> connectionParams) where T : class
        {
            var activePlugin = connectionParams[ConnectionParamConstants.PluginStorageId] != null ?
                _pluginStorageInstance?.FirstOrDefault(p => p.PluginId == Guid.Parse(connectionParams[ConnectionParamConstants.PluginStorageId].ToString() ?? string.Empty) && p.IsActive == true) :
                _pluginStorageInstance?.FirstOrDefault(p => p.IsActive == true && p.IsPrimary == true);
            if (activePlugin == null)
            {
                this._logger.LogError(message: "No active primary plugin found.", apiCall: null);
                return null;
            }
            return activePlugin?.GetStorageDataContext<T>(connectionParams);
        }

        /// <summary>
        /// Gets the encryption data context for the plugin with the specified connection parameters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionParams"></param>
        /// <returns></returns>
        public IEncryptionDataContext<T>? GetEncryptionDataContext<T>(Dictionary<string, object> connectionParams) where T : class
        {
            var activePlugin = connectionParams[ConnectionParamConstants.PluginEncryptionId] != null ?
                _pluginEncryptionInstance?.FirstOrDefault(p => p.PluginId == Guid.Parse(connectionParams[ConnectionParamConstants.PluginEncryptionId].ToString() ?? string.Empty) && p.IsActive == true) :
                _pluginEncryptionInstance?.FirstOrDefault(p => p.IsActive == true && p.IsPrimary == true);
            if (activePlugin == null)
            {
                this._logger.LogError(message: "No active primary plugin found.", apiCall: null);
                return null;
            }
            return activePlugin?.GetEncryptionDataContext<T>(connectionParams);
        }
    }
}
