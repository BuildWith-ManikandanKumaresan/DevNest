#region using directives
using DevNest.Business.Domain.RouterContracts;
using DevNest.Infrastructure.Entity;
using DevNest.Common.Logger;
using DevNest.Manager.Plugin;
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

        /// <summary>
        /// Initialize the new instance for <see cref="CredentialManagerReposRouter" class./>
        /// </summary>
        /// <param name="logger"></param>
        public CredentialManagerReposRouter(
            IApplicationLogger<CredentialManagerReposRouter> logger,
            IPluginManager pluginManager)
        {
            _logger = logger;
            _pluginManager = pluginManager;
        }

        /// <summary>
        /// Handler method for Get credentials entity.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CredentialEntity>?> GetAsync()
        {
            var context = _pluginManager.GetContext<CredentialEntity>(new Dictionary<string, object>());
            return context.Get();
        }
    }
}