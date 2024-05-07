
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PikaShop.Data.Context.ContextEntities.Core;

namespace PikaShop.Data.Context.EntityConfigurations.Core
{
    public class OrderItemEntityConfiguration : IEntityTypeConfiguration<OrderItemEntity>
    {
        public virtual void Configure(EntityTypeBuilder<OrderItemEntity> builder)
        {
            // Mapping

            #region Table & Primary Keys

            builder.ToTable("OrderItems");
            builder.HasKey(nameof(OrderItemEntity.ProductID), nameof(OrderItemEntity.OrderID));

            #endregion

            #region Relationships

            // Relationship with Product
            builder.HasOne<ProductEntity>(cartItem => cartItem.Product)
                .WithMany()
                .HasForeignKey(cartItem => cartItem.ProductID)
                .HasPrincipalKey(product => product.ID)
                .IsRequired();

            // Relationship with Order
            builder.HasOne<OrderEntity>(cartItem => cartItem.Order)
                .WithMany(order => order.Items)
                .HasForeignKey(cartItem => cartItem.OrderID)
                .HasPrincipalKey(order => order.ID)
                .IsRequired();

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
