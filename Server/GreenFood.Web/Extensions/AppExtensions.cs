using GreenFood.Application.Contracts;
using GreenFood.Infrastructure.Contexts;
using Hangfire;
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
            }

            return app;
        }

        public static async Task<WebApplication> InitializeDbContextAsync(this WebApplication app)
        {
            await using (var scope = app.Services.CreateAsyncScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                await roleManager.RolesInitialize();
            }

            return app;
        }

        public static async Task<WebApplication> InitializeHangFireContextAsync(this WebApplication app)
        {
            await using (var scope = app.Services.CreateAsyncScope())
            {
                var hangFireContext = scope.ServiceProvider.GetRequiredService<HangFireContext>();
                await hangFireContext.Database.EnsureCreatedAsync();
            }
            return app;
        }

        public static async Task<WebApplication> InitializeHangFireJobStorageAsync(this WebApplication app)
        {
            await using (var scope = app.Services.CreateAsyncScope())
            {
                var hangFireService = scope.ServiceProvider.GetRequiredService<INotificationServices>();

                RecurringJob.AddOrUpdate(() =>
                    hangFireService.DeleteLatestOrdersAsync(), Cron.Daily);
            }

            return app;
        }
    }
}
