using AvoidContactCommon.Sign;
using AvoidContactCommon.Sign.Messages;
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

        [MessageHandler((ushort)ClientSignMessage.SignIn)]
        public static void SignIn(ushort playerID, Message message)
        {
            m_IServerCommandExecutor.TryToSignIn(playerID, message.GetString(), message.GetString());
        }

        [MessageHandler((ushort)ClientSignMessage.SignUp)]
        public static void SignUp(ushort playerID, Message message)
        {
            m_IServerCommandExecutor.TryToSignUp(playerID, CreateSignInfo(message));
        }

        [MessageHandler((ushort)ClientSignMessage.SignOut)]
        public static void SignOut(ushort playerID, Message message)
        {
            m_IServerCommandExecutor.UnlinkPlayerIDAndToken(playerID);
        }

        [MessageHandler((ushort)ClientSignMessage.LinkToken)]
        public static void LinkPlayerIdAndToken(ushort playerID, Message message)
        {
            m_IServerCommandExecutor.LinkPlayerIDAndToken(playerID, message.GetString());
        }

        [MessageHandler((ushort)ClientSignMessage.UpdateCommonInfo)]
        public static void UpdateCommonInfo(ushort playerID, Message message)
        {
            m_IServerCommandExecutor.UpdateCommonInfo(playerID, new PlayerInfo() { CallSign = message.GetString(), PlayerDiscription = message.GetString() });
        }

        private static SignInfo CreateSignInfo(Message message)
        {
            return new SignInfo()
            {
                Login = message.GetString(),
                Password = message.GetString(),
                Email = message.GetString(),
            };
        }
    }
}