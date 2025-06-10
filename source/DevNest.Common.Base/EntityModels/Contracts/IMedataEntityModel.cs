namespace DevNest.Common.Base.Entity.Contracts
{
    /// <summary>
    /// Represents the interface instance for meta data information.
    /// </summary>
    public interface IMedataEntityModel
    {
        /// <summary>
        /// Gets or sets the meta data entity information.
        /// </summary>
        MetadataEntityModel? Metadata { get; set; }
    }
}