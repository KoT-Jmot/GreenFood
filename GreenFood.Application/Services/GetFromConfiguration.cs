using GreenFood.Application.Contracts;
using Microsoft.Extensions.Configuration;

namespace GreenFood.Application.Services
{
    public class GetFromConfiguration : IGetFromConfiguration
    {
        private readonly IConfiguration _configuration;

        public GetFromConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfigurationSection GetJWTSettings() => _configuration.GetSection("JwtSettings");
    }
}
