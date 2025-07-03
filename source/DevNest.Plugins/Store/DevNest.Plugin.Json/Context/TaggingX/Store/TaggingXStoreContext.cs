#region using directives
using DevNest.Common.Logger;
using DevNest.Infrastructure.Entity.TaggingX;
using DevNest.Plugin.Contracts.Context;
using DevNest.Store.Plugin.Json.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion using directives

namespace DevNest.Store.Plugin.Json.Context.TaggingX.Store
{
    /// <summary>
    /// Represents the class instance for <see cref="TaggingXStoreContext{T}">class.</see>/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_connectionParams"></param>
    /// <param name="logger"></param>
    public class TaggingXStoreContext<T>(Dictionary<string, object>? _connectionParams, IAppLogger<JsonStorePlugin> logger) : IStoreContext<T> where T : TagEntityModel
    {
        private readonly JsonDataHandler<T> _JsonHandler = new(_connectionParams ?? [], logger);
        private readonly IAppLogger<JsonStorePlugin> _logger = logger;

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