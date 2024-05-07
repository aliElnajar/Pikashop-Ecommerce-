using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PikaShop.Data.Context.ContextEntities.Identity;

namespace PikaShop.Data.Context.EntityConfigurations.Identity
{
    public class CustomerEntityConfiguration : IEntityTypeConfiguration<CustomerEntity>
    {
        public virtual void Configure(EntityTypeBuilder<CustomerEntity> builder)
        {
            // Mapping

            // Data

            // Other Configuration
        }
    }
}
