using GreenFood.Infrastructure;
using GreenFood.Web.Extensions;
using GreenFood.Web.features;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

var configuraton = new ConfigurationBuilder()
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                   .Build();

services.ConfigureSqlServer(configuraton)
        .AddControllers();
LoggerConfigurator.ConfigureLog(configuration);
builder.Host.UseSerilog();

var app = builder.Build();

await app.ConfigureMigrationAsync();

app.UseHttpsRedirection();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

