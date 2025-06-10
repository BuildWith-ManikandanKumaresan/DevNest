namespace DevNest.Common.Base.Entity.Contracts
{
    /// <summary>
    /// Gets or sets the base entity model class.
    /// </summary>
    public abstract class BaseEntityModel : IHistoryEntityModel, IMedataEntityModel
    {
        /// <summary>
        /// Gets or sets the metadata property.
        /// </summary>
        public MetadataEntityModel? Metadata { get; set; }

        /// <summary>
        /// Gets or sets the history information property.
        /// </summary>
        public HistoryEntityModel? HistoryInformation { get; set; }
    }
}