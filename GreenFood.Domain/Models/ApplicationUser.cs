using Microsoft.AspNetCore.Identity;

namespace GreenFood.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime RegistrationDate { get; set; }
        public virtual IEnumerable<Product> Products { get; set; } = null!;
        public virtual IEnumerable<Order> Orders { get; set; } = null!;
    }
}
