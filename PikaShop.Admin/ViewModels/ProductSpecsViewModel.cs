#nullable disable

using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
namespace PikaShop.Admin.ViewModels
{
    public class ProductSpecsViewModel:AuditViewModel
    {
        [Key]
        [Required]
        [HiddenInput]
        public int ID { get; set; }
        public string Key { get; set; }

        public string Value { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
    }
}
