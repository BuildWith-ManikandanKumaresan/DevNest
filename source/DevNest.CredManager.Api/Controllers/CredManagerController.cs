#region using directives
using DevNest.Application.Queries.CredentialManager;
using DevNest.Common.Base.Contracts;
using DevNest.Common.Base.Response;
using DevNest.Common.Logger;
using DevNest.Infrastructure.DTOs;
using DevNest.Infrastructure.Entity;
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
        private readonly IMediator _mediator;
        private readonly IApplicationConfigService<CredentialEntity> _applicationConfigService;

        /// <summary>
        /// Initialize the new instance for Credential manager controller.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mediatr"></param>
        public CredentialManagerController(
            IApplicationLogger<CredentialManagerController> logger,
            IApplicationConfigService<CredentialEntity> applicationConfigService,
            IMediator mediatr)
        {
            _logger = logger;
            _mediator = mediatr;
            _applicationConfigService = applicationConfigService;
        }

        /// <summary>
        /// Get all credentials.
        /// </summary>
        /// <returns></returns>
        [HttpGet]        
        [ProducesResponseType(typeof(ApplicationResponse<IEnumerable<CredentialsDTO>>),200)]
        [ProducesResponseType(typeof(IEnumerable<ApplicationErrors>), 400)]
        [ProducesResponseType(typeof(IEnumerable<ApplicationErrors>), 404)]
        [ProducesResponseType(typeof(IEnumerable<ApplicationErrors>), 500)]
        public async Task<IActionResult> GetCredentials()
        {
            _logger.LogInfo($"Api {nameof(GetCredentials)} called. ", apiCall: HttpContext.Request);

            GetCredentialsQuery query = new();

            var response = await _mediator.Send(query);

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
    }
}
