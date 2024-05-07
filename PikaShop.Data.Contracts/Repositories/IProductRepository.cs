using PikaShop.Data.Context.ContextEntities.Core;
using PikaShop.Data.Contracts.Partial;

namespace PikaShop.Data.Contracts.Repositories
{
    public interface IProductRepository :
        IRepository<ProductEntity,int>,
        IUpdate<ProductEntity, int>;
}
