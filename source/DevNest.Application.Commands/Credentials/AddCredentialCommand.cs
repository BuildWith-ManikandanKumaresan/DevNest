#region using directives
using DevNest.Common.Base.MediatR.Contracts;
using DevNest.Common.Base.Response;
using DevNest.Infrastructure.DTOs.CredentialManager.Request;
using DevNest.Infrastructure.DTOs.CredentialManager.Response;
using MediatR;
#endregion using directives

namespace DevNest.Application.Commands.Credentials
{
    /// <summary>
    /// Represents the class instance for Add credential command.
    /// </summary>
    public class AddCredentialCommand : ICommand<AppResponse<CredentialResponseDTO>>
    {
        /// <summary>
        /// Initialize the new instance for <see cref="AddCredentialCommand">class.</see>/>
        /// </summary>
        /// <param name="request"></param>
        public AddCredentialCommand(AddCredentialRequest request) 
        {
            AddCredentialRequest = request;
        }

        /// <summary>
        /// Gets or sets the add credential request.
        /// </summary>
        public AddCredentialRequest AddCredentialRequest { get; set; }
    }
}
