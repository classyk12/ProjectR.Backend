using System.Net;
using System.Net.Mime;
using System.Text.Json;
using ProjectR.Backend.Application.Models;

namespace ProjectR.Backend.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly RequestDelegate _next;
       private static readonly JsonSerializerOptions Options = new()  { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

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
                _logger.LogError(ex, message: ex.Message);
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