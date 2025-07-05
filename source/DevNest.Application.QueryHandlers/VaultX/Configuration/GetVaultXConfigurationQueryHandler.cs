#region using directives
using AutoMapper;
using DevNest.Application.Queries.VaultX;
using DevNest.Application.Queries.VaultX.Configuration;
using DevNest.Application.QueryHandlers.VaultX.Resource;
using DevNest.Application.QueryHandlers.VaultX.Store;
using DevNest.Business.Domain.Domains.VaultX.Contracts;
using DevNest.Common.Base.Contracts;
using DevNest.Common.Base.MediatR.Contracts;
using DevNest.Common.Base.Response;
using DevNest.Common.Logger;
using DevNest.Infrastructure.DTOs.Configurations.VaultX.Response;
using DevNest.Infrastructure.DTOs.VaultX.Response;
using DevNest.Infrastructure.Entity.Configurations.VaultX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion using directives

namespace DevNest.Application.QueryHandlers.VaultX.Configuration
{
    /// <summary>
    /// Represents the class instance for Get VaultX Configuration query handler class.
    /// </summary>
    /// <param name="appLogger"></param>
    /// <param name="domainService"></param>
    /// <param name="mapper"></param>
    public class GetVaultXConfigurationQueryHandler(
        IAppLogger<GetVaultXConfigurationQueryHandler> appLogger,
        IVaultXConfigurationDomainService domainService,
        IMapper mapper) : IQueryHandler<GetVaultXConfigurationQuery, AppResponse<VaultXConfigurationsResponseDTO>>
    {
        private readonly IAppLogger<GetVaultXConfigurationQueryHandler> _logger = appLogger;
        private readonly IVaultXConfigurationDomainService _domainService = domainService;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Handler method to execute the <see cref="GetVaultXConfigurationQuery">class.</see>
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
#pragma warning disable CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
        public async Task<AppResponse<VaultXConfigurationsResponseDTO?>> Handle(GetVaultXConfigurationQuery query, CancellationToken cancellationToken = default)
#pragma warning restore CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
        {
            _logger.LogDebug($"{nameof(GetVaultXConfigurationQueryHandler)} => {nameof(Handle)} method called.");
            // Validate the query
            IList<AppErrors> errors = query.Validate();
            if (errors.Any())
            {
                _logger.LogError($"{nameof(GetVaultXConfigurationQueryHandler)} => Validation errors occurred: ", errors: errors);
                return await Task.FromResult(new AppResponse<VaultXConfigurationsResponseDTO?>(errors.ToList()));
            }
            // Fetch the credential categories from the domain service
            return await _domainService.GetConfigurations();
        }
    }
}
