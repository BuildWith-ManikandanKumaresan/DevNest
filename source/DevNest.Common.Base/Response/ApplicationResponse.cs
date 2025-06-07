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

        /// <summary>
        /// Creates a successful response with the provided data.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ApplicationResponse<T> Success(T data)
        {
            return new ApplicationResponse<T>
            {
                Data = data,
                IsSuccess = true
            };
        }

        /// <summary>
        /// Creates a successful response with the provided data and message.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ApplicationResponse<T> Success(T data, string message)
        {
            return new ApplicationResponse<T>
            {
                Data = data,
                IsSuccess = true,
                Message = message
            };
        }

        /// <summary>
        /// Creates a successful response with the provided data, message, and warnings.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <param name="warnings"></param>
        /// <returns></returns>
        public static ApplicationResponse<T> Success(T data, string message, List<ApplicationWarnings> warnings)
        {
            return new ApplicationResponse<T>
            {
                Data = data,
                IsSuccess = true,
                Message = message,
                Warnings = warnings
            };
        }

        /// <summary>
        /// Creates a successful response with the provided data, message, and a single warning.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <param name="warning"></param>
        /// <returns></returns>
        public static ApplicationResponse<T> Success(T data, string message, ApplicationWarnings warning)
        {
            return new ApplicationResponse<T>
            {
                Data = data,
                IsSuccess = true,
                Message = message,
                Warnings = [warning]
            };
        }

        /// <summary>
        /// Creates a successful response with the provided data and message.
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        public static ApplicationResponse<T> Fail(List<ApplicationErrors> errors)
        {
            return new ApplicationResponse<T>
            {
                Data = default,
                IsSuccess = false,
                Errors = errors
            };
        }

        /// <summary>
        /// Creates a successful response with the provided data and message.
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public static ApplicationResponse<T> Fail(ApplicationErrors error)
        {
            return new ApplicationResponse<T>
            {
                Data = default,
                IsSuccess = false,
                Errors = [error]
            };
        }

        /// <summary>
        /// Creates a successful response with the provided data and message.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="warning"></param>
        /// <returns></returns>
        public static ApplicationResponse<T> Warning(T data, ApplicationWarnings warning)
        {
            return new ApplicationResponse<T>
            {
                Data = data,
                IsSuccess = false,
                Warnings = [warning]
            };
        }

        /// <summary>
        /// Creates a successful response with the provided data, message, and a single warning.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <param name="warning"></param>
        /// <returns></returns>
        public static ApplicationResponse<T> Warning(T data, string message, ApplicationWarnings warning)
        {
            return new ApplicationResponse<T>
            {
                Data = data,
                Message = message,
                IsSuccess = false,
                Warnings = [warning]
            };
        }

        /// <summary>
        /// Creates a successful response with the provided data, message, and warnings.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <param name="warnings"></param>
        /// <returns></returns>
        public static ApplicationResponse<T> Warning(T data, string message, List<ApplicationWarnings> warnings)
        {
            return new ApplicationResponse<T>
            {
                Data = data,
                Message = message,
                IsSuccess = false,
                Warnings = warnings
            };
        }

        /// <summary>
        /// Creates a successful response with the provided warning.
        /// </summary>
        /// <param name="warning"></param>
        /// <returns></returns>
        public static ApplicationResponse<T> Warning(ApplicationWarnings warning)
        {
            return new ApplicationResponse<T>
            {
                Data = default,
                IsSuccess = false,
                Warnings = [warning]
            };
        }

        /// <summary>
        /// Creates a successful response with the provided warnings.
        /// </summary>
        /// <param name="warnings"></param>
        /// <returns></returns>
        public static ApplicationResponse<T> Warning(List<ApplicationWarnings> warnings)
        {
            return new ApplicationResponse<T>
            {
                Data = default,
                IsSuccess = false,
                Warnings = warnings
            };
        }
    }
}