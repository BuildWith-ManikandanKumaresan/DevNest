#region using directives
using DevNest.Common.Base.Constants;
using DevNest.Common.Logger;
using DevNest.Infrastructure.Entity.VaultX;
using DevNest.Plugin.Contracts.Context;
using DevNest.Store.Plugin.Json.Handler;
#endregion using directives

namespace DevNest.Store.Plugin.Json.Context.VaultX.Resource
{
    /// <summary>
    /// Represents the class instance for <see cref="CategoryResourceContext"></see>/>
    /// </summary>
    public class VaultXCategoryResourceContext<T>(Dictionary<string, object>? _connectionParams, IAppLogger<JsonStorePlugin> logger) : IResourceContext<T> where T : CategoryEntityModel
    {
        private readonly JsonDataHandler<T> _JsonHandler = new(_connectionParams ?? [], logger);
        private readonly IAppLogger<JsonStorePlugin> _logger = logger;

        /// <summary>
        /// Gets the connection parameters for the data context.
        /// </summary>
        public Dictionary<string, object>? ConnectionParams { get; private set; } = _connectionParams;

        /// <summary>
        /// Handler method to get all the resources.
        /// </summary>
        /// <returns></returns>
        public IList<T>? Get()
        {
            _logger.LogDebug($"{nameof(VaultXCategoryResourceContext<T>)} => Retrieving entities from JSON resource context.", new { EntityType = typeof(T).Name });
            var data = _JsonHandler.ReadAsList(FileSearchPatternConstants.Extension_Resources) as List<CategoryEntityModel>;
            _logger.LogDebug($"{nameof(VaultXCategoryResourceContext<T>)} => Retrieved {data?.Count} entities from JSON resource context.", new { EntityType = typeof(T).Name, Count = data?.Count ?? 0 });
            return data as IList<T>;
        }

        /// <summary>
        /// Handler method to get the resource by the identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T? GetById(string id)
        {
            _logger.LogDebug($"{nameof(VaultXCategoryResourceContext<T>)} => Retrieving entities from JSON resource context.", new { EntityType = typeof(T).Name });
            var data = _JsonHandler.ReadAsList(FileSearchPatternConstants.Extension_Resources) as List<CategoryEntityModel>;
            var dataById = data?.FirstOrDefault(a => a.CategoryId.ToString() == id);
            _logger.LogDebug($"{nameof(VaultXCategoryResourceContext<T>)} => Retrieved {data?.Count} entities from JSON resource context.", new { EntityType = typeof(T).Name, Count = data?.Count ?? 0 });
            return dataById as T;
        }
    }
}
