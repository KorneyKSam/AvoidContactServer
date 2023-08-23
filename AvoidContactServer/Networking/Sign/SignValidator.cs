using AvoidContactCommon.Sign;
using AvoidContactCommon.Validation;
using AvoidContactServer.Database.Interfaces;

namespace AvoidContactServer.Networking.Sign
{
    internal class SignValidator : IUserSignValidator
    {
        private ILoginRepository m_LoginRepository;
        private SignsInfo m_SignedPlayers;
        private CommonSignValidator m_CommonSignValidator;

        public SignValidator(ILoginRepository loginRepository, SignsInfo signedPlayers)
        {
            m_LoginRepository = loginRepository;
            m_SignedPlayers = signedPlayers;
            m_CommonSignValidator = new CommonSignValidator();
        }

        public SignInResult CheckSignIn(string login, string password)
        {
            var result = m_CommonSignValidator.CheckSignIn(login, password);

            if (result == SignInResult.Success)
            {
                var modelFromRepository = m_LoginRepository.TryToGetSignedPlayerByLogin(login);

                if (modelFromRepository == null || modelFromRepository.Password != password)
                {
                    return SignInResult.WrongLoginOrPassword;
                }
                else
                {
                    if (m_SignedPlayers.SignedPlayers.Any(p => p.Login == login))
                    {
                        return SignInResult.AccountIsOccupied;
                    }

                    return SignInResult.Success;
                }
            }

            return result;
        }

        public SignUpResult CheckSignUp(SignedPlayerInfo signUpModel)
        {
            var result = m_CommonSignValidator.CheckSignUp(signUpModel);
            if (result == SignUpResult.Success)
            {
                var modelFromRepository = m_LoginRepository.TryToGetSignedPlayerByEmail(signUpModel.Email);
                if (modelFromRepository != null)
                {
                    return SignUpResult.EmailUsed;
                }

                modelFromRepository = m_LoginRepository.TryToGetSignedPlayerByLogin(signUpModel.Login);
                if (modelFromRepository != null)
                {
                    return SignUpResult.LoginUsed;
                }
            }

            return result;
        }
    }
}
