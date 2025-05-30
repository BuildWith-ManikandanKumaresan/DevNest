namespace DevNest.Common.Base.Contracts
{
    /// <summary>
    /// Represents the interface structure for standard configuration injector.
    /// </summary>
    public interface IApplicationConfigService<T>
    {
        /// <summary>
        /// Gets the configuration of type T.
        /// </summary>
        T? Value { get; }
    }
}