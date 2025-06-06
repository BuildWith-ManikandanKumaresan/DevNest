#region using directives
using DevNest.Infrastructure.Entity;
using DevNest.Common.Base.Contracts;
#endregion using directives

namespace DevNest.Business.Domain.RouterContracts
{
    /// <summary>
    /// Represents the interface instance for Credential manager repository.
    /// </summary>
    public interface ICredentialManagerReposRouter : IReposRouter
    {
        /// <summary>
        /// Handler method to get the credentials entity from the plugin repository.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<CredentialEntity>?> GetAsync();

        /// <summary>
        /// Handler method to get the credential entity by its unique identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CredentialEntity?> GetByIdAsync(Guid id);

        /// <summary>
        /// Handler method to delete the credentials.
        /// </summary>
        /// <returns></returns>
        Task<bool> DeleteAsync();

        /// <summary>
        /// handler method to delete the credential using id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteByIdAsync(Guid id);

        /// <summary>
        /// Handler method to add the credentials.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<CredentialEntity> AddAsync(CredentialEntity entity);
    }
}