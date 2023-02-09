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
        [Required]
        public DateTime LastLogDate { get; set; }
        public IEnumerable<Product>? Products { get; set; }
        public IEnumerable<Order>? Orders { get; set; }
    }
}
