using Microsoft.EntityFrameworkCore;
using PikaShop.Data.Context.ContextEntities.Core;
using PikaShop.Data.Contracts.UnitsOfWork;
using PikaShop.Services.Contracts;
using PikaShop.Services.Helpers;

namespace PikaShop.Services.Core
{
    public class ProductServices(IUnitOfWork _uow) : IProductServices
    {
        public IUnitOfWork UnitOfWork { get; set; } = _uow;

        public List<ProductEntity> FilterBySpecifications(Dictionary<string, string[]> Specifications, List<ProductEntity> Products)
        {
            var filteredProducts = Products.Where(p =>
            {
                foreach (var productSpec in p.ProductSpecs)
                {
                    if (Specifications.ContainsKey(productSpec.Key) && Specifications[productSpec.Key].Contains(productSpec.Value, StringComparer.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }

                return false;
            }).ToList();

            return filteredProducts.Any()?filteredProducts:Products;
        }

        public List<ProductEntity> GetCategoryProducts(int? categoryId)
        {
            if (categoryId.HasValue)
            {
                return UnitOfWork.Products.GetAll().Where(p => p.CategoryID == categoryId).Include(p=>p.ProductSpecs).ToList();           
            }
            else
            {
                return UnitOfWork.Products.GetAll().ToList();    
            }
        }



        public Dictionary<string, List<string>> GetCategorySpecs(int? categoryId, List<ProductEntity> products)
        {
            if (categoryId == null) { return new Dictionary<string, List<string>>() ; }
         
            var categorySpecifications = UnitOfWork.Categories.GetAll()
                       .Include(c => c.CategorySpecs)
                       .FirstOrDefault(c => c.ID == categoryId)?.CategorySpecs;

            return FunctionHelpers.GetSpecificationsByCategory(categorySpecifications, products);
            

        }

        public List<ProductEntity> SearchByName(string name)
        {
           return UnitOfWork.Products.GetAll()
             .Where(p => p.Name.Contains(name))
              .ToList();
        }

        public List<ProductEntity> SearchByPriceRange(double PriceRange,List<ProductEntity> Products)
        {

           return Products.Where(p => p.Price <= PriceRange).ToList();

        }

        public List<ProductEntity> SortProducts(string sortBy, List<ProductEntity> products)
        {
            products = sortBy switch
            {
                "NameAscending" => products.OrderBy(p => p.Name).ToList(),
                "NameDescending" => products.OrderByDescending(p => p.Name).ToList(),
                "PriceAscending" => products.OrderBy(p => p.Price).ToList(),
                "PriceDescending" => products.OrderByDescending(p => p.Price).ToList(),
            };
            return products;
        }
        //ProductEntity GetById(int id)
        //{
        //    UnitOfWork.Products.GetById(id).
        //}



    }
}