#region using directives
using DevNest.Business.Domain.RouterContracts;
using DevNest.Infrastructure.Entity;
using DevNest.Common.Logger;
using DevNest.Manager.Plugin;
using DevNest.Common.Base.Contracts;
using DevNest.Infrastructure.Entity.Configurations.CredentialManager;
using DevNest.Common.Base.Constants;
#endregion using directives

namespace DevNest.Infrastructure.Routers
{
    /// <summary>
    /// Represents the class instance for cred manager repository router.
    /// </summary>
    public class CredentialManagerReposRouter : ICredentialManagerReposRouter
    {
        private readonly IApplicationLogger<CredentialManagerReposRouter> _logger;
        private readonly IPluginManager _pluginManager;
        private readonly IApplicationConfigService<CredentialManagerConfigurations> _configurations;

        /// <summary>
        /// Initialize the new instance for <see cref="CredentialManagerReposRouter" class./>
        /// </summary>
        /// <param name="logger"></param>
        public CredentialManagerReposRouter(
            IApplicationLogger<CredentialManagerReposRouter> logger,
            IPluginManager pluginManager,
            IApplicationConfigService<CredentialManagerConfigurations> configurations)
        {
            _logger = logger;
            _pluginManager = pluginManager;
            _configurations = configurations;
        }

        /// <summary>
        /// Handler method to add the credentials using entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<CredentialEntity> AddAsync(CredentialEntity entity)
        {
            var primaryConfig = _configurations.Value?.StorageProvider?.FirstOrDefault();
            var context = _pluginManager.GetStorageContext<CredentialEntity>(primaryConfig ?? []);
            if(entity.IsEncrypted ?? false)
            {
                var connectionParam = _configurations?.Value?.EncryptionProvider.FirstOrDefault(a => a.TryGetValue(ConnectionParamConstants.EncryptionName, out object encryptionalgorithm) && encryptionalgorithm is not null && encryptionalgorithm.ToString() == entity.EncryptionAlgorithm) ?? [];
                if(connectionParam == null || connectionParam.Count <= 0)
                {
                    connectionParam = _configurations.Value?.EncryptionProvider?.FirstOrDefault() ?? [];
                }
                var encryptionContext = _pluginManager.GetEncryptionDataContext<string>(connectionParam);
                if (encryptionContext != null)
                {
                    entity.Password = encryptionContext.Encrypt(entity.Password ?? string.Empty);
                }
            }
            return context?.Add(entity) ?? null;
        }

        /// <summary>
        /// Handler method for archiving the credential entity by its unique identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> ArchiveByIdAsync(Guid id)
        {
            var primaryConfig = _configurations.Value?.StorageProvider.FirstOrDefault();
            var context = _pluginManager.GetStorageContext<CredentialEntity>(primaryConfig ?? []);
            return context?.Archive(id) ?? false;
        }

        /// <summary>
        /// Handler method for delete credentials entity.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> DeleteAsync()
        {
            var primaryConfig = _configurations.Value?.StorageProvider.FirstOrDefault();
            var context = _pluginManager.GetStorageContext<CredentialEntity>(primaryConfig ?? []);
            return context?.DeleteAll() ?? false;
        }

        /// <summary>
        /// Handler method for delete credential entity using id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var primaryConfig = _configurations.Value?.StorageProvider.FirstOrDefault();
            var context = _pluginManager.GetStorageContext<CredentialEntity>(primaryConfig ?? []);
            return context?.Delete(id) ?? false;
        }

        /// <summary>
        /// Handler method for Get credentials entity.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CredentialEntity>?> GetAsync()
        {
            var primaryConfig = _configurations.Value?.StorageProvider.FirstOrDefault();
            var context = _pluginManager.GetStorageContext<CredentialEntity>(primaryConfig ?? []);
            var getCredentials = context?.Get().ToList();
            getCredentials?.ForEach(credential =>
            {
                if(credential.IsEncrypted ?? false)
                {
                    var connectionParam = _configurations?.Value?.EncryptionProvider.FirstOrDefault(a => a.TryGetValue(ConnectionParamConstants.EncryptionName, out object encryptionalgorithm) && encryptionalgorithm is not null && encryptionalgorithm.ToString() == credential.EncryptionAlgorithm) ?? [];
                    if (connectionParam == null || connectionParam.Count <= 0)
                    {
                        connectionParam = _configurations.Value?.EncryptionProvider?.FirstOrDefault() ?? [];
                    }
                    var encryptionContext = _pluginManager.GetEncryptionDataContext<string>(connectionParam);
                    if (encryptionContext != null)
                    {
                        credential.Password = encryptionContext.Decrypt(credential.Password ?? string.Empty);
                    }
                }
            });

            return getCredentials;
        }

        /// <summary>
        /// Handler method for Get credential entity by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<CredentialEntity?> GetByIdAsync(Guid id)
        {
            var primaryConfig = _configurations.Value?.StorageProvider?.FirstOrDefault();
            var context = _pluginManager.GetStorageContext<CredentialEntity>(primaryConfig ?? []);
            var credential =  context?.GetById(id);
            if(credential?.IsEncrypted ?? false)
            {
                var connectionParam = _configurations?.Value?.EncryptionProvider.FirstOrDefault(a => a.TryGetValue(ConnectionParamConstants.EncryptionName, out object encryptionalgorithm) && encryptionalgorithm is not null && encryptionalgorithm.ToString() == credential.EncryptionAlgorithm) ?? [];
                if (connectionParam == null || connectionParam.Count <= 0)
                {
                    connectionParam = _configurations.Value?.EncryptionProvider?.FirstOrDefault() ?? [];
                }
                var encryptionContext = _pluginManager.GetEncryptionDataContext<string>(connectionParam);
                if (encryptionContext != null)
                {
                    credential.Password = encryptionContext.Decrypt(credential.Password ?? string.Empty);
                }
            }
            return credential;
        }

        /// <summary>
        /// Handler method for update the credentials entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<CredentialEntity?> UpdateAsync(CredentialEntity entity)
        {
            var primaryConfig = _configurations.Value?.StorageProvider.FirstOrDefault();
            var context = _pluginManager.GetStorageContext<CredentialEntity>(primaryConfig ?? []);
            return context?.Update(entity);
        }
    }
}