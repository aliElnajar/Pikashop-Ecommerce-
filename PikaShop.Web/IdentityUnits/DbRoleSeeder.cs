#nullable enable

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PikaShop.Data.Context.ContextEntities.Identity;
using PikaShop.Data.Context.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PikaShop.Web.IdentityUnits
{
    public static class DbRoleSeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider service)
        {
            // Seeds Roles

            // 1- getting the services (API's) responsible for `Users` and `roles` manging
            var userManager = service.GetService<UserManager<ApplicationUserEntity>>();
            var roleManager = service.GetService<RoleManager<ApplicationUserRoleEntity>>();

            // 2- creating and adding roles to the `roleManger` Instance
            if (roleManager is not null && userManager is not null)
            {
                await roleManager.CreateAsync(new ApplicationUserRoleEntity("Admin"));
                await roleManager.CreateAsync(new ApplicationUserRoleEntity("Anonymous"));
                await roleManager.CreateAsync(new ApplicationUserRoleEntity("Customer"));
                await roleManager.CreateAsync(new ApplicationUserRoleEntity("Delivery"));
                await roleManager.CreateAsync(new ApplicationUserRoleEntity("SuperAdmin"));

                // creating admin in code to be secure.

                var user = new ApplicationUserEntity
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    FirstName = "Ibrahim",
                    LastName = "Hassan",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };

                // Check for email
                var userInDb = await userManager.FindByEmailAsync(user.Email);

                // If no user is found with that email (Email = "admin@gmail.com")
                // Then we create an new account and set it as admin.

                // This part ensures that there is always an admin account.
                if (userInDb == null)
                {
                    // setting it's password
                    await userManager.CreateAsync(user, "Admin@123");
                    // setting it's role
                    await userManager.AddToRoleAsync(user,"SuperAdmin");
                }

                /* If the email doesn't exist we will make a new account

                 */
            }
        }
    }
}
