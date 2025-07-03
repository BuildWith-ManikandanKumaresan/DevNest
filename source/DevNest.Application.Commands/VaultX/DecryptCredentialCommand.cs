#region using directives
using DevNest.Common.Base.Constants.Message;
using DevNest.Common.Base.Helpers;
using DevNest.Common.Base.MediatR;
using DevNest.Common.Base.MediatR.Contracts;
using DevNest.Common.Base.Response;
using DevNest.Infrastructure.DTOs.VaultX.Response;
using System.Windows.Input;
#endregion using directives

namespace DevNest.Application.Commands.VaultX
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DecryptCredentialCommand"/> class.
    /// </summary>
    /// <param name="credentialId">The ID of the credential to encrypt.</param>
    public class DecryptCredentialCommand(Guid credentialId, string workSpace) : CommandBase, ICommand<AppResponse<CredentialResponseDTO>>
    {
        /// <summary>
        /// Gets or sets the ID of the credential to encrypt.
        /// </summary>
        public Guid CredentialId { get; set; } = credentialId;

        /// <summary>
        /// Gets or sets the workspace name or ID associated with the credential.
        /// </summary>
        public string Workspace { get; set; } = workSpace;

        /// <summary>
        /// Validates the command and returns a list of errors if any.
        /// </summary>
        /// <returns></returns>
        public override IList<AppErrors> Validate()
        {
            IList<AppErrors> errors =  base.Validate();

            string errId = QueryValidation.ValidateCredentialId(CredentialId);

            if (!string.IsNullOrEmpty(errId))
            {
                errors.Add(Messages.GetError(errId));
            }

            errId = QueryValidation.ValidateWorkspace(Workspace);

            if (!string.IsNullOrEmpty(errId))
            {
                errors.Add(Messages.GetError(errId));
            }

            return errors;
        }
    }
}
