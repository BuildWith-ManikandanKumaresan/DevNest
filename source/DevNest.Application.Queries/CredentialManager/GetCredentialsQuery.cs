#region using directives
using DevNest.Common.Base.MediatR;
using DevNest.Common.Base.Response;
using DevNest.Infrastructure.DTOs.CredentialManager.Response;
using MediatR;
#endregion using directives

namespace DevNest.Application.Queries.CredentialManager
{
    /// <summary>
    /// Represents the class instance for Get Credentials query class.
    /// </summary>
    public class GetCredentialsQuery : IQuery<ApplicationResponse<IEnumerable<CredentialsDTO>>>
    {
        // Todo: add the query parameters.
    }
}
