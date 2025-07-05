#region using directives
using DevNest.Common.Base.Constants;
using DevNest.Common.Logger;
using DevNest.Infrastructure.Entity.VaultX;
using DevNest.Plugin.Contracts.Context;
using DevNest.Store.Plugin.Json.Handler;
#endregion using directives

namespace DevNest.Store.Plugin.Json.Context.VaultX.Store
{
    /// <summary>
    /// Represents the class instance for <see cref="CredentialContext">class.</see>/>
    /// </summary>
    public class VaultXStoreContext<T>(Dictionary<string, object>? _connectionParams, IAppLogger<JsonStorePlugin> logger) : IStoreContext<T> where T : CredentialEntityModel
    {
        private readonly JsonDataHandler<T> _JsonHandler = new(_connectionParams ?? [], logger);
        private readonly IAppLogger<JsonStorePlugin> _logger = logger;

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
            _logger.LogDebug($"{nameof(VaultXStoreContext<T>)} => Adding entity to JSON storage context.", new { EntityType = typeof(T).Name, Entity = entity });
            if (entity == null)
                return entity as T;
            var data = _JsonHandler.ReadAsList(FileSearchPatternConstants.Extension_Data) as List<CredentialEntityModel>;
            if (!data?.Exists(a => a.Id == (entity as CredentialEntityModel ?? new()).Id) ?? false)
            {
                data?.Add(entity as CredentialEntityModel ?? new());
                _JsonHandler.WriteAsList(data as List<T> ?? [], FileSearchPatternConstants.Extension_Data);
            }
            _logger.LogDebug($"{nameof(VaultXStoreContext<T>)} => Entity added successfully to JSON storage context.", new { EntityType = typeof(T).Name, Entity = entity });
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
            _logger.LogDebug($"{nameof(VaultXStoreContext<T>)} => Archiving entity in JSON storage context.", new { EntityType = typeof(T).Name, Id = id });
            if (_JsonHandler.ReadAsList(FileSearchPatternConstants.Extension_Data) is not List<CredentialEntityModel> data) return false;

            var entity = data.FirstOrDefault(a => a.Id == id);
            if (entity == null)
                return false;

            if (entity != null && entity.Validatity != null)
                entity.Validatity.IsDisabled = true;

            _JsonHandler.WriteAsList([.. data.Cast<T>()], FileSearchPatternConstants.Extension_Data);

            _logger.LogDebug($"{nameof(VaultXStoreContext<T>)} => Entity archived successfully in JSON storage context.", new { EntityType = typeof(T).Name, Id = id });

            return true;
        }


        /// <summary>
        /// Deletes the entity of type T by its identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="NotImplementedException"></exception>
        public bool Delete(Guid id)
        {
            _logger.LogDebug($"{nameof(VaultXStoreContext<T>)} => Deleting entity from JSON storage context.", new { EntityType = typeof(T).Name, Id = id });
            var data = _JsonHandler.ReadAsList(FileSearchPatternConstants.Extension_Data) as List<CredentialEntityModel>;
            int res = data?.RemoveAll(x => x.Id == id) ?? 0;
            if (res > 0)
            {
                _JsonHandler.WriteAsList(data as List<T> ?? [], FileSearchPatternConstants.Extension_Data);
                _logger.LogDebug($"{nameof(VaultXStoreContext<T>)} => Entity deleted successfully from JSON storage context.", new { EntityType = typeof(T).Name, Id = id });
                return true;
            }
            _logger.LogDebug($"{nameof(VaultXStoreContext<T>)} => Entity deletion failed in JSON storage context.", new { EntityType = typeof(T).Name, Id = id });
            return false;
        }

        /// <summary>
        /// Deletes all entities of type T from the data context.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public bool DeleteAll()
        {
            _logger.LogDebug($"{nameof(VaultXStoreContext<T>)} => Deleting all entities from JSON storage context.", new { EntityType = typeof(T).Name });
            var data = _JsonHandler.ReadAsList(FileSearchPatternConstants.Extension_Data) as List<CredentialEntityModel>;
            int res = data?.RemoveAll(a => a.Id == a.Id) ?? 0;
            if (res > 0)
            {
                _JsonHandler.WriteAsList(data as List<T> ?? [], FileSearchPatternConstants.Extension_Data);
                _logger.LogDebug($"{nameof(VaultXStoreContext<T>)} => All entities deleted successfully from JSON storage context.", new { EntityType = typeof(T).Name, Count = res });
                return true;
            }
            _logger.LogDebug($"{nameof(VaultXStoreContext<T>)} => All entities deletion failed in JSON storage context.", new { EntityType = typeof(T).Name });
            return false;
        }

        /// <summary>
        /// Gets the collection of entities of type T from the data context.
        /// </summary>
        /// <returns></returns>
        public IList<T>? Get()
        {
            _logger.LogDebug($"{nameof(VaultXStoreContext<T>)} => Retrieving entities from JSON storage context.", new { EntityType = typeof(T).Name });
            var data = _JsonHandler.ReadAsList(FileSearchPatternConstants.Extension_Data) as List<CredentialEntityModel>;
            _logger.LogDebug($"{nameof(VaultXStoreContext<T>)} => Retrieved {data?.Count} entities from JSON storage context.", new { EntityType = typeof(T).Name, Count = data?.Count ?? 0 });
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
            _logger.LogDebug($"{nameof(VaultXStoreContext<T>)} => Retrieving entity by ID from JSON storage context.", new { EntityType = typeof(T).Name, Id = id });
            var data = _JsonHandler.ReadAsList(FileSearchPatternConstants.Extension_Data) as List<CredentialEntityModel>;
            var dataById = data?.FirstOrDefault(a => a.Id == id);
            _logger.LogDebug($"{nameof(VaultXStoreContext<T>)} => Retrieved entity by ID from JSON storage context.", new { EntityType = typeof(T).Name, Id = id, Entity = dataById });
            return dataById as T;
        }

        /// <summary>
        /// Updates an existing entity of type T in the data context.
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="NotImplementedException"></exception>
        public T? Update(T? entity)
        {
            _logger.LogDebug($"{nameof(VaultXStoreContext<T>)} => Updating entity in JSON storage context.", new { EntityType = typeof(T).Name, Entity = entity });
            if (entity == null || _JsonHandler.ReadAsList(FileSearchPatternConstants.Extension_Data) is not List<CredentialEntityModel> data)
                return entity;

            var input = entity as CredentialEntityModel;
            var existingEntity = data.FirstOrDefault(a => a.Id == input?.Id);

            if (existingEntity != null && input != null)
            {
                if (input.Title != null) existingEntity.Title = input.Title;
                if (input.Environment != null) existingEntity.Environment = input.Environment;
                if (input.Category != null) existingEntity.Category = input.Category;
                if (input.Tags != null) existingEntity.Tags = input.Tags;
                if (input.Notes != null) existingEntity.Notes = input.Notes;
                if (input.IsPasswordMasked != null) existingEntity.IsPasswordMasked = input.IsPasswordMasked;
                if (input.UsageCount != null) existingEntity.UsageCount = input.UsageCount;
                if (input.AssociatedGroups != null) existingEntity.AssociatedGroups = input.AssociatedGroups;

                // Details update.
                if (input.Details?.Domain != null)
                {
                    if (existingEntity.Details != null)
                        existingEntity.Details.Domain = input.Details.Domain;
                }
                if (input.Details?.Host != null)
                {
                    if (existingEntity.Details != null)
                        existingEntity.Details.Host = input.Details.Host;
                }
                if (input.Details?.Username != null)
                {
                    if (existingEntity.Details != null)
                        existingEntity.Details.Username = input.Details.Username;
                }
                if (input.Details?.Password != null)
                {
                    if (existingEntity.Details != null)
                        existingEntity.Details.Password = input.Details.Password;
                }
                if (input.Details?.Type != null)
                {
                    if (existingEntity.Details != null)
                        existingEntity.Details.Type = input.Details.Type;
                }
                if (input.Details?.Port != null)
                {
                    if (existingEntity.Details != null)
                        existingEntity.Details.Port = input.Details.Port;
                }

                // Security update.
                if (input.Security?.IsEncrypted != null)
                {
                    if (existingEntity.Security != null)
                        existingEntity.Security.IsEncrypted = input.Security.IsEncrypted;
                }
                if (input.Security?.EncryptionAlgorithm != null)
                {
                    if (existingEntity.Security != null)
                        existingEntity.Security.EncryptionAlgorithm = input.Security.EncryptionAlgorithm;
                }
                if (input.Security?.ShowPasswordAsEncrypted != null)
                {
                    if (existingEntity.Security != null)
                        existingEntity.Security.ShowPasswordAsEncrypted = input.Security.ShowPasswordAsEncrypted;
                }

                // Validity update.

                if (input.Validatity?.IsDisabled != null)
                {
                    if (existingEntity.Validatity != null)
                        existingEntity.Validatity.IsDisabled = input.Validatity.IsDisabled;
                }
                if (input.Validatity?.RotationPolicyInDays != null)
                {
                    if (existingEntity.Validatity != null)
                        existingEntity.Validatity.RotationPolicyInDays = input.Validatity.RotationPolicyInDays;
                }
                if (input.Validatity?.ExpirationDate != null)
                {
                    if (existingEntity.Validatity != null)
                        existingEntity.Validatity.ExpirationDate = input.Validatity.ExpirationDate;
                }

                // History information update.
                if (input.HistoryInformation != null)
                {
                    if (existingEntity.HistoryInformation != null)
                    {
                        existingEntity.HistoryInformation.UpdatedBy = existingEntity.HistoryInformation.LastAccessedBy = input.HistoryInformation.UpdatedBy;
                        existingEntity.HistoryInformation.UpdatedAt = existingEntity.HistoryInformation.LastAccessed = DateTime.Now;
                    }
                }

                _JsonHandler.WriteAsList(data as List<T> ?? [], FileSearchPatternConstants.Extension_Data);
                _logger.LogDebug($"{nameof(VaultXStoreContext<T>)} => Entity updated successfully in JSON storage context.", new { EntityType = typeof(T).Name, Entity = existingEntity });
            }

            return existingEntity as T;
        }
    }
}
