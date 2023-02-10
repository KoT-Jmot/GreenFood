using GreenFood.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace GreenFood.Web.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureSqlServer(
            this IServiceCollection services,
            IConfiguration configuraton)
        {
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(configuraton.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
