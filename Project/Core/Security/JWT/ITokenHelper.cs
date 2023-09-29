using Project.Models;

namespace Project.Core.Security.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateAccesToken(User user);
    }
}
