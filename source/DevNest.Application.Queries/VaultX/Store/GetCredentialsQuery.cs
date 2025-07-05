#region using directives
using DevNest.Common.Base.Constants.Message;
using DevNest.Common.Base.Helpers;
using DevNest.Common.Base.MediatR;
using DevNest.Common.Base.MediatR.Contracts;
using DevNest.Common.Base.Response;
using DevNest.Infrastructure.DTOs.Search;
using DevNest.Infrastructure.DTOs.VaultX.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;
#endregion using directives

namespace DevNest.Application.Queries.VaultX
{
    /// <summary>
    /// Represents the class instance for Get Credentials query class.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="GetCredentialsQuery"/> class with the specified workspace.
    /// </remarks>
    /// <param name="workSpace"></param>
    public class GetCredentialsQuery(
        string workSpace,
        string? environment,
        string? category,
        string? type,
        string? domain,
        string? passwordStrength,
        bool? isEncrypted,
        bool? isValid,
        bool? isDisabled,
        bool? isExpired,
        IList<string>? groups,
        SearchRequestDTO? searchFilter) : QueryBase, IQuery<AppResponse<IList<CredentialResponseDTO>>>
    {

        /// <summary>
        /// Gets or sets the workspace identifier or name for which to retrieve credentials.
        /// </summary>
        public string WorkSpace { get; set; } = workSpace;

        /// <summary>
        /// Gets or sets the environment (e.g., Dev, QA, Prod) for which to filter credentials.
        /// </summary>
        public string? Environment { get; set; } = environment;

        /// <summary>
        /// Gets or sets the category of the credential (e.g., Database, Server, API).
        /// </summary>
        public string? Category { get; set; } = category;

        /// <summary>
        /// Gets or sets the type of credential (e.g., API Key, Password, SSH Key).
        /// </summary>
        public string? Type { get; set; } = type;

        /// <summary>
        /// Gets or sets the domain associated with the credentials, if applicable.
        /// </summary>
        public string? Domain { get; set; } = domain;

        /// <summary>
        /// Gets or sets the password strength criteria to filter credentials.
        /// </summary>
        public string? PasswordStrength { get; set; } = passwordStrength;

        /// <summary>
        /// Gets or sets a value indicating whether the credentials are encrypted.
        /// </summary>
        public bool? IsEncrypted { get; set; } = isEncrypted;

        /// <summary>
        /// Gets or sets a value indicating whether the credentials are valid.
        /// </summary>
        public bool? IsValid { get; set; } = isValid;

        /// <summary>
        /// Gets or sets a value indicating whether the credentials are disabled.
        /// </summary>
        public bool? IsDisabled { get; set; } = isDisabled;

        /// <summary>
        /// Gets or sets a value indicating whether the credentials are expired.
        /// </summary>
        public bool? IsExpired { get; set; } = isExpired;

        /// <summary>
        /// Gets or sets the list of groups to which the credentials belong.
        /// </summary>
        public IList<string>? Groups { get; set; } = groups ?? [];

        /// <summary>
        /// Gets or sets the search filter criteria for the credentials query.
        /// </summary>
        public SearchRequestDTO? SearchFilter { get; set; } = searchFilter;

        /// <summary>
        /// Validates the query and returns a list of errors if any.
        /// </summary>
        /// <returns></returns>
        public override IList<AppErrors> Validate()
        {
            IList<AppErrors> errors = base.Validate();

            string errId = QueryValidation.ValidateWorkspace(WorkSpace);

            if (!string.IsNullOrEmpty(errId))
            {
                errors.Add(Messages.GetError(errId));
            }

            errId = QueryValidation.ValidateEnvironment(Environment);

            if (!string.IsNullOrEmpty(errId))
            {
                errors.Add(Messages.GetError(errId));
            }

            errId = QueryValidation.ValidateCategory(Category);

            if (!string.IsNullOrEmpty(errId))
            {
                errors.Add(Messages.GetError(errId));
            }

            errId = QueryValidation.ValidateType(Type);

            if (!string.IsNullOrEmpty(errId))
            {
                errors.Add(Messages.GetError(errId));
            }

            errId = QueryValidation.ValidateDomain(Domain);

            if (!string.IsNullOrEmpty(errId))
            {
                errors.Add(Messages.GetError(errId));
            }

            errId = QueryValidation.ValidatePasswordStrength(PasswordStrength);

            if (!string.IsNullOrEmpty(errId))
            {
                errors.Add(Messages.GetError(errId));
            }

            if (SearchFilter != null)
            {
                if (SearchFilter.DateSearch != null)
                {
                    errId = QueryValidation.ValidateDateSearch(
                        fieldName: SearchFilter.DateSearch.FieldName,
                        comparison: SearchFilter.DateSearch.Comparison,
                        fromValue: SearchFilter.DateSearch.From,
                        toValue: SearchFilter.DateSearch.To);

                    if (!string.IsNullOrEmpty(errId))
                    {
                        errors.Add(Messages.GetError(errId));
                    }
                }

                if (SearchFilter.TextSearch != null)
                {
                    errId = QueryValidation.ValidateTextSearch(
                        fieldName: SearchFilter.TextSearch.FieldName,
                        comparison: SearchFilter.TextSearch.Comparison,
                        value: SearchFilter.TextSearch.Values);

                    if (!string.IsNullOrEmpty(errId))
                    {
                        errors.Add(Messages.GetError(errId));
                    }
                }
            }

            return errors;
        }
    }
}
