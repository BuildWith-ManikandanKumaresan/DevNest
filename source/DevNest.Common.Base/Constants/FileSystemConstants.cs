namespace DevNest.Common.Base.Constants
{
    /// <summary>
    /// Represents the class instance that contains the constants related to file system paths.
    /// </summary>
    public partial class FileSystemConstants
    {
        // build-debug or build-release constants.

        public const string PluginDirectory = "Plugin";
        public const string StoragePluginsDirectory = "Storage";
        public const string EncryptionPluginsDirectory = "Encryption";

        // DevNest Directory constants.
        public const string DirectoryUp = "..";
        public const string DevNestDirectory = "DataNest";
        public const string LoggerDirectory = "Logger";
        public const string PreferencesDirectory = "Preferences";
        public const string ResourcesDirectory = "Resources";
        public const string SecureVaultDirectory = "SecureVault";

        public const string ErrorCodesDirectoy = "Errors";
        public const string SuccessCodesDirectory = "Success";
        public const string TagsDirectory = "Tags";
        public const string WarningCodesDirectory = "Warnings";

        public const string CredStoreDirectory = "CredStore";
        public const string TagStoreDirectory = "TagStore";

        public const string SystemTagsDirectory = "System tags";
        public const string SystemColorCodesDirectory = "System color codes";

        public const string DefaultLoggingDirectory = $"DataNest\\{LoggerDirectory}\\";
        public const string Default_LoggerDirectory = $"DataNest/{LoggerDirectory}/";

        // Default directory constants.
        public const string DefaultWorkspace = "Default";
    }
}