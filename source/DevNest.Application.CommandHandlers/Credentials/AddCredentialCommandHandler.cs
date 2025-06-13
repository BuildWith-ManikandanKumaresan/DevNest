#region using directives
using DevNest.Application.Commands.Credentials;
using DevNest.Business.Domain.Domains.Contracts;
using DevNest.Common.Base.Contracts;
using DevNest.Common.Base.MediatR.Contracts;
using DevNest.Common.Base.Response;
using DevNest.Common.Logger;
using DevNest.Infrastructure.DTOs.CredentialManager.Response;
using DevNest.Infrastructure.Entity;
using DevNest.Infrastructure.Entity.Configurations.CredentialManager;
using MediatR;
#endregion using directives

namespace DevNest.Application.CommandHandlers.Credentials
{
    /// <summary>
    /// Represents the class instance for add credentials command handler.
    /// </summary>
    public class AddCredentialCommandHandler : ICommandHandler<AddCredentialCommand, AppResponse<CredentialResponseDTO>>
    {
        private readonly IAppLogger<AddCredentialCommandHandler> _logger;
        private readonly ICredentialDomainService _domainService;
        private readonly IAppConfigService<CredentialManagerConfigurations> _applicationConfigService;

        /// <summary>
        /// Initialize the new instance for <see cref="AddCredentialCommandHandler">class.</see>/>
        /// </summary>
        /// <param name="appLogger"></param>
        /// <param name="applicationConfigService"></param>
        /// <param name="domainService"></param>
        public AddCredentialCommandHandler(
            IAppLogger<AddCredentialCommandHandler> appLogger,
            IAppConfigService<CredentialManagerConfigurations> applicationConfigService,
            ICredentialDomainService domainService)
        {
            _logger = appLogger;
            _domainService = domainService;
            _applicationConfigService = applicationConfigService;
        }

        /// <summary>
        /// Handler method to execute the <see cref="AddCredentialCommandHandler">class.</see>/>
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<AppResponse<CredentialResponseDTO>> Handle(AddCredentialCommand request, CancellationToken cancellationToken)
        {
            return await _domainService.Add(request: request.AddCredentialRequest);
        }
    }
}
