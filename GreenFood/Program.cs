using GreenFood.Web.Extensions;
using GreenFood.Web.features;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

var configuration = new ConfigurationBuilder()
                   .AddJsonFile("appsettings.json", false, true)
                   .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", false, true)
                   .Build();

services.ConfigureSqlServer(configuration)
        .AddControllers();

LoggerConfigurator.ConfigureLog(configuration);
builder.Host.UseSerilog();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();