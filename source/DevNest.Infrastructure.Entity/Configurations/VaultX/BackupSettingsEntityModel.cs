namespace DevNest.Infrastructure.Entity.Configurations.VaultX
{
    /// <summary>
    /// Represents Backup settings for the Credential Manager application.
    /// </summary>
    public class BackupSettingsEntityModel
    {
        /// <summary>
        /// Enable or disable automatic backups.
        /// </summary>
        public bool? EnableCloudSync { get; set; }
    }

}