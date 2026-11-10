namespace Buoi3.Models
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}
