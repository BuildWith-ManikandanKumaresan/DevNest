#region using directives
using AutoMapper;
using DevNest.Business.Domain.Domains.Contracts;
using DevNest.Business.Domain.RouterContracts;
using DevNest.Common.Base.Constants.Message;
using DevNest.Common.Base.Contracts;
using DevNest.Common.Base.Response;
using DevNest.Common.Logger;
using DevNest.Infrastructure.DTOs.CredentialManager.Request;
using DevNest.Infrastructure.DTOs.CredentialManager.Response;
using DevNest.Infrastructure.Entity.Configurations.CredentialManager;
using DevNest.Infrastructure.Entity.Credentials;
using MediatR;
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
    public class CredentialDomainService(
        IAppLogger<CredentialDomainService> logger,
        ICredentialRepositoryRouter router,
        IAppConfigService<CredentialManagerConfigurations> applicationConfigService,
        IMapper mapper) : ICredentialDomainService
    {
        private readonly IAppLogger<CredentialDomainService> _logger = logger;
        private readonly ICredentialRepositoryRouter _router = router;
        private readonly IMapper _mapper = mapper;
        private readonly IAppConfigService<CredentialManagerConfigurations> _applicationConfigService = applicationConfigService;

        /// <summary>
        /// Handler method for get credentials as DTO response.
        /// </summary>
        /// <returns></returns>
        public async Task<AppResponse<IEnumerable<CredentialsResponseDTO>>> Get()
        {
            try
            {
                _logger.LogDebug($"{nameof(CredentialDomainService)} => {nameof(Get)} method called.");

                var data = (await _router.GetAsync())?.ToList();
                if (data == null)
                {
                    _logger.LogDebug($"{nameof(CredentialDomainService)} => {nameof(Get)} method returned null data.");
                    return new AppResponse<IEnumerable<CredentialsResponseDTO>>(Messages.GetError(ErrorConstants.NoCredentialsFound));
                }

                if (!_applicationConfigService?.Value?.GeneralSettings?.ShowArchivedCredentials ?? false)
                    data.RemoveAll(a => a.IsDisabled ?? false);

                foreach (var credential in data)
                    await MaskingPasswords(credential, _applicationConfigService?.Value?.GeneralSettings?.ShowPasswordAsMasked);

                _logger.LogDebug($"{nameof(CredentialDomainService)} => {nameof(Get)} method returned {data.Count} credentials.");
                return new AppResponse<IEnumerable<CredentialsResponseDTO>>(_mapper.Map<IEnumerable<CredentialsResponseDTO>>(data));
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(CredentialDomainService)} => {ex.Message}", ex);
                return new AppResponse<IEnumerable<CredentialsResponseDTO>>(new AppErrors { Code = ErrorConstants.UndefinedErrorCode, Message = ex.Message });
            }
        }

        /// <summary>
        /// Handler method for get credentials by id as DTO response.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<AppResponse<CredentialsResponseDTO>> GetById(Guid id)
        {
            try
            {
                _logger.LogDebug($"{nameof(CredentialDomainService)} => {nameof(GetById)} method called with ID: {id}.");
                var entity = await _router.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogDebug($"{nameof(CredentialDomainService)} => {nameof(GetById)} method returned null for ID: {id}.");
                    return new AppResponse<CredentialsResponseDTO>(Messages.GetError(ErrorConstants.NoCredentialsFoundForTheId));
                }

                await MaskingPasswords(entity, _applicationConfigService?.Value?.GeneralSettings?.ShowPasswordAsMasked);
                _logger.LogDebug($"{nameof(CredentialDomainService)} => {nameof(GetById)} method returned credentials for ID: {id}.");
                return new AppResponse<CredentialsResponseDTO>(_mapper.Map<CredentialsResponseDTO>(entity));
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(CredentialDomainService)} => {ex.Message}", ex);
                return new AppResponse<CredentialsResponseDTO>(new AppErrors { Code = ErrorConstants.UndefinedErrorCode, Message = ex.Message });
            }
        }


        /// <summary>
        /// Handler method for delete all credentials and returns DTO response.
        /// </summary>
        /// <returns></returns>
        public async Task<AppResponse<bool>> Delete()
        {
            try
            {
                _logger.LogDebug($"{nameof(CredentialDomainService)} => {nameof(Delete)} method called to delete all credentials.");
                var result = await _router.DeleteAsync();
                if (result == false)
                {
                    _logger.LogDebug($"{nameof(CredentialDomainService)} => {nameof(Delete)} method failed to delete all credentials.");
                    return new AppResponse<bool>(Messages.GetError(ErrorConstants.DeleteCredentialsFailed_All));
                }
                _logger.LogDebug($"{nameof(CredentialDomainService)} => {nameof(Delete)} method successfully deleted all credentials.");
                return new AppResponse<bool>(
                    data: result,
                    message: Messages.GetSuccess(SuccessConstants.CredentialsDeletedSuccessfully));
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(CredentialDomainService)} => {ex.Message}", ex);
                return new AppResponse<bool>(new AppErrors { Code = ErrorConstants.UndefinedErrorCode, Message = ex.Message });
            }
        }

        /// <summary>
        /// Handler method for delete credential using id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AppResponse<bool>> DeleteById(Guid id)
        {
            try
            {
                _logger.LogDebug($"{nameof(CredentialDomainService)} => {nameof(DeleteById)} method called with ID: {id}.");
                var result = await _router.DeleteByIdAsync(id);
                if (result == false)
                {
                    _logger.LogDebug($"{nameof(CredentialDomainService)} => {nameof(DeleteById)} method failed to delete credential with ID: {id}.");
                    return new AppResponse<bool>(Messages.GetError(ErrorConstants.DeleteCredentialsFailed_ById));
                }

                _logger.LogDebug($"{nameof(CredentialDomainService)} => {nameof(DeleteById)} method successfully deleted credential with ID: {id}.");
                return new AppResponse<bool>(
                    data: result,
                    message: string.Format(
                        Messages.GetSuccess(SuccessConstants.CredentialsDeletedByIdSuccessfully),id));
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(CredentialDomainService)} => {ex.Message}", ex);
                return new AppResponse<bool>(new AppErrors { Code = ErrorConstants.UndefinedErrorCode, Message = ex.Message });
            }
        }

        /// <summary>
        /// Handler method to add the credentials entity.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<AppResponse<CredentialsResponseDTO>> Add(AddCredentialRequest? request)
        {
            try
            {
                _logger.LogDebug($"{nameof(CredentialDomainService)} => {nameof(Add)} method called with request: {request}.");
                var entity = _mapper.Map<CredentialEntityModel>(request);
                entity.Id = Guid.NewGuid();

                var result = await _router.AddAsync(entity);
                if (result == null)
                {
                    _logger.LogDebug($"{nameof(CredentialDomainService)} => {nameof(Add)} method failed to create credentials.");
                    return new AppResponse<CredentialsResponseDTO>(Messages.GetError(ErrorConstants.CreateCredentialsFailed));
                }

                _logger.LogDebug($"{nameof(CredentialDomainService)} => {nameof(Add)} method successfully created credentials with ID: {result.Id}.");
                return new AppResponse<CredentialsResponseDTO>( 
                    data: _mapper.Map<CredentialsResponseDTO>(result),
                    message: string.Format(
                        Messages.GetSuccess(SuccessConstants.CredentialsCreatedSuccessfully),
                        string.IsNullOrEmpty(result.Title) ? result.Id : result.Title));
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(CredentialDomainService)} => {ex.Message}", ex);
                return new AppResponse<CredentialsResponseDTO>(new AppErrors { Code = ErrorConstants.UndefinedErrorCode, Message = ex.Message });
            }
        }

        /// <summary>
        /// Handler method to update the credentials entity.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<AppResponse<CredentialsResponseDTO>> Update(UpdateCredentialRequest? request)
        {
            try
            {
                _logger.LogDebug($"{nameof(CredentialDomainService)} => {nameof(Update)} method called with request: {request}.");
                var entity = _mapper.Map<CredentialEntityModel>(request);
                var result = await _router.UpdateAsync(entity);

                if (result == null)
                {
                    _logger.LogDebug($"{nameof(CredentialDomainService)} => {nameof(Update)} method failed to update credentials.");
                    return new AppResponse<CredentialsResponseDTO>(Messages.GetError(ErrorConstants.UpdateCredentialsFailed));
                }

                _logger.LogDebug($"{nameof(CredentialDomainService)} => {nameof(Update)} method successfully updated credentials with ID: {result.Id}.");
                return new AppResponse<CredentialsResponseDTO>(
                    data: _mapper.Map<CredentialsResponseDTO>(result),
                    message: string.Format(
                        Messages.GetSuccess(SuccessConstants.CredentialsUpdatedSuccessfully), 
                        string.IsNullOrEmpty(result.Title) ? result.Id : result.Title));
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(CredentialDomainService)} => {ex.Message}", ex);
                return new AppResponse<CredentialsResponseDTO>(new AppErrors
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
        public async Task<AppResponse<bool>> Archive(Guid id)
        {
            try
            {
                _logger.LogDebug($"{nameof(CredentialDomainService)} => {nameof(Archive)} method called with ID: {id}.");
                var result = await _router.ArchiveByIdAsync(id);
                if (result == false)
                {
                    _logger.LogDebug($"{nameof(CredentialDomainService)} => {nameof(Archive)} method failed to archive credential with ID: {id}.");
                    return new AppResponse<bool>(Messages.GetError(ErrorConstants.DeleteCredentialsFailed_ById));
                }

                _logger.LogDebug($"{nameof(CredentialDomainService)} => {nameof(Archive)} method successfully archived credential with ID: {id}.");
                return  new AppResponse<bool>(
                    data: result, 
                    message: string.Format(
                        Messages.GetSuccess(SuccessConstants.CredentialsArchivedSuccessfully),
                        id));
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(CredentialDomainService)} => {ex.Message}", ex);
                return new AppResponse<bool>(new AppErrors
                {
                    Code = ErrorConstants.UndefinedErrorCode,
                    Message = ex.Message
                });
            }
        }


        /// <summary>
        /// Method to mask passwords in the credentials DTO.
        /// </summary>
        /// <param name="credentialsDTO"></param>
        /// <returns></returns>
        private async Task MaskingPasswords(CredentialEntityModel entity, bool? globalMaskingEnabled)
        {
            if (!string.IsNullOrEmpty(entity.Password) && (entity.IsPasswordMasked ?? globalMaskingEnabled ?? false))
            {
                var maskChar = _applicationConfigService.Value?.GeneralSettings?.MaskingPlaceHolder?.FirstOrDefault() ?? '*';
                entity.Password = new string(maskChar, entity.Password.Length);
            }
        }
    }
}