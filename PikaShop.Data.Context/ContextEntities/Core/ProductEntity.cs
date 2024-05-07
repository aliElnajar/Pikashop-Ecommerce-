using PikaShop.Data.Context.Contracts;
using PikaShop.Data.Contracts;
using PikaShop.Data.Entities.Core;

namespace PikaShop.Data.Context.ContextEntities.Core
{
    public class ProductEntity: Product, IEntitySoftDelete
    {
        public int ID { get; set; }

        public int? CategoryID { get; set; }

        public virtual CategoryEntity? Category { get; set; }

        public virtual ICollection<ProductSpecsEntity>? ProductSpecs { get; set; }

        public bool IsDeleted { get; set; }
    }
}
