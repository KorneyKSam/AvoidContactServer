using AvoidContactServer.Database.Networking.Models;

namespace AvoidContactServer.Interfaces.Networking
{
    public interface ISignCommandsExecutor
    {
        public void TryToSignIn(ushort playerId, string login, string password);
        public void TryToSignUp(ushort playerId, SignedPlayerModel signUpModel);
        public void SignOut(ushort playerId);
    }
}
