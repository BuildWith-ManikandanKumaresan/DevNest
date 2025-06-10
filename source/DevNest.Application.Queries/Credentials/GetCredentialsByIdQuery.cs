#region using directives
using DevNest.Common.Base.MediatR.Contracts;
using DevNest.Common.Base.Response;
using DevNest.Infrastructure.DTOs.CredentialManager.Response;
using MediatR;
#endregion using directives

namespace DevNest.Application.Queries.Credentials
{
    /// <summary>
    /// Represents the class instance for Get Credentials by Id query class.
    /// </summary>
    /// <remarks>
    /// Initialize the new instance for GetCredentialsByIdQuery class.
    /// </remarks>
    /// <param name="Id"></param>
    public class GetCredentialsByIdQuery(Guid Id) : IQuery<AppResponse<CredentialsResponseDTO>>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the credential.
        /// </summary>
        public Guid CredentialId { get; set; } = Id;
    }
}
