namespace DevNest.Manager.FileSystem
{
    public interface IFileSystemManager
    {
        /// <summary>
        /// Gets or sets the filesystem root directory.
        /// </summary>
        string? RootDirectory { get; }

        /// <summary>
        /// Get configuration directory from the file systems.
        /// </summary>
        string? ConfigurationDirectory { get; }

        /// <summary>
        /// Get the directry path than contains the plugins.
        /// </summary>
        string? PluginDirectory { get; }

        /// <summary>
        /// Get the directory path that contains the data's.
        /// </summary>
        string? DataDirectrory { get; }
    }
}
