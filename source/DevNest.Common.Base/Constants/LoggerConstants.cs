namespace DevNest.Common.Base.Constants
{
    /// <summary>
    /// Represents the class instance that contains the constants related to logger configurations.
    /// </summary>
    public partial class LoggerConstants
    {
        public const string RollingInterval_Hour = "hour";
        public const string RollingInterval_Minute = "minute";
        public const string RollingInterval_Infinite = "infinite";
        public const string RollingInterval_Year = "year";
        public const string RollingInterval_Day = "day";

        public const string LogLevel_Debug = "debug";
        public const string LogLevel_Info = "info";
        public const string LogLevel_Warning = "warning";
        public const string LogLevel_Fatal = "fatal";
        public const string LogLevel_Verbose = "verbose";

        public const string DefaultLoggingDirectory = "logs";
        public const string LogFileNameWithExtension = $".log";

        public const string Default_LoggerDirectory = "logs/";
        public const string Default_FileRollingInterval = "Day";
        public const long Default_FileSizeLimits = 5242880;
        public const string Default_OutputTemplate = "|{Timestamp:yyyy-MM-dd HH:mm:ss.fff}|{Level:u3}|> {Message:lj}{NewLine}{Exception}";
        public const string Default_MinimumLogLevel = "Info";
    }
}
