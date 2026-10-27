namespace Buoi3.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Action { get; set; }
        //Navigation property to Products
        public List<Product>? Products { get; set; } = new List<Product>();

    }
}
