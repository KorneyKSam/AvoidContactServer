using AvoidContactServer.Database.Networking.Enums;
using AvoidContactServer.Database.Networking.Models;
using Riptide;

namespace AvoidContactServer.Database.Networking
{
    internal static class MessageReceiver
    {
        private static LoginCommandExecutor? m_LoginCommandExecutor;

        public static void SetCommandExecutor(LoginCommandExecutor loginCommandExecutor)
        {
            m_LoginCommandExecutor = loginCommandExecutor;
        }

        [MessageHandler((ushort)ClientCommands.SignIn)]
        public static void SignIn(ushort playerId, Message message)
        {
            m_LoginCommandExecutor.SignIn(playerId, GetSignInModel(message));
        }

        [MessageHandler((ushort)ClientCommands.SignUp)]
        public static void SignUp(ushort playerId, Message message)
        {
            m_LoginCommandExecutor.SignUp(playerId, GetSignUpModel(message));
        }

        [MessageHandler((ushort)ClientCommands.SignOut)]
        public static void SignOut(ushort playerId)
        {
            m_LoginCommandExecutor.SignOut(playerId);
        }

        private static SignInModel GetSignInModel(Message message)
        {
            return new SignInModel(message.GetString(), message.GetString());
        }

        private static SignUpModel GetSignUpModel(Message message)
        {
            return new SignUpModel(message.GetString(), message.GetString(), message.GetString());
        }
    }
}