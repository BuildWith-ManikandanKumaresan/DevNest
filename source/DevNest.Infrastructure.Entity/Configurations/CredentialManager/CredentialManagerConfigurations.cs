namespace DevNest.Infrastructure.Entity.Configurations.CredentialManager
{
    /// <summary>
    /// Represents the configurations for the Credential Manager application.
    /// </summary>
    public class CredentialManagerConfigurations
    {
        /// <summary>
        /// Represents the general settings for the Credential Manager application.
        /// </summary>
        public GeneralSettings? GeneralSettings { get; set; }

        /// <summary>
        /// Represents the security settings for the Credential Manager application.
        /// </summary>
        public SecuritySettings? SecuritySettings { get; set; }

        /// <summary>
        /// Represents the storage and encryption providers for the Credential Manager application.
        /// </summary>
        public IEnumerable<StorageProvider>? StorageProviders { get; set; }

        /// <summary>
        /// Represents the encryption providers for the Credential Manager application.
        /// </summary>
        public IEnumerable<EncryptionProvider>? EncryptionProviders { get; set; }

        /// <summary>
        /// Represents the backup settings for the Credential Manager application.
        /// </summary>
        public BackupSettings? BackupSettings { get; set; }
    }
}