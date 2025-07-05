#region using directives
using AutoMapper;
using DevNest.Business.Domain.RouterContracts;
using DevNest.Common.Base.Constants;
using DevNest.Common.Base.Constants.Message;
using DevNest.Common.Base.Contracts;
using DevNest.Common.Base.Entity;
using DevNest.Common.Base.Response;
using DevNest.Common.Logger;
using DevNest.Infrastructure.DTOs.VaultX.Request;
using DevNest.Infrastructure.DTOs.VaultX.Response;
using DevNest.Infrastructure.DTOs.VaultX.Request;
using DevNest.Infrastructure.Entity.Search;
using MediatR;
using System.Linq.Expressions;
using System.Net;
using DevNest.Infrastructure.Entity.Configurations.VaultX;
using DevNest.Infrastructure.Entity.VaultX;
using DevNest.Business.Domain.Domains.VaultX.Contracts;
using DevNest.Business.Domain.RouterContracts.VaultX;
#endregion using directives

namespace DevNest.Business.Domain.Domains.VaultX
{
    /// <summary>
    /// Represents the class instance for credential manager services.
    /// </summary>
    /// <remarks>
    /// Initialize the new instance for credential manager services.
    /// </remarks>
    /// <param name="logger"></param>
    public class VaultXResourceDomainService(
        IAppLogger<VaultXResourceDomainService> logger,
        IVaultXResourceRepositoryRouter router,
        IMapper mapper) : IVaultXResourceDomainService
    {
        private readonly IAppLogger<VaultXResourceDomainService> _logger = logger;
        private readonly IVaultXResourceRepositoryRouter _router = router;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Method to get credential categories based on the workspace.
        /// </summary>
        /// <param name="workSpace"></param>
        /// <returns></returns>
        public async Task<AppResponse<IList<CategoryResponseDTO>?>> GetCategories(string workspace)
        {
            try
            {
                _logger.LogDebug($"{nameof(VaultXResourceDomainService)} => {nameof(GetCategories)} method called with workspace: {workspace}.");
                var entity = await _router.GetCategoriesAsync(workspace);
                if (entity == null)
                {
                    _logger.LogDebug($"{nameof(VaultXResourceDomainService)} => {nameof(GetCategories)} method returned null for workspace: {workspace}.");
                    return new AppResponse<IList<CategoryResponseDTO>?>(Messages.GetError(ErrorConstants.CredentialsCategoriesNotFound));
                }

                _logger.LogDebug($"{nameof(VaultXResourceDomainService)} => {nameof(GetCategories)} method returned categories for workspace: {workspace}.");

                return new AppResponse<IList<CategoryResponseDTO>?>(_mapper.Map<IList<CategoryResponseDTO>>(entity));
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(VaultXResourceDomainService)} => {ex.Message}", ex);
                return new AppResponse<IList<CategoryResponseDTO>?>(new AppErrors { Code = ErrorConstants.UndefinedErrorCode, Message = ex.Message });
            }
        }

        /// <summary>
        /// Method to get credential category types based on the category ID and workspace.
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="workSpace"></param>
        /// <returns></returns>
        public async Task<AppResponse<IList<TypesResponseDTO>?>> GetTypes(Guid categoryId, string workspace)
        {
            try
            {
                _logger.LogDebug($"{nameof(VaultXResourceDomainService)} => {nameof(GetTypes)} method called with id: {categoryId}.");
                var entity = await _router.GetTypesAsync(categoryId, workspace);
                if (entity == null)
                {
                    _logger.LogDebug($"{nameof(VaultXResourceDomainService)} => {nameof(GetTypes)} method returned null for id: {categoryId}.");
                    return new AppResponse<IList<TypesResponseDTO>?>(Messages.GetError(ErrorConstants.CredentialsCategoryTypesNotFound));
                }

                _logger.LogDebug($"{nameof(VaultXResourceDomainService)} => {nameof(GetTypes)} method returned categories for id: {categoryId}.");

                return new AppResponse<IList<TypesResponseDTO>?>(_mapper.Map<IList<TypesResponseDTO>?>(entity));
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(VaultXResourceDomainService)} => {ex.Message}", ex);
                return new AppResponse<IList<TypesResponseDTO>?>(new AppErrors { Code = ErrorConstants.UndefinedErrorCode, Message = ex.Message });
            }
        }
    }
}