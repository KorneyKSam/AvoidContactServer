using AvoidContactServer.Database;
using AvoidContactServer.Database.Interfaces;
using AvoidContactServer.Networking.Enums.Results;
using System.Text.RegularExpressions;

namespace AvoidContactServer.Networking.Sign
{
    internal class SignValidator : IUserSignValidator
    {
        private const string m_EmailRegexPattern = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                                   @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                                   @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        private ILoginRepository m_LoginRepository;
        private SignsInfo m_SignedPlayers;
        private Regex m_EmailRegex;

        public SignValidator(ILoginRepository loginRepository, SignsInfo signedPlayers)
        {
            m_LoginRepository = loginRepository;
            m_SignedPlayers = signedPlayers;
        }

        public SignInResult CheckSignIn(string login, string password)
        {
            if (!string.IsNullOrEmpty(login) &&
                !string.IsNullOrEmpty(password))
            {
                var modelFromRepository = m_LoginRepository.TryToGetSignedPlayerByLogin(login);
                if (modelFromRepository != null && modelFromRepository.Password == password)
                {
                    if (m_SignedPlayers.SignedPlayers.Any(p => p.Login == login))
                    {
                        return SignInResult.AccountIsOccupied;
                    }

                    return SignInResult.Success;
                }
            }
            return SignInResult.WrongLoginOrPassword;
        }

        public SignUpResult CheckSignUp(SignedPlayerModel signUpModel)
        {
            bool isEmailValid = IsValidEmail(signUpModel.Email);

            if (isEmailValid)
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

            bool isLoginValid = IsValidLogin(signUpModel.Login);
            bool isPasswordValid = IsValidPassword(signUpModel.Password);


            if (!isLoginValid && !isPasswordValid && !isEmailValid)
            {
                return SignUpResult.NotValidLoginAndPasswordAndEmail;
            }

            if (!isLoginValid && !isPasswordValid)
            {
                return SignUpResult.NotValidLoginAndPassword;
            }

            if (!isLoginValid && !isEmailValid)
            {
                return SignUpResult.NotValidLoginAndEmail;
            }

            if (!isPasswordValid && !isEmailValid)
            {
                return SignUpResult.NotValidEmailAndPassword;
            }

            if (!isLoginValid)
            {
                return SignUpResult.NotValidLogin;
            }

            if (!isPasswordValid)
            {
                return SignUpResult.NotValidPassword;
            }

            if (!isEmailValid)
            {
                return SignUpResult.NotValidEmail;
            }

            return SignUpResult.Success;
        }

        private bool IsValidLogin(string login)
        {
            return !string.IsNullOrEmpty(login);
        }

        private bool IsValidPassword(string password)
        {
            return !string.IsNullOrEmpty(password);
        }

        private bool IsValidEmail(string email)
        {
            m_EmailRegex ??= new Regex(m_EmailRegexPattern);
            return !string.IsNullOrEmpty(email) && m_EmailRegex.IsMatch(email);
        }
    }
}
