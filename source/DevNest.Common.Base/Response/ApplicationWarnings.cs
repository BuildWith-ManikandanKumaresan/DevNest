namespace DevNest.Common.Base.Response
{
    /// <summary>
    /// Represents the class instance for applications warnings.
    /// </summary>
    public class ApplicationWarnings
    {
        private string? _description = string.Empty;

        /// <summary>
        /// Gets or sets the warning code.
        /// </summary>
        public string Code { get; set; } = "UNDEFINED";

        /// <summary>
        /// Gets or sets the warning message.
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Gets or sets the warning description.
        /// </summary>
        public string? Description
        {
            get
            {
                return string.IsNullOrEmpty(_description) ? Message : Description;
            }
            set
            {
                _description = value;
            }
        }

        /// <summary>
        /// Gets or sets the warning field.
        /// </summary>
        public string? Field { get; set; } // Optional: useful for validation warnings
    }
}