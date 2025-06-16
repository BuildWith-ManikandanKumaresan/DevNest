#region using directives
using DevNest.Application.Commands.Credentials;
using DevNest.Business.Domain.Domains.Contracts;
using DevNest.Common.Base.Contracts;
using DevNest.Common.Base.MediatR.Contracts;
using DevNest.Common.Base.Response;
using DevNest.Common.Logger;
using DevNest.Infrastructure.DTOs.CredentialManager.Response;
using DevNest.Infrastructure.Entity.Configurations.CredentialManager;
#endregion using directives

namespace DevNest.Application.CommandHandlers.Credentials
{
    /// <summary>
    /// Represents the class instance for Update credential command.
    /// </summary>
    public class UpdateCredentialCommandHandler : ICommandHandler<UpdateCredentialCommand, AppResponse<CredentialResponseDTO>>
    {
        private readonly IAppLogger<UpdateCredentialCommandHandler> _logger;
        private readonly ICredentialDomainService _domainService;
        private readonly IAppConfigService<CredentialManagerConfigurations> _applicationConfigService;

        /// <summary>
        /// Initialize the new instance for <see cref="UpdateCredentialCommandHandler">class.</see>/>
        /// </summary>
        /// <param name="appLogger"></param>
        /// <param name="applicationConfigService"></param>
        /// <param name="domainService"></param>
        public UpdateCredentialCommandHandler(
            IAppLogger<UpdateCredentialCommandHandler> appLogger,
            IAppConfigService<CredentialManagerConfigurations> applicationConfigService,
            ICredentialDomainService domainService)
        {
            this._logger = appLogger;
            this._domainService = domainService;
            this._applicationConfigService = applicationConfigService;
        }

        /// <summary>
        /// Handler method to execute the <see cref="UpdateCredentialCommand">class.</see>
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<AppResponse<CredentialResponseDTO>> Handle(UpdateCredentialCommand command, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"{nameof(UpdateCredentialCommandHandler)} => {nameof(Handle)} method called.");

            // Validate the query
            IList<AppErrors> errors = command.Validate();

            if (errors.Any())
            {
                _logger.LogError($"{nameof(UpdateCredentialCommandHandler)} => Validation errors occurred: ", errors: errors);

                return new AppResponse<CredentialResponseDTO>(errors.ToList());
            }

            _logger.LogDebug($"{nameof(UpdateCredentialCommandHandler)} => {nameof(Handle)} method completed.", request: command);

            return await _domainService.Update(command.UpdateCredentialRequest, command.Workspace);
        }
    }
}
