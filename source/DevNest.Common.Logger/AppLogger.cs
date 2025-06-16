#region user directives
using DevNest.Common.Base.Constants;
using DevNest.Common.Base.Contracts;
using DevNest.Common.Base.Entity;
using DevNest.Common.Base.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog.Core;
using System;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;
#endregion user directives

namespace DevNest.Common.Logger
{
    /// <summary>
    /// Reprents the class instance for <see cref="AppLogger{T}">class./>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks>
    /// Constructor intialization for standard logger of T type.
    /// </remarks>
    /// <param name="logger"></param>
    /// <param name="configuration"></param>
    public class AppLogger<T>(
        Serilog.ILogger logger,
        IOptions<LoggerConfigEntityModel> configuration) : IAppLogger<T>
    {
        private readonly Serilog.ILogger _logger = logger;
        private readonly IOptions<LoggerConfigEntityModel> _configurationService = configuration;

        /// <summary>
        /// Handler method for logging debug logs.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="apiCall"></param>
        public void LogDebug(string message, object? request = null, object? response = null, object? apiCall = null) =>
            Log(level: Serilog.Events.LogEventLevel.Debug, message: message, request: request, response: response, apiCall: apiCall);

        /// <summary>
        /// Handler method for logging error logs with additional errors information.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="errors"></param>
        /// <param name="apiCall"></param>
        public void LogError(string message, Exception? exception = null, object? request = null, object? response = null, IList<AppErrors>? errors = null, object? apiCall = null) =>
            Log(level: Serilog.Events.LogEventLevel.Error, message: message,ex:exception, request: request, response: response, errors: errors, apiCall: apiCall);

        /// <summary>
        /// Handler method for logging fatal logs with additional errors information.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="apiCall"></param>
        public void LogFatal(string message, object? request = null, object? response = null, object? apiCall = null) =>
            Log(level: Serilog.Events.LogEventLevel.Fatal, message: message, request: request, response: response, apiCall: apiCall);

        /// <summary>
        /// Handler method for logging info logs with additional information such as request, response and api call details.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="apiCall"></param>
        public void LogInfo(string message, object? request = null, object? response = null, object? apiCall = null) =>
            Log(level: Serilog.Events.LogEventLevel.Information, message: message, request: request, response: response, apiCall: apiCall);

        /// <summary>
        /// Handler method for logging warning logs with additional information such as request, response, warnings and api call details.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="warnings"></param>
        /// <param name="apiCall"></param>
        public void LogWarning(string message, object? request = null, object? response = null, IList<AppWarnings>? warnings = null, object? apiCall = null) =>
            Log(level: Serilog.Events.LogEventLevel.Warning, message: message, request: request, response: response, warnings: warnings, apiCall: apiCall);


        /// <summary>
        /// Handles the logging method logic to add the logs based on the log levels,
        /// such as debug, information, warning, errors and fatal.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <param name="request"></param>
        /// <param name="response"></param>
        private void Log(
            Serilog.Events.LogEventLevel level,
            string message,
            Exception? ex = null,
            object? request = null,
            object? response = null,
            object? apiCall = null,
            IList<AppErrors>? errors = null,
            IList<AppWarnings>? warnings = null)
        {
            if (!_configurationService.Value?.Logging ?? false || !_logger.IsEnabled(level)) return;

            var logBuilder = new StringBuilder()
                .AppendLine($"{LoggerConstants.MessageTemplate}{message}");

            if (apiCall is not null)
            {
                var httpContext = apiCall as HttpRequest;

                var fullUrl = $"{httpContext?.Scheme}://{httpContext?.Host}{httpContext?.Path}{httpContext?.QueryString}";
                
                logBuilder.AppendLine($"{LoggerConstants.ApiCallTemplate}{JsonConvert.SerializeObject(fullUrl, Formatting.Indented)}");                
            }

            if (request is not null)
                logBuilder.AppendLine($"{LoggerConstants.RequestTemplate}{JsonConvert.SerializeObject(request)}");

            if (response is not null)
                logBuilder.AppendLine($"{LoggerConstants.ResponseTemplate}{JsonConvert.SerializeObject(response)}");

            if (errors is not null && errors.Any())
                logBuilder.AppendLine($"{LoggerConstants.ErrorsTemplate}{JsonConvert.SerializeObject(errors, Formatting.Indented)}");

            if (warnings is not null && warnings.Any())
                logBuilder.AppendLine($"{LoggerConstants.WarningsTemplate}{JsonConvert.SerializeObject(warnings, Formatting.Indented)}");

            if (ex != null)
                _logger.Write(level, ex, logBuilder.ToString());
            else
                _logger.Write(level, logBuilder.ToString());
        }
    }
}