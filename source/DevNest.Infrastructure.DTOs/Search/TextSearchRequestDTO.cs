namespace DevNest.Infrastructure.DTOs.Search
{
    /// <summary>
    /// Represents the text search request data transfer object (DTO) class.
    /// </summary>
    public class TextSearchRequestDTO
    {
        /// <summary>
        /// Gets or sets the field name for the text search filter.
        /// </summary>
        public string? FieldName { get; set; } // "name"

        /// <summary>
        /// Gets or sets the comparison type for the text search filter.
        /// </summary>
        public string? Comparison { get; set; } // startsWith | contains | endsWith | equals | notEquals

        /// <summary>
        /// Gets or sets the search filter values for the text search.
        /// </summary>
        public string? Values { get; set; } // "john"
    }
}