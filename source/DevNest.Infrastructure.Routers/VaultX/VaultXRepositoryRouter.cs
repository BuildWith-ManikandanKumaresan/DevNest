#region using directives
using DevNest.Business.Domain.RouterContracts;
using DevNest.Common.Logger;
using DevNest.Common.Base.Contracts;
using DevNest.Common.Base.Constants;
using Newtonsoft.Json;
using DevNest.Common.Base.Helpers;
using System.Threading.Tasks;
using DevNest.Common.Manager.Plugin;
using DevNest.Common.Manager.FileSystem;
using DevNest.Infrastructure.Entity.Configurations.VaultX;
using DevNest.Infrastructure.Entity.VaultX;
using DevNest.Plugin.Contracts.Context;
#endregion using directives

namespace DevNest.Infrastructure.Routers.VaultX
{
    /// <summary>
    /// Represents the class instance for cred manager repository router.
    /// </summary>
    /// <remarks>
    /// Initialize the new instance for <see cref="VaultXRepositoryRouter" class./>
    /// </remarks>
    /// <param name="logger"></param>
    public class VaultXRepositoryRouter(
        IAppLogger<VaultXRepositoryRouter> logger,
        IPluginManager pluginManager,
        IAppConfigService<VaultXConfigurationsEntityModel> configurations,
        IFileSystemManager fileSystemManager) : IVaultXRepositoryRouter
    {
        private readonly IAppLogger<VaultXRepositoryRouter> _logger = logger;
        private readonly IPluginManager _pluginManager = pluginManager;
        private readonly IAppConfigService<VaultXConfigurationsEntityModel> _configurations = configurations;
        private readonly IFileSystemManager _fileSystemManager = fileSystemManager;

        /// <summary>
        /// Handler method to add the credentials using entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<CredentialEntityModel?> AddAsync(CredentialEntityModel entity, string workspace)
        {
            var context = GetCredentialContext(workspace);

            var encryptedEntity = EncryptCredential(entity).GetAwaiter().GetResult();

            return await Task.FromResult(context?.Add(encryptedEntity) ?? null);
        }

        /// <summary>
        /// Handler method for archiving the credential entity by its unique identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> ArchiveByIdAsync(Guid id, string workspace)
        {
            var context = GetCredentialContext(workspace);
            return await Task.FromResult(context?.Archive(id) ?? false);
        }

        /// <summary>
        /// Handler method for delete credentials entity.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(string workspace)
        {
            var context = GetCredentialContext(workspace);
            return await Task.FromResult(context?.DeleteAll() ?? false);
        }

        /// <summary>
        /// Handler method for delete credential entity using id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteByIdAsync(Guid id, string workspace)
        {
            var context = GetCredentialContext(workspace);
            return await Task.FromResult(context?.Delete(id) ?? false);
        }

        /// <summary>
        /// Handler method for Get credentials entity.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<CredentialEntityModel>?> GetAsync(string workspace)
        {
            var context = GetCredentialContext(workspace);
            var credentials = context?.Get()?.ToList();
            var decryptedCredentials = new List<CredentialEntityModel>();
            credentials?.ForEach(cred =>
            {
                var listItem = DecryptCredential(cred).GetAwaiter().GetResult();
                if (listItem != null)
                    decryptedCredentials.Add(listItem);
            });

            return await Task.FromResult(decryptedCredentials);
        }

        /// <summary>
        /// Handler method for Get credential entity by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<CredentialEntityModel?> GetByIdAsync(Guid id, string workspace)
        {
            var context = GetCredentialContext(workspace);
            var credential = context?.GetById(id);
            return await DecryptCredential(credential);
        }

        /// <summary>
        /// Handler method for update the credentials entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<CredentialEntityModel?> UpdateAsync(CredentialEntityModel entity, string workspace)
        {
            var context = GetCredentialContext(workspace);
            return await Task.FromResult(context?.Update(entity));
        }

        /// <summary>
        /// Handler method to encrypt the credential entity by its unique identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CredentialEntityModel?> EncryptByIdAsync(Guid id, string workspace)
        {
            var context = GetCredentialContext(workspace);
            var credential = context?.GetById(id);
            if (credential != null && credential.Security != null)
                credential.Security.IsEncrypted = true;
            var encryptedCredential = EncryptCredential(credential).GetAwaiter().GetResult();
            return await Task.FromResult(context?.Update(encryptedCredential));
        }

        /// <summary>
        /// Handler method to decrypt the credential entity by its unique identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CredentialEntityModel?> DecryptByIdAsync(Guid id, string workspace)
        {
            var context = GetCredentialContext(workspace);
            var credential = context?.GetById(id);
            if (credential != null && credential.Security != null)
                credential.Security.IsEncrypted = true;
            return await DecryptCredential(credential);
        }

        /// <summary>
        /// Handler method to get the categories of credentials in the specified workspace.
        /// </summary>
        /// <param name="workspace"></param>
        /// <returns></returns>
        public async Task<IList<CategoryEntityModel>?> GetCategoriesAsync(string workspace)
        {
            var context = GetCategoryContext();
            var categories = context?.Get()?.ToList();
            return await Task.FromResult(categories);
        }

        /// <summary>
        /// Handler method to get the types of categories for a specific category ID in the specified workspace.
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="workspace"></param>
        /// <returns></returns>
        public async Task<IList<TypesEntityModel>?> GetTypesAsync(Guid categoryId, string workspace)
        {
            var context = GetCategoryContext();
            var types = context?.GetById(categoryId.ToString());
            return await Task.FromResult(types?.Types);
        }

        #region Private methods

        /// <summary>
        /// Gets the store context for credential entity operations.
        /// </summary>
        /// <returns></returns>
        private IStoreContext<CredentialEntityModel>? GetCredentialContext(string workspace)
        {
            StoreProviderEntityModel? provider = _configurations.Value?.StoreProviders?.FirstOrDefault();
            IFileSystem? credStoreDir = _fileSystemManager.Vault?.GetSubDirectory(FileSystemConstants.VaultXDirectory);
            if (provider != null)
                provider.DataDirectory = credStoreDir?.GetWorkspace(workspace)?.Root ?? string.Empty;
            Dictionary<string, object>? primaryConfig = provider?.ParseConnectionParams();
            return _pluginManager.GetStoreContext<CredentialEntityModel>(primaryConfig ?? []);
        }

        /// <summary>
        /// Gets the category context for credential categories in the specified workspace.
        /// </summary>
        /// <param name="workspace"></param>
        /// <returns></returns>
        public IResourceContext<CategoryEntityModel>? GetCategoryContext()
        {
            StoreProviderEntityModel? provider = _configurations.Value?.StoreProviders?.FirstOrDefault();
            IFileSystem? credStoreDir = _fileSystemManager.Resources?.GetSubDirectory(FileSystemConstants.VaultXDirectory);
            if (provider != null)
                provider.DataDirectory = credStoreDir?.Root;
            Dictionary<string, object>? primaryConfig = provider?.ParseConnectionParams();
            return _pluginManager.GetResourceContext<CategoryEntityModel>(primaryConfig ?? []);

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
        private async Task<CredentialEntityModel?> DecryptCredential(CredentialEntityModel? credential)
        {
            if (credential?.Security?.IsEncrypted == true)
            {
                var connectionParam = GetEncryptionParams(credential.Security.EncryptionAlgorithm);
                var encryptionContext = _pluginManager.GetEnxryptionContext<string>(connectionParam);

                if (encryptionContext != null)
                {
                    if (credential != null && credential.Details != null && credential.Details.Password != null)
                        credential.Details.Password = encryptionContext.Decrypt(credential.Details.Password ?? string.Empty);
                }
            }
            return await Task.FromResult(credential);
        }

        /// <summary>
        /// Encrypts the credential's password by its unique identifier if it is encrypted.
        /// </summary>
        /// <param name="credential"></param>
        /// <returns></returns>
        private async Task<CredentialEntityModel?> EncryptCredential(CredentialEntityModel? credential)
        {
            if (credential != null && credential.Security?.IsEncrypted == true)
            {
                var connectionParam = GetEncryptionParams(credential.Security.EncryptionAlgorithm);
                var encryptionContext = _pluginManager.GetEnxryptionContext<string>(connectionParam);
                if (encryptionContext != null)
                {
                    if (credential != null && credential.Details != null && credential.Details.Password != null)
                        credential.Details.Password = encryptionContext.Encrypt(credential.Details.Password ?? string.Empty);
                }
            }
            return await Task.FromResult(credential);
        }

        #endregion Private methods
    }
}