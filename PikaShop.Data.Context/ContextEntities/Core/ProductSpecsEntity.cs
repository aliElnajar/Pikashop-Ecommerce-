using PikaShop.Data.Context.Contracts;
using PikaShop.Data.Contracts;
using PikaShop.Data.Entities.Core;

namespace PikaShop.Data.Context.ContextEntities.Core
{
    public class ProductSpecsEntity:ProductSpecification, IEntitySoftDelete
    {
        public int ID { get; set; }

        public int? ProductID { get; set; }

        public virtual ProductEntity? Product { get; set; }

        public bool IsDeleted { get; set; }
    }
}
