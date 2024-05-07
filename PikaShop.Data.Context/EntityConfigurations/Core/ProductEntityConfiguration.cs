using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PikaShop.Data.Context.ContextEntities.Core;

namespace PikaShop.Data.Context.EntityConfigurations.Core
{
    public class ProductEntityConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public virtual void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            // Mapping

            #region Table & Primary Keys

            builder.ToTable("Products");
            builder.HasKey(p => p.ID);

            #endregion

            #region Relationships

            // Relationship with Category
            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryID)
                .HasPrincipalKey(c => c.ID).OnDelete(DeleteBehavior.Cascade);
                //.IsRequired();

            #endregion

            // Data
            #region Data

            builder.Property(p => p.Name).HasColumnType("nvarchar(50)");//.IsRequired();
            builder.Property(p => p.Description).HasColumnType("nvarchar(200)");//.IsRequired();
            builder.Property(p => p.Price).HasColumnType("money");//.IsRequired();
            builder.Property(p => p.UnitsInStock).HasColumnType("int");//.IsRequired();

            builder.Property(p => p.IsDeleted).HasColumnType("bit").HasDefaultValue(false);
            builder.Property(p => p.Img).HasColumnType("nvarchar(500)");

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
