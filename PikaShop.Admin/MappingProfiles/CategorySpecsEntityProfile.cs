using AutoMapper;
using PikaShop.Admin.ViewModels;
using PikaShop.Data.Context.ContextEntities.Core;

namespace PikaShop.Admin.MappingProfiles
{
    public class CategorySpecsEntityProfile:Profile
    {
        public CategorySpecsEntityProfile()
        {
            CreateMap<CategorySpecsEntity, CategorySpecsViewModel>()
                .ForMember<string>(csvm => csvm.CategoryName, opt =>
                {
                    opt.MapFrom(c => c.Category != null ? c.Category.Name : "");
                    opt.NullSubstitute("");
                })
                .ReverseMap();
            ShouldMapField = _ => false;
        }
    }
}
