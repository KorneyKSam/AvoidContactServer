using AvoidContactCommon.Sign;

namespace AvoidContactServer.Database.Interfaces
{
    public interface ILoginRepository
    {
        public void AddPlayer(SignedPlayerInfo signUpModel);
        public SignedPlayerInfo TryToGetSignedPlayerByLogin(string login);
        public SignedPlayerInfo TryToGetSignedPlayerByEmail(string email);
    }
}