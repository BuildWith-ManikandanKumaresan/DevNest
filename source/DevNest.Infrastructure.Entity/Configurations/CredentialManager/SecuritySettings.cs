namespace DevNest.Infrastructure.Entity.Configurations.CredentialManager
{
    /// <summary>
    /// Represents general settings for the Credential Manager application.
    /// </summary>
    public class SecuritySettings
    {
        /// <summary>
        /// Enable or disable the use of a master password for accessing credentials.
        /// </summary>
        public bool? EnableCredentialExpirationCheck { get; set; }

        /// <summary>
        /// The number of days after which credentials will expire by default.
        /// </summary>
        public int? DefaultCredentialExpirationDays { get; set; }

        /// <summary>
        /// Indicates whether to automatically archive expired credentials.
        /// </summary>
        public bool? AutoArchiveExpiredCredentials { get; set; }

        /// <summary>
        /// Indicates whether to enable password history for credentials.
        /// </summary>
        public bool? EnablePasswordHistory { get; set; }

        /// <summary>
        /// The number of previous passwords to keep in history for each credential.
        /// </summary>
        public bool? EnableTwoFactorForDeletion { get; set; }

        /// <summary>
        /// Indicates whether to allow exporting credentials to a file.
        /// </summary>
        public bool? AllowExport { get; set; }

        /// <summary>
        /// Indicates whether to show a password strength meter in the UI.
        /// </summary>
        public bool? ShowPasswordStrengthMeter { get; set; }
    }

}