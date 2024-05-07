
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PikaShop.Data.Context.ContextEntities.Core;
using PikaShop.Data.Context.ContextEntities.Identity;

namespace PikaShop.Data.Context.EntityConfigurations.Core
{
    public class ReviewEntityConfiguration : IEntityTypeConfiguration<ReviewEntity>
    {
        public virtual void Configure(EntityTypeBuilder<ReviewEntity> builder)
        {
            // Mapping
            #region Table & Primary Keys

            builder.ToTable("Reviews");
            builder.HasKey(nameof(ReviewEntity.ProductID), nameof(ReviewEntity.CustomerID));

            #endregion

            #region Relationships

            // Relationship with Product
            builder.HasOne<ProductEntity>(review => review.Product)
                .WithMany()
                .HasForeignKey(review => review.ProductID)
                .HasPrincipalKey(product => product.ID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship with Customer
            builder.HasOne<CustomerEntity>(review => review.Customer)
                .WithMany(customer => customer.Reviews)
                .HasForeignKey(review => review.CustomerID)
                .HasPrincipalKey(customer => customer.Id)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            // Data
            builder.Property<string>(review => review.Comment).HasColumnType("nvarchar(500)");
            builder.Property<byte>(review => review.Rating).HasDefaultValue(0);
            builder.Property<bool>(review => review.IsAnonymous).HasDefaultValue(false);

            #region Audit Configuration

            builder.Property<DateTime>(entity => entity.DateCreated).HasDefaultValueSql("getdate()");
            builder.Property<DateTime>(entity => entity.DateModified).HasDefaultValueSql("getdate()");
            builder.Property<string>(entity => entity.CreatedBy).HasDefaultValue("system");
            builder.Property<string>(entity => entity.ModifiedBy).HasDefaultValue("system");

            #endregion

            // Other Configuration
            builder.ToTable(review => review.HasCheckConstraint("CH_Rating", "[Rating] >= 0 AND [Rating] <= 10"));
        }
    }
}
