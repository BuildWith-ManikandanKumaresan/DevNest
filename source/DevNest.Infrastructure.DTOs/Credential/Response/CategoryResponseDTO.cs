#region using directives
using DevNest.Common.Base.DTOs.Contracts;
#endregion using directives

namespace DevNest.Infrastructure.DTOs.Credential.Response
{
    /// <summary>
    /// Represents the DTO class instance for Credential Category.
    /// </summary>
    public class CategoryResponseDTO : BaseResponseDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier for the credential category.
        /// </summary>
        public Guid? CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the name of the credential category.
        /// </summary>
        public string? Category { get; set; }

        /// <summary>
        /// Gets or sets the name of the credential category for display purposes.
        /// </summary>
        public string? CategoryName { get; set; }

        /// <summary>
        /// Gets or sets the list of credential category types associated with this category.
        /// </summary>
        public IList<TypesResponseDTO>? Types { get; set; } = [];
    }
}