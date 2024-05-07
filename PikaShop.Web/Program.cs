using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PikaShop.Data.Contracts.UnitsOfWork;
using PikaShop.Data.Persistence.UnitsOfWork;
using PikaShop.Services.Contracts;
using PikaShop.Services.Core;
using PikaShop.Data.Context.ContextEntities.Identity;
using PikaShop.Data.Context;
using PikaShop.Web.IdentityUnits;
using PikaShop.Services.Helpers;
using Stripe;
using NToastNotify;
using PikaShop.Services.Helpers;

namespace PikaShop.Web
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
            .UseSqlServer(connectionString, b => b.MigrationsAssembly("PikaShop.Web")));

            #endregion

            #region Identity Configuration
            // Identity Configuration
            builder.Services.AddIdentity<ApplicationUserEntity, ApplicationUserRoleEntity>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddUserManager<UserManager<ApplicationUserEntity>>()
                .AddSignInManager<SignInManager<ApplicationUserEntity>>()
                .AddRoleManager<RoleManager<ApplicationUserRoleEntity>>().AddDefaultUI().AddDefaultTokenProviders();//.AddUserConfirmation<ApplicationUserEntity>();
            builder.Services.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(30));

            //builder.Services.AddScoped<UserManager<ApplicationUserEntity>>();
            //builder.Services.AddScoped<RoleManager<ApplicationUserRoleEntity>>();
            #endregion

            #region External Logins
            builder.Services.AddAuthentication()
            .AddMicrosoftAccount(microsoftOptions =>
            {
                microsoftOptions.ClientId = builder.Configuration["Authentication:Microsoft:ClientId"] ?? throw new ArgumentNullException("Microsoft Client Id can not be null");
                microsoftOptions.ClientSecret = builder.Configuration["Authentication:Microsoft:ClientSecret"] ?? throw new ArgumentNullException("Microsoft Client Secret can not be null");
            }).AddGoogle(options =>
            {
                options.ClientId = builder.Configuration["Authentication:Google:ClientId"] ?? throw new ArgumentNullException("Google Client Id can not be null");
                options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"] ?? throw new ArgumentNullException("Google Client Secret can not be null");
            }).AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = builder.Configuration["Authentication:Facebook:AppId"] ?? throw new ArgumentNullException("Facebook Application Id can not be null");
                facebookOptions.AppSecret = builder.Configuration["Authentication:Facebook:AppSecret"] ?? throw new ArgumentNullException("Facebook Application Secret can not be null");
            });
            #endregion

            #region Custom Service Configuration
            // Service Injection

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IDepartmentServices, DepartmentServices>();
            builder.Services.AddScoped<ICategoryServices, CategoryServices>();
            builder.Services.AddScoped<IProductServices, ProductServices>();
            builder.Services.AddScoped<ICartItemServices, CartItemServices>();
            builder.Services.AddScoped<IWishListServices, WishListServices>();
            builder.Services.AddScoped<IOrderServices, OrderServices>();
            builder.Services.AddScoped<ICheckoutServices, CheckoutServices>();
            builder.Services.AddScoped<IStripeService, StripeService>(provider =>
            {
                var stripeSecretKey = builder.Configuration["Stripe:SecretKey"]; // Read from configuration
                return new StripeService(stripeSecretKey);
            });
            #endregion

            #region MVC Configurations
            // MVC Configuration
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            #endregion

            #region Add Toasting
            builder.Services.AddMvc().AddNToastNotifyToastr(new ToastrOptions()
            {
                CloseButton = true,
                TimeOut = 1000,
                ProgressBar = true,
                PositionClass = ToastPositions.TopRight,
                PreventDuplicates = true,

            });


            #endregion



            builder.Services.AddScoped<CacheHelper>();

            var app = builder.Build();
            app.UseNToastNotify();

            app.UseSession();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
                app.UseDeveloperExceptionPage();

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
            name: "profile1",
            pattern: "{area}/{controller}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}