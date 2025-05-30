#region using directives
#endregion using directives

namespace DevNest.Common.Base.Response
{
    /// <summary>
    /// Represents the class instance for app response of type T data.
    /// </summary>
    public class ApplicationResponse<T>
    {
        /// <summary>
        /// Gets or sets the data of type T.
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// Gets or sets the IsSuccess flag.
        /// </summary>
        public bool IsSuccess { get; set; } = false;

        /// <summary>
        /// Gets or sets the HasWarning when the list of errors are above 0.
        /// </summary>
        public bool HasWarnings => Warnings?.Any() == true;

        /// <summary>
        /// Gets or sets the HasErrors when the list of warnings are above 0.
        /// </summary>
        public bool HasErrors => Errors?.Any() == true;

        /// <summary>
        /// Gets or sets the List of Warnings.
        /// </summary>
        public IEnumerable<ApplicationWarnings> Warnings { get; set; } = [];

        /// <summary>
        /// Gets or sets the List of Errors.
        /// </summary>
        public IEnumerable<ApplicationErrors> Errors { get; set; } = [];

        /// <summary>
        /// Gets or sets the success message.
        /// </summary>
        public string? Message { get; set; } // Optional: summary of success/failure
    }
}