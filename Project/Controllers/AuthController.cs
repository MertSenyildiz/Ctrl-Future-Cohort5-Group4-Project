using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Project.Business.Abstract;
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
            return Login();
        }
        [HttpPost]
        public IActionResult Register(UserRegisterDto registerDto)
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
                        return Redirect("/");
                    }
                }
            }
            return Register();
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

        public IActionResult Logout()
        {
            if (HttpContext.User.Identities.Any())
            {
                AddToCookie("X-Access-Token", "", DateTime.Now.AddDays(-1));
                AddToCookie("X-Refresh-Token", "", DateTime.Now.AddDays(-1));
            }
            return Redirect("/");
        }

        public IActionResult Login()
        {
            var address=new Uri(Request.Headers["Referer"]);
            ViewData["returnUrl"]=address.PathAndQuery;
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        private void AddToCookie(string key, string value,DateTimeOffset exp)
        {
            Response.Cookies.Append(key, value, new CookieOptions { Expires = exp, HttpOnly = true });
        }
        
    }
}
