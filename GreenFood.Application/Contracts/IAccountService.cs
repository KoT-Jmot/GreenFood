using GreenFood.Application.DTO.InputDto;

namespace GreenFood.Application.Contracts
{
    public interface IAccountService
    {
        Task<string> SignUpAsync(UserForRegistrationDto userForRegistrationDto);
        Task<string> SignInAsync(UserForLoginDto userForLoginDto);
    }
}
