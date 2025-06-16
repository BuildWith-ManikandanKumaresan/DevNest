#region using directives
using System.ComponentModel.DataAnnotations;
#endregion using directives

namespace DevNest.Infrastructure.DTOs.Credential.Request
{
    /// <summary>
    /// Represents the request DTO for Security-related properties of credentials.
    /// </summary>
    public class SecurityDetailsRequest
    {
        /// <summary>
        /// Gets or sets a value indicating whether encryption is enabled.
        /// </summary>
        [Required(ErrorMessage = "Encryption status is required.")]
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
