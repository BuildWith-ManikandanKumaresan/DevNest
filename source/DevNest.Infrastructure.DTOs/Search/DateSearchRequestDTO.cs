namespace DevNest.Infrastructure.DTOs.Search
{
    /// <summary>
    /// Represents the date search request DTO class.
    /// </summary>
    public class DateSearchRequestDTO
    {
        /// <summary>
        /// Gets or sets the field name.
        /// </summary>
        public string? FieldName { get; set; } // "createdDate"

        /// <summary>
        /// Gets or sets the comparison type.
        /// </summary>
        public string? Comparison { get; set; } // exact | notExact | range | notInRange

        /// <summary>
        /// Gets or sets the date range for the search filter.
        /// </summary>
        public DateTime? From { get; set; } // "2025-06-01"

        /// <summary>
        /// Gets or sets the end date for the search filter.
        /// </summary>
        public DateTime? To { get; set; } // "2025-06-15"
    }
}