#region using directives
using DevNest.Common.Base.Constants.Message;
using DevNest.Common.Base.Helpers;
using DevNest.Common.Base.MediatR;
using DevNest.Common.Base.MediatR.Contracts;
using DevNest.Common.Base.Response;
using MediatR;
#endregion using directives

namespace DevNest.Application.Commands.VaultX.Store
{
    /// <summary>
    /// Represents the class instance for delete credentials by id command.
    /// </summary>
    /// <remarks>
    /// Initialize the new instance for <see cref="DeleteCredentialByIdCommand">class.</see>/>
    /// </remarks>
    /// <param name="id"></param>
    public class DeleteCredentialByIdCommand(Guid id, string workSpace) : CommandBase, ICommand<AppResponse<bool>>
    {

        /// <summary>
        /// Gets or sets the credential id property.
        /// </summary>
        public Guid CredentialId { get; set; } = id;

        /// <summary>
        /// Gets or sets the workspace identifier or name for which to delete the credential.
        /// </summary>
        public string Workspace { get; set; } = workSpace;



        /// <summary>
        /// Validates the command and returns a list of errors if any.
        /// </summary>
        /// <returns></returns>
        public override IList<AppErrors> Validate()
        {
            IList<AppErrors> errors = base.Validate();

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
