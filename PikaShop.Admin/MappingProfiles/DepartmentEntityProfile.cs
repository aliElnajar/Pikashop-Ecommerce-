using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using PikaShop.Admin.ViewModels;
using PikaShop.Data.Context.ContextEntities.Core;

namespace PikaShop.Admin.MappingProfiles
{
    public class DepartmentEntityProfile : Profile
    {
        public DepartmentEntityProfile()
        {
            CreateMap<DepartmentEntity, DepartmentViewModel>().ReverseMap();

            CreateMap<DepartmentEntity, SelectListItem>()
                .ForMember(
                    selectListItem => selectListItem.Value,
                    opt => opt.MapFrom(department => department.ID))
                .ForMember(
                    selectListItem => selectListItem.Text,
                    opt => opt.MapFrom(department => department.Name))
                .ReverseMap();
        }
    }
}
