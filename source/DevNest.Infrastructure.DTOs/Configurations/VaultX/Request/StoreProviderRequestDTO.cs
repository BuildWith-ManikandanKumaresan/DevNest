namespace DevNest.Infrastructure.DTOs.Configurations.VaultX.Request
{
    /// <summary>
    /// Represents general settings for the Credential Manager application.
    /// </summary>
    public class StoreProviderRequestDTO
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