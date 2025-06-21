#region using directives
using DevNest.Common.Logger;
using DevNest.Infrastructure.Entity.Tags;
using DevNest.Plugin.Contracts.Storage;
using DevNest.Plugin.Json.Handler;
#endregion using directives

namespace DevNest.Plugin.Json.Context
{
    /// <summary>
    /// Represents the class instance for JSON data context plugin for Tags.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_connectionParams"></param>
    /// <param name="logger"></param>
    public class JsonTagContext<T>(Dictionary<string, object>? _connectionParams, IAppLogger<JsonStoragePlugin> logger) : IStoreContext<T> where T : TagEntityModel
    {
        private readonly JsonDataHandler<T> _JsonHandler = new(_connectionParams ?? [], logger);
        private readonly IAppLogger<JsonStoragePlugin> _logger = logger;

        /// <summary>
        /// Gets the connection parameters for the data context.
        /// </summary>
        public Dictionary<string, object>? ConnectionParams { get; private set; } = _connectionParams;

        public T? Add(T? entity)
        {
            throw new NotImplementedException();
        }

        public bool Archive(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAll()
        {
            throw new NotImplementedException();
        }

        public IList<T>? Get()
        {
            throw new NotImplementedException();
        }

        public T? GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public T? Update(T? entity)
        {
            throw new NotImplementedException();
        }
    }
}
