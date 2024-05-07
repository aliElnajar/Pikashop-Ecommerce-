using AutoMapper;
using PikaShop.Admin.Areas.SuperAdminPanel.ViewModel;
using PikaShop.Data.Context.ContextEntities.Identity;

namespace PikaShop.Admin.MappingProfiles
{
    public class ApplicationUserEntityProfile : Profile
    {
        public ApplicationUserEntityProfile()
        {
            CreateMap<ApplicationUserEntity, ApplicationUserEntityViewModel>()
                .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName,
                opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email,
                opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FirstName,
                opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName,
                opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.OldPassword,
                opt => opt.MapFrom(src => ""))
                .ForMember(dest => dest.Password,
                opt => opt.MapFrom(src => ""))
                .ForMember(dest => dest.ConfirmPassword,
                opt => opt.MapFrom(src => ""));

        }
    }
}
