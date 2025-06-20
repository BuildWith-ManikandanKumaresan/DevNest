#region using directives
using DevNest.Common.Base.DTOs.Contracts;
using System.ComponentModel.DataAnnotations;
#endregion using directives

namespace DevNest.Infrastructure.DTOs.Credential.Request
{
    public class UpdateCredentialRequest : BaseResponseDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier of the credential.
        /// </summary>
        [Required(ErrorMessage = "Credential ID is required.")]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the title or name of the credential.
        /// </summary>
        [Required(ErrorMessage = "Title is required.")]
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets the environment (e.g., Dev, QA, Prod).
        /// </summary>
        [Required(ErrorMessage = "Environment is required.")]
        public string? Environment { get; set; }

        /// <summary>
        /// Gets or sets the category of the credential (e.g., Database, Server, API).
        /// </summary>
        public string? Category { get; set; }

        /// <summary>
        /// Gets or sets the list of tag identifiers.
        /// </summary>
        public string[]? Tags { get; set; }

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
        [Required(ErrorMessage = "Associated groups are required.")]
        public IList<string>? AssociatedGroups { get; set; }
    }
}
