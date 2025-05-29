#region using directives
using AutoMapper;
using DevNest.Business.Domain.DomainContracts;
using DevNest.Business.Domain.RouterContracts;
using DevNest.Common.Base.Response;
using DevNest.Common.Logger;
using DevNest.Infrastructure.DTOs;
using DevNest.Infrastructure.Entity;
#endregion using directives

namespace DevNest.Business.Domain.Services
{
    /// <summary>
    /// Represents the class instance for credential manager services.
    /// </summary>
    public class CredManagerDomainService : ICredManagerDomainService
    {
        private readonly IAppLogger<CredManagerDomainService> _logger;
        private readonly ICredManagerReposRouter _router;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initialize the new instance for credential manager services.
        /// </summary>
        /// <param name="logger"></param>
        public CredManagerDomainService(
            IAppLogger<CredManagerDomainService> logger,
            ICredManagerReposRouter router,
            IMapper mapper)
        {
            _logger = logger;
            _router = router;
            _mapper = mapper;
        }

        /// <summary>
        /// Handler method for get credentials as DTO response.
        /// </summary>
        /// <returns></returns>
        public async Task<AppResponse<IEnumerable<CredentialDTO>>> Get()
        {
            var response = new AppResponse<IEnumerable<CredentialDTO>>() { Data = null, IsSuccess = false };
            try
            {
                IEnumerable<CredentialEntity>? _data = await _router.GetAsync();

                if (_data == null)
                {
                    return new AppResponse<IEnumerable<CredentialDTO>>()
                    {
                        Data = null,
                        IsSuccess = false,
                        Errors = new List<AppError>() { new AppError()
                        {
                            Code = Guid.NewGuid().ToString(),
                            Message = "No Credentials found"
                        } }
                    };
                }


                return new AppResponse<IEnumerable<CredentialDTO>>()
                {
                    Data = _mapper.Map<IEnumerable<CredentialDTO>>(_data),
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message,ex);
                var errors = new List<AppError>();
                errors.Add(new AppError()
                {
                    Code = Guid.NewGuid().ToString(),
                    Message = ex.Message
                });
                response.Errors = errors;
            }
            return response;
        }
    }
}