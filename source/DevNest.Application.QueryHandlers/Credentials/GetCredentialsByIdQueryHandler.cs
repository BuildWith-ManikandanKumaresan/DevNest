#region using directives
using DevNest.Application.Queries.Credentials;
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

namespace DevNest.Application.QueryHandlers.Credentials
{
    /// <summary>
    /// Represents the handler for the GetCredentialsByIdQuery class.
    /// </summary>
    public class GetCredentialsByIdQueryHandler : IQueryHandler<GetCredentialsByIdQuery, AppResponse<CredentialsResponseDTO>>
    {
        private readonly IAppLogger<GetCredentialsQueryHandler> _logger;
        private readonly ICredentialDomainService _domainService;
        private readonly IAppConfigService<CredentialManagerConfigurations> _applicationConfigService;

        /// <summary>
        /// Initialize the new instance for GetCredentialsByIdQueryHandler class.
        /// </summary>
        /// <param name="appLogger"></param>
        /// <param name="applicationConfigService"></param>
        /// <param name="domainService"></param>
        public GetCredentialsByIdQueryHandler(
            IAppLogger<GetCredentialsQueryHandler> appLogger,
            IAppConfigService<CredentialManagerConfigurations> applicationConfigService,
            ICredentialDomainService domainService)
        {
            _logger = appLogger;
            _domainService = domainService;
            _applicationConfigService = applicationConfigService;
        }

        /// <summary>
        /// Handler method to Get the credentials by Id query as input and CredentialsDTO as response.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<AppResponse<CredentialsResponseDTO>> Handle(GetCredentialsByIdQuery request, CancellationToken cancellationToken)
        {
            return await _domainService.GetById(request.CredentialId);
        }
    }
}
