using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Buoi3.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Yeu cau nhap ten san pham")]
        [StringLength(100)]
        public string ProductName { get; set; } = string.Empty;
        [Range(0.01, 10000, ErrorMessage = "Gia phai lon hon 0")]
        public decimal ProductPrice { get; set; }
        
        public string Description { get; set; } = string.Empty;

        [ForeignKey("Category")]
        //Forein key to product 
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; } = null;

        //Navigation property to Category
        public Category? Category { get; set; } = null;
    }
}
