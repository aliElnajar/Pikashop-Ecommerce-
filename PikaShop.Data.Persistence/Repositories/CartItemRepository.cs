using PikaShop.Data.Context;
using PikaShop.Data.Context.ContextEntities.Core;
using PikaShop.Data.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PikaShop.Data.Persistence.Repositories
{

    public class CartItemRepository(ApplicationDbContext _context) : Repository<CartItemEntity, int>(_context), ICartItemRepository
    {
        public void deletebyid(int id, int id2)
        {
            context.CartItems.Remove(context.CartItems
                .FirstOrDefault(c => c.ProductID == id && c.CustomerID == id2));
        }
    }
}