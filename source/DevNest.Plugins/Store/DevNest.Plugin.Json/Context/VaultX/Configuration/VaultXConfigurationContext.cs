#region using directives
using DevNest.Common.Base.Constants;
using DevNest.Common.Logger;
using DevNest.Infrastructure.Entity.Configurations.VaultX;
using DevNest.Infrastructure.Entity.VaultX;
using DevNest.Plugin.Contracts.Context;
using DevNest.Store.Plugin.Json.Context.VaultX.Store;
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
        public T? Get()
        {
            _logger.LogDebug($"{nameof(VaultXConfigurationContext<T>)} => Retrieving entities from JSON configuration context.", new { EntityType = typeof(T).Name });
            var data = _JsonHandler.ReadAsObject(FileSearchPatternConstants.Extension_Configurations) as VaultXConfigurationsEntityModel;
            _logger.LogDebug($"{nameof(VaultXConfigurationContext<T>)} => Retrieved {data} entities from JSON configuration context.", new { EntityType = typeof(T).Name });
            return data as T;
        }

        /// <summary>
        /// Handler method to update the configurations for VaultX.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public T? Update(T? entity)
        {
            _logger.LogDebug($"{nameof(VaultXConfigurationContext<T>)} => Updating entity in JSON configuration context.", new { EntityType = typeof(T).Name, Entity = entity });
            if (entity == null || _JsonHandler.ReadAsObject(FileSearchPatternConstants.Extension_Configurations) is not VaultXConfigurationsEntityModel existingEntity)
                return entity;

            var input = entity as VaultXConfigurationsEntityModel;

            if (existingEntity != null && input != null)
            {
                if(existingEntity.GeneralSettings != null && input.GeneralSettings != null)
                {
                    if (input.GeneralSettings.DefaultSortingOrder != null) existingEntity.GeneralSettings.DefaultSortingOrder = input.GeneralSettings.DefaultSortingOrder;
                    if (input.GeneralSettings.DefaultSortingField != null) existingEntity.GeneralSettings.DefaultSortingField = input.GeneralSettings.DefaultSortingField;
                    if (input.GeneralSettings.ShowArchivedCredentials != null) existingEntity.GeneralSettings.ShowArchivedCredentials = input.GeneralSettings.ShowArchivedCredentials;
                    if (input.GeneralSettings.ShowPasswordAsMasked != null) existingEntity.GeneralSettings.ShowPasswordAsMasked = input.GeneralSettings.ShowPasswordAsMasked;
                    if (input.GeneralSettings.MaskingPlaceHolder != null) existingEntity.GeneralSettings.MaskingPlaceHolder = input.GeneralSettings.MaskingPlaceHolder;
                    if (input.GeneralSettings.DefaultCredentialCategory != null) existingEntity.GeneralSettings.DefaultCredentialCategory = input.GeneralSettings.DefaultCredentialCategory;
                    if (input.GeneralSettings.AllowDuplicateTitles != null) existingEntity.GeneralSettings.AllowDuplicateTitles = input.GeneralSettings.AllowDuplicateTitles;
                }
                if(existingEntity.SecuritySettings != null && input.SecuritySettings != null)
                {
                    if (input.SecuritySettings.EnableCredentialExpirationCheck != null) existingEntity.SecuritySettings.EnableCredentialExpirationCheck = input.SecuritySettings.EnableCredentialExpirationCheck;
                    if (input.SecuritySettings.DefaultCredentialExpirationDays != null) existingEntity.SecuritySettings.DefaultCredentialExpirationDays = input.SecuritySettings.DefaultCredentialExpirationDays;
                    if (input.SecuritySettings.AutoArchiveExpiredCredentials != null) existingEntity.SecuritySettings.AutoArchiveExpiredCredentials = input.SecuritySettings.AutoArchiveExpiredCredentials;
                    if (input.SecuritySettings.EnablePasswordHistory != null) existingEntity.SecuritySettings.EnablePasswordHistory = input.SecuritySettings.EnablePasswordHistory;
                    if (input.SecuritySettings.EnableTwoFactorForDeletion != null) existingEntity.SecuritySettings.EnableTwoFactorForDeletion = input.SecuritySettings.EnableTwoFactorForDeletion;
                    if (input.SecuritySettings.AllowExport != null) existingEntity.SecuritySettings.AllowExport = input.SecuritySettings.AllowExport;
                    if (input.SecuritySettings.ShowPasswordStrengthMeter != null) existingEntity.SecuritySettings.ShowPasswordStrengthMeter = input.SecuritySettings.ShowPasswordStrengthMeter;
                }
                if((existingEntity.StoreProviders != null && existingEntity.StoreProviders.Count > 0) && (input.StoreProviders != null && input.StoreProviders.Count > 0))
                {
                    foreach (var item in input.StoreProviders)
                    {
                        var existingStoreProvider = existingEntity.StoreProviders.FirstOrDefault(x => x.StoreId == item.StoreId);
                        if (existingStoreProvider != null)
                        {
                            if (item.IsPrimary != null) existingStoreProvider.IsPrimary = item.IsPrimary;
                            if (item.MaxFileSizeBytes != null) existingStoreProvider.MaxFileSizeBytes = item.MaxFileSizeBytes;
                            if (item.BaseFileName != null) existingStoreProvider.BaseFileName = item.BaseFileName;
                            if (item.DataDirectory != null) existingStoreProvider.DataDirectory = item.DataDirectory;
                        }
                    }
                }
                if((existingEntity.EncryptionProviders != null && existingEntity.EncryptionProviders.Count > 0) && (input.EncryptionProviders != null && input.EncryptionProviders.Count > 0))
                {
                    foreach (var item in input.EncryptionProviders)
                    {
                        var existingEncryptionProvider = existingEntity.EncryptionProviders.FirstOrDefault(x => x.EncryptionId == item.EncryptionId);
                        if (existingEncryptionProvider != null)
                        {
                            if (item.EncryptionKey != null) existingEncryptionProvider.EncryptionKey = item.EncryptionKey;
                            if (item.IsPrimary != null) existingEncryptionProvider.IsPrimary = item.IsPrimary;
                            if (item.EncryptionName != null) existingEncryptionProvider.EncryptionName = item.EncryptionName;
                        }
                    }
                }
                if(existingEntity.BackupSettings != null && input.BackupSettings != null)
                {
                    if (existingEntity.BackupSettings.EnableCloudSync != null) existingEntity.BackupSettings.EnableCloudSync = input.BackupSettings.EnableCloudSync;
                }

                _JsonHandler.WriteAsObject(existingEntity as T, FileSearchPatternConstants.Extension_Configurations);
                _logger.LogDebug($"{nameof(VaultXConfigurationContext<T>)} => Entity updated successfully in JSON configuration context.", new { EntityType = typeof(T).Name, Entity = existingEntity });
            }

            return existingEntity as T;
        }
    }
}
