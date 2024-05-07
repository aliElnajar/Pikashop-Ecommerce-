
using PikaShop.Data.Context.ContextEntities.Core;

namespace PikaShop.Data.Context.Contracts
{
    public interface IProductItem
    {
        public int ProductID { get; set; }

        public ProductEntity Product { get; set; }

        public int Quantity { get; set; }
    }
}
