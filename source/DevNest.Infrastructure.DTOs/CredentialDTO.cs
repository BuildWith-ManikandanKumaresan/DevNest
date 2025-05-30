#region using directives
using DevNest.Common.Base.DTOs.Contracts;
#endregion using directives

namespace DevNest.Infrastructure.DTOs
{
    /// <summary>
    /// Represents the DTO class instance for credentials.
    /// </summary>
    public class CredentialsDTO : BaseDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier of the credential.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the title or name of the credential.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets the domain associated with the credential.
        /// </summary>
        public string? Domain { get; set; }

        /// <summary>
        /// Gets or sets the host or machine address.
        /// </summary>
        public string? Host { get; set; }

        /// <summary>
        /// Gets or sets the username for login.
        /// </summary>
        public string? Username { get; set; }

        /// <summary>
        /// Gets or sets the password for login.
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// Gets or sets the type of the credential (e.g., SSH, RDP, Azure).
        /// </summary>
        public string? Type { get; set; }

        /// <summary>
        /// Gets or sets the associated workspace name or ID.
        /// </summary>
        public string? Workspace { get; set; }

        /// <summary>
        /// Gets or sets the environment (e.g., Dev, QA, Prod).
        /// </summary>
        public string? Environment { get; set; }

        /// <summary>
        /// Gets or sets the list of tag identifiers.
        /// </summary>
        public Guid[]? Tags { get; set; }

        /// <summary>
        /// Gets or sets additional notes related to the credential.
        /// </summary>
        public string? Notes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the credential is valid.
        /// </summary>
        public bool? IsValid { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the password is masked.
        /// </summary>
        public bool? IsPasswordMasked { get; set; }

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

        /// <summary>
        /// Gets or sets the number of times the credential was used.
        /// </summary>
        public int? UsageCount { get; set; }

        /// <summary>
        /// Gets or sets the credential expiration date.
        /// </summary>
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// Gets or sets the password rotation policy in days.
        /// </summary>
        public int? RotationPolicyInDays { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the credential is disabled.
        /// </summary>
        public bool? IsDisabled { get; set; }

        /// <summary>
        /// Gets or sets the password strength (e.g., Strong, Weak).
        /// </summary>
        public string? PasswordStrength { get; set; }

        /// <summary>
        /// Gets or sets the associated groups.
        /// </summary>
        public string[]? AssociatedGroups { get; set; }
    }
}
