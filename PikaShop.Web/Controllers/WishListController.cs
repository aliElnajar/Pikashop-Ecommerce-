using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;
using PikaShop.Data.Context.ContextEntities.Core;
using PikaShop.Services.Contracts;
using PikaShop.Services.Core;
using PikaShop.Web.ViewModels;
using System.Linq;
using System.Security.Claims;


namespace PikaShop.Web.Controllers
{
    [Authorize(Roles = "Customer")]
    public class WishListController : Controller
    {
        private readonly IWishListServices _wishListServices;
        private readonly IProductServices _productServices;
        private readonly ICartItemServices _cartItemServices;
        private readonly IToastNotification _toastNotification;

        public WishListController(IWishListServices wishListServices, IProductServices productServices, ICartItemServices cartItemServices, IToastNotification toastNotification)
        {
            _wishListServices = wishListServices;
            _productServices = productServices;
            _cartItemServices = cartItemServices;
            _toastNotification = toastNotification;
        }


        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                // if the user didn't exist redirect to Home
                return RedirectToAction("Index", "Home");
            }
            var WishList = _productServices.UnitOfWork.WishList
               .GetAll()
               .Where(wl => wl.CustomerID == int.Parse(userId))
               .Select(wl => new WishListViewModel
               {
                   ProductId = wl.ProductID,
                   CustomerId = wl.CustomerID,
                   ProductImage = wl.Product.Img,
                   ProductName = wl.Product.Name,
                   Quantity = wl.Quantity,
                   Price = (decimal)wl.Product.Price,
                   TotalPrice = (decimal)(wl.Quantity * wl.Product.Price),
                   UnitsInStock = wl.Product.UnitsInStock
               })
               .ToList();

            // Calculate total price of all items in the WishList
            var totalPrice = WishList.Sum(ci => ci.TotalPrice);

            // Pass WishList items and total price to the view
            ViewBag.TotalPrice = totalPrice;
            return View(WishList);
        }

        // GET: WishList/AddToWishList/2
        [HttpPost]
        public IActionResult AddToWishList(int productId, int productQuantity = 1)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var WishList = _wishListServices.UnitOfWork.WishList.GetAll()
                .FirstOrDefault(wl => wl.ProductID == productId && wl.CustomerID == int.Parse(userId));

            if (WishList != null)
            {
                WishList.Quantity += productQuantity;
            }
            else
            {
                var newWishList = new WishListEntity
                {
                    ProductID = productId,
                    CustomerID = int.Parse(userId),
                    Quantity = productQuantity
                };
                _wishListServices.UnitOfWork.WishList.Create(newWishList);
            }

            int Result = _wishListServices.UnitOfWork.Save();

            if (Result > 0)
            {
                _toastNotification.AddSuccessToastMessage("Add to wish List successfully !");
                return Ok();
            }
            else
            {
                _toastNotification.AddErrorToastMessage("Failed to add !");
                return BadRequest();
            }
        }

        // GET: WishList/MoveToCart/2
        public IActionResult MoveToCart(int id)
        {
            var wishListEntity = _wishListServices.UnitOfWork.WishList.GetById(id);
            if (wishListEntity == null)
            {
                return NotFound();
            }
            return View(wishListEntity);
        }
        // POST: WishList/MoveToCart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MoveToCart(int productId, int customerId, int productQuantity = 1)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cartItem = _cartItemServices.UnitOfWork.CartItems.GetAll()
                .FirstOrDefault(ci => ci.ProductID == productId && ci.CustomerID == int.Parse(userId));

            if (cartItem != null)
            {
                cartItem.Quantity += productQuantity;
            }
            else
            {
                var newCartItem = new CartItemEntity
                {
                    ProductID = productId,
                    CustomerID = int.Parse(userId),
                    Quantity = productQuantity
                };
                _cartItemServices.UnitOfWork.CartItems.Create(newCartItem);
            }

            _cartItemServices.UnitOfWork.Save();
            _wishListServices.UnitOfWork.WishList.deletebyid(productId, customerId);
            _wishListServices.UnitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        // POST: WishList/IncrementProduct/2
        [HttpPost]
        public IActionResult IncrementProduct(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var WishList = _wishListServices.UnitOfWork.WishList.GetAll()
                .FirstOrDefault(ci => ci.ProductID == productId && ci.CustomerID == int.Parse(userId));

            if (WishList == null)
            {
                return NotFound();
            }

            WishList.Quantity++;
            _wishListServices.UnitOfWork.WishList.Update(WishList);
            _wishListServices.UnitOfWork.Save();

            return NoContent();
        }

        // POST: WishList/DecreaseProduct/2
        [HttpPost]
        public IActionResult DecreaseProduct(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var WishList = _wishListServices.UnitOfWork.WishList.GetAll()
                .FirstOrDefault(ci => ci.ProductID == productId && ci.CustomerID == int.Parse(userId));

            if (WishList == null || WishList.Quantity <= 1)
            {
                return NoContent();
            }

            WishList.Quantity--;
            _wishListServices.UnitOfWork.WishList.Update(WishList);
            _wishListServices.UnitOfWork.Save();

            return NoContent();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int productId, int customerId)
        {
            _wishListServices.UnitOfWork.WishList.deletebyid(productId, customerId);
            _wishListServices.UnitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult DeleteAll()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                // if the user didn't exist redirect to Home
                return RedirectToAction("Index");
            }

            _wishListServices.UnitOfWork.WishList.DeleteRange(_wishListServices.UnitOfWork.WishList
               .GetAll()
               .Where(wl => wl.CustomerID == int.Parse(userId)));
            _wishListServices.UnitOfWork.Save();

            return RedirectToAction("Index");
        }



        [HttpGet]
        public IActionResult MoveAlltoCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                // if the user didn't exist redirect to Home
                return RedirectToAction("Index");
            }
            var WishListItems = _wishListServices.UnitOfWork.WishList
               .GetAll()
               .Where(wl => wl.CustomerID == int.Parse(userId)).ToList();

            List<CartItemEntity> items = new List<CartItemEntity>();
            foreach (var item in WishListItems)
            {
                items.Add(new CartItemEntity()
                {
                    CustomerID = item.CustomerID,
                    Customer = item.Customer,
                    ProductID = item.ProductID,
                    Product = item.Product,
                    CreatedBy = item.CreatedBy,
                    DateCreated = item.DateCreated,
                    DateModified = item.DateModified,
                    ModifiedBy = item.ModifiedBy,
                    Quantity = item.Quantity
                });
            }

            _cartItemServices.UnitOfWork.CartItems.CreateRange(items);
            _wishListServices.UnitOfWork.WishList.DeleteRange(WishListItems);
            _wishListServices.UnitOfWork.Save();

            return RedirectToAction("Index");
        }
    }
}