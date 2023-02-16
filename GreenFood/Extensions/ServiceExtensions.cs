using GreenFood.Application.Contracts;
using GreenFood.Application.Mapster;
using GreenFood.Application.Services;
using GreenFood.Domain.Models;
using GreenFood.Infrastructure;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace GreenFood.Web.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureSqlServer(
                   this IServiceCollection services,
                   IConfiguration configuraton)
        {
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(configuraton.GetConnectionString("DefaultConnection"), b =>
                {
                    b.MigrationsAssembly(Assembly.Load("GreenFood.Infrastructure").FullName);
                }));

            return services;
        }

        public static IServiceCollection ConfigureServices(
                   this IServiceCollection services)
        {
            services
                .AddSingleton(MapperConfig.GetConfiguredMappingConfig())
                .AddSingleton<IUserMapper, UserMapper>();

            services
                .AddScoped<IAccountService, AccountService>()
                .AddScoped<IMapper,ServiceMapper>();

            return services;
        }

        public static void ConfigureJWT(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = Environment.GetEnvironmentVariable("SECRET");
            services.AddAuthentication(opt => {
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
                    SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<ApplicationUser>();
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole),
           builder.Services);
            builder.AddEntityFrameworkStores<ApplicationContext>()
            .AddDefaultTokenProviders();
        }

    }
}
