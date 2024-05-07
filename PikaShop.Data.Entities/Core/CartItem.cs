
using PikaShop.Data.Entities.Audit;

namespace PikaShop.Data.Entities.Core
{
    public class CartItem : AuditEntity
    {
        public int Quantity { get; set; }
    }
}
