#region using directives
using DevNest.Common.Base.Constants;
using DevNest.Common.Base.Constants.Message;
using System.ComponentModel.DataAnnotations;
#endregion using directives

namespace DevNest.Common.Base.Helpers
{
    /// <summary>
    /// Represents a class for validating query parameters in the application.
    /// </summary>
    public class QueryValidation
    {
        /// <summary>
        /// Validates the credential ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string ValidateCredentialId(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                return ErrorConstants.CredentialCategoryIdCannotBeEmpty;
            }
            return string.Empty;
        }

        /// <summary>
        /// Validates the credential category ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string ValidateCategoryId(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                return ErrorConstants.CredentialCategoryIdCannotBeEmpty;
            }
            return string.Empty;
        }

        /// <summary>
        /// Validates the credential environment.
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static string ValidateEnvironment(string? environment)
        {
            if (environment is not null && string.IsNullOrWhiteSpace(environment))
            {
                return ErrorConstants.CredentialEnvironmentCannotBeEmpty;
            }

            return string.Empty;
        }

        /// <summary>
        /// Validates the credential category.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public static string ValidateCategory(string? category)
        {
            if (category is not null && string.IsNullOrWhiteSpace(category))
            {
                return ErrorConstants.CredentialCategoryCannotBeEmpty;
            }
            return string.Empty;
        }

        /// <summary>
        /// Validates the credential type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string ValidateType(string? type)
        {
            if (type is not null && string.IsNullOrWhiteSpace(type))
            {
                return ErrorConstants.CredentialTypeCannotBeEmpty;
            }
            return string.Empty;
        }

        /// <summary>
        /// Validates the credential title.
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public static string ValidateDomain(string? domain)
        {
            if (domain is not null && string.IsNullOrWhiteSpace(domain))
            {
                return ErrorConstants.CredentialDomainCannotBeEmpty;
            }
            return string.Empty;
        }

        /// <summary>
        /// Validates the credential password strength.
        /// </summary>
        /// <param name="passwordStrength"></param>
        /// <returns></returns>
        public static string ValidatePasswordStrength(string? passwordStrength)
        {
            if (passwordStrength is not null && string.IsNullOrWhiteSpace(passwordStrength))
            {
                return ErrorConstants.CredentialPasswordStrengthCannotBeEmpty;
            }
            return string.Empty;
        }

        /// <summary>
        /// Validates the credential workspace.
        /// </summary>
        /// <param name="workspace"></param>
        /// <returns></returns>
        public static string ValidateWorkspace(string? workspace)
        {
            if (workspace is not null && string.IsNullOrWhiteSpace(workspace))
            {
                return ErrorConstants.CredentialWorkspaceCannotBeEmpty;
            }
            return string.Empty;
        }

        /// <summary>
        /// Validates the text search parameters for a query.
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="comparison"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static string ValidateTextSearch(string? fieldName,string? comparison,string? value)
        {
            if (string.IsNullOrWhiteSpace(fieldName))
            {
                return ErrorConstants.SearchFieldRequired;
            }
            if (string.IsNullOrWhiteSpace(comparison))
            {
                return ErrorConstants.SearchComparisonRequired;
            }

            IList<string> comparisonTypes = 
                [ 
                TextSearchConstants.StartsWith.ToLower(), 
                TextSearchConstants.EndsWith.ToLower(),
                TextSearchConstants.Contains.ToLower(), 
                TextSearchConstants.Equals.ToLower(), 
                TextSearchConstants.NotEquals.ToLower()
                ];

            if (!comparisonTypes.Contains(comparison.ToLower()))
            {
                return ErrorConstants.SearchTextComparisonTypeInvalid;
            }
            if (string.IsNullOrWhiteSpace(value))
            {
                return ErrorConstants.SearchValuesRequired;
            }
            return string.Empty;
        }

        /// <summary>
        /// Validates the date search parameters for a query.
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="comparison"></param>
        /// <param name="fromValue"></param>
        /// <param name="toValue"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static string ValidateDateSearch(string? fieldName, string? comparison, DateTime? fromValue, DateTime? toValue)
        {
            if (string.IsNullOrWhiteSpace(fieldName))
            {
                return ErrorConstants.SearchFieldRequired;
            }
            if (string.IsNullOrWhiteSpace(comparison))
            {
                return ErrorConstants.SearchComparisonRequired;
            }

            IList<string> comparisonTypes =
                [
                DateSearchConstants.Exact.ToLower(),
                DateSearchConstants.NotExact.ToLower(),
                DateSearchConstants.Range.ToLower(),
                DateSearchConstants.NotInRange.ToLower()
                ];

            if (!comparisonTypes.Contains(comparison.ToLower()))
            {
                return ErrorConstants.SearchDateComparisonTypeInvalid;
            }
            if (fromValue == null)
            {
                return ErrorConstants.SearchFromDateRequired;
            }
            if (toValue == null)
            {
                return ErrorConstants.SearchToDateRequired;
            }
            if(fromValue > DateTime.Now || toValue > DateTime.Now)
            {
                return ErrorConstants.SearchDateCannotBeFutureDate;
            }
            return string.Empty;
        }
    }
}
