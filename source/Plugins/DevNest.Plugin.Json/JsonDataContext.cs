#region using directives
using DevNest.Infrastructure.Entity;
using DevNest.Plugin.Contracts;
using DevNest.Plugin.Json.Handler;
#endregion using directives

namespace DevNest.Plugin.Json
{
    /// <summary>
    /// Represents the class instance for JSON data context plugin.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JsonDataContext<T> : IDataContext<T> where T : class
    {
        private readonly JsonDataHandler<T> _JsonHandler;
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonDataContext{T}"/> class with the specified connection parameters.
        /// </summary>
        /// <param name="_connectionParams"></param>
        public JsonDataContext(Dictionary<string, object>? _connectionParams)
        {
            ConnectionParams = _connectionParams;
            _JsonHandler = new JsonDataHandler<T>(_connectionParams ?? []);
        }

        /// <summary>
        /// Gets the connection parameters for the data context.
        /// </summary>
        public Dictionary<string, object>? ConnectionParams { get; private set; }

        /// <summary>
        /// Add the entity type of T to the data context.
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="NotImplementedException"></exception>
        public T? Add(T? entity)
        {
            if (entity == null)
                return entity;
            var data = _JsonHandler.Read() as List<CredentialEntity>;
            if (!data?.Exists(a=>a.Id == (entity as CredentialEntity ?? new()).Id) ?? false)
            {
                data?.Add((entity as CredentialEntity ?? new()));
                _JsonHandler.Write(data as List<T> ?? []);
            }
            return entity;
        }

        /// <summary>
        /// Deletes the entity of type T by its identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="NotImplementedException"></exception>
        public bool Delete(Guid id)
        {
            var data = _JsonHandler.Read() as List<CredentialEntity>;
            int res = data?.RemoveAll(x => x.Id == id) ?? 0;
            if(res > 0)
            {
                _JsonHandler.Write(data as List<T> ?? []);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Deletes all entities of type T from the data context.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public bool DeleteAll()
        {
            var data = _JsonHandler.Read() as List<CredentialEntity>;
            int res = data?.RemoveAll(a => a.Id == a.Id) ?? 0;
            if (res > 0)
            {
                _JsonHandler.Write(data as List<T> ?? []);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets the collection of entities of type T from the data context.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T>? Get()
        {
            var data = _JsonHandler.Read() as List<CredentialEntity>;
            return data as IEnumerable<T>;
        }

        /// <summary>
        /// Gets an entity of type T by its identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public T? GetById(Guid id)
        {
            var data = _JsonHandler.Read() as List<CredentialEntity>;
            return data?.FirstOrDefault(a => a.Id == id) as T;
        }

        /// <summary>
        /// Updates an existing entity of type T in the data context.
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="NotImplementedException"></exception>
        public T? Update(T? entity)
        {
            var data = _JsonHandler.Read() as List<CredentialEntity>;
            if (entity == null || data == null)
                return entity;
            var existingEntity = data.FirstOrDefault(a => a.Id == (entity as CredentialEntity ?? new()).Id);

            if (existingEntity != null)
            {
                // Update the existing entity with the new values
                existingEntity.Title = (entity as CredentialEntity)?.Title;
                existingEntity.Domain = (entity as CredentialEntity)?.Domain;
                existingEntity.Host = (entity as CredentialEntity)?.Host;
                existingEntity.Username = (entity as CredentialEntity)?.Username;
                existingEntity.Password = (entity as CredentialEntity)?.Password;
                existingEntity.Type = (entity as CredentialEntity)?.Type;
                existingEntity.Workspace = (entity as CredentialEntity)?.Workspace;
                existingEntity.Environment = (entity as CredentialEntity)?.Environment;
                existingEntity.Tags = (entity as CredentialEntity)?.Tags;
                
                _JsonHandler.Write(data as List<T> ?? []);
            }
            return existingEntity as T;
        }
    }
}
