using GreenFood.Application.DTO;

namespace GreenFood.Application.Contracts
{
    public interface IAccountService
    {
        public Task<bool> SignUpAsync(UserForRegistrationDto userForRegistration);
        public Task<string> SignInAsync(UserForLoginDto user);
    }
}
