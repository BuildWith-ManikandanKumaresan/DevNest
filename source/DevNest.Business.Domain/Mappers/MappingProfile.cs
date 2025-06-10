#region using directives
using AutoMapper;
using DevNest.Common.Base.DTOs;
using DevNest.Common.Base.Entity;
using DevNest.Infrastructure.DTOs.CredentialManager.Request;
using DevNest.Infrastructure.DTOs.CredentialManager.Response;
using DevNest.Infrastructure.Entity.Credentials;
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
            CreateMap<HistoryEntityModel,HistoryDTO>();
            CreateMap<HistoryDTO,HistoryEntityModel>();

            CreateMap<MetadataDTO,MetadataEntityModel>();
            CreateMap<MetadataEntityModel, MetadataDTO>();

            CreateMap<CredentialsResponseDTO, CredentialEntityModel>();
            CreateMap<CredentialEntityModel, CredentialsResponseDTO>();

            CreateMap<AddCredentialRequest, CredentialEntityModel>();
            CreateMap<CredentialEntityModel,AddCredentialRequest>();

            CreateMap<UpdateCredentialRequest, CredentialEntityModel>();
            CreateMap<CredentialEntityModel, UpdateCredentialRequest>();
        }
    }
}