namespace DevNest.Infrastructure.Entity.TaggingX
{
    /// <summary>
    /// Represents the entity class instance for Tag Colors.
    /// </summary>
    public class TagColorsEntityModel
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
