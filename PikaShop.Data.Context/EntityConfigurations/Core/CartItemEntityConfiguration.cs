using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PikaShop.Data.Context.ContextEntities.Core;
using PikaShop.Data.Context.ContextEntities.Identity;

namespace PikaShop.Data.Context.EntityConfigurations.Core
{
    public class CartItemEntityConfiguration : IEntityTypeConfiguration<CartItemEntity>
    {
        public virtual void Configure(EntityTypeBuilder<CartItemEntity> builder)
        {
            // Mapping
            #region Table & Primary Keys

            builder.ToTable("CartItems");
            builder.HasKey(nameof(CartItemEntity.ProductID), nameof(CartItemEntity.CustomerID));

            #endregion

            #region Relationships

            // Relationship with Product
            builder.HasOne<ProductEntity>(c => c.Product)
                .WithMany()
                .HasForeignKey(c => c.ProductID)
                .HasPrincipalKey(product => product.ID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship with Customer
            builder.HasOne<CustomerEntity>(cartItem => cartItem.Customer)
                .WithMany(customer => customer.Cart)
                .HasForeignKey(cartItem => cartItem.CustomerID)
                .HasPrincipalKey(customer => customer.Id)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            // Data

            #region Audit Configuration

            builder.Property<DateTime>(entity => entity.DateCreated).HasDefaultValueSql("getdate()");
            builder.Property<DateTime>(entity => entity.DateModified).HasDefaultValueSql("getdate()");
            builder.Property<string>(entity => entity.CreatedBy).HasDefaultValue("system");
            builder.Property<string>(entity => entity.ModifiedBy).HasDefaultValue("system");

            #endregion

            // Other Configuration
        }
    }
}
