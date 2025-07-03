namespace DevNest.Plugin.Contracts.Base
{
    /// <summary>
    /// Represents the interface instances of plugins must implement.
    /// </summary>
    public interface IPluginBase
    {
        /// <summary>
        /// Gets or sets the plugin id.
        /// </summary>
        Guid PluginId { get; set; }

        /// <summary>
        /// Gets or sets the name of the plugin.
        /// </summary>
        string? Name { get; set; }

        /// <summary>
        /// Gets or sets the version of the plugin.
        /// </summary>
        string? Version { get; set; }

        /// <summary>
        /// Gets or sets the Is primary plugin flag for the plugin.
        /// </summary>
        bool? IsPrimary { get; set; }

        /// <summary>
        /// Gets or sets the active status for the plugins.
        /// </summary>
        bool? IsActive { get; set; }

        /// <summary>
        /// Gets or sets the description of the plugin.
        /// </summary>
        string? Description { get; set; }

        /// <summary>
        /// Gets or sets the connection params input for the plugin.
        /// </summary>
        Dictionary<string,object>? ConnectionParams { get; set; }
    }
}