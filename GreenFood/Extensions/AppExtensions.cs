using GreenFood.Domain.Utils;
using GreenFood.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace GreenFood.Web.Extensions
{
    public static class AppExtensions
    {
        public static async Task<WebApplication> ConfigureMigrationAsync(
            this WebApplication app)
        {
            await using(var scope = app.Services.CreateAsyncScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                await dbContext.Database.MigrateAsync();
            }

            return app;
        }
    }
}
