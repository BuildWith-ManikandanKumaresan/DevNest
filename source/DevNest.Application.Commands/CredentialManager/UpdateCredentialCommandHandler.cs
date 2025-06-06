#region using directives
using DevNest.Business.Domain.Domains.Contracts;
using DevNest.Common.Base.Contracts;
using DevNest.Common.Base.MediatR;
using DevNest.Common.Base.Response;
using DevNest.Common.Logger;
using DevNest.Infrastructure.DTOs.CredentialManager.Response;
using DevNest.Infrastructure.Entity.Configurations.CredentialManager;
#endregion using directives

namespace DevNest.Application.Commands.CredentialManager
{
    /// <summary>
    /// Represents the class instance for Update credential command.
    /// </summary>
    public class UpdateCredentialCommandHandler : ICommandHandler<UpdateCredentialCommand, ApplicationResponse<CredentialsDTO>>
    {
        private readonly IApplicationLogger<UpdateCredentialCommandHandler> _logger;
        private readonly ICredentialManagerDomainService _domainService;
        private readonly IApplicationConfigService<CredentialManagerConfigurations> _applicationConfigService;

        /// <summary>
        /// Initialize the new instance for <see cref="UpdateCredentialCommandHandler">class.</see>/>
        /// </summary>
        /// <param name="appLogger"></param>
        /// <param name="applicationConfigService"></param>
        /// <param name="domainService"></param>
        public UpdateCredentialCommandHandler(
            IApplicationLogger<UpdateCredentialCommandHandler> appLogger,
            IApplicationConfigService<CredentialManagerConfigurations> applicationConfigService,
            ICredentialManagerDomainService domainService)
        {
            this._logger = appLogger;
            this._domainService = domainService;
            this._applicationConfigService = applicationConfigService;
        }

        /// <summary>
        /// Handler method to execute the <see cref="UpdateCredentialCommand">class.</see>
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ApplicationResponse<CredentialsDTO>> Handle(UpdateCredentialCommand request, CancellationToken cancellationToken)
        {
            return await _domainService.Update(request.UpdateCredentialRequest);
        }
    }
}
