using GreenFood.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GreenFood.Web.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureSqlServer(
            this IServiceCollection services,
            IConfiguration configuraton)
        {
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(configuraton.GetConnectionString("DefaultConnection"), b =>
                {
                    b.MigrationsAssembly(Assembly.Load("GreenFood.Infrastructure").FullName);
                }));

            return services;
        }
    }
}
