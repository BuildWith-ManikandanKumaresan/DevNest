namespace DevNest.Infrastructure.Entity.Configurations.CredentialManager
{
    /// <summary>
    /// Represents the configuration for credential manager storage.
    /// </summary>
    public class CredentialManagerConfigurations
    {
        /// <summary>
        /// Gets or sets the flag indicating whether to show archived credentials.
        /// </summary>
        public bool? ShowArchivedCredentials { get; set; }

        /// <summary>
        /// Gets or sets the flag indicating whether to show password as masked (e.g., asterisks).
        /// </summary>
        public bool? ShowPasswordAsMasked { get; set; }

        /// <summary>
        /// Gets or sets the placeholder for masking the password (e.g., asterisks).
        /// </summary>
        public string? MaskingPlaceHolder { get; set; }

        /// <summary>
        /// Gets or sets the storage configurations for credential manager.
        /// </summary>
        public IEnumerable<Dictionary<string, object>>? StorageProvider { get; set; }

        /// <summary>
        /// Gets or sets the encryption provider configurations for credential manager.
        /// </summary>
        public IEnumerable<Dictionary<string,object>>? EncryptionProvider { get; set; }
    }
}