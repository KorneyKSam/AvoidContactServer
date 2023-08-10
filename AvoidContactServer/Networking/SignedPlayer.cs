namespace AvoidContactServer.Networking
{
    public class SignedPlayer
    {
        public ushort PlayerID { get; set; }
        public string AuthorizationToken { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
    }
}