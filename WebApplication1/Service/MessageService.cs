using WebApplication1.Interfaces;
using WebApplication1.Static_Links;
namespace WebApplication1.Service
{
    public static class MessageService
    {
        public static IServiceCollection ConfigureServices(
         this IServiceCollection services)
        {
            services.AddScoped<IMessanger, Messanger>();

            return services;
        }
    }
}
