namespace DevNest.Infrastructure.Entity.Configurations.VaultX
{
    /// <summary>
    /// Represents general settings for the Credential Manager application.
    /// </summary>
    public class StoreProviderEntityModel
    {
        /// <summary>
        /// Unique identifier for the store provider.
        /// </summary>
        public Guid? StoreId { get; set; }

        /// <summary>
        /// Is primary store provider.
        /// </summary>
        public bool? IsPrimary { get; set; }

        /// <summary>
        /// Indicates whether this store provider is secondary or not.
        /// </summary>
        public bool? IsSecondary { get; set; }

        /// <summary>
        /// Maximum file size of the file in bytes.
        /// </summary>
        public long? MaxFileSizeBytes { get; set; }

        /// <summary>
        /// Base file name for the store provider.
        /// </summary>
        public string? BaseFileName { get; set; }

        /// <summary>
        /// Data directory for the store provider.
        /// </summary>
        public string? DataDirectory { get; set; }
    }

}