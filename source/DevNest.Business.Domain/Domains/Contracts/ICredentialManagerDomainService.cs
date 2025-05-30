#region using directives
using DevNest.Common.Base.Contracts;
using DevNest.Common.Base.Response;
using DevNest.Infrastructure.DTOs;
#endregion using directives

namespace DevNest.Business.Domain.Domains.Contracts
{
    /// <summary>
    /// Represents the interface instance for credential manager services.
    /// </summary>
    public interface ICredentialManagerDomainService : IDomainService
    {
        /// <summary>
        /// Handler method interface for get credentials.
        /// </summary>
        /// <returns></returns>
        Task<ApplicationResponse<IEnumerable<CredentialsDTO>>> Get();
    }
}