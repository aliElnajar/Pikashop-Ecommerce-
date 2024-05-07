using PikaShop.Data.Context.ContextEntities.Core;
using PikaShop.Data.Contracts.Partial;

namespace PikaShop.Data.Contracts.Repositories
{
    public interface ICategorySpecsRepository:
        IRepository<CategorySpecsEntity, int>,
        IUpdate<CategorySpecsEntity, int>;
}
