namespace DevNest.Common.Base.DTOs.Contracts
{
    /// <summary>
    /// Represents the interface instance for meta data information.
    /// </summary>
    public interface IMetadataDTO
    {
        /// <summary>
        /// Gets or sets the meta data entity information.
        /// </summary>
        MetadataDTO? Metadata { get; set; }
    }
}