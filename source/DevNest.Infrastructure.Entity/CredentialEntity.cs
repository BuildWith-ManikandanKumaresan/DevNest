namespace DevNest.Infrastructure.Entity
{
    /// <summary>
    /// Represent the entity class instance for Credentials.
    /// </summary>
    public class CredentialEntity
    {
        /// <summary>
        /// Gets or sets the credential id.
        /// </summary>
        public Guid CredentialId { get; set; }

        /// <summary>
        /// Gets or sets the credential name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string? UserName { get; set; }
    }
}
