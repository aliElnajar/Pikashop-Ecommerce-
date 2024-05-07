
using PikaShop.Data.Entities.Audit;
using PikaShop.Data.Entities.Enums;

namespace PikaShop.Data.Entities.Core
{
    public class Order : AuditEntity
    {
        public DateTime OrderedAt { get; set; }

        public double Total { get; set; }

        public double PaymentAddedValue { get; set; }

        public bool IsPaid { get; set; }

        public string TransactionID { get; set; } = default!;

        public string Status { get; set; } = default!;

        public PaymentMethods PaymentMethod { get; set; }
    }
}