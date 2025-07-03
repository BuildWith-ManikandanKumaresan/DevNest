namespace DevNest.Plugin.Contracts.Context
{
    /// <summary>
    /// Represents the interface for data context operations.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IStoreContext<T> where T : class
    {
        /// <summary>
        /// Gets the connection params for the data context.
        /// </summary>
        Dictionary<string,object>? ConnectionParams { get; }

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
        T? GetById(Guid id);

        /// <summary>
        /// Adds a new entity to the data context.
        /// </summary>
        /// <param name="entity"></param>
        T? Add(T? entity);

        /// <summary>
        /// Updates an existing entity in the data context.
        /// </summary>
        /// <param name="entity"></param>
        T? Update(T? entity);

        /// <summary>
        /// Deletes an entity by its identifier.
        /// </summary>
        /// <param name="id"></param>
        bool Delete(Guid id);

        /// <summary>
        /// Deletes all entities in the data context.
        /// </summary>
        bool DeleteAll();

        /// <summary>
        /// Archives an entity by its identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Archive(Guid id);
    }
}