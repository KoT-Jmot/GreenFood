using GreenFood.Web.Extensions;
using GreenFood.Web.Features;
using GreenFood.Web.ExceptionHandler;
using Serilog;
using FluentValidation;
using System.Reflection;
//Hello
var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

var configuration = new ConfigurationBuilder()
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                   .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",optional: false, reloadOnChange: true)
                   .Build();

LoggerConfigurator.ConfigureLog(configuration);

builder.Host.UseSerilog();

services.ConfigureSqlServer(configuration)
        .AddControllers();

services.AddValidatorsFromAssembly(Assembly.Load("GreenFood.Application"));

services.AddAuthentication();
services.AddAuthorization();

services.ConfigureIdentity()
        .ConfigureJWT(configuration)
        .ConfigureMapster()
        .ConfigureServices();

var app = await builder.Build().ConfigureMigrationAsync();
await app.InitializeDbContextAsync();

app.UseMiddleware<ExceptionMiddleware>();

await app.ConfigureMigrationAsync();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication()
   .UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();