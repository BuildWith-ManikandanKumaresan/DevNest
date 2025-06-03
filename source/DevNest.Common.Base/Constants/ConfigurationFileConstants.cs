namespace DevNest.Common.Base.Constants
{
    /// <summary>
    /// Represents the class instance that contains the constants related to configuration files.
    /// </summary>
    public partial class ConfigurationFileConstants
    {
        public const string ConfigurationFileName = "appsettings.json";
        public const string ConfigurationFileName_Development = "appsettings.Development.json";
        public const string ConfigurationFileName_Production = "appsettings.Production.json";
        public const string ConfigurationFileName_Testing = "appsettings.Testing.json";

        public const string ConfigurationFileName_CredentialManager = "credential-manager-configurations.json";
        public const string ConfigurationFileName_PluginManager = "plugin-manager-configurations.json";
    }
}
