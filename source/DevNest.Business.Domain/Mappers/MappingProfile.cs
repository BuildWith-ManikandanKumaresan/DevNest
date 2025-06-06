#region using directives
using AutoMapper;
using DevNest.Common.Base.DTOs;
using DevNest.Common.Base.Entity;
using DevNest.Infrastructure.DTOs.CredentialManager.Request;
using DevNest.Infrastructure.DTOs.CredentialManager.Response;
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
            CreateMap<HistoryEntity,HistoryDTO>();
            CreateMap<HistoryDTO,HistoryEntity>();
            CreateMap<MetadataDTO,MetadataEntity>();
            CreateMap<MetadataEntity, MetadataDTO>();
            CreateMap<CredentialsDTO, CredentialEntity>();
            CreateMap<CredentialEntity, CredentialsDTO>();

            CreateMap<AddCredentialRequest, CredentialEntity>();
            CreateMap<CredentialEntity,AddCredentialRequest>();
        }
    }
}