#region using directives
using DevNest.Common.Base.Constants.Message;
using DevNest.Common.Base.Helpers;
using DevNest.Common.Base.MediatR;
using DevNest.Common.Base.MediatR.Contracts;
using DevNest.Common.Base.Response;
using DevNest.Infrastructure.DTOs.VaultX.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion using directives

namespace DevNest.Application.Queries.VaultX
{
    /// <summary>
    /// Represents a query to retrieve credential categories.
    /// </summary>
    public class GetCategoriesQuery(string workSpace) : QueryBase, IQuery<AppResponse<IList<CategoryResponseDTO>>>
    {

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

            string errId = QueryValidation.ValidateWorkspace(WorkSpace);

            if (!string.IsNullOrEmpty(errId))
            {
                errors.Add(Messages.GetError(errId));
            }

            return errors;
        }
    }
}
