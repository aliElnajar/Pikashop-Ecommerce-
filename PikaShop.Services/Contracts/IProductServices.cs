using PikaShop.Data.Context.ContextEntities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PikaShop.Services.Contracts
{
    public interface IProductServices : IServices
    {
        List<ProductEntity> GetCategoryProducts(int? categoryId);

        Dictionary<string,List<string>> GetCategorySpecs(int? categoryId, List<ProductEntity> products);

        List<ProductEntity> SearchByName(string name);

        List<ProductEntity> SearchByPriceRange(double PriceRange,List<ProductEntity> Products);
        List<ProductEntity> SortProducts(string sortBy, List<ProductEntity> Products);
        List<ProductEntity> FilterBySpecifications(Dictionary<string, string[]> Specifications, List<ProductEntity> Products);

    }
}
