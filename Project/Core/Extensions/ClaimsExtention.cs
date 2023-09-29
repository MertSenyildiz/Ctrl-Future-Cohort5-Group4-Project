using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Project.Core.Extensions
{
    public static class ClaimExtentions
    {
        public static void AddUserName(this ICollection<Claim> claims, string userName)
        {
            claims.Add(new Claim("username", userName));
        }
        public static void AddEmail(this ICollection<Claim> claims, string email)
        {
            claims.Add(new Claim("mail", email));
        }
        public static void AddRole(this ICollection<Claim> claims, string role)
        {
            claims.Add(new Claim("role", role));
        }
        public static void AddNameIdentifier(this ICollection<Claim> claims, string nameIdentifier)
        {
            claims.Add(new Claim("id", nameIdentifier));
        }
    }
}
