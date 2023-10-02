using Microsoft.AspNetCore.Http.Extensions;
using Project.Business.Abstract;
using System.Net.Http.Headers;
using System.Web;

namespace Project.Core.Extensions
{
    public class RefreshTokenMiddleware
    {
        RequestDelegate _next;
        IAuthService _authService;
        public RefreshTokenMiddleware(RequestDelegate next,IAuthService authService)
        {
            _next = next;
            _authService = authService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Cookies["X-Access-Token"] is null && context.Request.Cookies["X-Refresh-Token"] is not null)
            {
                var user = _authService.RefreshToken(context.Request.Cookies["X-Refresh-Token"]);
                if (user != null)
                {
                    var token=_authService.CreateAccessToken(user);
                    AppendCookie(context,"X-Access-Token",token.Token,token.Expiration);
                    AppendCookie(context, "X-Refresh-Token", token.RefreshToken,DateTime.Now.AddDays(7));
                    context.Request.Headers.Authorization = $"Bearer {token.Token}";
                }
                else
                {
                    context.Response.Cookies.Append("X-Refresh-Token", "", new CookieOptions { Expires = DateTime.Now.AddDays(-1) });
                }
            }
            await _next(context);
        }
        private void AppendCookie(HttpContext context,string key,string value,DateTime exp)
        {
            context.Response.Cookies.Append(key, value,new CookieOptions { Expires=exp,HttpOnly=true});
        }
    }
}
