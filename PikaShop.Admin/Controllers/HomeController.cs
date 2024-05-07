using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PikaShop.Admin.Models;
using PikaShop.Admin.ViewModels;
using PikaShop.Data.Context;
using PikaShop.Data.Context.ContextEntities.Identity;
using PikaShop.Services.Contracts;
using PikaShop.Services.Contracts.Admin;

namespace PikaShop.Admin.Controllers
{
	[Authorize(Roles = "SuperAdmin,Admin")]
	public class HomeController
		(ApplicationDbContext context,
			IReportGenerationServices reportGenerationServices)

		: Controller
	{
		readonly ApplicationDbContext _context = context;
		readonly IReportGenerationServices reportGenerationServices = reportGenerationServices;

		public async Task<IActionResult> Index(DateOnly from = default)
		{
			DashboardViewModel dashboardModel = new();

			dashboardModel.CustomersCount = await CountUsersInRoleAsync("Customer");

			dashboardModel.TotalSales = reportGenerationServices.TotalSales();

			dashboardModel.ProductsCount = reportGenerationServices.ProductCount();

			dashboardModel.OrdersCount = reportGenerationServices.OrdersCount();

			dashboardModel.LatestOrders = reportGenerationServices.LatestOrders(10).ToList();

			dashboardModel.MonthlySales = reportGenerationServices.YearMonthlySales(from).ToList();

			dashboardModel.TopSellingProducts = reportGenerationServices.BestSellingProducts(10).ToList();

			return View(dashboardModel);
		}

		public async Task<ApplicationUserRoleEntity> GetRoleByNameAsync(string roleName)
		{
			var role = await _context.Roles
				.FirstOrDefaultAsync(r => r.Name == roleName);

			return role;
		}

		public async Task<int> CountUsersInRoleAsync(string roleName)
		{
			var roleId = await _context.Roles
				.Where(r => r.Name == roleName)
				.Select(r => r.Id)
				.FirstOrDefaultAsync();

			if (roleId != null)
			{
				var userCount = await _context.UserRoles
					.CountAsync(ur => ur.RoleId == roleId);

				return userCount;
			}
			else
			{
				return 0; // Role not found, return 0
			}
		}

		public IQueryable<ApplicationUserEntity> GetUsersInRole(string roleName)
		{
			var usersInRole = _context.UserRoles
				.Join(_context.Roles,
					userRole => userRole.RoleId,
					role => role.Id,
					(userRole, role) => new { UserRole = userRole, Role = role })
				.Join(_context.Users,
					userRoleRole => userRoleRole.UserRole.UserId,
					user => user.Id,
					(userRoleRole, user) => new { UserRoleRole = userRoleRole, User = user })
				.Where(result => result.UserRoleRole.Role.Name == roleName)
				.Select(result => result.User)
				.AsQueryable();

			return usersInRole;
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
