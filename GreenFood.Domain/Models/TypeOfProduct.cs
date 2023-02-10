using System.ComponentModel.DataAnnotations;

namespace GreenFood.Domain.Models
{
    public class TypeOfProduct
    {
        [Key]
        [Required]
        public Guid Type_Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public virtual IEnumerable<Product> Products { get; set; } = null!;
    }
}
