using Microsoft.AspNetCore.Identity;

namespace PikaShop.Data.Context.ContextEntities.Identity
{
    public class ApplicationUserRoleEntity : IdentityRole<int>
    {
        public ApplicationUserRoleEntity() : base()
        {
        }
        public ApplicationUserRoleEntity(string rolename) : base(rolename)
        {
        }
    }
}
