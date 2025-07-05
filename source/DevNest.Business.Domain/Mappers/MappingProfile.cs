#region using directives
using AutoMapper;
using DevNest.Common.Base.DTOs;
using DevNest.Common.Base.Entity;
using DevNest.Infrastructure.DTOs.VaultX.Request;
using DevNest.Infrastructure.DTOs.VaultX.Response;
using DevNest.Infrastructure.DTOs.Search;
using DevNest.Infrastructure.DTOs.TaggingX;
using DevNest.Infrastructure.DTOs.VaultX.Request;
using DevNest.Infrastructure.Entity.Search;
using DevNest.Infrastructure.Entity.TaggingX;
using DevNest.Infrastructure.Entity.VaultX;
using DevNest.Infrastructure.Entity.Configurations.VaultX;
using DevNest.Infrastructure.DTOs.Configurations.VaultX.Request;
using DevNest.Infrastructure.DTOs.Configurations.VaultX.Response;
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
            CreateMap<HistoryEntityModel,HistoryResponseDTO>();
            CreateMap<HistoryResponseDTO,HistoryEntityModel>();

            CreateMap<MetadataResponseDTO,MetadataEntityModel>();
            CreateMap<MetadataEntityModel, MetadataResponseDTO>();

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

            CreateMap<TagEntityModel, TagResponseDTO>();
            CreateMap<TagResponseDTO, TagEntityModel>();

            CreateMap<TagColorsEntityModel, TagColorsResponseDTO>();
            CreateMap<TagColorsResponseDTO, TagColorsEntityModel>();

            CreateMap<DateSearchRequestDTO, DateSearchEntityModel>();
            CreateMap<DateSearchEntityModel, DateSearchRequestDTO>();

            CreateMap<TextSearchRequestDTO, TextSearchEntityModel>();
            CreateMap<TextSearchEntityModel, TextSearchRequestDTO>();

            CreateMap<SearchRequestDTO, SearchEntityModel>();
            CreateMap<SearchEntityModel, SearchRequestDTO>();

            CreateMap<TypesEntityModel, TypesResponseDTO>();
            CreateMap<TypesResponseDTO, TypesEntityModel>();

            CreateMap<CategoryEntityModel, CategoryResponseDTO>();
            CreateMap<CategoryResponseDTO, CategoryEntityModel>();

            CreateMap<BackupSettingsEntityModel, BackupSettingsResponseDTO>();
            CreateMap<BackupSettingsResponseDTO, BackupSettingsEntityModel>();

            CreateMap<EncryptionProviderEntityModel, EncryptionProviderResponseDTO>();
            CreateMap<EncryptionProviderResponseDTO, EncryptionProviderEntityModel>();

            CreateMap<GeneralSettingsEntityModel, GeneralSettingsResponseDTO>();
            CreateMap<GeneralSettingsResponseDTO, GeneralSettingsEntityModel>();

            CreateMap<SecuritySettingsEntityModel, SecuritySettingsResponseDTO>();
            CreateMap<SecuritySettingsResponseDTO, SecuritySettingsEntityModel>();

            CreateMap<StoreProviderEntityModel, StoreProviderResponseDTO>();
            CreateMap<StoreProviderResponseDTO, StoreProviderEntityModel>();

            CreateMap<VaultXConfigurationsEntityModel, VaultXConfigurationsResponseDTO>();
            CreateMap<VaultXConfigurationsResponseDTO, VaultXConfigurationsEntityModel>();

            CreateMap<BackupSettingsRequestDTO, BackupSettingsEntityModel>();
            CreateMap<BackupSettingsEntityModel, BackupSettingsRequestDTO>();

            CreateMap<EncryptionProviderRequestDTO, EncryptionProviderEntityModel>();
            CreateMap<EncryptionProviderEntityModel, EncryptionProviderRequestDTO>();

            CreateMap<GeneralSettingsRequestDTO, GeneralSettingsEntityModel>();
            CreateMap<GeneralSettingsEntityModel, GeneralSettingsRequestDTO>();

            CreateMap<SecuritySettingsRequestDTO, SecuritySettingsEntityModel>();
            CreateMap<SecuritySettingsEntityModel, SecuritySettingsRequestDTO>();

            CreateMap<StoreProviderRequestDTO, StoreProviderEntityModel>();
            CreateMap<StoreProviderEntityModel, StoreProviderRequestDTO>();

            CreateMap<UpdateVaultXConfigurationsRequestDTO, VaultXConfigurationsEntityModel>();
            CreateMap<VaultXConfigurationsEntityModel, UpdateVaultXConfigurationsRequestDTO>();

        }
    }
}