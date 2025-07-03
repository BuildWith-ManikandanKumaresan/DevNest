#region using directives
using DevNest.Common.Logger;
using DevNest.Infrastructure.Entity;
using DevNest.Infrastructure.Entity.Configurations.VaultX;
using DevNest.Infrastructure.Entity.TaggingX;
using DevNest.Infrastructure.Entity.VaultX;
using DevNest.Plugin.Contracts.Context;
using DevNest.Plugin.Contracts.Plugin;
using DevNest.Store.Plugin.Json.Context.TaggingX.Store;
using DevNest.Store.Plugin.Json.Context.VaultX.Configuration;
using DevNest.Store.Plugin.Json.Context.VaultX.Resource;
using DevNest.Store.Plugin.Json.Context.VaultX.Store;
#endregion using directives

namespace DevNest.Store.Plugin.Json
{
    /// <summary>
    /// Represents the class instance for JSON storage plugin.
    /// </summary>
    public class JsonStorePlugin(IAppLogger<JsonStorePlugin> logger) : IStorePlugin
    {
        private readonly Dictionary<Type, object> _contexts = [];
        private readonly IAppLogger<JsonStorePlugin> _logger = logger;

        /// <summary>
        /// Gets or sets the unique identifier for the plugin.
        /// </summary>
        public Guid PluginId { get; set; } = Guid.Parse("1E9D26C8-0105-4113-9154-230A88D3F8B3");

        /// <summary>
        /// Gets or sets the name of the plugin.
        /// </summary>
        public string? Name { get; set; } = "Json";

        /// <summary>
        /// Gets or sets the version of the plugin.
        /// </summary>
        public string? Version { get; set; } = "1.0.0.0";

        /// <summary>
        /// Gets or sets a value indicating whether this plugin is the primary plugin.
        /// </summary>
        public bool? IsPrimary { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether this plugin is active.
        /// </summary>
        public bool? IsActive { get; set; } = true;

        /// <summary>
        /// Gets or sets the description of the plugin.
        /// </summary>
        public string? Description { get; set; } = "Plugin for store the data as Json.";

        /// <summary>
        /// Gets or sets the connection parameters for the plugin.
        /// </summary>
        public Dictionary<string, object>? ConnectionParams { get; set; } = [];

        /// <summary>
        /// Handler method to get the configuration context instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionParams"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public IConfigurationContext<T> GetConfigurationContext<T>(Dictionary<string, object> connectionParams) where T : class
        {
            if (_contexts.TryGetValue(typeof(T), out var context))
                return (IConfigurationContext<T>)context;
            if (typeof(T) == typeof(VaultXConfigurationsEntityModel))
            {
                var newContent = new VaultXConfigurationContext<VaultXConfigurationsEntityModel>(connectionParams, _logger);
                _contexts[typeof(IConfigurationContext<T>)] = (IConfigurationContext<T>)newContent;
                return (IConfigurationContext<T>)newContent;
            }

            // Optional: throw or return null if unsupported
            throw new NotSupportedException($"Unsupported entity type: {typeof(T).Name}");
        }

        /// <summary>
        /// Handler method to get the resource context instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionParams"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public IResourceContext<T> GetResourceContext<T>(Dictionary<string, object> connectionParams) where T : class
        {
            if (_contexts.TryGetValue(typeof(T), out var context))
                return (IResourceContext<T>)context; 
            if (typeof(T) == typeof(CategoryEntityModel))
            {
                var newContent = new VaultXCategoryResourceContext<CategoryEntityModel>(connectionParams, _logger);
                _contexts[typeof(IResourceContext<T>)] = (IResourceContext<T>)newContent;
                return (IResourceContext<T>)newContent;
            }

            // Optional: throw or return null if unsupported
            throw new NotSupportedException($"Unsupported entity type: {typeof(T).Name}");
        }

        /// <summary>
        /// Handler method to get the storage context instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionParams"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public IStoreContext<T> GetStoreContext<T>(Dictionary<string, object> connectionParams) where T : class
        {

            if (_contexts.TryGetValue(typeof(T), out var context))
                return (IStoreContext<T>)context;

            if (typeof(T) == typeof(CredentialEntityModel))
            {
                var credStoreContext = new VaultXStoreContext<CredentialEntityModel>(connectionParams, _logger);
                _contexts[typeof(IStoreContext<T>)] = (IStoreContext<T>)credStoreContext;
                return (IStoreContext<T>)credStoreContext;
            }
            else if (typeof(T) == typeof(TagEntityModel))
            {
                var tagStoreContext = new TaggingXStoreContext<TagEntityModel>(connectionParams, _logger); 
                _contexts[typeof(IStoreContext<T>)] = (IStoreContext<T>)tagStoreContext;
                return (IStoreContext<T>)tagStoreContext;
            }

            // Optional: throw or return null if unsupported
            throw new NotSupportedException($"Unsupported entity type: {typeof(T).Name}");
        }
    }
}
