using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenFood.Domain.Models
{
    public class Order
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid Product_Id { get; set; }
        [Required]
        public Product? Product { get; set; }
        [Required]
        public Guid User_Id { get; set; }
        [Required]
        public ApplicationUser? User { get; set; }
        [Required]
        [Range(0, Int32.MaxValue)]
        public int Count { get; set; }
        [Required]
        public DateTime RegDate { get; set; }

    }
}
