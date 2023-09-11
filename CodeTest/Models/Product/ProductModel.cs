using System.ComponentModel.DataAnnotations;

namespace CodeTest.Models.Product
{
    public class ProductModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public bool InStock { get; set; }

    }
}