#region using directives
using DevNest.Common.Base.Constants.Message;
using DevNest.Common.Base.Helpers;
using DevNest.Common.Base.MediatR;
using DevNest.Common.Base.MediatR.Contracts;
using DevNest.Common.Base.Response;
using DevNest.Infrastructure.DTOs.Credential.Request;
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
    public class UpdateCredentialCommand(Guid credentialId, UpdateCredentialRequest request, string workspace) : CommandBase, ICommand<AppResponse<CredentialResponseDTO>>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the credential to be updated.
        /// </summary>
        public Guid? CredentialId { get; set; } = credentialId;

        /// <summary>
        /// Gets or sets the update credential request.
        /// </summary>
        public UpdateCredentialRequest? UpdateCredentialRequest { get; set; } = request;

        /// <summary>
        /// Gets or sets the workspace identifier or name for which to update the credential.
        /// </summary>
        public string Workspace { get; set; } = workspace;



        /// <summary>
        /// Validates the command and returns a list of errors if any.
        /// </summary>
        /// <returns></returns>
        public override IList<AppErrors> Validate()
        {
            IList<AppErrors> errors = base.Validate();

            string errId = QueryValidation.ValidateCredentialId(this.CredentialId);

            if (!string.IsNullOrEmpty(errId))
            {
                errors.Add(Messages.GetError(errId));
            }

            errId = QueryValidation.ValidateWorkspace(this.Workspace);

            if (!string.IsNullOrEmpty(errId))
            {
                errors.Add(Messages.GetError(errId));
            }
            
            // Todo: add validations for update request if needed.
            //errId = QueryValidation.ValidateUpdateCredentialRequest(this.UpdateCredentialRequest);

            return errors;
        }
    }
}