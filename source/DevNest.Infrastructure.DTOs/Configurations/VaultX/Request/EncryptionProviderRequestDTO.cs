namespace DevNest.Infrastructure.DTOs.Configurations.VaultX.Request
{
    /// <summary>
    /// Represents general settings for the Credential Manager application.
    /// </summary>
    public class EncryptionProviderRequestDTO
    {
        /// <summary>
        /// Represents the unique identifier for the encryption provider.
        /// </summary>
        public Guid? EncryptionId { get; set; }

        /// <summary>
        /// Indicates whether the encryption provider is enabled.
        /// </summary>
        public bool? IsPrimary { get; set; }

        /// <summary>
        /// Represents the encryption algorithm used by the provider.
        /// </summary>
        public string? EncryptionKey { get; set; }
    }

}