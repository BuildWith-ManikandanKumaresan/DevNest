#region using directives
#endregion using directives

namespace DevNest.Infrastructure.DTOs.Credential.Response
{
    /// <summary>
    /// Represents the DTO class instance for Credential Category Types.
    /// </summary>
    public class TypesResponseDTO
    {

        /// <summary>
        /// Gets or sets the unique identifier for the credential category type.
        /// </summary>
        public Guid? TypeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the credential category type.
        /// </summary>
        public string? Type { get; set; }

        /// <summary>
        /// Gets or sets the name of the credential category type for display purposes.
        /// </summary>
        public string? TypeName { get; set; }
    }
}
