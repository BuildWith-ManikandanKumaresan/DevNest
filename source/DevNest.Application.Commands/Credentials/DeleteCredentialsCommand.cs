#region using directives
using DevNest.Common.Base.MediatR.Contracts;
using DevNest.Common.Base.Response;
using MediatR;
#endregion using directives

namespace DevNest.Application.Commands.Credentials
{
    /// <summary>
    /// Represents the command query for deleting credentials.
    /// </summary>
    public class DeleteCredentialsCommand : ICommand<AppResponse<bool>>
    {
        // Todo: add the command parameters here.
    }
}
