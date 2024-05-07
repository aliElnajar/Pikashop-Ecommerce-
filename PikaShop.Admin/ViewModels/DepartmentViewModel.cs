
#nullable disable

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace PikaShop.Admin.ViewModels
{
    public class DepartmentViewModel: AuditViewModel
    {
        [Key]
        [Required]
        [HiddenInput]
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

    }
}
