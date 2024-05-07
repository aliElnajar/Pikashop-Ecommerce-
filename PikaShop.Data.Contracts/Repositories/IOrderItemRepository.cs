using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PikaShop.Data.Context.ContextEntities.Core;

namespace PikaShop.Data.Contracts.Repositories
{
    public interface IOrderItemRepository : IRepository<OrderItemEntity, int>
    {
    }
}
