using GreenFood.features;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var configuraton = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

LoggerConfig.ConfigureLog(configuraton);
builder.Host.UseSerilog();

var app = builder.Build();

app.MapGet("/", () => "World!");
app.MapGet("/Home", () => "Home!");
app.Run();

