using PikaShop.Data.Context.ContextEntities.Core;
using PikaShop.Data.Contracts.Partial;

namespace PikaShop.Data.Contracts.Repositories
{
    public interface IDepartmentRepository :
        IRepository<DepartmentEntity,int>,
        IUpdate<DepartmentEntity, int>;
}
