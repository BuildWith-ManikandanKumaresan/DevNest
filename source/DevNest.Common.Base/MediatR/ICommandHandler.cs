#region using directives
using MediatR;
#endregion using directives

namespace DevNest.Common.Base.MediatR
{
    /// <summary>
    /// Represents a command that can be sent to the MediatR pipeline.
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public interface ICommandHandler<TCommand, TResult> : IRequestHandler<TCommand, TResult> where TCommand : ICommand<TResult>
    {
        /// <summary>
        /// Handles the command and returns a result.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        new Task<TResult> Handle(TCommand command, CancellationToken cancellationToken = default);
    }
}