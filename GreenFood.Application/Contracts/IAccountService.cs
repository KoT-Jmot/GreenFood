using GreenFood.Application.DTO.InputDto;

namespace GreenFood.Application.Contracts
{
    public interface IAccountService
    {
        Task<string> SignUpAsync(UserForRegistrationDto userForRegistrationDto, CancellationToken cancellationToken = default);
        Task<string> SignInAsync(UserForLoginDto userForLoginDto, CancellationToken cancellationToken = default);
    }
}
