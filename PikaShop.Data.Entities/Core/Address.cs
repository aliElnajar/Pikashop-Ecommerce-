#nullable disable

using PikaShop.Data.Entities.Audit;

namespace PikaShop.Data.Entities.Core
{
    public class Address : AuditEntity
    {
        public string State { get; set; }

        public string Region { get; set; }

        public string Street { get; set; }

        public string BuildingNumber { get; set; }

        public string FloorNumber { get; set; }

        public string AppartmentNumber { get; set; }
    }
}
