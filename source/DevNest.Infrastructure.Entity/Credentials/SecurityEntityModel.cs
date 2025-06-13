#region using directives
#endregion using directives

namespace DevNest.Infrastructure.Entity.Credentials
{
    /// <summary>
    /// Represents the entity class instance for Security-related properties of credentials.
    /// </summary>
    public class SecurityEntityModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether encryption is enabled.
        /// </summary>
        public bool? IsEncrypted { get; set; }

        /// <summary>
        /// Gets or sets the encryption algorithm used (e.g., AES, RSA).
        /// </summary>
        public string? EncryptionAlgorithm { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to display passwords in encrypted form.
        /// </summary>
        public bool? ShowPasswordAsEncrypted { get; set; }
    }
}