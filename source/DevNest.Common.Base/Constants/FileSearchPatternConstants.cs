namespace DevNest.Common.Base.Constants
{
    /// <summary>
    /// Represents the class instance that contains the constants related to file search patterns used in the application.
    /// </summary>
    public partial class FileSearchPatternConstants
    {
        // File search patterns for various file types.

        public const string Extension_Data = $"*{FileSystemExtensionConstants.Extension_Data}";
        public const string Extension_Preferences = $"*{FileSystemExtensionConstants.Extension_Preferences}";
        public const string Extension_Resources = $"*{FileSystemExtensionConstants.Extension_Resources}";
        public const string Extension_Dashboard = $"*{FileSystemExtensionConstants.Extension_Dashboard}";

        public const string SuccessContent = $"*{FileSystemExtensionConstants.SuccessContent}";
        public const string ErrorContent = $"*{FileSystemExtensionConstants.ErrorContent}";
        public const string WarningContent = $"*{FileSystemExtensionConstants.WarningContent}";

        // File search patterns.
        public const string DevNestAssembly = "DevNest.";
        public const string DevNest_Plugins = "DevNest.*.dll";
        public const string DevNest_Logs = $"*{FileSystemExtensionConstants.LogFileNameWithExtension}";

    }
}
