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
using DevNest.Infrastructure.Entity;
using DevNest.Infrastructure.Entity.Configurations.CredentialManager;
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
    public class CredentialManagerDomainService(
        IApplicationLogger<CredentialManagerDomainService> logger,
        ICredentialManagerReposRouter router,
        IApplicationConfigService<CredentialManagerConfigurations> applicationConfigService,
        IMapper mapper) : ICredentialManagerDomainService
    {
        private readonly IApplicationLogger<CredentialManagerDomainService> _logger = logger;
        private readonly ICredentialManagerReposRouter _router = router;
        private readonly IMapper _mapper = mapper;
        private readonly IApplicationConfigService<CredentialManagerConfigurations> _applicationConfigService = applicationConfigService;

        /// <summary>
        /// Handler method for get credentials as DTO response.
        /// </summary>
        /// <returns></returns>
        public async Task<ApplicationResponse<IEnumerable<CredentialsDTO>>> Get()
        {
            try
            {
                var data = (await _router.GetAsync())?.ToList();
                if (data == null)
                    return ApplicationResponse<IEnumerable<CredentialsDTO>>.Fail(Messages.GetError(ErrorConstants.NoCredentialsFound));

                if (!_applicationConfigService.Value.GeneralSettings?.ShowArchivedCredentials ?? false)
                    data.RemoveAll(a => a.IsDisabled ?? false);

                foreach (var credential in data)
                    await MaskingPasswords(credential, _applicationConfigService.Value.GeneralSettings?.ShowPasswordAsMasked);

                return ApplicationResponse<IEnumerable<CredentialsDTO>>.Success(_mapper.Map<IEnumerable<CredentialsDTO>>(data));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return ApplicationResponse<IEnumerable<CredentialsDTO>>.Fail(Messages.DefaultError(ex));
            }
        }

        /// <summary>
        /// Handler method for get credentials by id as DTO response.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ApplicationResponse<CredentialsDTO>> GetById(Guid id)
        {
            try
            {
                var entity = await _router.GetByIdAsync(id);
                if (entity == null)
                    return ApplicationResponse<CredentialsDTO>.Fail(Messages.GetError(ErrorConstants.NoCredentialsFoundForTheId));

                await MaskingPasswords(entity, _applicationConfigService.Value.GeneralSettings?.ShowPasswordAsMasked);
                return ApplicationResponse<CredentialsDTO>.Success(_mapper.Map<CredentialsDTO>(entity));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return ApplicationResponse<CredentialsDTO>.Fail(Messages.DefaultError(ex));
            }
        }


        /// <summary>
        /// Handler method for delete all credentials and returns DTO response.
        /// </summary>
        /// <returns></returns>
        public async Task<ApplicationResponse<bool>> Delete()
        {
            try
            {
                var result = await _router.DeleteAsync();
                if (result == null)
                    return ApplicationResponse<bool>.Fail(Messages.GetError(ErrorConstants.DeleteCredentialsFailed_All));

                return ApplicationResponse<bool>.Success(
                    data: result,
                    message: Messages.GetSuccess(SuccessConstants.CredentialsDeletedSuccessfully));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return ApplicationResponse<bool>.Fail(Messages.DefaultError(ex));
            }
        }

        /// <summary>
        /// Handler method for delete credential using id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApplicationResponse<bool>> DeleteById(Guid id)
        {
            try
            {
                var result = await _router.DeleteByIdAsync(id);
                if (result == null)
                    return ApplicationResponse<bool>.Fail(Messages.GetError(ErrorConstants.DeleteCredentialsFailed_ById));

                return ApplicationResponse<bool>.Success(
                    data: result,
                    message: string.Format(
                        Messages.GetSuccess(SuccessConstants.CredentialsDeletedByIdSuccessfully),id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return ApplicationResponse<bool>.Fail(Messages.DefaultError(ex));
            }
        }

        /// <summary>
        /// Handler method to add the credentials entity.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApplicationResponse<CredentialsDTO>> Add(AddCredentialRequest? request)
        {
            try
            {
                var entity = _mapper.Map<CredentialEntity>(request);
                entity.Id = Guid.NewGuid();

                var result = await _router.AddAsync(entity);
                if (result == null)
                    return ApplicationResponse<CredentialsDTO>.Fail(Messages.GetError(ErrorConstants.CreateCredentialsFailed));

                return ApplicationResponse<CredentialsDTO>.Success( 
                    data: _mapper.Map<CredentialsDTO>(result),
                    message: string.Format(
                        Messages.GetSuccess(SuccessConstants.CredentialsCreatedSuccessfully),
                        string.IsNullOrEmpty(result.Title) ? result.Id : result.Title));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return ApplicationResponse<CredentialsDTO>.Fail(Messages.DefaultError(ex));
            }
        }

        /// <summary>
        /// Handler method to update the credentials entity.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ApplicationResponse<CredentialsDTO>> Update(UpdateCredentialRequest? request)
        {
            try
            {
                var entity = _mapper.Map<CredentialEntity>(request);
                var result = await _router.UpdateAsync(entity);

                if (result == null)
                    return ApplicationResponse<CredentialsDTO>.Fail(Messages.GetError(ErrorConstants.UpdateCredentialsFailed));

                return ApplicationResponse<CredentialsDTO>.Success(
                    data: _mapper.Map<CredentialsDTO>(result),
                    message: string.Format(
                        Messages.GetSuccess(SuccessConstants.CredentialsUpdatedSuccessfully), 
                        string.IsNullOrEmpty(result.Title) ? result.Id : result.Title));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return ApplicationResponse<CredentialsDTO>.Fail(Messages.DefaultError(ex));
            }
        }

        /// <summary>
        /// Handler method to archive a credential by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApplicationResponse<bool>> Archive(Guid id)
        {
            try
            {
                var result = await _router.ArchiveByIdAsync(id);
                if (result == null)
                    return ApplicationResponse<bool>.Fail(Messages.GetError(ErrorConstants.DeleteCredentialsFailed_ById));

                return ApplicationResponse<bool>.Success(
                    data: result, 
                    message: string.Format(
                        Messages.GetSuccess(SuccessConstants.CredentialsArchivedSuccessfully),
                        id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return ApplicationResponse<bool>.Fail(Messages.DefaultError(ex));
            }
        }


        /// <summary>
        /// Method to mask passwords in the credentials DTO.
        /// </summary>
        /// <param name="credentialsDTO"></param>
        /// <returns></returns>
        private async Task MaskingPasswords(CredentialEntity entity, bool? globalMaskingEnabled)
        {
            if (!string.IsNullOrEmpty(entity.Password) && (entity.IsPasswordMasked ?? globalMaskingEnabled ?? false))
            {
                var maskChar = _applicationConfigService.Value?.GeneralSettings.MaskingPlaceHolder?.FirstOrDefault() ?? '*';
                entity.Password = new string(maskChar, entity.Password.Length);
            }
        }
    }
}