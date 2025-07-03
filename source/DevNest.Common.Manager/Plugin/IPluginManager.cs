#region using directives
using DevNest.Infrastructure.Entity.TaggingX;
using DevNest.Infrastructure.Entity.VaultX;
using DevNest.Plugin.Contracts;
using DevNest.Plugin.Contracts.Context;
#endregion using directives

namespace DevNest.Common.Manager.Plugin
{
    /// <summary>
    /// Represents the interface for managing plugins in the application.
    /// </summary>
    public interface IPluginManager
    {
        /// <summary>
        /// Gets the data context for the plugin with the specified connection parameters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionParams"></param>
        /// <returns></returns>
        IStoreContext<T> GetStoreContext<T>(Dictionary<string, object> connectionParams) where T : class;

        /// <summary>
        /// Gets the configuration context for the plugin with the specified connection parameters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionParams"></param>
        /// <returns></returns>
        IConfigurationContext<T> GetConfigurationContext<T>(Dictionary<string, object> connectionParams) where T : class;

        /// <summary>
        /// Gets the resource context for the plugin with the specified connection parameters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionParams"></param>
        /// <returns></returns>
        IResourceContext<T> GetResourceContext<T>(Dictionary<string, object> connectionParams) where T : class;

        /// <summary>
        /// Gets the encryption data context for the plugin with the specified connection parameters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionParams"></param>
        /// <returns></returns>
        IEncryptionContext<T>? GetEnxryptionContext<T>(Dictionary<string, object> connectionParams) where T : class;

        /// <summary>
        /// Loads the plugins from the specified directory and initializes the plugin instance.
        /// </summary>
        void LoadStorePlugins();

        /// <summary>
        /// Loads the encryption plugins from the specified directory and initializes the plugin instance.
        /// </summary>
        void LoadEncryptionPlugins();
    }
}