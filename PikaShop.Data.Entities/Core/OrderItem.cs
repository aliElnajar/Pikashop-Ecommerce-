
using PikaShop.Data.Entities.Audit;

namespace PikaShop.Data.Entities.Core
{
    public class OrderItem : AuditEntity
    {
        public int Quantity { get; set; }

        public double SellingPrice { get; set; }

        public double SubTotal { get; set; }
    }
}