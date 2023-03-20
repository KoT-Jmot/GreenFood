using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Login
    {
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Поля не должны быть пустыми")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Поля не должны быть пустыми")]
        public string Password { get; set; } = null!;
    }
}
