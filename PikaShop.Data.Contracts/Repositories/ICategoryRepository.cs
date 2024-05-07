using PikaShop.Data.Context.ContextEntities.Core;
using PikaShop.Data.Contracts.Partial;

namespace PikaShop.Data.Contracts.Repositories
{
    public interface ICategoryRepository :
        IRepository<CategoryEntity , int>,
        IUpdate<CategoryEntity, int>;
}
