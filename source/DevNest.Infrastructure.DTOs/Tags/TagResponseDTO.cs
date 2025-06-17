using DevNest.Common.Base.DTOs.Contracts;

namespace DevNest.Infrastructure.DTOs.Tags
{
    /// <summary>
    /// Data Transfer Object for Tag response.
    /// </summary>
    public class TagResponseDTO : BaseResponseDTO
    {
        /// <summary>
        /// Unique identifier for the tag.
        /// </summary>
        public string? TagId { get; set; }

        /// <summary>
        /// Name of the tag.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Slug for the tag, used in URLs.
        /// </summary>
        public string? Slug { get; set; }

        /// <summary>
        /// Category of the tag, used for grouping or filtering tags.
        /// </summary>
        public string[]? Categories { get; set; }

        /// <summary>
        /// Color associated with the tag, represented as a hex string.
        /// </summary>
        public string? Color { get; set; }

        /// <summary>
        /// Text color for the tag, represented as a hex string.
        /// </summary>
        public string? TextColor { get; set; }

        /// <summary>
        /// Icon associated with the tag, typically a font icon or image URL.
        /// </summary>
        public string? Icon { get; set; }

        /// <summary>
        /// Indicates whether the tag is a system tag.
        /// </summary>
        public bool? IsSystem { get; set; }
    }
}
