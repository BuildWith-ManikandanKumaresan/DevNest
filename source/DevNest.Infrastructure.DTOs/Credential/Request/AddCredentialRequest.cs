#region using directives
using DevNest.Common.Base.DTOs.Contracts;
using DevNest.Infrastructure.DTOs.Credential.Request;
#endregion using directives

namespace DevNest.Infrastructure.DTOs.CredentialManager.Request
{
    /// <summary>
    /// Represents the class instance for add credentials request.
    /// </summary>
    public class AddCredentialRequest : BaseDTO
    {
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
        /// Gets or sets the details for the credential.
        /// </summary>
        public CredentialDetailsRequest? Details { get; set; }

        /// <summary>
        /// Gets or sets the security details for the credential.
        /// </summary>
        public SecurityDetailsRequest? Security { get; set; }

        /// <summary>
        /// Gets or sets the validity information for the credential.
        /// </summary>
        public ValidityDetailsRequest? Validatity { get; set; }

        /// <summary>
        /// Gets or sets the associated groups.
        /// </summary>
        public string[]? AssociatedGroups { get; set; }
    }
}
