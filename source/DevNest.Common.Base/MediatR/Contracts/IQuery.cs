#region using directives
using MediatR;
#endregion using directives

namespace DevNest.Common.Base.MediatR.Contracts
{
    /// <summary>
    /// Represents a query that can be sent to the MediatR pipeline.
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    public interface IQuery<TResponse> : IRequest<TResponse>, IBaseRequest
    {
        //TODO: Add any additional properties or methods that are common to all queries
    }
}