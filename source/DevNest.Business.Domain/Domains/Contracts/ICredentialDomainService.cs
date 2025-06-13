#region using directives
using DevNest.Common.Base.Contracts;
using DevNest.Common.Base.Response;
using DevNest.Infrastructure.DTOs.CredentialManager.Request;
using DevNest.Infrastructure.DTOs.CredentialManager.Response;
#endregion using directives

namespace DevNest.Business.Domain.Domains.Contracts
{
    /// <summary>
    /// Represents the interface instance for credential manager services.
    /// </summary>
    public interface ICredentialDomainService : IDomainService
    {
        /// <summary>
        /// Handler method interface for get credentials.
        /// </summary>
        /// <returns></returns>
        Task<AppResponse<IEnumerable<CredentialResponseDTO>>> Get();

        /// <summary>
        /// Handler method interface for get credentials by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AppResponse<CredentialResponseDTO>> GetById(Guid id);

        /// <summary>
        /// Handler method interface for delete credentials.
        /// </summary>
        /// <returns></returns>
        Task<AppResponse<bool>> Delete();

        /// <summary>
        /// Handler method interface for delete credentials by credential id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AppResponse<bool>> DeleteById(Guid id);

        /// <summary>
        /// Handler method interface for add credentials.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<AppResponse<CredentialResponseDTO>> Add(AddCredentialRequest? request);

        /// <summary>
        /// Handler method interface for update credentials.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<AppResponse<CredentialResponseDTO>> Update(UpdateCredentialRequest? request);

        /// <summary>
        /// Handler method interface for archiving a credential by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AppResponse<bool>> Archive(Guid id);
    }
}