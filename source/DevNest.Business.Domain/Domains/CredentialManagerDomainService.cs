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

namespace DevNest.Business.Domain.Services
{
    /// <summary>
    /// Represents the class instance for credential manager services.
    /// </summary>
    public class CredentialManagerDomainService : ICredentialManagerDomainService
    {
        private readonly IApplicationLogger<CredentialManagerDomainService> _logger;
        private readonly ICredentialManagerReposRouter _router;
        private readonly IMapper _mapper;
        private readonly IApplicationConfigService<CredentialManagerConfigurations> _applicationConfigService;
        /// <summary>
        /// Initialize the new instance for credential manager services.
        /// </summary>
        /// <param name="logger"></param>
        public CredentialManagerDomainService(
            IApplicationLogger<CredentialManagerDomainService> logger,
            ICredentialManagerReposRouter router,
            IApplicationConfigService<CredentialManagerConfigurations> applicationConfigService,
            IMapper mapper)
        {
            _logger = logger;
            _router = router;
            _mapper = mapper;
            _applicationConfigService = applicationConfigService;
        }

        /// <summary>
        /// Handler method for get credentials as DTO response.
        /// </summary>
        /// <returns></returns>
        public async Task<ApplicationResponse<IEnumerable<CredentialsDTO>>> Get()
        {
            var response = new ApplicationResponse<IEnumerable<CredentialsDTO>>() { Data = null, IsSuccess = false };
            try
            {
                List<CredentialEntity>? _data = _router.GetAsync().GetAwaiter().GetResult()?.ToList();

                if (_data == null)
                {
                    return new ApplicationResponse<IEnumerable<CredentialsDTO>>() { IsSuccess = false, Errors = [Messages.GetError(ErrorConstants.NoCredentialsFound)] };
                }

                // Show archived credentials.
                if (!_applicationConfigService.Value?.ShowArchivedCredentials ?? false)
                {
                    _data.RemoveAll(a => a.IsDisabled == true);
                }

                // Masking credentials
                _data.ForEach(async credential => await MaskingPasswords(credential, _applicationConfigService.Value?.ShowPasswordAsMasked));

                return new ApplicationResponse<IEnumerable<CredentialsDTO>>()
                {
                    Data = _mapper.Map<IEnumerable<CredentialsDTO>>(_data),
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.Errors = [new ApplicationErrors() { Code = Messages.DefaultExceptionCode, Message = ex.Message }];
            }
            return response;
        }

        /// <summary>
        /// Handler method for get credentials by id as DTO response.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ApplicationResponse<CredentialsDTO>> GetById(Guid id)
        {
            var response = new ApplicationResponse<CredentialsDTO>() { Data = null, IsSuccess = false };
            try
            {
                CredentialEntity? _data = await _router.GetByIdAsync(id);

                if (_data == null)
                {
                    return new ApplicationResponse<CredentialsDTO>() { IsSuccess = false, Errors = [Messages.GetError(ErrorConstants.NoCredentialsFoundForTheId)] };
                }

                // Masking credentials
                await MaskingPasswords(_data, _applicationConfigService.Value?.ShowPasswordAsMasked);

                return new ApplicationResponse<CredentialsDTO>
                {
                    Data = _mapper.Map<CredentialsDTO>(_data),
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.Errors = [new ApplicationErrors() { Code = Messages.DefaultExceptionCode, Message = ex.Message }];
            }
            return response;
        }


        /// <summary>
        /// Handler method for delete all credentials and returns DTO response.
        /// </summary>
        /// <returns></returns>
        public async Task<ApplicationResponse<bool>> Delete()
        {
            var response = new ApplicationResponse<bool>() { Data = false, IsSuccess = false };
            try
            {
                bool? _data = await _router.DeleteAsync();

                if (_data == null)
                {
                    return new ApplicationResponse<bool>() { IsSuccess = false, Errors = [Messages.GetError(ErrorConstants.DeleteCredentialsFailed_All)] };
                }

                return new ApplicationResponse<bool>
                {
                    Data = _mapper.Map<bool>(_data),
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.Errors = [new ApplicationErrors() { Code = Messages.DefaultExceptionCode, Message = ex.Message }];
            }
            return response;
        }

        /// <summary>
        /// Handler method for delete credential using id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApplicationResponse<bool>> DeleteById(Guid id)
        {
            var response = new ApplicationResponse<bool>() { Data = false, IsSuccess = false };
            try
            {
                bool? _data = await _router.DeleteByIdAsync(id);

                if (_data == null)
                {
                    return new ApplicationResponse<bool>() { IsSuccess = false, Errors = [Messages.GetError(ErrorConstants.DeleteCredentialsFailed_ById)] };
                }

                return new ApplicationResponse<bool>
                {
                    Data = _mapper.Map<bool>(_data),
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.Errors = [new ApplicationErrors() { Code = Messages.DefaultExceptionCode, Message = ex.Message }];
            }
            return response;

        }

        /// <summary>
        /// Handler method to add the credentials entity.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApplicationResponse<CredentialsDTO>> Add(AddCredentialRequest? request)
        {
            var response = new ApplicationResponse<CredentialsDTO>() { Data = null, IsSuccess = false };
            try
            {
                CredentialEntity entity = _mapper.Map<CredentialEntity>(request);
                entity.Id = Guid.NewGuid();
                CredentialEntity? _data = await _router.AddAsync(entity);

                if (_data == null)
                {
                    return new ApplicationResponse<CredentialsDTO>() { IsSuccess = false, Errors = [Messages.GetError(ErrorConstants.CreateCredentialsFailed)] };
                }

                return new ApplicationResponse<CredentialsDTO>
                {
                    Data = _mapper.Map<CredentialsDTO>(_data),
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.Errors = [new ApplicationErrors() { Code = Messages.DefaultExceptionCode, Message = ex.Message }];
            }
            return response;
        }

        /// <summary>
        /// Handler method to update the credentials entity.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ApplicationResponse<CredentialsDTO>> Update(UpdateCredentialRequest? request)
        {
            var response = new ApplicationResponse<CredentialsDTO>() { Data = null, IsSuccess = false };
            try
            {
                CredentialEntity entity = _mapper.Map<CredentialEntity>(request);
                CredentialEntity? _data = await _router.UpdateAsync(entity);

                if (_data == null)
                {
                    return new ApplicationResponse<CredentialsDTO>() { IsSuccess = false, Errors = [Messages.GetError(ErrorConstants.UpdateCredentialsFailed)] };
                }

                return new ApplicationResponse<CredentialsDTO>
                {
                    Data = _mapper.Map<CredentialsDTO>(_data),
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.Errors = [new ApplicationErrors() { Code = Messages.DefaultExceptionCode, Message = ex.Message }];
            }
            return response;
        }

        /// <summary>
        /// Handler method to archive a credential by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApplicationResponse<bool>> Archive(Guid id)
        {

            var response = new ApplicationResponse<bool>() { Data = false, IsSuccess = false };
            try
            {
                bool? _data = await _router.ArchiveByIdAsync(id);

                if (_data == null)
                {
                    return new ApplicationResponse<bool>() { IsSuccess = false, Errors = [Messages.GetError(ErrorConstants.DeleteCredentialsFailed_ById)] };
                }

                return new ApplicationResponse<bool>
                {
                    Data = _mapper.Map<bool>(_data),
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.Errors = [new ApplicationErrors() { Code = Messages.DefaultExceptionCode, Message = ex.Message }];
            }
            return response;
        }


        /// <summary>
        /// Method to mask passwords in the credentials DTO.
        /// </summary>
        /// <param name="credentialsDTO"></param>
        /// <returns></returns>
        private async Task MaskingPasswords(CredentialEntity credentialsDTO, bool? masking_GlobalConfiguration)
        {
            if (!string.IsNullOrEmpty(credentialsDTO.Password))
            {
                if ((credentialsDTO.IsPasswordMasked ?? false))
                {
                    credentialsDTO.Password = new string(char.Parse(_applicationConfigService.Value?.MaskingPlaceHolder ?? string.Empty), 
                        credentialsDTO.Password.Length);
                }
                else if (masking_GlobalConfiguration ?? false)
                {
                    credentialsDTO.Password = new string(char.Parse(_applicationConfigService.Value?.MaskingPlaceHolder ?? string.Empty), 
                        credentialsDTO.Password.Length);
                }
                else
                {
                    credentialsDTO.Password = credentialsDTO.Password;
                }
            }
        }
    }
}