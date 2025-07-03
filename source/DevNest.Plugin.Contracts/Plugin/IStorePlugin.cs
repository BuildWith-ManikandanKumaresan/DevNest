#region using directives
using DevNest.Infrastructure.Entity.TaggingX;
using DevNest.Infrastructure.Entity.VaultX;
using DevNest.Plugin.Contracts.Base;
using DevNest.Plugin.Contracts.Context;
#endregion using directives

namespace DevNest.Plugin.Contracts.Plugin
{
    /// <summary>
    /// Represents the interface instances of store plugins must implement.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IStorePlugin : IPluginBase
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
    }
}