using GreenFood.Application.DTO.ServicesDto;

namespace GreenFood.Application.Contracts
{
    public interface IAccountService
    {
        Task<bool> SignUpAsync(UserForRegistrationDto userForRegistrationDto);
        Task<string> SignInAsync(UserForLoginDto userForLoginDto);
    }
}
