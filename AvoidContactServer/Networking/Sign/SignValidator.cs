using AvoidContactCommon.Sign;
using AvoidContactCommon.Validation;
using AvoidContactServer.Database.Interfaces;

namespace AvoidContactServer.Networking.Sign
{
    internal class SignValidator : IUserSignValidator
    {
        private SignsInfo m_SignedPlayers;
        private ISignDataGetter m_SignDataGetter;
        private CommonSignValidator m_CommonSignValidator;

        public SignValidator(ISignDataGetter loginRepository, SignsInfo signedPlayers)
        {
            m_SignDataGetter = loginRepository;
            m_SignedPlayers = signedPlayers;
            m_CommonSignValidator = new CommonSignValidator();
        }

        public SignInResult CheckSignIn(string login, string password)
        {
            var result = m_CommonSignValidator.CheckSignIn(login, password);

            if (result == SignInResult.Success)
            {
                var modelFromRepository = m_SignDataGetter.TryToGetSignByLogin(login);

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

        public SignUpResult CheckSignUp(SignInfo signInfo)
        {
            var result = m_CommonSignValidator.CheckSignUp(signInfo);
            if (result == SignUpResult.Success)
            {
                if (m_SignDataGetter.CheckEmailUsed(signInfo.Email))
                {
                    return SignUpResult.EmailUsed;
                }

                if (m_SignDataGetter.CheckLoginUsed(signInfo.Login))
                {
                    return SignUpResult.LoginUsed;
                }
            }

            return result;
        }
    }
}
