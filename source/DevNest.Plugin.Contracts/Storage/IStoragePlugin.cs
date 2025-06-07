namespace DevNest.Plugin.Contracts.Storage
{
    /// <summary>
    /// Represents the interface instances of storage plugins must implement.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IStoragePlugin : IPlugin
    {
        /// <summary>
        /// Gets the data context for the plugin with the specified connection parameters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionParams"></param>
        /// <returns></returns>
        IStorageDataContext<T>? GetStorageDataContext<T>(Dictionary<string, object> connectionParams) where T : class;
    }
}