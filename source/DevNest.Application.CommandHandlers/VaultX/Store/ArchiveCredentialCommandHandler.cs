#region using directives
using DevNest.Application.Commands.VaultX.Store;
using DevNest.Business.Domain.Domains.VaultX.Contracts;
using DevNest.Common.Base.Contracts;
using DevNest.Common.Base.MediatR.Contracts;
using DevNest.Common.Base.Response;
using DevNest.Common.Logger;
using DevNest.Infrastructure.Entity.Configurations.VaultX;
#endregion using directives

namespace DevNest.Application.CommandHandlers.VaultX.Store
{
    /// <summary>
    /// Command to archive a credential.
    /// </summary>
    public class ArchiveCredentialCommandHandler : ICommandHandler<ArchiveCredentialCommand, AppResponse<bool>>
    {
        private readonly IAppLogger<ArchiveCredentialCommandHandler> _logger;
        private readonly IVaultXStoreDomainService _domainService;
        private readonly IAppConfigService<VaultXConfigurationsEntityModel> _applicationConfigService;

        /// <summary>
        /// Initialize the new instance for <see cref="ArchiveCredentialCommandHandler">class.</see>/>
        /// </summary>
        /// <param name="appLogger"></param>
        /// <param name="applicationConfigService"></param>
        /// <param name="domainService"></param>
        public ArchiveCredentialCommandHandler(
            IAppLogger<ArchiveCredentialCommandHandler> appLogger,
            IAppConfigService<VaultXConfigurationsEntityModel> applicationConfigService,
            IVaultXStoreDomainService domainService)
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
        public async Task<AppResponse<bool>> Handle(ArchiveCredentialCommand command, CancellationToken cancellationToken = default)
        {
            _logger.LogDebug($"{nameof(ArchiveCredentialCommandHandler)} => {nameof(Handle)} method called.");

            // Validate the query
            IList<AppErrors> errors = command.Validate();

            if (errors.Any())
            {
                _logger.LogError($"{nameof(ArchiveCredentialCommandHandler)} => Validation errors occurred: ", errors: errors);

                return new AppResponse<bool>(errors.ToList());
            }

            _logger.LogDebug($"{nameof(ArchiveCredentialCommandHandler)} => {nameof(Handle)} method completed.", request: command);

            return await _domainService.Archive(command.CredentialId, command.WorkSpace);
        }
    }
}
