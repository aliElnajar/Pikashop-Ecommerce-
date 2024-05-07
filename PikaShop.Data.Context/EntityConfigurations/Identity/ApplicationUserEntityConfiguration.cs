using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PikaShop.Data.Context.ContextEntities.Identity;
using PikaShop.Data.Context.Enums;

namespace PikaShop.Data.Context.EntityConfigurations.Identity
{
    public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUserEntity>
    {
        public virtual void Configure(EntityTypeBuilder<ApplicationUserEntity> builder)
        {
            // Mapping
            builder.HasDiscriminator<UserDiscriminator>("UserType")
                .HasValue<ApplicationUserEntity>(UserDiscriminator.User)
                .HasValue<AdminEntity>(UserDiscriminator.Admin)
                .HasValue<CustomerEntity>(UserDiscriminator.Customer)
                .HasValue<DeliveryPersonEntity>(UserDiscriminator.DeliveryPerson);

            // Data
            builder.Property<string>(nameof(ApplicationUserEntity.FirstName)).HasMaxLength(20);
            builder.Property<string>(nameof(ApplicationUserEntity.LastName)).HasMaxLength(20);
            builder.Property<string>(nameof(ApplicationUserEntity.PhoneNumber)).HasMaxLength(20);

            // Other Configuration
            builder.ToTable(t => t.HasCheckConstraint("CH_UserType",
                $"[UserType] >= {(int)UserDiscriminator.User} AND [UserType] <= " +
                $"{(int)UserDiscriminator.DeliveryPerson}"));
        }
    }
}
