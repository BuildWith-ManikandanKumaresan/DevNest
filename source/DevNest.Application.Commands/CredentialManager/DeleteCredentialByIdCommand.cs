#region using directives
using DevNest.Common.Base.MediatR;
using DevNest.Common.Base.Response;
using MediatR;
#endregion using directives

namespace DevNest.Application.Commands.CredentialManager
{
    /// <summary>
    /// Represents the class instance for delete credentials by id command.
    /// </summary>
    public class DeleteCredentialByIdCommand : ICommand<ApplicationResponse<bool>>
    {
        /// <summary>
        /// Initialize the new instance for <see cref="DeleteCredentialByIdCommand">class.</see>/>
        /// </summary>
        /// <param name="id"></param>
        public DeleteCredentialByIdCommand(Guid id) 
        {
            this.CredentialId = id;
        }
        
        /// <summary>
        /// Gets or sets the credential id property.
        /// </summary>
        public Guid CredentialId { get; set; }
    }
}
