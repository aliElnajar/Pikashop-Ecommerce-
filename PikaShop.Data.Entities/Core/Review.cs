#nullable disable

using PikaShop.Data.Entities.Audit;

namespace PikaShop.Data.Entities.Core
{
    public class Review : AuditEntity
    {
        public string Comment { get; set; }

        public byte Rating { get; set; }

        public bool IsAnonymous { get; set; }
    }
}
