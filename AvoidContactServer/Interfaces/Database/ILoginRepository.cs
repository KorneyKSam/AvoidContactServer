using AvoidContactServer.Database.Networking.Models;

namespace AvoidContactServer.Interfaces.Database
{
    public interface ILoginRepository
    {
        public void AddPlayer(SignedPlayerModel signUpModel);
        public SignedPlayerModel TryToGetSignedPlayerByLogin(string login);
        public SignedPlayerModel TryToGetSignedPlayerByEmail(string email);
    }
}