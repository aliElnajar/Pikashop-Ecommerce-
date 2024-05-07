#nullable disable

using PikaShop.Data.Entities.Audit;

namespace PikaShop.Data.Entities.Core
{
    public class Category : AuditEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
