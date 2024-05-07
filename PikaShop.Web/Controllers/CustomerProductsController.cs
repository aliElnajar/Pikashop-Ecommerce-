using Microsoft.AspNetCore.Mvc;

using PikaShop.Data.Context.ContextEntities.Core;
using PikaShop.Services.Contracts;
using PikaShop.Web.ViewModels;

using PikaShop.Services.Helpers;
using System.Diagnostics;
using System.Text.Json;
using Newtonsoft.Json;
using PikaShop.Data.Context.Contracts;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Stripe;
using PikaShop.Common.Pagination;
using PikaShop.Services.Helpers;

namespace PikaShop.Web.Controllers
{

    public class CustomerProductsController(IProductServices _prdService, CacheHelper _cacheHelper) : Controller
    {
        private IProductServices productServices { get; set; } = _prdService;
        private CacheHelper cacheHelper { get; } = _cacheHelper;


        [HttpGet]
        public IActionResult Index(int? catId)
        {

            List<ProductEntity> products = productServices.GetCategoryProducts(catId);
            PaginatedList<ProductViewModel> productViewModels = new PaginatedList<ProductViewModel>();
            GetCachedValues(out List<DepartmentEntity> Departments, out double MaxPrice);
            ViewBag.Departments = Departments;
            ViewBag.MaxPrice = MaxPrice;
            ViewBag.Specification = productServices.GetCategorySpecs(catId, products);


            if (products.Count() > 0)
            {
                cacheHelper.SetProductsCache(products);

                IQueryable<ProductViewModel> prdsModel = products.Select(IHelperMapper.ProductViewMapper).AsQueryable();
                productViewModels = PaginatedList<ProductViewModel>.Create(prdsModel, 1, 9);
                return View(productViewModels);
            }
            return View(productViewModels);
        }

        public IActionResult ProductDetails(int id)
        {
            var product = productServices.UnitOfWork.Products.GetById(id);
            return View(product);
        }


        [HttpGet]
        public IActionResult NavigatePages(int pagenumber = 1)
        {
            cacheHelper.getProductsCache(out List<ProductEntity> cachedProducts);
            IQueryable<ProductViewModel> prdsModel = cachedProducts.Select(IHelperMapper.ProductViewMapper).AsQueryable();
            PaginatedList<ProductViewModel> productViewModels = PaginatedList<ProductViewModel>.Create(prdsModel, pagenumber, 9);

            return PartialView("_partialProductItem", productViewModels);
        }


        [HttpGet]
        public IActionResult SearchProducts(string searchKeyword)
        {
            List<ProductEntity> products = productServices.SearchByName(searchKeyword);
            PaginatedList<ProductViewModel> productViewModels = new PaginatedList<ProductViewModel>();

            if (products.Count > 0)
            {
                cacheHelper.SetProductsCache(products);


                IQueryable<ProductViewModel> prdsModel = products.Select(IHelperMapper.ProductViewMapper).AsQueryable();
                productViewModels = PaginatedList<ProductViewModel>.Create(prdsModel, 1, 9);

            }
            return PartialView("_partialProductItem", productViewModels);

        }


        [HttpGet]
        public IActionResult FilterByPrice(double maxPrice)
        {

            cacheHelper.getProductsCache(out List<ProductEntity> cachedProducts);
            PaginatedList<ProductViewModel> productViewModels = new PaginatedList<ProductViewModel>();

            List<ProductEntity> Products = productServices.SearchByPriceRange(maxPrice, cachedProducts);

            IQueryable<ProductViewModel> prdsModel = Products.Select(IHelperMapper.ProductViewMapper).AsQueryable();
            productViewModels = PaginatedList<ProductViewModel>.Create(prdsModel, 1, 9);

            return PartialView("_partialProductItem", productViewModels);
        }


        public IActionResult SortProductsBy(string orderBy)
        {
            cacheHelper.getProductsCache(out List<ProductEntity> cachedProducts);
            List<ProductEntity> products = productServices.SortProducts(orderBy, cachedProducts);
            PaginatedList<ProductViewModel> productViewModels = new PaginatedList<ProductViewModel>();
            cacheHelper.SetProductsCache(products);


            IQueryable<ProductViewModel> prdsModel = products.Select(IHelperMapper.ProductViewMapper).AsQueryable();
            productViewModels = PaginatedList<ProductViewModel>.Create(prdsModel, 1, 9);

            return PartialView("_partialProductItem", productViewModels);
        }


        [HttpGet]
        public IActionResult FilterByFeatures(string specificationJson)
        {

            var SpecificationKeys = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(specificationJson);

            cacheHelper.getProductsCache(out List<ProductEntity> cachedProducts);

            List<ProductEntity> products = productServices.FilterBySpecifications(SpecificationKeys, cachedProducts);


            IQueryable<ProductViewModel> prdsModel = products.Select(IHelperMapper.ProductViewMapper).AsQueryable(); ;
            PaginatedList<ProductViewModel> productViewModels = PaginatedList<ProductViewModel>.Create(prdsModel, 1, 9);


            return PartialView("_partialProductItem", productViewModels);
        }


        private void GetCachedValues(out List<DepartmentEntity> Departments, out double MaxPrice)
        {
            cacheHelper.GetDepartments(out List<DepartmentEntity> _Departments);
            cacheHelper.GetMaximumPriceRange(out double _MaxPrice);
            Departments = _Departments;
            MaxPrice = _MaxPrice;

        }

    }
}