#region using directives
using DevNest.Application.Commands.VaultX;
using DevNest.Business.Domain.Domains.Contracts;
using DevNest.Common.Base.Contracts;
using DevNest.Common.Base.MediatR.Contracts;
using DevNest.Common.Base.Response;
using DevNest.Common.Logger;
using DevNest.Infrastructure.DTOs.VaultX.Response;
using DevNest.Infrastructure.Entity;
using DevNest.Infrastructure.Entity.Configurations.VaultX;
using MediatR;
#endregion using directives

namespace DevNest.Application.CommandHandlers.VaultX
{
    /// <summary>
    /// Represents the class instance for add credentials command handler.
    /// </summary>
    /// <remarks>
    /// Initialize the new instance for <see cref="AddCredentialCommandHandler">class.</see>/>
    /// </remarks>
    /// <param name="appLogger"></param>
    /// <param name="applicationConfigService"></param>
    /// <param name="domainService"></param>
    public class AddCredentialCommandHandler(
        IAppLogger<AddCredentialCommandHandler> appLogger,
        IAppConfigService<VaultXConfigurationsEntityModel> applicationConfigService,
        IVaultXDomainService domainService) : ICommandHandler<AddCredentialCommand, AppResponse<CredentialResponseDTO>>
    {
        private readonly IAppLogger<AddCredentialCommandHandler> _logger = appLogger;
        private readonly IVaultXDomainService _domainService = domainService;
        private readonly IAppConfigService<VaultXConfigurationsEntityModel> _applicationConfigService = applicationConfigService;

        /// <summary>
        /// Handler method to execute the <see cref="AddCredentialCommandHandler">class.</see>/>
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<AppResponse<CredentialResponseDTO>> Handle(AddCredentialCommand command, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"{nameof(AddCredentialCommandHandler)} => {nameof(Handle)} method called.");

            // Validate the query
            IList<AppErrors> errors = command.Validate();

            if (errors.Any())
            {
                _logger.LogError($"{nameof(AddCredentialCommandHandler)} => Validation errors occurred: ", errors: errors);

                return new AppResponse<CredentialResponseDTO>(errors.ToList());
            }

            _logger.LogDebug($"{nameof(AddCredentialCommandHandler)} => {nameof(Handle)} method completed.", request: command);

            return await _domainService.Add(request: command.AddCredentialRequest, command.Workspace);
        }
    }
}
