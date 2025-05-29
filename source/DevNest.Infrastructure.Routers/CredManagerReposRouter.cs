#region using directives
using DevNest.Business.Domain.RouterContracts;
using DevNest.Infrastructure.Entity;
using DevNest.Common.Logger;
#endregion using directives

namespace DevNest.Infrastructure.Routers
{
    /// <summary>
    /// Represents the class instance for cred manager repository router.
    /// </summary>
    public class CredManagerReposRouter : ICredManagerReposRouter
    {
        private readonly IAppLogger<CredManagerReposRouter> _logger;

        /// <summary>
        /// Initialize the new instance for <see cref="CredManagerReposRouter" class./>
        /// </summary>
        /// <param name="logger"></param>
        public CredManagerReposRouter(IAppLogger<CredManagerReposRouter> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Handler method for Get credentials entity.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CredentialEntity>?> GetAsync()
        {
            return [new CredentialEntity()];
        }
    }
}