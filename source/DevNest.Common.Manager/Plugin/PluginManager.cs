#region using directives
using DevNest.Common.Base.Constants;
using DevNest.Common.Base.Constants.Message;
using DevNest.Common.Base.Contracts;
using DevNest.Common.Logger;
using DevNest.Common.Manager.FileSystem;
using DevNest.Infrastructure.Entity.Credentials;
using DevNest.Infrastructure.Entity.Tags;
using DevNest.Plugin.Contracts;
using DevNest.Plugin.Contracts.Encryption;
using DevNest.Plugin.Contracts.Storage;
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
        private readonly IList<IStoragePlugin>? _pluginStorageInstance;
        private readonly IList<IEncryptionPlugin>? _pluginEncryptionInstance;
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
            _pluginStorageInstance = new List<IStoragePlugin>();
            _pluginEncryptionInstance = new List<IEncryptionPlugin>();
            LoadStoragePlugins();
            LoadEncryptionPlugins();
        }

        /// <summary>
        /// Gets the encryption data context for the plugin with the specified connection parameters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionParams"></param>
        /// <returns></returns>
        public IEncryptionContext<T>? GetEncryptionContext<T>(Dictionary<string, object> connectionParams) where T : class
        {
            var activePlugin = connectionParams[ConnectionParamConstants.PluginEncryptionId] != null ?
                _pluginEncryptionInstance?.FirstOrDefault(p => p.PluginId == Guid.Parse(connectionParams[ConnectionParamConstants.PluginEncryptionId].ToString() ?? string.Empty) && p.IsActive == true) :
                _pluginEncryptionInstance?.FirstOrDefault(p => p.IsActive == true && p.IsPrimary == true);
            if (activePlugin == null)
            {
                _logger.LogError($"{nameof(PluginManager)} => No active primary encryption plugin found.", request: new { connectionParams });
                return null;
            }
            return activePlugin?.GetEncryptionContext<T>(connectionParams);
        }

        /// <summary>
        /// Gets the data context for the plugin with the specified connection parameters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionParams"></param>
        /// <returns></returns>
        public IStorageContext<T>? GetCredStoreContext<T>(Dictionary<string, object> connectionParams) where T : CredentialEntityModel
        {
            var activePlugin = connectionParams[ConnectionParamConstants.PluginStorageId] != null ?
                _pluginStorageInstance?.FirstOrDefault(p => p.PluginId == Guid.Parse(connectionParams[ConnectionParamConstants.PluginStorageId].ToString() ?? string.Empty) && p.IsActive == true) :
                _pluginStorageInstance?.FirstOrDefault(p => p.IsActive == true && p.IsPrimary == true);
            if (activePlugin == null)
            {
                _logger.LogError($"{nameof(PluginManager)} => No active primary storage plugin found.", request: new { connectionParams });
                return null;
            }
            return activePlugin?.GetCredStoreContext<T>(connectionParams);
        }

        /// <summary>
        /// Gets the credential store category context for the plugin with the specified connection parameters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionParams"></param>
        /// <returns></returns>
        public IStorageContext<T>? GetCredStoreCategoryContext<T>(Dictionary<string, object> connectionParams) where T : CategoryEntityModel
        {
            var activePlugin = connectionParams[ConnectionParamConstants.PluginStorageId] != null ?
                _pluginStorageInstance?.FirstOrDefault(p => p.PluginId == Guid.Parse(connectionParams[ConnectionParamConstants.PluginStorageId].ToString() ?? string.Empty) && p.IsActive == true) :
                _pluginStorageInstance?.FirstOrDefault(p => p.IsActive == true && p.IsPrimary == true);
            if (activePlugin == null)
            {
                _logger.LogError($"{nameof(PluginManager)} => No active primary storage plugin found.", request: new { connectionParams });
                return null;
            }
            return activePlugin?.GetCredStoreCategoryContext<T>(connectionParams);
        }

        /// <summary>
        /// Gets the tag store context for the plugin with the specified connection parameters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionParams"></param>
        /// <returns></returns>
        public IStorageContext<T>? GetTagStoreContext<T>(Dictionary<string, object> connectionParams) where T : TagEntityModel
        {
            var activePlugin = connectionParams[ConnectionParamConstants.PluginStorageId] != null ?
                _pluginStorageInstance?.FirstOrDefault(p => p.PluginId == Guid.Parse(connectionParams[ConnectionParamConstants.PluginStorageId].ToString() ?? string.Empty) && p.IsActive == true) :
                _pluginStorageInstance?.FirstOrDefault(p => p.IsActive == true && p.IsPrimary == true);
            if (activePlugin == null)
            {
                _logger.LogError($"{nameof(PluginManager)} => No active primary storage plugin found.", request: new { connectionParams });
                return null;
            }
            return activePlugin?.GetTagStoreContext<T>(connectionParams);
        }

        #region Private methods

        /// <summary>
        /// Loads the plugins from the specified directory and initializes the plugin instance.
        /// </summary>
        /// <exception cref="FileNotFoundException"></exception>
        private void LoadStoragePlugins()
        {
            try
            {
                IFileSystem? storagePluginDirectory = _fileSystemManager.Plugin?.GetSubDirectory(FileSystemConstants.StoragePluginsDirectory);

                foreach (var pluginDirectory in storagePluginDirectory?.Directories ?? [])
                {
                    foreach (var pluginFile in pluginDirectory.GetFilesWithSearchPattern() ?? [])
                    {
                        Assembly asm = AssemblyLoadContext.Default.LoadFromAssemblyPath(Path.GetFullPath(pluginFile));
                        var type = asm.GetTypes().FirstOrDefault(t => typeof(IStoragePlugin).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);
                        if (type != null)
                        {
                            var instance = (IStoragePlugin)ActivatorUtilities.CreateInstance(_serviceProvider, type);
                            _pluginStorageInstance?.Add(instance);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(PluginManager)} => {ErrorConstants.NoStoragePluginFound}{ex.Message}", ex);
            }
        }

        /// <summary>
        /// Loads the encryption plugins from the specified directory and initializes the plugin instance.
        /// </summary>
        /// <exception cref="FileNotFoundException"></exception>
        private void LoadEncryptionPlugins()
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

        #endregion Private methods
    }
}
