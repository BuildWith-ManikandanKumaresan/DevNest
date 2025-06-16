#region using directives
using DevNest.Application.Commands.Credentials;
using DevNest.Application.Queries.Credentials;
using DevNest.Common.Base.Constants;
using DevNest.Common.Base.Contracts;
using DevNest.Common.Base.MediatR.Contracts;
using DevNest.Common.Base.Response;
using DevNest.Common.Logger;
using DevNest.Infrastructure.DTOs.Credential.Request;
using DevNest.Infrastructure.DTOs.CredentialManager.Response;
using DevNest.Infrastructure.Entity;
using DevNest.Infrastructure.Entity.Configurations.CredentialManager;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
#endregion using directives


namespace DevNest.CredentialsManager.Api.Controllers
{
    /// <summary>
    /// Api controller for credential managers.
    /// </summary>
    /// <remarks>
    /// Initialize the new instance for Credential manager controller.
    /// </remarks>
    /// <param name="logger"></param>
    /// <param name="mediatr"></param>
    [Produces("application/json")]
    [ApiController]
    [Route("api/credentials")] // api/credentials
    public class CredentialsController(
        IAppLogger<CredentialsController> logger,
        IAppConfigService<CredentialManagerConfigurations> applicationConfigService,
        IAppMedidator mediatr) : ControllerBase
    {
        private readonly IAppLogger<CredentialsController> _logger = logger;
        private readonly IAppMedidator _mediator = mediatr;
        private readonly IAppConfigService<CredentialManagerConfigurations> _applicationConfigService = applicationConfigService;

        /// <summary>
        /// Get all credentials.
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(typeof(AppResponse<IList<CredentialResponseDTO>>), 200)]
        [ProducesResponseType(typeof(IList<AppErrors>), 400)]
        [ProducesResponseType(typeof(IList<AppErrors>), 404)]
        [ProducesResponseType(typeof(IList<AppErrors>), 500)]
        public async Task<IActionResult> GetCredentials(
            [FromQuery] string? environment,
            [FromQuery] string? type,
            [FromQuery] string? domain,
            [FromQuery] string? passwordStrength,
            [FromQuery] bool? isEncrypted,
            [FromQuery] bool? isValid,
            [FromQuery] bool? isDisabled,
            [FromQuery] bool? isExpired,
            [FromQuery] string[]? groups,
            [FromQuery][Required] string workspace = CommonConstants.DefaultWorkspace
            )
        {
            _logger.LogDebug($"Api => {nameof(GetCredentials)} called. ", apiCall: HttpContext.Request);

            GetCredentialsQuery query = new(
                workSpace: workspace,
                environment:environment,
                type:type,domain:domain,
                passwordStrength:passwordStrength,
                isEncrypted:isEncrypted,
                isValid:isValid,
                isDisabled: isDisabled,
                isExpired: isExpired,
                groups: groups
                );

            var response = await _mediator.SendQueryAsync(query);

            if (response.IsSuccess)
            {
                _logger.LogDebug($"Api => {nameof(GetCredentials)} completed. ",
                    apiCall: HttpContext.Request,
                    request: query,
                    response: response);

                return Ok(response);
            }

            _logger.LogError($"Api => {nameof(GetCredentials)} encountered an error. ",
                apiCall: HttpContext.Request,
                request: query,
                response: response);

            return BadRequest(response.Errors);
        }

        /// <summary>
        /// Get credential by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{credentialId}")]
        [ProducesResponseType(typeof(AppResponse<CredentialResponseDTO>), 200)]
        [ProducesResponseType(typeof(IList<AppErrors>), 400)]
        [ProducesResponseType(typeof(IList<AppErrors>), 404)]
        [ProducesResponseType(typeof(IList<AppErrors>), 500)]
        public async Task<IActionResult> GetCredentialById(
            [FromRoute] Guid credentialId,
            [FromQuery][Required] string workspace = CommonConstants.DefaultWorkspace)
        {
            _logger.LogDebug($"Api => {nameof(GetCredentialById)} called. ", apiCall: HttpContext.Request, request: credentialId);

            GetCredentialsByIdQuery query = new(Id: credentialId, workSpace: workspace);

            var response = await _mediator.SendQueryAsync(query);

            if (response.IsSuccess)
            {
                _logger.LogDebug($"Api => {nameof(GetCredentialById)} completed. ",
                    apiCall: HttpContext.Request,
                    request: query,
                    response: response);
                return Ok(response);
            }
            _logger.LogDebug($"Api => {nameof(GetCredentialById)} encountered an error. ",
                apiCall: HttpContext.Request,
                request: query,
                response: response);
            return BadRequest(response.Errors);
        }

        /// <summary>
        /// Delete credentials.
        /// </summary>
        /// <returns></returns>
        [HttpDelete()]
        [ProducesResponseType(typeof(AppResponse<bool>), 200)]
        [ProducesResponseType(typeof(IList<AppErrors>), 400)]
        [ProducesResponseType(typeof(IList<AppErrors>), 404)]
        [ProducesResponseType(typeof(IList<AppErrors>), 500)]
        public async Task<IActionResult> DeleteCredentials([FromQuery][Required] string workspace = CommonConstants.DefaultWorkspace)
        {
            _logger.LogDebug($"Api => {nameof(DeleteCredentials)} called. ", apiCall: HttpContext.Request);

            DeleteCredentialsCommand command = new(workSpace: workspace);

            var response = await _mediator.SendCommandAsync(command);

            if (response.IsSuccess)
            {
                _logger.LogDebug($"Api => {nameof(DeleteCredentials)} completed. ",
                    apiCall: HttpContext.Request,
                    request: command,
                    response: response);
                return Ok(response);
            }
            _logger.LogError($"Api => {nameof(DeleteCredentials)} encountered an error. ",
                apiCall: HttpContext.Request,
                request: command,
                response: response);
            return BadRequest(response.Errors);
        }

        /// <summary>
        /// Delete credentials by id.
        /// </summary>
        /// <param name="credentialId"></param>
        /// <returns></returns>
        [HttpDelete("{credentialId}")]
        [ProducesResponseType(typeof(AppResponse<bool>), 200)]
        [ProducesResponseType(typeof(IList<AppErrors>), 400)]
        [ProducesResponseType(typeof(IList<AppErrors>), 404)]
        [ProducesResponseType(typeof(IList<AppErrors>), 500)]
        public async Task<IActionResult> DeleteCredentialById(
            [FromRoute] Guid credentialId,
            [FromQuery][Required] string workspace = CommonConstants.DefaultWorkspace)
        {
            _logger.LogDebug($"Api => {nameof(DeleteCredentialById)} called. ", apiCall: HttpContext.Request, request: credentialId);

            DeleteCredentialByIdCommand command = new(id: credentialId, workSpace:workspace);

            var response = await _mediator.SendCommandAsync(command);

            if (response.IsSuccess)
            {
                _logger.LogDebug($"Api => {nameof(DeleteCredentialById)} completed. ",
                    apiCall: HttpContext.Request,
                    request: command,
                    response: response);
                return Ok(response);
            }
            _logger.LogError($"Api => {nameof(DeleteCredentialById)} encountered an error. ",
                apiCall: HttpContext.Request,
                request: command,
                response: response);
            return BadRequest(response.Errors);

        }

        /// <summary>
        /// Add credentials.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost()]
        [ProducesResponseType(typeof(AppResponse<CredentialResponseDTO>), 200)]
        [ProducesResponseType(typeof(IList<AppErrors>), 400)]
        [ProducesResponseType(typeof(IList<AppErrors>), 404)]
        [ProducesResponseType(typeof(IList<AppErrors>), 500)]
        public async Task<IActionResult> AddCredentials(
            [FromBody] AddCredentialRequest request,
            [FromQuery][Required] string workspace = CommonConstants.DefaultWorkspace)
        {

            _logger.LogDebug($"Api => {nameof(AddCredentials)} called. ", apiCall: HttpContext.Request, request: request);

            AddCredentialCommand command = new(request: request,workspace:workspace);

            var response = await _mediator.SendCommandAsync(command);

            if (response.IsSuccess)
            {
                _logger.LogDebug($"Api => {nameof(AddCredentials)} completed. ",
                    apiCall: HttpContext.Request,
                    request: command,
                    response: response);
                return Ok(response);
            }
            _logger.LogError($"Api => {nameof(AddCredentials)} encountered an error. ",
                apiCall: HttpContext.Request,
                request: command,
                response: response);
            return BadRequest(response.Errors);
        }

        /// <summary>
        /// Update credentials by Id.
        /// </summary>
        /// <param name="credentialId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{credentialId}")]
        [ProducesResponseType(typeof(AppResponse<CredentialResponseDTO>), 200)]
        [ProducesResponseType(typeof(IList<AppErrors>), 400)]
        [ProducesResponseType(typeof(IList<AppErrors>), 404)]
        [ProducesResponseType(typeof(IList<AppErrors>), 500)]
        public async Task<IActionResult> UpdateCredentials(
            [FromRoute] Guid credentialId, 
            [FromBody] UpdateCredentialRequest request,
            [FromQuery][Required] string workspace = CommonConstants.DefaultWorkspace)
        {
            _logger.LogDebug($"Api => {nameof(UpdateCredentials)} called. ", apiCall: HttpContext.Request, request: request);
            UpdateCredentialCommand command = new(credentialId: credentialId, request: request,workspace:workspace);
            var response = await _mediator.SendCommandAsync(command);
            if (response.IsSuccess)
            {
                _logger.LogDebug($"Api => {nameof(UpdateCredentials)} completed. ",
                    apiCall: HttpContext.Request,
                    request: command,
                    response: response);
                return Ok(response);
            }
            _logger.LogError($"Api => {nameof(UpdateCredentials)} encountered an error. ",
                apiCall: HttpContext.Request,
                request: command,
                response: response);
            return BadRequest(response.Errors);
        }

        /// <summary>
        /// Archive credentials by Id.
        /// </summary>
        /// <param name="credentialId"></param>
        /// <returns></returns>
        [HttpPut("{credentialId}/archive")]
        [ProducesResponseType(typeof(AppResponse<bool>), 200)]
        [ProducesResponseType(typeof(IList<AppErrors>), 400)]
        [ProducesResponseType(typeof(IList<AppErrors>), 404)]
        [ProducesResponseType(typeof(IList<AppErrors>), 500)]
        public async Task<IActionResult> ArchiveCredentials(
            [FromRoute] Guid credentialId,
            [FromQuery][Required] string workspace = CommonConstants.DefaultWorkspace)
        {
            _logger.LogDebug($"Api => {nameof(ArchiveCredentials)} called. ", apiCall: HttpContext.Request, request: credentialId);
            ArchiveCredentialCommand command = new(id: credentialId, workspace: workspace);
            var response = await _mediator.SendCommandAsync(command);
            if (response.IsSuccess)
            {
                _logger.LogDebug($"Api => {nameof(ArchiveCredentials)} completed. ",
                    apiCall: HttpContext.Request,
                    request: command,
                    response: response);
                return Ok(response);
            }
            _logger.LogError($"Api => {nameof(ArchiveCredentials)} encountered an error. ",
                apiCall: HttpContext.Request,
                request: command,
                response: response);
            return BadRequest(response.Errors);
        }

        /// <summary>
        /// Encrypt credentials by Id.
        /// </summary>
        /// <param name="credentialId"></param>
        /// <returns></returns>
        [HttpPut("{credentialId}/encrypt")]
        [ProducesResponseType(typeof(AppResponse<CredentialResponseDTO>), 200)]
        [ProducesResponseType(typeof(IList<AppErrors>), 400)]
        [ProducesResponseType(typeof(IList<AppErrors>), 404)]
        [ProducesResponseType(typeof(IList<AppErrors>), 500)]
        public async Task<IActionResult> EncryptCredentials(
            [FromRoute] Guid credentialId,
            [FromQuery][Required] string workspace = CommonConstants.DefaultWorkspace)
        {
            _logger.LogDebug($"Api => {nameof(EncryptCredentials)} called. ", apiCall: HttpContext.Request, request: credentialId);
            EncryptCredentialCommand command = new(credentialId: credentialId, workSpace:workspace);
            var response = await _mediator.SendCommandAsync(command);
            if (response.IsSuccess)
            {
                _logger.LogDebug($"Api => {nameof(EncryptCredentials)} completed. ",
                    apiCall: HttpContext.Request,
                    request: command,
                    response: response);
                return Ok(response);
            }
            _logger.LogError($"Api => {nameof(EncryptCredentials)} encountered an error. ",
                apiCall: HttpContext.Request,
                request: command,
                response: response);
            return BadRequest(response.Errors);

        }

        /// <summary>
        /// Decrypt credentials by Id.
        /// </summary>
        /// <param name="credentialId"></param>
        /// <returns></returns>
        [HttpPut("{credentialId}/decrypt")]
        [ProducesResponseType(typeof(AppResponse<CredentialResponseDTO>), 200)]
        [ProducesResponseType(typeof(IList<AppErrors>), 400)]
        [ProducesResponseType(typeof(IList<AppErrors>), 404)]
        [ProducesResponseType(typeof(IList<AppErrors>), 500)]
        public async Task<IActionResult> DecryptCredentials(
            [FromRoute] Guid credentialId,
            [FromQuery][Required] string workspace = CommonConstants.DefaultWorkspace)
        {
            _logger.LogDebug($"Api => {nameof(DecryptCredentials)} called. ", apiCall: HttpContext.Request, request: credentialId);
            DecryptCredentialCommand command = new(credentialId: credentialId, workSpace:workspace);
            var response = await _mediator.SendCommandAsync(command);
            if (response.IsSuccess)
            {
                _logger.LogDebug($"Api => {nameof(DecryptCredentials)} completed. ",
                    apiCall: HttpContext.Request,
                    request: command,
                    response: response);
                return Ok(response);
            }
            _logger.LogError($"Api => {nameof(DecryptCredentials)} encountered an error. ",
                apiCall: HttpContext.Request,
                request: command,
                response: response);
            return BadRequest(response.Errors);
        }
    }
}
