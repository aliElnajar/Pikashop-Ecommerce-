namespace PikaShop.Admin.ViewModels
{
    public class AuditViewModel
    {
        public DateTime? DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public string? CreatedBy { get; set; }

        public string? ModifiedBy { get; set; }
    }
}
