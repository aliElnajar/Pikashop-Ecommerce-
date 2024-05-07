
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using PikaShop.Admin.ViewModels;
using PikaShop.Common.Pagination;
using PikaShop.Data.Context.ContextEntities.Core;
namespace PikaShop.Admin.MappingProfiles
{
    public class CategoryEntityProfile:Profile
    {
        public CategoryEntityProfile()
        {
            CreateMap<CategoryEntity, CategoryViewModel>()
                .ForMember<string>(cvm => cvm.DepartmentName, opt => 
                {
                    opt.MapFrom(c => c.Department != null ? c.Department.Name : "");
                    opt.NullSubstitute("");
                })
                .ForMember<ICollection<CategorySpecsViewModel>>(cvm=>cvm.CategorySpecifications, opt =>
                {
                    opt.MapFrom(c => c.CategorySpecs);
                })
                .ReverseMap();
            CreateMap<CategoryEntity, SelectListItem>()
                .ForMember(
                    selectListItem => selectListItem.Value,
                    opt => opt.MapFrom(category => category.ID))
                .ForMember(
                    selectListItem => selectListItem.Text,
                    opt => opt.MapFrom(category => category.Name))
                .ReverseMap();
            ShouldMapField = _ => false;
        }
    }
}
