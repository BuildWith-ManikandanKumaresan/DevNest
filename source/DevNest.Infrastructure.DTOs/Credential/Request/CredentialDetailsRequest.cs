#region using directives
using System.ComponentModel.DataAnnotations;
#endregion using directives

namespace DevNest.Infrastructure.DTOs.Credential.Request
{
    /// <summary>
    /// Represents the class instance for credential details request.
    /// </summary>
    public class CredentialDetailsRequest
    {
        /// <summary>
        /// Gets or sets the type of the credential (e.g., SSH, RDP, Azure).
        /// </summary>
        [Required(ErrorMessage = "Credential type is required.")]
        public string? Type { get; set; }

        /// <summary>
        /// Gets or sets the domain associated with the credential.
        /// </summary>
        public string? Domain { get; set; }

        /// <summary>
        /// Gets or sets the host or machine address.
        /// </summary>
        [Required(ErrorMessage = "Host is required.")]
        public string? Host { get; set; }

        /// <summary>
        /// Gets or sets the port number for the credential connection.
        /// </summary>
        public int? Port { get; set; }

        /// <summary>
        /// Gets or sets the username for login.
        /// </summary>
        [Required(ErrorMessage = "Username is required.")]
        public string? Username { get; set; }

        /// <summary>
        /// Gets or sets the password for login.
        /// </summary>
        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }
    }
}