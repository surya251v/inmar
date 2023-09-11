using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeTest.Models.Product
{
    public class CartItemModel
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int ProductId { get; set; }
        public int Quantity { get; set; } = 1;

    }
}