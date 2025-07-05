#region using directives
using DevNest.Common.Base.Constants.Message;
using DevNest.Common.Base.Helpers;
using DevNest.Common.Base.MediatR.Contracts;
using DevNest.Common.Base.MediatR;
using DevNest.Common.Base.Response;
using DevNest.Infrastructure.DTOs.VaultX.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevNest.Infrastructure.DTOs.Configurations.VaultX.Response;
#endregion using directives

namespace DevNest.Application.Queries.VaultX.Configuration
{
    /// <summary>
    /// Represents a query to retrieve VaultX configuration settings.
    /// </summary>
    public class GetVaultXConfigurationQuery : QueryBase, IQuery<AppResponse<VaultXConfigurationsResponseDTO>>
    {

        /// <summary>
        /// Validates the query parameters to ensure they meet the required criteria.
        /// </summary>
        /// <returns></returns>
        public override IList<AppErrors> Validate()
        {
            IList<AppErrors> errors = base.Validate();
            return errors;
        }
    }
}
