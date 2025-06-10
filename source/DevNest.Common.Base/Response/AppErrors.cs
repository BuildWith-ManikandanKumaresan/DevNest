#region using directives
using DevNest.Common.Base.Constants;
using DevNest.Common.Base.Constants.Message;
#endregion using directives

namespace DevNest.Common.Base.Response
{
    /// <summary>
    /// Represents the class instance for Application errrors.
    /// </summary>
    public class AppErrors
    {
        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        public string Code { get; set; } = ErrorConstants.UndefinedErrorCode;

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Gets or sets the error field.
        /// </summary>
        public string? Field { get; set; } // Optional: useful for validation errors
    }
}