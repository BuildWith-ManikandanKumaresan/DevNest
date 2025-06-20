namespace DevNest.Common.Base.Constants
{
    /// <summary>
    /// Represents the class instance that contains the constants related to file search patterns used in the application.
    /// </summary>
    public partial class FileSearchPatternConstants
    {
        // File search patterns for various file types.

        public const string Extension_Data = $"*.dndat";
        public const string Extension_Preferences = $"*.dncfg";
        public const string Extension_Resources = $"*.dncont";
        public const string Extension_Dashboard = $"*.dndash";

        public const string SuccessContent = $"*.success.dncont";
        public const string ErrorContent = $"*.errors.dncont";
        public const string WarningContent = $"*.warnings.dncont";

        // File search patterns.
        public const string DevNestAssembly = "DevNest.";
        public const string DevNest_Plugins = "DevNest.*.dll";
        public const string DevNest_Logs = $"*.log";

    }
}
