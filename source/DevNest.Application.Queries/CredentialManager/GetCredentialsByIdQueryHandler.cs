#region using directives
using DevNest.Business.Domain.Domains.Contracts;
using DevNest.Common.Base.Contracts;
using DevNest.Common.Base.MediatR;
using DevNest.Common.Base.Response;
using DevNest.Common.Logger;
using DevNest.Infrastructure.DTOs.CredentialManager.Response;
using DevNest.Infrastructure.Entity;
using DevNest.Infrastructure.Entity.Configurations.CredentialManager;
using MediatR;
#endregion using directives

namespace DevNest.Application.Queries.CredentialManager
{
    /// <summary>
    /// Represents the handler for the GetCredentialsByIdQuery class.
    /// </summary>
    public class GetCredentialsByIdQueryHandler : IQueryHandler<GetCredentialsByIdQuery, ApplicationResponse<CredentialsDTO>>
    {
        private readonly IApplicationLogger<GetCredentialsQueryHandler> _logger;
        private readonly ICredentialManagerDomainService _domainService;
        private readonly IApplicationConfigService<CredentialManagerConfigurations> _applicationConfigService;

        /// <summary>
        /// Initialize the new instance for GetCredentialsByIdQueryHandler class.
        /// </summary>
        /// <param name="appLogger"></param>
        /// <param name="applicationConfigService"></param>
        /// <param name="domainService"></param>
        public GetCredentialsByIdQueryHandler(
            IApplicationLogger<GetCredentialsQueryHandler> appLogger,
            IApplicationConfigService<CredentialManagerConfigurations> applicationConfigService,
            ICredentialManagerDomainService domainService)
        {
            this._logger = appLogger;
            this._domainService = domainService;
            this._applicationConfigService = applicationConfigService;
        }

        /// <summary>
        /// Handler method to Get the credentials by Id query as input and CredentialsDTO as response.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ApplicationResponse<CredentialsDTO>> Handle(GetCredentialsByIdQuery request, CancellationToken cancellationToken)
        {
            return await _domainService.GetById(request.CredentialId);
        }
    }
}
