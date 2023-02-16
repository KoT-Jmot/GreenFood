using GreenFood.Application.DTO;
using GreenFood.Domain.Models;
using Mapster;

namespace GreenFood.Application.Mapster
{
    [Mapper]
    public interface IUserMapper
    {
        ApplicationUser? MapTo(UserForRegistration userDto);

        UserForRegistration? MapTo(ApplicationUser user);
    }
}
