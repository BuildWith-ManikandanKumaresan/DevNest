#region using directives
using DevNest.Application.Commands.Credentials;
using DevNest.Business.Domain.Domains.Contracts;
using DevNest.Common.Base.Contracts;
using DevNest.Common.Base.MediatR.Contracts;
using DevNest.Common.Base.Response;
using DevNest.Common.Logger;
using DevNest.Infrastructure.DTOs.CredentialManager.Response;
using DevNest.Infrastructure.Entity.Configurations.CredentialManager;
using MediatR;
#endregion using directives

namespace DevNest.Application.CommandHandlers.Credentials
{
    /// <summary>
    /// Handles the command to encrypt a credential.
    /// </summary>
    public class EncryptCredentialCommandHandler : ICommandHandler<EncryptCredentialCommand, AppResponse<CredentialResponseDTO>>
    {
        private readonly IAppLogger<EncryptCredentialCommandHandler> _logger;
        private readonly ICredentialDomainService _domainService;
        private readonly IAppConfigService<CredentialManagerConfigurations> _applicationConfigService;

        /// <summary>
        /// Initialize the new instance for <see cref="EncryptCredentialCommandHandler">class.</see>/>
        /// </summary>
        /// <param name="appLogger"></param>
        /// <param name="applicationConfigService"></param>
        /// <param name="domainService"></param>
        public EncryptCredentialCommandHandler(
            IAppLogger<EncryptCredentialCommandHandler> appLogger,
            IAppConfigService<CredentialManagerConfigurations> applicationConfigService,
            ICredentialDomainService domainService)
        {
            this._logger = appLogger;
            this._domainService = domainService;
            this._applicationConfigService = applicationConfigService;
        }

        /// <summary>
        /// Handle the command query for archiving a credential.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<AppResponse<CredentialResponseDTO>> Handle(EncryptCredentialCommand command, CancellationToken cancellationToken = default)
        {
            _logger.LogDebug($"{nameof(EncryptCredentialCommandHandler)} => {nameof(Handle)} method called.");

            // Validate the query
            IList<AppErrors> errors = command.Validate();

            if (errors.Any())
            {
                _logger.LogError($"{nameof(EncryptCredentialCommandHandler)} => Validation errors occurred: ", errors: errors);

                return new AppResponse<CredentialResponseDTO>(errors.ToList());
            }

            _logger.LogDebug($"{nameof(EncryptCredentialCommandHandler)} => {nameof(Handle)} method completed.", request: command);

            return await _domainService.Encrypt(command.CredentialId, command.Workspace);
        }
    }
}
