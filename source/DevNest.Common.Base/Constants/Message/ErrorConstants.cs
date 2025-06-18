namespace DevNest.Common.Base.Constants.Message
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

        // Credential manager validation error codes.
        public const string CredentialTitleAlreadyExist = "DEV-CM-110";

        public const string CredentialIdCannotBeEmpty = "DEV-CM-111";
        public const string CredentialTypeCannotBeEmpty = "DEV-CM-112";
        public const string CredentialDomainCannotBeEmpty = "DEV-CM-113";
        public const string CredentialPasswordStrengthCannotBeEmpty = "DEV-CM-114";
        public const string CredentialWorkspaceCannotBeEmpty = "DEV-CM-115";
        public const string CredentialEnvironmentCannotBeEmpty = "DEV-CM-116";
        public const string CredentialGroupsCannotBeEmpty = "DEV-CM-117";

        // Logger Manager error codes.
        public const string LoggerConfigurationMissing = "DEV-LOG-100";

        // Search filter error codes.
        public const string SearchFieldRequired = "DEV-00-100";
        public const string SearchComparisonRequired = "DEV-00-101";
        public const string SearchValuesRequired = "DEV-00-102";

        public const string SearchTextComparisonTypeInvalid = "DEV-00-103";
        public const string SearchDateComparisonTypeInvalid = "DEV-00-105";

        public const string SearchFromDateRequired = "DEV-00-106";
        public const string SearchToDateRequired = "DEV-00-107";
        public const string SearchDateCannotBeFutureDate = "DEV-00-108";
    }
}
