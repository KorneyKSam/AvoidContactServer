using AvoidContactServer.Database.Networking.Models;

namespace AvoidContactServer.Networking.Interfaces
{
    public interface ISignCommandsExecutor
    {
        public void TryToSignIn(ushort playerId, string login, string password);
        public void TryToSignUp(ushort playerId, SignedPlayerModel signUpModel);
        public void SignOut(ushort playerId);
    }
}
