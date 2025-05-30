#region using directives
using DevNest.Infrastructure.Entity;
using DevNest.Common.Base.Contracts;
#endregion using directives

namespace DevNest.Business.Domain.RouterContracts
{
    /// <summary>
    /// Represents the interface instance for Credential manager repository.
    /// </summary>
    public interface ICredentialManagerReposRouter : IReposRouter
    {
        /// <summary>
        /// Handler method to get the credentials entity from the plugin repository.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<CredentialEntity>?> GetAsync();
    }
}