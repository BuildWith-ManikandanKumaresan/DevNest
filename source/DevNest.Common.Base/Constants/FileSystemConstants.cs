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
        public const string PreferencesDirectoryName = "Preferences";
        public const string SecureVaultDirectoryName = "SecureVault";
        public const string CredentialStoreDirectory = "CredStore";
        public const string ResourcesDirectoryName = "Resources";
        public const string ErrorCodesDirectoryName = "Errors";
        public const string WarningCodesDirectoryName = "Warnings";
        public const string SuccessCodesDirectoryName = "Success";
        public const string StoragePluginsDirectoryName = "Plugin\\Storage";
        public const string EncryptionPluginsDirectoryName = "Plugin\\Encryption";
        public const string DefaultLoggingDirectory = "DataNest\\Logger\\";
        public const string Default_LoggerDirectory = "DataNest/Logger/";

        // Default directory constants.
        public const string DefaultWorkspace = "Default";

        // File search patterns.
        public const string AssemblySearchPattern = "DevNest.";
        public const string Plugin_AssemblySearchPattern = "DevNest.*.dll";
    }
}