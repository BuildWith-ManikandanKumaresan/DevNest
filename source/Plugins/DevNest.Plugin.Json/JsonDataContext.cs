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
            _JsonHandler = new JsonDataHandler<T>(_connectionParams ?? new Dictionary<string, object>());
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
        public void Add(T entity)
        {
            if (entity == null)
                return;
            if(entity is List<CredentialEntity>)
            {
                _JsonHandler.Write(entity as List<T>);
            }
        }

        /// <summary>
        /// Deletes the entity of type T by its identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Delete(Guid id)
        {
            var data = _JsonHandler.Read() as List<CredentialEntity>;
            int res = data.RemoveAll(x => x.Id == id);
            if(res > 0)
            {
                _JsonHandler.Write(data as List<T>);
            }
        }

        /// <summary>
        /// Deletes all entities of type T from the data context.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void DeleteAll()
        {
            var data = _JsonHandler.Read() as List<CredentialEntity>;
            int res = data.RemoveAll(a=>a.Id == a.Id);
            if (res > 0)
            {
                _JsonHandler.Write(data as List<T>);
            }
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
            return data.FirstOrDefault(a => a.Id == id) as T;
        }

        /// <summary>
        /// Updates an existing entity of type T in the data context.
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Update(T entity = null)
        {
        }
    }
}
