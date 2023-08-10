namespace AvoidContactServer.Networking.Sign
{
    public class SignsInfo
    {
        public List<SignedPlayer> SignedPlayers { get; set; } = new();
        public List<GeneratedToken> GeneratedTokens { get; set; } = new();
    }
}