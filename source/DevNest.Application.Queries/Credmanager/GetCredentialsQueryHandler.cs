#region using directives
using DevNest.Business.Domain.DomainContracts;
using DevNest.Common.Base.Response;
using DevNest.Infrastructure.DTOs;
using DevNest.Common.Logger;
using MediatR;
#endregion using directives

namespace DevNest.Application.Queries.credmanager
{
    /// <summary>
    /// Represents the class instance for Get Credentials query handler class.
    /// </summary>
    public class GetCredentialsQueryHandler : IRequestHandler<GetCredentialsQuery, AppResponse<IEnumerable<CredentialDTO>>>
    {
        private readonly IAppLogger<GetCredentialsQueryHandler> _logger;
        private readonly ICredManagerDomainService _domainService;

        /// <summary>
        /// Initialize the new instance for Get Credentials query handler class.
        /// </summary>
        public GetCredentialsQueryHandler(IAppLogger<GetCredentialsQueryHandler> appLogger,
            ICredManagerDomainService domainService)
        {
            this._logger = appLogger;
            this._domainService = domainService;
        }

        /// <summary>
        /// Handler method to Get the credentials query as input and string as response.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        async Task<AppResponse<IEnumerable<CredentialDTO>>> IRequestHandler<GetCredentialsQuery, AppResponse<IEnumerable<CredentialDTO>>>.Handle(GetCredentialsQuery request, CancellationToken cancellationToken)
        {
            return await _domainService.Get();
        }
    }
}