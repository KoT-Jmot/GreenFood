using GreenFood.Application.Contracts;
using GreenFood.Application.Services;
using GreenFood.Domain.Models;
using GreenFood.Domain.Utils;
using GreenFood.Infrastructure;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using GreenFood.Infrastructure.Repositories;
using GreenFood.Infrastructure.Configurations;
using Hangfire;

namespace GreenFood.Web.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureSqlServer(
                   this IServiceCollection services,
                   IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(optionsBuilder =>
                optionsBuilder
                    .UseLazyLoadingProxies(true)
                    .UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b =>
                    {
                        b.MigrationsAssembly(Assembly.Load("GreenFood.Infrastructure").FullName);
                    }));

            services.AddScoped<IRepositoryManager, RepositoryManager>();

            return services;
        }
        public static IServiceCollection ConfigureHangFire(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddHangfire(optionsBuilder =>
                optionsBuilder
                    .UseSqlServerStorage(configuration.GetConnectionString("HangFireConnection")));

            services.AddHangfireServer();

            return services;
        }
        public static IServiceCollection ConfigureMapster(
                   this IServiceCollection services)
        {
            var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
            typeAdapterConfig.Scan(Assembly.Load("GreenFood.Application"));

            var mapperConfig = new Mapper(typeAdapterConfig);
            services.AddSingleton<IMapper>(mapperConfig);

            return services;
        }
        public static IServiceCollection ConfigureServices(
                   this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IOrderService, OrderService>();

            return services;
        }

        public static IServiceCollection ConfigureJWT(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = Environment.GetEnvironmentVariable("SECRET");

            services.AddSingleton<JWTConfig>();

            services.AddAuthentication(opt =>
                    {
                        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
                            ValidAudience = jwtSettings.GetSection("validAudience").Value,
                            IssuerSigningKey = new
                            SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!))
                        };
                    });

            return services;
        }

        public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<ApplicationUser>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = true;
                o.Password.RequireUppercase = true;
                o.Password.RequireNonAlphanumeric = true;
                o.Password.RequiredLength = 8;
                o.User.RequireUniqueEmail = true;
            });

            IdentityModelEventSource.ShowPII = true;

            builder = new IdentityBuilder(
                builder.UserType,
                typeof(IdentityRole),
                builder.Services);

            builder.AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();

            builder.AddRoleManager<RoleManager<IdentityRole>>();

            return services;
        }
    }
}
