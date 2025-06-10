#region using directives
using DevNest.Application.Commands.Credentials;
using DevNest.Business.Domain.Domains.Contracts;
using DevNest.Common.Base.Contracts;
using DevNest.Common.Base.MediatR.Contracts;
using DevNest.Common.Base.Response;
using DevNest.Common.Logger;
using DevNest.Infrastructure.Entity;
using DevNest.Infrastructure.Entity.Configurations.CredentialManager;
using MediatR;
#endregion using directives

namespace DevNest.Application.CommandHandlers.Credentials
{
    /// <summary>
    /// Represents the command query handler for deleting credentials using id.
    /// </summary>
    public class DeleteCredentialByIdCommandHandler : ICommandHandler<DeleteCredentialByIdCommand, AppResponse<bool>>
    {
        private readonly IAppLogger<DeleteCredentialByIdCommandHandler> _logger;
        private readonly ICredentialDomainService _domainService;
        private readonly IAppConfigService<CredentialManagerConfigurations> _applicationConfigService;

        /// <summary>
        /// Initialize the new instance for <see cref="DeleteCredentialByIdCommandHandler">class.</see>/>
        /// </summary>
        /// <param name="appLogger"></param>
        /// <param name="applicationConfigService"></param>
        /// <param name="domainService"></param>
        public DeleteCredentialByIdCommandHandler(
            IAppLogger<DeleteCredentialByIdCommandHandler> appLogger,
            IAppConfigService<CredentialManagerConfigurations> applicationConfigService,
            ICredentialDomainService domainService)
        {
            this._logger = appLogger;
            this._domainService = domainService;
            this._applicationConfigService = applicationConfigService;
        }


        /// <summary>
        /// Handle the command query for deleting credentials using id.
        /// </summary>
        /// <param name="request">The request containing the command parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A response indicating the success or failure of the operation.</returns>
        public async Task<AppResponse<bool>> Handle(DeleteCredentialByIdCommand request, CancellationToken cancellationToken)
        {
            return await _domainService.DeleteById(id: request.CredentialId);
        }
    }
}
