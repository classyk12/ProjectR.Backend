
using ProjectR.Backend.Application.AppSettings;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
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


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
