#region using directives
using DevNest.Application.Commands.VaultX;
using DevNest.Business.Domain.Domains.Contracts;
using DevNest.Common.Base.Contracts;
using DevNest.Common.Base.MediatR.Contracts;
using DevNest.Common.Base.Response;
using DevNest.Common.Logger;
using DevNest.Infrastructure.Entity;
using DevNest.Infrastructure.Entity.Configurations.VaultX;
using MediatR;
#endregion using directives

namespace DevNest.Application.CommandHandlers.VaultX
{
    /// <summary>
    /// Represents the command query handler for deleting credentials.
    /// </summary>
    public class DeleteCredentialsCommandHandler : ICommandHandler<DeleteCredentialsCommand, AppResponse<bool>>
    {
        private readonly IAppLogger<DeleteCredentialsCommandHandler> _logger;
        private readonly IVaultXDomainService _domainService;
        private readonly IAppConfigService<VaultXConfigurationsEntityModel> _applicationConfigService;

        /// <summary>
        /// Initialize the new instance for <see cref="DeleteCredentialsCommandHandler">class.</see>/>
        /// </summary>
        /// <param name="appLogger"></param>
        /// <param name="applicationConfigService"></param>
        /// <param name="domainService"></param>
        public DeleteCredentialsCommandHandler(
            IAppLogger<DeleteCredentialsCommandHandler> appLogger,
            IAppConfigService<VaultXConfigurationsEntityModel> applicationConfigService,
            IVaultXDomainService domainService)
        {
            _logger = appLogger;
            _domainService = domainService;
            _applicationConfigService = applicationConfigService;
        }

        /// <summary>
        /// Handle the command query for deleting credentials.
        /// </summary>
        /// <param name="command">The request containing the command parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A response indicating the success or failure of the operation.</returns>
        public async Task<AppResponse<bool>> Handle(DeleteCredentialsCommand command, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"{nameof(DeleteCredentialsCommandHandler)} => {nameof(Handle)} method called.");

            // Validate the query
            IList<AppErrors> errors = command.Validate();

            if (errors.Any())
            {
                _logger.LogError($"{nameof(DeleteCredentialsCommandHandler)} => Validation errors occurred: ", errors: errors);

                return new AppResponse<bool>(errors.ToList());
            }

            _logger.LogDebug($"{nameof(DeleteCredentialsCommandHandler)} => {nameof(Handle)} method completed.", request: command);

            return await _domainService.Delete(command.Workspace);
        }
    }
}
