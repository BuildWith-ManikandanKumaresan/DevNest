#region using directives
using DevNest.Common.Base.MediatR;
using DevNest.Common.Base.Response;
using MediatR;
#endregion using directives

namespace DevNest.Application.Commands.CredentialManager
{
    /// <summary>
    /// Represents the command query for deleting credentials.
    /// </summary>
    public class DeleteCredentialsCommand : ICommand<ApplicationResponse<bool>>
    {
        // Todo: add the command parameters here.
    }
}
