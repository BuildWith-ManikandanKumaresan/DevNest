namespace DevNest.Common.Base.Entity.Contracts
{
    /// <summary>
    /// Gets or sets the interface instance for entity history.
    /// </summary>
    public interface IHistoryEntityModel
    {
        /// <summary>
        /// Gets or sets the history informations of entity.
        /// </summary>
        HistoryEntityModel? HistoryInformation { get; set; }
    }
}