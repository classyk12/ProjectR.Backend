using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using ProjectR.Backend.Middleware;
using ProjectR.Backend.Application.AppSettings;
using ProjectR.Backend.Persistence.DatabaseContext;
using Serilog;
using ProjectR.Backend.Application.Interfaces.Repository;
using ProjectR.Backend.Persistence.Repository;
using ProjectR.Backend.Application.Interfaces.Managers;
using ProjectR.Backend.Infrastructure.Managers;

namespace ProjectR.Backend
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            builder.Host.UseSerilog((context, services, loggerConfig) =>
            {
                loggerConfig
                    .MinimumLevel.Debug()
                    .WriteTo.Console()
                    .WriteTo.File("Logs/applog.txt", rollingInterval: RollingInterval.Day);
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region  Repositories
            builder.Services.AddScoped<IAppThemeRepository, AppThemeRepository>();
            #endregion

            #region Managers
            builder.Services.AddScoped<IAppThemeManager, AppThemeManager>();
            #endregion

            builder.Configuration
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables();

            builder.Services.Configure<TestSettings>(builder.Configuration.GetSection("ProcessingSettings"));
            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")!, options =>
            {
                options.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(10), errorCodesToAdd: []);
            }));

            builder.Services.AddHealthChecks();

            WebApplication app = builder.Build();

            using (IServiceScope scope = app.Services.CreateScope())
            {
                AppDbContext db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.Migrate();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Vigipay CrossBorda Admin");
                c.RoutePrefix = string.Empty;
            });

            app.UseSerilogRequestLogging();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.MapHealthChecks("/health", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.Run();
        }
    }
}