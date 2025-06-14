﻿#region using directives
using DevNest.Common.Base.MediatR.Contracts;
using DevNest.Common.Base.Response;
#endregion using directives

namespace DevNest.Application.Commands.Credentials
{
    /// <summary>
    /// Command to archive a credential.
    /// </summary>
    public class ArchiveCredentialCommand : ICommand<AppResponse<bool>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArchiveCredentialCommand"/> class.
        /// </summary>
        /// <param name="id">The identifier of the credential to be archived.</param>
        public ArchiveCredentialCommand(Guid id)
        {
            this.CredentialId = id;
        }

        /// <summary>
        /// Gets or sets the identifier of the credential to be archived.
        /// </summary>
        public Guid CredentialId { get; set; }

    }
}