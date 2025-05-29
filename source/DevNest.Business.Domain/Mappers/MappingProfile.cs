#region using directives
using AutoMapper;
using DevNest.Infrastructure.DTOs;
using DevNest.Infrastructure.Entity;
#endregion using directives

namespace DevNest.Business.Domain.Mappers
{
    /// <summary>
    /// Represents the class instance for mapping profile class for automapper.
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Initialize the new instance for mapping profile class.
        /// </summary>
        public MappingProfile() 
        {
            CreateMap<CredentialDTO, CredentialEntity>();
            CreateMap<CredentialEntity, CredentialDTO>();
        }
    }
}