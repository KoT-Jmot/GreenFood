using GreenFood.Application.DTO;
using GreenFood.Domain.Models;


namespace GreenFood.Application.Mapster
{
    public class UserMapper : IUserMapper
    {
        public ApplicationUser? MapTo(UserForRegistration userDto)
        {
            return userDto == null? null : new ApplicationUser()
            {
                UserName = userDto.FullName,
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber
            };
        }

        public UserForRegistration? MapTo(ApplicationUser user)
        {
            return user == null ? null : new UserForRegistration()
            {
                FullName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };
        }
    }
}
