#region user directives
#endregion user directives

namespace DevNest.Common.Logger
{
    /// <summary>
    /// Represents the interface for <see cref="IApplicationLogger{T}"> class./>.
    /// </summary>
    public interface IApplicationLogger<T>
    {
        /// <summary>
        /// Handler method for logging debug logs.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="apiCall"></param>
        void LogDebug(string message, object? request = null, object? response = null, object? apiCall = null);

        /// <summary>
        /// Handler method for logging info logs.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="apiCall"></param>
        void LogInfo(string message, object? request = null, object? response = null, object? apiCall = null);

        /// <summary>
        /// Handler method for logging warning logs.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="warnings"></param>
        /// <param name="apiCall"></param>
        void LogWarning(string message, object? warnings = null, object? apiCall = null);

        /// <summary>
        /// Handler method for logging error logs.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="apiCall"></param>
        void LogError(string message, object? apiCall = null);

        /// <summary>
        /// Handler method for logging error logs.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="apiCall"></param>
        void LogError(string message, object? request = null, object? response = null, object? apiCall = null);

        /// <summary>
        /// Handler method for logging error logs.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="apiCall"></param>
        void LogError(string message, Exception exception, object? request = null, object? response = null, object? apiCall = null);

        /// <summary>
        /// Handler method for logging fatal logs.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="apiCall"></param>
        void LogFatal(string message, object? request = null, object? response = null, object? apiCall = null);
    }
}