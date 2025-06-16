#region using directives
using System.Collections;
using System.Linq;
#endregion using directives

namespace DevNest.Common.Base.Response
{
    /// <summary>
    /// Represents the class instance for app response of type T data.
    /// </summary>
    public class AppResponse<T>
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
        public List<AppWarnings> Warnings { get; set; } = [];

        /// <summary>
        /// Gets or sets the List of Errors.
        /// </summary>
        public List<AppErrors> Errors { get; set; } = [];

        /// <summary>
        /// Gets or sets the success message.
        /// </summary>
        public string? Message { get; set; } // Optional: summary of success/failure

        /// <summary>
        /// Gets or sets the count of items in the response, if applicable.
        /// </summary>
        public int? Count { get; set; }

        /// <summary>
        /// Creates a successful response with the provided data.
        /// </summary>
        /// <param name="data"></param>
        public AppResponse(T? data)
        {
            this.Data = data;
            this.IsSuccess = true;
            this.Count = AppResponse<T>.GetResultCount(data);
        }

        /// <summary>
        /// Creates a successful response with the provided data and message.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        public AppResponse(T? data, string message)
        {
            Data = data;
            IsSuccess = true;
            Message = message;
            this.Count = AppResponse<T>.GetResultCount(data);
        }

        /// <summary>
        /// Creates a successful response with the provided data, message, and a single warning.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <param name="warning"></param>
        public AppResponse(T? data, string message, AppWarnings warning)
        {
            Data = data;
            IsSuccess = true;
            Message = message;
            Warnings = [warning];
            this.Count = AppResponse<T>.GetResultCount(data);
        }

        /// <summary>
        /// Creates a successful response with the provided data, message, and list of warnings.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <param name="warnings"></param>
        public AppResponse(T? data, string message, List<AppWarnings> warnings)
        {
            Data = data;
            IsSuccess = true;
            Message = message;
            Warnings = warnings;
            this.Count = AppResponse<T>.GetResultCount(data);
        }

        /// <summary>
        /// Creates a response with data and a single warning, indicating a non-successful operation.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="warning"></param>
        public AppResponse(T? data, AppWarnings warning)
        {
            Data = data;
            IsSuccess = false;
            Warnings = [warning];
            this.Count = AppResponse<T>.GetResultCount(data);
        }

        /// <summary>
        /// Creates a response with data and a list of warnings, indicating a non-successful operation.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="warnings"></param>
        public AppResponse(T? data, List<AppWarnings> warnings)
        {
            Data = data;
            IsSuccess = false;
            Warnings = warnings;
            this.Count = AppResponse<T>.GetResultCount(data);
        }

        /// <summary>
        /// Creates a response with a single warning, indicating a non-successful operation without data.
        /// </summary>
        /// <param name="warning"></param>
        public AppResponse(AppWarnings warning)
        {
            Data = default;
            IsSuccess = false;
            Warnings = [warning];
        }

        /// <summary>
        /// Creates a response with a list of warnings, indicating a non-successful operation without data.
        /// </summary>
        /// <param name="warnings"></param>
        public AppResponse(List<AppWarnings> warnings)
        {
            Data = default;
            IsSuccess = false;
            Warnings = warnings;
        }

        /// <summary>
        /// Creates a response with a single error, indicating a non-successful operation without data.
        /// </summary>
        /// <param name="errors"></param>
        public AppResponse(List<AppErrors> errors)
        {
            Data = default;
            IsSuccess = false;
            Errors = errors;
        }

        /// <summary>
        /// Creates a response with a single error, indicating a non-successful operation without data.
        /// </summary>
        /// <param name="error"></param>
        public AppResponse(AppErrors error)
        {
            Data = default;
            IsSuccess = false;
            Errors = [error];
        }

        /// <summary>
        /// Sets the count of items in the response, if applicable.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static int? GetResultCount(T? data)
        {
            if (data is IEnumerable enumerableData)
            {
                // Try casting to ICollection first for better performance
                if (data is ICollection collection)
                    return collection.Count;
                return enumerableData.Cast<object>().Count();
            }
            else
            {
                return data != null ? 1 : 0;
            }
        }
    }
}