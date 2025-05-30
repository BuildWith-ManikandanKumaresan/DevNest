#region using directives
using DevNest.Common.Logger;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using DevNest.Application.Queries.CredentialManager;
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

        /// <summary>
        /// Initialize the new instance for Credential manager controller.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mediatr"></param>
        public CredentialManagerController(
            IApplicationLogger<CredentialManagerController> logger,
            IMediator mediatr)
        {
            _logger = logger;
            _mediator = mediatr;
        }

        /// <summary>
        /// Get all credentials.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetCredentials()
        {
            _logger.LogInfo($"Api {nameof(GetCredentials)} called. ", apiCall: HttpContext.Request);

            GetCredentialsQuery query = new GetCredentialsQuery();

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
