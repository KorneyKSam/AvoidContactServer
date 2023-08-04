using AvoidContactServer.Database.Networking.Models;
using AvoidContactServer.Networking.Enums.Results;

namespace AvoidContactServer.Database.Interfaces
{
    public interface IUserSignValidator
    {
        public SignInResult CheckSignIn(string login, string password);
        public SignUpResult CheckSignUp(SignedPlayerModel signUpModel);
    }
}