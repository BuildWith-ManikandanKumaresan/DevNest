#region using directives
using DevNest.Common.Base.Constants.Message;
using DevNest.Common.Base.Helpers;
using DevNest.Common.Base.MediatR.Contracts;
using DevNest.Common.Base.MediatR;
using DevNest.Common.Base.Response;
using DevNest.Infrastructure.DTOs.VaultX.Request;
using DevNest.Infrastructure.DTOs.VaultX.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevNest.Infrastructure.DTOs.Configurations.VaultX.Response;
using DevNest.Infrastructure.DTOs.Configurations.VaultX.Request;
#endregion using directives

namespace DevNest.Application.Commands.VaultX.Configuration
{
    /// <summary>
    /// Represents the class instance for Update VaultX configuration command.
    /// </summary>
    /// <param name="request"></param>
    public class UpdateVaultXConfigurationCommand(UpdateVaultXConfigurationsRequestDTO? request) : CommandBase, ICommand<AppResponse<VaultXConfigurationsResponseDTO>>
    {

        /// <summary>
        /// Gets or sets the add credential request.
        /// </summary>
        public UpdateVaultXConfigurationsRequestDTO? UpdateVaultXConfigurationRequest { get; set; } = request;

        /// <summary>
        /// Validates the command and returns a list of errors if any.
        /// </summary>
        /// <returns></returns>
        public override IList<AppErrors> Validate()
        {
            IList<AppErrors> errors = base.Validate();

            // Todo: validate the request if need
            //errId = CommandValidation.ValidateUpdateVaultXConfiguration(this.AddCredentialRequest);

            return errors;
        }
    }
}
