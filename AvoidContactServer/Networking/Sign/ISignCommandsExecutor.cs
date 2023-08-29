using AvoidContactCommon.Sign;

namespace AvoidContactServer.Networking.Sign
{
    public interface ISignCommandsExecutor
    {
        public void TryToSignIn(ushort playerId, string login, string password);
        public void TryToSignUp(ushort playerId, SignInfo signInfo);
        public void UnlinkPlayerIDAndToken(ushort playerId);
        public void LinkPlayerIDAndToken(ushort playerId, string token);
        public void UpdateCommonInfo(ushort playerId, PlayerInfo playerInfo);
    }
}
