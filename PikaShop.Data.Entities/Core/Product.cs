
using PikaShop.Data.Entities.Audit;

namespace PikaShop.Data.Entities.Core
{
    public class Product : AuditEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        //public string Specifications { get; set; }
        public double Price { get; set; }

        public int UnitsInStock { get; set; }

        public string? Img {  get; set; }

        public Product()
        {
            Name = string.Empty;
            Description = string.Empty;
        }
    }
}
