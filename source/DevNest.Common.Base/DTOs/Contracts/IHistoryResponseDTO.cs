namespace DevNest.Common.Base.DTOs.Contracts
{
    /// <summary>
    /// Gets or sets the interface instance for entity history.
    /// </summary>
    public interface IHistoryResponseDTO
    {
        /// <summary>
        /// Gets or sets the history informations of entity.
        /// </summary>
        HistoryResponseDTO? HistoryInformation { get; set; }
    }
}
