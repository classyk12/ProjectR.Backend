using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ProjectR.Backend.Application.Interfaces.Managers;
using ProjectR.Backend.Application.Interfaces.Providers;
using ProjectR.Backend.Application.Interfaces.Repository;
using ProjectR.Backend.Application.Settings;
using ProjectR.Backend.Infrastructure.Managers;
using ProjectR.Backend.Infrastructure.Providers;
using ProjectR.Backend.Persistence.DatabaseContext;
using ProjectR.Backend.Persistence.Repository;
using System.Text;

namespace ProjectR.Backend.Infrastructure.ServiceConfigurations
{
    public static class ServiceCollectionRegistration
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region  Settings
            services.Configure<GoogleSettings>(configuration.GetSection("Google"));
            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
            services.Configure<TwilioSettings>(configuration.GetSection("Twilio"));
            services.Configure<OtpSettings>(configuration.GetSection("Otp"));
            #endregion

            #region  Providers
            services.AddScoped<ISocialAuthProvider, SocialAuthProvider>();
            services.AddScoped<ITwilioProvider, TwilioProvider>();
            #endregion

            #region  Repositories
            services.AddScoped<IAppThemeRepository, AppThemeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            #endregion

            #region Managers
            services.AddScoped<IAppThemeManager, AppThemeManager>();
            services.AddScoped<IAuthManager, AuthManager>();
            services.AddScoped<INotificationManager, NotificationManager>();
            services.AddScoped<IUserManager, UserManager>();
            #endregion
        }

        public static void RegisterAuthenticationService(this IServiceCollection services, IConfiguration configuration)
        {
            #region Authentication
            services.AddAuthentication(c =>
            {
                c.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                c.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)),
                    ClockSkew = TimeSpan.Zero
                };
            });
            #endregion
        }

        public static void RegisterDatabaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
              options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")!, options =>
              {
                  options.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(10), errorCodesToAdd: []);
              }));
        }
    }
}
