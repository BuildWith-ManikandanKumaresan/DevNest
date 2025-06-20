#region using directives
using DevNest.Infrastructure.Entity.Credentials;
using DevNest.Infrastructure.Entity.Tags;
#endregion using directives

namespace DevNest.Plugin.Contracts.Storage
{
    /// <summary>
    /// Represents the interface instances of storage plugins must implement.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IStoragePlugin : IPlugin
    {
        /// <summary>
        /// Gets the data context for the plugin with the specified connection parameters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionParams"></param>
        /// <returns></returns>
        IStorageContext<T>? GetCredStoreContext<T>(Dictionary<string, object> connectionParams) where T : CredentialEntityModel;

        /// <summary>
        /// Gets the tag store context for the plugin with the specified connection parameters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionParams"></param>
        /// <returns></returns>
        IStorageContext<T>? GetTagStoreContext<T>(Dictionary<string, object> connectionParams) where T : TagEntityModel;

        /// <summary>
        /// Gets the credential category store context for the plugin with the specified connection parameters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionParams"></param>
        /// <returns></returns>
        IStorageContext<T>? GetCredStoreCategoryContext<T>(Dictionary<string, object> connectionParams) where T : CategoryEntityModel;
    }
}