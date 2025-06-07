#region using directives
using DevNest.Business.Domain.RouterContracts;
using DevNest.Infrastructure.Entity;
using DevNest.Common.Logger;
using DevNest.Manager.Plugin;
using DevNest.Common.Base.Contracts;
using DevNest.Infrastructure.Entity.Configurations.CredentialManager;
using DevNest.Common.Base.Constants;
using DevNest.Plugin.Contracts.Storage;
#endregion using directives

namespace DevNest.Infrastructure.Routers
{
    /// <summary>
    /// Represents the class instance for cred manager repository router.
    /// </summary>
    /// <remarks>
    /// Initialize the new instance for <see cref="CredentialManagerReposRouter" class./>
    /// </remarks>
    /// <param name="logger"></param>
    public class CredentialManagerReposRouter(
        IApplicationLogger<CredentialManagerReposRouter> logger,
        IPluginManager pluginManager,
        IApplicationConfigService<CredentialManagerConfigurations> configurations) : ICredentialManagerReposRouter
    {
        private readonly IApplicationLogger<CredentialManagerReposRouter> _logger = logger;
        private readonly IPluginManager _pluginManager = pluginManager;
        private readonly IApplicationConfigService<CredentialManagerConfigurations> _configurations = configurations;

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

            if (entity.IsEncrypted == true)
            {
                var connectionParam = GetEncryptionParams(entity.EncryptionAlgorithm);
                var encryptionContext = _pluginManager.GetEncryptionContext<string>(connectionParam);

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
            var context = GetStorageContext();
            return context?.Archive(id) ?? false;
        }

        /// <summary>
        /// Handler method for delete credentials entity.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> DeleteAsync()
        {
            var context = GetStorageContext();
            return context?.DeleteAll() ?? false;
        }

        /// <summary>
        /// Handler method for delete credential entity using id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var context = GetStorageContext();
            return context?.Delete(id) ?? false;
        }

        /// <summary>
        /// Handler method for Get credentials entity.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CredentialEntity>?> GetAsync()
        {
            var context = GetStorageContext();
            var credentials = context?.Get().ToList();

            credentials?.ForEach(DecryptCredential);

            return credentials;
        }

        /// <summary>
        /// Handler method for Get credential entity by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<CredentialEntity?> GetByIdAsync(Guid id)
        {
            var context = GetStorageContext();
            var credential = context?.GetById(id);
            DecryptCredential(credential);
            return credential;
        }

        /// <summary>
        /// Handler method for update the credentials entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<CredentialEntity?> UpdateAsync(CredentialEntity entity)
        {
            var context = GetStorageContext();
            return context?.Update(entity);
        }

        #region Private methods

        /// <summary>
        /// Gets the storage context for credential entity operations.
        /// </summary>
        /// <returns></returns>
        private IStorageContext<CredentialEntity>? GetStorageContext()
        {
            var primaryConfig = _configurations.Value?.StorageProvider?.FirstOrDefault();
            return _pluginManager.GetStorageContext<CredentialEntity>(primaryConfig ?? []);
        }

        /// <summary>
        /// Gets the encryption parameters based on the specified algorithm name.
        /// </summary>
        /// <param name="algorithm"></param>
        /// <returns></returns>
        private Dictionary<string, object> GetEncryptionParams(string? algorithm)
        {
            var param = _configurations?.Value?.EncryptionProvider?.FirstOrDefault(
                a => a.TryGetValue(ConnectionParamConstants.EncryptionName, out var value) &&
                     value?.ToString() == algorithm);

            return param != null && param.Count > 0
                ? param
                : _configurations?.Value?.EncryptionProvider?.FirstOrDefault() ?? [];
        }

        /// <summary>
        /// Decrypts the credential's password if it is encrypted.
        /// </summary>
        /// <param name="credential"></param>
        private void DecryptCredential(CredentialEntity? credential)
        {
            if (credential?.IsEncrypted == true)
            {
                var connectionParam = GetEncryptionParams(credential.EncryptionAlgorithm);
                var encryptionContext = _pluginManager.GetEncryptionContext<string>(connectionParam);

                if (encryptionContext != null)
                {
                    credential.Password = encryptionContext.Decrypt(credential.Password ?? string.Empty);
                }
            }
        }

        #endregion Private methods
    }
}