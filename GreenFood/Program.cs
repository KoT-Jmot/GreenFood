using GreenFood.Web.Extensions;
using GreenFood.Web.Features;
using GreenFood.Web.ExceptionHandler;
using Serilog;
using FluentValidation;
using System.Reflection;
using Hangfire;
using GreenFood.Application.RequestFeatures;
using GreenFood.Domain.Utils;
using GreenFood.Web.Authorization;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

var configuration = new ConfigurationBuilder()
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                   .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",optional: false, reloadOnChange: true)
                   .Build();

LoggerConfigurator.ConfigureLog(configuration);

builder.Host.UseSerilog();

services.ConfigureSqlServer(configuration)
        .ConfigureHangFire(configuration)
        .AddControllers();

services.AddValidatorsFromAssembly(Assembly.Load("GreenFood.Application"));

services.AddAuthentication();
services.AddAuthorization(options =>
{
    options.AddPolicy("IsNotBlocked", policy =>
        policy.Requirements.Add(new RoleRequirement(AccountRoles.GetBlockedRole)));
});

services.ConfigureIdentity()
        .ConfigureJWT(configuration)
        .ConfigureMapster()
        .ConfigureServices();

var app = await builder.Build().ConfigureMigrationAsync();
await app.InitializeDbContextAsync();

app.UseMiddleware<ExceptionMiddleware>();

await app.ConfigureMigrationAsync();
await app.InitializeHangFireContextAsync();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication()
   .UseAuthorization();

app.UseHangfireDashboard(options: new DashboardOptions
{
    Authorization = new[] { new HangfireAuthorizationFilter() }
});
await app.InitializeHangFireJobStorageAsync();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();