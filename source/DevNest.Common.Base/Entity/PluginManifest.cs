namespace DevNest.Common.Base.Entity
{
    /// <summary>
    /// Represents the class instance for plugin manifest json entity.
    /// </summary>
    public class PluginManifest
    {
        /// <summary>
        /// Gets or sets the plugin id.
        /// </summary>
        public Guid PluginId { get; set; }

        /// <summary>
        /// Gets or sets the plugin name.
        /// </summary>
        public string? PluginName { get; set; }

        /// <summary>
        /// Gets or sets the plugin type.
        /// </summary>
        public string? PluginType { get; set; }

        /// <summary>
        /// Gets or sets the plugin version.
        /// </summary>
        public string? Version { get; set; }

        /// <summary>
        /// Gets or sets the plugin description.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the assmebly location.
        /// </summary>
        public string? AssemblyLocation { get; set; }

        /// <summary>
        /// Gets or sets the storage id.
        /// </summary>
        public Guid? StorageId { get; set; }

        /// <summary>
        /// Gets or sets the is active flag.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the is primary flag.
        /// </summary>
        public bool IsPrimary { get; set; }
    }
}