﻿#region using directives
using DevNest.Common.Base.Constants;
using DevNest.Common.Base.Constants.Message;
using DevNest.Common.Base.Contracts;
using DevNest.Common.Base.Entity;
using Microsoft.Extensions.Options;
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
    /// <remarks>
    /// Initialize the constructor instance for logging manager.
    /// </remarks>
    /// <param name="config"></param>
    public class LoggingManager(IOptions<LoggerConfigEntityModel> config)
    {
        private readonly IOptions<LoggerConfigEntityModel> _config = config;

        /// <summary>
        /// Handler method to initialize the logger based on each service instance.
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public Serilog.Core.Logger Initialize(string serviceName)
        {
            var config = _config ?? throw new InvalidOperationException(Messages.GetError(ErrorConstants.LoggerConfigurationMissing).Message);
            var logDir = Path.Combine(
                Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), FileSystemConstants.DirectoryUp)),
                config.Value.LoggerDirectory ?? FileSystemConstants.DefaultLoggingDirectory,
                serviceName.ToLower());

            Directory.CreateDirectory(logDir); // Ensure log directory exists

            string logPath = Path.Combine(logDir, 
                $"{serviceName.ToLower()}-services-{FileSystemConstants.LogFileNameWithExtension}");

            var loggerConfig = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(
                    path: logPath,
                    rollingInterval: GetRollingInterval(config.Value.FileRollingInterval),
                    fileSizeLimitBytes: config.Value.FileSizeLimits,
                    rollOnFileSizeLimit: true,
                    outputTemplate: config.Value.OutputTemplate ?? string.Empty)
                .MinimumLevel.Is(GetLogLevel(config.Value.MinimumLogLevel));

            return loggerConfig.CreateLogger();
        }

        /// <summary>
        /// Handle the get rolling interval for logging.
        /// </summary>
        /// <returns></returns>
        private static RollingInterval GetRollingInterval(string? interval) => interval?.ToLower() switch
        {
            LoggerConstants.RollingInterval_Hour => RollingInterval.Hour,
            LoggerConstants.RollingInterval_Minute => RollingInterval.Minute,
            LoggerConstants.RollingInterval_Infinite => RollingInterval.Infinite,
            LoggerConstants.RollingInterval_Year => RollingInterval.Year,
            _ => RollingInterval.Day
        };

        /// <summary>
        /// handle to sets the minimum logging levels.
        /// </summary>
        private static LogEventLevel GetLogLevel(string? level) => level?.ToLower() switch
        {
            LoggerConstants.LogLevel_Debug => LogEventLevel.Debug,
            LoggerConstants.LogLevel_Info => LogEventLevel.Information,
            LoggerConstants.LogLevel_Warning => LogEventLevel.Warning,
            LoggerConstants.LogLevel_Fatal => LogEventLevel.Fatal,
            _ => LogEventLevel.Verbose
        };
    }
}