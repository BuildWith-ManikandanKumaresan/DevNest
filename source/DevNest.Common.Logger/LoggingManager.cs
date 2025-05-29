#region using directives
using DevNest.Common.Base.Contracts;
using DevNest.Common.Logger.Model;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion using directives

namespace DevNest.Common.Logger
{
    /// <summary>
    /// Represents the class instance for <see cref="LoggingManager"/> class./>
    /// </summary>
    public class LoggingManager
    {
        private readonly IAppConfigService<LoggerConfig> _config;
        private const string _LoggerConfigurationsMissing = "Logger configuration is missing.";
        private const string _UpDirectory = "..";
        private const string _DefaultLoggingDirectory = "logs";
        private static readonly string _LogFileNameWithExtension = $".log";

        private const string _RollingInterval_Hour = "hour";
        private const string _RollingInterval_Minute = "minute";
        private const string _RollingInterval_Infinite = "infinite";
        private const string _RollingInterval_Year = "year";
        private const string _RollingInterval_Day = "day";

        private const string _LogLevel_Debug = "debug";
        private const string _LogLevel_Info = "info";
        private const string _LogLevel_Warning = "warning";
        private const string _LogLevel_Fatal = "fatal";
        private const string _LogLevel_Verbose = "verbose";

        /// <summary>
        /// Initialize the constructor instance for logging manager.
        /// </summary>
        /// <param name="config"></param>
        public LoggingManager(IAppConfigService<LoggerConfig> config)
        {
            _config = config;
        }

        /// <summary>
        /// Handler method to initialize the logger based on each service instance.
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public Serilog.Core.Logger Initialize(string serviceName)
        {
            var config = _config.Value ?? throw new InvalidOperationException(_LoggerConfigurationsMissing);
            var logDir = Path.Combine(
                Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), _UpDirectory)),
                config.LoggerDirectory ?? _DefaultLoggingDirectory,
                serviceName.ToLower());

            Directory.CreateDirectory(logDir); // Ensure log directory exists

            string logPath = Path.Combine(logDir, 
                $"{serviceName.ToLower()}.{_LogFileNameWithExtension}");

            var loggerConfig = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(
                    path: logPath,
                    rollingInterval: GetRollingInterval(config.FileRollingInterval),
                    fileSizeLimitBytes: config.FileSizeLimits,
                    rollOnFileSizeLimit: true,
                    outputTemplate: config.OutputTemplate ?? string.Empty)
                .MinimumLevel.Is(GetLogLevel(config.MinimumLogLevel));

            return loggerConfig.CreateLogger();
        }

        /// <summary>
        /// Handle the get rolling interval for logging.
        /// </summary>
        /// <returns></returns>
        private RollingInterval GetRollingInterval(string? interval) => interval?.ToLower() switch
        {
            _RollingInterval_Hour => RollingInterval.Hour,
            _RollingInterval_Minute => RollingInterval.Minute,
            _RollingInterval_Infinite => RollingInterval.Infinite,
            _RollingInterval_Year => RollingInterval.Year,
            _ => RollingInterval.Day
        };

        /// <summary>
        /// handle to sets the minimum logging levels.
        /// </summary>
        private LogEventLevel GetLogLevel(string? level) => level?.ToLower() switch
        {
            _LogLevel_Debug => LogEventLevel.Debug,
            _LogLevel_Info => LogEventLevel.Information,
            _LogLevel_Warning => LogEventLevel.Warning,
            _LogLevel_Fatal => LogEventLevel.Fatal,
            _ => LogEventLevel.Verbose
        };
    }
}