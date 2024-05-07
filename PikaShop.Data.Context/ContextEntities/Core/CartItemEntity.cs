using PikaShop.Data.Entities.Core;
using PikaShop.Data.Context.Contracts;
using PikaShop.Data.Context.ContextEntities.Identity;

namespace PikaShop.Data.Context.ContextEntities.Core
{
    public class CartItemEntity : CartItem, IProductItem
    {
        public int ProductID { get; set; }

        public int? CustomerID { get; set; }

        // Navigation properties
        public virtual ProductEntity Product { get; set; } = default!;

        public virtual CustomerEntity Customer { get; set; } = default!;
    }
}
