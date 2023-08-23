using AvoidContactCommon.Sign;
using AvoidContactServer.Networking.Enums.Commands;
using AvoidContactServer.Networking.Sign;
using Riptide;

namespace AvoidContactServer.Database.Networking
{
    internal static class MessageReceiver
    {
        private static ISignCommandsExecutor m_IServerCommandExecutor;

        public static void Initialize(ISignCommandsExecutor serverCommandExecutor)
        {
            m_IServerCommandExecutor = serverCommandExecutor;
        }

        [MessageHandler((ushort)ClientCommands.SignIn)]
        public static void SignIn(ushort playerID, Message message)
        {
            m_IServerCommandExecutor.TryToSignIn(playerID, message.GetString(), message.GetString());
        }

        [MessageHandler((ushort)ClientCommands.SignUp)]
        public static void SignUp(ushort playerID, Message message)
        {
            m_IServerCommandExecutor.TryToSignUp(playerID, CreateSignedPlayerModel(message));
        }

        [MessageHandler((ushort)ClientCommands.SignOut)]
        public static void SignOut(ushort playerID, Message message)
        {
            m_IServerCommandExecutor.UnlinkPlayerIDAndToken(playerID);
        }

        [MessageHandler((ushort)ClientCommands.LinkToken)]
        public static void LinkPlayerIdAndToken(ushort playerID, Message message)
        {
            m_IServerCommandExecutor.LinkPlayerIDAndToken(playerID, message.GetString());
        }

        private static SignedPlayerInfo CreateSignedPlayerModel(Message message)
        {
            return new SignedPlayerInfo()
            {
                Login = message.GetString(),
                Password = message.GetString(),
                Email = message.GetString(),
            };
        }
    }
}