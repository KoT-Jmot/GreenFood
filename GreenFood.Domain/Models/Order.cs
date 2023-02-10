using System.ComponentModel.DataAnnotations;

namespace GreenFood.Domain.Models
{
    public class Order
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        [Range(0, Int32.MaxValue)]
        public int Count { get; set; }
        [Required]
        public Guid User_Id { get; set; }
        [Required]
        public ApplicationUser User { get; set; } = null!;
        [Required]
        public Guid Product_Id { get; set; }
        [Required]
        public Product Product { get; set; } = null!;
        [Required]
        public DateTime RegDate { get; set; }
    }
}
