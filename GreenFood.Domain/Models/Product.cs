using System.ComponentModel.DataAnnotations;

namespace GreenFood.Domain.Models
{
    public class Product
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid User_Id { get; set; }
        [Required]
        public ApplicationUser User { get; set; } = null!;
        [Required]
        public string? Header { get; set; }
        public string? Description { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Count { get; set; }
        [Required]
        public Guid Type_Id { get; set; }
        public TypeOfProduct Type { get; set; } = null!;
        public virtual IEnumerable<Order> Orders { get; set; } = null!;
    }
}
