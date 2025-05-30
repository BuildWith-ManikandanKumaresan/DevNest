#region using directives
using MediatR;
#endregion using directives

namespace DevNest.Common.Base.Request
{
    /// <summary>
    /// Represents the class instance for AppRequest of type T.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApplicationRequest<T> : IRequest<T>
    {
        /// <summary>
        /// Gets or sets the data of type T.
        /// </summary>
        public T? Data { get; set; }
    }
}