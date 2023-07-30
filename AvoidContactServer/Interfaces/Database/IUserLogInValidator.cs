using AvoidContactServer.Database.Networking.Enums;
using AvoidContactServer.Database.Networking.Models;

namespace AvoidContactServer.Interfaces.Database
{
    public interface IUserSignValidator
    {
        public SignInResult CheckSignIn(string login, string password);
        public SignUpResult CheckSignUp(SignedPlayerModel signUpModel);
    }
}