#region using directives
using DevNest.Common.Base.MediatR.Contracts;
using DevNest.Common.Base.Response;
using DevNest.Infrastructure.DTOs.CredentialManager.Response;
using MediatR;
#endregion using directives

namespace DevNest.Application.Queries.Credentials
{
    /// <summary>
    /// Represents the class instance for Get Credentials query class.
    /// </summary>
    public class GetCredentialsQuery : IQuery<AppResponse<IList<CredentialResponseDTO>>>
    {
        // Todo: add the query parameters.
    }
}
