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
using DevNest.Business.Domain.RouterContracts.VaultX;
#endregion using directives

namespace DevNest.Infrastructure.Routers.VaultX
{
    /// <summary>
    /// Represents the class instance for cred manager repository router.
    /// </summary>
    /// <remarks>
    /// Initialize the new instance for <see cref="VaultXStoreRepositoryRouter" class./>
    /// </remarks>
    /// <param name="logger"></param>
    public class VaultXResourceRepositoryRouter(
        IAppLogger<VaultXStoreRepositoryRouter> logger,
        IPluginManager pluginManager,
        IAppConfigService<VaultXConfigurationsEntityModel> configurations,
        IFileSystemManager fileSystemManager) : IVaultXResourceRepositoryRouter
    {
        private readonly IAppLogger<VaultXStoreRepositoryRouter> _logger = logger;
        private readonly IPluginManager _pluginManager = pluginManager;
        private readonly IAppConfigService<VaultXConfigurationsEntityModel> _configurations = configurations;
        private readonly IFileSystemManager _fileSystemManager = fileSystemManager;

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

        #endregion Private methods
    }
}