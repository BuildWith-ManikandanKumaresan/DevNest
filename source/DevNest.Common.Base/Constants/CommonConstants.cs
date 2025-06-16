namespace DevNest.Common.Base.Constants
{
    /// <summary>
    /// Represents the class instance that contains the common constants used across the application.
    /// </summary>
    public partial class CommonConstants
    {
        public const string AssemblySearchPattern = "DevNest.";
        public const string Plugin_AssemblySearchPattern = "DevNest.*.dll";
        public const string JsonFileSearchPattern = "*.json";
        public const string JsonFileExtension = ".json";

        public const string SuccessConstantFileSearchPattern = "*success.json";
        public const string ErrorConstantFileSearchPattern = "*errors.json";
        public const string WarningConstantFileSearchPattern = "*warnings.json";

        public const string DefaultWorkspace = "Default";
    }
}
