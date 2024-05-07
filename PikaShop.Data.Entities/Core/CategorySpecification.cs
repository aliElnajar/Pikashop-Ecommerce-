#nullable disable

using PikaShop.Data.Entities.Audit;

namespace PikaShop.Data.Entities.Core
{
    public class CategorySpecification : AuditEntity
    {
        public string Key {  get; set; }

        public string Value { get; set; } = "";
    }
}
