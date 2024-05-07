using PikaShop.Data.Context.Contracts;
using PikaShop.Data.Contracts;
using PikaShop.Data.Entities.Core;

namespace PikaShop.Data.Context.ContextEntities.Core
{
    public class DepartmentEntity: Department, IEntitySoftDelete
    {
        public int ID { get; set; }

        public virtual ICollection<CategoryEntity>? Categories { get; set; }

        public bool IsDeleted { get; set; }
    }
}
