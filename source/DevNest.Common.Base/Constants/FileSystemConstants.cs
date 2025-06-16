namespace DevNest.Common.Base.Constants
{
    /// <summary>
    /// Represents the class instance that contains the constants related to file system paths.
    /// </summary>
    public partial class FileSystemConstants
    {
        public const string FolderUp = "..";

        public const string DevNestDirectory = "DataNest";
        public const string PreferencesDirectoryName = "Preferences";
        public const string SecureVaultDirectoryName = "SecureVault";
        public const string CredentialStoreDirectory = "CredStore";
        public const string ResourcesDirectoryName = "Resources";
        public const string ErrorCodesDirectoryName = "Errors";
        public const string WarningCodesDirectoryName = "Warnings";
        public const string SuccessCodesDirectoryName = "Success";

        public const string DevNestDataFileSearchPattern = "*.dndat";
        public const string DevNestDataFileExtension = ".dndat";
        public const string SuccessContentFileSearchPattern = "*success.dncont";
        public const string ErrorContentFileSearchPattern = "*errors.dncont";
        public const string WarningContentFileSearchPattern = "*warnings.dncont";

        public const string DefaultWorkspace = "Default";
        public const string StoragePluginsDirectoryName = "Plugin\\Storage";
        public const string EncryptionPluginsDirectoryName = "Plugin\\Encryption";
    }
}
