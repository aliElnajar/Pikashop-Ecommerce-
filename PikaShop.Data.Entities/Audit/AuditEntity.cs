
using PikaShop.Data.Entities.Contracts;

namespace PikaShop.Data.Entities.Audit
{
    public class AuditEntity : IAuditEntity
    {
        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public AuditEntity()
        {
            ModifiedBy = CreatedBy = "system";
            DateCreated = DateModified = DateTime.Now;
        }
    }
}
