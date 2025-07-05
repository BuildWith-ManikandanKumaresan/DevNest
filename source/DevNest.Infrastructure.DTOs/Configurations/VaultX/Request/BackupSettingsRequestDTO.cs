namespace DevNest.Infrastructure.DTOs.Configurations.VaultX.Request
{
    /// <summary>
    /// Represents Backup settings for the Credential Manager application.
    /// </summary>
    public class BackupSettingsRequestDTO
    {
        /// <summary>
        /// Enable or disable automatic backups.
        /// </summary>
        public bool? EnableCloudSync { get; set; }
    }

}