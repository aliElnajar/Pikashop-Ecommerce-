using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PikaShop.Data.Context.ContextEntities.Core;

namespace PikaShop.Data.Context.EntityConfigurations.Core
{
    public class DepartmentEntityConfiguration : IEntityTypeConfiguration<DepartmentEntity>
    {
        public virtual void Configure(EntityTypeBuilder<DepartmentEntity> builder)
        {
            // Mapping
            #region Table & Primary Keys

            builder.ToTable("Departments");
            builder.HasKey(d => d.ID);

            #endregion

            // Data
            #region Data

            builder.Property(d => d.Name).HasColumnType("nvarchar(200)");//.IsRequired();
            builder.Property(d => d.Description).HasColumnType("nvarchar(200)");//.IsRequired();

            builder.Property(d => d.IsDeleted).HasColumnType("bit").HasDefaultValue(false);

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
