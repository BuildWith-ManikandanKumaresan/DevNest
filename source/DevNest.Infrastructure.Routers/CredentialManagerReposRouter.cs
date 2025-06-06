#region using directives
using DevNest.Business.Domain.RouterContracts;
using DevNest.Infrastructure.Entity;
using DevNest.Common.Logger;
using DevNest.Manager.Plugin;
using DevNest.Common.Base.Contracts;
using DevNest.Infrastructure.Entity.Configurations.CredentialManager;
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
            var primaryConfig = _configurations.Value?.Storage.FirstOrDefault();
            var context = _pluginManager.GetContext<CredentialEntity>(primaryConfig ?? []);
            return context?.Add(entity) ?? null;
        }

        /// <summary>
        /// Handler method for delete credentials entity.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> DeleteAsync()
        {
            var primaryConfig = _configurations.Value?.Storage.FirstOrDefault();
            var context = _pluginManager.GetContext<CredentialEntity>(primaryConfig ?? []);
            return context?.DeleteAll() ?? false;
        }

        /// <summary>
        /// Handler method for delete credential entity using id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var primaryConfig = _configurations.Value?.Storage.FirstOrDefault();
            var context = _pluginManager.GetContext<CredentialEntity>(primaryConfig ?? []);
            return context?.Delete(id) ?? false;
        }

        /// <summary>
        /// Handler method for Get credentials entity.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CredentialEntity>?> GetAsync()
        {
            var primaryConfig = _configurations.Value?.Storage.FirstOrDefault();
            var context = _pluginManager.GetContext<CredentialEntity>(primaryConfig ?? []);
            return context?.Get();
        }

        /// <summary>
        /// Handler method for Get credential entity by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<CredentialEntity?> GetByIdAsync(Guid id)
        {
            var primaryConfig = _configurations.Value?.Storage.FirstOrDefault();
            var context = _pluginManager.GetContext<CredentialEntity>(primaryConfig ?? []);
            return context?.GetById(id);
        }
    }
}