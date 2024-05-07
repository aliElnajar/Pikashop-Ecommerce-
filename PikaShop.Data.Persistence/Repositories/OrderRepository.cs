
using PikaShop.Data.Context;
using PikaShop.Data.Context.ContextEntities.Core;
using PikaShop.Data.Contracts.Repositories;

namespace PikaShop.Data.Persistence.Repositories
{
	public class OrderRepository(ApplicationDbContext _context) : Repository<OrderEntity, int>(_context), IOrderRepository;
}
