using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApp.Middleware
{
    public class HttpContextItemsMiddleware
    {
        private readonly RequestDelegate _next;
        public static readonly object HttpContextItemsMiddlewareKey = new object();

        public HttpContextItemsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext) {
            httpContext.Items[HttpContextItemsMiddlewareKey] = "k-9";

            await _next(httpContext);
        }
    }


    public static class HttpContextItemsMiddlewareExtensions
    {
        public static IApplicationBuilder UseHttpContextItemsMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HttpContextItemsMiddleware>();
        }
    }
}
