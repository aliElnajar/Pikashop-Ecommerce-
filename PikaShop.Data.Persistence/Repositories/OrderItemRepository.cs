using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PikaShop.Data.Context;
using PikaShop.Data.Context.ContextEntities.Core;
using PikaShop.Data.Contracts.Repositories;

namespace PikaShop.Data.Persistence.Repositories
{
    public class OrderItemRepository(ApplicationDbContext _context)
        : Repository<OrderItemEntity, int>(_context),

        IOrderItemRepository
    {
        public OrderItemEntity? GetByCompositeId(int productId, int orderId)
        {
            return entities
                .Where(oi => oi.OrderID == orderId && oi.ProductID == productId)
                .FirstOrDefault();
        }
    }
}
