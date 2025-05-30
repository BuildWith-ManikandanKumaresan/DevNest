namespace DevNest.Common.Base.Entity.Contracts
{
    /// <summary>
    /// Represents the interface instance for meta data information.
    /// </summary>
    public interface IMedataEntity
    {
        /// <summary>
        /// Gets or sets the meta data entity information.
        /// </summary>
        MetadataEntity? Metadata { get; set; }
    }
}