using Newtonsoft.Json.Serialization;

namespace DevNest.Common.Base.Constants
{
    /// <summary>
    /// Represents the class instance that contains the constants related to file system extensions used in the application.
    /// </summary>
    public partial class FileSystemExtensionConstants
    {
        // File extensions for various file types.

        public const string Extension_Data = ".dndat";
        public const string Extension_Preferences = ".dncfg";
        public const string Extension_Resources = ".dncont";
        public const string Extension_Dashboard = ".dndash";

        public const string SuccessContent = $".success{Extension_Resources}";
        public const string ErrorContent = $".errors{Extension_Resources}";
        public const string WarningContent = $".warnings{Extension_Resources}";

        // Log file constants.

        public const string LogFileNameWithExtension = $".log";

    }
}
