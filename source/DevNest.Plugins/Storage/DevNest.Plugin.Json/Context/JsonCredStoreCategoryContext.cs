#region using directives
using DevNest.Common.Base.Constants;
using DevNest.Common.Logger;
using DevNest.Infrastructure.Entity.Credentials;
using DevNest.Plugin.Contracts.Storage;
using DevNest.Plugin.Json.Handler;
#endregion using directives

namespace DevNest.Plugin.Json.Context
{
    /// <summary>
    /// Represents the class instance for JSON data context plugin for Credential Category entities.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_connectionParams"></param>
    /// <param name="logger"></param>
    public class JsonCredStoreCategoryContext<T>(Dictionary<string, object>? _connectionParams, IAppLogger<JsonStoragePlugin> logger) : IStorageContext<T> where T : CategoryEntityModel
    {
        private readonly JsonDataHandler<T> _JsonHandler = new(_connectionParams ?? [], logger);
        private readonly IAppLogger<JsonStoragePlugin> _logger = logger;

        /// <summary>
        /// Gets the connection parameters for the data context.
        /// </summary>
        public Dictionary<string, object>? ConnectionParams { get; private set; } = _connectionParams;

        /// <summary>
        /// Adds a new entity to the data context.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public T? Add(T? entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Archives an entity by its identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Archive(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes an entity by its identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes all entities in the data context.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool DeleteAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves the collection of entities of type T from the JSON storage context.
        /// </summary>
        /// <returns></returns>
        public IList<T>? Get()
        {
            _logger.LogDebug($"{nameof(JsonCredStoreCategoryContext<T>)} => Retrieving entities from JSON storage context.", new { EntityType = typeof(T).Name });
            var data = _JsonHandler.Read(FileSearchPatternConstants.Extension_Resources) as List<CategoryEntityModel>;
            _logger.LogDebug($"{nameof(JsonCredStoreCategoryContext<T>)} => Retrieved {data?.Count} entities from JSON storage context.", new { EntityType = typeof(T).Name, Count = data?.Count ?? 0 });
            return data as IList<T>;
        }

        /// <summary>
        /// Retrieves an entity by its identifier from the JSON storage context.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T? GetById(Guid id)
        {
            _logger.LogDebug($"{nameof(JsonCredStoreCategoryContext<T>)} => Retrieving entities from JSON storage context.", new { EntityType = typeof(T).Name });
            var data = _JsonHandler.Read(FileSearchPatternConstants.Extension_Resources) as List<CategoryEntityModel>;
            var dataById = data?.FirstOrDefault(a => a.CategoryId == id);
            _logger.LogDebug($"{nameof(JsonCredStoreCategoryContext<T>)} => Retrieved {data?.Count} entities from JSON storage context.", new { EntityType = typeof(T).Name, Count = data?.Count ?? 0 });
            return dataById as T;
        }

        /// <summary>
        /// Updates an existing entity in the data context.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public T? Update(T? entity)
        {
            throw new NotImplementedException();
        }
    }
}
