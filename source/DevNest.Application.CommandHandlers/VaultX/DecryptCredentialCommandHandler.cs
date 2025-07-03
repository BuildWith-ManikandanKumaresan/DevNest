#region using directives
using DevNest.Application.Commands.VaultX;
using DevNest.Business.Domain.Domains.Contracts;
using DevNest.Common.Base.Contracts;
using DevNest.Common.Base.MediatR.Contracts;
using DevNest.Common.Base.Response;
using DevNest.Common.Logger;
using DevNest.Infrastructure.DTOs.VaultX.Response;
using DevNest.Infrastructure.Entity.Configurations.VaultX;
using MediatR;
#endregion using directives

namespace DevNest.Application.CommandHandlers.VaultX
{
    /// <summary>
    /// Handles the command to decrypt a credential.
    /// </summary>
    public class DecryptCredentialCommandHandler : ICommandHandler<DecryptCredentialCommand, AppResponse<CredentialResponseDTO>>
    {
        private readonly IAppLogger<DecryptCredentialCommandHandler> _logger;
        private readonly IVaultXDomainService _domainService;
        private readonly IAppConfigService<VaultXConfigurationsEntityModel> _applicationConfigService;

        /// <summary>
        /// Initialize the new instance for <see cref="DecryptCredentialCommandHandler">class.</see>/>
        /// </summary>
        /// <param name="appLogger"></param>
        /// <param name="applicationConfigService"></param>
        /// <param name="domainService"></param>
        public DecryptCredentialCommandHandler(
            IAppLogger<DecryptCredentialCommandHandler> appLogger,
            IAppConfigService<VaultXConfigurationsEntityModel> applicationConfigService,
            IVaultXDomainService domainService)
        {
            _logger = appLogger;
            _domainService = domainService;
            _applicationConfigService = applicationConfigService;
        }

        /// <summary>
        /// Handle the command query for archiving a credential.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<AppResponse<CredentialResponseDTO>> Handle(DecryptCredentialCommand command, CancellationToken cancellationToken = default)
        {
            _logger.LogDebug($"{nameof(DecryptCredentialCommandHandler)} => {nameof(Handle)} method called.");

            // Validate the query
            IList<AppErrors> errors = command.Validate();

            if (errors.Any())
            {
                _logger.LogError($"{nameof(DecryptCredentialCommandHandler)} => Validation errors occurred: ", errors: errors);

                return new AppResponse<CredentialResponseDTO>(errors.ToList());
            }

            _logger.LogDebug($"{nameof(DecryptCredentialCommandHandler)} => {nameof(Handle)} method completed.", request: command);

            return await _domainService.Decrypt(command.CredentialId, command.Workspace);
        }
    }
}
