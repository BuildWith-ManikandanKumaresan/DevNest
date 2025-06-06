﻿#region using directives
using DevNest.Common.Base.Response;
using DevNest.Common.Logger;
using MediatR;
using DevNest.Business.Domain.Domains.Contracts;
using DevNest.Common.Base.Contracts;
using DevNest.Infrastructure.Entity;
using DevNest.Infrastructure.DTOs.CredentialManager.Response;
using DevNest.Infrastructure.Entity.Configurations.CredentialManager;
using DevNest.Common.Base.MediatR;
#endregion using directives

namespace DevNest.Application.Queries.CredentialManager
{
    /// <summary>
    /// Represents the class instance for Get Credentials query handler class.
    /// </summary>
    public class GetCredentialsQueryHandler : IQueryHandler<GetCredentialsQuery, ApplicationResponse<IEnumerable<CredentialsDTO>>>
    {
        private readonly IApplicationLogger<GetCredentialsQueryHandler> _logger;
        private readonly ICredentialManagerDomainService _domainService;
        private readonly IApplicationConfigService<CredentialManagerConfigurations> _applicationConfigService;

        /// <summary>
        /// Initialize the new instance for Get Credentials query handler class.
        /// </summary>
        public GetCredentialsQueryHandler(
            IApplicationLogger<GetCredentialsQueryHandler> appLogger,
            IApplicationConfigService<CredentialManagerConfigurations> applicationConfigService,
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
        public async Task<ApplicationResponse<IEnumerable<CredentialsDTO>>> Handle(GetCredentialsQuery query, CancellationToken cancellationToken = default)
        {
            return await _domainService.Get();
        }
    }
}