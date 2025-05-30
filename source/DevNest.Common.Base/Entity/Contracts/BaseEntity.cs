namespace DevNest.Common.Base.Entity.Contracts
{
    /// <summary>
    /// Gets or sets the base entity model class.
    /// </summary>
    public abstract class BaseEntity : IHistoryEntity, IMedataEntity
    {
        /// <summary>
        /// Gets or sets the metadata property.
        /// </summary>
        public MetadataEntity? Metadata { get; set; }

        /// <summary>
        /// Gets or sets the history information property.
        /// </summary>
        public HistoryEntity? HistoryInformation { get; set; }
    }
}