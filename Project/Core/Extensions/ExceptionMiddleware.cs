using Microsoft.AspNetCore.Http;
using System.Net;

namespace Project.Core.Extensions
{
    public class ExceptionMiddleware 
    {
        RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next) {
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
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.Redirect("/Home/Error");
            }
        }
    }
}
