using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PikaShop.Common.Pagination;
using PikaShop.Admin.ViewModels;
using PikaShop.Data.Context.ContextEntities.Core;
using PikaShop.Services.Contracts;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace PikaShop.Admin.Controllers
{
    [Route("dashboard/[controller]/[action]")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    //Depatment/id/categories
    public class CategoryController : Controller
    {
        private ICategoryServices _categoryServices { get; }
        private readonly IMapper _mapper;
        public CategoryController(ICategoryServices categoryServices, IMapper mapper)
        {
            this._categoryServices = categoryServices;
            this._mapper = mapper;
        }
        // GET: CategoryController
        [HttpGet]
        public ActionResult Index()
        {
           
            var categories = _categoryServices.UnitOfWork.Categories.GetAll()
                .ToList();
            if (categories != null)
            {
                var result = _mapper.Map<List<CategoryViewModel>>(categories);
                return View(result);
            }
            return View(null);

        }

        // GET: CategoryController/Details/5
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult Details(int id)
        {
            var category=_categoryServices.UnitOfWork.Categories.GetById(id);
            if (category != null)
            {
                CategoryViewModel result=_mapper.Map<CategoryViewModel>(category);
                return View(result);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: CategoryController/Create
        [HttpGet]
        public ActionResult Create()
        {
            var departments = _categoryServices.UnitOfWork.Departments.GetAll().ToList();
            if(departments == null)
            {
                // Redirect because you cannot create a category
                // without a department
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Departments = _mapper.Map<List<SelectListItem>>(departments);
            return View(new CategoryViewModel());
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryViewModel categry)
        {
            try
            {
                if(categry!=null && ModelState.IsValid)
                {
                    CategoryEntity entity= _mapper.Map<CategoryViewModel, CategoryEntity>(categry);
                    entity.Department = null;
                    _categoryServices.UnitOfWork.Categories.Create(entity);
                    _categoryServices.UnitOfWork.Save();
                    return RedirectToAction(nameof(Index));
                }
                var departments = _categoryServices.UnitOfWork.Departments.GetAll().ToList();
                ViewBag.Departments = _mapper.Map<List<SelectListItem>>(departments);
                return View(categry);

            }
            catch
            {
                var departments = _categoryServices.UnitOfWork.Departments.GetAll().ToList();
                ViewBag.Departments = _mapper.Map<List<SelectListItem>>(departments);
                return View(categry);
            }
        }

        // GET: CategoryController/Edit/5
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult Edit(int id)
        {

            var category = _categoryServices.UnitOfWork.Categories.GetById(id);
            if (category != null)
            {
                CategoryViewModel result = _mapper.Map<CategoryViewModel>(category);
                var departments = _categoryServices.UnitOfWork.Departments.GetAll().ToList();
                ViewBag.Departments = _mapper.Map<List<SelectListItem>>(departments);
                return View(result);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [Route("{id:int}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CategoryViewModel category)
        {
            try
            {
                var target = _categoryServices.UnitOfWork.Categories.GetById(id);
                if (target != null && ModelState.IsValid)
                {
                    CategoryEntity cat = _mapper.Map<CategoryEntity>(category);
                    _categoryServices.UnitOfWork.Categories.Update(target, cat);
                    _categoryServices.UnitOfWork.Save();
                    return RedirectToAction(nameof(Index));
                }
                var departments = _categoryServices.UnitOfWork.Departments.GetAll().ToList();
                ViewBag.Departments = _mapper.Map<List<SelectListItem>>(departments);
                return View(category);
            }
            catch
            {
                var departments = _categoryServices.UnitOfWork.Departments.GetAll().ToList();
                ViewBag.Departments = _mapper.Map<List<SelectListItem>>(departments);
                return View(category);
            }
        }

        // GET: CategoryController/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var target = _categoryServices.UnitOfWork.Categories.GetById(id);
            if (target != null)
            {
                CategoryViewModel result=_mapper.Map<CategoryViewModel>(target);
                return View(result);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(CategoryViewModel category)
        {
            try
            {
                if(category!=null && ModelState.IsValid)
                {
                    CategoryEntity target=_mapper.Map<CategoryEntity>(category);
                    _categoryServices.UnitOfWork.Categories.Delete(target);
                    _categoryServices.UnitOfWork.Save();
                    return RedirectToAction(nameof(Index));

                }
                return View(category) ;
            }
            catch
            {
                return View(category);
            }
        }
    }
}
