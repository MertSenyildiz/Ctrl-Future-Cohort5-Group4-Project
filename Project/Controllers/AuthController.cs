using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Project.Business.Abstract;
using Project.Core.Security.Attributes;
using Project.Models;

namespace Project.Controllers
{
    public class AuthController : Controller
    {
        //testing vs
        IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [AnonymousOnly]

        [HttpPost]
        public IActionResult Login(UserLoginDto loginDto,string returnUrl) 
        {
            var user=_authService.Login(loginDto);
            if(user != null)
            {
                var token=_authService.CreateAccessToken(user);
                AddToCookie("X-Access-Token", token.Token, token.Expiration);
                AddToCookie("X-Refresh-Token", token.RefreshToken,DateTime.Now.AddDays(7));
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return Redirect("/");
            }
            ViewData["returnUrl"]=returnUrl;
            return Login(null);
        }
        [AnonymousOnly]
        [HttpPost]
        public IActionResult Register(UserRegisterDto registerDto,string returnUrl)
        {
            if(ModelState.IsValid)
            {
                if (!_authService.IsUserExist(registerDto.Email))
                {
                    var user = _authService.Register(registerDto);
                    if (user != null)
                    {
                        var token = _authService.CreateAccessToken(user);
                        AddToCookie("X-Access-Token", token.Token, token.Expiration);
                        AddToCookie("X-Refresh-Token", token.RefreshToken, DateTime.Now.AddDays(7));
                        if (Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        return Redirect("/");
                    }
                }
            }
            ViewData["returnUrl"] = returnUrl;
            return Register(null);
        }

        public IActionResult RefreshToken(string returnUrl)
        {
            string refreshToken;
            if ((refreshToken = HttpContext.Request.Cookies["X-Refresh-Token"]) is not null){
                var user = _authService.RefreshToken(refreshToken);
                if (user != null)
                {
                    var token = _authService.CreateAccessToken(user);
                    AddToCookie("X-Access-Token", token.Token, token.Expiration);
                    AddToCookie("X-Refresh-Token", token.RefreshToken, DateTime.Now.AddDays(7));
                    if(Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                        
                }
            }
            return Redirect("/");
           
        }

        [Authorize]
        public IActionResult Logout()
        {
            if (HttpContext.User.Identities.Any())
            {
                AddToCookie("X-Access-Token", "", DateTime.Now.AddDays(-1));
                AddToCookie("X-Refresh-Token", "", DateTime.Now.AddDays(-1));
            }
            return Redirect("/");
        }

        [AnonymousOnly]
        public IActionResult Login(string? returnUrl)
        {
            if (ViewData["returnUrl"]==null)
            {
                if (returnUrl is not null)
                {
                    ViewData["returnUrl"] = returnUrl;
                }
                else if (Request.Headers["Referer"].Any())
                {
                    var address = new Uri(Request.Headers["Referer"]);
                    if (address.PathAndQuery != "/Auth/Login")
                        ViewData["returnUrl"] = address.PathAndQuery;
                }
                else
                {
                    ViewData["returnUrl"] = "/";
                }
            }
            return View();
        }

        [AnonymousOnly]
        public IActionResult Register(string? returnUrl)
        {
            if (ViewData["returnUrl"] == null)
            {
                if (returnUrl is not null)
                {
                    ViewData["returnUrl"] = returnUrl;
                }
                else if (Request.Headers["Referer"].Any())
                {
                    var address = new Uri(Request.Headers["Referer"]);
                    if (address.PathAndQuery != "/Auth/Login")
                        ViewData["returnUrl"] = address.PathAndQuery;
                }
                else
                {
                    ViewData["returnUrl"] = "/";
                }
            }
            return View();
        }

        private void AddToCookie(string key, string value,DateTimeOffset exp)
        {
            Response.Cookies.Append(key, value, new CookieOptions { Expires = exp, HttpOnly = true });
        }
        
    }
}
