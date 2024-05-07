#nullable enable

using Microsoft.AspNetCore.Identity;
using PikaShop.Data.Context.ContextEntities.Identity;

namespace PikaShop.Admin.IdentityUnits
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
                await roleManager.CreateAsync(new ApplicationUserRoleEntity("SuperAdmin"));
                await roleManager.CreateAsync(new ApplicationUserRoleEntity("Customer"));

                // creating admin in code to be secure.

                var userSuperAdmin = new ApplicationUserEntity
                {
                    UserName = "superadmin@gmail.com",
                    Email = "superadmin@gmail.com",
                    FirstName = "Hebatallah",
                    LastName = "Ali",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };

                var userAdmin = new ApplicationUserEntity
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    FirstName = "Ibrahim",
                    LastName = "Hassan",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };

                var userCustomer = new ApplicationUserEntity
                {
                    UserName = "customer@gmail.com",
                    Email = "customer@gmail.com",
                    FirstName = "Ali",
                    LastName = "Elnaggar",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };

                // Check for email
                var userInDbSuperAdmin = await userManager.FindByEmailAsync(userSuperAdmin.Email);
                var userInDbAdmin = await userManager.FindByEmailAsync(userAdmin.Email);
                var userInDbCustomer = await userManager.FindByEmailAsync(userCustomer.Email);

                // If no user is found with that email (Email = "admin@gmail.com")
                // Then we create an new account and set it as admin.

                // This part ensures that there is always an admin account.
                if (userInDbSuperAdmin == null)
                {
                    // setting it's password
                    await userManager.CreateAsync(userSuperAdmin, "SuperAdmin@123");
                    // setting it's role
                    await userManager.AddToRoleAsync(userSuperAdmin, "SuperAdmin");
                }

                if (userInDbAdmin == null)
                {
                    // setting it's password
                    await userManager.CreateAsync(userAdmin, "Admin@123");
                    // setting it's role
                    await userManager.AddToRoleAsync(userAdmin, "Admin");
                }

                if (userInDbCustomer == null)
                {
                    // setting it's password
                    await userManager.CreateAsync(userCustomer, "Customer@123");
                    // setting it's role
                    await userManager.AddToRoleAsync(userCustomer, "Customer");
                }
                /* If the email doesn't exist we will make a new account

                 */
            }
        }
    }
}
