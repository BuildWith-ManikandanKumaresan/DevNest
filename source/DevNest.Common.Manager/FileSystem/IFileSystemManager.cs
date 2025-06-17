namespace DevNest.Common.Manager.FileSystem
{
    /// <summary>
    /// Represents the interface for managing file systems.
    /// </summary>
    public interface IFileSystemManager
    {
        /// <summary>
        /// Gets or sets the root directory of the file system.
        /// </summary>
        IFileSystem? Logger { get; set; }

        /// <summary>
        /// Gets or sets the directory that contains the logger files.
        /// </summary>
        IFileSystem? Plugin { get; set; }

        /// <summary>
        /// Gets or sets the directory that contains the preferences files.
        /// </summary>
        IFileSystem? Preferences { get; set; }

        /// <summary>
        /// Gets or sets the directory that contains the resources files.
        /// </summary>
        IFileSystem? Resources { get; set; }

        /// <summary>
        /// Gets or sets the directory that contains the secure vault files for storing sensitive data.
        /// </summary>
        IFileSystem? SecureVault { get; set; }
    }
}