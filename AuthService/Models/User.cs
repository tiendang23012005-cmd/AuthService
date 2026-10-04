namespace AuthService.Models
{
    public class User
    {
        public int IdUser { get; set; }

        public string UserName { get; set; } = "";

        public string Password { get; set; } = "";

        public string? Token { get; set; }
    }
}