using PikaShop.Data.Context.Contracts;
using PikaShop.Data.Contracts;
using PikaShop.Data.Entities.Core;

namespace PikaShop.Data.Context.ContextEntities.Core
{
    public class CategoryEntity:Category, IEntitySoftDelete
    {
        public int ID { get; set; }

        public int? DepartmentID { get; set; }

        public virtual DepartmentEntity? Department { get; set; }

        public virtual ICollection<ProductEntity>? Products { get; set; }

        public virtual ICollection<CategorySpecsEntity>? CategorySpecs { get; set; }

        public bool IsDeleted { get; set; }
    }
}
