using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenFood.Application.DTO
{
    public class UserForLoginDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

    }
}
