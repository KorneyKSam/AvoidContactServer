using AvoidContactCommon.Sign;
using AvoidContactServer.Database;

namespace AvoidContactServer.Networking.Sign
{
    public interface ISignCommandsExecutor
    {
        public void TryToSignIn(ushort playerId, string login, string password);
        public void TryToSignUp(ushort playerId, SignedPlayerInfo signedPlayerInfo);
        public void UnlinkPlayerIDAndToken(ushort playerId);
        public void LinkPlayerIDAndToken(ushort playerId, string token);
    }
}
