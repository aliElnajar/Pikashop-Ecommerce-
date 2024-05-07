
using PikaShop.Data.Context.ContextEntities.Identity;
using PikaShop.Data.Entities.Core;

namespace PikaShop.Data.Context.ContextEntities.Core
{
    public class ReviewEntity : Review
    {
        public int ProductID { get; set; }

        public int CustomerID { get; set; }

        public virtual ProductEntity? Product { get; set; }

        public virtual CustomerEntity? Customer { get; set; }
     
    }
}
