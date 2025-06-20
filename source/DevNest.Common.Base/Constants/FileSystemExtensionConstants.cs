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

        public const string SuccessContent = $".success.dncont";
        public const string ErrorContent = $".errors.dncont";
        public const string WarningContent = $".warnings.dncont";
        public const string CredStoreCategoryContent = $"credstore.data.category.dncont";
        public const string CredStoreCategoryTypesContent = $"credstore.data.types.dncont";

        // Log file constants.

        public const string LogFileNameWithExtension = $".log";

    }
}
