using GreenFood.Domain.Utils;
using GreenFood.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GreenFood.Web.Extensions
{
    public static class AppExtensions
    {
        public static async Task<WebApplication> ConfigureMigrationAsync(this WebApplication app)
        {
            await using(var scope = app.Services.CreateAsyncScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                await dbContext.Database.MigrateAsync();

                var hangFireContext = scope.ServiceProvider.GetRequiredService<HangFireContext>();
                await hangFireContext.Database.EnsureCreatedAsync();
            }

            return app;
        }

        public static async Task<WebApplication> InitializeDbContextAsync(this WebApplication app)
        {
            await using (var scope = app.Services.CreateAsyncScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                await DbInitialize.RolesInitialize(roleManager);
            }

            return app;
        }
    }
}
