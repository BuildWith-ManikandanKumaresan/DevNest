#region using directives
using DevNest.Common.Base.Response;
using DevNest.Common.Logger;
using MediatR;
using DevNest.Common.Base.Contracts;
using DevNest.Infrastructure.Entity;
using DevNest.Common.Base.MediatR.Contracts;
using Microsoft.AspNetCore.Http;
using DevNest.Business.Domain.Domains;
using AutoMapper;
using DevNest.Infrastructure.Entity.Search;
using DevNest.Application.Queries.VaultX;
using DevNest.Infrastructure.DTOs.VaultX.Response;
using DevNest.Infrastructure.Entity.Configurations.VaultX;
using DevNest.Business.Domain.Domains.VaultX.Contracts;
#endregion using directives

namespace DevNest.Application.QueryHandlers.VaultX.Store
{
    /// <summary>
    /// Represents the class instance for Get Credentials query handler class.
    /// </summary>
    /// <remarks>
    /// Initialize the new instance for Get Credentials query handler class.
    /// </remarks>
    public class GetCredentialsQueryHandler(
        IAppLogger<GetCredentialsQueryHandler> appLogger,
        IAppConfigService<VaultXConfigurationsEntityModel> applicationConfigService,
        IVaultXStoreDomainService domainService,
        IMapper mapper) : IQueryHandler<GetCredentialsQuery, AppResponse<IList<CredentialResponseDTO>>>
    {
        private readonly IAppLogger<GetCredentialsQueryHandler> _logger = appLogger;
        private readonly IVaultXStoreDomainService _domainService = domainService;
        private readonly IAppConfigService<VaultXConfigurationsEntityModel> _applicationConfigService = applicationConfigService;
        private readonly IMapper _mapper = mapper;

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
                category: query.Category,
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