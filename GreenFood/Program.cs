using GreenFood.Web.Extensions;
using GreenFood.Web.Features;
using GreenFood.Web.ExceptionHandler;
using GreenFood.Application.Contracts;
using GreenFood.Application.Services;
using Serilog;
using FluentValidation;
using System.Reflection;
using Microsoft.IdentityModel.Logging;

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
services.AddAuthorization();

services.ConfigureIdentity();
IdentityModelEventSource.ShowPII = true;

services.ConfigureJWT(configuration);

services.ConfigureServices();

services.AddScoped<IAccountService, AccountService>();
services.AddSingleton<IGetFromConfiguration,GetFromConfiguration>();

builder.Host.UseSerilog();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

await app.ConfigureMigrationAsync();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

