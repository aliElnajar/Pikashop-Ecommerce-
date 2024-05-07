using PikaShop.Data.Context.ContextEntities.Core;
using PikaShop.Data.Entities.ContextEntities.Core;

namespace PikaShop.Data.Context.ContextEntities.Identity
{
    public class CustomerEntity : ApplicationUserEntity
    {
        public virtual ICollection<AddressEntity>? Addresses { get; set; }

        public virtual ICollection<CartItemEntity>? Cart { get; set; }

        public virtual ICollection<ReviewEntity>? Reviews { get; set; }

        public virtual ICollection<WishListEntity>? WishList { get; set; }
    }
}
