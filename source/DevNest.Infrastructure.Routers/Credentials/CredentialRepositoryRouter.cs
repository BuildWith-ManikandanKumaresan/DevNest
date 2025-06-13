#region using directives
using DevNest.Business.Domain.RouterContracts;
using DevNest.Common.Logger;
using DevNest.Common.Base.Contracts;
using DevNest.Infrastructure.Entity.Configurations.CredentialManager;
using DevNest.Common.Base.Constants;
using Newtonsoft.Json;
using DevNest.Common.Base.Helpers;
using DevNest.Infrastructure.Entity.Credentials;
using System.Threading.Tasks;
using DevNest.Common.Manager.Plugin;
using DevNest.Plugin.Contracts.Storage;
#endregion using directives

namespace DevNest.Infrastructure.Routers.Credentials
{
    /// <summary>
    /// Represents the class instance for cred manager repository router.
    /// </summary>
    /// <remarks>
    /// Initialize the new instance for <see cref="CredentialRepositoryRouter" class./>
    /// </remarks>
    /// <param name="logger"></param>
    public class CredentialRepositoryRouter(
        IAppLogger<CredentialRepositoryRouter> logger,
        IPluginManager pluginManager,
        IAppConfigService<CredentialManagerConfigurations> configurations) : ICredentialRepositoryRouter
    {
        private readonly IAppLogger<CredentialRepositoryRouter> _logger = logger;
        private readonly IPluginManager _pluginManager = pluginManager;
        private readonly IAppConfigService<CredentialManagerConfigurations> _configurations = configurations;

        /// <summary>
        /// Handler method to add the credentials using entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<CredentialEntityModel> AddAsync(CredentialEntityModel entity)
        {
            Dictionary<string, object>? primaryConfig = _configurations.Value?.StorageProviders?.FirstOrDefault()?.ParseConnectionParams();

            var context = _pluginManager.GetStorageContext<CredentialEntityModel>(primaryConfig ?? []);

            if (entity.Security.IsEncrypted == true)
            {
                var connectionParam = GetEncryptionParams(entity.Security.EncryptionAlgorithm);
                var encryptionContext = _pluginManager.GetEncryptionContext<string>(connectionParam);

                if (encryptionContext != null)
                {
                    entity.Details.Password = encryptionContext.Encrypt(entity.Details.Password ?? string.Empty);
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
        public async Task<IEnumerable<CredentialEntityModel>?> GetAsync()
        {
            var context = GetStorageContext();
            var credentials = context?.Get().ToList();

            credentials?.ForEach(cred =>
            {
                DecryptCredential(cred);
            });

            return credentials;
        }

        /// <summary>
        /// Handler method for Get credential entity by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<CredentialEntityModel?> GetByIdAsync(Guid id)
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
        public async Task<CredentialEntityModel?> UpdateAsync(CredentialEntityModel entity)
        {
            var context = GetStorageContext();
            return context?.Update(entity);
        }

        #region Private methods

        /// <summary>
        /// Gets the storage context for credential entity operations.
        /// </summary>
        /// <returns></returns>
        private IStorageContext<CredentialEntityModel>? GetStorageContext()
        {
            Dictionary<string, object>? primaryConfig = _configurations.Value?.StorageProviders?.FirstOrDefault()?.ParseConnectionParams();
            return _pluginManager.GetStorageContext<CredentialEntityModel>(primaryConfig ?? []);
        }

        /// <summary>
        /// Gets the encryption parameters based on the specified algorithm name.
        /// </summary>
        /// <param name="algorithm"></param>
        /// <returns></returns>
        private Dictionary<string, object> GetEncryptionParams(string? algorithm)
        {
            var param = _configurations?.Value?.EncryptionProviders?.FirstOrDefault(
                a => a.EncryptionName == algorithm);

            Dictionary<string, object>? primaryConfig = param?.ParseConnectionParams();

            return primaryConfig != null && primaryConfig.Count > 0
                ? primaryConfig
                : _configurations?.Value?.EncryptionProviders?.FirstOrDefault()?.ParseConnectionParams() ?? [];
        }

        /// <summary>
        /// Decrypts the credential's password if it is encrypted.
        /// </summary>
        /// <param name="credential"></param>
        private async Task DecryptCredential(CredentialEntityModel? credential)
        {
            if (credential.Security?.IsEncrypted == true)
            {
                var connectionParam = GetEncryptionParams(credential.Security.EncryptionAlgorithm);
                var encryptionContext = _pluginManager.GetEncryptionContext<string>(connectionParam);

                if (encryptionContext != null)
                {
                    credential.Details.Password = encryptionContext.Decrypt(credential.Details.Password ?? string.Empty);
                }
            }
        }

        #endregion Private methods
    }
}