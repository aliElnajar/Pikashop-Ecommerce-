#nullable disable
using PikaShop.Data.Context.ContextEntities.Identity;

namespace PikaShop.Web.Areas.AdminPanel.ViewModel
{
    public class UsersAndRoles
    {
        public List<ApplicationUserEntity> Users { get; set; }

        public List<ApplicationUserRoleEntity> Roles { get; set; }

        public int SelectedUserID { get; set; }

        public int SelectedUserRole { get; set; }
    }
}