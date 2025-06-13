#region using directives
using AutoMapper;
using DevNest.Common.Base.DTOs;
using DevNest.Common.Base.Entity;
using DevNest.Infrastructure.DTOs.Credential.Request;
using DevNest.Infrastructure.DTOs.Credential.Response;
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

            CreateMap<CredentialResponseDTO, CredentialEntityModel>();
            CreateMap<CredentialEntityModel, CredentialResponseDTO>();

            CreateMap<PasswordHealthEntityModel, PasswordHealthResponseDTO>();
            CreateMap<PasswordHealthResponseDTO, PasswordHealthEntityModel>();

            CreateMap<CredentialDetailsEntityModel, CredentialDetailsResponseDTO>();
            CreateMap<CredentialDetailsResponseDTO, CredentialDetailsEntityModel>();

            CreateMap<CredentialDetailsRequest, CredentialDetailsEntityModel>();
            CreateMap<CredentialDetailsEntityModel, CredentialDetailsRequest>();

            CreateMap<SecurityDetailsRequest, SecurityEntityModel>();
            CreateMap<SecurityEntityModel, SecurityDetailsRequest>();

            CreateMap<ValidityDetailsRequest, ValidityEntityModel>();
            CreateMap<ValidityEntityModel, ValidityDetailsRequest>();

            CreateMap<SecurityEntityModel, SecurityResponseDTO>();
            CreateMap<SecurityResponseDTO, SecurityEntityModel>();

            CreateMap<ValidityEntityModel, ValidityResponseDTO>();
            CreateMap<ValidityResponseDTO, ValidityEntityModel>();

            CreateMap<AddCredentialRequest, CredentialEntityModel>();
            CreateMap<CredentialEntityModel,AddCredentialRequest>();

            CreateMap<UpdateCredentialRequest, CredentialEntityModel>();
            CreateMap<CredentialEntityModel, UpdateCredentialRequest>();
        }
    }
}