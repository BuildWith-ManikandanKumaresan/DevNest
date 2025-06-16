#region user directives
using DevNest.Common.Base.Response;
#endregion user directives

namespace DevNest.Common.Logger
{
    /// <summary>
    /// Represents the interface for <see cref="IAppLogger{T}"> class./>.
    /// </summary>
    public interface IAppLogger<T>
    {
        /// <summary>
        /// Handler method for logging debug logs.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="apiCall"></param>
        void LogDebug(
            string message, 
            object? request = null, 
            object? response = null, 
            object? apiCall = null);

        /// <summary>
        /// Handler method for logging info logs.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="apiCall"></param>
        void LogInfo(
            string message, 
            object? request = null, 
            object? response = null, 
            object? apiCall = null);

        /// <summary>
        /// Handler method for logging warning logs.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="warnings"></param>
        /// <param name="apiCall"></param>
        void LogWarning(
            string message, 
            object? request = null,
            object? response = null, 
            IList<AppWarnings>? warnings = null, 
            object? apiCall = null);

        /// <summary>
        /// Handler method for logging error logs with additional errors information.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errors"></param>
        void LogError(
            string message, 
            Exception? exception = null, 
            object? request = null, 
            object? response = null, 
            IList<AppErrors>? errors = null, 
            object? apiCall = null);

        /// <summary>
        /// Handler method for logging fatal logs.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="apiCall"></param>
        void LogFatal(
            string message, 
            object? request = null, 
            object? response = null, 
            object? apiCall = null);
    }
}