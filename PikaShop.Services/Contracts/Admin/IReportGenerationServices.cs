using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PikaShop.Data.Context.ContextEntities.Core;
using PikaShop.Services.Helpers.Admin;

namespace PikaShop.Services.Contracts.Admin
{
    public interface IReportGenerationServices : IServices
    {
        public double TotalSales();

        public int ProductCount();

        public int OrdersCount();

        public IQueryable<OrderEntity> LatestOrders(int count);

        public IQueryable<MonthlySales> YearMonthlySales(DateOnly from);

        public IQueryable<ProductWithSales> BestSellingProducts(int count);
    }
}
