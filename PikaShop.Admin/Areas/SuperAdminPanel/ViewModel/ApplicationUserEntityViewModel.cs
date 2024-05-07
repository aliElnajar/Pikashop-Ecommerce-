using System.ComponentModel.DataAnnotations;

namespace PikaShop.Admin.Areas.SuperAdminPanel.ViewModel
{
	public class ApplicationUserEntityViewModel
	{
		public int Id { get; set; }

		public string UserName { get; set; }

		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[Required]
		[MinLength(3)]
		public string FirstName { get; set; }
		[Required]
		[MinLength(3)]
		public string LastName { get; set; }

		public string? OldPassword { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Confirm password")]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }

		public ApplicationUserEntityViewModel()
		{
			UserName = string.Empty;
			Email = string.Empty;
			FirstName = string.Empty;
			LastName = string.Empty;
		}
	}
}
