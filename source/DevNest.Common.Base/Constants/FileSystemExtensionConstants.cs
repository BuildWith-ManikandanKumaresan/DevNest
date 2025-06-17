namespace DevNest.Common.Base.Constants
{
    /// <summary>
    /// Represents the class instance that contains the constants related to file system extensions used in the application.
    /// </summary>
    public partial class FileSystemExtensionConstants
    {
        // File extensions for various file types.

        public const string Extension_Data = ".dndat";
        public const string Extension_Configuration = ".dncfg";
        public const string Extension_Content = ".dncont";
        public const string Extension_Dashboard = ".dndash";

        public const string SuccessContentFileExtension = $".success{Extension_Content}";
        public const string ErrorContentFileExtension = $".errors{Extension_Content}";
        public const string WarningContentFileExtension = $".warnings{Extension_Content}";

        // File search patterns for various file types.

        public const string DevNestDataFileSearchPattern = $"*{Extension_Data}";
        public const string SuccessContentFileSearchPattern = $"*success{Extension_Content}";
        public const string ErrorContentFileSearchPattern = $"*errors{Extension_Content}";
        public const string WarningContentFileSearchPattern = $"*warnings{Extension_Content}";
        public const string DashboardFileSearchPattern = $"*{Extension_Dashboard}";

        // Log file constants.

        public const string LogFileNameWithExtension = $".log";
    }
}
