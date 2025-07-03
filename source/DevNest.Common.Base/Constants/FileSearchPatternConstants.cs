namespace DevNest.Common.Base.Constants
{
    /// <summary>
    /// Represents the class instance that contains the constants related to file search patterns used in the application.
    /// </summary>
    public partial class FileSearchPatternConstants
    {
        // File search patterns for various file types.

        public const string Extension_Data = $"*.dndat";
        public const string Extension_Configurations = $"*.dncfg";
        public const string Extension_Resources = $"*.dnres";
        public const string Extension_Dashboard = $"*.dndsh";

        // File search patterns.
        public const string DevNestAssembly = "DevNest.";
        public const string DevNest_Plugins = "DevNest.*.dll";
        public const string DevNest_Logs = $"*.log";

    }
}
