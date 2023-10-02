using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Project.Business.Abstract;
using Project.Models;

namespace Project.Controllers
{
    public class AuthController : Controller
    {
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
        public IActionResult Login([FromBody]UserLoginDto loginDto) 
        {
            var user=_authService.Login(loginDto);
            if(user != null)
            {
                var token=_authService.CreateAccessToken(user);
                AddToCookie("X-Access-Token", token.Token, token.Expiration);
                AddToCookie("X-Refresh-Token", token.RefreshToken,DateTime.Now.AddDays(7));
                return Ok(token);
            }
            return BadRequest();
        }
        [HttpPost]
        public IActionResult Register([FromBody]UserRegisterDto registerDto)
        {
            if (!_authService.IsUserExist(registerDto.Email))
            {
                var user = _authService.Register(registerDto);
                if (user != null)
                {
                    var token = _authService.CreateAccessToken(user);
                    AddToCookie("X-Access-Token", token.Token, token.Expiration);
                    AddToCookie("X-Refresh-Token", token.RefreshToken, DateTime.Now.AddDays(7));
                    return Ok(token);
                }
            }
            return BadRequest();
        }
        public IActionResult RefreshToken(string refreshToken,string returnUrl)
        {
            var user = _authService.RefreshToken(refreshToken);
            if(user != null)
            {
                var token = _authService.CreateAccessToken(user);
                AddToCookie("X-Access-Token", token.Token, token.Expiration);
                AddToCookie("X-Refresh-Token", token.RefreshToken, DateTime.Now.AddDays(7));
                Redirect(returnUrl);
            }
            return Redirect("/");
        }

        private void AddToCookie(string key, string value,DateTimeOffset exp)
        {
            Response.Cookies.Append(key, value, new CookieOptions { Expires = exp, HttpOnly = true });
        }
        
    }
}
