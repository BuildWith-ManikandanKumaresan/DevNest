namespace DevNest.Infrastructure.DTOs.Search
{
    /// <summary>
    /// Represents a search request containing various search criteria.
    /// </summary>
    public class SearchRequestDTO
    {
        /// <summary>
        /// Gets or sets the Text search object.
        /// </summary>
        public TextSearchRequestDTO? TextSearch { get; set; } // {"textFilter":{"fieldName":"name","comparison":"startsWith","value":"john"}}

        /// <summary>
        /// Gets or sets the date search object.
        /// </summary>
        public DateSearchRequestDTO? DateSearch { get; set; } // {"dateFilter":{"fieldName":"createdDate","comparison":"range","from":"2025-06-01","to":"2025-06-15"}}
    }
}
