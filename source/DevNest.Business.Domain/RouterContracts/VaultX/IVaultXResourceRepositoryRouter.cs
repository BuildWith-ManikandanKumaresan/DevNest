#region using directives
using DevNest.Common.Base.Contracts;
using DevNest.Infrastructure.Entity.VaultX;
#endregion using directives

namespace DevNest.Business.Domain.RouterContracts.VaultX
{
    /// <summary>
    /// Represents the interface instance for Credential manager repository.
    /// </summary>
    public interface IVaultXResourceRepositoryRouter : IRepositoryRouter
    {
        /// <summary>
        /// Handler method to get the credential categories for a specific workspace.
        /// </summary>
        /// <param name="workspace"></param>
        /// <returns></returns>
        Task<IList<CategoryEntityModel>?> GetCategoriesAsync(string workspace);

        /// <summary>
        /// Handler method to get the credential category types for a specific workspace.
        /// </summary>
        /// <param name="workspace"></param>
        /// <returns></returns>
        Task<IList<TypesEntityModel>?> GetTypesAsync(Guid categoryId, string workspace);
    }
}