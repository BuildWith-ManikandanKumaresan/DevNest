#region using directives
using DevNest.Common.Base.Constants.Message;
using DevNest.Common.Base.Helpers;
using DevNest.Common.Base.MediatR;
using DevNest.Common.Base.MediatR.Contracts;
using DevNest.Common.Base.Response;
using DevNest.Infrastructure.DTOs.VaultX.Request;
using DevNest.Infrastructure.DTOs.VaultX.Response;
using MediatR;
#endregion using directives

namespace DevNest.Application.Commands.VaultX
{
    /// <summary>
    /// Represents the class instance for Add credential command.
    /// </summary>
    /// <remarks>
    /// Initialize the new instance for <see cref="AddCredentialCommand">class.</see>/>
    /// </remarks>
    /// <param name="request"></param>
    public class AddCredentialCommand(AddCredentialRequest request, string workspace) : CommandBase, ICommand<AppResponse<CredentialResponseDTO>>
    {

        /// <summary>
        /// Gets or sets the add credential request.
        /// </summary>
        public AddCredentialRequest AddCredentialRequest { get; set; } = request;

        /// <summary>
        /// Gets or sets the workspace identifier or name for which to add the credential.
        /// </summary>
        public string Workspace { get; set; } = workspace;

        /// <summary>
        /// Validates the command and returns a list of errors if any.
        /// </summary>
        /// <returns></returns>
        public override IList<AppErrors> Validate()
        {
            IList<AppErrors> errors =  base.Validate();

            string errId = QueryValidation.ValidateWorkspace(Workspace);

            if (!string.IsNullOrEmpty(errId))
            {
                errors.Add(Messages.GetError(errId));
            }

            // Todo: validate the request if need
            //errId = CommandValidation.ValidateAddCredentialRequest(this.AddCredentialRequest);

            return errors;
        }
    }
}
