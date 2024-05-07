
using PikaShop.Data.Context.Contracts;
using PikaShop.Data.Entities.Core;

namespace PikaShop.Data.Context.ContextEntities.Core
{
    public class OrderItemEntity : OrderItem, IProductItem
    {
        public int ProductID { get; set; }

        public int? OrderID { get; set; }

        public virtual ProductEntity Product { get; set; } = default!;

        public virtual OrderEntity Order { get; set; } = default!;
    }
}
