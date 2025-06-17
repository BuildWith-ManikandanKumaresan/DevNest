namespace DevNest.Common.Base.DTOs.Contracts
{
    /// <summary>
    /// Gets or sets the base entity DTO class.
    /// </summary>
    public abstract class BaseResponseDTO : IMetadataResponseDTO, IHistoryResponseDTO
    {
        /// <summary>
        /// Gets or sets the metadata property.
        /// </summary>
        public MetadataResponseDTO? Metadata { get; set; }

        /// <summary>
        /// Gets or sets the history information property.
        /// </summary>
        public HistoryResponseDTO? HistoryInformation { get; set; }
    }
}
