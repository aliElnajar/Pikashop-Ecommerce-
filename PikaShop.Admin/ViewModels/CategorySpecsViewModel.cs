#nullable disable

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace PikaShop.Admin.ViewModels
{
    public class CategorySpecsViewModel:AuditViewModel
    {
        [Key]
        [Required]
        [HiddenInput]
        public int ID { get; set; }
        public string Key { get; set; }

        public string Value { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public bool Searchable { get; set; }
    }
}
