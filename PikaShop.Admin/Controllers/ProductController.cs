using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PikaShop.Common.Pagination;
using PikaShop.Admin.ViewModels;
using PikaShop.Data.Context.ContextEntities.Core;
using PikaShop.Data.Entities.Core;
using PikaShop.Services.Contracts;
using PikaShop.Services.Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace PikaShop.Admin.Controllers
{
    [Route("dashboard/[controller]/[action]")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class ProductController : Controller
    {
        private IProductServices _productServices { get; }
        private readonly IMapper _mapper;
        public ProductController(IProductServices productServices, IMapper mapper)
        {
            this._productServices = productServices;
            this._mapper = mapper;
        }
        // GET: ProductController
        [HttpGet]
        public ActionResult Index()
        {
            
            var products = _productServices.UnitOfWork.Products.GetAll()
                .ToList();
            if (products != null)
            {
                var result = _mapper.Map<List<ProductViewModel>>(products);
                return View(result);
            }
            return View(null);

        }

		// GET: ProductController/Details/5
		[HttpGet]
		[Route("{id:int}")]
		public ActionResult Details(int id)
        {
			var product = _productServices.UnitOfWork.Products.GetById(id);
			if (product != null)
			{
				ProductViewModel result = _mapper.Map<ProductViewModel>(product);
				return View(result);
			}
			return RedirectToAction(nameof(Index));
        }

        // GET: ProductController/Create
        [HttpGet]
        public ActionResult Create()
        {
            var categories = _productServices.UnitOfWork.Categories.GetAll().ToList();
            if (categories == null)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = _mapper.Map<List<SelectListItem>>(categories);
            return View(new ProductViewModel());
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel product)
        {
            try
            {
                if (product != null && ModelState.IsValid)
                {
                    /////////////////////////////////////////////
                    ProductEntity entity = _mapper.Map<ProductEntity>(product);
                    entity.Category = null;
                    _productServices.UnitOfWork.Products.Create(entity);
                    _productServices.UnitOfWork.Save();
                    return RedirectToAction(nameof(Index));
                }
                var categories = _productServices.UnitOfWork.Categories.GetAll().ToList();
                ViewBag.Categories = _mapper.Map<List<SelectListItem>>(categories);
                return View(product);
            }
            catch
            {
                var categories = _productServices.UnitOfWork.Categories.GetAll().ToList();
                ViewBag.Categories = _mapper.Map<List<SelectListItem>>(categories);
                return View(product);
            }
        }

        // GET: ProductController/Edit/5
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult Edit(int id)
        {
            var product = _productServices.UnitOfWork.Products.GetById(id);
            if (product != null)
            {
                ProductViewModel result = _mapper.Map<ProductViewModel>(product);
                var categories = _productServices.UnitOfWork.Categories.GetAll().ToList();
                ViewBag.Categories = _mapper.Map<List<SelectListItem>>(categories);
                return View(result);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [Route("{id:int}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProductViewModel product)
        {
            try
            {
                var target = _productServices.UnitOfWork.Products.GetById(id);
                if (target != null && ModelState.IsValid)
                {
                    ProductEntity other = _mapper.Map<ProductEntity>(product);
                    other.Category = null;
                    _productServices.UnitOfWork.Products.Update(target, other);
                    _productServices.UnitOfWork.Save();
                    return RedirectToAction(nameof(Index));
                }
                var categories = _productServices.UnitOfWork.Categories.GetAll().ToList();
                ViewBag.Categories = _mapper.Map<List<SelectListItem>>(categories);
                return View(product);
            }
            catch
            {
                var categories = _productServices.UnitOfWork.Categories.GetAll().ToList();
                ViewBag.Categories = _mapper.Map<List<SelectListItem>>(categories);
                return View(product);
            }
        }

        // GET: ProductController/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var target = _productServices.UnitOfWork.Products.GetById(id);
            if (target != null)
            {
                ProductViewModel result = _mapper.Map<ProductViewModel>(target);
                return View(result);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ProductViewModel product)
        {
            try
            {
                if (product != null && ModelState.IsValid)
                {
                    ProductEntity target = _mapper.Map<ProductEntity>(product);
                    _productServices.UnitOfWork.Products.Delete(target);
                    _productServices.UnitOfWork.Save();
                    return RedirectToAction(nameof(Index));
                }
                return View(product);
            }
            catch
            {
                return View(product);
            }
        }
    }
}
//public ActionResult Index()
//{
//    var products = _productServices.UnitOfWork.Products.GetAll();
//    IEnumerable<ProductViewModel> models = new List<ProductViewModel>();
//    if (products != null)
//    {
//        models = _mapper.Map<IQueryable<ProductEntity>, IEnumerable<ProductViewModel>>(products);
//    }
//    return View(models);
//}
