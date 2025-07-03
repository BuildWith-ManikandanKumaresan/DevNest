#region using directives
using DevNest.Application.Commands.VaultX;
using DevNest.Application.Queries.VaultX;
using DevNest.Common.Base.Constants;
using DevNest.Common.Base.Contracts;
using DevNest.Common.Base.MediatR.Contracts;
using DevNest.Common.Base.Response;
using DevNest.Common.Logger;
using DevNest.Infrastructure.DTOs.VaultX.Request;
using DevNest.Infrastructure.DTOs.VaultX.Response;
using DevNest.Infrastructure.DTOs.Search;
using DevNest.Infrastructure.DTOs.VaultX.Request;
using DevNest.Infrastructure.Entity;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using DevNest.Infrastructure.Entity.Configurations.VaultX;
#endregion using directives

namespace DevNest.VaultX.Api.Controllers
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
    [Route("api/vaultx/resources/")] // api/vaultx
    public class VaultXResourceController(
        IAppLogger<VaultXResourceController> logger,
        IAppConfigService<VaultXConfigurationsEntityModel> applicationConfigService,
        IAppMedidator mediatr) : ControllerBase
    {
        private readonly IAppLogger<VaultXResourceController> _logger = logger;
        private readonly IAppMedidator _mediator = mediatr;
        private readonly IAppConfigService<VaultXConfigurationsEntityModel> _applicationConfigService = applicationConfigService;


        /// <summary>
        /// Get credential categories.
        /// Api pattern : https:localhost:port/api/vaultx/resources/category
        /// </summary>
        /// <returns></returns>
        [HttpGet("category")]
        [ProducesResponseType(typeof(AppResponse<IList<CategoryResponseDTO>>), 200)]
        [ProducesResponseType(typeof(IList<AppErrors>), 400)]
        [ProducesResponseType(typeof(IList<AppErrors>), 404)]
        [ProducesResponseType(typeof(IList<AppErrors>), 500)]
        public async Task<IActionResult> GetCategories([FromQuery][Required] string workspace = FileSystemConstants.DefaultWorkspace)
        {
            _logger.LogDebug($"Api => {nameof(GetCategories)} called. ", apiCall: HttpContext.Request);
            GetCategoriesQuery query = new(workSpace: workspace);
            var response = await _mediator.SendQueryAsync(query);
            if (response.IsSuccess)
            {
                _logger.LogDebug($"Api => {nameof(GetCategories)} completed. ",
                    apiCall: HttpContext.Request,
                    request: query,
                    response: response);
                return Ok(response);
            }
            _logger.LogError($"Api => {nameof(GetCategories)} encountered an error. ",
                apiCall: HttpContext.Request,
                request: query,
                response: response);
            return BadRequest(response.Errors);
        }

        /// <summary>
        /// Get credential category types by category Id.
        /// Api pattern : https:localhost:port/api/vaultx/resources/category/1234/types
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="workspace"></param>
        /// <returns></returns>
        [HttpGet("category/{categoryId}/types")]
        [ProducesResponseType(typeof(AppResponse<IList<TypesResponseDTO>>), 200)]
        [ProducesResponseType(typeof(IList<AppErrors>), 400)]
        [ProducesResponseType(typeof(IList<AppErrors>), 404)]
        [ProducesResponseType(typeof(IList<AppErrors>), 500)]
        public async Task<IActionResult> GetCredentialTypes(
            [FromRoute]  Guid categoryId,
            [FromQuery][Required] string workspace = FileSystemConstants.DefaultWorkspace)
        {
            _logger.LogDebug($"Api => {nameof(GetCredentialTypes)} called. ", apiCall: HttpContext.Request, request: categoryId);
            GetTypesQuery query = new(categoryId: categoryId, workSpace: workspace);
            var response = await _mediator.SendQueryAsync(query);
            if (response.IsSuccess)
            {
                _logger.LogDebug($"Api => {nameof(GetCredentialTypes)} completed. ",
                    apiCall: HttpContext.Request,
                    request: query,
                    response: response);
                return Ok(response);
            }
            _logger.LogError($"Api => {nameof(GetCredentialTypes)} encountered an error. ",
                apiCall: HttpContext.Request,
                request: query,
                response: response);
            return BadRequest(response.Errors);
        }
    }
}