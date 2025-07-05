#region using directives
using DevNest.Common.Base.Contracts;
using DevNest.Infrastructure.Entity.Configurations.VaultX;
#endregion using directives

namespace DevNest.Business.Domain.RouterContracts.VaultX
{
    /// <summary>
    /// Represents the interface instance for VaultX configuration repository.
    /// </summary>
    public interface IVaultXConfigurationRespositoryRouter : IRepositoryRouter
    {
        /// <summary>
        /// Handler method to get the VaultX configurations.
        /// </summary>
        /// <returns></returns>
        Task<VaultXConfigurationsEntityModel?> GetConfigurationsAsync();

        /// <summary>
        /// Handler method to update the VaultX configurations.
        /// </summary>
        /// <param name="entityModel"></param>
        /// <returns></returns>
        Task<VaultXConfigurationsEntityModel?> UpdateConfigurationsAsync(VaultXConfigurationsEntityModel entityModel);
    }
}