namespace DevNest.Common.Base.Constants
{
    /// <summary>
    /// Represents the class instance that contains the constants related to logger configurations.
    /// </summary>
    public class LoggerConstants
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
    }

    /// <summary>
    /// Represents the class instance that contains the common constants used across the application.
    /// </summary>
    public class CommonConstants
    {
        public const string AssemblySearchPattern = "DevNest.";
    }
}
