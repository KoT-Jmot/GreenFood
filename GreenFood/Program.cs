using GreenFood.Web.Contracts;
using GreenFood.Web.Exception;
using GreenFood.Web.Extensions;
using GreenFood.Web.Features;
using GreenFood.Web.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

var configuraton = new ConfigurationBuilder()
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                   .Build();

services.ConfigureSqlServer(configuraton)
        .AddControllers();
LoggerConfigurator.ConfigureLog(configuration);
builder.Services.AddScoped<IManagerOperations, ManagerOperations>();

builder.Host.UseSerilog();

var app = builder.Build();

var logger = app.Services.GetService<ILogger<Program>>();
app.ConfigureExceptionHandler(logger);

await app.ConfigureMigrationAsync();

app.UseHttpsRedirection();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

