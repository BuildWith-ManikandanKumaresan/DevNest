#region using directives
using DevNest.Plugin.Contracts.Encryption;
#endregion using directives

namespace DevNest.Plugin.Aes.Gcm
{
    /// <summary>
    /// Represents the class instance for AES-GCM encryption plugin.
    /// </summary>
    public class AesgcmEncryptionPlugin : IEncryptionPlugin
    {
        private readonly Dictionary<Type, object> _contexts = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="AesgcmEncryptionPlugin"/> class.
        /// </summary>
        public AesgcmEncryptionPlugin()
        {
            _contexts = [];
        }

        /// <summary>
        /// Gets or sets the unique identifier for the plugin.
        /// </summary>
        public Guid PluginId { get; set; } = Guid.Parse("33BE6BF2-9CE0-4862-92A9-98497EF19A1A");

        /// <summary>
        /// Gets or sets the name of the plugin.
        /// </summary>
        public string? Name { get; set; } = "AES_GCM";

        /// <summary>
        /// Gets or sets the version of the plugin.
        /// </summary>
        public string? Version { get; set; } = "1.0.0.0";

        /// <summary>
        /// Gets or sets a value indicating whether this plugin is the primary plugin.
        /// </summary>
        public bool? IsPrimary { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether this plugin is active.
        /// </summary>
        public bool? IsActive { get; set; } = true;

        /// <summary>
        /// Gets or sets the description of the plugin.
        /// </summary>
        public string? Description { get; set; } = "AES GCM Encryption Plugin for DevNest";

        /// <summary>
        /// Gets or sets the connection parameters for the plugin.
        /// </summary>
        public Dictionary<string, object>? ConnectionParams { get; set; } = [];

        /// <summary>
        /// Gets the encryption data context for the plugin with the specified connection parameters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionParams"></param>
        /// <returns></returns>
        public IEncryptionContext<T>? GetEncryptionContext<T>(Dictionary<string, object> connectionParams) where T : class
        {
            if (_contexts.TryGetValue(typeof(T), out var context))
                return (IEncryptionContext<T>)context;

            var newContext = new AesgcmEncryptionContext<T>(connectionParams);
            _contexts[typeof(T)] = newContext;
            return newContext;
        }
    }
}