using AvoidContactServer.Database.Networking;
using AvoidContactServer.Database.Networking.Enums;
using AvoidContactServer.Database.Networking.Models;
using AvoidContactServer.Interfaces.Database;
using AvoidContactServer.Interfaces.Networking;

namespace AvoidContactServer
{
    public class SignCommandsExecutor : ISignCommandsExecutor
    {
        private readonly ILoginRepository m_LoginRepository;
        private readonly IUserSignValidator m_UserSignValidator;
        private readonly MessageSender m_MessageSender;
        private SignedPlayers m_SignedPlayers;

        public SignCommandsExecutor(ILoginRepository loginRepository, 
                                    IUserSignValidator userLogInValidator, 
                                    MessageSender messageSender, 
                                    SignedPlayers signedPlayers)
        {
            m_LoginRepository = loginRepository;
            m_UserSignValidator = userLogInValidator;
            m_MessageSender = messageSender;
            m_SignedPlayers = signedPlayers;
        }

        public void TryToSignIn(ushort playerId, string login, string password)
        {
            var validationResult = m_UserSignValidator.CheckSignIn(login, password);
            if (validationResult == SignInResult.Success)
            {
                m_SignedPlayers.List.Add(new SignedPlayer() { PlayerId = playerId, Login = login });
            }

            m_MessageSender.SendSignInResult(playerId, validationResult);
        }

        public void TryToSignUp(ushort playerId, SignedPlayerModel signUpModel)
        {
            var validationResult = m_UserSignValidator.CheckSignUp(signUpModel);

            if (validationResult == SignUpResult.Success)
            {
                m_LoginRepository.AddPlayer(signUpModel);
            }

            m_MessageSender.SendSignUpResult(playerId, validationResult);
        }

        public void SignOut(ushort playerId)
        {

        }
    }
}