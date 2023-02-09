using GreenFood.Domain.Models;
using GreenFood.Infrastructure;
using GreenFood.Web.features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var configuraton = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(configuraton.GetConnectionString("DefaultConnection")))
    .AddIdentity<ApplicationUser,IdentityRole>()
    .AddEntityFrameworkStores<ApplicationContext>();

builder.Services.AddAuthorization(); // without this line i took this error :
                                     // System.InvalidOperationException: "Unable to find the required services.
                                     // Please add all the required services by calling
                                     // 'IServiceCollection.AddAuthorization'
                                     // in the application startup code."

builder.Services.AddControllersWithViews(); //for mvc

LoggerConfigurator.ConfigureLog(configuraton);
builder.Host.UseSerilog();

var app = builder.Build();

app.UseHttpsRedirection(); //for mvc
app.UseRouting(); //for mvc

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); //for mvc

//app.MapGet("/", () => "World!");
app.Run();

