using DevNest.Common.Base.Response;

namespace DevNest.Common.Base.Constants.Message
{
    /// <summary>
    /// Represents the class instance for holding constant messages used across the application.
    /// </summary>
    public static class Messages
    {
        private static readonly Dictionary<string, AppErrors>? _errors;
        private static readonly Dictionary<string, AppWarnings>? _warnings;
        private static readonly Dictionary<string, AppSuccess>? _success;

        /// <summary>
        /// Initialize the static instance for Messages.
        /// </summary>
        static Messages()
        {
            _errors = [];
            _warnings = [];
            _success = [];
        }

        /// <summary>
        /// Initializes the static class with predefined error messages.
        /// </summary>
        public static Dictionary<string, AppErrors>? Errors => _errors;

        /// <summary>
        /// Initializes the static class with predefined warnings.
        /// </summary>
        public static Dictionary<string, AppWarnings>? Warnings => _warnings;

        /// <summary>
        /// Initializes the static class with predefined success messages.
        /// </summary>
        public static Dictionary<string, AppSuccess>? Success => _success;

        /// <summary>
        /// Initializes the error codes from the specified directory.
        /// </summary>
        /// <param name="errorCodesDirectory"></param>
        public static void InitErrorCodes(string errorCodesDirectory)
        {
            try
            {
                if (_errors?.Count == 0)
                {
                    string[] errorFiles = Directory.GetFiles(errorCodesDirectory, FileSystemExtensionConstants.ErrorContentFileSearchPattern);
                    foreach (string errorFile in errorFiles)
                    {
                        if (File.Exists(errorFile))
                        {
                            string jsonContent = File.ReadAllText(errorFile);
                            var errorCodes = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, AppErrors>>(jsonContent);
                            if (errorCodes != null)
                            {
                                foreach (var errorCode in errorCodes)
                                {
                                    _errors?.Add(errorCode.Key, errorCode.Value);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Initializes the warning codes from the specified directory.
        /// </summary>
        /// <param name="warningCodesDirectory"></param>
        public static void InitWarningCodes(string warningCodesDirectory)
        {
            try
            {
                if (_warnings?.Count == 0)
                {
                    string[] warningFiles = Directory.GetFiles(warningCodesDirectory, FileSystemExtensionConstants.WarningContentFileSearchPattern);
                    foreach (string warningFile in warningFiles)
                    {
                        if (File.Exists(warningFile))
                        {
                            string jsonContent = File.ReadAllText(warningFile);
                            var warningCodes = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, AppWarnings>>(jsonContent);
                            if (warningCodes != null)
                            {
                                foreach (var warningCode in warningCodes)
                                {
                                    _warnings?.Add(warningCode.Key, warningCode.Value);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Initializes the success codes from the specified directory.
        /// </summary>
        /// <param name="successCodesDirectory"></param>
        public static void InitSuccessCodes(string successCodesDirectory)
        {
            try
            {
                if (_success?.Count == 0)
                {
                    string[] successFiles = Directory.GetFiles(successCodesDirectory, FileSystemExtensionConstants.SuccessContentFileSearchPattern);
                    foreach (string successFile in successFiles)
                    {
                        if (File.Exists(successFile))
                        {
                            string jsonContent = File.ReadAllText(successFile);
                            var successCodes = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, AppSuccess>>(jsonContent);
                            if (successCodes != null)
                            {
                                foreach (var successCode in successCodes)
                                {
                                    _success?.Add(successCode.Key, successCode.Value);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Gets the error message based on the provided key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static AppErrors GetError(string key)
        {
            if (Errors?.TryGetValue(key, out AppErrors? error) ?? false)
                return error;
            return new AppErrors();
        }

        /// <summary>
        /// Gets the warning message based on the provided key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static AppWarnings GetWarning(string key)
        {
            if (Warnings?.TryGetValue(key, out AppWarnings? warning) ?? false)
                return warning;
            return new AppWarnings();
        }

        /// <summary>
        /// Gets the success message based on the provided key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetSuccess(string key)
        {
            if (Success?.TryGetValue(key, out AppSuccess? success) ?? false)
                return success.Message ?? string.Empty;
            return string.Empty;
        }
    }
}
