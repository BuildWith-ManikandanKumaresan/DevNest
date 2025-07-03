namespace DevNest.Common.Base.Constants
{
    /// <summary>
    /// Represents the class instance that contains the constants related to file system paths.
    /// </summary>
    public partial class FileSystemConstants
    {
        // Build or Build constants.

        public const string PluginDirectory = "Plugin";
        public const string StorePluginDirectory = "Store";
        public const string EncryptionPluginsDirectory = "Encryption";

        // DevNest Directory constants.
        public const string DirectoryUp = "..";
        public const string DataDirectory = "Data";
        public const string LoggerDirectory = "Logger";
        public const string ConfigurationsDirectory = "Configurations";
        public const string ResourcesDirectory = "Resources";
        public const string VaultDirectory = "Vault";

        public const string ErrorCodesDirectoy = "Errors";
        public const string SuccessCodesDirectory = "Success";
        public const string WarningCodesDirectory = "Warnings";
        public const string SystemDirectory = "System";

        public const string VaultXDirectory = "VaultX";
        public const string TaggingXDirectory = "TaggingX";

        public const string DefaultLoggingDirectory = $"Logger\\";
        public const string Default_LoggerDirectory = $"Logger/";

        // Default directory constants.
        public const string DefaultWorkspace = "Workspace";

        // File extensions and names.
        public const string LogFileNameWithExtension = $".log";
    }
}