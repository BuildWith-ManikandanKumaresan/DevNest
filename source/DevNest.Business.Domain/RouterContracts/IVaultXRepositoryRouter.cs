#region using directives
using DevNest.Common.Base.Contracts;
using DevNest.Infrastructure.Entity.VaultX;
#endregion using directives

namespace DevNest.Business.Domain.RouterContracts
{
    /// <summary>
    /// Represents the interface instance for Credential manager repository.
    /// </summary>
    public interface IVaultXRepositoryRouter : IRepositoryRouter
    {
        /// <summary>
        /// Handler method to get the credentials entity from the plugin repository.
        /// </summary>
        /// <returns></returns>
        Task<IList<CredentialEntityModel>?> GetAsync(string workspace);

        /// <summary>
        /// Handler method to get the credential entity by its unique identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CredentialEntityModel?> GetByIdAsync(Guid id,string workspace);

        /// <summary>
        /// Handler method to delete the credentials.
        /// </summary>
        /// <returns></returns>
        Task<bool> DeleteAsync(string workspace);

        /// <summary>
        /// handler method to delete the credential using id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteByIdAsync(Guid id, string workspace);

        /// <summary>
        /// Handler method to add the credentials.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<CredentialEntityModel?> AddAsync(CredentialEntityModel entity, string workspace);

        /// <summary>
        /// Handler method to update the credentials entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<CredentialEntityModel?> UpdateAsync(CredentialEntityModel entity, string workspace);

        /// <summary>
        /// Handler method to archive the credential by its unique identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> ArchiveByIdAsync(Guid id, string workspace);

        /// <summary>
        /// Handler method to encrypt the credential by its unique identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CredentialEntityModel?> EncryptByIdAsync(Guid id, string workspace);

        /// <summary>
        /// Handler method to decrypt the credential by its unique identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CredentialEntityModel?> DecryptByIdAsync(Guid id, string workspace);

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