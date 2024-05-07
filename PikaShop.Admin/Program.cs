
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PikaShop.Admin.IdentityUnits;
using PikaShop.Admin.MappingProfiles;
using PikaShop.Common.Pagination;
using PikaShop.Data.Context;
using PikaShop.Data.Context.ContextEntities.Identity;
using PikaShop.Data.Contracts.UnitsOfWork;
using PikaShop.Data.Persistence.UnitsOfWork;
using PikaShop.Services.Admin;
using PikaShop.Services.Contracts;
using PikaShop.Services.Contracts.Admin;
using PikaShop.Services.Core;

namespace PikaShop.Admin
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region DbContext Configuration

            // DbContext Configuration & Injection
            var connectionString = builder.Configuration.GetConnectionString("DevelopmentConnection") ?? throw new InvalidOperationException("Connection string 'DevelopmentConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(dbOptionsBuilder =>
            dbOptionsBuilder
            .UseLazyLoadingProxies()
            .UseSqlServer(connectionString, b => b.MigrationsAssembly("PikaShop.Admin")));

            #endregion

            #region Identity Configuration

            //// Identity Configuration
            builder.Services.AddIdentity<ApplicationUserEntity, ApplicationUserRoleEntity>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddUserManager<UserManager<ApplicationUserEntity>>()
                .AddSignInManager<SignInManager<ApplicationUserEntity>>()
                .AddRoleManager<RoleManager<ApplicationUserRoleEntity>>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            builder.Services.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(30));

            #endregion

            #region Custom Service Configuration

            // Service Injection
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IDepartmentServices, DepartmentServices>();
            builder.Services.AddScoped<ICategoryServices, CategoryServices>();
            builder.Services.AddScoped<IProductServices, ProductServices>();
            builder.Services.AddScoped<ICategorySpecsServices, CategorySpecsServices>();
            builder.Services.AddScoped<IProductSpecsServices, ProductSpecsServices>();

            builder.Services.AddScoped<IReportGenerationServices, ReportGenerationServices>();

            #endregion

            #region AutoMapper Configuration

            builder.Services.AddAutoMapper(typeof(DepartmentEntityProfile).Assembly, typeof(PaginatedList<>).Assembly);

            #endregion

            #region MVC Configuration
            // MVC Configuration
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
                app.UseDeveloperExceptionPage();

                #region Identity Seeding
                // Identity Seeding
                using (var scope = app.Services.CreateScope())
                    await DbRoleSeeder.SeedRolesAndAdminAsync(scope.ServiceProvider);
                #endregion

                #region Database Development Seeding
                //DbRoleSeeder.SeedRolesAndAdminAsync();
                // Seed Database for Development
                ApplicationDbContextFactory contextFactory = new();
                UnitOfWork unitOfWork = new(contextFactory.CreateDbContext([""]));
                unitOfWork.EnsureSeedDataForContext();
                #endregion
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();



            await app.RunAsync();
        }
    }
}
