namespace DevNest.Common.Base.Constants
{
    /// <summary>
    /// Represents the class instance that contains the constants related to file system paths.
    /// </summary>
    public partial class FileSystemConstants
    {
        // Directory constants.
        public const string DirectoryUp = "..";
        public const string DevNestDirectory = "DataNest";
        public const string PreferencesDirectory = "Preferences";
        public const string SecureVaultDirectory = "SecureVault";
        public const string CredStoreDirectory = "CredStore";
        public const string ResourcesDirectory = "Resources";
        public const string ErrorCodesDirectoy = "Errors";
        public const string WarningCodesDirectory = "Warnings";
        public const string SuccessCodesDirectory = "Success";
        public const string PluginDirectory = "Plugin";
        public const string StoragePluginsDirectory = "Storage";
        public const string EncryptionPluginsDirectory = "Encryption";
        public const string DefaultLoggingDirectory = $"DataNest\\{LoggerDirectory}\\";
        public const string Default_LoggerDirectory = $"DataNest/{LoggerDirectory}/";
        public const string LoggerDirectory = "Logger";

        // Default directory constants.
        public const string DefaultWorkspace = "Default";
    }
}