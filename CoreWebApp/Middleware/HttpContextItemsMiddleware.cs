using Microsoft.AspNetCore.Http;
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

}
