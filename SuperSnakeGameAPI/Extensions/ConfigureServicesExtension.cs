using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SuperSnakeGameAPI.Core.Domain.IdentityEntities;
using SuperSnakeGameAPI.Core.Domain.RepositoryContracts;
using SuperSnakeGameAPI.Core.Helpers;
using SuperSnakeGameAPI.Core.ServiceContracts;
using SuperSnakeGameAPI.Core.Services;
using SuperSnakeGameAPI.Infrastructure.DbContext;
using SuperSnakeGameAPI.Infrastructure.Repositories;

namespace SuperSnakeGameAPI.Web.Extensions
{
    public static class ConfigureServicesExtension
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            services.AddScoped<IPlayersRepository, PlayersRepository>();
            services.AddScoped<IPlayersService, PlayersService>();
            services.AddTransient<IJwtService, JwtService>();

            services.AddApiVersioning(config =>
            {
                config.ApiVersionReader = new UrlSegmentApiVersionReader();
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Default"));
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Super Snake Game API",
                    Version = "1.0"
                });
            });

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = JwtHelpers.GetTokenValidationParameters(configuration);
            });

            services.AddAuthorizationBuilder()
                .AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"))
                .AddDefaultPolicy("AuthenticatedOnly", policy => policy.RequireAuthenticatedUser());

            services.AddHttpLogging(logging =>
            {
                logging.LoggingFields = HttpLoggingFields.RequestMethod
                    | HttpLoggingFields.RequestPath
                    | HttpLoggingFields.ResponseStatusCode
                    | HttpLoggingFields.Duration;
            });
        }
    }
}
