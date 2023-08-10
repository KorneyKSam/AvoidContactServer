using AvoidContactServer.Database;

namespace AvoidContactServer.Networking.Sign
{
    public interface ISignCommandsExecutor
    {
        public 
            void TryToSignIn(ushort playerId, string login, string password);
        public void TryToSignUp(ushort playerId, SignedPlayerModel signUpModel);
        public void UnlinkPlayerIDAndToken(ushort playerId);
        public void LinkPlayerIDAndToken(ushort playerId, string token);
    }
}
