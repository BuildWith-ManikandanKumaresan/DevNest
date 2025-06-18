namespace DevNest.Infrastructure.Entity.Search
{
    /// <summary>
    /// Represents the search filter entity model class.
    /// </summary>
    public class SearchEntityModel
    {
        /// <summary>
        /// Gets or sets the Text search object.
        /// </summary>
        public TextSearchEntityModel? TextSearch { get; set; } // {"textFilter":{"fieldName":"name","comparison":"startsWith","value":"john"}}

        /// <summary>
        /// Gets or sets the date search object.
        /// </summary>
        public DateSearchEntityModel? DateSearch { get; set; } // {"dateFilter":{"fieldName":"createdDate","comparison":"range","from":"2025-06-01","to":"2025-06-15"}}

    }
}
