namespace DevNest.Common.Base.DTOs
{
    /// <summary>
    /// Represents the class instance for entity history.
    /// </summary>
    public class HistoryDTO
    {

        /// <summary>
        /// Gets or sets the date and time the entity was created.
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the user who created the entity.
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time the entity was last updated.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the user who last updated the entity.
        /// </summary>
        public string? UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time the entity was last accessed.
        /// </summary>
        public DateTime? LastAccessed { get; set; }

        /// <summary>
        /// Gets or sets the user who last accessed the entity.
        /// </summary>
        public string? LastAccessedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time the entity was last validated.
        /// </summary>
        public DateTime? LastValidatedAt { get; set; }

        /// <summary>
        /// Gets or sets the user who last validated the entity.
        /// </summary>
        public string? LastValidatedBy { get; set; }
    }
}