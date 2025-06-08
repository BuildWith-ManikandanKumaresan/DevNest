namespace DevNest.Infrastructure.Entity.Configurations.CredentialManager
{
    /// <summary>
    /// Represents general settings for the Credential Manager application.
    /// </summary>
    public class StorageProvider
    {
        /// <summary>
        /// Unique identifier for the storage provider.
        /// </summary>
        public Guid? StorageId { get; set; }

        /// <summary>
        /// Is primary storage provider.
        /// </summary>
        public bool? IsPrimary { get; set; }

        /// <summary>
        /// Indicates whether this storage provider is secondary or not.
        /// </summary>
        public bool? IsSecondary { get; set; }

        /// <summary>
        /// Maximum file size of the file in bytes.
        /// </summary>
        public long? MaxFileSizeBytes { get; set; }

        /// <summary>
        /// Base file name for the storage provider.
        /// </summary>
        public string? BaseFileName { get; set; }

        /// <summary>
        /// Data directory for the storage provider.
        /// </summary>
        public string? DataDirectory { get; set; }
    }

}