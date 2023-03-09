using GreenFood.Application.DTO.InputDto;
using GreenFood.Application.DTO.OutputDto;
using GreenFood.Application.RequestFeatures;

namespace GreenFood.Application.Contracts
{
    public interface IUserService
    {
        Task<PagedList<OutputUserDto>> GetAllUsersAsync(
            UserQueryDto userQuery,
            CancellationToken cancellationToken);
        Task<OutputUserDto> GetUserByEmail(string userEmail);
    }
}
