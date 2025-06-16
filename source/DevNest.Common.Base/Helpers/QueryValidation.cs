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
                return ErrorConstants.CredentialIdCannotBeEmpty;
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
        /// Validates the associated groups for credentials.
        /// </summary>
        /// <param name="groups"></param>
        /// <returns></returns>
        //public static string ValidateAssociatedGroups(string[]? groups)
        //{
        //    if (groups is not null && groups.Length == 0)
        //    {
        //        return ErrorConstants.CredentialGroupsCannotBeEmpty;
        //    }
        //    return string.Empty;
        //}
    }
}
