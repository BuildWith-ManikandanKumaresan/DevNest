#region using directives
using DevNest.Common.Base.Contracts;
using DevNest.Common.Base.Response;
using DevNest.Infrastructure.DTOs.VaultX.Request;
using DevNest.Infrastructure.DTOs.VaultX.Response;
using DevNest.Infrastructure.DTOs.VaultX.Request;
using DevNest.Infrastructure.Entity.Search;
using System.ComponentModel.DataAnnotations;
using DevNest.Infrastructure.DTOs.Configurations.VaultX.Response;
using DevNest.Infrastructure.DTOs.Configurations.VaultX.Request;
#endregion using directives

namespace DevNest.Business.Domain.Domains.VaultX.Contracts
{
    /// <summary>
    /// Represents the interface instance for credential manager services.
    /// </summary>
    public interface IVaultXConfigurationDomainService : IDomainService
    {
        /// <summary>
        /// Handler method interface for getting VaultX configurations.
        /// </summary>
        /// <returns></returns>
        Task<AppResponse<VaultXConfigurationsResponseDTO?>> GetConfigurations();

        /// <summary>
        /// Handler method interface for updating VaultX configurations.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<AppResponse<VaultXConfigurationsResponseDTO?>> UpdateConfigurations(UpdateVaultXConfigurationsRequestDTO? request);
    }
}