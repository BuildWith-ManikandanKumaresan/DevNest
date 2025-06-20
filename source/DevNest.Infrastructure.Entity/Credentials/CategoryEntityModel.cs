#region using directives
using DevNest.Common.Base.Entity.Contracts;
#endregion using directives

namespace DevNest.Infrastructure.Entity.Credentials
{
    /// <summary>
    /// Represents the entity class instance for Credential Category.
    /// </summary>
    public class CategoryEntityModel : BaseEntityModel
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
        public IList<TypesEntityModel>? Types { get; set; } = [];
    }
}