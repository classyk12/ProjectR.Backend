using CloudinaryDotNet;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProjectR.Backend.Application.Interfaces.Managers;
using ProjectR.Backend.Application.Interfaces.Providers;
using ProjectR.Backend.Application.Interfaces.Repository;
using ProjectR.Backend.Application.Interfaces.Utility;
using ProjectR.Backend.Application.Settings;
using ProjectR.Backend.Application.Validators;
using ProjectR.Backend.Infrastructure.Managers;
using ProjectR.Backend.Infrastructure.Providers;
using ProjectR.Backend.Infrastructure.Utility;
using ProjectR.Backend.Persistence.DatabaseContext;
using ProjectR.Backend.Persistence.Repository;
using ProjectR.Backend.Shared;
using System.Text;

namespace ProjectR.Backend.Infrastructure.ServiceConfigurations
{
    public static class ServiceCollectionRegistration
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region  Settings
            services.Configure<WhatsappCloudApiSettings>(configuration.GetSection("WhatsApp"));
            services.Configure<GoogleSettings>(configuration.GetSection("Google"));
            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
            services.Configure<TwilioSettings>(configuration.GetSection("Twilio"));
            services.Configure<OtpSettings>(configuration.GetSection("Otp"));
            services.Configure<CloudinarySettings>(configuration.GetSection("Cloudinary"));
            services.Configure<CloudinarySettings>(configuration.GetSection("BusinessAvailability"));
            #endregion

            #region Validators
            services.AddValidatorsFromAssemblyContaining<BusinessAvailabilityValidator>();
            #endregion

            #region  Providers
            services.AddScoped<ISocialAuthProvider, SocialAuthProvider>();
            services.AddScoped<ITwilioProvider, TwilioProvider>();
            services.AddScoped<IWhatsAppProvider, WhatsAppProvider>();
            #endregion

            #region  Repositories
            services.AddScoped<IAppThemeRepository, AppThemeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBusinessRepository, BusinessRepository>();
            services.AddScoped<IOtpRepository, OtpRepository>();
            services.AddScoped<IIndustryRepository, IndustryRepository>();
            #endregion

            #region Managers
            services.AddScoped<IAppThemeManager, AppThemeManager>();
            services.AddScoped<IAuthManager, AuthManager>();
            services.AddScoped<IBusinessManager, BusinessManager>();
            services.AddScoped<INotificationManager, NotificationManager>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<INotificationManager, NotificationManager>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IBusinessManager, BusinessManager>();
            services.AddScoped<IOtpManager, OtpManager>();
            services.AddScoped<IIndustryManager, IndustryManager>();
            services.AddScoped<IAuthManager, AuthManager>();
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
        public static void RegisterHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient(AppConstants.WhatsappTag!, client =>
            {
                client.BaseAddress = new Uri(configuration["WhatsApp:BaseUrl"]!);
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {configuration["WhatsApp:AccessToken"]!}");
            });
        }

        public static void RegisterCloudinaryService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(provider =>
            {
                ILogger<Cloudinary> logger = provider.GetRequiredService<ILogger<Cloudinary>>();
                CloudinarySettings config = provider.GetRequiredService<IOptions<CloudinarySettings>>().Value;
                // Log the configuration (mask sensitive values!)
                logger.LogInformation("Retrieving Cloudinary with CloudName using IOptions: {CloudName}, ApiKey (last 4): {ApiKey}, ApiSecret: {ApiSecret}",
                    config.CloudName,
                    config.ApiKey?.Length > 4 ? config.ApiKey[^4..] : config.ApiKey, // only log last 4 chars
                    "***masked***"
                );

                // Retrieve values from config
                string cloudName = configuration["Cloudinary:CloudName"]!;
                string apiKey = configuration["Cloudinary:ApiKey"]!;
                string apiSecret = configuration["Cloudinary:ApiSecret"]!;

                // Log the configuration (mask sensitive values!)
                logger.LogInformation("Retrieving Cloudinary with CloudName using Config: {CloudName}, ApiKey (last 4): {ApiKey}, ApiSecret: {ApiSecret}",
                    cloudName,
                    apiKey.Length > 4 ? apiKey[^4..] : apiKey, // only log last 4 chars
                    "***masked***"
                );

                // Create the Cloudinary account
                Account account = new(cloudName, apiKey, apiSecret);
                return new Cloudinary(account);
            });
        }
    }
}
