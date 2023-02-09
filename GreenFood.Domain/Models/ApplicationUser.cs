using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GreenFood.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        string? FullName { get; set; }
        [Required]
        DateTime CreateDate { get; set; }
        [Required]
        DateTime LastLogDate { get; set; }
    }
}
