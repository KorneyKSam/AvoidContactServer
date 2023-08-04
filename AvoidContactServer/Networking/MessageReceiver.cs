using AvoidContactServer.Database.Networking.Models;
using AvoidContactServer.Networking.Enums.Commands;
using AvoidContactServer.Networking.Interfaces;
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
        public static void SignIn(ushort playerId, Message message)
        {
            m_IServerCommandExecutor.TryToSignIn(playerId, message.GetString(), message.GetString());
        }

        [MessageHandler((ushort)ClientCommands.SignUp)]
        public static void SignUp(ushort playerId, Message message)
        {
            m_IServerCommandExecutor.TryToSignUp(playerId, CreateSignedPlayerModel(message));
        }

        [MessageHandler((ushort)ClientCommands.SignOut)]
        public static void SignOut(ushort playerId, Message message)
        {
            m_IServerCommandExecutor.SignOut(playerId);
        }

        private static SignedPlayerModel CreateSignedPlayerModel(Message message)
        {
            return new SignedPlayerModel()
            {
                Login = message.GetString(),
                Password = message.GetString(),
                Email = message.GetString(),
            };
        }
    }
}