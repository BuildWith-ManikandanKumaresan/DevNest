#region using directives
using DevNest.Plugin.Contracts;
using DevNest.Plugin.Contracts.Encryption;
using DevNest.Plugin.Contracts.Storage;
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
        IStorageContext<T>? GetStorageContext<T>(Dictionary<string, object> connectionParams) where T : class;

        /// <summary>
        /// Gets the encryption data context for the plugin with the specified connection parameters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionParams"></param>
        /// <returns></returns>
        IEncryptionContext<T>? GetEncryptionContext<T>(Dictionary<string, object> connectionParams) where T : class;

        /// <summary>
        /// Loads all storage plugins available in the system.
        /// </summary>
        void LoadStoragePlugins();

        /// <summary>
        /// Loads all encryption plugins available in the system.
        /// </summary>
        void LoadEncryptionPlugins();
    }
}