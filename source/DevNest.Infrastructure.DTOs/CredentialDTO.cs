namespace DevNest.Infrastructure.DTOs
{
    /// <summary>
    /// Represents the DTO class instance for credentials.
    /// </summary>
    public class CredentialDTO
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
