namespace DevNest.Common.Base.Constants.Message
{
    /// <summary>
    /// Represents the class instance for error constants used in the application.
    /// </summary>
    public class ErrorConstants
    {
        // Plugin Manager
        public const string NoPluginFound = "DEV-PM-001";

        // Credential Manager
        public const string NoCredentialsFound = "DEV-CM-001";
        public const string NoCredentialsFoundForTheId = "DEV-CM-002";
        public const string DeleteCredentialsFailed_All = "DEV-CM-003";
        public const string DeleteCredentialsFailed_ById = "DEV-CM-004";
        public const string UpdateCredentialsFailed = "DEV-CM-005";
        public const string CreateCredentialsFailed = "DEV-CM-006";


        // Logger Manager
        public const string LoggerConfigurationMissing = "DEV-LOG-001";
    }
}
