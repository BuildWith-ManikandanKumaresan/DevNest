#region using directives
using DevNest.Common.Base.MediatR.Contracts;
using DevNest.Common.Base.Response;
using DevNest.Infrastructure.DTOs.CredentialManager.Response;
using System.Windows.Input;
#endregion using directives

namespace DevNest.Application.Commands.Credentials
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EncryptCredentialCommand"/> class.
    /// </summary>
    /// <param name="credentialId">The ID of the credential to encrypt.</param>
    public class EncryptCredentialCommand(Guid credentialId) : ICommand<AppResponse<CredentialResponseDTO>>
    {
        /// <summary>
        /// Gets or sets the ID of the credential to encrypt.
        /// </summary>
        public Guid CredentialId { get; set; } = credentialId;
    }
}
