using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using ProjectR.Backend.API.Middleware;
using ProjectR.Backend.Application.AppSettings;
using Serilog;

namespace ProjectR.Backend.API
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

            builder.Configuration
                   .AddJsonFile("appsettings.json", optional: false)
                   .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true);

            builder.Services.Configure<TestSettings>(builder.Configuration.GetSection("ProcessingSettings"));

            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

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