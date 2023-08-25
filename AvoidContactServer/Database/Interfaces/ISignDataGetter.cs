using AvoidContactCommon.Sign;

namespace AvoidContactServer.Database.Interfaces
{
    public interface ISignDataGetter
    {
        public SignedPlayerInfo TryToGetSignByLogin(string login);
        public bool CheckEmailUsed(string email);
        public bool CheckLoginUsed(string login);
    }
}