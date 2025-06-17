namespace DevNest.Infrastructure.DTOs.Tags
{
    /// <summary>
    /// Data Transfer Object for Tag Colors response.
    /// </summary>
    public class TagColorsResponseDTO
    {
        /// <summary>
        /// Unique identifier for the color associated with the tag.
        /// </summary>
        public int ColorId { get; set; }

        /// <summary>
        /// Name of the color associated with the tag.
        /// </summary>
        public string? Color { get; set; }

        /// <summary>
        /// Hexadecimal color code for the tag, typically used for UI representation.
        /// </summary>
        public string? TextColor { get; set; }
    }
}
