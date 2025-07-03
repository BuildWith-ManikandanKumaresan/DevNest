#region using directives
using DevNest.Common.Base.Constants;
using DevNest.Common.Logger;
using DevNest.Infrastructure.Entity.Configurations.VaultX;
using DevNest.Infrastructure.Entity.VaultX;
using DevNest.Plugin.Contracts.Context;
using DevNest.Store.Plugin.Json.Handler;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
#endregion using directives

namespace DevNest.Store.Plugin.Json.Context.VaultX.Configuration
{
    /// <summary>
    /// Represents the class instance for <see cref="VaultXConfigurationContext">class.</see>/>
    /// </summary>
    public class VaultXConfigurationContext<T>(Dictionary<string, object>? _connectionParams, IAppLogger<JsonStorePlugin> logger) : IConfigurationContext<T> where T : VaultXConfigurationsEntityModel
    {
        private readonly JsonDataHandler<T> _JsonHandler = new(_connectionParams ?? [], logger);
        private readonly IAppLogger<JsonStorePlugin> _logger = logger;

        public Dictionary<string, object>? ConnectionParams { get; private set; } = _connectionParams;

        /// <summary>
        /// Handler method to get the configurations for VaultX.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IList<T>? Get()
        {
            _logger.LogDebug($"{nameof(VaultXConfigurationContext<T>)} => Retrieving entities from JSON storage context.", new { EntityType = typeof(T).Name });
            var data = _JsonHandler.Read(FileSearchPatternConstants.Extension_Configurations) as VaultXConfigurationsEntityModel;
            _logger.LogDebug($"{nameof(VaultXConfigurationContext<T>)} => Retrieved {data} entities from JSON storage context.", new { EntityType = typeof(T).Name });
            return data as IList<T>;
        }

        /// <summary>
        /// Handler method to get the configuration using the json root path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public T? GetByPath(string path)
        {
            _logger.LogDebug($"{nameof(VaultXConfigurationContext<T>)} => Retrieving entities from JSON storage context.", new { EntityType = typeof(T).Name });
            var data = _JsonHandler.Read(FileSearchPatternConstants.Extension_Configurations) as VaultXConfigurationsEntityModel;
            var rawData = JsonConvert.SerializeObject(data);
            if (string.IsNullOrWhiteSpace(rawData))
            {
                _logger.LogWarning("No data found in the JSON source.");
                return null;
            }

            var jObject = JObject.Parse(rawData);
            var token = jObject.SelectToken(rawData);

            if (token == null)
            {
                _logger.LogWarning($"JSON path '{rawData}' not found in configuration.");
                return null;
            }

            try
            {
                var result = token.ToObject<T>();
                _logger.LogDebug($"{nameof(VaultXConfigurationContext<T>)} => Retrieved {data} entities from JSON storage context.", new { EntityType = typeof(T).Name });
                return result as T;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error converting JSON path result to {typeof(T).Name}.", ex);
                return null;
            }
        }

        /// <summary>
        /// Handler method to update the configurations for VaultX.
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
