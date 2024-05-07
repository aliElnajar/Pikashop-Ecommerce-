using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using PikaShop.Data.Entities.Enums;

namespace PikaShop.Admin.ViewModels
{
	public class OrderViewModel
	{
		[Key]
		[Required]
		[HiddenInput]
		public int ID { get; set; }

		public DateTime OrderedAt { get; set; }

		public double Total { get; set; }

		public double PaymentAddedValue { get; set; }

		public bool IsPaid { get; set; }

		public string TransactionID { get; set; } = default!;

		public string Status { get; set; } = default!;

		public PaymentMethods PaymentMethod { get; set; }
	}
}
