using AdvancedDebugger;
using AvoidContactCommon.Sign;
using AvoidContactCommon.Validation;
using AvoidContactServer.Database.Interfaces;
using AvoidContactServer.Database.Networking;
using AvoidContactServer.Debugging;

namespace AvoidContactServer.Networking.Sign
{
    public class SignCommandsExecutor : ISignCommandsExecutor
    {
        private readonly IUserSignValidator m_UserSignValidator;
        private readonly ISignDataSetter m_SignDataSetter;
        private readonly MessageSender m_MessageSender;
        private SignsInfo m_SignsInfo;

        public SignCommandsExecutor(IUserSignValidator userLogInValidator,
                                    ISignDataSetter signDataSetter,
                                    MessageSender messageSender,
                                    SignsInfo signedPlayers)
        {
            m_UserSignValidator = userLogInValidator;
            m_MessageSender = messageSender;
            m_SignsInfo = signedPlayers;
            m_SignDataSetter = signDataSetter;
        }

        public void TryToSignIn(ushort playerId, string login, string password)
        {
            string authorizationToken = string.Empty;
            var vailidationResult = m_UserSignValidator.CheckSignIn(login, password);

            if (vailidationResult == SignInResult.Success)
            {
                authorizationToken = GetAuthorizationToken(login);
            }

            m_MessageSender.SendSignInResult(playerId, vailidationResult, authorizationToken);
        }

        public void TryToSignUp(ushort playerId, SignedPlayerInfo signedPlayerInfo)
        {
            var validationResult = m_UserSignValidator.CheckSignUp(signedPlayerInfo);
            if (validationResult == SignUpResult.Success)
            {
                m_SignDataSetter.AddPlayer(signedPlayerInfo);
            }
            m_MessageSender.SendSignUpResult(playerId, validationResult);
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
                Debugger.Log($"Player {foundToken.Login} (ID: {playerId}) authorized (Token: {foundToken.AuthorizationToken}", DebuggerLog.InfoDebug);
            }
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
                Debugger.Log($"Player {foundPlayer.Login} (ID: {playerId}) unauthorized (Token: {foundPlayer.AuthorizationToken}", DebuggerLog.InfoDebug);
            }
        }

        private string GetAuthorizationToken(string login)
        {
            string authorizationToken;
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
            return authorizationToken;
        }
    }
}