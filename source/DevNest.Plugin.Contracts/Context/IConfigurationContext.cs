namespace DevNest.Plugin.Contracts.Context
{
    /// <summary>
    /// Represents the interface instance for <see cref="IConfigurationContext">.</see>/>
    /// </summary>
    public interface IConfigurationContext<T> where T : class
    {
        /// <summary>
        /// Gets the connection params for the data context.
        /// </summary>
        Dictionary<string, object>? ConnectionParams { get; }

        /// <summary>
        /// Gets the collection of entities of type T.
        /// </summary>
        /// <returns></returns>
        T? Get();

        /// <summary>
        /// Updates an existing entity in the data context.
        /// </summary>
        /// <param name="entity"></param>
        T? Update(T? entity);
    }
}
