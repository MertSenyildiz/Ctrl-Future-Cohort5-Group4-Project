namespace Project.Models
{
    public class User
    {
        public Guid ID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }

        public string Role { get; set; }
        public string RefreshToken { get; set; }
    }
}
