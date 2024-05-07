using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PikaShop.Data.Context.ContextEntities.Core;
using PikaShop.Data.Entities.Enums;

namespace PikaShop.Data.Context.EntityConfigurations.Core
{
    public class OrderEntityConfiguration : IEntityTypeConfiguration<OrderEntity>
    {
        public virtual void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
            // Mapping
            #region Table & Primary Keys

            builder.ToTable("Orders");
            builder.HasKey(order => order.ID);

            #endregion

            // Data
            #region Data

            builder.Property<DateTime>(order => order.OrderedAt).HasDefaultValueSql("getdate()");
            builder.Property<bool>(order => order.IsPaid).HasDefaultValue(false);
            builder.Property<string>(order => order.TransactionID).HasColumnType("nvarchar(450)");
            builder.Property<string>(order => order.Status).HasColumnType("nvarchar(450)");
            builder.Property<PaymentMethods>(order => order.PaymentMethod);

            #region Audit Configuration

            builder.Property<DateTime>(entity => entity.DateCreated).HasDefaultValueSql("getdate()");
            builder.Property<DateTime>(entity => entity.DateModified).HasDefaultValueSql("getdate()");
            builder.Property<string>(entity => entity.CreatedBy).HasDefaultValue("system");
            builder.Property<string>(entity => entity.ModifiedBy).HasDefaultValue("system");

            #endregion

            #endregion

            // Other Configuration
            builder.ToTable(t => t.HasCheckConstraint("CH_PaymentMethod", $"[PaymentMethod] >= {(int)PaymentMethods.CashOnDelivery} AND [PaymentMethod] <= {(int)PaymentMethods.Stripe}"));
        }
    }
}
