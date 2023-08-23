using AvoidContactCommon.Sign;
using AvoidContactCommon.Validation;
using AvoidContactServer.Database.Interfaces;

namespace AvoidContactServer.Networking.Sign
{
    internal class SignValidator : IUserSignValidator
    {
        private SignsInfo m_SignedPlayers;
        private ILoginRepository m_LoginRepository;
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

                if (m_SignedPlayers.SignedPlayers.Any(p => p.Login == login))
                {
                    return SignInResult.AccountIsOccupied;
                }
            }

            return result;
        }

        public SignUpResult CheckSignUp(SignedPlayerInfo signedPlayerInfo)
        {
            var result = m_CommonSignValidator.CheckSignUp(signedPlayerInfo);
            if (result == SignUpResult.Success)
            {
                var modelFromRepository = m_LoginRepository.TryToGetSignedPlayerByEmail(signedPlayerInfo.Email);
                if (modelFromRepository != null)
                {
                    return SignUpResult.EmailUsed;
                }

                modelFromRepository = m_LoginRepository.TryToGetSignedPlayerByLogin(signedPlayerInfo.Login);
                if (modelFromRepository != null)
                {
                    return SignUpResult.LoginUsed;
                }

                m_LoginRepository.AddPlayer(signedPlayerInfo);
            }

            return result;
        }
    }
}
