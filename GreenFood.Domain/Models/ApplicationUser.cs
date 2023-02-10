using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GreenFood.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string? FullName { get; set; }
        [Required]
        public DateTime RegDate { get; set; }
        public virtual IEnumerable<Product> Products { get; set; } = null!;
        public virtual IEnumerable<Order> Orders { get; set; } = null!;
    }
}
