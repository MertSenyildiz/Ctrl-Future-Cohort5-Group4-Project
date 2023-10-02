using Microsoft.AspNetCore.Http.Extensions;

namespace Project.Core.Extensions
{
    public class RefreshTokenMiddleware
    {
        RequestDelegate _next;
        public RefreshTokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Cookies["X-Access-Token"] is null && context.Request.Cookies["X-Refresh-Token"] is not null)
            {
                var returnUrl=context.Request.GetDisplayUrl();
                await Console.Out.WriteLineAsync(returnUrl);
            }
            await _next(context);
        }
    }
}
