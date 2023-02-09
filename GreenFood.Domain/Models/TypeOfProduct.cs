using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenFood.Domain.Models
{
    public class TypeOfProduct
    {
        [Key]
        [Required]
        public int Type_Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public virtual IEnumerable<Product>? Products { get; set; }
    }
}
