#region using directives
using DevNest.Common.Base.Response;
using DevNest.Common.Logger;
using MediatR;
using DevNest.Business.Domain.Domains.Contracts;
using DevNest.Common.Base.Contracts;
using DevNest.Infrastructure.Entity;
using DevNest.Infrastructure.DTOs.CredentialManager.Response;
using DevNest.Infrastructure.Entity.Configurations.CredentialManager;
using DevNest.Common.Base.MediatR.Contracts;
using DevNest.Application.Queries.Credentials;
using Microsoft.AspNetCore.Http;
using DevNest.Business.Domain.Domains;
using AutoMapper;
using DevNest.Infrastructure.Entity.Search;
#endregion using directives

namespace DevNest.Application.QueryHandlers.Credentials
{
    /// <summary>
    /// Represents the class instance for Get Credentials query handler class.
    /// </summary>
    public class GetCredentialsQueryHandler : IQueryHandler<GetCredentialsQuery, AppResponse<IList<CredentialResponseDTO>>>
    {
        private readonly IAppLogger<GetCredentialsQueryHandler> _logger;
        private readonly ICredentialDomainService _domainService;
        private readonly IAppConfigService<CredentialManagerConfigurations> _applicationConfigService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initialize the new instance for Get Credentials query handler class.
        /// </summary>
        public GetCredentialsQueryHandler(
            IAppLogger<GetCredentialsQueryHandler> appLogger,
            IAppConfigService<CredentialManagerConfigurations> applicationConfigService,
            ICredentialDomainService domainService,
            IMapper mapper)
        {
            this._mapper = mapper;
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
        public async Task<AppResponse<IList<CredentialResponseDTO>>> Handle(GetCredentialsQuery query, CancellationToken cancellationToken = default)
        {
            _logger.LogDebug($"{nameof(GetCredentialsQueryHandler)} => {nameof(Handle)} method called.");

            // Validate the query
            IList<AppErrors> errors = query.Validate();

            if (errors.Any())
            {
                _logger.LogError($"{nameof(GetCredentialsQueryHandler)} => Validation errors occurred: ", errors: errors);

                return new AppResponse<IList<CredentialResponseDTO>>(errors.ToList());
            }

            _logger.LogDebug($"{nameof(GetCredentialsQueryHandler)} => {nameof(Handle)} method completed.", request: query);

            return await _domainService.Get(
                environment: query.Environment,
                type: query.Type,
                domain: query.Domain,
                passwordStrength: query.PasswordStrength,
                isEncrypted: query.IsEncrypted,
                isValid: query.IsValid,
                isDisabled: query.IsDisabled,
                isExpired: query.IsExpired,
                groups: query.Groups,
                workspace: query.WorkSpace,
                searchFilter: _mapper.Map<SearchEntityModel>(query.SearchFilter)
                );
        }
    }
}