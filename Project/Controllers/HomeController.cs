using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.Core.Extensions;
using Project.Core.Security.JWT;
using Project.Models;
using System.Diagnostics;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ITokenHelper _tokenHelper;
        public HomeController(ILogger<HomeController> logger,ITokenHelper tokenHelper)
        {
            _logger = logger;
            _tokenHelper = tokenHelper;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles ="Creator")]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody]User user)
        {
            user.ID = Guid.NewGuid();
            
            var token = _tokenHelper.CreateAccesToken(user);
            Response.Cookies.Append("X-Access-Token", token.Token,new CookieOptions { Expires=token.Expiration,HttpOnly=true});
            return Ok(token);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}