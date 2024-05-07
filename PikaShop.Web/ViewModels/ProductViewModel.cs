namespace PikaShop.Web.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
        public int UnitsInStock { get; set; }
        public int? CategoryID { get; set; }
        public double UnitPrice { get; set; }
    }
}