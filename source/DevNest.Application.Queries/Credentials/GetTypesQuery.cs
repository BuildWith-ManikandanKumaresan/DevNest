#region using directives
using DevNest.Common.Base.Constants.Message;
using DevNest.Common.Base.Helpers;
using DevNest.Common.Base.MediatR.Contracts;
using DevNest.Common.Base.MediatR;
using DevNest.Common.Base.Response;
using DevNest.Infrastructure.DTOs.Credential.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevNest.Infrastructure.Entity.Credentials;
#endregion using directives7

namespace DevNest.Application.Queries.Credentials
{
    public class GetTypesQuery(Guid categoryId, string workSpace) : QueryBase, IQuery<AppResponse<IList<TypesResponseDTO>>>
    {

        /// <summary>
        /// Gets or sets the unique identifier for the credential category.
        /// </summary>
        public Guid CategoryId { get; set; } = categoryId;

        /// <summary>
        /// Gets or sets the workspace identifier or name for which to retrieve credentials.
        /// </summary>
        public string WorkSpace { get; set; } = workSpace;

        /// <summary>
        /// Validates the query parameters to ensure they meet the required criteria.
        /// </summary>
        /// <returns></returns>
        public override IList<AppErrors> Validate()
        {
            IList<AppErrors> errors = base.Validate();

            string errId = QueryValidation.ValidateWorkspace(this.WorkSpace);

            if (!string.IsNullOrEmpty(errId))
            {
                errors.Add(Messages.GetError(errId));
            }

            errId = QueryValidation.ValidateCategoryId(this.CategoryId);

            if (!string.IsNullOrEmpty(errId))
            {
                errors.Add(Messages.GetError(errId));
            }

            return errors;
        }
    }
}
