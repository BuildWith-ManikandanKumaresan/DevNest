#region using directives
using MediatR;
#endregion using directives

namespace DevNest.Common.Base.MediatR
{
    /// <summary>
    /// Base interface for all requests that can be sent to the MediatR pipeline.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="ApplicationMedidator"/> class with the specified MediatR mediator.
    /// </remarks>
    /// <param name="mediator"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public class ApplicationMedidator(IMediator mediator) : IApplicationMedidator
    {
        private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator), "Mediator cannot be null");

        /// <summary>
        /// Sends a query to the MediatR pipeline and returns a response.
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<TResponse> SendQueryAsync<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default)
        {
            return await _mediator.Send(query, cancellationToken);
        }

        /// <summary>
        /// Sends a command to the MediatR pipeline and returns a response.
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<TResponse> SendCommandAsync<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        /// <summary>
        /// Publishes an event to the MediatR pipeline.
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="event"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task PublishEvent<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : INotification
        {
            return _mediator.Publish(@event, cancellationToken);
        }
    }
}