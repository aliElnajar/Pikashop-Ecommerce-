using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PikaShop.Common.Pagination;
using PikaShop.Admin.ViewModels;
using PikaShop.Data.Context.ContextEntities.Core;
using PikaShop.Services.Contracts;
using PikaShop.Services.Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace PikaShop.Admin.Controllers
{
    [Route("dashboard/[controller]/[action]")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class CategorySpecsController : Controller
    {
        private ICategorySpecsServices _categorySpecsServices { get; }
        private readonly IMapper _mapper;
        public CategorySpecsController(ICategorySpecsServices categorySpecsServices, IMapper mapper)
        {
            this._categorySpecsServices = categorySpecsServices;
            this._mapper = mapper;
        }
        // GET: CategorySpecsController
        [HttpGet]
        public ActionResult Index(int? pageNumber)
        {
            int pageSize = 5;
            var categorySpecs = _categorySpecsServices.UnitOfWork.CategorySpecs.GetAll()
                .ToPaginatedList(pageNumber ?? 1, pageSize);
            if (categorySpecs != null)
            {
                var result = _mapper.Map<PaginatedList<CategorySpecsViewModel>>(categorySpecs);
                return View(result);
            }
            return View(null);
        }

        // GET: CategorySpecsController/Details/5
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult Details(int id)
        {
            var categorySpec = _categorySpecsServices.UnitOfWork.CategorySpecs.GetById(id);
            if (categorySpec != null)
            {
                CategorySpecsViewModel result = _mapper.Map<CategorySpecsViewModel>(categorySpec);
                return View(result);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: CategorySpecsController/Create
        [HttpGet]
        public ActionResult Create()
        {
            var categories = _categorySpecsServices.UnitOfWork.Categories.GetAll();
            ViewBag.Categories = new SelectList(categories, "ID", "Name");
            return View(new CategorySpecsViewModel());
        }

        // POST: CategorySpecsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategorySpecsViewModel categorySpec)
        {
            try
            {
                if (categorySpec != null && ModelState.IsValid && categorySpec.CategoryID!=default)
                {
                    CategorySpecsEntity entity = _mapper.Map<CategorySpecsEntity>(categorySpec);
                    entity.Category = null;
                    entity.Value = "";
                    _categorySpecsServices.UnitOfWork.CategorySpecs.Create(entity);
                    _categorySpecsServices.UnitOfWork.Save();
                    return Redirect("/dashboard/Category/Edit/" + entity.CategoryID.ToString());
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: CategorySpecsController/Edit/5
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult Edit(int id)
        {
            var categorySpec = _categorySpecsServices.UnitOfWork.CategorySpecs.GetById(id);
            if (categorySpec != null)
            {
                CategorySpecsViewModel result = _mapper.Map<CategorySpecsViewModel>(categorySpec);
                return View(result);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: CategorySpecsController/Edit/5
        [HttpPost]
        [Route("{id:int}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CategorySpecsViewModel categorySpec)
        {
            try
            {
                var target = _categorySpecsServices.UnitOfWork.CategorySpecs.GetById(id);
                if (target != null && ModelState.IsValid)
                {
                    CategorySpecsEntity other = _mapper.Map<CategorySpecsEntity>(categorySpec);
                    other.Category = null;
                    other.Value = "";
                    _categorySpecsServices.UnitOfWork.CategorySpecs.Update(target, other);
                    _categorySpecsServices.UnitOfWork.Save();
                    return RedirectToAction(nameof(Index));
                }
                return View(categorySpec);
            }
            catch
            {
                return View(categorySpec);
            }
        }

        // GET: CategorySpecsController/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var target = _categorySpecsServices.UnitOfWork.CategorySpecs.GetById(id);
            if (target != null)
            {
                CategorySpecsViewModel result = _mapper.Map<CategorySpecsViewModel>(target);
                return View(result);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: CategorySpecsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(CategorySpecsViewModel categorySpec)
        {
            try
            {
                if (categorySpec != null && ModelState.IsValid)
                {
                    CategorySpecsEntity target = _mapper.Map<CategorySpecsEntity>(categorySpec);
                    target.Category = null;
                    target.Value = "";
                    _categorySpecsServices.UnitOfWork.CategorySpecs.Delete(target);
                    _categorySpecsServices.UnitOfWork.Save();
                    return RedirectToAction(nameof(Index));
                }
                return View(categorySpec);
            }
            catch
            {
                return View(categorySpec);
            }
        }
    }
}
