#region using directives
using MediatR;
#endregion using directives

namespace DevNest.Common.Base.MediatR.Contracts
{
    /// <summary>
    /// Represents a handler for a query that can be sent to the MediatR pipeline.
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public interface IQueryHandler<TQuery, TResult> : IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        /// <summary>
        /// Handles the specified query and returns a result.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        new Task<TResult> Handle(TQuery query, CancellationToken cancellationToken = default);
    }
}