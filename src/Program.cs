using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using ProjectR.Backend.Middleware;
using ProjectR.Backend.Persistence.DatabaseContext;
using Serilog;
using ProjectR.Backend.Infrastructure.ServiceConfigurations;

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
            builder.Services.RegisterServices(builder.Configuration);

            builder.Configuration
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables();

            builder.Services.RegisterDatabaseServices(builder.Configuration);
            builder.Services.AddHealthChecks();
            builder.Services.RegisterAuthenticationService(builder.Configuration);
            builder.Services.AddHealthChecks();

            builder.Services.RegisterAuthenticationService(builder.Configuration);

            WebApplication app = builder.Build();

            using (IServiceScope scope = app.Services.CreateScope())
            {
                AppDbContext db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.Migrate();
            }
            
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProjectR Backend");
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
