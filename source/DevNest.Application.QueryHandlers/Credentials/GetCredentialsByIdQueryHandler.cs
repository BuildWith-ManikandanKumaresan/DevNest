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
    /// <remarks>
    /// Initialize the new instance for GetCredentialsByIdQueryHandler class.
    /// </remarks>
    /// <param name="appLogger"></param>
    /// <param name="applicationConfigService"></param>
    /// <param name="domainService"></param>
    public class GetCredentialsByIdQueryHandler(
        IAppLogger<GetCredentialsQueryHandler> appLogger,
        IAppConfigService<CredentialManagerConfigurations> applicationConfigService,
        ICredentialDomainService domainService) : IQueryHandler<GetCredentialsByIdQuery, AppResponse<CredentialResponseDTO>>
    {
        private readonly IAppLogger<GetCredentialsQueryHandler> _logger = appLogger;
        private readonly ICredentialDomainService _domainService = domainService;
        private readonly IAppConfigService<CredentialManagerConfigurations> _applicationConfigService = applicationConfigService;

        /// <summary>
        /// Handler method to Get the credentials by Id query as input and CredentialsDTO as response.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<AppResponse<CredentialResponseDTO>> Handle(GetCredentialsByIdQuery query, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"{nameof(GetCredentialsByIdQueryHandler)} => {nameof(Handle)} method called.");

            // Validate the query
            IList<AppErrors> errors = query.Validate();

            if (errors.Any())
            {
                _logger.LogError($"{nameof(GetCredentialsByIdQueryHandler)} => Validation errors occurred: ", errors: errors);

                return new AppResponse<CredentialResponseDTO>(errors.ToList());
            }

            _logger.LogDebug($"{nameof(GetCredentialsByIdQueryHandler)} => {nameof(Handle)} method completed.", request: query);

            return await _domainService.GetById(query.CredentialId, query.WorkSpace);
        }
    }
}
