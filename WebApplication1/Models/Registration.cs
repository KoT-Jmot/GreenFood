using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Registration
    {
        [StringLength(15)]
        public string UserName { get; set; } = null!;


        [StringLength(15)]
        public string Password { get; set; } = null!;


        [DataType(DataType.EmailAddress)]
        [StringLength(35)]
        public string Email { get; set; } = null!;


        [DataType(DataType.PhoneNumber)]
        [StringLength(15)]
        public string PhoneNumber { get; set; } = null!;
    }
}
