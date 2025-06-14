﻿#region using directives
using DevNest.Common.Base.Constants;
using DevNest.Common.Base.Constants.Message;
#endregion using directives

namespace DevNest.Common.Base.Response
{
    /// <summary>
    /// Represents the class instance for applications warnings.
    /// </summary>
    public class AppWarnings
    {
        private string? _description = string.Empty;

        /// <summary>
        /// Gets or sets the warning code.
        /// </summary>
        public string Code { get; set; } = ErrorConstants.UndefinedErrorCode;

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