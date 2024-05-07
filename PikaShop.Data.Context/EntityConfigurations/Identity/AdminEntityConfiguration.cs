using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PikaShop.Data.Context.ContextEntities.Identity;

namespace PikaShop.Data.Context.EntityConfigurations.Identity
{
    public class AdminEntityConfiguration : IEntityTypeConfiguration<AdminEntity>
    {
        public virtual void Configure(EntityTypeBuilder<AdminEntity> builder)
        {
            // Mapping

            // Data

            // Other Configuration
        }
    }
}
