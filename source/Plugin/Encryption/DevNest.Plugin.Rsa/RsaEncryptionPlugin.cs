#region using directives
using DevNest.Common.Logger;
using DevNest.Plugin.Contracts.Encryption;
#endregion using directives

namespace DevNest.Plugin.Rsa
{
    /// <summary>
    /// Represents the class instance for RSA encryption plugin.
    /// </summary>
    public class RsaEncryptionPlugin : IEncryptionPlugin
    {
        private readonly Dictionary<Type, object> _contexts = [];
        private readonly IAppLogger<RsaEncryptionPlugin> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RsaEncryptionPlugin"/> class.
        /// </summary>
        public RsaEncryptionPlugin(IAppLogger<RsaEncryptionPlugin> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Logger cannot be null.");
            _contexts = [];
        }

        /// <summary>
        /// Gets or sets the unique identifier for the plugin.
        /// </summary>
        public Guid PluginId { get; set; } = Guid.Parse("AC05832F-8C88-438A-A1BE-1FCB22F585D2");

        /// <summary>
        /// Gets or sets the name of the plugin.
        /// </summary>
        public string? Name { get; set; } = "RSA";

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
        public string? Description { get; set; } = "RSA Encryption Plugin for DevNest";

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

            var newContext = new RsaEncryptionContext<T>(connectionParams, _logger);
            _contexts[typeof(T)] = newContext;
            return newContext;
        }
    }
}