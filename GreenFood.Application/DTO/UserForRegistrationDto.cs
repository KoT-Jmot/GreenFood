﻿namespace GreenFood.Application.DTO
{
    public class UserForRegistrationDto
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }
}
