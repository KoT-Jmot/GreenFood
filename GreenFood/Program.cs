using GreenFood.features;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

ConfigSettings.ConfigureLog(new ConfigurationBuilder());
builder.Host.UseSerilog();

var app = builder.Build();

app.MapGet("/", () => "World!");
app.MapGet("/Home", () => "Home!");
app.Run();

