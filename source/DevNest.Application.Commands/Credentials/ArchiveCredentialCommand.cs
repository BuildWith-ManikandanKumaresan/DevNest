#region using directives
using DevNest.Common.Base.Constants.Message;
using DevNest.Common.Base.Helpers;
using DevNest.Common.Base.MediatR;
using DevNest.Common.Base.MediatR.Contracts;
using DevNest.Common.Base.Response;
#endregion using directives

namespace DevNest.Application.Commands.Credentials
{
    /// <summary>
    /// Command to archive a credential.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="ArchiveCredentialCommand"/> class.
    /// </remarks>
    /// <param name="id">The identifier of the credential to be archived.</param>
    public class ArchiveCredentialCommand(Guid id, string workspace) : CommandBase, ICommand<AppResponse<bool>>
    {

        /// <summary>
        /// Gets or sets the identifier of the credential to be archived.
        /// </summary>
        public Guid CredentialId { get; set; } = id;

        /// <summary>
        /// Gets or sets the workspace identifier or name for which to archive the credential.
        /// </summary>
        public string WorkSpace { get; set; } = workspace;

        /// <summary>
        /// Validates the command and returns a list of errors if any.
        /// </summary>
        /// <returns></returns>
        public override IList<AppErrors> Validate()
        {
            IList<AppErrors> Errors = base.Validate();

            string errId = QueryValidation.ValidateCredentialId(this.CredentialId);

            if (!string.IsNullOrEmpty(errId))
            {
                Errors.Add(Messages.GetError(errId));
            }

            errId = QueryValidation.ValidateWorkspace(this.WorkSpace);

            if (!string.IsNullOrEmpty(errId))
            {
                Errors.Add(Messages.GetError(errId));
            }

            return Errors;
        }

    }
}