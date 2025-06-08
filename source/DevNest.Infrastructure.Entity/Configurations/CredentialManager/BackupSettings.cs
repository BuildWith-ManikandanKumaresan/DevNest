namespace DevNest.Infrastructure.Entity.Configurations.CredentialManager
{
    /// <summary>
    /// Represents Backup settings for the Credential Manager application.
    /// </summary>
    public class BackupSettings
    {
        /// <summary>
        /// Enable or disable automatic backups.
        /// </summary>
        public bool? EnableCloudSync { get; set; }
    }

}