using AvoidContactCommon.Sign;
using AvoidContactCommon.Validation;

namespace AvoidContactServer.Database.Interfaces
{
    public interface IUserSignValidator
    {
        public SignInResult CheckSignIn(string login, string password);
        public SignUpResult CheckSignUp(SignedPlayerInfo signedPlayerInfo);
    }
}