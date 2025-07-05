#region using directives
using AutoMapper;
using DevNest.Business.Domain.Domains.VaultX.Contracts;
using DevNest.Business.Domain.RouterContracts;
using DevNest.Business.Domain.RouterContracts.VaultX;
using DevNest.Common.Base.Constants.Message;
using DevNest.Common.Base.Response;
using DevNest.Common.Logger;
using DevNest.Infrastructure.DTOs.Configurations.VaultX.Request;
using DevNest.Infrastructure.DTOs.Configurations.VaultX.Response;
using DevNest.Infrastructure.DTOs.VaultX.Response;
using DevNest.Infrastructure.Entity.Configurations.VaultX;
using DevNest.Infrastructure.Entity.VaultX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion using directives

namespace DevNest.Business.Domain.Domains.VaultX
{
    /// <summary>
    /// Represents the class instance for VaultX configuration services.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="router"></param>
    /// <param name="mapper"></param>
    public class VaultXConfigurationDomainService(
        IAppLogger<VaultXConfigurationDomainService> logger,
        IVaultXConfigurationRespositoryRouter router,
        IMapper mapper) : IVaultXConfigurationDomainService
    {
        private readonly IAppLogger<VaultXConfigurationDomainService> _logger = logger;
        private readonly IVaultXConfigurationRespositoryRouter _router = router;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Method to get VaultX configurations.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<AppResponse<VaultXConfigurationsResponseDTO?>> GetConfigurations()
        {
            try
            {
                _logger.LogDebug($"{nameof(VaultXConfigurationDomainService)} => {nameof(GetConfigurations)} method called.");
                var entity = await _router.GetConfigurationsAsync();
                if (entity == null)
                {
                    _logger.LogDebug($"{nameof(VaultXConfigurationDomainService)} => {nameof(GetConfigurations)} method returned null.");
                    return new AppResponse<VaultXConfigurationsResponseDTO?>(Messages.GetError(ErrorConstants.VaultXConfigurationsNotFound));
                }

                _logger.LogDebug($"{nameof(VaultXConfigurationDomainService)} => {nameof(GetConfigurations)} method returned configurations.");

                return new AppResponse<VaultXConfigurationsResponseDTO?>(_mapper.Map<VaultXConfigurationsResponseDTO>(entity))
                {
                    Message = Messages.GetSuccess(SuccessConstants.VaultXConfigurationsRetrievedSuccessfully)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(VaultXConfigurationDomainService)} => {ex.Message}", ex);
                return new AppResponse<VaultXConfigurationsResponseDTO?>(new AppErrors { Code = ErrorConstants.UndefinedErrorCode, Message = ex.Message });
            }
        }

        /// <summary>
        /// Method to update VaultX configurations.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<AppResponse<VaultXConfigurationsResponseDTO?>> UpdateConfigurations(UpdateVaultXConfigurationsRequestDTO request)
        {
            try
            {
                _logger.LogDebug($"{nameof(VaultXConfigurationDomainService)} => {nameof(request)} method called with request: {request}.");
                var entity = _mapper.Map<VaultXConfigurationsEntityModel>(request);

                var result = await _router.UpdateConfigurationsAsync(entity);

                if (result == null)
                {
                    _logger.LogDebug($"{nameof(VaultXConfigurationDomainService)} => {nameof(UpdateConfigurations)} method failed to update configurations.");
                    return new AppResponse<VaultXConfigurationsResponseDTO?>(Messages.GetError(ErrorConstants.VaultXConfigurationsUpdateFailed));
                }

                _logger.LogDebug($"{nameof(VaultXConfigurationDomainService)} => {nameof(UpdateConfigurations)} method successfully updated configurations.");
                return new AppResponse<VaultXConfigurationsResponseDTO?>(
                    data: _mapper.Map<VaultXConfigurationsResponseDTO>(result),
                    message: Messages.GetSuccess(SuccessConstants.VaultXConfigurationsUpdatedSuccessfully));
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(VaultXConfigurationDomainService)} => {ex.Message}", ex);
                return new AppResponse<VaultXConfigurationsResponseDTO?>(new AppErrors
                {
                    Code = ErrorConstants.UndefinedErrorCode,
                    Message = ex.Message
                });
            }
        }
    }
}
