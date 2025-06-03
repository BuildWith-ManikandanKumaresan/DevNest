using DevNest.Common.Base.Constants;

namespace DevNest.Common.Base.Entity
{
    /// <summary>
    /// Represents the class instance for standard logger.
    /// </summary>
    public class LoggerConfigEntity
    {
        /// <summary>
        /// Gets or set the logging enable.
        /// Default value is 'true'.
        /// </summary>
        public bool Logging { get; set; } = true;

        /// <summary>
        /// Gets or set the logger base directory.
        /// Default value is 'logs/'
        /// </summary>
        public string LoggerDirectory { get; set; } = LoggerConstants.Default_LoggerDirectory;

        /// <summary>
        /// Gets or sets the value for file rolling interval.
        /// Default value is 'Day'.
        /// </summary>
        public string? FileRollingInterval { get; set; } = LoggerConstants.Default_FileRollingInterval;

        /// <summary>
        /// Gets or sets the File Size limits for rolled log for file.
        /// Default value is '5242880'
        /// </summary>
        public long FileSizeLimits { get; set; } = LoggerConstants.Default_FileSizeLimits;

        /// <summary>
        /// Gets or sets the output template for file logging.
        /// Default value is '|{Timestamp:yyyy-MM-dd HH:mm:ss.fff}|{Level:u3}|> {Message:lj}{NewLine}{Exception}'.
        /// </summary>
        public string? OutputTemplate { get; set; } = LoggerConstants.Default_OutputTemplate;

        /// <summary>
        /// Gets or sets the minimum level for logger configurations
        /// Default value is 'Debug'.
        /// </summary>
        public string? MinimumLogLevel { get; set; } = LoggerConstants.Default_MinimumLogLevel;
    }
}