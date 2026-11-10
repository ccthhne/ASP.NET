using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Buoi3.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }
        //Navigation property to Products
        public ICollection<Product>? Products { get; set; }

    }
}
