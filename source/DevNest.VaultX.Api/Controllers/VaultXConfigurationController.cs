#region using directives
using DevNest.Common.Base.Contracts;
using DevNest.Common.Base.MediatR.Contracts;
using DevNest.Common.Logger;
using Microsoft.AspNetCore.Mvc;
using DevNest.Infrastructure.Entity.Configurations.VaultX;
using DevNest.Common.Base.Response;
using DevNest.Infrastructure.DTOs.VaultX.Response;
using DevNest.Infrastructure.DTOs.Configurations.VaultX.Request;
using DevNest.Infrastructure.DTOs.Configurations.VaultX.Response;
using DevNest.Application.Queries.VaultX.Resource;
using DevNest.Application.Commands.VaultX.Store;
using DevNest.Application.Queries.VaultX.Configuration;
using DevNest.Application.Commands.VaultX.Configuration;
#endregion using directives

namespace DevNest.VaultX.Api.Controllers
{
    /// <summary>
    /// Api controller for VaultX configurations.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="applicationConfigService"></param>
    /// <param name="mediatr"></param>
    [Produces("application/json")]
    [ApiController]
    [Route("api/vaultx/configuration")] // api/vaultx/configuration
    public class VaultXConfigurationController(
        IAppLogger<VaultXController> logger,
        IAppConfigService<VaultXConfigurationsEntityModel> applicationConfigService,
        IAppMedidator mediatr) : ControllerBase
    {
        private readonly IAppLogger<VaultXController> _logger = logger;
        private readonly IAppMedidator _mediator = mediatr;
        private readonly IAppConfigService<VaultXConfigurationsEntityModel> _applicationConfigService = applicationConfigService;

        /// <summary>
        /// Get VaultX configurations.
        /// Api pattern : https:localhost:port/api/vaultx/configuration
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(AppResponse<VaultXConfigurationsResponseDTO>), 200)]
        [ProducesResponseType(typeof(IList<AppErrors>), 400)]
        [ProducesResponseType(typeof(IList<AppErrors>), 404)]
        [ProducesResponseType(typeof(IList<AppErrors>), 500)]
        public async Task<IActionResult> GetConfigurations()
        {
            _logger.LogDebug($"Api => {nameof(GetConfigurations)} called. ", apiCall: HttpContext.Request);
            GetVaultXConfigurationQuery query = new();
            var response = await _mediator.SendQueryAsync(query);
            if (response.IsSuccess)
            {
                _logger.LogDebug($"Api => {nameof(GetConfigurations)} completed. ",
                    apiCall: HttpContext.Request,
                    request: query,
                    response: response);
                return Ok(response);
            }
            _logger.LogError($"Api => {nameof(GetConfigurations)} encountered an error. ",
                apiCall: HttpContext.Request,
                request: query,
                response: response);
            return BadRequest(response.Errors);
        }

        /// <summary>
        /// Update VaultX configurations.
        /// Api pattern : https:localhost:port/api/vaultx/configuration
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(AppResponse<VaultXConfigurationsResponseDTO>), 200)]
        [ProducesResponseType(typeof(IList<AppErrors>), 400)]
        [ProducesResponseType(typeof(IList<AppErrors>), 404)]
        [ProducesResponseType(typeof(IList<AppErrors>), 500)]
        public async Task<IActionResult> UpdateConfigurations([FromBody] UpdateVaultXConfigurationsRequestDTO? request)
        {
            _logger.LogDebug($"Api => {nameof(UpdateConfigurations)} called. ", apiCall: HttpContext.Request, request: request);
            UpdateVaultXConfigurationCommand command = new(request);
            var response = await _mediator.SendCommandAsync(command);
            if (response.IsSuccess)
            {
                _logger.LogDebug($"Api => {nameof(UpdateConfigurations)} completed. ",
                    apiCall: HttpContext.Request,
                    request: command,
                    response: response);
                return Ok(response);
            }
            _logger.LogError($"Api => {nameof(UpdateConfigurations)} encountered an error. ",
                apiCall: HttpContext.Request,
                request: command,
                response: response);
            return BadRequest(response.Errors);
        }
    }
}