#region using directives
using DevNest.Common.Base.Response;
#endregion using directives

namespace DevNest.Common.Base.MediatR
{
    /// <summary>
    /// Base class for queries that implements the IQuery interface.
    /// </summary>
    public class QueryBase
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