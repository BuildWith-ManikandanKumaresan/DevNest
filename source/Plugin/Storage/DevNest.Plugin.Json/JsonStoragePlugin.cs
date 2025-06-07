#region using directives
using DevNest.Infrastructure.Entity;
using DevNest.Plugin.Contracts.Storage;
#endregion using directives

namespace DevNest.Plugin.Json
{
    /// <summary>
    /// Represents the class instance for JSON storage plugin.
    /// </summary>
    public class JsonStoragePlugin : IStoragePlugin
    {
        private readonly Dictionary<Type, object> _contexts = [];

        public JsonStoragePlugin()
        {
            _contexts = [];
        }

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
        /// Gets the data context for the plugin with the specified connection parameters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionParams"></param>
        /// <returns></returns>
        public IStorageContext<T>? GetStorageContext<T>(Dictionary<string, object>? connectionParams) where T : class
        {
            if (_contexts.TryGetValue(typeof(T), out var context))
                return (IStorageContext<T>)context;

            var newContext = new JsonStorageContext<T>(connectionParams);
            _contexts[typeof(T)] = newContext;
            return newContext;
        }
    }
}
