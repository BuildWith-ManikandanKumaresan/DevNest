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
                IEnumerable<CredentialEntity>? _data = await _router.GetAsync();

                if (_data == null)
                {
                    return new ApplicationResponse<IEnumerable<CredentialsDTO>>() { IsSuccess = false, Errors = [Messages.GetError(ErrorConstants.NoCredentialsFound)] };
                }

                return new ApplicationResponse<IEnumerable<CredentialsDTO>>()
                {
                    Data = _mapper.Map<IEnumerable<CredentialsDTO>>(_data),
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.Errors = [new ApplicationErrors() { Code = Messages.DefaultExceptionCode, Message = ex.Message}];
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
    }
}