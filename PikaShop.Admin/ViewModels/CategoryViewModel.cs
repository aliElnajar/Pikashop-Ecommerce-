#nullable disable

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using PikaShop.Common.Pagination;
using PikaShop.Data.Context.ContextEntities.Core;

namespace PikaShop.Admin.ViewModels
{
    public class CategoryViewModel:AuditViewModel
    {
        [Key]
        [Required]
        [HiddenInput]
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [DisplayName("Department")]
        public int? DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public virtual ICollection<CategorySpecsEntity> CategorySpecs { get; set; }
        public virtual ICollection<CategorySpecsViewModel> CategorySpecifications { get; set; }


    }
}
