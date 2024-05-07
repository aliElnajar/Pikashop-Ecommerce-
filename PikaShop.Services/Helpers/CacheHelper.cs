using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PikaShop.Data.Context.ContextEntities.Core;
using PikaShop.Services.Contracts;


namespace PikaShop.Services.Helpers
{
    public class CacheHelper
    {
        private IMemoryCache _cache;
        private IProductServices _productServices;

        private string DepartmentCacheKey = "DepartmentKey";
        private string PriceRangeCacheKey = "PriceKey";
        private string CategorySpecificationKey = "CategorySpecificationKey";
        private string ProductCachedKey = "ProductCached";

        private MemoryCacheEntryOptions cacheEntryOption = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(60))
            .SetPriority(CacheItemPriority.Normal);

        public CacheHelper(IMemoryCache cache, IProductServices productServices)
        {
            _cache = cache;
            _productServices = productServices;
        }



        public void GetDepartments(out List<DepartmentEntity> departments)
        {
            if (_cache.TryGetValue(DepartmentCacheKey, out departments))
            {
                return;
            }

            var depts = _productServices.UnitOfWork.Departments.GetAll().Include(d => d.Categories).ToList();
            _cache.Set(DepartmentCacheKey, depts, cacheEntryOption);
            departments = depts;
        }

        public void GetMaximumPriceRange(out double maxPrice)
        {
            if (_cache.TryGetValue(PriceRangeCacheKey, out maxPrice))
            {
                return;
            }

            maxPrice = _productServices.UnitOfWork.Products.GetAll().Select(p => p.Price).Max() + 1;
            _cache.Set(PriceRangeCacheKey, maxPrice, cacheEntryOption);
        }

        public void SetProductsCache(List<ProductEntity> products)
        {
            _cache.Set(ProductCachedKey, products, cacheEntryOption);
        }
        public void getProductsCache(out List<ProductEntity> Products)
        {
            if (_cache.TryGetValue(ProductCachedKey, out Products))
            {
                return;
            }
            Products = _productServices.UnitOfWork.Products.GetAll().ToList();
            _cache.Set(ProductCachedKey, Products, cacheEntryOption);
        }

        public void setCategorySpecification(Dictionary<string, List<string>> specification)
        {
            _cache.Set(CategorySpecificationKey, specification, cacheEntryOption);

        }
        public void getCategorySpecification(out Dictionary<string, List<string>> specification)
        {
            if (_cache.TryGetValue(CategorySpecificationKey, out specification)) { }
            else
            {
                specification = new Dictionary<string, List<string>>();
            }
        }


    }
}
