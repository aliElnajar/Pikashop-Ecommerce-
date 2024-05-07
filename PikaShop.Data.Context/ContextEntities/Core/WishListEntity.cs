using PikaShop.Data.Context.ContextEntities.Identity;
using PikaShop.Data.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PikaShop.Data.Context.ContextEntities.Core
{
    public class WishListEntity : WishList
    {
        public int ProductID { get; set; }

        public int? CustomerID { get; set; }

        // Navigation properties
        public virtual ProductEntity Product { get; set; } = default!;

        public virtual CustomerEntity Customer { get; set; } = default!;
    }
}