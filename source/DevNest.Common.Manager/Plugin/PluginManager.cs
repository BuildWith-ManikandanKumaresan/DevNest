#region using directives
using DevNest.Common.Base.Constants;
using DevNest.Common.Base.Constants.Message;
using DevNest.Common.Base.Contracts;
using DevNest.Common.Logger;
using DevNest.Common.Manager.FileSystem;
using DevNest.Infrastructure.Entity.VaultX;
using DevNest.Plugin.Contracts;
using DevNest.Plugin.Contracts.Context;
using DevNest.Plugin.Contracts.Plugin;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Runtime.Loader;
#endregion using directives7

namespace DevNest.Common.Manager.Plugin
{
    /// <summary>
    /// Represents the class instance for managing plugins in the application.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="PluginManager"/> class.
    /// </remarks>
    /// <param name="fileSystemManager"></param>
    /// <param name="logger"></param>
    public class PluginManager : IPluginManager
    {
        private readonly IFileSystemManager _fileSystemManager;
        private readonly IAppLogger<PluginManager> _logger;
        private IList<IStorePlugin>? _pluginStoreInstance;
        private IList<IEncryptionPlugin>? _pluginEncryptionInstance;
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginManager"/> class with the specified file system manager, logger, and service provider.
        /// </summary>
        /// <param name="fileSystemManager"></param>
        /// <param name="logger"></param>
        /// <param name="serviceProvider"></param>
        public PluginManager(IFileSystemManager fileSystemManager, IAppLogger<PluginManager> logger, IServiceProvider serviceProvider)
        {
            this._fileSystemManager = fileSystemManager;
            this._logger = logger;
            this._serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Gets the encryption data context for the plugin with the specified connection parameters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionParams"></param>
        /// <returns></returns>
        public IEncryptionContext<T>? GetEnxryptionContext<T>(Dictionary<string, object> connectionParams) where T : class
        {
            var activePlugin = GetEncryptionPlugin(connectionParams);
            return activePlugin?.GetEncryptionContext<T>(connectionParams);
        }

        /// <summary>
        /// Gets the data context for the plugin with the specified connection parameters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionParams"></param>
        /// <returns></returns>
        public IStoreContext<T>? GetStoreContext<T>(Dictionary<string, object> connectionParams) where T : class
        {
            var activePlugin = GetStorePlugin(connectionParams);
            return activePlugin?.GetStoreContext<T>(connectionParams);
        }

        /// <summary>
        /// Gets the credential store category context for the plugin with the specified connection parameters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionParams"></param>
        /// <returns></returns>
        public IResourceContext<T>? GetResourceContext<T>(Dictionary<string, object> connectionParams) where T : class
        {
            var activePlugin = GetStorePlugin(connectionParams);
            return activePlugin?.GetResourceContext<T>(connectionParams);
        }

        /// <summary>
        /// Gets the credential store configuration context for the plugin with the specified connection parameters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionParams"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IConfigurationContext<T> GetConfigurationContext<T>(Dictionary<string, object> connectionParams) where T : class
        {
            var activePlugin = GetStorePlugin(connectionParams);
            return activePlugin?.GetConfigurationContext<T>(connectionParams);
        }

        /// <summary>
        /// Loads the plugins from the specified directory and initializes the plugin instance.
        /// </summary>
        /// <exception cref="FileNotFoundException"></exception>
        public void LoadStorePlugins()
        {
            try
            {
                IFileSystem? storePluginDirectory = _fileSystemManager.Plugin?.GetSubDirectory(FileSystemConstants.StorePluginDirectory);

                foreach (var pluginDirectory in storePluginDirectory?.Directories ?? [])
                {
                    foreach (var pluginFile in pluginDirectory.GetFilesWithSearchPattern() ?? [])
                    {
                        Assembly asm = AssemblyLoadContext.Default.LoadFromAssemblyPath(Path.GetFullPath(pluginFile));
                        var type = asm.GetTypes().FirstOrDefault(t => typeof(IStorePlugin).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);
                        if (type != null)
                        {
                            var instance = (IStorePlugin)ActivatorUtilities.CreateInstance(_serviceProvider, type);
                            _pluginStoreInstance ??= [];
                            _pluginStoreInstance?.Add(instance);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(PluginManager)} => {ErrorConstants.NoStorePluginFound}{ex.Message}", ex);
            }
        }

        /// <summary>
        /// Loads the encryption plugins from the specified directory and initializes the plugin instance.
        /// </summary>
        /// <exception cref="FileNotFoundException"></exception>
        public void LoadEncryptionPlugins()
        {
            try
            {
                IFileSystem? encryptionPluginDirectory = _fileSystemManager.Plugin?.GetSubDirectory(FileSystemConstants.EncryptionPluginsDirectory);

                foreach (var pluginDirectory in encryptionPluginDirectory?.Directories ?? [])
                {
                    foreach (var pluginFile in pluginDirectory.GetFilesWithSearchPattern() ?? [])
                    {
                        Assembly asm = AssemblyLoadContext.Default.LoadFromAssemblyPath(Path.GetFullPath(pluginFile));
                        var type = asm.GetTypes().FirstOrDefault(t => typeof(IEncryptionPlugin).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

                        if (type != null)
                        {
                            var instance = (IEncryptionPlugin)ActivatorUtilities.CreateInstance(_serviceProvider, type);
                            _pluginEncryptionInstance ??= [];
                            _pluginEncryptionInstance?.Add(instance);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(PluginManager)} => {ErrorConstants.NoEncryptionPluginFound}{ex.Message}", ex);
            }
        }

        #region Private methods

        /// <summary>
        /// Handler method to get the active store plugin based on the connection parameters provided.
        /// </summary>
        /// <param name="connectionParams"></param>
        /// <returns></returns>
        private IStorePlugin? GetStorePlugin(Dictionary<string, object> connectionParams)
        {
            IStorePlugin? activePlugin = connectionParams[ConnectionParamConstants.PluginStoreId] != null ?
                _pluginStoreInstance?.FirstOrDefault(p => p.PluginId == Guid.Parse(connectionParams[ConnectionParamConstants.PluginStoreId].ToString() ?? string.Empty) && p.IsActive == true) :
                _pluginStoreInstance?.FirstOrDefault(p => p.IsActive == true && p.IsPrimary == true);
            if (activePlugin == null)
            {
                _logger.LogError($"{nameof(PluginManager)} => No active primary store plugin found.", request: new { connectionParams });
                return null;
            }
            return activePlugin;
        }

        /// <summary>
        /// Handler method to get the active encryption plugin based on the connection parameters provided.
        /// </summary>
        /// <param name="connectionParams"></param>
        /// <returns></returns>
        private IEncryptionPlugin? GetEncryptionPlugin(Dictionary<string, object> connectionParams)
        {
            IEncryptionPlugin? activePlugin = connectionParams[ConnectionParamConstants.PluginEncryptionId] != null ?
                _pluginEncryptionInstance?.FirstOrDefault(p => p.PluginId == Guid.Parse(connectionParams[ConnectionParamConstants.PluginEncryptionId].ToString() ?? string.Empty) && p.IsActive == true) :
                _pluginEncryptionInstance?.FirstOrDefault(p => p.IsActive == true && p.IsPrimary == true);
            if (activePlugin == null)
            {
                _logger.LogError($"{nameof(PluginManager)} => No active primary encryption plugin found.", request: new { connectionParams });
                return null;
            }
            return activePlugin;
        }

        #endregion Private methods
    }
}
