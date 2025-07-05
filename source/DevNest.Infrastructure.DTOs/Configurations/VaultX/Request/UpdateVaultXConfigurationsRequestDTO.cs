#region using directives
#endregion using directives

namespace DevNest.Infrastructure.DTOs.Configurations.VaultX.Request
{
    /// <summary>
    /// Represents the configurations for the Credential Manager application.
    /// </summary>
    public class UpdateVaultXConfigurationsRequestDTO
    {
        /// <summary>
        /// Represents the general settings for the Credential Manager application.
        /// </summary>
        public GeneralSettingsRequestDTO? GeneralSettings { get; set; }

        /// <summary>
        /// Represents the security settings for the Credential Manager application.
        /// </summary>
        public SecuritySettingsRequestDTO? SecuritySettings { get; set; }

        /// <summary>
        /// Represents the store and encryption providers for the Credential Manager application.
        /// </summary>
        public IList<StoreProviderRequestDTO>? StoreProviders { get; set; }

        /// <summary>
        /// Represents the encryption providers for the Credential Manager application.
        /// </summary>
        public IList<EncryptionProviderRequestDTO>? EncryptionProviders { get; set; }

        /// <summary>
        /// Represents the backup settings for the Credential Manager application.
        /// </summary>
        public BackupSettingsRequestDTO? BackupSettings { get; set; }
    }
}