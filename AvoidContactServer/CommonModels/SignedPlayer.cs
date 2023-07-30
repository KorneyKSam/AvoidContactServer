namespace AvoidContactServer.Database.Networking.Models
{
    public class SignedPlayer
    {
        public ushort PlayerId { get; set; }
        public string Login { get; set; } = string.Empty;
    }
}