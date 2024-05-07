using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PikaShop.Data.Context.ContextEntities.Core;
using PikaShop.Services.Contracts;

namespace PikaShop.Web.Controllers
{
    [Authorize(Roles = "Customer")]
    public class OrdersController : Controller
    {
        private readonly IOrderServices _orderServices;

        public OrdersController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }
        public IActionResult Index()
        {
            List<OrderEntity> orders = _orderServices.UnitOfWork.Orders.GetAll().ToList();

            return View(orders);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            OrderEntity order = _orderServices.UnitOfWork.Orders.GetById(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order.Items);
        }
    }
}