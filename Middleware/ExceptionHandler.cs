using System.Net;
using System.Net.Mime;
using System.Text.Json;
using ProjectR.Backend.Application.Models;

namespace ProjectR.Backend.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly RequestDelegate _next;
        private static readonly JsonSerializerOptions Options = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                string serviceName = ex.TargetSite?.DeclaringType?.FullName ?? "UnknownService";
                string methodName = ex.TargetSite?.Name ?? "UnknownMethod";
                _logger.LogError(ex, "Exception in {Service}.{Method}: {Message}", serviceName, methodName, ex.Message);
                await HandleCustomExceptionResponseAsync(context, ex);
            }
        }

        private static async Task HandleCustomExceptionResponseAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            ErrorModel response = new(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString());

            string json = JsonSerializer.Serialize(response, Options);
            await context.Response.WriteAsync(json);
        }
    }
}