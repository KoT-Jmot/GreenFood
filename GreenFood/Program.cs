using GreenFood.Web.Extensions;
using GreenFood.Web.Features;
using GreenFood.Web.ExceptionHandler;
using GreenFood.Application.Contracts;
using GreenFood.Application.Services;
using Serilog;
using FluentValidation;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

var configuration = new ConfigurationBuilder()
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                   .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",optional: false, reloadOnChange: true)
                   .Build();

LoggerConfigurator.ConfigureLog(configuration);

services.ConfigureSqlServer(configuration)
        .AddControllers();

services.AddValidatorsFromAssembly(Assembly.Load("GreenFood.Application"));

services.AddAuthentication();
services.ConfigureIdentity();
services.ConfigureJWT(configuration);

services.ConfigureServices();

builder.Host.UseSerilog();

var app = builder.Build();

var logger = app.Services.GetService<ILogger<Program>>();
app.UseMiddleware<ExceptionMiddleware>();

await app.ConfigureMigrationAsync();

app.UseHttpsRedirection();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

