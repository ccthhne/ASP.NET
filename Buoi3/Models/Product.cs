namespace Buoi3.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }
        
        public string Description { get; set; } = string.Empty;

        //Forein key to product 
        public int CategoryId { get; set; }

        //Navigation property to Category
        public Category? Category { get; set; } = null;
    }
}
