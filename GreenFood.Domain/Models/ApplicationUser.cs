using Microsoft.AspNetCore.Identity;

namespace GreenFood.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public DateTime RegDate { get; set; }
        public virtual IEnumerable<Product> Products { get; set; } = null!;
        public virtual IEnumerable<Order> Orders { get; set; } = null!;
    }
}
