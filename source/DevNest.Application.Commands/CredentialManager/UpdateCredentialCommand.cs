#region using directives
using DevNest.Common.Base.MediatR;
using DevNest.Common.Base.Response;
using DevNest.Infrastructure.DTOs.CredentialManager.Request;
using DevNest.Infrastructure.DTOs.CredentialManager.Response;
using System.Windows.Input;
#endregion using directives

namespace DevNest.Application.Commands.CredentialManager
{
    /// <summary>
    /// Represents the class instance for Update credential command.
    /// </summary>
    public class UpdateCredentialCommand : ICommand<ApplicationResponse<CredentialsDTO>>
    {
        /// <summary>
        /// Initialize the new instance for <see cref="UpdateCredentialCommand">class.</see>
        /// </summary>
        /// <param name="request"></param>
        public UpdateCredentialCommand(UpdateCredentialRequest request)
        {
            this.UpdateCredentialRequest = request;
        }

        /// <summary>
        /// Gets or sets the update credential request.
        /// </summary>
        public UpdateCredentialRequest? UpdateCredentialRequest { get; set; }
    }
}