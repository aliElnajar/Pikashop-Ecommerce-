#nullable enable

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PikaShop.Data.Context.ContextEntities.Core;
using PikaShop.Services.Contracts;

namespace PikaShop.Web.Controllers
{
    [Authorize(Roles = "Customer")]
    public class DepartmentController(IDepartmentServices _depServ) : Controller
    {
        private IDepartmentServices DepartmentService { get; } = _depServ;

        public ActionResult Index()
        {
            return View(DepartmentService.UnitOfWork.Departments.GetAll());
        }

        public ActionResult Details(int id)
        {
            return View(DepartmentService.UnitOfWork.Departments.GetById(id));
        }

        // GET: DepartmentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DepartmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DepartmentEntity entity)
        {
            if (entity == null) return View(entity);
            if (ModelState.IsValid)
            {
                DepartmentService.UnitOfWork.Departments.Create(entity);
                DepartmentService.UnitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        // GET: DepartmentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(DepartmentService.UnitOfWork.Departments.GetById(id));
        }

        // POST: DepartmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, DepartmentEntity entity)
        {
            if (entity == null) return View();
            if (ModelState.IsValid)
            {
                DepartmentService.UnitOfWork.Departments.UpdateById(id, entity);
                DepartmentService.UnitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        // GET: DepartmentController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return View();
        }

        // POST: DepartmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, DepartmentEntity entity)
        {
            if (entity == null) return View();
            if (ModelState.IsValid)
            {
                DepartmentService.UnitOfWork.Departments.DeleteById(id);
                DepartmentService.UnitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}