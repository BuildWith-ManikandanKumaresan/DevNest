#region using directives
using AutoMapper;
using DevNest.Application.Queries.Credentials;
using DevNest.Business.Domain.Domains.Contracts;
using DevNest.Common.Base.Contracts;
using DevNest.Common.Base.MediatR.Contracts;
using DevNest.Common.Base.Response;
using DevNest.Common.Logger;
using DevNest.Infrastructure.DTOs.Credential.Response;
using DevNest.Infrastructure.Entity.Configurations.CredentialManager;
using DevNest.Infrastructure.Entity.Credentials;
#endregion using directives

namespace DevNest.Application.QueryHandlers.Credentials
{
    /// <summary>
    /// Initialize the new instance for Get Credentials query handler class.
    /// </summary>
    public class GetTypesQueryHandler(
        IAppLogger<GetTypesQueryHandler> appLogger,
        IAppConfigService<CredentialManagerConfigurations> applicationConfigService,
        ICredentialDomainService domainService,
        IMapper mapper) : IQueryHandler<GetTypesQuery, AppResponse<IList<TypesResponseDTO>>>
    {
        private readonly IAppLogger<GetTypesQueryHandler> _logger = appLogger;
        private readonly ICredentialDomainService _domainService = domainService;
        private readonly IAppConfigService<CredentialManagerConfigurations> _applicationConfigService = applicationConfigService;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Handler method to Get the credential category types query as input and list of CredentialCategoryResponseDTO as response.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
#pragma warning disable CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
        public async Task<AppResponse<IList<TypesResponseDTO>?>> Handle(GetTypesQuery query, CancellationToken cancellationToken = default)
#pragma warning restore CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
        {
            _logger.LogDebug($"{nameof(GetTypesQueryHandler)} => {nameof(Handle)} method called.");
            // Validate the query
            IList<AppErrors> errors = query.Validate();
            if (errors.Any())
            {
                _logger.LogError($"{nameof(GetTypesQueryHandler)} => Validation errors occurred: ", errors: errors);
                return await Task.FromResult(new AppResponse<IList<TypesResponseDTO>?>(errors.ToList()));
            }
            // Fetch the credential category types from the domain service
            return await _domainService.GetTypes(query.CategoryId, query.WorkSpace);
        }
    }
}
