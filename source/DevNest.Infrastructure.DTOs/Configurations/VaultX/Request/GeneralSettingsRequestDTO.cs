namespace DevNest.Infrastructure.DTOs.Configurations.VaultX.Request
{
    /// <summary>
    /// Represents general settings for the Credential Manager application.
    /// </summary>
    public class GeneralSettingsRequestDTO
    {
        /// <summary>
        /// Indicates whether to show archived credentials in the UI.
        /// </summary>
        public bool? ShowArchivedCredentials { get; set; }

        /// <summary>
        /// Indicates whether to show password as masked (e.g., with asterisks) in the UI.
        /// </summary>
        public bool? ShowPasswordAsMasked { get; set; }

        /// <summary>
        /// Placeholder text to use when masking passwords in the UI.
        /// </summary>
        public char? MaskingPlaceHolder { get; set; }

        /// <summary>
        /// Default category to use when creating new credentials.
        /// </summary>
        public string? DefaultCredentialCategory { get; set; }

        /// <summary>
        /// Default sorting field to use when displaying credentials in the UI.
        /// </summary>
        public string? DefaultSortingField { get; set; }

        /// <summary>
        /// Default sorting order to use when displaying credentials in the UI.
        /// </summary>
        public string? DefaultSortingOrder { get; set; }

        /// <summary>
        /// Indicates whether to allow duplicate titles for credentials.
        /// </summary>
        public bool? AllowDuplicateTitles { get; set; }
    }
}