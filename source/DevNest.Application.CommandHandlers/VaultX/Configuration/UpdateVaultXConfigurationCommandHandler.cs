#region using directives
using DevNest.Application.CommandHandlers.VaultX.Store;
using DevNest.Application.Commands.VaultX.Configuration;
using DevNest.Application.Commands.VaultX.Store;
using DevNest.Business.Domain.Domains.VaultX.Contracts;
using DevNest.Common.Base.Contracts;
using DevNest.Common.Base.MediatR.Contracts;
using DevNest.Common.Base.Response;
using DevNest.Common.Logger;
using DevNest.Infrastructure.DTOs.Configurations.VaultX.Response;
using DevNest.Infrastructure.DTOs.VaultX.Response;
using DevNest.Infrastructure.Entity.Configurations.VaultX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion using directives

namespace DevNest.Application.CommandHandlers.VaultX.Configuration
{
    /// <summary>
    /// Represents the class instance for Update VaultX configuration command handler.
    /// </summary>
    public class UpdateVaultXConfigurationCommandHandler : ICommandHandler<UpdateVaultXConfigurationCommand, AppResponse<VaultXConfigurationsResponseDTO>>
    {
        private readonly IAppLogger<UpdateVaultXConfigurationCommandHandler> _logger;
        private readonly IVaultXConfigurationDomainService _domainService;

        /// <summary>
        /// Initialize the new instance for <see cref="UpdateVaultXConfigurationCommandHandler">class.</see>/>
        /// </summary>
        /// <param name="appLogger"></param>
        /// <param name="applicationConfigService"></param>
        /// <param name="domainService"></param>
        public UpdateVaultXConfigurationCommandHandler(
            IAppLogger<UpdateVaultXConfigurationCommandHandler> appLogger,
            IVaultXConfigurationDomainService domainService)
        {
            _logger = appLogger;
            _domainService = domainService;
        }

        /// <summary>
        /// Handler method to execute the <see cref="UpdateVaultXConfigurationCommand">class.</see>
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
#pragma warning disable CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
        public async Task<AppResponse<VaultXConfigurationsResponseDTO?>> Handle(UpdateVaultXConfigurationCommand command, CancellationToken cancellationToken = default)
#pragma warning restore CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
        {
            _logger.LogDebug($"{nameof(UpdateVaultXConfigurationCommandHandler)} => {nameof(Handle)} method called.");

            // Validate the query
            IList<AppErrors> errors = command.Validate();

            if (errors.Any())
            {
                _logger.LogError($"{nameof(UpdateVaultXConfigurationCommandHandler)} => Validation errors occurred: ", errors: errors);

                return new AppResponse<VaultXConfigurationsResponseDTO?>(errors.ToList());
            }

            _logger.LogDebug($"{nameof(UpdateVaultXConfigurationCommandHandler)} => {nameof(Handle)} method completed.", request: command);

            return await _domainService.UpdateConfigurations(command.UpdateVaultXConfigurationRequest);
        }
    }
}
