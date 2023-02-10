using GreenFood.Web.Extensions;
using GreenFood.Web.features;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

var configuraton = new ConfigurationBuilder()
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                   .Build();

services.ConfigureSqlServer(configuraton)
        .AddControllers();

LoggerConfigurator.ConfigureLog(configuraton);
builder.Host.UseSerilog();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

