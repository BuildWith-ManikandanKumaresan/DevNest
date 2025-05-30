namespace DevNest.Common.Base.Response
{
    /// <summary>
    /// Represents the class instance for Application errrors.
    /// </summary>
    public class ApplicationErrors
    {
        private string? _description = string.Empty;

        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        public string Code { get; set; } = "UNDEFINED";

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Gets or sets the error description.
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
        /// Gets or sets the error field.
        /// </summary>
        public string? Field { get; set; } // Optional: useful for validation errors
    }
}