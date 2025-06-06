#region using directives
using DevNest.Application.Commands.CredentialManager;
using DevNest.Application.Queries.CredentialManager;
using DevNest.Common.Base.Contracts;
using DevNest.Common.Base.MediatR;
using DevNest.Common.Base.Response;
using DevNest.Common.Logger;
using DevNest.Infrastructure.DTOs.CredentialManager.Request;
using DevNest.Infrastructure.DTOs.CredentialManager.Response;
using DevNest.Infrastructure.Entity;
using DevNest.Infrastructure.Entity.Configurations.CredentialManager;
using MediatR;
using Microsoft.AspNetCore.Mvc;
#endregion using directives


namespace DevNest.CredManager.Api.Controllers
{
    /// <summary>
    /// Api controller for credential managers.
    /// </summary>
    [Produces("application/json")]
    [ApiController]
    [Route("api/credentialmanager")] // api/credmanager
    public class CredentialManagerController : ControllerBase
    {
        private readonly IApplicationLogger<CredentialManagerController> _logger;
        private readonly IApplicationMedidator _mediator;
        private readonly IApplicationConfigService<CredentialManagerConfigurations> _applicationConfigService;

        /// <summary>
        /// Initialize the new instance for Credential manager controller.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mediatr"></param>
        public CredentialManagerController(
            IApplicationLogger<CredentialManagerController> logger,
            IApplicationConfigService<CredentialManagerConfigurations> applicationConfigService,
            IApplicationMedidator mediatr)
        {
            _logger = logger;
            _mediator = mediatr;
            _applicationConfigService = applicationConfigService;
        }

        /// <summary>
        /// Get all credentials.
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(typeof(ApplicationResponse<IEnumerable<CredentialsDTO>>), 200)]
        [ProducesResponseType(typeof(IEnumerable<ApplicationErrors>), 400)]
        [ProducesResponseType(typeof(IEnumerable<ApplicationErrors>), 404)]
        [ProducesResponseType(typeof(IEnumerable<ApplicationErrors>), 500)]
        public async Task<IActionResult> GetCredentials()
        {
            _logger.LogInfo($"Api {nameof(GetCredentials)} called. ", apiCall: HttpContext.Request);

            GetCredentialsQuery query = new();

            var response = await _mediator.SendQueryAsync(query);

            if (response.IsSuccess)
            {
                _logger.LogInfo($"Api {nameof(GetCredentials)} completed. ",
                    apiCall: HttpContext.Request,
                    request: query,
                    response: response);

                return Ok(response);
            }

            _logger.LogInfo($"Api {nameof(GetCredentials)} encountered an error. ",
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
        [ProducesResponseType(typeof(ApplicationResponse<CredentialsDTO>), 200)]
        [ProducesResponseType(typeof(IEnumerable<ApplicationErrors>), 400)]
        [ProducesResponseType(typeof(IEnumerable<ApplicationErrors>), 404)]
        [ProducesResponseType(typeof(IEnumerable<ApplicationErrors>), 500)]
        public async Task<IActionResult> GetCredentialById(Guid credentialId)
        {
            _logger.LogInfo($"Api {nameof(GetCredentialById)} called. ", apiCall: HttpContext.Request, request: credentialId);

            GetCredentialsByIdQuery query = new(Id: credentialId);

            var response = await _mediator.SendQueryAsync(query);

            if (response.IsSuccess)
            {
                _logger.LogInfo($"Api {nameof(GetCredentialById)} completed. ",
                    apiCall: HttpContext.Request,
                    request: query,
                    response: response);
                return Ok(response);
            }
            _logger.LogInfo($"Api {nameof(GetCredentialById)} encountered an error. ",
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
        [ProducesResponseType(typeof(ApplicationResponse<bool>), 200)]
        [ProducesResponseType(typeof(IEnumerable<ApplicationErrors>), 400)]
        [ProducesResponseType(typeof(IEnumerable<ApplicationErrors>), 404)]
        [ProducesResponseType(typeof(IEnumerable<ApplicationErrors>), 500)]
        public async Task<IActionResult> DeleteCredentials()
        {
            _logger.LogInfo($"Api {nameof(DeleteCredentials)} called. ", apiCall: HttpContext.Request);

            DeleteCredentialsCommand command = new();

            var response = await _mediator.SendCommandAsync(command);

            if (response.IsSuccess)
            {
                _logger.LogInfo($"Api {nameof(DeleteCredentials)} completed. ",
                    apiCall: HttpContext.Request,
                    request: command,
                    response: response);
                return Ok(response);
            }
            _logger.LogInfo($"Api {nameof(DeleteCredentials)} encountered an error. ",
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
        [ProducesResponseType(typeof(ApplicationResponse<bool>), 200)]
        [ProducesResponseType(typeof(IEnumerable<ApplicationErrors>), 400)]
        [ProducesResponseType(typeof(IEnumerable<ApplicationErrors>), 404)]
        [ProducesResponseType(typeof(IEnumerable<ApplicationErrors>), 500)]
        public async Task<IActionResult> DeleteCredentialById(Guid credentialId)
        {
            _logger.LogInfo($"Api {nameof(DeleteCredentialById)} called. ", apiCall: HttpContext.Request, request:credentialId);

            DeleteCredentialByIdCommand command = new(id: credentialId);

            var response = await _mediator.SendCommandAsync(command);

            if (response.IsSuccess)
            {
                _logger.LogInfo($"Api {nameof(DeleteCredentialById)} completed. ",
                    apiCall: HttpContext.Request,
                    request: command,
                    response: response);
                return Ok(response);
            }
            _logger.LogInfo($"Api {nameof(DeleteCredentialById)} encountered an error. ",
                apiCall: HttpContext.Request,
                request: command,
                response: response);
            return BadRequest(response.Errors);

        }

        [HttpPost()]
        [ProducesResponseType(typeof(ApplicationResponse<CredentialsDTO>), 200)]
        [ProducesResponseType(typeof(IEnumerable<ApplicationErrors>), 400)]
        [ProducesResponseType(typeof(IEnumerable<ApplicationErrors>), 404)]
        [ProducesResponseType(typeof(IEnumerable<ApplicationErrors>), 500)]
        public async Task<IActionResult> AddCredentials([FromBody] AddCredentialRequest request)
        {

            _logger.LogInfo($"Api {nameof(AddCredentials)} called. ", apiCall: HttpContext.Request, request: request);

            AddCredentialCommand command = new(request:request);

            var response = await _mediator.SendCommandAsync(command);

            if (response.IsSuccess)
            {
                _logger.LogInfo($"Api {nameof(AddCredentials)} completed. ",
                    apiCall: HttpContext.Request,
                    request: command,
                    response: response);
                return Ok(response);
            }
            _logger.LogInfo($"Api {nameof(AddCredentials)} encountered an error. ",
                apiCall: HttpContext.Request,
                request: command,
                response: response);
            return BadRequest(response.Errors);
        }
    }
}
