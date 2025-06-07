#region using directives
using DevNest.Plugin.Contracts.Encryption;
using DevNest.Plugin.Contracts.Storage;
#endregion using directives

namespace DevNest.Plugin.AES.CBC
{
    /// <summary>
    /// Represents the class instance for AES-CBC encryption plugin.
    /// </summary>
    public class AESCBCEncryptionPlugin : IEncryptionPlugin
    {
        private readonly Dictionary<Type, object> _contexts = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="AESCBCEncryptionPlugin"/> class.
        /// </summary>
        public AESCBCEncryptionPlugin()
        {
            _contexts = [];
        }

        /// <summary>
        /// Gets or sets the unique identifier for the plugin.
        /// </summary>
        public Guid PluginId { get; set; } = Guid.Parse("72CC5A1C-24DB-4E1A-B6ED-00B47450960F");

        /// <summary>
        /// Gets or sets the name of the plugin.
        /// </summary>
        public string? Name { get; set; } = "AES-CBC";

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
        public string? Description { get; set; } = "Plugin for AES-CBC encryption and decryption operations.";

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

            var newContext = new AESCBCEncryptionContext<T>(connectionParams);
            _contexts[typeof(T)] = newContext;
            return newContext;
        }
    }
}
