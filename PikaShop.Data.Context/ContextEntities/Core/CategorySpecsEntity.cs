using PikaShop.Data.Context.Contracts;
using PikaShop.Data.Contracts;
using PikaShop.Data.Entities.Core;

namespace PikaShop.Data.Context.ContextEntities.Core
{
    public class CategorySpecsEntity : CategorySpecification, IEntitySoftDelete
    {
        public int ID { get; set; }

        public int? CategoryID { get; set; }

        public virtual CategoryEntity? Category { get; set; }

        public bool IsDeleted { get; set; }
        public bool Searchable { get; set; }
    }
}
