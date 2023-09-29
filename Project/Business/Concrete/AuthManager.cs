using Project.Business.Abstract;
using Project.Core.Security.Hashing;
using Project.Core.Security.JWT;
using Project.Models;

namespace Project.Business.Concrete
{
    public class AuthManager:IAuthService
    {
        ITokenHelper _tokenHelper;
        IUserService _userService;
        public AuthManager(ITokenHelper tokenHelper,IUserService userService)
        {
            _tokenHelper = tokenHelper;
            _userService = userService;
        }

        public AccessToken CreateAccessToken(User user)
        {
            var token=_tokenHelper.CreateAccesToken(user);
            user.RefreshToken = token.RefreshToken;
            _userService.Update(user);
            return token;
        }

        public User Login(UserLoginDto loginDto)
        {
            var user=_userService.GetByMail(loginDto.Email);
            if (user == null)
            {
                return null;
            }
            if(HashingHelper.VerifyPasswordHash(loginDto.Password,user.PasswordHash,user.PasswordSalt))
            {
                return user;
            }
            return null;
        }

        public User Register(UserRegisterDto registerDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(registerDto.Password,out passwordHash, out passwordSalt);
            var user = new User
            {
                ID = Guid.NewGuid(),
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Username = registerDto.Username,
                Email = registerDto.Email,
                Role = "User",
                RefreshToken = ""
            };
            _userService.Add(user);
            return user;
        }


        public bool IsUserExist(string mail)
        {
            var user = _userService.GetByMail(mail);
            return user != null?true:false;
        }

        public User RefreshToken(string refreshToken)
        {
            var user = _userService.GetByRefreshToken(refreshToken);
            return user;
        }
    }
}
