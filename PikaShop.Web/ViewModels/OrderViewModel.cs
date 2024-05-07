using PikaShop.Data.Context.ContextEntities.Core;
using PikaShop.Data.Context.ContextEntities.Identity;

namespace PikaShop.Web.ViewModels
{
    public class OrderViewModel
    {
        public int ProductID { get; set; }

        public int OrderID { get; set; }

        public int Quantity { get; set; }

        public double SellingPrice { get; set; }

        public double SubTotal { get; set; }
    }
}