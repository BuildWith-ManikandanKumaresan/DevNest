#region using directives
using AutoMapper;
using DevNest.Business.Domain.Domains.Contracts;
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
    public class CredentialManagerDomainService : ICredentialManagerDomainService
    {
        private readonly IApplicationLogger<CredentialManagerDomainService> _logger;
        private readonly ICredentialManagerReposRouter _router;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initialize the new instance for credential manager services.
        /// </summary>
        /// <param name="logger"></param>
        public CredentialManagerDomainService(
            IApplicationLogger<CredentialManagerDomainService> logger,
            ICredentialManagerReposRouter router,
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
        public async Task<ApplicationResponse<IEnumerable<CredentialsDTO>>> Get()
        {
            var response = new ApplicationResponse<IEnumerable<CredentialsDTO>>() { Data = null, IsSuccess = false };
            try
            {
                IEnumerable<CredentialEntity>? _data = await _router.GetAsync();

                if (_data == null)
                {
                    return new ApplicationResponse<IEnumerable<CredentialsDTO>>()
                    {
                        Data = null,
                        IsSuccess = false,
                        Errors = new List<ApplicationErrors>() { new ApplicationErrors()
                        {
                            Code = Guid.NewGuid().ToString(),
                            Message = "No Credentials found"
                        } }
                    };
                }


                return new ApplicationResponse<IEnumerable<CredentialsDTO>>()
                {
                    Data = _mapper.Map<IEnumerable<CredentialsDTO>>(_data),
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message,ex);
                var errors = new List<ApplicationErrors>();
                errors.Add(new ApplicationErrors()
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