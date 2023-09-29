using System.Text;

namespace Project.Core.Security.Hashing
{
    public class HashingHelper
    {
        public static void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
        {
            using var hmac=new System.Security.Cryptography.HMACSHA512();
            salt = hmac.Key;
            hash=hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        public static bool VerifyPasswordHash(string password,byte[] hash,byte[] salt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512(salt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (hash[i] != computedHash[i])
                    return false;
            }
            return true;
        }
            
    }
}
