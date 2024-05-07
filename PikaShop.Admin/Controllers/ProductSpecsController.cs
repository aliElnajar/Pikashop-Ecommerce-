using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PikaShop.Common.Pagination;
using PikaShop.Admin.ViewModels;
using PikaShop.Data.Context.ContextEntities.Core;
using PikaShop.Services.Contracts;
using Microsoft.AspNetCore.Mvc.Rendering;
using PikaShop.Services.Core;
using Microsoft.AspNetCore.Authorization;

namespace PikaShop.Admin.Controllers
{
    [Route("dashboard/[controller]/[action]")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class ProductSpecsController : Controller
    {
        private IProductSpecsServices _productSpecsServices { get; }
        private readonly IMapper _mapper;
        public ProductSpecsController(IProductSpecsServices productSpecsServices, IMapper mapper)
        {
            this._productSpecsServices = productSpecsServices;
            this._mapper = mapper;
        }
        // GET: ProductSpecsController
        [HttpGet]
        public ActionResult Index(int? pageNumber)
        {
            int pageSize = 5;
            var productSpecs = _productSpecsServices.UnitOfWork.ProductSpecs.GetAll()
                .ToPaginatedList(pageNumber ?? 1, pageSize);
            if (productSpecs != null)
            {
                var result = _mapper.Map<PaginatedList<ProductSpecsViewModel>>(productSpecs);
                return View(result);
            }
            return View(null);
        }

        // GET: ProductSpecsController/Details/5
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult Details(int id)
        {
            var productSpec = _productSpecsServices.UnitOfWork.ProductSpecs.GetById(id);
            if (productSpec != null)
            {
                ProductSpecsViewModel result = _mapper.Map<ProductSpecsViewModel>(productSpec);
                return View(result);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: ProductSpecsController/Create
        [HttpGet]
        public ActionResult Create()
        {
            var products = _productSpecsServices.UnitOfWork.Products.GetAll();
            ViewBag.Products = new SelectList(products, "ID", "Name");
            return View(new ProductSpecsViewModel());
        }

        // POST: ProductSpecsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductSpecsViewModel productSpec)
        {
            try
            {
                if (productSpec != null && ModelState.IsValid && productSpec.ProductID != default)
                {
                    ProductSpecsEntity entity = _mapper.Map<ProductSpecsEntity>(productSpec);
                    entity.Product = null;
                    entity.Value = "";
                    _productSpecsServices.UnitOfWork.ProductSpecs.Create(entity);
                    _productSpecsServices.UnitOfWork.Save();
                    return Redirect("/dashboard/Product/Edit/" + entity.ProductID.ToString());
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: ProductSpecsController/Edit/5
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult Edit(int id)
        {
            var productSpec = _productSpecsServices.UnitOfWork.ProductSpecs.GetById(id);
            if (productSpec != null)
            {
                ProductSpecsViewModel result = _mapper.Map<ProductSpecsViewModel>(productSpec);
                return View(result);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: ProductSpecsController/Edit/5
        [HttpPost]
        [Route("{id:int}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProductSpecsViewModel productSpec)
        {
            try
            {
                var target = _productSpecsServices.UnitOfWork.ProductSpecs.GetById(id);
                if (target != null && ModelState.IsValid)
                {
                    ProductSpecsEntity other = _mapper.Map<ProductSpecsEntity>(productSpec);
                    other.Product = null;
                    //other.Value = "";
                    _productSpecsServices.UnitOfWork.ProductSpecs.Update(target, other);
                    _productSpecsServices.UnitOfWork.Save();
                    return RedirectToAction(nameof(Index));
                }
                return View(productSpec);
            }
            catch
            {
                return View(productSpec);
            }
        }

        // GET: ProductSpecsController/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var target = _productSpecsServices.UnitOfWork.ProductSpecs.GetById(id);
            if (target != null)
            {
                ProductSpecsViewModel result = _mapper.Map<ProductSpecsViewModel>(target);
                return View(result);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: ProductSpecsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ProductSpecsViewModel productSpec)
        {
            try
            {
                if (productSpec != null && ModelState.IsValid)
                {
                    ProductSpecsEntity target = _mapper.Map<ProductSpecsEntity>(productSpec);
                    _productSpecsServices.UnitOfWork.ProductSpecs.Delete(target);
                    _productSpecsServices.UnitOfWork.Save();
                    return RedirectToAction(nameof(Index));
                }
                return View(productSpec);
            }
            catch
            {
                return View(productSpec);
            }
        }
    }
}
