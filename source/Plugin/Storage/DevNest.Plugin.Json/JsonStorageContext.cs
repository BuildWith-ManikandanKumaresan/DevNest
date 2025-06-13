#region using directives
using DevNest.Common.Logger;
using DevNest.Infrastructure.Entity.Credentials;
using DevNest.Plugin.Contracts.Storage;
using DevNest.Plugin.Json.Handler;
#endregion using directives

namespace DevNest.Plugin.Json
{
    /// <summary>
    /// Represents the class instance for JSON data context plugin.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks>
    /// Initializes a new instance of the <see cref="JsonStorageContext{T}"/> class with the specified connection parameters.
    /// </remarks>
    /// <param name="_connectionParams"></param>
    public class JsonStorageContext<T>(Dictionary<string, object>? _connectionParams, IAppLogger<JsonStoragePlugin> logger) : IStorageContext<T> where T : class
    {
        private readonly JsonDataHandler<T> _JsonHandler = new(_connectionParams ?? [],logger);
        private readonly IAppLogger<JsonStoragePlugin> _logger = logger;

        /// <summary>
        /// Gets the connection parameters for the data context.
        /// </summary>
        public Dictionary<string, object>? ConnectionParams { get; private set; } = _connectionParams;

        /// <summary>
        /// Add the entity type of T to the data context.
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="NotImplementedException"></exception>
        public T? Add(T? entity)
        {
            _logger.LogDebug($"{nameof(JsonStorageContext<T>)} => Adding entity to JSON storage context.", new { EntityType = typeof(T).Name, Entity = entity });
            if (entity == null)
                return entity as T;
            var data = _JsonHandler.Read() as List<CredentialEntityModel>;
            if (!data?.Exists(a=>a.Id == (entity as CredentialEntityModel ?? new()).Id) ?? false)
            {
                data?.Add((entity as CredentialEntityModel ?? new()));
                _JsonHandler.Write(data as List<T> ?? []);
            }
            _logger.LogDebug($"{nameof(JsonStorageContext<T>)} => Entity added successfully to JSON storage context.", new { EntityType = typeof(T).Name, Entity = entity });
            return entity;
        }

        /// <summary>
        /// Archives the entity of type T by its identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Archive(Guid id)
        {
            _logger.LogDebug($"{nameof(JsonStorageContext<T>)} => Archiving entity in JSON storage context.", new { EntityType = typeof(T).Name, Id = id });
            if (_JsonHandler.Read() is not List<CredentialEntityModel> data) return false;

            var entity = data.FirstOrDefault(a => a.Id == id);
            if (entity == null) return false;

            entity.Validatity.IsDisabled = true;

            _JsonHandler.Write([.. data.Cast<T>()]);

            _logger.LogDebug($"{nameof(JsonStorageContext<T>)} => Entity archived successfully in JSON storage context.", new { EntityType = typeof(T).Name, Id = id });

            return true;
        }


        /// <summary>
        /// Deletes the entity of type T by its identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="NotImplementedException"></exception>
        public bool Delete(Guid id)
        {
            _logger.LogDebug($"{nameof(JsonStorageContext<T>)} => Deleting entity from JSON storage context.", new { EntityType = typeof(T).Name, Id = id });
            var data = _JsonHandler.Read() as List<CredentialEntityModel>;
            int res = data?.RemoveAll(x => x.Id == id) ?? 0;
            if(res > 0)
            {
                _JsonHandler.Write(data as List<T> ?? []);
                _logger.LogDebug($"{nameof(JsonStorageContext<T>)} => Entity deleted successfully from JSON storage context.", new { EntityType = typeof(T).Name, Id = id });
                return true;
            }
            _logger.LogDebug($"{nameof(JsonStorageContext<T>)} => Entity deletion failed in JSON storage context.", new { EntityType = typeof(T).Name, Id = id });
            return false;
        }

        /// <summary>
        /// Deletes all entities of type T from the data context.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public bool DeleteAll()
        {
            _logger.LogDebug($"{nameof(JsonStorageContext<T>)} => Deleting all entities from JSON storage context.", new { EntityType = typeof(T).Name });
            var data = _JsonHandler.Read() as List<CredentialEntityModel>;
            int res = data?.RemoveAll(a => a.Id == a.Id) ?? 0;
            if (res > 0)
            {
                _JsonHandler.Write(data as List<T> ?? []);
                _logger.LogDebug($"{nameof(JsonStorageContext<T>)} => All entities deleted successfully from JSON storage context.", new { EntityType = typeof(T).Name, Count = res });
                return true;
            }
            _logger.LogDebug($"{nameof(JsonStorageContext<T>)} => All entities deletion failed in JSON storage context.", new { EntityType = typeof(T).Name });
            return false;
        }

        /// <summary>
        /// Gets the collection of entities of type T from the data context.
        /// </summary>
        /// <returns></returns>
        public IList<T>? Get()
        {
            _logger.LogDebug($"{nameof(JsonStorageContext<T>)} => Retrieving entities from JSON storage context.", new { EntityType = typeof(T).Name });
            var data = _JsonHandler.Read() as List<CredentialEntityModel>;
            _logger.LogDebug($"{nameof(JsonStorageContext<T>)} => Retrieved {data?.Count} entities from JSON storage context.", new { EntityType = typeof(T).Name, Count = data?.Count ?? 0 });
            return data as IList<T>;
        }

        /// <summary>
        /// Gets an entity of type T by its identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public T? GetById(Guid id)
        {
            _logger.LogDebug($"{nameof(JsonStorageContext<T>)} => Retrieving entity by ID from JSON storage context.", new { EntityType = typeof(T).Name, Id = id });
            var data = _JsonHandler.Read() as List<CredentialEntityModel>;
            var dataById = data?.FirstOrDefault(a => a.Id == id);
            _logger.LogDebug($"{nameof(JsonStorageContext<T>)} => Retrieved entity by ID from JSON storage context.", new { EntityType = typeof(T).Name, Id = id, Entity = dataById });
            return dataById as T;
        }

        /// <summary>
        /// Updates an existing entity of type T in the data context.
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="NotImplementedException"></exception>
        public T? Update(T? entity)
        {
            _logger.LogDebug($"{nameof(JsonStorageContext<T>)} => Updating entity in JSON storage context.", new { EntityType = typeof(T).Name, Entity = entity });
            if (entity == null || _JsonHandler.Read() is not List<CredentialEntityModel> data)
                return entity;

            var input = entity as CredentialEntityModel;
            var existingEntity = data.FirstOrDefault(a => a.Id == input?.Id);

            if (existingEntity != null && input != null)
            {
                if (input.Title != null) existingEntity.Title = input.Title;
                if (input.Details.Domain != null) existingEntity.Details.Domain = input.Details.Domain;
                if (input.Details.Host != null) existingEntity.Details.Host = input.Details.Host;
                if (input.Details.Username != null) existingEntity.Details.Username = input.Details.Username;
                if (input.Details.Password != null) existingEntity.Details.Password = input.Details.Password;
                if (input.Details.Type != null) existingEntity.Details.Type = input.Details.Type;
                if (input.Workspace != null) existingEntity.Workspace = input.Workspace;
                if (input.Environment != null) existingEntity.Environment = input.Environment;
                if (input.Tags != null) existingEntity.Tags = input.Tags;
                if (input.Notes != null) existingEntity.Notes = input.Notes;
                if (input.IsPasswordMasked != null) existingEntity.IsPasswordMasked = input.IsPasswordMasked;
                if (input.Security.IsEncrypted != null) existingEntity.Security.IsEncrypted = input.Security.IsEncrypted;
                if (input.Security.EncryptionAlgorithm != null) existingEntity.Security.EncryptionAlgorithm = input.Security.EncryptionAlgorithm;
                if (input.Security.ShowPasswordAsEncrypted != null) existingEntity.Security.ShowPasswordAsEncrypted = input.Security.ShowPasswordAsEncrypted;
                if (input.AssociatedGroups != null) existingEntity.AssociatedGroups = input.AssociatedGroups;

                _JsonHandler.Write(data as List<T> ?? []);
                _logger.LogDebug($"{nameof(JsonStorageContext<T>)} => Entity updated successfully in JSON storage context.", new { EntityType = typeof(T).Name, Entity = existingEntity });
            }

            return existingEntity as T;
        }

    }
}
