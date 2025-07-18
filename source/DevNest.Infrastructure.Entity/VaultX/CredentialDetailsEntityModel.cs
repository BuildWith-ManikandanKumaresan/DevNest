﻿#region using directives
#endregion using directives

namespace DevNest.Infrastructure.Entity.VaultX
{
    /// <summary>
    /// Represents the entity class instance for Credential Details.
    /// </summary>
    public class CredentialDetailsEntityModel
    {
        /// <summary>
        /// Gets or sets the type of the credential (e.g., SSH, RDP, Azure).
        /// </summary>
        public string? Type { get; set; }

        /// <summary>
        /// Gets or sets the domain associated with the credential.
        /// </summary>
        public string? Domain { get; set; }

        /// <summary>
        /// Gets or sets the host or machine address.
        /// </summary>
        public string? Host { get; set; }

        /// <summary>
        /// Gets or sets the port number for the credential connection.
        /// </summary>
        public int? Port { get; set; }

        /// <summary>
        /// Gets or sets the username for login.
        /// </summary>
        public string? Username { get; set; }

        /// <summary>
        /// Gets or sets the password for login.
        /// </summary>
        public string? Password { get; set; }
    }
}