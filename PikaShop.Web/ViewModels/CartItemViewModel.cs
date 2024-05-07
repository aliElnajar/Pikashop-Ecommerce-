namespace PikaShop.Web.ViewModels
{

    public class CartItemViewModel
    {
        public string ProductImage { get; set; }
        public int ProductId { get; set; }
        public int? CustomerId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
    }

}