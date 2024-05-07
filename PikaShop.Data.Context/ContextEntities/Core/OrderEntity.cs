
using PikaShop.Data.Context.ContextEntities.Identity;
using PikaShop.Data.Entities.Core;

namespace PikaShop.Data.Context.ContextEntities.Core
{
    public class OrderEntity : Order
    {
        public int ID { get; set; }

        public int? CustomerID { get; set; }

        public virtual CustomerEntity? Customer { get; set; }

        public virtual ICollection<OrderItemEntity>? Items { get; set; }
    }
}