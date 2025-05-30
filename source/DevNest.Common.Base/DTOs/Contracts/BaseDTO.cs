namespace DevNest.Common.Base.DTOs.Contracts
{
    /// <summary>
    /// Gets or sets the base entity DTO class.
    /// </summary>
    public abstract class BaseDTO : IMetadataDTO, IHistoryDTO
    {
        /// <summary>
        /// Gets or sets the metadata property.
        /// </summary>
        public MetadataDTO? Metadata { get; set; }

        /// <summary>
        /// Gets or sets the history information property.
        /// </summary>
        public HistoryDTO? HistoryInformation { get; set; }
    }
}
