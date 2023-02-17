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
                var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                await db.Database.MigrateAsync();
            }

            return app;
        }
    }
}
