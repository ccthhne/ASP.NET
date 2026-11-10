using Buoi3.Models;

namespace Buoi3.Data
{
    public class DataSeed
    {
        public static List<Category> Categories = new List<Category>
        {
            new Category {CategoryId=1, CategoryName="Laptop"},
            new Category{CategoryId=2, CategoryName="Phone"},
            new Category{CategoryId=3, CategoryName="Tablet"}
        };
        public static List<Product> Products = new List<Product>
        {
            new Product(){ProductId=1, ProductName="Laptop", ProductPrice=1000, CategoryId = 1},
            new Product(){ProductId=2, ProductName="Phone", ProductPrice=500, CategoryId = 2},
            new Product(){ProductId=3, ProductName="Tablet", ProductPrice=300, CategoryId = 3},
        };
    }
}