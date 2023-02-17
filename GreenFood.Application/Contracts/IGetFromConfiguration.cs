using Microsoft.Extensions.Configuration;

namespace GreenFood.Application.Contracts
{
    public interface IGetFromConfiguration
    {
        IConfigurationSection GetJWTSettings();
    }
}
