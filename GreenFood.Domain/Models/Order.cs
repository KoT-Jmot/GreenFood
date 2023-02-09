using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenFood.Domain.Models
{
    public class Order
    {
        [Key]
        [Required]
        public int Order_Id { get; set; }
        [Required]
        [ForeignKey("Product_Id")]
        public Product? Product { get; set; }
        [Required]
        public ApplicationUser? Сustomer { get; set; }
        [Required]
        [Range(0, Int32.MaxValue)]
        public int Count { get; set; }
        [Required]
        public DateTime RegDate { get; set; }

    }
}
