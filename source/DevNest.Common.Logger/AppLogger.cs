#region user directives
using DevNest.Common.Base.Contracts;
using DevNest.Common.Base.Entity;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog.Core;
using System.Text;
#endregion user directives

namespace DevNest.Common.Logger
{
    /// <summary>
    /// Reprents the class instance for <see cref="ApplicationLogger{T}">class./>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApplicationLogger<T> : IApplicationLogger<T>
    {
        private readonly Serilog.ILogger _logger;
        private readonly IApplicationConfigService<LoggerConfigEntity> _configurationService;

        /// <summary>
        /// Constructor intialization for standard logger of T type.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="configuration"></param>
        public ApplicationLogger(
            Serilog.ILogger logger, 
            IApplicationConfigService<LoggerConfigEntity> configuration)
        {
            this._logger = logger;
            this._configurationService = configuration;
        }

        /// <summary>
        /// Handle the debugging logs and use serilog log to log the message in file.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="request"></param>
        /// <param name="response"></param>
        public void LogDebug(string message, object? request = null, object? response = null, object? apiCall = null) =>
            Log(Serilog.Events.LogEventLevel.Debug, message, null, request, response, apiCall);

        /// <summary>
        /// Handle the error logs and use serilog log to log the message in file.
        /// </summary>
        /// <param name="message"></param>
        public void LogError(string message, object? apiCall = null) =>
        _logger.Error(message, apiCall);

        /// <summary>
        /// Handle the debugging logs and use serilog log to log the message in file.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="request"></param>
        /// <param name="response"></param>
        public void LogError(string message, object? request = null, object? response = null, object? apiCall = null) =>
        Log(Serilog.Events.LogEventLevel.Error, message, null, request, response, apiCall);

        /// <summary>
        /// Handle the error logs and use serilog log to log the message in file.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="exception"></param>
        public void LogError(string message, Exception exception, object? request = null, object? response = null, object? apiCall = null) =>
        Log(Serilog.Events.LogEventLevel.Error, message, exception, request, response,apiCall);

        /// <summary>
        /// Handle the fatal logs and use serilog log to log the message in file.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="request"></param>
        /// <param name="response"></param>
        public void LogFatal(string message, object? request = null, object? response = null, object? apiCall = null) =>
        Log(Serilog.Events.LogEventLevel.Fatal, message, null, request, response, apiCall);

        /// <summary>
        /// Handle the information logs and use serilog log to log the message in file.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="request"></param>
        /// <param name="response"></param>
        public void LogInfo(string message, object? request = null, object? response = null, object? apiCall = null) =>
        Log(Serilog.Events.LogEventLevel.Information, message, null, request, response, apiCall);

        /// <summary>
        /// Handle the warning logs and use serilog log to log the message in file.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="warnings"></param>
        public void LogWarning(string message, object? warnings = null, object? apiCall = null) =>
        Log(Serilog.Events.LogEventLevel.Warning, message, null, warnings, null, apiCall);

        /// <summary>
        /// Handles the logging method logic to add the logs based on the log levels,
        /// such as debug, information, warning, errors and fatal.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <param name="request"></param>
        /// <param name="response"></param>
        private void Log(Serilog.Events.LogEventLevel level, string message, Exception? ex = null, object? request = null, object? response = null, object? apiCall = null)
        {
            if (!_configurationService.Value?.Logging ?? false || !_logger.IsEnabled(level)) return;

            var logBuilder = new StringBuilder()
                .AppendLine($"Message: {message}");

            if (apiCall is not null)
            {
                var httpContext = apiCall as HttpRequest;

                var fullUrl = $"{httpContext?.Scheme}://{httpContext?.Host}{httpContext?.Path}{httpContext?.QueryString}";
                
                logBuilder.AppendLine($"Api-Call: {JsonConvert.SerializeObject(fullUrl, Formatting.Indented)}");
                
                if(!httpContext?.Method.Equals("GET") ?? false)
                {
                    logBuilder.AppendLine($"Request Body: {JsonConvert.SerializeObject(httpContext.Body)}");
                }
            }

            if (request is not null)
                logBuilder.AppendLine($"Request: {JsonConvert.SerializeObject(request)}");

            if (response is not null)
                logBuilder.AppendLine($"Response: {JsonConvert.SerializeObject(response)}");

            if (ex != null)
                _logger.Write(level, ex, logBuilder.ToString());
            else
                _logger.Write(level, logBuilder.ToString());
        }
    }
}