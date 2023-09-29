using Project.Core.Security.JWT;
using Project.Models;

namespace Project.Business.Abstract
{
    public interface IAuthService
    {
        User Login(UserLoginDto loginDto);
        User Register(UserRegisterDto registerDto);
        AccessToken CreateAccessToken(User user);
        User RefreshToken(string refreshToken);
        bool IsUserExist(string mail);
    }
}
