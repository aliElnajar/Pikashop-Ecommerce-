using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PikaShop.Data.Context.ContextEntities.Core;
using PikaShop.Data.Contracts.UnitsOfWork;
using PikaShop.Data.Entities.Core;
using PikaShop.Services.Contracts.Admin;
using PikaShop.Services.Helpers.Admin;

namespace PikaShop.Services.Admin
{
    public class ReportGenerationServices : IReportGenerationServices
    {
        public IUnitOfWork UnitOfWork { get; set; }

        public ReportGenerationServices(IUnitOfWork unit)
        {
            UnitOfWork = unit;
        }

        public double TotalSales()
        {
            return UnitOfWork.Orders
                .GetSet()
                .Sum(o => o.Total);
        }

        public int ProductCount()
        {
            return UnitOfWork.Products
                .GetSet()
                .Count();
        }

        public int OrdersCount()
        {
            return UnitOfWork.Orders
                .GetSet()
                .Count();
        }

        public IQueryable<OrderEntity> LatestOrders (int count)
        {
            count = count < 1 ? 1 : count;

            return UnitOfWork.Orders
                .GetSet()
                .Include(order => order.Items)
                .OrderByDescending(o => o.OrderedAt)
                .Take(count);
        }

        public IQueryable<MonthlySales> YearMonthlySales (DateOnly from = default)
        {
            DateTime startDate = default;
            DateTime endDate = default;
            if (from != default)
            {
                startDate = new DateTime(from.Year, from.Month, 1);
                endDate = startDate.AddYears(1).AddDays(-1);
            }
            else
            {
                startDate = DateTime.Now.Date.AddYears(-1);
                endDate = DateTime.Now.Date.AddDays(-1);
            }

            return UnitOfWork.Orders.GetSet()
                .Where(o => o.OrderedAt >= startDate && o.OrderedAt <= endDate)
                .GroupBy(o => new { o.OrderedAt.Year, o.OrderedAt.Month })
                .Select(g => new MonthlySales
                {
                    year = g.Key.Year,
                    Month = g.Key.Month,
                    Sales = g.Sum(o => o.Total)
                })
                .OrderBy(ms => ms.year)
                .ThenBy(ms => ms.Month)
                .AsQueryable();
        }

        public IQueryable<ProductWithSales> BestSellingProducts (int count)
        {
            count = count < 1 ? 1 : count;
            return UnitOfWork.Products.GetSet()
            .Select(p => new ProductWithSales
            {
                Product = p,
                AverageRating = (int?)UnitOfWork.Reviews.GetSet()
                .Where(r => r.ProductID == p.ID)
                .Average(r => r.Rating),

                TotalQuantitySold = UnitOfWork.OrderItems.GetSet()
                .Where(oi => oi.ProductID == p.ID)
                .Sum(oi => oi.Quantity),

                TotalSales = UnitOfWork.OrderItems.GetSet()
                .Where(oi => oi.ProductID == p.ID)
                .Sum(oi => oi.SubTotal)
            })
            .Take(count)
            .AsQueryable();
        }
    }
}
