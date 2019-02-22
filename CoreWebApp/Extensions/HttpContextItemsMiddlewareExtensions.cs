using CoreWebApp.Middleware;
using Microsoft.AspNetCore.Builder;

namespace CoreWebApp.Extensions
{
    public static class HttpContextItemsMiddlewareExtensions
    {
        public static IApplicationBuilder UseHttpContextItems(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HttpContextItemsMiddleware>();
        }
    }
}
