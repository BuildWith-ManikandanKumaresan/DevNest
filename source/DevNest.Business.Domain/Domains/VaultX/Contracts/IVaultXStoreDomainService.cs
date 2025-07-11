﻿#region using directives
using DevNest.Common.Base.Contracts;
using DevNest.Common.Base.Response;
using DevNest.Infrastructure.DTOs.VaultX.Request;
using DevNest.Infrastructure.DTOs.VaultX.Response;
using DevNest.Infrastructure.DTOs.VaultX.Request;
using DevNest.Infrastructure.Entity.Search;
using System.ComponentModel.DataAnnotations;
#endregion using directives

namespace DevNest.Business.Domain.Domains.VaultX.Contracts
{
    /// <summary>
    /// Represents the interface instance for credential manager services.
    /// </summary>
    public interface IVaultXStoreDomainService : IDomainService
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
    }
}