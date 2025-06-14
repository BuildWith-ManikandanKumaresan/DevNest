﻿namespace DevNest.Common.Base.Constants.Message
{
    /// <summary>
    /// Represents the class instance for error constants used in the application.
    /// </summary>
    public class ErrorConstants
    {
        // Common error codes.
        public const string UndefinedErrorCode = "DEV-EXP-001";

        // Plugin Manager
        public const string NoStoragePluginFound = "DEV-PM-100";
        public const string NoEncryptionPluginFound = "DEV-PM-101";

        // Credential Manager error codes.
        public const string NoCredentialsFound = "DEV-CM-100";
        public const string NoCredentialsFoundForTheId = "DEV-CM-101";
        public const string DeleteCredentialsFailed_All = "DEV-CM-102";
        public const string DeleteCredentialsFailed_ById = "DEV-CM-103";
        public const string UpdateCredentialsFailed = "DEV-CM-104";
        public const string CreateCredentialsFailed = "DEV-CM-105";
        public const string ArchiveCredentialsFailed = "DEV-CM-106";
        public const string CredentialEncryptionFailed = "DEV-CM-107";
        public const string CredentialDecryptionFailed = "DEV-CM-108";

        public const string CredentialTitleAlreadyExist = "DEV-CM-110";

        // Logger Manager error codes.
        public const string LoggerConfigurationMissing = "DEV-LOG-100";
    }
}
