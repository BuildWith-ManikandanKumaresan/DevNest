#region using directives
using DevNest.Common.Base.Response;
#endregion using directives

namespace DevNest.Common.Base.MediatR
{
    /// <summary>
    /// Base class for commands that implements the ICommand interface.
    /// </summary>
    public class CommandBase
    {
        /// <summary>
        /// Validates the query and returns a list of errors if any.
        /// </summary>
        /// <returns></returns>
        public virtual IList<AppErrors> Validate()
        {
            return [];
        }
    }
}
