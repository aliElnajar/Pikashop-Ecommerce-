using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PikaShop.Admin.ViewModels;
using PikaShop.Common.Pagination;
using PikaShop.Services.Contracts;

namespace PikaShop.Admin.Controllers
{
	[Route("dashboard/[controller]/[action]")]
	[Authorize(Roles = "SuperAdmin,Admin")]
	public class OrderController : Controller
	{
		IOrderServices OrderService { get; set; }
		IMapper Mapper { get; set; }

		public OrderController(IOrderServices orderServices, IMapper mapper)
		{
			OrderService = orderServices;
			Mapper = mapper;
		}

		// GET: OrderController
		[HttpGet]
		public ActionResult Index(int? pageNumber)
		{
			int pageSize = 10;
			var orders = OrderService.UnitOfWork.Orders
				.GetAll()
				.ToPaginatedList(pageNumber ?? 1, pageSize);
			if(orders != null)
			{
				return View(Mapper.Map<PaginatedList<OrderViewModel>>(orders));
			}
			return View();
		}

		// GET: OrderController/Details/5
		public ActionResult Details(int id)
		{
			int pageSize = 10;
			var orders = OrderService.UnitOfWork.Orders
				.Find(o => o.ID == id)
				.Include(o => o.Items)
				.FirstOrDefault();
			if(orders != null)
			{
				ViewBag.OrderItems = orders.Items;
				return View(Mapper.Map<OrderViewModel>(orders));
			}
			return View();
		}
	}
}
