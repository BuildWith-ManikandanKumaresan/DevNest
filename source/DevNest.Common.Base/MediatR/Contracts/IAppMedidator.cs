#region using directives
using MediatR;
#endregion using directives

namespace DevNest.Common.Base.MediatR.Contracts
{
    /// <summary>
    /// Represents a command that can be sent to the MediatR pipeline.
    /// </summary>
    public interface IAppMedidator
    {
        /// <summary>
        /// Publishes an event to the MediatR pipeline.
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="event"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task PublishEvent<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : INotification;

        /// <summary>
        /// Sends a command to the MediatR pipeline and returns a response.
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TResponse> SendCommandAsync<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends a query to the MediatR pipeline and returns a response.
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TResponse> SendQueryAsync<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default);
    }
}