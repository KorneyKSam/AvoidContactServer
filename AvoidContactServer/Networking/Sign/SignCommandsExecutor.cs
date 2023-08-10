using AvoidContactServer.Database;
using AvoidContactServer.Database.Interfaces;
using AvoidContactServer.Database.Networking;
using AvoidContactServer.Debugger.Interfaces;
using AvoidContactServer.Networking.Enums.Results;

namespace AvoidContactServer.Networking.Sign
{
    public class SignCommandsExecutor : ISignCommandsExecutor
    {
        private readonly ILoginRepository m_LoginRepository;
        private readonly IUserSignValidator m_UserSignValidator;
        private readonly IDebugger m_MessageLogger;
        private readonly MessageSender m_MessageSender;
        private SignsInfo m_SignsInfo;

        public SignCommandsExecutor(ILoginRepository loginRepository,
                                    IUserSignValidator userLogInValidator,
                                    IDebugger messageLogger,
                                    MessageSender messageSender,
                                    SignsInfo signedPlayers)
        {
            m_LoginRepository = loginRepository;
            m_UserSignValidator = userLogInValidator;
            m_MessageSender = messageSender;
            m_SignsInfo = signedPlayers;
            m_MessageLogger = messageLogger;
        }

        public void TryToSignIn(ushort playerId, string login, string password)
        {
            string authorizationToken = string.Empty;
            var vailidationResult = m_UserSignValidator.CheckSignIn(login, password);

            if (vailidationResult == SignInResult.Success)
            {
                var generatedToken = m_SignsInfo.GeneratedTokens.FirstOrDefault(g => g.Login == login);

                if (generatedToken == null)
                {
                    generatedToken = new GeneratedToken()
                    {
                        AuthorizationToken = Convert.ToBase64String(Guid.NewGuid().ToByteArray()),
                        Login = login
                    };
                    m_SignsInfo.GeneratedTokens.Add(generatedToken);
                }

                authorizationToken = generatedToken.AuthorizationToken;             
            }

            m_MessageSender.SendSignInResult(playerId, vailidationResult, authorizationToken);
        }

        public void TryToSignUp(ushort playerId, SignedPlayerModel signUpModel)
        {
            var validationResult = m_UserSignValidator.CheckSignUp(signUpModel);
            m_MessageSender.SendSignUpResult(playerId, validationResult);
        }

        public void UnlinkPlayerIDAndToken(ushort playerId)
        {
            var foundPlayer = m_SignsInfo.SignedPlayers.FirstOrDefault(p => p.PlayerID == playerId);
            if (foundPlayer != null)
            {
                foundPlayer.PlayerID = default;
                m_SignsInfo.SignedPlayers.Remove(foundPlayer);
                m_SignsInfo.GeneratedTokens.Add(new GeneratedToken()
                {
                    Login = foundPlayer.Login,
                    AuthorizationToken = foundPlayer.AuthorizationToken,
                });
                m_MessageLogger.Log($"Player {foundPlayer.Login} (ID: {playerId}) unauthorized (Token: {foundPlayer.AuthorizationToken}");
            }
        }

        public void LinkPlayerIDAndToken(ushort playerId, string authorizationToken)
        {
            var foundToken = m_SignsInfo.GeneratedTokens.FirstOrDefault(p => p.AuthorizationToken == authorizationToken);
            if (foundToken != null)
            {
                m_SignsInfo.GeneratedTokens.Remove(foundToken);
                m_SignsInfo.SignedPlayers.Add(new SignedPlayer()
                {
                    PlayerID = playerId,
                    Login = foundToken.Login,
                    AuthorizationToken = authorizationToken,
                });
                m_MessageLogger.Log($"Player {foundToken.Login} (ID: {playerId}) authorized (Token: {foundToken.AuthorizationToken}");
            }
        }
    }
}