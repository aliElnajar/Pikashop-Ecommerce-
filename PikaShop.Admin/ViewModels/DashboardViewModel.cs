using PikaShop.Data.Context.ContextEntities.Core;
using PikaShop.Services.Helpers.Admin;

namespace PikaShop.Admin.ViewModels
{
	public class DashboardViewModel
	{
		public int CustomersCount { get; set; }

		public double TotalSales { get; set; }

		public int ProductsCount { get; set; }

		public int OrdersCount { get; set; }

		public List<OrderEntity> LatestOrders { get; set; } = [];

		public List<MonthlySales> MonthlySales { get; set; } = [];

		public List<ProductWithSales> TopSellingProducts { get; set; } = [];

	}
}
