namespace DevNest.Infrastructure.Entity.Configurations.VaultX
{
    /// <summary>
    /// Represents the configurations for the Credential Manager application.
    /// </summary>
    public class VaultXConfigurationsEntityModel
    {
        /// <summary>
        /// Represents the general settings for the Credential Manager application.
        /// </summary>
        public GeneralSettingsEntityModel? GeneralSettings { get; set; }

        /// <summary>
        /// Represents the security settings for the Credential Manager application.
        /// </summary>
        public SecuritySettingsEntityModel? SecuritySettings { get; set; }

        /// <summary>
        /// Represents the store and encryption providers for the Credential Manager application.
        /// </summary>
        public IList<StoreProviderEntityModel>? StoreProviders { get; set; }

        /// <summary>
        /// Represents the encryption providers for the Credential Manager application.
        /// </summary>
        public IList<EncryptionProviderEntityModel>? EncryptionProviders { get; set; }

        /// <summary>
        /// Represents the backup settings for the Credential Manager application.
        /// </summary>
        public BackupSettingsEntityModel? BackupSettings { get; set; }
    }
}