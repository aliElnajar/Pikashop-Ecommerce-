using AutoMapper;
using PikaShop.Admin.ViewModels;
using PikaShop.Data.Context.ContextEntities.Core;

namespace PikaShop.Admin.MappingProfiles
{
    public class ProductSpecsEntityProfile:Profile
    {
        public ProductSpecsEntityProfile()
        {
            CreateMap<ProductSpecsEntity, ProductSpecsViewModel>()
                .ForMember<string>(psvm => psvm.ProductName, opt =>
                {
                    opt.MapFrom(p => p.Product != null ? p.Product.Name : "");
                    opt.NullSubstitute("");
                })
                .ReverseMap();
            ShouldMapField = _ => false;
        }
    }
}
