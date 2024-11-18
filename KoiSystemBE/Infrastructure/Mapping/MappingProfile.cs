using ApplicationCore.Dto;
using AutoMapper;
using Infrastructure.Dto;
using Infrastructure.Models;

namespace ApplicationCore.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region User Mapping
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, RegisterUserDto>().ReverseMap();
            CreateMap<User, LoginUserDto>().ReverseMap();
            CreateMap<User, UpdateUserProfileDto>().ReverseMap();
            CreateMap<User, ProfileDto>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.RoleName));
            #endregion

            #region Pricing
            CreateMap<Pricing, PriceDto>().ReverseMap();
            CreateMap<Pricing, UpdatePriceDto>().ReverseMap();
            #endregion

            #region Order Mapping
            CreateMap<KoiOrder, KoiOrderDto>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer != null ? src.Customer.Name : "Unknown"))
                .ForMember(dest => dest.PricingDetails, opt => opt.MapFrom(src => src.Pricing != null ? src.Pricing.TransportMethod + " - " + src.Pricing.PricePerKg : "N/A"));

            CreateMap<CreateKoiOrderDto, KoiOrder>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Pending")) 
                .ForMember(dest => dest.PlacedDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UpdateKoiOrderDto, KoiOrder>();
            #endregion

            #region OrderDetailMapping
            CreateMap<KoiOrder, KoiOrderDto>()
           .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name));
            CreateMap<CreateKoiOrderDto, KoiOrder>();
            CreateMap<UpdateKoiOrderDto, KoiOrder>();
            CreateMap<HealthCertificateDto, OrderDocument>()
                .ForMember(dest => dest.DocumentType, opt => opt.MapFrom(src => src.CertificateType))
                .ForMember(dest => dest.DocumentPath, opt => opt.MapFrom(src => src.CertificatePath));

             CreateMap<TransportProcess, TransportProcessDto>()
                .ForMember(dest => dest.ProcessId, opt => opt.MapFrom(src => src.ProcessId))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.ProcessDate, opt => opt.MapFrom(src => src.ProcessDate))
                .ForMember(dest => dest.Step, opt => opt.MapFrom(src => src.Step));
            #endregion

            #region Order Documents
            CreateMap<OrderDocument, OrderDocumentDto>().ReverseMap();
            CreateMap<HealthCertificateDto, OrderDocument>().ReverseMap();

            CreateMap<CreateOrderDocumentDto, OrderDocument>()
               .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<OrderDocument, CreateOrderDocumentDto>();
            CreateMap<UpdateOrderDocumentDto, OrderDocument>()
              .ForMember(dest => dest.DocumentId, opt => opt.Ignore()) 
              .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null)); 
            #endregion

            #region CMS Content
            CreateMap<CmsContent, CMSContentDto>()
                .ForMember(dest => dest.CreateByName, opt => opt.MapFrom(src => src.CreateByNavigation.Name))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreateBy))
                .ReverseMap();

            CreateMap<CmsContent, UpdateCMSContentDto>().ReverseMap();
            CreateMap<CmsContent, CreateCMSContentDto>().ReverseMap();
            #endregion

            #region Transport Process
            CreateMap<TransportProcess, TransportProcessDto>().ReverseMap();
            #endregion
        }
    }
}
