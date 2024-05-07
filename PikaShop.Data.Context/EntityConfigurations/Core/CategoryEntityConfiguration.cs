using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PikaShop.Data.Context.ContextEntities.Core;

namespace PikaShop.Data.Context.EntityConfigurations.Core
{
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<CategoryEntity>
    {
        public virtual void Configure(EntityTypeBuilder<CategoryEntity> builder)
        {
            // Mapping

            #region Table & Primary Keys

            builder.ToTable("Categories");
            builder.HasKey(c => c.ID);

            #endregion

            #region Relationships

            // Relationship with Department
            builder.HasOne(c => c.Department)
                .WithMany(d => d.Categories)
                .HasForeignKey(c => c.DepartmentID)
                .HasPrincipalKey(d => d.ID);
                //.IsRequired();

            #endregion

            // Data
            #region Data

            builder.Property(c => c.Name).HasColumnType("nvarchar(50)");//.IsRequired();
            builder.Property(c => c.Description).HasColumnType("nvarchar(200)");//.IsRequired();

            builder.Property(c => c.IsDeleted).HasColumnType("bit").HasDefaultValue(false);

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
