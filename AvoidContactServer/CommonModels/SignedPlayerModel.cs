namespace AvoidContactServer.Database.Networking.Models
{
    public class SignedPlayerModel
    {
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty; 
    }
}