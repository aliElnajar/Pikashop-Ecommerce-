#nullable enable

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PikaShop.Data.Context.ContextEntities.Core;
using PikaShop.Services.Contracts;

namespace PikaShop.Web.Controllers
{
    [Authorize(Roles = "Customer")]
    public class ProductController(IProductServices _productService) : Controller
    {
        private readonly IProductServices productServices = _productService;

        // GET: Product
        public IActionResult Index()
        {
            return View(productServices.UnitOfWork.Products.GetAll());
        }

        // GET: Product/Details/5
        public IActionResult Details(int id)
        {
            var productEntity = productServices.UnitOfWork.Products.GetById(id);
            if (productEntity == null)
            {
                return NotFound();
            }

            return View(productEntity);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(productServices.UnitOfWork.Categories.GetAll(), "Id", "Description");
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(ProductEntity entity)
        {
            if (ModelState.IsValid)
            {
                productServices.UnitOfWork.Products.Create(entity);
                productServices.UnitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(productServices.UnitOfWork.Categories.GetAll(), "Id", "Description", entity.CategoryID);
            return View(entity);
        }

        // GET: Product/Edit/5
        public IActionResult Edit(int id)
        {
            var productEntity = productServices.UnitOfWork.Products.GetById(id);
            if (productEntity == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(productServices.UnitOfWork.Categories.GetAll(), "Id", "Description", productEntity.CategoryID);
            return View(productEntity);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ProductEntity productEntity)
        {
            if (id != productEntity.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    productServices.UnitOfWork.Products.UpdateById(id, productEntity);
                    productServices.UnitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException) when (!ProductEntityExists(productEntity.ID))
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(productServices.UnitOfWork.Categories.GetAll(), "Id", "Description", productEntity.CategoryID);
            return View(productEntity);
        }

        // GET: Product/Delete/5
        public IActionResult Delete(int id)
        {
            ProductEntity? productEntity = productServices.UnitOfWork.Products.GetById(id);
            if (productEntity == null)
            {
                return NotFound();
            }

            return View(productEntity);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            productServices.UnitOfWork.Products.DeleteById(id);
            productServices.UnitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }

        private bool ProductEntityExists(int id)
        {
            return productServices.UnitOfWork.Products.GetAll().Any(p => p.ID == id);
        }
    }
}