using GreenFood.Application.DTO;
using GreenFood.Domain.Models;
using Mapster;

namespace GreenFood.Application.Mapster
{
    public class RegisterMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<UserForRegistration,ApplicationUser>()
                .RequireDestinationMemberSource(true);

            config.NewConfig<ApplicationUser, UserForRegistration>()
                .RequireDestinationMemberSource(true);
        }
    }
}
