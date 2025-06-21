#region using directives
using AutoMapper;
using DevNest.Application.Queries.Credentials;
using DevNest.Business.Domain.Domains.Contracts;
using DevNest.Common.Base.Contracts;
using DevNest.Common.Base.MediatR.Contracts;
using DevNest.Common.Base.Response;
using DevNest.Common.Logger;
using DevNest.Infrastructure.DTOs.Credential.Response;
using DevNest.Infrastructure.DTOs.CredentialManager.Response;
using DevNest.Infrastructure.Entity.Configurations.CredentialManager;
#endregion using directives

namespace DevNest.Application.QueryHandlers.Credentials
{
    /// <summary>
    /// Represents the class instance for Get Credential Categories query handler class.
    /// </summary>
    /// <remarks>
    /// Initialize the new instance for Get Credentials query handler class.
    /// </remarks>
    public class GetCategoriesQueryHandler(
        IAppLogger<GetCategoriesQueryHandler> appLogger,
        IAppConfigService<CredentialManagerConfigurations> applicationConfigService,
        ICredentialDomainService domainService,
        IMapper mapper) : IQueryHandler<GetCategoriesQuery, AppResponse<IList<CategoryResponseDTO>>>
    {
        private readonly IAppLogger<GetCategoriesQueryHandler> _logger = appLogger;
        private readonly ICredentialDomainService _domainService = domainService;
        private readonly IAppConfigService<CredentialManagerConfigurations> _applicationConfigService = applicationConfigService;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Handler method to Get the credential categories query as input and list of CredentialCategoryResponseDTO as response.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
#pragma warning disable CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
        public async Task<AppResponse<IList<CategoryResponseDTO>?>> Handle(GetCategoriesQuery query, CancellationToken cancellationToken = default)
#pragma warning restore CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
        {
            _logger.LogDebug($"{nameof(GetCategoriesQueryHandler)} => {nameof(Handle)} method called.");
            // Validate the query
            IList<AppErrors> errors = query.Validate();
            if (errors.Any())
            {
                _logger.LogError($"{nameof(GetCategoriesQueryHandler)} => Validation errors occurred: ", errors: errors);
                return await Task.FromResult(new AppResponse<IList<CategoryResponseDTO>?>(errors.ToList()));
            }
            // Fetch the credential categories from the domain service
            return await _domainService.GetCategories(query.WorkSpace);
        }
    }
}
