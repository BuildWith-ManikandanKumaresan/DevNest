namespace DevNest.Common.Base.DTOs.Contracts
{
    /// <summary>
    /// Represents the interface instance for meta data information.
    /// </summary>
    public interface IMetadataResponseDTO
    {
        /// <summary>
        /// Gets or sets the meta data entity information.
        /// </summary>
        MetadataResponseDTO? Metadata { get; set; }
    }
}