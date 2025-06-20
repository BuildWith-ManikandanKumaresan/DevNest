#region using directives
#endregion using directives

namespace DevNest.Infrastructure.Entity.Credentials
{
    /// <summary>
    /// Represents the entity class instance for Credential Category Types.
    /// </summary>
    public class TypesEntityModel
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