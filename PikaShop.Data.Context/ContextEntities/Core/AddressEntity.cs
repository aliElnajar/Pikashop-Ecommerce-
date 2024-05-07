using PikaShop.Data.Context.ContextEntities.Identity;
using PikaShop.Data.Contracts;
using PikaShop.Data.Entities.Core;

namespace PikaShop.Data.Entities.ContextEntities.Core
{
    public class AddressEntity : Address, IEntitySoftDelete
    {
        public int ID { get; set; }

        public int CustomerID { get; set; }

        public virtual CustomerEntity? Customer { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
