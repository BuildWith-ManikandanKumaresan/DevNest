#region using directives
using DevNest.Common.Base.Constants.Message;
using DevNest.Common.Base.Helpers;
using DevNest.Common.Base.MediatR.Contracts;
using DevNest.Common.Base.Response;
using MediatR;
#endregion using directives

namespace DevNest.Application.Commands.Credentials
{
    /// <summary>
    /// Represents the command query for deleting credentials.
    /// </summary>
    public class DeleteCredentialsCommand(string workSpace) : CommandBase, ICommand<AppResponse<bool>>
    {
        /// <summary>
        /// Gets or sets the workspace identifier or name for which to delete credentials.
        /// </summary>
        public string Workspace { get; set; } = workSpace;


        /// <summary>
        /// Validates the command and returns a list of errors if any.
        /// </summary>
        /// <returns></returns>
        public override IList<AppErrors> Validate()
        {
            IList<AppErrors> errors = base.Validate();

            string errId = QueryValidation.ValidateWorkspace(this.Workspace);

            if (!string.IsNullOrEmpty(errId))
            {
                errors.Add(Messages.GetError(errId));
            }

            return errors;
        }
    }
}
