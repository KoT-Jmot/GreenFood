using WebApplication1.Interfaces;
using WebApplication1.Static_Links;

namespace WebApplication1.Service
{
    public static class IMessageService
    {
        public static IServiceCollection ConfigureServices(
          this IServiceCollection services)
        {
            services.AddScoped<IMessage, Message>();

            return services;
        }
    }
}
