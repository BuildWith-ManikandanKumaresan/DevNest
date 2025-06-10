#region using directives
using DevNest.Common.Base.MediatR.Contracts;
using DevNest.Common.Base.Response;
using DevNest.Infrastructure.DTOs.CredentialManager.Request;
using DevNest.Infrastructure.DTOs.CredentialManager.Response;
using System.Windows.Input;
#endregion using directives

namespace DevNest.Application.Commands.Credentials
{
    /// <summary>
    /// Represents the class instance for Update credential command.
    /// </summary>
    /// <remarks>
    /// Initialize the new instance for <see cref="UpdateCredentialCommand">class.</see>
    /// </remarks>
    /// <param name="request"></param>
    public class UpdateCredentialCommand(Guid credentialId, UpdateCredentialRequest request) : ICommand<AppResponse<CredentialsResponseDTO>>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the credential to be updated.
        /// </summary>
        public Guid? CredentialId { get; set; } = credentialId;

        /// <summary>
        /// Gets or sets the update credential request.
        /// </summary>
        public UpdateCredentialRequest? UpdateCredentialRequest { get; set; } = request;
    }
}