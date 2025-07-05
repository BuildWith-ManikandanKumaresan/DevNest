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
    public class VaultXConfigurationRepositoryRouter(
        IAppLogger<VaultXConfigurationRepositoryRouter> logger,
        IPluginManager pluginManager,
        IAppConfigService<VaultXConfigurationsEntityModel> configurations,
        IFileSystemManager fileSystemManager) : IVaultXConfigurationRespositoryRouter
    {
        private readonly IAppLogger<VaultXConfigurationRepositoryRouter> _logger = logger;
        private readonly IPluginManager _pluginManager = pluginManager;
        private readonly IAppConfigService<VaultXConfigurationsEntityModel> _configurations = configurations;
        private readonly IFileSystemManager _fileSystemManager = fileSystemManager;

        /// <summary>
        /// Handler method to get the VaultX configurations.
        /// </summary>
        /// <returns></returns>
        public async Task<VaultXConfigurationsEntityModel?> GetConfigurationsAsync()
        {
            var context = GetConfigurationContext();
            var configurations = context?.Get();
            return await Task.FromResult(configurations);
        }

        /// <summary>
        /// Handler method to update the VaultX configurations.
        /// </summary>
        /// <param name="entityModel"></param>
        /// <returns></returns>
        public async Task<VaultXConfigurationsEntityModel?> UpdateConfigurationsAsync(VaultXConfigurationsEntityModel entityModel)
        {
            var context = GetConfigurationContext();
            return await Task.FromResult(context?.Update(entityModel));
        }

        #region Private methods

        /// <summary>
        /// Gets the category context for credential categories in the specified workspace.
        /// </summary>
        /// <param name="workspace"></param>
        /// <returns></returns>
        public IConfigurationContext<VaultXConfigurationsEntityModel>? GetConfigurationContext()
        {
            StoreProviderEntityModel? provider = _configurations.Value?.StoreProviders?.FirstOrDefault();
            IFileSystem? configDir = _fileSystemManager.Configurations?.GetSubDirectory(FileSystemConstants.VaultXDirectory);
            if (provider != null)
                provider.DataDirectory = configDir?.Root;
            Dictionary<string, object>? primaryConfig = provider.ParseConnectionParams();
            return _pluginManager.GetConfigurationContext<VaultXConfigurationsEntityModel>(primaryConfig ?? []);

        }

        #endregion Private methods
    }
}