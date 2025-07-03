#region using directives
using AutoMapper;
using DevNest.Business.Domain.Domains.Contracts;
using DevNest.Business.Domain.RouterContracts;
using DevNest.Common.Base.Constants;
using DevNest.Common.Base.Constants.Message;
using DevNest.Common.Base.Contracts;
using DevNest.Common.Base.Entity;
using DevNest.Common.Base.Response;
using DevNest.Common.Logger;
using DevNest.Infrastructure.DTOs.VaultX.Request;
using DevNest.Infrastructure.DTOs.VaultX.Response;
using DevNest.Infrastructure.DTOs.VaultX.Request;
using DevNest.Infrastructure.Entity.Search;
using MediatR;
using System.Linq.Expressions;
using System.Net;
using DevNest.Infrastructure.Entity.Configurations.VaultX;
using DevNest.Infrastructure.Entity.VaultX;
#endregion using directives

namespace DevNest.Business.Domain.Domains
{
    /// <summary>
    /// Represents the class instance for credential manager services.
    /// </summary>
    /// <remarks>
    /// Initialize the new instance for credential manager services.
    /// </remarks>
    /// <param name="logger"></param>
    public class VaultXDomainService(
        IAppLogger<VaultXDomainService> logger,
        IVaultXRepositoryRouter router,
        IAppConfigService<VaultXConfigurationsEntityModel> applicationConfigService,
        IMapper mapper) : IVaultXDomainService
    {
        private readonly IAppLogger<VaultXDomainService> _logger = logger;
        private readonly IVaultXRepositoryRouter _router = router;
        private readonly IMapper _mapper = mapper;
        private readonly IAppConfigService<VaultXConfigurationsEntityModel> _applicationConfigService = applicationConfigService;

        /// <summary>
        /// Handler method for get credentials as DTO response.
        /// </summary>
        /// <returns></returns>
        public async Task<AppResponse<IList<CredentialResponseDTO>>> Get(
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
            string workspace,
            SearchEntityModel? searchFilter)
        {
            try
            {
                _logger.LogDebug($"{nameof(VaultXDomainService)} => {nameof(Get)} method called.");

                var data = (await _router.GetAsync(workspace))?.ToList();
                if (data == null)
                {
                    _logger.LogDebug($"{nameof(VaultXDomainService)} => {nameof(Get)} method returned null data.");
                    return new AppResponse<IList<CredentialResponseDTO>>(Messages.GetError(ErrorConstants.NoCredentialsFound));
                }

                // Filter out the credentials based on the environment.

                if (!string.IsNullOrEmpty(environment))
                    data = [.. data.Where(a => a.Environment?.Equals(environment, StringComparison.OrdinalIgnoreCase) ?? false)];

                // Filter out the credentials based on the category.

                if (!string.IsNullOrEmpty(category))
                    data = [.. data.Where(a => a.Category?.Equals(category, StringComparison.OrdinalIgnoreCase) ?? false)];

                // Filter out the credentials based on the type.

                if (!string.IsNullOrEmpty(type))
                    data = [.. data.Where(a => a.Details?.Type?.Equals(type, StringComparison.OrdinalIgnoreCase) ?? false)];

                // Filter out the credentials based on the domain.

                if (!string.IsNullOrEmpty(domain))
                    data = [.. data.Where(a => a.Details?.Domain?.Equals(domain, StringComparison.OrdinalIgnoreCase) ?? false)];

                // Filter out the credentials based on the password strength.

                if (!string.IsNullOrEmpty(passwordStrength))
                    data = [.. data.Where(a => a.PasswordHealth?.PasswordStrength?.ToString() == passwordStrength)];

                // Filter out the credentials based on the IsEncrypted status.

                if (isEncrypted != null)
                    data = [.. data.Where(a => a.Security?.IsEncrypted == isEncrypted)];

                // Filter out the credentials based on the Isvalid status.

                if (isValid != null)
                    data = [.. data.Where(a => a.Validatity?.IsValid == isValid)];

                // Filter out the credentials based on the IsExpired status.

                if (isExpired != null)
                    data = [.. data.Where(a => a.Validatity?.IsExpired == isExpired)];

                // Filter out the credentials based on the associated groups.

                if (groups != null && groups.Count > 0)
                {
                    var groupedCredentials = new List<CredentialEntityModel>();
                    groups.ToList().ForEach(group =>
                    {
                        // Filter out the credentials based on the group.
                        groupedCredentials.AddRange([.. data.Where(a => a.AssociatedGroups?.Contains(group, StringComparer.OrdinalIgnoreCase) ?? false)]);
                    });
                    data = groupedCredentials;
                }

                // Filter out archived credentials if the setting is enabled - showArchivedCredentials

                if (!_applicationConfigService?.Value?.GeneralSettings?.ShowArchivedCredentials ?? false)
                    data = [.. data.Where(a => a.Validatity?.IsDisabled == true)];

                // Filter out the credentials based on the IsExpired status.

                if (isDisabled != null)
                    data = [.. data.Where(a => a.Validatity?.IsDisabled == isDisabled)];

                // Filter credentials based on the search filter if provided.

                data = await FilterCredentials(data, searchFilter);

                data?.ForEach(async credential =>
                {
                    // Mask passwords if the setting is enabled - showPasswordAsMasked
                    await MaskingPasswords(credential, _applicationConfigService?.Value?.GeneralSettings?.ShowPasswordAsMasked);

                    // Check expiration for credentials if the setting is enabled - EnableCredentialExpirationCheck
                    if (_applicationConfigService?.Value?.SecuritySettings?.EnableCredentialExpirationCheck ?? false)
                        await CheckExpirationForCredentials(credential);

                    await SetPasswordHealthCheck(credential);
                });

                // Sort credentials based on the provided property and order - defaultSortingField, defaultSortingOrder
                data = await SortCredentials(data);

                _logger.LogDebug($"{nameof(VaultXDomainService)} => {nameof(Get)} method returned {data?.Count} credentials.");

                return new AppResponse<IList<CredentialResponseDTO>>(
                    data: _mapper.Map<IList<CredentialResponseDTO>>(data))
                {
                    Message = Messages.GetSuccess(SuccessConstants.CredentialsRetreivedSuccessfully)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(VaultXDomainService)} => {ex.Message}", ex);
                return new AppResponse<IList<CredentialResponseDTO>>(new AppErrors { Code = ErrorConstants.UndefinedErrorCode, Message = ex.Message });
            }
        }

        /// <summary>
        /// Handler method for get credentials by id as DTO response.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<AppResponse<CredentialResponseDTO>> GetById(Guid id, string workspace)
        {
            try
            {
                _logger.LogDebug($"{nameof(VaultXDomainService)} => {nameof(GetById)} method called with ID: {id}.");
                var entity = await _router.GetByIdAsync(id, workspace);
                if (entity == null)
                {
                    _logger.LogDebug($"{nameof(VaultXDomainService)} => {nameof(GetById)} method returned null for ID: {id}.");
                    return new AppResponse<CredentialResponseDTO>(Messages.GetError(ErrorConstants.NoCredentialsFoundForTheId));
                }

                // Mask passwords if the setting is enabled - showPasswordAsMasked
                await MaskingPasswords(entity, _applicationConfigService?.Value?.GeneralSettings?.ShowPasswordAsMasked);

                // Check expiration for credentials if the setting is enabled - EnableCredentialExpirationCheck
                if (_applicationConfigService?.Value?.SecuritySettings?.EnableCredentialExpirationCheck ?? false)
                    await CheckExpirationForCredentials(entity);

                await SetPasswordHealthCheck(entity);

                _logger.LogDebug($"{nameof(VaultXDomainService)} => {nameof(GetById)} method returned credentials for ID: {id}.");

                return new AppResponse<CredentialResponseDTO>(_mapper.Map<CredentialResponseDTO>(entity))
                {
                    Message = string.Format(Messages.GetSuccess(SuccessConstants.CredentialsByIdRetreivedSuccessfully), id)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(VaultXDomainService)} => {ex.Message}", ex);
                return new AppResponse<CredentialResponseDTO>(new AppErrors { Code = ErrorConstants.UndefinedErrorCode, Message = ex.Message });
            }
        }


        /// <summary>
        /// Handler method for delete all credentials and returns DTO response.
        /// </summary>
        /// <returns></returns>
        public async Task<AppResponse<bool>> Delete(string workspace)
        {
            try
            {
                _logger.LogDebug($"{nameof(VaultXDomainService)} => {nameof(Delete)} method called to delete all credentials.");
                var result = await _router.DeleteAsync(workspace);
                if (result == false)
                {
                    _logger.LogDebug($"{nameof(VaultXDomainService)} => {nameof(Delete)} method failed to delete all credentials.");
                    return new AppResponse<bool>(Messages.GetError(ErrorConstants.DeleteCredentialsFailed_All));
                }
                _logger.LogDebug($"{nameof(VaultXDomainService)} => {nameof(Delete)} method successfully deleted all credentials.");
                return new AppResponse<bool>(
                    data: result,
                    message: Messages.GetSuccess(SuccessConstants.CredentialsDeletedSuccessfully));
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(VaultXDomainService)} => {ex.Message}", ex);
                return new AppResponse<bool>(new AppErrors { Code = ErrorConstants.UndefinedErrorCode, Message = ex.Message });
            }
        }

        /// <summary>
        /// Handler method for delete credential using id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AppResponse<bool>> DeleteById(Guid id, string workspace)
        {
            try
            {
                _logger.LogDebug($"{nameof(VaultXDomainService)} => {nameof(DeleteById)} method called with ID: {id}.");
                var result = await _router.DeleteByIdAsync(id, workspace);
                if (result == false)
                {
                    _logger.LogDebug($"{nameof(VaultXDomainService)} => {nameof(DeleteById)} method failed to delete credential with ID: {id}.");
                    return new AppResponse<bool>(Messages.GetError(ErrorConstants.DeleteCredentialsFailed_ById));
                }

                _logger.LogDebug($"{nameof(VaultXDomainService)} => {nameof(DeleteById)} method successfully deleted credential with ID: {id}.");
                return new AppResponse<bool>(
                    data: result,
                    message: string.Format(
                        Messages.GetSuccess(SuccessConstants.CredentialsDeletedByIdSuccessfully), id));
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(VaultXDomainService)} => {ex.Message}", ex);
                return new AppResponse<bool>(new AppErrors { Code = ErrorConstants.UndefinedErrorCode, Message = ex.Message });
            }
        }

        /// <summary>
        /// Handler method to add the credentials entity.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<AppResponse<CredentialResponseDTO>> Add(AddCredentialRequest? request, string workspace)
        {
            try
            {
                _logger.LogDebug($"{nameof(VaultXDomainService)} => {nameof(Add)} method called with request: {request}.");
                var entity = _mapper.Map<CredentialEntityModel>(request);
                entity.Id = Guid.NewGuid();

                // Set default expiration date if not set or expired - enableCredentialExpirationCheck
                await SetDefaultExpirationDate(entity);

                if (!_applicationConfigService?.Value?.GeneralSettings?.AllowDuplicateTitles ?? true)
                {
                    var existingResult = await _router.GetAsync(workspace);
                    if (existingResult?.Any(a => a.Title?.Equals(entity.Title) ?? false) ?? false)
                        return new AppResponse<CredentialResponseDTO>(error: Messages.GetError(ErrorConstants.CredentialTitleAlreadyExist));
                }

                // Check expiration for credentials if the setting is enabled - EnableCredentialExpirationCheck
                if (_applicationConfigService?.Value?.SecuritySettings?.EnableCredentialExpirationCheck ?? false)
                    await CheckExpirationForCredentials(entity);

                await SetPasswordHealthCheck(entity);

                var result = await _router.AddAsync(entity, workspace);
                if (result == null)
                {
                    _logger.LogDebug($"{nameof(VaultXDomainService)} => {nameof(Add)} method failed to create credentials.");
                    return new AppResponse<CredentialResponseDTO>(Messages.GetError(ErrorConstants.CreateCredentialsFailed)) { Data = null };
                }

                _logger.LogDebug($"{nameof(VaultXDomainService)} => {nameof(Add)} method successfully created credentials with ID: {result.Id}.");
                return new AppResponse<CredentialResponseDTO>(
                    data: _mapper.Map<CredentialResponseDTO>(result),
                    message: string.Format(
                        Messages.GetSuccess(SuccessConstants.CredentialsCreatedSuccessfully),
                        string.IsNullOrEmpty(result.Title) ? result.Id : result.Title));
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(VaultXDomainService)} => {ex.Message}", ex);
                return new AppResponse<CredentialResponseDTO>(new AppErrors { Code = ErrorConstants.UndefinedErrorCode, Message = ex.Message });
            }
        }

        /// <summary>
        /// Handler method to update the credentials entity.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<AppResponse<CredentialResponseDTO>> Update(UpdateCredentialRequest? request, string workspace)
        {
            try
            {
                _logger.LogDebug($"{nameof(VaultXDomainService)} => {nameof(Update)} method called with request: {request}.");
                var entity = _mapper.Map<CredentialEntityModel>(request);

                // Set default expiration date if not set or expired - enableCredentialExpirationCheck
                await SetDefaultExpirationDate(entity);

                // Check expiration for credentials if the setting is enabled - EnableCredentialExpirationCheck
                if (_applicationConfigService?.Value?.SecuritySettings?.EnableCredentialExpirationCheck ?? false)
                    await CheckExpirationForCredentials(entity);

                await SetPasswordHealthCheck(entity);

                var result = await _router.UpdateAsync(entity, workspace);

                if (result == null)
                {
                    _logger.LogDebug($"{nameof(VaultXDomainService)} => {nameof(Update)} method failed to update credentials.");
                    return new AppResponse<CredentialResponseDTO>(Messages.GetError(ErrorConstants.UpdateCredentialsFailed));
                }

                _logger.LogDebug($"{nameof(VaultXDomainService)} => {nameof(Update)} method successfully updated credentials with ID: {result.Id}.");
                return new AppResponse<CredentialResponseDTO>(
                    data: _mapper.Map<CredentialResponseDTO>(result),
                    message: string.Format(
                        Messages.GetSuccess(SuccessConstants.CredentialsUpdatedSuccessfully),
                        string.IsNullOrEmpty(result.Title) ? result.Id : result.Title));
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(VaultXDomainService)} => {ex.Message}", ex);
                return new AppResponse<CredentialResponseDTO>(new AppErrors
                {
                    Code = ErrorConstants.UndefinedErrorCode,
                    Message = ex.Message
                });
            }
        }

        /// <summary>
        /// Handler method to archive a credential by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AppResponse<bool>> Archive(Guid id, string workspace)
        {
            try
            {
                _logger.LogDebug($"{nameof(VaultXDomainService)} => {nameof(Archive)} method called with ID: {id}.");
                var result = await _router.ArchiveByIdAsync(id, workspace);
                if (result == false)
                {
                    _logger.LogDebug($"{nameof(VaultXDomainService)} => {nameof(Archive)} method failed to archive credential with ID: {id}.");
                    return new AppResponse<bool>(Messages.GetError(ErrorConstants.DeleteCredentialsFailed_ById));
                }

                _logger.LogDebug($"{nameof(VaultXDomainService)} => {nameof(Archive)} method successfully archived credential with ID: {id}.");
                return new AppResponse<bool>(
                    data: result,
                    message: string.Format(
                        Messages.GetSuccess(SuccessConstants.CredentialsArchivedSuccessfully),
                        id));
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(VaultXDomainService)} => {ex.Message}", ex);
                return new AppResponse<bool>(new AppErrors
                {
                    Code = ErrorConstants.UndefinedErrorCode,
                    Message = ex.Message
                });
            }
        }

        /// <summary>
        /// Handler method to encrypt the credentials by its ID and returns DTO response.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AppResponse<CredentialResponseDTO>> Encrypt(Guid id, string workspace)
        {
            try
            {
                _logger.LogDebug($"{nameof(VaultXDomainService)} => {nameof(Encrypt)} method called with ID: {id}.");
                var entity = await _router.EncryptByIdAsync(id, workspace);
                if (entity == null)
                {
                    _logger.LogDebug($"{nameof(VaultXDomainService)} => {nameof(Encrypt)} method returned null for ID: {id}.");
                    return new AppResponse<CredentialResponseDTO>(Messages.GetError(ErrorConstants.CredentialEncryptionFailed));
                }

                // Mask passwords if the setting is enabled - showPasswordAsMasked
                await MaskingPasswords(entity, _applicationConfigService?.Value?.GeneralSettings?.ShowPasswordAsMasked);

                // Check expiration for credentials if the setting is enabled - EnableCredentialExpirationCheck
                if (_applicationConfigService?.Value?.SecuritySettings?.EnableCredentialExpirationCheck ?? false)
                    await CheckExpirationForCredentials(entity);

                await SetPasswordHealthCheck(entity);

                _logger.LogDebug($"{nameof(VaultXDomainService)} => {nameof(Encrypt)} method returned credentials for ID: {id}.");

                return new AppResponse<CredentialResponseDTO>(_mapper.Map<CredentialResponseDTO>(entity))
                {
                    Message = string.Format(Messages.GetSuccess(SuccessConstants.CredentialsEncryptedSuccessfully), id)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(VaultXDomainService)} => {ex.Message}", ex);
                return new AppResponse<CredentialResponseDTO>(new AppErrors { Code = ErrorConstants.UndefinedErrorCode, Message = ex.Message });
            }
        }

        /// <summary>
        /// Handler method to decrypt the credentials by its ID and returns DTO response.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AppResponse<CredentialResponseDTO>> Decrypt(Guid id, string workspace)
        {
            try
            {
                _logger.LogDebug($"{nameof(VaultXDomainService)} => {nameof(Decrypt)} method called with ID: {id}.");
                var entity = await _router.DecryptByIdAsync(id, workspace);
                if (entity == null)
                {
                    _logger.LogDebug($"{nameof(VaultXDomainService)} => {nameof(Decrypt)} method returned null for ID: {id}.");
                    return new AppResponse<CredentialResponseDTO>(Messages.GetError(ErrorConstants.CredentialDecryptionFailed));
                }

                // Mask passwords if the setting is enabled - showPasswordAsMasked
                await MaskingPasswords(entity, _applicationConfigService?.Value?.GeneralSettings?.ShowPasswordAsMasked);

                // Check expiration for credentials if the setting is enabled - EnableCredentialExpirationCheck
                if (_applicationConfigService?.Value?.SecuritySettings?.EnableCredentialExpirationCheck ?? false)
                    await CheckExpirationForCredentials(entity);

                await SetPasswordHealthCheck(entity);

                _logger.LogDebug($"{nameof(VaultXDomainService)} => {nameof(Decrypt)} method returned credentials for ID: {id}.");

                return new AppResponse<CredentialResponseDTO>(_mapper.Map<CredentialResponseDTO>(entity))
                {
                    Message = string.Format(Messages.GetSuccess(SuccessConstants.CredentialsDecryptedSuccessfully), id)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(VaultXDomainService)} => {ex.Message}", ex);
                return new AppResponse<CredentialResponseDTO>(new AppErrors { Code = ErrorConstants.UndefinedErrorCode, Message = ex.Message });
            }
        }

        /// <summary>
        /// Method to get credential categories based on the workspace.
        /// </summary>
        /// <param name="workSpace"></param>
        /// <returns></returns>
        public async Task<AppResponse<IList<CategoryResponseDTO>?>> GetCategories(string workspace)
        {
            try
            {
                _logger.LogDebug($"{nameof(VaultXDomainService)} => {nameof(GetCategories)} method called with workspace: {workspace}.");
                var entity = await _router.GetCategoriesAsync(workspace);
                if (entity == null)
                {
                    _logger.LogDebug($"{nameof(VaultXDomainService)} => {nameof(GetCategories)} method returned null for workspace: {workspace}.");
                    return new AppResponse<IList<CategoryResponseDTO>?>(Messages.GetError(ErrorConstants.CredentialsCategoriesNotFound));
                }

                _logger.LogDebug($"{nameof(VaultXDomainService)} => {nameof(GetCategories)} method returned categories for workspace: {workspace}.");

                return new AppResponse<IList<CategoryResponseDTO>?>(_mapper.Map<IList<CategoryResponseDTO>>(entity));
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(VaultXDomainService)} => {ex.Message}", ex);
                return new AppResponse<IList<CategoryResponseDTO>?>(new AppErrors { Code = ErrorConstants.UndefinedErrorCode, Message = ex.Message });
            }
        }

        /// <summary>
        /// Method to get credential category types based on the category ID and workspace.
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="workSpace"></param>
        /// <returns></returns>
        public async Task<AppResponse<IList<TypesResponseDTO>?>> GetTypes(Guid categoryId, string workspace)
        {
            try
            {
                _logger.LogDebug($"{nameof(VaultXDomainService)} => {nameof(GetTypes)} method called with id: {categoryId}.");
                var entity = await _router.GetTypesAsync(categoryId, workspace);
                if (entity == null)
                {
                    _logger.LogDebug($"{nameof(VaultXDomainService)} => {nameof(GetTypes)} method returned null for id: {categoryId}.");
                    return new AppResponse<IList<TypesResponseDTO>?>(Messages.GetError(ErrorConstants.CredentialsCategoryTypesNotFound));
                }

                _logger.LogDebug($"{nameof(VaultXDomainService)} => {nameof(GetTypes)} method returned categories for id: {categoryId}.");

                return new AppResponse<IList<TypesResponseDTO>?>(_mapper.Map<IList<TypesResponseDTO>?>(entity));
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(VaultXDomainService)} => {ex.Message}", ex);
                return new AppResponse<IList<TypesResponseDTO>?>(new AppErrors { Code = ErrorConstants.UndefinedErrorCode, Message = ex.Message });
            }
        }

        #region Private methods

        /// <summary>
        /// Method to mask passwords in the credentials DTO.
        /// </summary>
        /// <param name="credentialsDTO"></param>
        /// <returns></returns>
        private async Task MaskingPasswords(CredentialEntityModel entity, bool? globalMaskingEnabled)
        {
            if (!string.IsNullOrEmpty(entity?.Details?.Password) && (entity.IsPasswordMasked ?? globalMaskingEnabled ?? false))
            {
                char? maskChar = _applicationConfigService.Value?.GeneralSettings?.MaskingPlaceHolder != null
                    ? _applicationConfigService.Value?.GeneralSettings?.MaskingPlaceHolder
                    : '*';
                entity.Details.Password = new string(maskChar ?? '*', entity.Details.Password.Length);
            }
            await Task.FromResult(Task.CompletedTask);
        }

        /// <summary>
        /// Method to sort credentials based on the provided property and order.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="sortProperty"></param>
        /// <param name="descending"></param>
        /// <returns></returns>
        private async Task<List<CredentialEntityModel>?> SortCredentials(
            List<CredentialEntityModel>? entity,
            string? sortProperty = null,
            bool? descending = true)
        {
            if (entity == null || entity.Count == 0)
                return entity;

            if (string.IsNullOrEmpty(sortProperty))
                sortProperty = _applicationConfigService.Value?.GeneralSettings?.DefaultSortingField ?? string.Empty;

            descending ??= (_applicationConfigService.Value?.GeneralSettings?.DefaultSortingOrder?.ToLower() == "desc");

            if (string.IsNullOrWhiteSpace(sortProperty))
                return [.. entity.OrderBy(c => c.Id)]; // Fallback

            var param = Expression.Parameter(typeof(CredentialEntityModel), "c");
            var property = Expression.PropertyOrField(param, sortProperty);
            var lambda = Expression.Lambda(property, param);

            var methodName = descending ?? true ? "OrderByDescending" : "OrderBy";

            var result = typeof(Enumerable)
                .GetMethods()
                .First(m => m.Name == methodName && m.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(CredentialEntityModel), property.Type)
                .Invoke(null, [entity, lambda.Compile()]);

            var finalResult = ((IEnumerable<CredentialEntityModel>)result!).ToList();

            return await Task.FromResult(finalResult);
        }

        /// <summary>
        /// Method to set the default expiration date for a credential entity if not set or expired.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private async Task SetDefaultExpirationDate(CredentialEntityModel entity)
        {
            if (entity.Validatity != null)
            {
                if (entity.Validatity.ExpirationDate == null || entity.Validatity.ExpirationDate < DateTime.Now)
                {
                    entity.Validatity.ExpirationDate = DateTime.Now.AddDays(_applicationConfigService.Value?.SecuritySettings?.DefaultCredentialExpirationDays ?? 90);
                }
            }
            await Task.FromResult(Task.CompletedTask);
        }

        /// <summary>
        /// Method to check and update the expiration status of a credential entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private async Task CheckExpirationForCredentials(CredentialEntityModel entity)
        {
            if (entity.Validatity?.ExpirationDate != null)
            {
                entity.Validatity.IsExpired = entity.Validatity.ExpirationDate < DateTime.Now; // Mark as expired
                entity.Validatity.RemainingDaysBeforeExpiration = entity.Validatity.ExpirationDate > DateTime.Now
                    ? (entity.Validatity.ExpirationDate.Value.Date - DateTime.Now.Date).Days
                    : 0; // Calculate remaining days before expiration
            }

            await Task.FromResult(Task.CompletedTask);
        }

        /// <summary>
        /// Method to set password health check for a credential entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private async Task SetPasswordHealthCheck(CredentialEntityModel entity)
        {
            if (!string.IsNullOrEmpty(entity?.Details?.Password))
            {
                var result = Zxcvbn.Core.EvaluatePassword(entity.Details.Password);
                entity.PasswordHealth = new PasswordHealthEntityModel()
                {
                    Score = result.Score,
                    CalculationTime = result.CalcTime,
                    Guesses = result.Guesses,
                    Suggestions = result.Feedback.Suggestions,
                    Warnings = result.Feedback.Warning,
                };
            }
            else
            {
                if (entity != null)
                    entity.PasswordHealth = new PasswordHealthEntityModel() { Score = 0 };
            }
            await Task.FromResult(Task.CompletedTask);
        }

        /// <summary>
        /// Method to filter credentials based on the search entity model.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="searchEntity"></param>
        /// <returns></returns>
        private async Task<List<CredentialEntityModel>?> FilterCredentials(List<CredentialEntityModel>? entity, SearchEntityModel? searchEntity)
        {
            if (entity == null)
                return entity;
            if (searchEntity == null)
                return entity;

            if (searchEntity.TextSearch != null)
            {
                PerformTextSearch(ref entity, searchEntity.TextSearch);
            }

            if (searchEntity.DateSearch != null)
            {
                PerformDateSearch(ref entity, searchEntity.DateSearch);
            }

            return await Task.FromResult(entity);
        }

        /// <summary>
        /// Method to perform text search on the credentials based on the search entity model.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="searchEntity"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        /// <exception cref="NotSupportedException"></exception>
        private void PerformTextSearch(ref List<CredentialEntityModel>? entity, TextSearchEntityModel searchEntity)
        {
            if (searchEntity == null || entity == null)
                return;

            var fieldName = searchEntity.FieldName;
            var comparison = searchEntity.Comparison?.ToLower();
            var value = searchEntity.Values; // Assuming single-value text filter

            if (string.IsNullOrWhiteSpace(fieldName))
            {
                _logger.LogError($"{nameof(VaultXDomainService)} => {nameof(PerformTextSearch)}: Field name is null or empty.");
                throw new ArgumentException("Text search filter requires a valid field name.");
            }

            if (string.IsNullOrWhiteSpace(comparison))
            {
                _logger.LogError($"{nameof(VaultXDomainService)} => {nameof(PerformTextSearch)}: Comparison type is null or empty.");
                throw new ArgumentException("Text search filter requires a valid comparison type.");
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                _logger.LogError($"{nameof(VaultXDomainService)} => {nameof(PerformTextSearch)}: Comparison value is null or empty.");
                throw new ArgumentException("Text search filter requires a non-empty value.");
            }

            // Ensure the field exists
            var propertyInfo = typeof(CredentialEntityModel).GetProperty(fieldName);
            if (propertyInfo == null || propertyInfo.PropertyType != typeof(string))
            {
                _logger.LogError($"{nameof(VaultXDomainService)} => {nameof(PerformTextSearch)}: Invalid or unsupported field '{fieldName}' for text search.");
                throw new Exception($"Invalid or unsupported text field: {fieldName}");
            }

            // Build expression
            var parameter = Expression.Parameter(typeof(CredentialEntityModel), "c");
            var property = Expression.Property(parameter, propertyInfo);

            // null check: c.FieldName != null
            var notNull = Expression.NotEqual(property, Expression.Constant(null, typeof(string)));
            var constant = Expression.Constant(value, typeof(string));
            Expression? body;

            switch (comparison.ToLower())
            {
                case TextSearchConstants.Equals:
                    body = Expression.Equal(property, constant);
                    break;

                case TextSearchConstants.NotEquals:
                    body = Expression.NotEqual(property, constant);
                    break;

                case TextSearchConstants.StartsWith:
                    body = Expression.Call(property, typeof(string).GetMethod("StartsWith", new[] { typeof(string) })!, constant);
                    break;

                case TextSearchConstants.Contains:
                    body = Expression.Call(property, typeof(string).GetMethod("Contains", new[] { typeof(string) })!, constant);
                    break;

                case TextSearchConstants.EndsWith:
                    body = Expression.Call(property, typeof(string).GetMethod("EndsWith", new[] { typeof(string) })!, constant);
                    break;

                default:
                    _logger.LogError($"{nameof(VaultXDomainService)} => {nameof(PerformTextSearch)}: Unsupported comparison type '{comparison}'.");
                    throw new NotSupportedException($"Unsupported comparison type: {comparison}");
            }

            // Combine null check and actual comparison
            var finalExpression = Expression.AndAlso(notNull, body);

            var predicate = Expression.Lambda<Func<CredentialEntityModel, bool>>(finalExpression, parameter);

            entity = entity.Where(predicate.Compile()).ToList();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="searchEntity"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        /// <exception cref="NotSupportedException"></exception>
        private void PerformDateSearch(ref List<CredentialEntityModel>? entity, DateSearchEntityModel searchEntity)
        {
            if (searchEntity == null || entity == null)
                return;

            var fieldName = searchEntity.FieldName;
            var comparison = searchEntity.Comparison?.ToLower();
            var fromDate = searchEntity.From;
            var toDate = searchEntity.To;

            if (string.IsNullOrWhiteSpace(fieldName))
            {
                _logger.LogError($"{nameof(VaultXDomainService)} => {nameof(PerformDateSearch)}: Field name is null or empty.");
                throw new ArgumentException("Date filter requires a valid field name.");
            }

            // Ensure field exists in HistoryInformation
            var historyProp = typeof(HistoryEntityModel).GetProperty(fieldName);
            if (historyProp == null || historyProp.PropertyType != typeof(DateTime?))
            {
                _logger.LogError($"{nameof(VaultXDomainService)} => {nameof(PerformDateSearch)}: Invalid or unsupported datetime field '{fieldName}'.");
                throw new Exception($"Invalid or unsupported datetime field: {fieldName}");
            }

            // c => c.HistoryInformation != null && c.HistoryInformation.<Field> [comparison] ...
            var parameter = Expression.Parameter(typeof(CredentialEntityModel), "c");

            // Access: c.HistoryInformation
            var historyAccess = Expression.PropertyOrField(parameter, nameof(CredentialEntityModel.HistoryInformation));

            // Null check: c.HistoryInformation != null
            var nullCheck = Expression.NotEqual(historyAccess, Expression.Constant(null));

            // Access: c.HistoryInformation.<FieldName>
            var dateProperty = Expression.Property(historyAccess, historyProp);

            Expression body;

            switch (comparison?.ToLower())
            {
                case DateSearchConstants.Exact:
                    body = Expression.Equal(dateProperty, Expression.Constant(fromDate, typeof(DateTime?)));
                    break;

                case DateSearchConstants.NotExact:
                    body = Expression.NotEqual(dateProperty, Expression.Constant(fromDate, typeof(DateTime?)));
                    break;

                case DateSearchConstants.Range:
                    var lowerBound = Expression.GreaterThanOrEqual(dateProperty, Expression.Constant(fromDate, typeof(DateTime?)));
                    var upperBound = Expression.LessThanOrEqual(dateProperty, Expression.Constant(toDate, typeof(DateTime?)));
                    body = Expression.AndAlso(lowerBound, upperBound);
                    break;

                case DateSearchConstants.NotInRange:
                    var outsideLower = Expression.LessThan(dateProperty, Expression.Constant(fromDate, typeof(DateTime?)));
                    var outsideUpper = Expression.GreaterThan(dateProperty, Expression.Constant(toDate, typeof(DateTime?)));
                    body = Expression.OrElse(outsideLower, outsideUpper);
                    break;

                default:
                    _logger.LogError($"{nameof(VaultXDomainService)} => {nameof(PerformDateSearch)}: Unsupported date comparison type '{comparison}'.");
                    throw new NotSupportedException($"Unsupported date comparison: {comparison}");
            }

            // Combine null check and comparison
            var finalExpression = Expression.AndAlso(nullCheck, body);

            var predicate = Expression.Lambda<Func<CredentialEntityModel, bool>>(finalExpression, parameter);

            entity = entity.Where(predicate.Compile()).ToList();
        }
    }

    #endregion Private methods
}