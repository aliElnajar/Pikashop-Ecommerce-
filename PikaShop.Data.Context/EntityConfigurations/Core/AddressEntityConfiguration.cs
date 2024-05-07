    using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PikaShop.Data.Context.ContextEntities.Identity;
using PikaShop.Data.Entities.ContextEntities.Core;

namespace PikaShop.Data.Context.EntityConfigurations.Core
{
    public class AddressEntityConfiguration : IEntityTypeConfiguration<AddressEntity>
    {
        public virtual void Configure(EntityTypeBuilder<AddressEntity> builder)
        {
            // Mapping
            #region Table & Primary Keys

            builder.ToTable("Addresses");
            builder.HasKey(a => a.ID);

            #endregion

            #region Relationships

            // Relationship with Customer
            builder.HasOne<CustomerEntity>(a => a.Customer)
                .WithMany(c => c.Addresses)
                .HasForeignKey(a => a.CustomerID)
                .HasPrincipalKey(c => c.Id)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            // Data
            #region Data
            builder.Property<string>(nameof(AddressEntity.State)).HasMaxLength(50);
            builder.Property<string>(nameof(AddressEntity.Region)).HasMaxLength(50);
            builder.Property<string>(nameof(AddressEntity.Street)).HasMaxLength(50);
            builder.Property<string>(nameof(AddressEntity.BuildingNumber)).HasMaxLength(10);
            builder.Property<string>(nameof(AddressEntity.FloorNumber)).HasMaxLength(10);
            builder.Property<string>(nameof(AddressEntity.AppartmentNumber)).HasMaxLength(10);

            #region Audit Configuration

            builder.Property<DateTime>(entity => entity.DateCreated).HasDefaultValueSql("getdate()");
            builder.Property<DateTime>(entity => entity.DateModified).HasDefaultValueSql("getdate()");
            builder.Property<string>(entity => entity.CreatedBy).HasDefaultValue("system");
            builder.Property<string>(entity => entity.ModifiedBy).HasDefaultValue("system");

            #endregion

            #endregion

            // Other Configuration
        }
    }
}