using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PikaShop.Data.Context.ContextEntities.Core;

namespace PikaShop.Data.Context.EntityConfigurations.Core
{
    public class CategorySpecsEntityConfiguration: IEntityTypeConfiguration<CategorySpecsEntity>
    {
        public virtual void Configure(EntityTypeBuilder<CategorySpecsEntity> builder)
        {
            // Mapping
            #region Table & Primary Keys

            builder.ToTable("CategorySpecificationsTemplate");
            builder.HasKey(cs => cs.ID);

            #endregion

            #region Relationships

            // Relationship with Category
            builder.HasOne(cs => cs.Category)
                .WithMany(c => c.CategorySpecs)
                .HasForeignKey(cs => cs.CategoryID)
                .HasPrincipalKey(c => c.ID)
                //.IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            // Data
            #region Data

            builder.Property(ps => ps.Key).HasColumnType("nvarchar(50)");//.IsRequired();
            builder.Property(ps => ps.Value).HasColumnType("nvarchar(300)");//.IsRequired();

            builder.Property(p => p.IsDeleted).HasColumnType("bit").HasDefaultValue(false);

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
