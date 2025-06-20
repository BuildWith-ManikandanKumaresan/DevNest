#region using directives
using DevNest.Common.Base.Contracts;
using DevNest.Common.Base.Response;
using DevNest.Infrastructure.DTOs.Credential.Request;
using DevNest.Infrastructure.DTOs.Credential.Response;
using DevNest.Infrastructure.DTOs.CredentialManager.Response;
using DevNest.Infrastructure.Entity.Search;
using System.ComponentModel.DataAnnotations;
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
        Task<AppResponse<IList<CredentialResponseDTO>>> Get(
            string? environment,
            string? category,
            string? type,
            string? domain,
            string? passwordStrength,
            bool? isEncrypted,
            bool? isValid,
            bool? isDisabled,
            bool? isExpired,
            IList<string>? groups,
            string workspace,
            SearchEntityModel? searchFilter);

        /// <summary>
        /// Handler method interface for get credentials by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AppResponse<CredentialResponseDTO>> GetById(
            Guid id,
            string workspace);

        /// <summary>
        /// Handler method interface for delete credentials.
        /// </summary>
        /// <returns></returns>
        Task<AppResponse<bool>> Delete(string workspace);

        /// <summary>
        /// Handler method interface for delete credentials by credential id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AppResponse<bool>> DeleteById(Guid id, string workspace);

        /// <summary>
        /// Handler method interface for add credentials.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<AppResponse<CredentialResponseDTO>> Add(AddCredentialRequest? request, string workspace);

        /// <summary>
        /// Handler method interface for update credentials.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<AppResponse<CredentialResponseDTO>> Update(UpdateCredentialRequest? request, string workspace);

        /// <summary>
        /// Handler method interface for archiving a credential by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AppResponse<bool>> Archive(Guid id, string workspace);

        /// <summary>
        /// Handler method interface for encrypting a credential by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AppResponse<CredentialResponseDTO>> Encrypt(Guid id, string workspace);

        /// <summary>
        /// Handler method interface for decrypting a credential by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AppResponse<CredentialResponseDTO>> Decrypt(Guid id, string workspace);

        /// <summary>
        /// Handler method interface for getting credential category types without category ID.
        /// </summary>
        /// <param name="workspace"></param>
        /// <returns></returns>
        Task<AppResponse<IList<CategoryResponseDTO>?>> GetCategories(string workspace);

        /// <summary>
        /// Handler method interface for getting credential category types.
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="workspace"></param>
        /// <returns></returns>
        Task<AppResponse<IList<TypesResponseDTO>?>> GetTypes(Guid categoryId, string workspace);
    }
}