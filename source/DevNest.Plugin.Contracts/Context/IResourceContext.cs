namespace DevNest.Plugin.Contracts.Context
{
    /// <summary>
    /// Represents the interface instance for <see cref="IResourceContext{T}">.</see>/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IResourceContext<T> where T : class
    {
        /// <summary>
        /// Gets the connection params for the data context.
        /// </summary>
        Dictionary<string, object>? ConnectionParams { get; }

        /// <summary>
        /// Gets the collection of entities of type T.
        /// </summary>
        /// <returns></returns>
        IList<T>? Get();

        /// <summary>
        /// Gets an entity by its identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T? GetById(string id);
    }
}