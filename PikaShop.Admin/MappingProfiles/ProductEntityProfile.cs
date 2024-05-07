using AutoMapper;
using PikaShop.Admin.ViewModels;
using PikaShop.Data.Context.ContextEntities.Core;

namespace PikaShop.Admin.MappingProfiles
{
    public class ProductEntityProfile:Profile
    {
        public ProductEntityProfile()
        {
            CreateMap<ProductEntity, ProductViewModel>()
                .ForMember<string>(pvm => pvm.CategoryName, opt =>
                {
                    opt.MapFrom(p => p.Category != null ? p.Category.Name : "");
                    opt.NullSubstitute("");
                })
                .ForMember<ICollection<ProductSpecsViewModel>>(pvm => pvm.ProductSpecifications, opt =>
                {
                    opt.MapFrom(p => p.ProductSpecs);
                })
                .ReverseMap();
            ShouldMapField = _ => false;
        }
    }
}
