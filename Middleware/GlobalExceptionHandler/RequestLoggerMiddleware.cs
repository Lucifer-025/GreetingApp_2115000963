using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using NLog;
using System.Threading.Tasks;

namespace Middleware.ExceptionMiddleware
{
    public class RequestLoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public RequestLoggerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _logger.Info($"Request: {context.Request.Method} {context.Request.Path}");

            await _next(context);

            _logger.Info($"Response: {context.Response.StatusCode}");
        }
    }

    public static class RequestLoggerMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLoggerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLoggerMiddleware>();
        }
    }
}