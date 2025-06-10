namespace DevNest.Common.Manager.FileSystem
{
    /// <summary>
    /// Represents the class interface for File system manager.
    /// </summary>
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
        string? PluginStorageDirectory { get; }

        /// <summary>
        /// Get the directory path that contains the encryption plugin storage.
        /// </summary>
        string? EncryptionPluginStorageDirectory { get; }

        /// <summary>
        /// Get the directory path that contains the data's.
        /// </summary>
        string? DataDirectrory { get; }

        /// <summary>
        /// Get the directory path that contains the assets.
        /// </summary>
        string? AssetsDirectory { get; }

        /// <summary>
        /// Get the directory path that contains the error codes.
        /// </summary>
        string? ErrorCodesDirectory { get; }

        /// <summary>
        /// Get the directory path that contains the warning codes.
        /// </summary>
        string? WarningCodesDirectory { get; }

        /// <summary>
        /// Get the directory path that contains the success codes.
        /// </summary>
        string? SuccessCodesDirectory { get; }
    }
}
