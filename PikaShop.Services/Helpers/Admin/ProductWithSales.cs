using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PikaShop.Data.Context.ContextEntities.Core;

namespace PikaShop.Services.Helpers.Admin
{
    public class ProductWithSales
    {
        public ProductEntity Product { get; set; } = default!;

        public int? AverageRating { get; set; }

        public int TotalQuantitySold { get; set; }

        public double TotalSales { get; set; }
    }
}
