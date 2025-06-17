#region using directives
using DevNest.Common.Base.DTOs.Contracts;
using DevNest.Infrastructure.DTOs.Credential.Response;
#endregion using directives

namespace DevNest.Infrastructure.DTOs.CredentialManager.Response
{
    /// <summary>
    /// Represents the DTO class instance for credentials.
    /// </summary>
    public class CredentialResponseDTO : BaseResponseDTO
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
        /// Gets or sets a value indicating whether the password is masked.
        /// </summary>
        public bool? IsPasswordMasked { get; set; }

        /// <summary>
        /// Gets or sets the number of times the credential was used.
        /// </summary>
        public int? UsageCount { get; set; }

        /// <summary>
        /// Gets or sets the details for the credential.
        /// </summary>
        public CredentialDetailsResponseDTO? Details { get; set; }

        /// <summary>
        /// Gets or sets the password health information for the credential.
        /// </summary>
        public PasswordHealthResponseDTO? PasswordHealth { get; set; }

        /// <summary>
        /// Gets or sets the security details for the credential.
        /// </summary>
        public SecurityResponseDTO? Security { get; set; }

        /// <summary>
        /// Gets or sets the validity information for the credential.
        /// </summary>
        public ValidityResponseDTO? Validatity { get; set; }

        /// <summary>
        /// Gets or sets the associated groups.
        /// </summary>
        public string[]? AssociatedGroups { get; set; }
    }
}
