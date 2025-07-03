#region using directives
using DevNest.Common.Base.Constants.Message;
using DevNest.Common.Base.Helpers;
using DevNest.Common.Base.MediatR;
using DevNest.Common.Base.MediatR.Contracts;
using DevNest.Common.Base.Response;
using DevNest.Infrastructure.DTOs.VaultX.Response;
using MediatR;
using static System.Runtime.InteropServices.JavaScript.JSType;
#endregion using directives

namespace DevNest.Application.Queries.VaultX
{
    /// <summary>
    /// Represents the class instance for Get Credentials by Id query class.
    /// </summary>
    /// <remarks>
    /// Initialize the new instance for GetCredentialsByIdQuery class.
    /// </remarks>
    /// <param name="Id"></param>
    public class GetCredentialsByIdQuery(Guid Id, string workSpace) : QueryBase, IQuery<AppResponse<CredentialResponseDTO>>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the credential.
        /// </summary>
        public Guid CredentialId { get; set; } = Id;

        /// <summary>
        /// Gets or sets the workspace identifier or name for which to retrieve the credential.
        /// </summary>
        public string WorkSpace { get; set; } = workSpace;

        /// <summary>
        /// Validates the query and returns a list of errors if any.
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

            errId = QueryValidation.ValidateWorkspace(WorkSpace);

            if (!string.IsNullOrEmpty(errId))
            {
                errors.Add(Messages.GetError(errId));
            }

            return errors;
        }
    }
}
