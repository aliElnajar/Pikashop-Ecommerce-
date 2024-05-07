using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PikaShop.Data.Context.ContextEntities.Identity;
using PikaShop.Data.Entities.ContextEntities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PikaShop.Data.Context.ContextEntities.Core;

namespace PikaShop.Data.Context.EntityConfigurations.Core
{
    public class WishListEntityConfiguration : IEntityTypeConfiguration<WishListEntity>
    {
        public virtual void Configure(EntityTypeBuilder<WishListEntity> builder)
        {
            // Mapping
            #region Table & Primary Keys

            builder.ToTable("WishList");
            builder.HasKey(nameof(WishListEntity.ProductID), nameof(WishListEntity.CustomerID));

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
            builder.HasOne<CustomerEntity>(WishList => WishList.Customer)
                .WithMany(customer => customer.WishList)
                .HasForeignKey(WishList => WishList.CustomerID)
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