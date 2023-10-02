using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.Business.Abstract;
using Project.Core.Extensions;
using Project.Core.Helpers.File;
using Project.Core.Security.Hashing;
using Project.Core.Security.JWT;
using Project.DataAccess.Abstract;
using Project.Models;
using System.Diagnostics;
using System.Linq;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ITokenHelper _tokenHelper;
        IUserDal _userDal;
        IUserService _userService;
        IFileHelper _fileHelper;
        public HomeController(ILogger<HomeController> logger,ITokenHelper tokenHelper,IUserDal userDal,IUserService userService,IFileHelper fileHelper)
        {
            _logger = logger;
            _tokenHelper = tokenHelper;
            _userDal = userDal;
            _fileHelper = fileHelper;
            _userService= userService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles ="Student,Admin")]
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
        [HttpGet("kayit")]
        public IActionResult Kayit()
        {
            var password = "sfjsdjsdf";
            byte[] passwordHash,passwordSalt;
            HashingHelper.CreatePasswordHash(password,out passwordHash,out passwordSalt);
            var user = new User
            {
                ID = Guid.NewGuid(),
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Username = "merhaba" + new Random().Next(10000000).ToString(),
                Email = "merhaba@" + new Random().Next(10000000).ToString(),
                Role = "Creator"
            };

            var token = _tokenHelper.CreateAccesToken(user);
            user.RefreshToken= token.RefreshToken;
            _userService.Add(user);
            Response.Cookies.Append("X-Access-Token", token.Token, new CookieOptions { Expires = token.Expiration, HttpOnly = true });
            return Ok(token);
        }

        [HttpPost("RefreshToken")]
        public IActionResult RefreshToken(string token)
        {
            var user = _userDal.Get(u=>u.RefreshToken==token);
            if(user is not null)
            {
                var jwt = _tokenHelper.CreateAccesToken(user);
                user.RefreshToken = jwt.RefreshToken;
                _userDal.Update(user);
                Response.Cookies.Append("X-Access-Token", jwt.Token, new CookieOptions { Expires = jwt.Expiration, HttpOnly = true });
                return Ok(jwt);
            }
            return NotFound();
        }

        [HttpPost("savefile")]
        public async Task<IActionResult> SaveFile([FromForm]IFormFile file)
        {
            var path=await _fileHelper.SaveFileAsync(file);
            return Ok(path);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}