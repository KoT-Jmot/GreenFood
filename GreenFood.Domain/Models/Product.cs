using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenFood.Domain.Models
{
    public class Product
    {
        [Key]
        [Required]
        public int Product_Id { get; set; }
        [Required]
        public ApplicationUser? Saller { get; set; }
        [Required]
        public string? Header { get; set; }
        public string? Description { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Count { get; set; }
        [Required]
        [ForeignKey("Type_Id")]
        public TypeOfProduct? Type {get; set;}
        public IEnumerable<Order>? Orders { get; set; }
    }
}
