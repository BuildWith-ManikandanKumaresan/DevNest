#region using directives
using DevNest.Common.Base.Contracts;
using DevNest.Common.Base.Response;
using DevNest.Infrastructure.DTOs.VaultX.Request;
using DevNest.Infrastructure.DTOs.VaultX.Response;
using DevNest.Infrastructure.DTOs.VaultX.Request;
using DevNest.Infrastructure.Entity.Search;
using System.ComponentModel.DataAnnotations;
#endregion using directives

namespace DevNest.Business.Domain.Domains.VaultX.Contracts
{
    /// <summary>
    /// Represents the interface instance for credential manager services.
    /// </summary>
    public interface IVaultXResourceDomainService : IDomainService
    {
        /// <summary>
        /// Handler method interface for getting credential category types without category ID.
        /// </summary>
        /// <param name="workspace"></param>
        /// <returns></returns>
        Task<AppResponse<IList<CategoryResponseDTO>?>> GetCategories(string workspace);

        /// <summary>
        /// Handler method interface for getting credential category types.
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="workspace"></param>
        /// <returns></returns>
        Task<AppResponse<IList<TypesResponseDTO>?>> GetTypes(Guid categoryId, string workspace);
    }
}