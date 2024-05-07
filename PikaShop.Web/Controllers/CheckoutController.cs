using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PikaShop.Data.Context.ContextEntities.Core;
using PikaShop.Data.Context.ContextEntities.Identity;
using PikaShop.Data.Entities.Enums;
using PikaShop.Services.Contracts;
using PikaShop.Web.ViewModels;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using PikaShop.Web.ViewModels;
using PikaShop.Data.Context.ContextEntities.Core;
using Microsoft.AspNetCore.Authorization;

namespace YourNamespace.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CheckoutController : Controller
    {
        private readonly string _stripeSecretKey = "sk_test_51Ou2OOGFkpxy9DHRADa2B3NpA00Jd65SAxn3uZfOSQL0sHcGERLn0XFZKkF6wzfm80HAO2CKu7pbIdDvvqUl60sO00YGRzG5Hk";
        private readonly ICartItemServices _cartItemService;
        private readonly IOrderServices _orderService;

        public CheckoutController(ICartItemServices cartItemService, IOrderServices orderServices)
        {
            _cartItemService = cartItemService;
            _orderService = orderServices;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Index", "Home");
            }

            var cartItems = _cartItemService.UnitOfWork.CartItems
               .GetAll()
               .Where(ci => ci.CustomerID == int.Parse(userId))
               .Select(ci => new CartItemViewModel
               {
                   ProductId = ci.ProductID,
                   CustomerId = ci.CustomerID,
                   ProductImage = ci.Product.Img,
                   ProductName = ci.Product.Name,
                   Quantity = ci.Quantity,
                   Price = (decimal)ci.Product.Price,
                   TotalPrice = (decimal)(ci.Quantity * ci.Product.Price)
               })
               .ToList();

            var totalPrice = cartItems.Sum(ci => ci.TotalPrice);
            ViewBag.TotalPrice = totalPrice;

            return View(cartItems);
        }

        public IActionResult CreateCheckoutSession()
        {
            StripeConfiguration.ApiKey = _stripeSecretKey;
            var domain = "http://localhost:5015/";

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cartItems = _cartItemService.UnitOfWork.CartItems
                .GetAll()
                .Where(ci => ci.CustomerID == int.Parse(userId))
                .ToList();

            var lineItems = cartItems.Select(ci => new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = "usd",
                    UnitAmount = (long)(ci.Product.Price * 100),
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = ci.Product.Name,
                    },
                },
                Quantity = ci.Quantity,
            }).ToList();

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = domain + $"Checkout/Success",
                CancelUrl = domain + "Checkout/Cancel",
            };

            var service = new SessionService();
            var session = service.Create(options);

            return Redirect(session.Url);
        }

        public IActionResult Success(string sessionId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            OrderPlacing();
            ClearCart();

            return View();
        }

        public IActionResult Cancel()
        {
            return View();
        }

        public IActionResult OrderPlacing()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cartItems = _cartItemService.UnitOfWork.CartItems
                .GetAll()
                .Where(ci => ci.CustomerID == int.Parse(userId))
                .ToList();

            double TotalPrice = 0;

            foreach (var cartItem in cartItems)
            {
                TotalPrice += cartItem.Quantity * cartItem.Product.Price;
            }

            List<OrderItemEntity> orderItems = new List<OrderItemEntity>();

            foreach (var cartItem in cartItems)
            {
                cartItem.Product.UnitsInStock -= cartItem.Quantity;
                OrderItemEntity orderItem = new OrderItemEntity
                {
                    ProductID = cartItem.ProductID,
                    Quantity = cartItem.Quantity,
                    SellingPrice = cartItem.Product.Price,
                    SubTotal = cartItem.Product.Price * cartItem.Quantity,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    CreatedBy = "system",
                    ModifiedBy = "system"
                };

                orderItems.Add(orderItem);
            }

            OrderEntity orderPlaced = new OrderEntity()
            {
                CustomerID = int.Parse(userId),
                Total = TotalPrice,
                PaymentMethod = PikaShop.Data.Entities.Enums.PaymentMethods.Stripe,
                Status = "Delivered",
                TransactionID = "12032233434",
                IsPaid = true,
                PaymentAddedValue = TotalPrice,
                OrderedAt = DateTime.Now
            };

            orderPlaced.Items = orderItems;

            _orderService.UnitOfWork.Orders.Create(orderPlaced);
            _orderService.UnitOfWork.Save();

            return Ok();
        }
        public IActionResult ClearCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cartItems = _cartItemService.UnitOfWork.CartItems
                .GetAll()
                .Where(ci => ci.CustomerID == int.Parse(userId))
                .ToList();

            _cartItemService.UnitOfWork.CartItems.DeleteRange(cartItems);
            _cartItemService.UnitOfWork.Save();

            return RedirectToAction("Index", "Home");
        }
    }
}