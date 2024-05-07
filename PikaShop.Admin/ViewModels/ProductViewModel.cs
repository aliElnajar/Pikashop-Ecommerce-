#nullable disable
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using PikaShop.Data.Context.ContextEntities.Core;

namespace PikaShop.Admin.ViewModels
{
    public class ProductViewModel:AuditViewModel
    {
        [Key]
        [Required]
        [HiddenInput]
        public int ID { get; set; }
        public string Name { get; set; } = default!;

        public string Description { get; set; } = default!;

        public double Price { get; set; }

        public int UnitsInStock { get; set; }

        public string Img { get; set; }
        public int? CategoryID { get; set; }
        public string CategoryName { get; set; }
        public virtual ICollection<ProductSpecsEntity> ProductSpecs { get; set; }
        public virtual ICollection<ProductSpecsViewModel> ProductSpecifications { get; set; }
    }
}
