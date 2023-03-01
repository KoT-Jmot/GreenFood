using GreenFood.Application.DTO.ServicesDto;
using GreenFood.Domain.Models;
using Mapster;

namespace GreenFood.Application.Mapster
{
    public class ApplicationUserMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<UserForRegistrationDto, ApplicationUser>();
        }
    }
}
