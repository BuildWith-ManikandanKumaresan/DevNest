#region using directives
using DevNest.Common.Base.Response;
using DevNest.Infrastructure.DTOs;
using DevNest.Common.Logger;
using MediatR;
using DevNest.Business.Domain.Domains.Contracts;
using DevNest.Common.Base.Contracts;
using DevNest.Infrastructure.Entity;
#endregion using directives

namespace DevNest.Application.Queries.CredentialManager
{
    /// <summary>
    /// Represents the class instance for Get Credentials query handler class.
    /// </summary>
    public class GetCredentialsQueryHandler : IRequestHandler<GetCredentialsQuery, ApplicationResponse<IEnumerable<CredentialsDTO>>>
    {
        private readonly IApplicationLogger<GetCredentialsQueryHandler> _logger;
        private readonly ICredentialManagerDomainService _domainService;
        private readonly IApplicationConfigService<CredentialEntity> _applicationConfigService;

        /// <summary>
        /// Initialize the new instance for Get Credentials query handler class.
        /// </summary>
        public GetCredentialsQueryHandler(
            IApplicationLogger<GetCredentialsQueryHandler> appLogger,
            IApplicationConfigService<CredentialEntity> applicationConfigService,
            ICredentialManagerDomainService domainService)
        {
            this._logger = appLogger;
            this._domainService = domainService;
            this._applicationConfigService = applicationConfigService;
        }

        /// <summary>
        /// Handler method to Get the credentials query as input and string as response.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        async Task<ApplicationResponse<IEnumerable<CredentialsDTO>>> IRequestHandler<GetCredentialsQuery, ApplicationResponse<IEnumerable<CredentialsDTO>>>.Handle(GetCredentialsQuery request, CancellationToken cancellationToken)
        {
            return await _domainService.Get();
        }
    }
}