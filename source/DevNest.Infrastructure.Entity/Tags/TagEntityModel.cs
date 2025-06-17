#region using directives
using DevNest.Common.Base.Entity.Contracts;
#endregion using directives

namespace DevNest.Infrastructure.Entity.Tags
{
    /// <summary>
    /// Represents a tag entity in the system.
    /// </summary>
    public class TagEntityModel : BaseEntityModel
    {
        /// <summary>
        /// Unique identifier for the tag.
        /// </summary>
        public string? TagId { get; set; } = default!;

        /// <summary>
        /// Name of the tag.
        /// </summary>
        public string? Name { get; set; } = default!;

        /// <summary>
        /// Slug for the tag, used in URLs.
        /// </summary>
        public string? Slug { get; set; } = default!;

        /// <summary>
        /// Category of the tag, used for grouping or filtering tags.
        /// </summary>
        public string[]? Categories { get; set; } = default!;

        /// <summary>
        /// Color associated with the tag, represented as a hex string.
        /// </summary>
        public string? Color { get; set; } = "#000000";

        /// <summary>
        /// Text color for the tag, represented as a hex string.
        /// </summary>
        public string? TextColor { get; set; } = "#FFFFFF";

        /// <summary>
        /// Icon associated with the tag, typically a font icon or image URL.
        /// </summary>
        public string? Icon { get; set; } = default!;

        /// <summary>
        /// Indicates whether the tag is a system tag.
        /// </summary>
        public bool? IsSystem { get; set; } = false;
    }
}
