#region using directives
using DevNest.Infrastructure.Entity.Credentials;
using DevNest.Infrastructure.Entity.Tags;
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
        IStoreContext<T>? GetCredentialContext<T>(Dictionary<string, object> connectionParams) where T : CredentialEntityModel;

        /// <summary>
        /// Gets the credential store category context for the plugin with the specified connection parameters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionParams"></param>
        /// <returns></returns>
        IStoreContext<T>? GetCredentialCategoryContext<T>(Dictionary<string, object> connectionParams) where T : CategoryEntityModel;

        /// <summary>
        /// Gets the tag store context for the plugin with the specified connection parameters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionParams"></param>
        /// <returns></returns>
        IStoreContext<T>? GetTagContext<T>(Dictionary<string, object> connectionParams) where T : TagEntityModel;

        /// <summary>
        /// Gets the encryption data context for the plugin with the specified connection parameters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionParams"></param>
        /// <returns></returns>
        ICryptoContext<T>? GetCryptoContext<T>(Dictionary<string, object> connectionParams) where T : class;
    }
}