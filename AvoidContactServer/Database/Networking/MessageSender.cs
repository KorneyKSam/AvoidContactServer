using AvoidContactServer.Database.Networking.Enums;
using Riptide;

namespace AvoidContactServer.Database.Networking
{
    internal class MessageSender
    {
        private Server m_Server;

        public MessageSender(Server server)
        {
            m_Server = server;
        }

        public void SignIn(ushort playerId, bool success)
        {
            var message = Message.Create(MessageSendMode.Reliable, ServerCommands.SignInResult);
            message.AddBool(success);
            m_Server.Send(message, playerId);
        }

        public void SignUpResult(ushort playerId, SignUpResult result)
        {
            var message = Message.Create(MessageSendMode.Reliable, ServerCommands.SignUpResult);
            message.AddByte((byte)result);
            m_Server.Send(message, playerId);
        }

        public void SignOutResult(ushort playerId, bool success)
        {
            var message = Message.Create(MessageSendMode.Reliable, ServerCommands.SignOutResult);
            message.AddBool(success);
            m_Server.Send(message, playerId);
        }
    }
}