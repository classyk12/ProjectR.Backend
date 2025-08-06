using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProjectR.Backend.Application.Interfaces.Managers;
using ProjectR.Backend.Application.Interfaces.Providers;
using ProjectR.Backend.Application.Interfaces.Repository;
using ProjectR.Backend.Application.Interfaces.Utility;
using ProjectR.Backend.Application.Settings;
using ProjectR.Backend.Infrastructure.Managers;
using ProjectR.Backend.Infrastructure.Providers;
using ProjectR.Backend.Infrastructure.Utility;
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
            services.AddScoped<IBusinessRepository, BusinessRepository>();
            services.AddScoped<IOtpRepository, OtpRepository>();
            #endregion

            #region Managers
            services.AddScoped<IAppThemeManager, AppThemeManager>();
            services.AddScoped<IAuthManager, AuthManager>();
            services.AddScoped<IBusinessManager, BusinessManager>();
            services.AddScoped<INotificationManager, NotificationManager>();
            services.AddScoped<IUserManager, UserManager>();
<<<<<<< HEAD
            services.AddScoped<INotificationManager, NotificationManager>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IBusinessManager, BusinessManager>();
=======
            services.AddScoped<IOtpManager, OtpManager>();
>>>>>>> 3c59552164877d07e1a7fbf50da879afceef0a2b
            #endregion

            #region Services
            services.AddScoped<ISlugService, SlugService>();
            services.AddScoped<ICloudinaryService, CloudinaryService>();
            #endregion
        }

        public static void RegisterAuthenticationService(this IServiceCollection services, IConfiguration configuration)
        {
            string key = configuration["Jwt:Key"] ?? throw new Exception($"Configuration 'Jwt:Key' not found.");
            string issuer = configuration["Jwt:Issuer"] ?? throw new Exception($"Configuration 'Jwt:Issuer' not found.");
            string audience = configuration["Jwt:Audience"] ?? throw new Exception($"Configuration 'Jwt:AUdience' not found.");
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
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    ClockSkew = TimeSpan.Zero
                };
            });
            #endregion
        }
        public static void RegisterSwaggerService(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProjectR.Backend", Version = "v1" });

                // Add JWT bearer authorization to Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid JWT token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGci...\""
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        Array.Empty<string>()
                    }
                });
            });
        }

        public static void RegisterDatabaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection")!;
            services.AddDbContext<AppDbContext>(options =>
              options.UseNpgsql(connectionString, options =>
              {
                  options.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(10), errorCodesToAdd: []);
              }));
        }
    }
}
