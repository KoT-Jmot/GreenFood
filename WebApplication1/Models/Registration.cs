using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Registration
    {
        [StringLength(15)]
        [Required(ErrorMessage = "Поля не должны быть пустыми")]
        public string UserName { get; set; } = null!;


        [StringLength(15)]
        [Required(ErrorMessage = "Поля не должны быть пустыми")]
        public string Password { get; set; } = null!;


        [DataType(DataType.EmailAddress)]
        [StringLength(35)]
        [Required(ErrorMessage = "Поля не должны быть пустыми")]
        public string Email { get; set; } = null!;


        [DataType(DataType.PhoneNumber)]
        [StringLength(15)]
        [Required(ErrorMessage = "Поля не должны быть пустыми")]
        public string PhoneNumber { get; set; } = null!;
    }
}
