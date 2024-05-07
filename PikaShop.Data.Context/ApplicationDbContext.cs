using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PikaShop.Data.Context.ContextEntities.Core;
using PikaShop.Data.Context.ContextEntities.Identity;
using PikaShop.Data.Context.EntityConfigurations.Core;
using PikaShop.Data.Context.EntityConfigurations.Identity;
using PikaShop.Data.Entities.ContextEntities.Core;

namespace PikaShop.Data.Context
{
    public class ApplicationDbContext
        (DbContextOptions<ApplicationDbContext> options)
        : IdentityDbContext<ApplicationUserEntity, ApplicationUserRoleEntity, int>(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Identity Configuration

            modelBuilder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new AdminEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerEntityConfiguration());
            modelBuilder.ApplyConfiguration(new DeliveryPersonEntityConfiguration());

            #endregion

            #region Core Configuration

            modelBuilder.ApplyConfiguration(new DepartmentEntityConfiguration());

            modelBuilder.ApplyConfiguration(new CategoryEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CategorySpecsEntityConfiguration());

            modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProductSpecsEntityConfiguration());

            modelBuilder.ApplyConfiguration(new OrderItemEntityConfiguration());
            modelBuilder.ApplyConfiguration(new OrderEntityConfiguration());

            modelBuilder.ApplyConfiguration(new CartItemEntityConfiguration());

            modelBuilder.ApplyConfiguration(new AddressEntityConfiguration());

            modelBuilder.ApplyConfiguration(new ReviewEntityConfiguration());

            modelBuilder.ApplyConfiguration(new WishListEntityConfiguration());

            #endregion

        }

        public virtual DbSet<DepartmentEntity> Departments { get; set; }

        public virtual DbSet<CategoryEntity> Categories { get; set; }
        public virtual DbSet<CategorySpecsEntity> CategorySpecTemplates { get; set; }

        public virtual DbSet<ProductEntity> Products { get; set; }
        public virtual DbSet<ProductSpecsEntity> ProductSpecs { get; set; }

        public virtual DbSet<OrderItemEntity> OrderItems { get; set; }
        public virtual DbSet<OrderEntity> Orders { get; set; }

        public virtual DbSet<CartItemEntity> CartItems { get; set; }

        public virtual DbSet<AddressEntity> Addresses { get; set; }

        public virtual DbSet<ReviewEntity> Reviews { get; set; }

        public virtual DbSet<WishListEntity> WishList { get; set; }

    }
}
