namespace DevNest.Infrastructure.DTOs.Configurations.VaultX.Response
{
    /// <summary>
    /// Represents Backup settings for the Credential Manager application.
    /// </summary>
    public class BackupSettingsResponseDTO
    {
        /// <summary>
        /// Enable or disable automatic backups.
        /// </summary>
        public bool? EnableCloudSync { get; set; }
    }

}